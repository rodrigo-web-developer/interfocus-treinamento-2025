namespace InterfocusConsole
{
    public class MensagemErro
    {
        public MensagemErro(string propriedade,
            string mensagem)
        {
            Propriedade = propriedade;
            Mensagem = mensagem;
        }

        public string Propriedade { get; set; }
        public string Mensagem { get; set; }
    }
}
