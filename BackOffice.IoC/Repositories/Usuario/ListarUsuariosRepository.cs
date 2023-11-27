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
    public class ListarUsuariosRepository : IListarUsuariosRepository
    {
        private readonly SqlContext _context;
        public ListarUsuariosRepository(SqlContext context) 
        { 
            _context = context;
        }

        public async Task<IList<ListarUsuariosDTO>> ListarUsuarios()
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var usuarioRole = await connection.QueryAsync<ListarUsuariosDTO>(
                    sql: @"SELECT
                                u.Id,
                                u.Email,
                                u.Login,
                                u.Role 
                            FROM dbo.Usuario u
                            WHERE u.Ativo = 1");

                return usuarioRole.ToList();
            }
        }
    }
}
