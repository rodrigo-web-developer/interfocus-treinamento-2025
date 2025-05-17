using InterfocusConsole.Interfaces;

namespace InterfocusConsole.Models
{
    public class Inscricao : IEntidade
    {
        public long Id { get; set; }
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public DateTime DataInscricao { get; set; } = DateTime.Now;
    }
}

