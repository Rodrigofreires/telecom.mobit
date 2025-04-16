namespace Telecom.Entities.Request
{
    public class FaturaRequest
    {

        public FaturaRequest()
        {
            
        }
        public FaturaRequest(int? id, int? contratoId, DateTime? dataEmissao, string status)
        {
            Id = id;
            ContratoId = contratoId;
            DataEmissao = dataEmissao;
            Status = status;
        }

        public int? Id { get; set; }                
        public int? ContratoId { get; set; }        
        public DateTime? DataEmissao { get; set; }  
        public string Status { get; set; }  
    }

}
