using BackOffice.Domain.DTO.Departamentos;
using BackOffice.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Interface.Departamento
{
    public interface IDeletarDepartamentoApplication
    {
        Task<Resultado> DeletarDepartamento(int id);
    }
}
