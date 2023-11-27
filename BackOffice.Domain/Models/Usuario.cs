using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public string Role { get; set; }
        public string PerguntaSeguranca { get; set; }
        public string RespostaSeguranca { get; set; }
    }
}
