using BackOffice.Domain.DTO.Pessoa;

namespace BackOffice.Domain.InterfaceRepository.Pessoa
{
    public interface ICadastroPessoaRepository
    {
        Task<int> CadastrarPessoa(AdicionarPessoaDTO adicionarPessoaDTO);
    }
}
