using InterfocusConsole.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace InterfocusConsole.Models
{
    public class Inscricao : IEntidade
    {
        public long Id { get; set; }
        [Required]
        public Aluno Aluno { get; set; }
        [Required]
        public Curso Curso { get; set; }
        public DateTime DataInscricao { get; set; } = DateTime.Now;
    }
}

