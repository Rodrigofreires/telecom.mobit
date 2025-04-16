using Microsoft.EntityFrameworkCore;
using Telecom.DataBase;
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

        public Contrato? BuscarContratoPorId(int id) => _context.Contratos.FirstOrDefault(o => o.Id == id);

        public Contrato CriarNovoContrato(ContratoResponse contratoResponse)
        {

            Contrato novoContrato = new Contrato();

            novoContrato.NomeFilial = contratoResponse.NomeFilial;
            novoContrato.ValorMensal = contratoResponse.ValorMensal;
            novoContrato.PlanoContratado = contratoResponse.PlanoContratado;
            novoContrato.OperadoraId = contratoResponse.OperadoraId;
            novoContrato.DataInicio = contratoResponse.DataInicio;
            novoContrato.DataVencimento = contratoResponse.DataVencimento;
            novoContrato.Status = true;

            _context.Contratos.Add(novoContrato);
            _context.SaveChanges();

            return novoContrato;
        }


        public void DeletarContrato(int id) =>
            _context.Contratos.Where(o => o.Id == id).ExecuteDelete();


        public Contrato? AlternarStatusContrato(int id)
        {
            var contrato = _context.Contratos.FirstOrDefault(c => c.Id == id);

            if (contrato == null)
                return null;

            contrato.Status = !contrato.Status;
            _context.SaveChanges();

            return contrato;
        }

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
