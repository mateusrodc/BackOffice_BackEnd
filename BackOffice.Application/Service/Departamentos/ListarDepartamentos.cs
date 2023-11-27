using BackOffice.Application.Interface.Departamento;
using BackOffice.Domain.DTO.Departamentos;
using BackOffice.Domain.InterfaceRepository.Departamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Service.Departamentos
{
    public class ListarDepartamentos : IListarDepartamentos
    {
        private readonly IListarDepartamentosRepository _listarDepartamentosRepository;
        public ListarDepartamentos(IListarDepartamentosRepository listarDepartamentosRepository) 
        {
            _listarDepartamentosRepository = listarDepartamentosRepository;
        }

        public async Task<IList<ListarDepartamentosDTO>> ListarTodosDepartamentos()
        {
            return await _listarDepartamentosRepository.ListarTodosDepartamentos();
        }
    }
}
