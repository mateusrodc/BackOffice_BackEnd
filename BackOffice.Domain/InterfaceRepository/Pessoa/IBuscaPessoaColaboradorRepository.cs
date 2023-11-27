using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.InterfaceRepository.Pessoa
{
    public interface IBuscaPessoaColaboradorRepository
    {
        Task<bool> BuscaPessoaColaborador(int id);
    }
}
