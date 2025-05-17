using InterfocusConsole.Models;

namespace InterfocusConsole
{
    public class Bolsista : Aluno
    {
        public double Desconto { get; set; }

        public override string GetPrintMessage()
        {
            return $"{Id} - {Nome} ({Desconto}%)";
        }
    }
}
