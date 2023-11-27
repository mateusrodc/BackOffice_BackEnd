using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Interface.Endereco
{
    public interface IEnderecoApplication
    {
        Task<int> BuscarEndereco(string cep);
    }
}
