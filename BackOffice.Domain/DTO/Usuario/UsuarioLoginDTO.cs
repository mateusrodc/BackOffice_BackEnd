using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Usuario
{
    public class UsuarioLoginDTO : Resultado
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
