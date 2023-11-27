using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO.Usuario;
using BackOffice.Domain.InterfaceRepository.Usuario;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Repositories.Usuario
{
    public class BuscarUsuarioRepository : IBuscarUsuarioRepository
    {
        private readonly SqlContext _context;
        public BuscarUsuarioRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<UsuarioBuscaDTO> BuscarUsuarioPorId(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var usuario = await connection.QueryFirstOrDefaultAsync<UsuarioBuscaDTO>(
                    sql: @"SELECT 
                                u.Login,
                                u.Ativo,
                                u.Email,
                                u.Role
                            FROM dbo.Usuario u WHERE u.Id = @Id ",
                    param: new
                    {
                        Id = id
                    });

                return usuario;
            }
        }

        public async Task<string> ProcurarUsuario(string login, string senha)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var usuarioRole = await connection.QueryFirstOrDefaultAsync<string>(
                    sql: @"SELECT Role FROM dbo.Usuario WHERE Login = @Login AND Senha = @Senha AND Ativo = 1",
                    param: new
                    {
                        Login = login,
                        Senha = senha
                    });

                return usuarioRole;
            }
        }

        public async Task<bool> VerificarEmail(string email)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var verificacao = await connection.QueryFirstOrDefaultAsync<int>(
                    sql: @"SELECT Count(*) FROM dbo.Usuario WHERE Email = @Email",
                    param: new
                    {
                        Email = email
                    });

                return verificacao > 0;
            }
        }

        public async Task<bool> VerificarLogin(string login)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var verificacao = await connection.QueryFirstOrDefaultAsync<int>(
                    sql: @"SELECT Count(*) FROM dbo.Usuario WHERE Login = @Login",
                    param: new
                    {
                        Login = login
                    });

                return verificacao > 0;
            }
        }

        public async Task<bool> VerificarLoginEmail(string login, string email)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var verificacao = await connection.QueryFirstOrDefaultAsync<int>(
                    sql: @"SELECT Count(*) FROM dbo.Usuario WHERE Login = @Login OR Email = @Email",
                    param: new
                    {
                        Login = login,
                        Email = email
                    });

                return verificacao > 0;
            }
        }
        public async Task<string> VerificarRespostaSeguranca(string resposta)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var senha = await connection.QueryFirstOrDefaultAsync<string>(
                    sql: @"SELECT Senha FROM dbo.Usuario WHERE RespostaSeguranca = @RespostaSeguranca",
                    param: new
                    {
                        RespostaSeguranca = resposta
                    });

                return senha;
            }
        }

        public async Task<bool> VerificarSenhaAntiga(string senhaAntiga, int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var senha = await connection.QueryFirstOrDefaultAsync<int>(
                    sql: @"SELECT Count(*) FROM dbo.Usuario WHERE Id = @Id AND Senha = @Senha",
                    param: new
                    {
                        Id = id,
                        Senha = senhaAntiga
                    });

                return senha > 0;
            }
        }
    }
}
