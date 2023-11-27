using BackOffice.Application.Interface.Usuario;
using BackOffice.Domain.DTO;
using BackOffice.Domain.DTO.Usuario;
using BackOffice.Domain.InterfaceRepository.Usuario;

namespace BackOffice.Application.Service.Usuario
{
    public class LoginApplication : ILoginApplication
    {
        private readonly IBuscarUsuarioRepository _buscarUsuarioRepository;
        public LoginApplication(IBuscarUsuarioRepository buscarUsuarioRepository)
        {
            _buscarUsuarioRepository = buscarUsuarioRepository;
        }
        public async Task<UsuarioLoginDTO> Autenticar(string login, string senha)
        {
            var resultado = new UsuarioLoginDTO();
            var usuarioRole = await _buscarUsuarioRepository.ProcurarUsuario(login, senha);

            if (string.IsNullOrEmpty(usuarioRole))
            {
                resultado.AdicionarErro("Login ou senha invalidos");
                return resultado;
            }
            resultado.Login = login;
            resultado.Role = usuarioRole;

            return resultado;
        }
    }
}
