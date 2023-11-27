using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Pessoa
{
    public class PessoaDTO
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public int TipoPessoa { get; set; }
    }
}
