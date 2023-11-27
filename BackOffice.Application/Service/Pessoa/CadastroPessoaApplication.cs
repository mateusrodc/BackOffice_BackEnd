using BackOffice.Application.Interface.Pessoa;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.InterfaceRepository.Pessoa;

namespace BackOffice.Application.Service.Pessoa
{
    public class CadastroPessoaApplication : ICadastroPessoaApplication
    {
        private readonly ICadastroPessoaRepository _cadastroPessoaRepository;
        private readonly IBuscaDocumentoRepository _buscaDocumentoRepository;
        public CadastroPessoaApplication(ICadastroPessoaRepository cadastroPessoaRepository, IBuscaDocumentoRepository buscaDocumentoRepository)
        {
            _cadastroPessoaRepository = cadastroPessoaRepository;
            _buscaDocumentoRepository = buscaDocumentoRepository;
        }

        public async Task<ResultadoAdicionarPessoaDTO> AdicionarPessoa(AdicionarPessoaDTO adicionarPessoaDTO)
        {
            var resultado = new ResultadoAdicionarPessoaDTO();
            var validaDocumento = await _buscaDocumentoRepository.VerificaDocumento(adicionarPessoaDTO.Documento);
            if (validaDocumento)
            {
                resultado.AdicionarErro("Pessoa com Documento ja existente");
                return resultado;
            }

            resultado.Id = await _cadastroPessoaRepository.CadastrarPessoa(adicionarPessoaDTO);

            if (resultado.Id == 0)
            {
                resultado.AdicionarErro("Falha no cadastro de Pessoa");
                return resultado;
            }
            return resultado;
        }
    }
}
