using Microsoft.AspNetCore.Mvc;
using Telecom.Biz;
using Telecom.Entities;
using Telecom.Entities.Request;
using Telecom.Entities.Response;

namespace Telecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoraServicoController : ControllerBase
    {
        private readonly OperadoraServicoService _operadoraServicoService;

        public OperadoraServicoController(OperadoraServicoService operadoraServicoService)
        {
            _operadoraServicoService = operadoraServicoService;
        }


        [HttpGet("listar")]
        public IActionResult ListarTodasAsOperadoras()
        {

            List<OperadoraServico> ListaDeOperadoras = _operadoraServicoService.ListarOperadoras();
            if (ListaDeOperadoras == null)
            {
                return NotFound();
            }
            return Ok(ListaDeOperadoras);
        }


        [HttpGet("listar-nome-operadoras")]
        public IActionResult ListarNomesOperadoras()
        {

            List<NomeOperadoraRequest> ListaDeNomesDasOperadoras = _operadoraServicoService.ListarNomesOperadoras();

            if (ListaDeNomesDasOperadoras == null)
            {
                return NotFound();
            }
            return Ok(ListaDeNomesDasOperadoras);
        }



        [HttpGet("buscar/{id}")]
        public IActionResult BuscarOperadoraPorId(int id)
        {

            var operadoras = _operadoraServicoService.BuscarOperadoraPorId(id);
            if (operadoras == null)
            {
                return NotFound();
            }
            return Ok(operadoras);
        }

        [HttpPost("criar")]
        public IActionResult CriarOperadora([FromBody] OperadoraServicoResponse operadoraResponse)
        {
            if (operadoraResponse is null)
            {
                return BadRequest("Objeto preenchido incorretamente");
            }

            // Criar um novo colaborador usando o serviço
            var novaOperadora = _operadoraServicoService.CriarOperadora(operadoraResponse);

            if (novaOperadora == null)
            {
                return BadRequest("Não foi possível criar a colaboradora");
            }

            // Retornar um código 201 (Created) com o novo colaborador criado
            return Ok(novaOperadora);

        }

        [HttpPut("editar")]
        public IActionResult EditarOperadora([FromBody] OperadoraRequest operadoraRequest)
        {
            if (operadoraRequest == null)
            {
                return BadRequest("Objeto preenchido incorretamente");
            }

            try
            {
                _operadoraServicoService.EditarOperadora(operadoraRequest);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult DeletarOperadora(int id)
        {
            try
            {
                _operadoraServicoService.DeletarOperadora(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }






    }
}
