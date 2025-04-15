using Microsoft.EntityFrameworkCore;
using Telecom.DataBase;
using Telecom.Entities.Request;
using Telecom.Entities.Response;

namespace Telecom.Biz
{
    public class FaturaService
    {
        private readonly ApplicationDbContext _context;


        public FaturaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Criar nova fatura
        public async Task<Fatura> CriarFaturaAsync(FaturaResponse faturaResponse)
        {
            var contrato = await _context.Contratos.FindAsync(faturaResponse.ContratoId);

            if (contrato == null)
            {
                throw new Exception("Contrato não encontrado.");
            }



            var fatura = new Fatura
            {
                ContratoId = faturaResponse.ContratoId,
                Contrato = contrato,
                ValorCobrado = faturaResponse.ValorCobrado,
                Status = faturaResponse.Status,
                DataEmissao = DateTime.UtcNow,
                DataVencimento = DateTime.UtcNow.AddDays(15)
            };

            _context.Faturas.Add(fatura);
            await _context.SaveChangesAsync();

            return fatura;
        }


        // Atualizar fatura
        public async Task<Fatura> AtualizarFaturaAsync(FaturaResponse faturaResponse)
        {
            var fatura = await _context.Faturas.FindAsync(faturaResponse.Id);

            var contrato = await _context.Contratos.FindAsync(faturaResponse.ContratoId);

            if (fatura == null) return null;

            fatura.Contrato = contrato;
            fatura.ContratoId = faturaResponse.ContratoId;
            fatura.ValorCobrado = faturaResponse.ValorCobrado;
            fatura.Status = faturaResponse.Status;

            _context.Faturas.Update(fatura);
            await _context.SaveChangesAsync();

            return fatura;
        }

        // Buscar uma fatura pelo ID
        public async Task<Fatura> ObterFaturaPorIdAsync(int id)
        {
            return await _context.Faturas.FindAsync(id);
        }

        // Listar faturas com base nos filtros
        public async Task<List<Fatura>> ListarFaturasAsync()
        {
            var query = _context.Faturas.AsQueryable();

            return await query.ToListAsync();
        }

        public decimal CalcularTotalGastosMes(DateTime mesReferencia)
        {
            var totalGastos = _context.Faturas
                .Where(f => f.DataEmissao.Month == mesReferencia.Month &&
                            f.DataEmissao.Year == mesReferencia.Year &&
                            (f.Status == "Paga" || f.Status == "Pendente"))
                .Sum(f => f.ValorCobrado);

            return totalGastos;
        }


        public decimal CalcularTotalGastosPorFilial(int contratoId, DateTime mesReferencia)
        {
            var totalGastos = _context.Faturas
                .Where(f => f.Contrato.Id == contratoId &&
                            f.DataEmissao.Month == mesReferencia.Month &&
                            f.DataEmissao.Year == mesReferencia.Year &&
                            (f.Status == "Paga" || f.Status == "Pendente"))
                .Sum(f => f.ValorCobrado);

            return totalGastos;
        }


        public void DeletarOperadora(int id) =>
            _context.Faturas.Where(o => o.Id == id).ExecuteDelete();

    }

}
