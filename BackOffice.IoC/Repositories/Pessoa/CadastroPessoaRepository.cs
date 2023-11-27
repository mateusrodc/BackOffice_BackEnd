using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.DTO.Usuario;
using Microsoft.EntityFrameworkCore;
using Dapper;
using BackOffice.Domain.InterfaceRepository.Pessoa;

namespace BackOffice.Data.Repositories.Pessoa
{
    public class CadastroPessoaRepository : ICadastroPessoaRepository
    {
        private readonly SqlContext _context;
        public CadastroPessoaRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task<int> CadastrarPessoa(AdicionarPessoaDTO adicionarPessoaDTO)
        {
            using (var conexao = _context.CreateConnection())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        
                        var rowsAffected = await transacao.Connection.ExecuteAsync(
                            sql: @"INSERT INTO [dbo].[Pessoa] 
                                    (TipoPessoa, Nome, Documento, NomeFantasia, Qualificacao, EnderecoId)
                                    OUTPUT INSERTED.Id
                                    VALUES (@TipoPessoa, @Nome, @Documento, @NomeFantasia, @Qualificacao, @EnderecoId);",
                        param: new
                        {
                            TipoPessoa = adicionarPessoaDTO.TipoPessoa,
                            Nome = adicionarPessoaDTO.Nome,
                            Documento = adicionarPessoaDTO.Documento,
                            NomeFantasia = adicionarPessoaDTO.NomeFantasia,
                            Qualificacao = adicionarPessoaDTO.Qualificacao,
                            EnderecoId = adicionarPessoaDTO.EnderecoId
                        },
                        transaction: transacao);

                        transacao.Commit();

                        return rowsAffected;
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        return 0;
                    }
                    finally
                    {
                        conexao.Close();
                    }
                }
            }
        }
    }
}