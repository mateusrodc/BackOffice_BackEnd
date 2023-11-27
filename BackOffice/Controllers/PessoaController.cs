using BackOffice.Application.Interface.Pessoa;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PessoaController : ControllerBase
    {
        private readonly ICadastroPessoaApplication _cadastroPessoaApplication;
        private readonly IListarPessoaApplication _listarPessoaApplication;
        private readonly IAlterarPessoaApplication _alterarPessoaApplication;


        public PessoaController(ICadastroPessoaApplication cadastroPessoaApplication, 
            IListarPessoaApplication listarPessoaApplication, IAlterarPessoaApplication alterarPessoaApplication)
        {
            _cadastroPessoaApplication = cadastroPessoaApplication;
            _listarPessoaApplication = listarPessoaApplication;
            _alterarPessoaApplication = alterarPessoaApplication;
        }

        [HttpPost]
        [Route("adicionar")]
        public async Task<IActionResult> CadastroPessoa([FromBody] AdicionarPessoaDTO adicionarPessoaDTO)
        {
            try
            {
                var resultado = await _cadastroPessoaApplication.AdicionarPessoa(adicionarPessoaDTO);

                return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> ListarPessoas(ETipoPessoa tipoPessoa)
        {
            var resultado = await _listarPessoaApplication.ListarPessoas((int)tipoPessoa);
            return Ok(resultado);
        }
        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> AlterarPessoas([FromBody] AlterarPessoaDTO alterarPessoaDTO)
        {
            var resultado = await _alterarPessoaApplication.AlterarPessoa(alterarPessoaDTO);

            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }
    }
}
