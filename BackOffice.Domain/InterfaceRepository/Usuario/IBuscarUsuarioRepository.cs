using BackOffice.Domain.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Usuario
{
    public interface IBuscarUsuarioRepository
    {
        Task<string> ProcurarUsuario(string login, string senha);
        Task<bool> VerificarLoginEmail(string login, string email);
        Task<bool> VerificarLogin(string login);
        Task<bool> VerificarEmail(string email);
        Task<UsuarioBuscaDTO> BuscarUsuarioPorId(int id);
        Task<string> VerificarRespostaSeguranca(string pergunta);
        Task<bool> VerificarSenhaAntiga(string senhaAntiga, int id);
    }
}
