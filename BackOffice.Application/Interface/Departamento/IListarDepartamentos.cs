using BackOffice.Domain.DTO.Departamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Interface.Departamento
{
    public interface IListarDepartamentos
    {
        public Task<IList<ListarDepartamentosDTO>> ListarTodosDepartamentos();
    }
}
