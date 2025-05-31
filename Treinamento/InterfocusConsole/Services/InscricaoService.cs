using InterfocusConsole.Dtos;
using InterfocusConsole.Enums;
using InterfocusConsole.Models;
using InterfocusConsole.Repository;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

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
                try
                {
                    using var transacao = repository.IniciarTransacao();
                    repository.Incluir(entidade);
                    repository.Commit();
                    return true;
                }
                catch (Exception)
                {
                    repository.Rollback();
                    return false;
                }
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

            if (entidade.Curso?.Nivel == NivelCurso.Avancado)
            {
                var cursosAvancadosAluno = repository.Consultar<Inscricao>()
                                        .Where(e => e.Curso.Nivel == NivelCurso.Avancado
                                            && e.Aluno.Id == e.Aluno.Id
                                        )
                                        .Count();

                if (cursosAvancadosAluno > 0)
                {
                    mensagens.Add(new MensagemErro("curso", "O aluno só pode ter um curso do nível avançado"));
                    validation = false;
                }

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
                .ToList(); // materializar

            var resultado3 = from item in repository.Consultar<Inscricao>()
                             where item.Aluno.Nome.Contains(pesquisa) ||
                                item.Curso.Nome.Contains(pesquisa)
                            orderby item.Id ascending
                            select item;

            return resultado2;
        }
        // DTO = Data Transfer Object
        public Inscricao ConsultarPorCodigo(long id)
        {
            return repository.ConsultarPorId<Inscricao>(id);
        }

        public Inscricao Deletar(long id)
        {
            var existente = ConsultarPorCodigo(id);
            try
            {
                using var transacao = repository.IniciarTransacao();
                repository.Excluir(existente);
                repository.Commit();
                return existente;
            }
            catch (Exception)
            {
                repository.Rollback();
                return null;
            }
        }

        public RelatorioCurso RelatorioPorNivel()
        {
            var inscricoes = repository.Consultar<Inscricao>().ToList();
            // forma 1
            var result = new RelatorioCurso
            {
                Iniciante = inscricoes.Count(e => e.Curso.Nivel == NivelCurso.Iniciante),
                Intermediario = inscricoes.Count(e => e.Curso.Nivel == NivelCurso.Intermediario),
                Avancado = inscricoes.Count(e => e.Curso.Nivel == NivelCurso.Avancado),
            };

            result = new RelatorioCurso
            {
                Iniciante = repository.Consultar<Inscricao>().Count(e => e.Curso.Nivel == NivelCurso.Iniciante),
                Intermediario = repository.Consultar<Inscricao>().Count(e => e.Curso.Nivel == NivelCurso.Intermediario),
                Avancado = repository.Consultar<Inscricao>().Count(e => e.Curso.Nivel == NivelCurso.Avancado),
            };


            var grupoPorNivel = inscricoes.GroupBy(e => e.Curso.Nivel)
                                .ToDictionary(e => e.Key.ToString(), e => e.Count());

            result = new RelatorioCurso
            {
                Iniciante = grupoPorNivel.ContainsKey("Iniciante") ? grupoPorNivel["Iniciante"] : 0,
                Intermediario = grupoPorNivel.ContainsKey("Intermediario") ? grupoPorNivel["Intermediario"] : 0,
                Avancado = grupoPorNivel.ContainsKey("Avancado") ? grupoPorNivel["Avancado"] : 0,
            };

            return result;
        }
    }
}
