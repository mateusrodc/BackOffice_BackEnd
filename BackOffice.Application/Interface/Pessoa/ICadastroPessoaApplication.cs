using BackOffice.Domain.DTO.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Interface.Pessoa
{
    public interface ICadastroPessoaApplication
    {
        Task<ResultadoAdicionarPessoaDTO> AdicionarPessoa(AdicionarPessoaDTO adicionarPessoaDTO);
    }
}
