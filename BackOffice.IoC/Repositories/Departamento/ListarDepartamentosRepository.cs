using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO.Departamentos;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.Enum;
using BackOffice.Domain.InterfaceRepository.Departamento;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Repositories.Departamento
{
    public class ListarDepartamentosRepository : IListarDepartamentosRepository
    {
        private readonly SqlContext _context;
        public ListarDepartamentosRepository(SqlContext context) 
        {
            _context = context;
        }
        public async Task<IList<ListarDepartamentosDTO>> ListarTodosDepartamentos()
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var usuarioRole = await connection.QueryAsync<ListarDepartamentosDTO>(
                    sql: @"SELECT
                                p.Id,
                                p.Nome,
                                d.Nome
                            FROM dbo.Pessoa p
                            INNER JOIN dbo.Departamento d ON p.Id = d.ResponsavelId");

                return usuarioRole.ToList();
            }
        }
    }
}
