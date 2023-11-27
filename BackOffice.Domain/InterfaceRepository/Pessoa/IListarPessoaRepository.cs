using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Pessoa
{
    public interface IListarPessoaRepository
    {
        Task<IList<ResultadoListarPessoasDTO>> ListarPessoas(int? tipoPessoa);
    }
}
