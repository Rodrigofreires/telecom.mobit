using System.ComponentModel.DataAnnotations.Schema;
using Telecom.Entities;

namespace Telecom.DataBase
{
    public class Contrato
    {
        public Contrato()
        {
        }

        public Contrato(int id, string nomeFilial, int operadoraId, string planoContratado, DateTime dataInicio, DateTime dataVencimento, decimal valorMensal, bool status)
        {
            Id = id;
            NomeFilial = nomeFilial;
            OperadoraId = operadoraId;
            PlanoContratado = planoContratado;
            DataInicio = dataInicio;
            DataVencimento = dataVencimento;
            ValorMensal = valorMensal;
            Status = status;
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("nome_filial")]
        public string NomeFilial { get; set; }

        [Column("operadora_id")]
        public int OperadoraId { get; set; }

        [Column("plano_contratado")]
        public string PlanoContratado { get; set; }

        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Column("data_vencimento")]
        public DateTime DataVencimento { get; set; }

        [Column("valor_mensal")]
        public decimal ValorMensal { get; set; }

        [Column("status")]
        public bool Status { get; set; } = true;

        public virtual OperadoraServico OperadoraServico { get; set;}

        public List<Fatura> Faturas { get; set; }
    }
}
