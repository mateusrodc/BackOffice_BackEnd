using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ResponsavelId { get; set; } 
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
