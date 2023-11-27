using BackOffice.Domain.DTO;
using BackOffice.Domain.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Usuario
{
    public interface IAtualizarUsuarioRepository
    {
        Task<ResultadoAlterarUsuarioDTO> AtualizarUsuario(AtualizarUsuarioDTO alterarUsuarioDTO);
        Task<Resultado> DesativarUsuario(int id);
        Task<Resultado> AlterarSenha(AlterarSenhaDTO alterarSenhaDTO);
    }
}
