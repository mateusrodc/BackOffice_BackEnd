using BackOffice.Application.Interface.Pessoa;
using BackOffice.Domain.DTO;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.InterfaceRepository.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Service.Pessoa
{
    public class AlterarPessoaApplication : IAlterarPessoaApplication
    {
        private readonly IAlterarPessoaRepository _alterarPessoaRepository;
        private readonly IBuscaDocumentoRepository _buscaDocumentoRepository;
        public AlterarPessoaApplication(IAlterarPessoaRepository alterarPessoaRepository, IBuscaDocumentoRepository buscaDocumentoRepository) 
        { 
            _alterarPessoaRepository = alterarPessoaRepository;
            _buscaDocumentoRepository = buscaDocumentoRepository;
        }

        public async Task<Resultado> AlterarPessoa(AlterarPessoaDTO alterarPessoaDTO)
        {
            var resultado = new Resultado();
            var verificaDocumento = await _buscaDocumentoRepository.VerificaDocumento(alterarPessoaDTO.Documento);
            if (verificaDocumento)
            {
                resultado.AdicionarErro("Documento ja esta sendo utilizado");
                return resultado;
            }
            return await _alterarPessoaRepository.AlterarPessoa(alterarPessoaDTO);
        }
    }
}
