using InterfocusConsole.Models;
using InterfocusConsole.Repository;
using System.ComponentModel.DataAnnotations;

namespace InterfocusConsole.Services
{
    public class InscricaoService
    {
        private readonly IRepository repository;

        public InscricaoService(IRepository repository)
        {
            this.repository = repository;
        }

        public bool Cadastrar(Inscricao entidade, out List<MensagemErro> mensagens)
        {
            var valido = Validar(entidade, out mensagens);
            if (valido)
            {
                repository.Incluir(entidade);
                return true;
            }
            return false;
        }

        public bool Validar(Inscricao entidade, out List<MensagemErro> mensagens)
        {
            var validationContext = new ValidationContext(entidade);
            var erros = new List<ValidationResult>();

            entidade.Aluno = repository.ConsultarPorId<Aluno>(entidade.Aluno.Id);
            entidade.Curso = repository.ConsultarPorId<Curso>(entidade.Curso.Id);

            var validation = Validator.TryValidateObject(entidade,
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
            //throw new Exception("dados invalidos!!!!");
            return validation;
        }

        public List<Inscricao> Consultar()
        {
            return repository.Consultar<Inscricao>().ToList();
        }

        public List<Inscricao> Consultar(string pesquisa)
        {
            bool FiltraLista(Inscricao item)
            {
                return item.Aluno.Nome.Contains(pesquisa);
            }
            // lambda expression
            var resultado2 = repository
                .Consultar<Inscricao>()
                .Where(item => 
                    item.Aluno.Nome.Contains(pesquisa) ||
                    item.Curso.Nome.Contains(pesquisa)
                )
                .OrderBy(item => item.Id)
                .Take(10)
                .ToList();
            return resultado2;
        }

        public Inscricao ConsultarPorCodigo(long id)
        {
            return repository.ConsultarPorId<Inscricao>(id);
        }

        public Inscricao Deletar(long id)
        {
            var existente = ConsultarPorCodigo(id);
            repository.Excluir(existente);
            return existente;
        }
    }
}
