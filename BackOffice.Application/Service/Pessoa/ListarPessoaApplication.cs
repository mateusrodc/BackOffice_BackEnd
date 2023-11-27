using BackOffice.Application.Interface.Pessoa;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.InterfaceRepository.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Service.Pessoa
{
    public class ListarPessoaApplication : IListarPessoaApplication
    {
        private readonly IListarPessoaRepository _listarPessoaRepository;
        public ListarPessoaApplication(IListarPessoaRepository listarPessoaRepository) 
        {
            _listarPessoaRepository = listarPessoaRepository;
        }
        public Task<IList<ResultadoListarPessoasDTO>> ListarPessoas(int? tipoPessoa)
        {
            return _listarPessoaRepository.ListarPessoas(tipoPessoa);
        }
    }
}
