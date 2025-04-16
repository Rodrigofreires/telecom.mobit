using Microsoft.EntityFrameworkCore;
using Telecom.DataBase;
using Telecom.Entities;
using Telecom.Entities.Request;
using Telecom.Entities.Response;

namespace Telecom.Biz
{
    public class OperadoraServicoService
    {

        private readonly ApplicationDbContext _context;


        public OperadoraServicoService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public OperadoraServico? BuscarOperadoraPorId(int id) =>
                _context.OperadorasServicos.FirstOrDefault(o => o.Id == id);


        public OperadoraServico CriarOperadora(OperadoraServicoResponse operadoraResponse)
        {
            OperadoraServico novaOperadora = new OperadoraServico();

            novaOperadora.NomeOperadora = operadoraResponse.NomeOperadora;
            novaOperadora.ContatoSuporte = operadoraResponse.ContatoSuporte;
            novaOperadora.TipoServico = operadoraResponse.TipoServico;

            _context.OperadorasServicos.Add(novaOperadora);
            _context.SaveChanges();


            return novaOperadora;
        }

        public void DeletarOperadora(int id) =>
            _context.OperadorasServicos.Where(o => o.Id == id).ExecuteDelete();
        


        public OperadoraServico EditarOperadora(OperadoraRequest operadoraRequest)
        {
            var operadora = _context.OperadorasServicos
                .FirstOrDefault(c => c.Id == operadoraRequest.Id);

            if (operadora == null)
            {
                throw new KeyNotFoundException("Operadora não encontrada");
            }

            //Estruturando classe para receber edições:

            operadora.ContatoSuporte = operadoraRequest.ContatoSuporte;
            operadora.TipoServico = operadoraRequest.TipoServico;
            operadora.NomeOperadora = operadoraRequest.NomeOperadora;

            _context.OperadorasServicos.Update(operadora);
            _context.SaveChanges();

            return operadora;
        }

        public List<NomeOperadoraRequest> ListarNomesOperadoras()
        {
            
            var ListaDeOperadoras = new List<NomeOperadoraRequest>();

            var existeOperadoras = _context.OperadorasServicos.Any();

            if (existeOperadoras)
            {

                var operadoras = _context.OperadorasServicos.ToList();

                foreach (var operadora in operadoras)
                {
                    ListaDeOperadoras.Add(new NomeOperadoraRequest
                    {
                        Id = operadora.Id,
                        NomeOperadora = operadora.NomeOperadora
                    });
                }
            }

            return ListaDeOperadoras;
        }


        public List<OperadoraServico> ListarOperadoras()
        {
            var ListaDeOperadoras = _context.OperadorasServicos.ToList();

            return ListaDeOperadoras;
        }
    }
}
