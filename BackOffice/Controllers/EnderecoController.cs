using BackOffice.Application.Interface.Endereco;
using BackOffice.Domain.DTO.Pessoa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoApplication _enderecoApplication;
        public EnderecoController(IEnderecoApplication enderecoApplication) 
        {
            _enderecoApplication = enderecoApplication;
        }
        [HttpPost]
        [Route("adicionar")]
        public async Task<IActionResult> CadastroPessoa([FromQuery] string cep)
        {
            try
            {
                var resultado = await _enderecoApplication.BuscarEndereco(cep);

                if(resultado == 0)
                    BadRequest(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
