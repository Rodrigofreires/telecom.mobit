using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Entities.Response
{
    public class ContratoResponse
    {
        public ContratoResponse(string nomeFilial, int operadoraId, string planoContratado, DateTime dataInicio, decimal valorMensal)
        {
            NomeFilial = nomeFilial;
            OperadoraId = operadoraId;
            PlanoContratado = planoContratado;
            DataInicio = dataInicio;
            ValorMensal = valorMensal;
        }

        public required string NomeFilial { get; set; }
        public int OperadoraId { get; set; }
        public required string PlanoContratado { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public required decimal ValorMensal { get; set; }

    }
}
