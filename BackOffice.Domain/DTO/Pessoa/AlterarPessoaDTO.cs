using BackOffice.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Pessoa
{
    public class AlterarPessoaDTO
    {
        public int Id { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string? NomeFantasia { get; set; }
        public string Qualificacao { get; set; }
        public int EnderecoId { get; set; }
    }
}
