using BackOffice.Domain.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Usuario
{
    public interface IListarUsuariosRepository
    {
        Task<IList<ListarUsuariosDTO>> ListarUsuarios();
    }
}
