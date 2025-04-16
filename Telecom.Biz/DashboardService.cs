using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telecom.DataBase;

namespace Telecom.Biz
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int ObterTotalFaturas()
        {
            return _context.Faturas.Count();
        }

        public decimal ObterValorTotalFaturado()
        {
            return _context.Faturas.Sum(f => f.ValorCobrado);
        }

        public (int pagas, int pendentes, int atrasadas) ObterDistribuicaoPorStatus()
        {
            int pagas = _context.Faturas.Count(f => f.Status == "Paga");
            int pendentes = _context.Faturas.Count(f => f.Status == "Pendente");
            int atrasadas = _context.Faturas.Count(f => f.Status == "Atrasada");

            return (pagas, pendentes, atrasadas);
        }

        public (string[] meses, int[] emitidas, int[] pagas) ObterEmitidasVsPagas()
        {
            var anoAtual = DateTime.Now.Year;

            var faturasAno = _context.Faturas
                .Where(f => f.DataEmissao.Year == anoAtual)
                .Select(f => new { f.DataEmissao, f.Status })
                .ToList();

            var meses = new[] { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
            var emitidas = new int[12];
            var pagas = new int[12];

            foreach (var fatura in faturasAno)
            {
                int mes = fatura.DataEmissao.Month - 1;

                emitidas[mes]++;

                if (fatura.Status == "Paga")
                    pagas[mes]++;
            }

            return (meses, emitidas, pagas);
        }


    }
}
