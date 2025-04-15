using System.ComponentModel.DataAnnotations.Schema;

namespace Telecom.Entities.Response
{
    [Table("operadoras_servicos")]
    public class OperadoraServicoResponse
    {

        public OperadoraServicoResponse()
        {
        }
        public OperadoraServicoResponse(int id, string nomeOperadora, string tipoServico, string contatoSuporte)
        {
            Id = id;
            NomeOperadora = nomeOperadora;
            TipoServico = tipoServico;
            ContatoSuporte = contatoSuporte;
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("nome_operadora")]
        public string NomeOperadora { get; set; }

        [Column("tipo_servico")]
        public string TipoServico { get; set; }

        [Column("contato_suporte")]
        public string ContatoSuporte { get; set; }
    }
}