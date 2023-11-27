using BackOffice.Application.Interface.Departamento;
using BackOffice.Domain.DTO.Departamentos;
using BackOffice.Domain.InterfaceRepository.Departamento;
using BackOffice.Domain.InterfaceRepository.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Service.Departamentos
{
    public class AdicionarDepartamentoApplication : IAdicionarDepartamentoApplication
    {
        private readonly IAdicionarDepartamentoRepository _adicionarDepartamentoRepository;
        private readonly IBuscaPessoaColaboradorRepository _buscaPessoaColaboradorRepository;
        public AdicionarDepartamentoApplication(IAdicionarDepartamentoRepository adicionarDepartamentoRepository, IBuscaPessoaColaboradorRepository buscaPessoaColaboradorRepository) 
        {
            _adicionarDepartamentoRepository = adicionarDepartamentoRepository;
            _buscaPessoaColaboradorRepository = buscaPessoaColaboradorRepository;
        }

        public async Task<ResultadoAdicionarDepartamentoDTO> AdicionarDepartamento(AdicionarDepartamentoDTO adicionarDepartamentoDTO)
        {
            var resultado = new ResultadoAdicionarDepartamentoDTO();
            var verificaColaborador = await _buscaPessoaColaboradorRepository.BuscaPessoaColaborador(adicionarDepartamentoDTO.ResponsavelId);

            if (!verificaColaborador)
            {
                resultado.AdicionarErro("Responsavel nao possui qualificacao Colaborador");
                return resultado;
            }
            return await _adicionarDepartamentoRepository.AdicionarDepartamento(adicionarDepartamentoDTO);
        }
    }
}
