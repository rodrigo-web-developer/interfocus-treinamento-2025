using System.ComponentModel.DataAnnotations;

namespace InterfocusConsole
{
    public class AlunoService
    {
        public AlunoService()
        {
            
        }
        private static List<Aluno> list = new List<Aluno>();

        public bool Cadastrar(Aluno aluno, out List<MensagemErro> mensagens)
        {
            var valido = Validar(aluno, out mensagens);
            if (valido)
            {
                list.Add(aluno);
                return true;
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
            //throw new Exception("dados invalidos!!!!");
            return validation;
        }

        public List<Aluno> Consultar()
        {
            return list.ToList();
        }

        public List<Aluno> Consultar(string pesquisa)
        {
            bool FiltraLista(Aluno item)
            {
                return item.Nome.Contains(pesquisa);
            }
            // lambda expression
            var resultado2 = list
                .Where(item => item.Nome.Contains(pesquisa))
                .OrderBy(item => item.Nome)
                .Take(10)
                .ToList();
            return resultado2;
        }

        public Aluno ConsultarPorCodigo(string codigo)
        {
            return list.FirstOrDefault(a => a.Codigo == codigo);
        }

        public Aluno Editar(Aluno aluno)
        {
            var existente = ConsultarPorCodigo(aluno.Codigo);

            if (existente == null)
            {
                return null;
            }
            existente.Nome = aluno.Nome;
            return existente;
        }

        public Aluno Deletar(string codigo)
        {
            var existente = ConsultarPorCodigo(codigo);
            list.Remove(existente);
            return existente;
        }
    }
}
