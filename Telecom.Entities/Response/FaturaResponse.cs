namespace Telecom.Entities.Response
{
    public class FaturaResponse
    {

        public FaturaResponse()
        {
            
        }
        public FaturaResponse(int id, int contratoId, decimal valorCobrado, string status)
        {
            Id = id;
            ContratoId = contratoId;
            ValorCobrado = valorCobrado;
            Status = status;
        }

        public int Id { get; set; }                
        public int ContratoId { get; set; }
        public decimal ValorCobrado { get; set; }  
        public string? Status { get; set; }

    }

}
