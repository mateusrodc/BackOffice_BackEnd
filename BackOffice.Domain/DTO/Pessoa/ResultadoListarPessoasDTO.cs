using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Pessoa
{
    public class ResultadoListarPessoasDTO
    {
        public int Id { get; set; }
        public int TipoPessoa { get; set; }
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
        public string Documento { get; set; }
        public string? NomeFantasia { get; set; }
        public string Qualificacao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
