namespace InterfocusConsole
{
    public class Bolsista : Aluno
    {
        public double Desconto { get; set; }

        public override string GetPrintMessage()
        {
            return $"{Codigo} - {Nome} ({Desconto}%)";
        }
    }
}
