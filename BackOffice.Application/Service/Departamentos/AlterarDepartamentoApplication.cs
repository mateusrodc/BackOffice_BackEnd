using BackOffice.Application.Interface.Departamento;
using BackOffice.Domain.DTO;
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
    public class AlterarDepartamentoApplication : IAlterarDepartamentoApplication
    {
        private readonly IAlterarDepartamentoRepository _alterarDepartamentoRepository;
        private readonly IBuscaPessoaColaboradorRepository _buscaPessoaColaboradorRepository;
        private readonly IBuscarDepartamentoRepository _buscaDepartamentoRepository;
        public AlterarDepartamentoApplication(IAlterarDepartamentoRepository alterarDepartamentoRepository,
            IBuscaPessoaColaboradorRepository buscaPessoaColaboradorRepository,
            IBuscarDepartamentoRepository buscaDepartamentoRepository)
        {
            _alterarDepartamentoRepository = alterarDepartamentoRepository;
            _buscaPessoaColaboradorRepository = buscaPessoaColaboradorRepository;
            _buscaDepartamentoRepository = buscaDepartamentoRepository;
        }

        public async Task<Resultado> AlterarDepartamento(AlterarDepartamentoDTO alterarDepartamentoDTO)
        {
            var resultado = new Resultado();
            var verificar = await _buscaPessoaColaboradorRepository.BuscaPessoaColaborador(alterarDepartamentoDTO.Id);
            if (!verificar)
            {
                resultado.AdicionarErro("Responsavel indicado nao possui qualificacao de Colaborador");
                return resultado;
            }

            var verificarNome = await _buscaDepartamentoRepository.VerificarNomeDepartamento(alterarDepartamentoDTO.Nome);
            if (verificarNome)
            {
                resultado.AdicionarErro("Nome de Departamento existente");
                return resultado;
            }

            return await _alterarDepartamentoRepository.AlterarDepartamento(alterarDepartamentoDTO);
        }
    }
}
