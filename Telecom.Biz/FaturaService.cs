using Microsoft.EntityFrameworkCore;
using Telecom.DataBase;
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
        public async Task<List<Fatura>> CriarFaturasMensaisAsync(FaturaResponse faturaResponse)
        {
            var contrato = await _context.Contratos.FindAsync(faturaResponse.ContratoId);

            if (contrato == null)
            {
                throw new Exception("Contrato não encontrado.");
            }

            var mesesRestantes = (contrato.DataVencimento.Year - contrato.DataInicio.Year) * 12 + contrato.DataVencimento.Month - contrato.DataInicio.Month;

            if (mesesRestantes <= 0)
            {
                throw new Exception("O contrato não tem meses restantes ou já passou da data de vencimento.");
            }

            var valorMensal = contrato.ValorMensal / mesesRestantes;

            var faturas = new List<Fatura>();
            for (int i = 0; i < mesesRestantes; i++)
            {
                var fatura = new Fatura
                {
                    ContratoId = faturaResponse.ContratoId,
                    Contrato = contrato,
                    ValorCobrado = valorMensal,
                    Status = "Pendente",
                    DataEmissao = DateTime.UtcNow,
                    DataVencimento = contrato.DataInicio.AddMonths(i + 1) // Data de vencimento de cada fatura
                };

                faturas.Add(fatura);
            }

            // Adiciona as faturas no contexto e salva no banco
            _context.Faturas.AddRange(faturas);
            await _context.SaveChangesAsync();

            return faturas;
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

        public decimal CalcularTotalGastosOperadora(int operadoraId, int ano, int mes)
        {

            var total = _context.Faturas
                .Include(f => f.Contrato) 
                .ThenInclude(c => c.OperadoraServico) 
                .Where(f => f.Contrato.OperadoraId == operadoraId
                            && f.DataEmissao.Year == ano
                            && f.DataEmissao.Month == mes)
                .Sum(f => f.ValorCobrado);

            return total;
        }
    }

}
