using BackOffice.Domain.DTO.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Departamentos
{
    public class ListarDepartamentosDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Responsavel { get; set; }
    }
}
