using BackOffice.Domain.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Interface.Usuario
{
    public interface IListarUsuariosApplication
    {
        Task<IList<ListarUsuariosDTO>> ListarUsuarios();
    }
}
