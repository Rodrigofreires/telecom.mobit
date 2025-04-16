namespace Telecom.Entities.Response
{
    public class ContratoResponse
    {
        public ContratoResponse(string nomeFilial, int operadoraId, string planoContratado, decimal valorMensal)
        {
            NomeFilial = nomeFilial;
            OperadoraId = operadoraId;
            PlanoContratado = planoContratado;
            ValorMensal = valorMensal;
        }

        public required string NomeFilial { get; set; }
        public int OperadoraId { get; set; }
        public required string PlanoContratado { get; set; }
        public required decimal ValorMensal { get; set; }

    }
}
