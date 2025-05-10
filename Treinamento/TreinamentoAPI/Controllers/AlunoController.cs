using InterfocusConsole;
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
        public IActionResult Get()
        {
            return Ok(servico.Consultar());
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
            var resultado = servico.Editar(aluno);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(string codigo)
        {
            var resultado = servico.Deletar(codigo);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }
    }
}
