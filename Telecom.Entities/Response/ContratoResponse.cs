namespace Telecom.Entities.Response
{
    public class ContratoResponse
    {
        public ContratoResponse(string nomeFilial, int operadoraId, string planoContratado, decimal valorMensal, DateTime dataInicio, DateTime dataVencimento)
        {
            NomeFilial = nomeFilial;
            OperadoraId = operadoraId;
            PlanoContratado = planoContratado;
            ValorMensal = valorMensal;
            DataInicio = dataInicio;
            DataVencimento = dataVencimento;
        }

        public required string NomeFilial { get; set; }
        public int OperadoraId { get; set; }
        public required string PlanoContratado { get; set; }
        public required decimal ValorMensal { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataVencimento { get; set; }
    }
}
