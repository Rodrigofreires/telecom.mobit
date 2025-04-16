namespace Telecom.Entities.Request
{
    public class OperadoraRequest
    {

        public OperadoraRequest()
        {
        }
        public OperadoraRequest(int id, string nomeOperadora, string tipoServico, string contatoSuporte)
        {
            Id = id; 
            NomeOperadora = nomeOperadora;
            TipoServico = tipoServico;
            ContatoSuporte = contatoSuporte;
        }

        public int Id { get; set; }
        public required string NomeOperadora { get; set; }
        public required string TipoServico { get; set; }
        public required string ContatoSuporte { get; set; }


    }
}
