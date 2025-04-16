using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Entities.Request
{
    public class NomeOperadoraRequest
    {

        public NomeOperadoraRequest()
        {
        }
        public NomeOperadoraRequest(int id, string nomeOperadora)
        {
            Id = id;
            NomeOperadora = nomeOperadora;
        }

        public int Id { get; set; }
        public required string NomeOperadora { get; set; }



    }
}
