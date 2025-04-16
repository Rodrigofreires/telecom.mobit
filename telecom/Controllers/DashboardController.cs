using Microsoft.AspNetCore.Mvc;
using Telecom.Biz;

namespace Telecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("indicadores")]
        public IActionResult GetIndicadores()
        {
            var totalFaturas = _dashboardService.ObterTotalFaturas();
            var valorTotal = _dashboardService.ObterValorTotalFaturado();

            return Ok(new { totalFaturas, valorTotal });
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var (pagas, pendentes, atrasadas) = _dashboardService.ObterDistribuicaoPorStatus();

            return Ok(new { pagas, pendentes, atrasadas });
        }

        [HttpGet("mensal")]
        public IActionResult GetMensal()
        {
            var (meses, emitidas, pagas) = _dashboardService.ObterEmitidasVsPagas();

            return Ok(new { meses, emitidas, pagas });
        }
    }
}
