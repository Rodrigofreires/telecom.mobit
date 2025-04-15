using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telecom.Biz;
using Telecom.Entities.Request;
using Telecom.Entities.Response;

namespace Telecom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaturaController : ControllerBase
    {
        private readonly FaturaService _faturaService;

        public FaturaController(FaturaService faturaService)
        {
            _faturaService = faturaService;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> GetFaturas()
        {
            var faturas = await _faturaService.ListarFaturasAsync();
            return Ok(faturas);
        }

        
        [HttpGet("buscar/{id}")]
        public async Task<IActionResult> GetFatura(int id)
        {
            var fatura = await _faturaService.ObterFaturaPorIdAsync(id);
            if (fatura == null)
                return NotFound();

            return Ok(fatura);
        }


        [HttpPost("criar")]
        public async Task<IActionResult> PostFatura([FromBody] FaturaResponse faturaResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fatura = await _faturaService.CriarFaturaAsync(faturaResponse);
            return CreatedAtAction(nameof(GetFatura), new { id = fatura.Id }, fatura);
        }


        [HttpPut("editar")]
        public async Task<IActionResult> PutFatura(FaturaResponse faturaResponse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var faturaAtualizada = await _faturaService.AtualizarFaturaAsync(faturaResponse);

            if (faturaAtualizada == null)
                return NotFound();

            return Ok(faturaAtualizada);
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult DeletarOperadora(int id)
        {
            try
            {
                _faturaService.DeletarOperadora(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("total-gastos/{mesReferencia}")]
        public IActionResult CalcularTotalGastosMes(DateTime mesReferencia)
        {
            var totalGastos = _faturaService.CalcularTotalGastosMes(mesReferencia);

            return Ok(new { totalGastos });  
        }

        [HttpGet("gastos-mensais-por-contrato")]
        public IActionResult CalcularGastosPorContrato([FromQuery] int contratoId, [FromQuery] DateTime mesReferencia)
        {
            var total = _faturaService.CalcularTotalGastosPorFilial(contratoId, mesReferencia);
            return Ok(new { contratoId, mesReferencia = mesReferencia.ToString("yyyy-MM"), total });
        }


    }

}
