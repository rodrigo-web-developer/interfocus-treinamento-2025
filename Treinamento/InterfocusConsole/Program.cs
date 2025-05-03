// See https://aka.ms/new-console-template for more information
using InterfocusConsole;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");

var x = Console.ReadLine(); // input()
//x = 123;
Console.WriteLine("Você digitou: {0}", x);

short s = 127;
int i = 2000;
long l = 200000000000000;
bool b = false;
float d = 0.0f;
double e = 1.2;
char c = 'X';
byte b2 = (byte)127;

// tipos por valor
decimal d2 = 1.155555M;
DateTime data = DateTime.Now;
var data2 = new DateTime(2025, 01, 01);
var hoje = DateTime.Today;

if (data2 != data &&
    data2 != hoje ||
    hoje == data)
{
    i += 1;
    i *= 1;
    i -= 1;

    i = i & 123456;
    i = i | 123456;

    var c2 = 1 << 30;

    Console.WriteLine("datas diferentes");
    Console.WriteLine("datas diferentes");
    Console.WriteLine("datas diferentes");
    Console.WriteLine("datas diferentes");
}
else if (true)
{

}
else
{

}

while (false)
{

}
// None

var array = new List<int> { 1, 2, 3 };
List<int> copiaArray = array;

foreach (var item in array)
{

}

for (int it = 0; it < 10; it++)
{
    Console.WriteLine(it);
}

Console.WriteLine(
    "{0} {1} {2} {3} {4}",
    data.Year,
    data
        .Month,
    data
        .Day,
    data
        .DayOfWeek,
    data.Hour);


var copia = i;

copia += 1;

copiaArray.Add(5); // ambas variaveis foram modificadas

Console.WriteLine(copia);
Console.WriteLine(i);

var alunos = new List<Aluno>();
// LINQ - Language Integrated Query
while (true)
{
    Console.WriteLine("1 - cadastrar");
    Console.WriteLine("2 - listar");
    Console.WriteLine("3 - pesquisar");
    Console.Write("Opção: ");
    var opcao = LerInteiro();

    switch (opcao)
    {
        case 1:
            Console.Write("Nome: ");
            var nome = Console.ReadLine();
            Console.Write("Código: ");
            var codigo = Console.ReadLine();

            Console.Write("Desconto: ");
            var desconto = double.Parse(Console.ReadLine());

            var aluno = desconto > 0 ? new Bolsista()
            {
                Nome = nome,
                Codigo = codigo,
                Desconto = desconto
            } : new Aluno()
            {
                Nome = nome,
                Codigo = codigo
            };
            if (ValidarAluno(aluno))
            {
                alunos.Add(aluno);
            }
            else
            {
                Console.WriteLine("Dados inválidos para cadastrar aluno");
            }
            break;
        case 2:
            PrintarLista(alunos);
            break;
        case 3: // label
            var pesquisa = Console.ReadLine();
            var resultado = new List<string>();
            foreach (var item in alunos)
            {
                if (item.Nome.Contains(pesquisa))
                {
                    resultado.Add(item.Nome);
                }
            }
            for (int index = 0; index < resultado.Count; index++)
            {
                // ordena
            }
            // resultado = X { item / item contains "pesquisa" }
            var resultado2 = alunos
                .Where(item => item.Nome.Contains(pesquisa))
                .OrderBy(item => item.Nome)
                .Take(10)
                .ToList();

            PrintarLista(resultado2);
            break;
        case 4:
            Console.Write("Código: ");
            var codigoBusca = Console.ReadLine();

            var alunoPesquisado = alunos
                .Where(e => e.Codigo == codigoBusca)
                .FirstOrDefault();

            if (alunoPesquisado == null)
            {
                Console.WriteLine("Aluno não encontrado");
            }
            else
            {
                Console.Write("Nome: ");
                var novoNome = Console.ReadLine();
                //setnome
                alunoPesquisado.Nome = novoNome;
                //alunoPesquisado.DataCadastro = DateTime.Now;
                Console.WriteLine(alunoPesquisado.DataCadastro);
            }
            break;
    }
}

static void PrintarLista(List<Aluno> lista)
{
    foreach (var item in lista)
    {
        Console.WriteLine(item.GetPrintMessage());
    }
}

static int LerInteiro()
{
    try
    {
        var inteiro = Console.ReadLine();
        return int.Parse(inteiro);
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Texto inválido!");
        return 0;
    }
    //var inteiro = Console.ReadLine();
    //return int.TryParse(inteiro, out int valor) ? valor : 0;
}

static bool ValidarAluno(Aluno aluno)
{
    var validationContext = new ValidationContext(aluno);
    var erros = new List<ValidationResult>();
    var validation = Validator.TryValidateObject(aluno, 
        validationContext, 
        erros,
        true);
    foreach (var erro in erros)
    {
        Console.WriteLine("{0}: {1}", erro.MemberNames.First(), erro.ErrorMessage);
    }
    //throw new Exception("dados invalidos!!!!");
    return validation;
}

//class Program
//{
//    public static void Main()
//    {
//        Console.WriteLine("Hello world!")
//    }
//}