using InterfocusConsole;
using InterfocusConsole.Models;
using InterfocusConsole.Services;
using Microsoft.AspNetCore.Mvc;

namespace TreinamentoAPI.Controllers
{
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly CursoService servico;

        public CursoController(CursoService servico)
        {
            this.servico = servico;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(servico.Consultar());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Curso Curso)
        {
            // CursoService, CursoBusiness
            // controllers: camada de acesso
            // services: camada de negócio
            // repositories: camada de dados
            if (servico.Cadastrar(Curso, out List<MensagemErro> erros))
            {
                return Ok(Curso);
            }
            return UnprocessableEntity(erros);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Curso Curso)
        {
            var resultado = servico.Editar(Curso);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(int codigo)
        {
            var resultado = servico.Deletar(codigo);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var curso = servico.ConsultarPorCodigo(id);
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }
    }
}
