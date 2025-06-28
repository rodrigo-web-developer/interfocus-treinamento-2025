using InterfocusConsole;
using InterfocusConsole.Models;
using InterfocusConsole.Services;
using Microsoft.AspNetCore.Mvc;

namespace TreinamentoAPI.Controllers
{
    [Route("api/aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService servico;

        public AlunoController(AlunoService servico)
        {
            this.servico = servico;
        }

        [HttpGet]
        public IActionResult Get(string pesquisa)
        {
            return string.IsNullOrEmpty(pesquisa) ? 
                Ok(servico.Consultar()) :
                Ok(servico.Consultar(pesquisa));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Aluno aluno)
        {
            // AlunoService, AlunoBusiness
            // controllers: camada de acesso
            // services: camada de negócio
            // repositories: camada de dados
            if (servico.Cadastrar(aluno, out List<MensagemErro> erros))
            {
                return Ok(aluno);
            }
            return UnprocessableEntity(erros);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Aluno aluno)
        {
            var resultado = servico.Editar(aluno, out _);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(long codigo)
        {
            var resultado = servico.Deletar(codigo);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpGet("{codigo}")]
        public IActionResult GetById(long codigo)
        {
            var resultado = servico.ConsultarPorCodigo(codigo);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }
    }
}
