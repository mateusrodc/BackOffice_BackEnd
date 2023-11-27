using BackOffice.Domain.DTO.Departamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Departamento
{
    public interface IListarDepartamentosRepository
    {
        Task<IList<ListarDepartamentosDTO>> ListarTodosDepartamentos();
    }
}
