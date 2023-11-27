using BackOffice.Application.Interface.Usuario;
using BackOffice.Domain.DTO.Usuario;
using BackOffice.Domain.InterfaceRepository.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Service.Usuario
{
    public class ListarUsuariosApplication: IListarUsuariosApplication
    {
        private IListarUsuariosRepository _listarUsuariosRepository;
        public ListarUsuariosApplication(IListarUsuariosRepository listarUsuariosRepository) 
        {
            _listarUsuariosRepository = listarUsuariosRepository;
        }

        Task<IList<ListarUsuariosDTO>> IListarUsuariosApplication.ListarUsuarios()
        {
            return _listarUsuariosRepository.ListarUsuarios();
        }
    }
}
