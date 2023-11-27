using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO.Usuario;
using BackOffice.Domain.InterfaceRepository.Usuario;
using Microsoft.EntityFrameworkCore;
using Dapper;
using BackOffice.Domain.DTO;

namespace BackOffice.Data.Repositories.Usuario
{
    public class AtualizarUsuarioRepository : IAtualizarUsuarioRepository
    {
        private readonly SqlContext _context;
        public AtualizarUsuarioRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task<Resultado> AlterarSenha(AlterarSenhaDTO alterarSenhaDTO)
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
                            sql: @"UPDATE [dbo].[Usuario] 
                                    SET 
                                        Senha = @Senha,
                                        AtualizadoEm = @AtualizadoEm
                                    WHERE Id = @Id",
                            param: new
                            {
                                Senha = alterarSenhaDTO.SenhaNova,
                                Id = alterarSenhaDTO.Id,
                                AtualizadoEm = DateTime.Now
                            },
                            transaction: transacao);

                        transacao.Commit();

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();

                        resultado.AdicionarErro("Nao foi possivel alterar a senha");
                        return resultado;
                    }
                    finally
                    {
                        conexao.Close();
                    }
                }
            }
        }

        public async Task<ResultadoAlterarUsuarioDTO> AtualizarUsuario(AtualizarUsuarioDTO alterarUsuarioDTO)
        {
            var resultado = new ResultadoAlterarUsuarioDTO();
            using (var conexao = _context.CreateConnection())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var rowsAffected = await transacao.Connection.ExecuteAsync(
                            sql: @"UPDATE [dbo].[Usuario] 
                                    SET Login = @Login,
                                        Role = @Role,
                                        Email = @Email,
                                        AtualizadoEm = @AtualizadoEm
                                    WHERE Id = @Id",
                            param: new
                            {
                                Id = alterarUsuarioDTO.Id,
                                Login = alterarUsuarioDTO.Login,
                                Role = alterarUsuarioDTO.Role,
                                Email = alterarUsuarioDTO.Email,
                                AtualizadoEm = DateTime.Now
                            },
                            transaction: transacao);

                        transacao.Commit();

                        resultado.Sucesso = true;

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

        public async Task<Resultado> DesativarUsuario(int id)
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
                            sql: @"UPDATE [dbo].[Usuario] 
                                    SET Ativo = 0,
                                        AtualizadoEm = @AtualizadoEm
                                    WHERE Id = @Id",
                            param: new
                            {
                                Id = id,
                                AtualizadoEm = DateTime.Now
                            },
                            transaction: transacao);

                        transacao.Commit();

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();

                        resultado.AdicionarErro("Nao foi possivel desativar o usuario");
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
