using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.DataBase
{
    public class Fatura
    {

        public Fatura()
        {
            
        }

        public Fatura(int id, int contratoId, Contrato contrato, DateTime dataEmissao, DateTime dataVencimento, decimal valorCobrado, string status)
        {
            Id = id;
            ContratoId = contratoId;
            Contrato = contrato;
            DataEmissao = dataEmissao;
            DataVencimento = dataVencimento;
            ValorCobrado = valorCobrado;
            Status = status;
        }

        [Column("id")] 
        public int Id { get; set; }
        [Column("contrato_id")]
        public int ContratoId { get; set; }

        [ForeignKey("ContratoId")]
        public Contrato Contrato { get; set; }

        [Column("data_emissao")]
        public DateTime DataEmissao { get; set; }

        [Column("data_vencimento")]
        public DateTime DataVencimento { get; set; }

        [Column("valor_cobrado")]
        public decimal ValorCobrado { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }

}
