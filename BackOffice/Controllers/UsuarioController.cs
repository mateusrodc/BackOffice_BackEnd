using BackOffice.Application.Interface;
using BackOffice.Application.Interface.Usuario;
using BackOffice.Domain.DTO.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILoginApplication _loginApplication;
        private readonly ICadastroUsuarioApplication _cadastroUsuarioApplication;
        private readonly IListarUsuariosApplication _listarUsuariosApplication;
        private readonly IAtualizarUsuarioApplication _atualizarUsuarioApplication;

        public UsuarioController(ITokenService tokenService, ILoginApplication loginApplication, ICadastroUsuarioApplication cadastroUsuarioApplication, IListarUsuariosApplication listarUsuariosApplication, IAtualizarUsuarioApplication atualizarUsuarioApplication)
        {
            _tokenService = tokenService;
            _loginApplication = loginApplication;
            _cadastroUsuarioApplication = cadastroUsuarioApplication;
            _listarUsuariosApplication = listarUsuariosApplication;
            _atualizarUsuarioApplication = atualizarUsuarioApplication;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO model)
        {
            var resultado = await _loginApplication.Autenticar(model.Login, model.Senha);
            
            if (!resultado.Sucesso)
                return BadRequest(new { message = "Usuário ou senha inválidos" });

            resultado.Token = _tokenService.GenerateToken(resultado);

            return Ok(resultado);
        }

        [HttpPost]
        [Route("novousuario")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> SignUp([FromBody] AdicionarUsuarioDTO adicionarUsuarioDTO)
        {
            var resultado = await _cadastroUsuarioApplication.CadastrarUsuario(adicionarUsuarioDTO);

            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }

        [HttpGet]
        [Route("listar")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var resultado = await _listarUsuariosApplication.ListarUsuarios();

            if(resultado.Count == 0)
                return NotFound(new { message = "Nao foi possivel encontrar usuarios" });

            return Ok(resultado);
        }

        [HttpPut]
        [Route("atualizar")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AtualizarUsuarios([FromBody] AtualizarUsuarioDTO atualizarUsuarioDTO)
        {
            var resultado = await _atualizarUsuarioApplication.AtualizarUsuario(atualizarUsuarioDTO);

            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }

        [HttpPut]
        [Route("esqueci-senha")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EsqueciSenha(string email, string respostaSeguranca)
        {
            var resultado = await _atualizarUsuarioApplication.EsqueciSenha(email, respostaSeguranca);

            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }

        [HttpPut]
        [Route("desativar-acesso")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DesativarAcesso(int id)
        {
            var resultado = await _atualizarUsuarioApplication.DesativarUsuario(id);

            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }

        [HttpPut]
        [Route("alterar-senha")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AlterarSenha([FromBody]AlterarSenhaDTO alterarSenhaDTO)
        {
            var resultado = await _atualizarUsuarioApplication.AlterarSenha(alterarSenhaDTO);

            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }
    }
}
