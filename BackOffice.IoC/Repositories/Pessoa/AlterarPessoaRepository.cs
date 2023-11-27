using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.Enum;
using BackOffice.Domain.InterfaceRepository.Pessoa;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Repositories.Pessoa
{
    public class AlterarPessoaRepository : IAlterarPessoaRepository
    {
        private readonly SqlContext _context;
        public AlterarPessoaRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<Resultado> AlterarPessoa(AlterarPessoaDTO alterarPessoaDTO)
        {
            var resultado = new Resultado();

            using (var conexao = _context.CreateConnection())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var rowsAffected = await transacao.Connection.ExecuteAsync(
                            sql: @"UPDATE [dbo].[Pessoa] 
                                    SET TipoPessoa = @TipoPessoa,
                                        Nome = @Nome,
                                        Documento = @Documento,
                                        NomeFantasia = @NomeFantasia,
                                        Qualificacao = @Qualificacao,
                                        EnderecoId = @EnderecoId,
                                        AtualizadoEm = @AtualizadoEm
                                    WHERE Id = @Id",
                            param: new
                            {
                                Id = alterarPessoaDTO.Id,
                                TipoPessoa = (int)alterarPessoaDTO.TipoPessoa,
                                Nome = alterarPessoaDTO.Nome,
                                Documento = alterarPessoaDTO.Documento,
                                NomeFantasia = alterarPessoaDTO.NomeFantasia,
                                Qualificacao = alterarPessoaDTO.Qualificacao,
                                EnderecoId = alterarPessoaDTO.EnderecoId,
                                AtualizadoEm = DateTime.Now
                            },
                            transaction: transacao);

                        transacao.Commit();

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();

                        resultado.AdicionarErro("Nao foi possivel alterar o usuario");
                        return resultado;
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
