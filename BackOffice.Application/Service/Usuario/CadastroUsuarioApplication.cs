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
    public class CadastroUsuarioApplication : ICadastroUsuarioApplication
    {
        private readonly ICadastroUsuarioRepository _cadastroUsuarioRepository;
        private readonly IBuscarUsuarioRepository _buscarUsuarioRepository;
        public CadastroUsuarioApplication(ICadastroUsuarioRepository cadastroUsuarioRepository, IBuscarUsuarioRepository buscarUsuarioRepository)
        {
            _cadastroUsuarioRepository = cadastroUsuarioRepository;
            _buscarUsuarioRepository = buscarUsuarioRepository;
        }
        public async Task<ResultadoAdicionarUsuarioDTO> CadastrarUsuario(AdicionarUsuarioDTO adicionarUsuarioDTO)
        {
            var resultado = new ResultadoAdicionarUsuarioDTO();
            var verificacaoLoginEmail = await _buscarUsuarioRepository.VerificarLoginEmail(adicionarUsuarioDTO.Login, adicionarUsuarioDTO.Email);

            if (verificacaoLoginEmail)
            {
                resultado.AdicionarErro("Ja existe um usuario que possui o mesmo Login ou Email");
                return resultado;
            }

            return await _cadastroUsuarioRepository.CadastrarUsuario(adicionarUsuarioDTO);
        }
    }
}
