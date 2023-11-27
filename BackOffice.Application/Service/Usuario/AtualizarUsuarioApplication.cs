using BackOffice.Application.Interface.Usuario;
using BackOffice.Domain.DTO;
using BackOffice.Domain.DTO.Usuario;
using BackOffice.Domain.InterfaceRepository.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Service.Usuario
{
    public class AtualizarUsuarioApplication : IAtualizarUsuarioApplication
    {
        private readonly IAtualizarUsuarioRepository _atualizarUsuarioRepository;
        private readonly IBuscarUsuarioRepository _buscarUsuarioRepository;
        public AtualizarUsuarioApplication(IAtualizarUsuarioRepository atualizarUsuarioRepository, IBuscarUsuarioRepository buscarUsuarioRepository)
        {
            _atualizarUsuarioRepository = atualizarUsuarioRepository;
            _buscarUsuarioRepository = buscarUsuarioRepository;
        }

        public async Task<Resultado> AlterarSenha(AlterarSenhaDTO alterarSenhaDTO)
        {
            var resultado = new Resultado();
            var verificaSenha = await _buscarUsuarioRepository.VerificarSenhaAntiga(alterarSenhaDTO.SenhaAntiga, alterarSenhaDTO.Id);
            if (!verificaSenha)
            {
                resultado.AdicionarErro("Senha antiga Incorreta");
                return resultado;
            }
            return await _atualizarUsuarioRepository.AlterarSenha(alterarSenhaDTO);
        }

        public async Task<ResultadoAlterarUsuarioDTO> AtualizarUsuario(AtualizarUsuarioDTO atualizarUsuarioDTO)
        {
            var resultado = new ResultadoAlterarUsuarioDTO();
            var buscarUsuario = await _buscarUsuarioRepository.BuscarUsuarioPorId(atualizarUsuarioDTO.Id);

            if(buscarUsuario == null)
            {
                resultado.AdicionarErro("Usuario nao foi encontrado");
                return resultado;
            }

            if ((atualizarUsuarioDTO.Login != buscarUsuario.Login) && (atualizarUsuarioDTO.Email != buscarUsuario.Email))
            {
                var validarLoginEmail = await _buscarUsuarioRepository.VerificarLoginEmail(atualizarUsuarioDTO.Login, atualizarUsuarioDTO.Email);
                if (validarLoginEmail)
                {
                    resultado.AdicionarErro("O Login ou Email ja existentes");   
                }
            }

            if(atualizarUsuarioDTO.Login != buscarUsuario.Login)
            {
                var validarLoginEmail = await _buscarUsuarioRepository.VerificarLogin(atualizarUsuarioDTO.Login);
                if (validarLoginEmail)
                {
                    resultado.AdicionarErro("Login ja existente");
                }
            }
            if(atualizarUsuarioDTO.Email != buscarUsuario.Email)
            {
                var validarLoginEmail = await _buscarUsuarioRepository.VerificarEmail(atualizarUsuarioDTO.Email);
                if (validarLoginEmail)
                {
                    resultado.AdicionarErro("Email ja existente");
                }
            }

            return await _atualizarUsuarioRepository.AtualizarUsuario(atualizarUsuarioDTO);
        }

        public async Task<Resultado> DesativarUsuario(int id)
        {
            return await _atualizarUsuarioRepository.DesativarUsuario(id);
        }

        public async Task<EsqueciSenhaDTO> EsqueciSenha(string email, string respostaSeguranca)
        {
            var resultado = new EsqueciSenhaDTO();
            var validarEmail = await _buscarUsuarioRepository.VerificarEmail(email);
            if (validarEmail)
            {
                var senha = await _buscarUsuarioRepository.VerificarRespostaSeguranca(respostaSeguranca);

                if (string.IsNullOrEmpty(senha))
                {
                    resultado.AdicionarErro("Resposta invalida");
                    return resultado;
                }

                resultado.Senha = senha;

                return resultado;
            }

            resultado.AdicionarErro("Email incorreto");
            return resultado;
        }
    }
}
