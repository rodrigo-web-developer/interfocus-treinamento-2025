using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfocusConsole
{
    public class Aluno
    {
        //private string nome;
        //public string GetNome()
        //{
        //    return nome;
        //}
        //public void SetNome(string nome)
        //{
        //    this.nome = nome;
        //}
        [Required, StringLength(10, MinimumLength = 5)]
        public string Codigo { get; set; }
        [Required, MaxLength(50)]
        public string Nome { get; set; }

        public DateTime? DataNascimento { get; set; }

        public int Idade
        {
            get
            {
                if (!DataNascimento.HasValue)
                {
                    return 0;
                }
                var anoAtual = DateTime.Today.Year;
                var idade = anoAtual - DataNascimento.Value.Year;
                var aniversario = DateTime.Today.AddYears(-idade);
                if (DataNascimento > aniversario) idade--;
                return idade;
            }
        }

        public DateTime DataCadastro { get; private set; }
        = DateTime.Now;

        public virtual string GetPrintMessage()
        {
            // getcodigo e getnome
            return $"{Codigo} - {Nome}"; // f"" python
        }
    }
}
