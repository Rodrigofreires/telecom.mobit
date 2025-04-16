using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Entities.Request
{
    public class ContratoRequest
    {
        public ContratoRequest(int id, string nomeFilial, int operadoraId, string planoContratado, DateTime dataVencimento, decimal valorMensal, bool status)
        {
            NomeFilial = nomeFilial;
            OperadoraId = operadoraId;
            PlanoContratado = planoContratado;
            DataVencimento = dataVencimento;
            ValorMensal = valorMensal;
            Id = id;
            Status = status; 
        }

        public int Id { get; set; }
        public required string NomeFilial { get; set; }
        public int OperadoraId { get; set; }
        public required string PlanoContratado { get; set; }
        public DateTime DataVencimento { get; set; }
        public required decimal ValorMensal { get; set; }
        public bool Status {  get; set; }


    }
}
