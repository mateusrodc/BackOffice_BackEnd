using BackOffice.Domain.DTO.Departamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Departamento
{
    public interface IAdicionarDepartamentoRepository
    {
        Task<ResultadoAdicionarDepartamentoDTO> AdicionarDepartamento(AdicionarDepartamentoDTO adicionarDepartamentoDTO);
    }
}
