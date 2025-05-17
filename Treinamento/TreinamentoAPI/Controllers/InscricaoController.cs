using InterfocusConsole;
using InterfocusConsole.Models;
using InterfocusConsole.Services;
using Microsoft.AspNetCore.Mvc;

namespace TreinamentoAPI.Controllers
{
    [Route("api/[controller]")]
    public class InscricaoController : ControllerBase
    {
        private readonly InscricaoService servico;

        public InscricaoController(InscricaoService servico)
        {
            this.servico = servico;
        }

        [HttpGet]
        public IActionResult Get(string pesquisa)
        {
            if (string.IsNullOrWhiteSpace(pesquisa))
            {
                return Ok(servico.Consultar());
            }
            return Ok(servico.Consultar(pesquisa));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Inscricao Inscricao)
        {
            if (servico.Cadastrar(Inscricao, out List<MensagemErro> erros))
            {
                return Ok(Inscricao);
            }
            return UnprocessableEntity(erros);
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

        [HttpGet("[action]")]
        public IActionResult Relatorio()
        {
            var result = servico.RelatorioPorNivel();
            return Ok(result);
        }
    }
}
