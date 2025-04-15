using System.ComponentModel.DataAnnotations.Schema;

namespace Telecom.Entities
{
    public class OperadoraServico
    {

        public OperadoraServico()
        {
        }

        public OperadoraServico(int id, string nomeOperadora, string tipoServico, string contatoSuporte)
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
