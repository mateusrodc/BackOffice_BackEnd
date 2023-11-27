using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Departamento
{
    public interface IBuscarDepartamentoRepository
    {
        Task<bool> VerificarNomeDepartamento(string nomeDepartamento);
    }
}
