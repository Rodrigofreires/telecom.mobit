using Microsoft.EntityFrameworkCore;
using Telecom.DataBase;
using Telecom.Entities;
using Telecom.Entities.Request;
using Telecom.Entities.Response;

namespace Telecom.Biz
{
    public class ContratoService
    {

        private readonly ApplicationDbContext _context;


        public ContratoService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Contrato? BuscarContratoPorId(int id) =>  _context.Contratos.FirstOrDefault(o => o.Id == id);

        public Contrato CriarNovoContrato(ContratoResponse contratoResponse)
        {
        
            Contrato novoContrato = new Contrato();

            novoContrato.NomeFilial = contratoResponse.NomeFilial;
            novoContrato.ValorMensal = contratoResponse.ValorMensal;
            novoContrato.PlanoContratado = contratoResponse.PlanoContratado;
            novoContrato.OperadoraId = contratoResponse.OperadoraId;
            novoContrato.DataInicio = contratoResponse.DataInicio;
            novoContrato.DataVencimento = contratoResponse.DataInicio.AddMonths(12);

            _context.Contratos.Add(novoContrato);
            _context.SaveChanges();

            return novoContrato;
        }


        public void DeletarContrato(int id) =>
            _context.Contratos.Where(o => o.Id == id).ExecuteDelete();

        public Contrato EditarContrato(ContratoRequest contratoRequest)
        {

            Contrato? contratoExistente = _context.Contratos
                                            .FirstOrDefault(c => c.Id == contratoRequest.Id);

            if (contratoExistente == null)
            {
                throw new Exception("Contrato não encontrado.");
            }

            contratoExistente.NomeFilial = contratoRequest.NomeFilial;
            contratoExistente.ValorMensal = contratoRequest.ValorMensal;
            contratoExistente.PlanoContratado = contratoRequest.PlanoContratado;
            contratoExistente.OperadoraId = contratoRequest.OperadoraId;
            contratoExistente.DataVencimento = contratoRequest.DataVencimento;

            // Salva as alterações no banco de dados
            _context.Contratos.Update(contratoExistente); 
            _context.SaveChanges();

            return contratoExistente; 
        }


        public List<Contrato> ListarContratos()
        {
            var ListaDeContratos = _context.Contratos.ToList();

            return ListaDeContratos;
        }
    }
}
