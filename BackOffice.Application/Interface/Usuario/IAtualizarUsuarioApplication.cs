using BackOffice.Domain.DTO;
using BackOffice.Domain.DTO.Usuario;

namespace BackOffice.Application.Interface.Usuario
{
    public interface IAtualizarUsuarioApplication
    {
        Task<ResultadoAlterarUsuarioDTO> AtualizarUsuario(AtualizarUsuarioDTO alterarUsuarioDTO);
        Task<EsqueciSenhaDTO> EsqueciSenha(string email, string respostaSeguranca);
        Task<Resultado> DesativarUsuario(int id);
        Task<Resultado> AlterarSenha(AlterarSenhaDTO alterarSenhaDTO);
    }
}
