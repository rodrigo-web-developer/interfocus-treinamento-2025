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

        public bool Cadastrar(Curso Curso, out List<MensagemErro> mensagens)
        {
            var valido = Validar(Curso, out mensagens);
            if (valido)
            {
                repository.Incluir(Curso);
                return true;
            }
            return false;
        }

        public static bool Validar(Curso curso, out List<MensagemErro> mensagens)
        {
            var validationContext = new ValidationContext(curso);
            var erros = new List<ValidationResult>();
            var validation = Validator.TryValidateObject(curso,
                validationContext,
                erros,
                true);
            mensagens = new List<MensagemErro>();

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

        public Curso Editar(Curso Curso)
        {
            var existente = ConsultarPorCodigo(Curso.Id);

            if (existente == null)
            {
                return null;
            }
            existente.Nome = Curso.Nome;

            repository.Salvar(existente);
            return existente;
        }

        public Curso Deletar(int id)
        {
            var existente = ConsultarPorCodigo(id);
            repository.Excluir(existente);
            return existente;
        }
    }
}
