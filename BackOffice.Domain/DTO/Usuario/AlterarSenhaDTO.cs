using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Usuario
{
    public class AlterarSenhaDTO
    {
        public int Id { get; set; }
        public string SenhaAntiga { get; set; }
        public string SenhaNova { get; set; }
    }
}
