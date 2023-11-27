using BackOffice.Application.Interface.Departamento;
using BackOffice.Domain.DTO.Departamentos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartamentoController : ControllerBase
    {
        private readonly IListarDepartamentos _listarDepartamentos;
        private readonly IAdicionarDepartamentoApplication _adicionarDepartamentoApplication;
        private readonly IAlterarDepartamentoApplication _alterarDepartamentoApplication;
        private readonly IDeletarDepartamentoApplication _deletarDepartamentoApplication;
        public DepartamentoController(IListarDepartamentos listarDepartamentos,
            IAdicionarDepartamentoApplication adicionarDepartamentoApplication,
            IAlterarDepartamentoApplication alterarDepartamentoApplication,
            IDeletarDepartamentoApplication deletarDepartamentoApplication)
        {
            _listarDepartamentos = listarDepartamentos;
            _adicionarDepartamentoApplication = adicionarDepartamentoApplication;
            _alterarDepartamentoApplication = alterarDepartamentoApplication;
            _deletarDepartamentoApplication = deletarDepartamentoApplication;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> ListarDepartamento()
        {
            var resultado = await _listarDepartamentos.ListarTodosDepartamentos();
            return Ok(resultado);
        }
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarDepartamento([FromBody] AdicionarDepartamentoDTO adicionarDepartamentoDTO)
        {
            var resultado = await _adicionarDepartamentoApplication.AdicionarDepartamento(adicionarDepartamentoDTO);
            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }
        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> AlterarDepartamento([FromBody] AlterarDepartamentoDTO alterarDepartamentoDTO)
        {
            var resultado = await _alterarDepartamentoApplication.AlterarDepartamento(alterarDepartamentoDTO);
            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> ExcluirDepartamento(int id)
        {
            var resultado = await _deletarDepartamentoApplication.DeletarDepartamento(id);
            return resultado.ContemErros
                    ? BadRequest(resultado)
                    : Ok(resultado);
        }
    }
}
