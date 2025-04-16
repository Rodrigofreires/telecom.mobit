namespace Telecom.Entities.Response
{
    public class FaturaResponse
    {

        public FaturaResponse()
        {
            
        }
        public FaturaResponse(int id, int contratoId, decimal valorCobrado, string status, DateTime dataVecimento, DateTime dataEmissao)
        {
            Id = id;
            ContratoId = contratoId;
            ValorCobrado = valorCobrado;
            Status = status;
            DataVencimento =  dataVecimento;
            DataEmissao = dataEmissao;
        }

        public int Id { get; set; }                
        public int ContratoId { get; set; }
        public decimal ValorCobrado { get; set; }  
        public string? Status { get; set; }
        public DateTime DataVencimento {get; set; }

        public DateTime DataEmissao { get; set; }


    }

}
