using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telecom.Biz;
using Telecom.Entities.Request;
using Telecom.Entities.Response;
using Telecom.Entities;
using Telecom.DataBase;

namespace Telecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly ContratoService _contratoService;

        public ContratoController(ContratoService contratoService)
        {
            _contratoService = contratoService;
        }


        [HttpGet("listar")]
        public IActionResult ListarTodasAsOperadoras()
        {

            List<Contrato> ListaDeOperadoras = _contratoService.ListarContratos();
            if (ListaDeOperadoras == null)
            {
                return NotFound();
            }
            return Ok(ListaDeOperadoras);
        }

        [HttpGet("buscar/{id}")]
        public IActionResult BuscarOperadoraPorId(int id)
        {

            var operadoras = _contratoService.BuscarContratoPorId(id);
            if (operadoras == null)
            {
                return NotFound();
            }
            return Ok(operadoras);
        }

        [HttpPost("criar")]
        public IActionResult CriarContrato([FromBody] ContratoResponse contratoResponse)
        {
            if (contratoResponse is null)
            {
                return BadRequest("Objeto preenchido incorretamente");
            }

            // Criar um novo colaborador usando o serviço
            var novaOperadora = _contratoService.CriarNovoContrato(contratoResponse);

            if (novaOperadora == null)
            {
                return BadRequest("Não foi possível criar o contrato");
            }

            return Ok(novaOperadora);

        }

        [HttpPut("editar")]
        public IActionResult EditarOperadora([FromBody] ContratoRequest contratoRequest)
        {
            if (contratoRequest == null)
            {
                return BadRequest("Objeto preenchido incorretamente");
            }

            try
            {
                Contrato contratoEditado = _contratoService.EditarContrato(contratoRequest);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Delete api/<ColaboradorController>
        [HttpDelete("deletar/{id}")]
        public IActionResult DeletarContrato(int id)
        {
            try
            {
                _contratoService.DeletarContrato(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
