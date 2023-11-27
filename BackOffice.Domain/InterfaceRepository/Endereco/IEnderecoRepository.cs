using BackOffice.Domain.DTO.Endereco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Endereco
{
    public interface IEnderecoRepository
    {
        Task<int> CadastrarEndereco(BuscaEnderecoDTO buscaEnderecoDTO);
        Task<int> RetornarEndereco(string cep);
    }
}
