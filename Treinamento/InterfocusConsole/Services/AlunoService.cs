using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using InterfocusConsole.Models;
using InterfocusConsole.Repository;

namespace InterfocusConsole.Services
{
    public class AlunoService
    {
        private readonly IRepository repository;

        public AlunoService(IRepository repository)
        {
            this.repository = repository;
        }

        public bool Cadastrar(Aluno aluno, out List<MensagemErro> mensagens)
        {
            var valido = Validar(aluno, out mensagens);
            if (valido)
            {
                try
                {
                    using var transacao = repository.IniciarTransacao();
                    repository.Incluir(aluno);
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

        public static bool Validar(Aluno aluno, out List<MensagemErro> mensagens)
        {
            var validationContext = new ValidationContext(aluno);
            var erros = new List<ValidationResult>();
            var validation = Validator.TryValidateObject(aluno,
                validationContext,
                erros,
                true);

            mensagens = new List<MensagemErro>();
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

            if (aluno.Idade < 16)
            {
                mensagens.Add(new MensagemErro("dataNascimento", "Aluno deve ser maior de 16 anos"));
                validation = false;
            }

            //throw new Exception("dados invalidos!!!!");
            return validation;
        }

        public List<Aluno> Consultar()
        {
            return repository.Consultar<Aluno>().ToList();
        }

        public List<Aluno> Consultar(string pesquisa)
        {
            bool FiltraLista(Aluno item)
            {
                return item.Nome.Contains(pesquisa);
            }
            // lambda expression - LINQ
            var resultado2 = repository
                .Consultar<Aluno>()
                .Where(item => item.Nome.Contains(pesquisa))
                .OrderBy(item => item.Nome)
                .Take(10)
                .ToList();
            return resultado2;
        }

        public Aluno ConsultarPorCodigo(long codigo)
        {
            return repository.ConsultarPorId<Aluno>(codigo);
        }

        public Aluno Editar(Aluno aluno, out List<MensagemErro> mensagens)
        {
            var existente = ConsultarPorCodigo(aluno.Id);

            if (existente == null)
            {
                mensagens = null;
                return null;
            }
            existente.Nome = aluno.Nome;
            existente.Email = aluno.Email;
            existente.Cep = aluno.Cep;

            var valido = Validar(aluno, out mensagens);
            if (valido)
            {
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
            return null;
        }

        public Aluno Deletar(long codigo)
        {
            var existente = ConsultarPorCodigo(codigo);
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
