using BackOffice.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Departamento
{
    public interface IDeletarDepartamentoRepository
    {
        Task<Resultado> DeletarDepartamento(int id);
    }
}
