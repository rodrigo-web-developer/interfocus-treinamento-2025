using InterfocusConsole.Enums;
using InterfocusConsole.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace InterfocusConsole.Models
{
    // niveis: iniciante, intermediario e avancado
    public class Curso : INomeavel, IEntidade
    {
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        public NivelCurso Nivel { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    
        public void PrintNivel()
        {
            if (Nivel == NivelCurso.Iniciante)
            {
                Console.WriteLine("Iniciante");
            }
            else if (Nivel == NivelCurso.Intermediario)
            {
                Console.WriteLine("Intermediario");
            }
        }
    }
}
