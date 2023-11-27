using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO.Usuario;
using Microsoft.EntityFrameworkCore;
using Dapper;
using BackOffice.Domain.InterfaceRepository.Usuario;

namespace BackOffice.Data.Repositories.Usuario
{
    public class CadastroUsuarioRepository : ICadastroUsuarioRepository
    {
        private readonly SqlContext _context;
        public CadastroUsuarioRepository(SqlContext context) 
        {
            _context = context;
        }
        public async Task<ResultadoAdicionarUsuarioDTO> CadastrarUsuario(AdicionarUsuarioDTO adicionarUsuarioDTO)
        {
            var resultado = new ResultadoAdicionarUsuarioDTO();
            using (var conexao = _context.CreateConnection())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var rowsAffected = await transacao.Connection.ExecuteAsync(
                            sql: @"INSERT INTO [dbo].[Usuario] 
                            (Login, Senha, Role, Ativo, Email, PerguntaSeguranca, RespostaSeguranca)
                            OUTPUT INSERTED.Id
                            VALUES (@Login, @Senha, @Role, @Ativo, @Email, @PerguntaSeguranca, @RespostaSeguranca);",
                            param: new
                            {
                                Login = adicionarUsuarioDTO.Login,
                                Senha = adicionarUsuarioDTO.Senha,
                                Role = adicionarUsuarioDTO.Role,
                                Ativo = adicionarUsuarioDTO.Ativo,
                                Email = adicionarUsuarioDTO.Email,
                                PerguntaSeguranca = adicionarUsuarioDTO.PerguntaSeguranca,
                                RespostaSeguranca = adicionarUsuarioDTO.RespostaSeguranca
                            },
                            transaction: transacao);

                        transacao.Commit();

                        resultado.Id = rowsAffected;

                        return resultado;
                    }
                    catch (Exception ex) 
                    {
                        transacao.Rollback();

                        resultado.AdicionarErro("Nao foi possivel cadastrar o usuario");
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
