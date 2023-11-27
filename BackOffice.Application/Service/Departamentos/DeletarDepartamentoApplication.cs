using BackOffice.Application.Interface.Departamento;
using BackOffice.Domain.DTO;
using BackOffice.Domain.InterfaceRepository.Departamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Service.Departamentos
{
    public class DeletarDepartamentoApplication : IDeletarDepartamentoApplication
    {
        private readonly IDeletarDepartamentoRepository _deletarDepartamentoRepository;
        public DeletarDepartamentoApplication(IDeletarDepartamentoRepository deletarDepartamentoRepository) 
        {
            _deletarDepartamentoRepository = deletarDepartamentoRepository;
        }

        public async Task<Resultado> DeletarDepartamento(int id)
        {
            return await _deletarDepartamentoRepository.DeletarDepartamento(id);
        }
    }
}
