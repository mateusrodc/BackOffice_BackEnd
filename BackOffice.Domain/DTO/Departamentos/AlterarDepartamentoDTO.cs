using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Departamentos
{
    public class AlterarDepartamentoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ResponsavelId { get; set; }
    }
}
