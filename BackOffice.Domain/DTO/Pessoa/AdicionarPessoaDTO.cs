using BackOffice.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Pessoa
{
    public class AdicionarPessoaDTO
    {
        public ETipoPessoa TipoPessoa { get; set; }
        public string Nome { get; set; }
        public string CEP { get; set; }
        public string Documento { get; set; }
        public string? NomeFantasia { get; set; }
        public string Qualificacao { get; set; }
        public int EnderecoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
