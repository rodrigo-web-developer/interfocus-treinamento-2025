using System.ComponentModel.DataAnnotations;
using InterfocusConsole.Enums;
using InterfocusConsole.Models;
using InterfocusConsole.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InterfocusConsole.Services
{
    public class CursoService
    {
        public CursoService(IRepository repository)
        {
            this.repository = repository;
        }

        private readonly IRepository repository;

        public bool Cadastrar(Curso curso, out List<MensagemErro> mensagens)
        {
            var valido = Validar(curso, out mensagens);
            if (valido)
            {
                try
                {
                    using var transacao = repository.IniciarTransacao();
                    repository.Incluir(curso);
                    repository.Commit();
                    return true;
                }
                catch (Exception)
                {
                    repository.Rollback();
                    return false;
                }
            }
            return false;
        }

        public bool Validar(Curso curso, out List<MensagemErro> mensagens)
        {
            var validationContext = new ValidationContext(curso);
            var erros = new List<ValidationResult>();
            var validation = Validator.TryValidateObject(curso,
                validationContext,
                erros,
                true);
            mensagens = new List<MensagemErro>();

            var cursosNome = repository.Consultar<Curso>()
                            .Where(e => e.Nome == curso.Nome)
                            .Count();

            if (cursosNome > 0)
            {
                mensagens.Add(new MensagemErro("nome", "Já existe um curso com esse nome"));
                validation = false;
            }

            if (!Enum.IsDefined<NivelCurso>(curso.Nivel))
            {
                var mensagem = new MensagemErro(
                    "nivel",
                    "Nível inválido, deve ser 0, 1 ou 2.");

                mensagens.Add(mensagem);
                validation = false;
            }

            foreach (var erro in erros)
            {
                var mensagem = new MensagemErro(
                    erro.MemberNames.First(),
                    erro.ErrorMessage);

                mensagens.Add(mensagem);
                Console.WriteLine("{0}: {1}",
                    erro.MemberNames.First(),
                    erro.ErrorMessage);
            }
            //throw new Exception("dados invalidos!!!!");
            return validation;
        }

        public List<Curso> Consultar()
        {
            return repository.Consultar<Curso>().ToList();
        }

        public List<Curso> Consultar(string pesquisa)
        {
            bool FiltraLista(Curso item)
            {
                return item.Nome.Contains(pesquisa);
            }
            // lambda expression
            var resultado2 = repository
                .Consultar<Curso>()
                .Where(item => item.Nome.Contains(pesquisa))
                .OrderBy(item => item.Nome)
                .Take(10)
                .ToList();
            return resultado2;
        }

        public Curso ConsultarPorCodigo(long id)
        {
            return repository.ConsultarPorId<Curso>(id);
        }

        public Curso Editar(Curso curso)
        {
            var existente = ConsultarPorCodigo(curso.Id);

            if (existente == null)
            {
                return null;
            }
            existente.Nome = curso.Nome;
            existente.Descricao = curso.Descricao;
            try
            {
                using var transacao = repository.IniciarTransacao();
                repository.Salvar(existente);
                repository.Commit();
                return existente;
            }
            catch (Exception)
            {
                repository.Rollback();
                return null;
            }
        }

        public Curso Deletar(int id)
        {
            var existente = ConsultarPorCodigo(id);

            try
            {
                using var transacao = repository.IniciarTransacao();
                repository.Excluir(existente);
                repository.Commit();
                return existente;
            }
            catch (Exception)
            {
                repository.Rollback();
                return null;
            }
        }
    }
}
