using BackOffice.Data.Contexto;
using BackOffice.Domain.InterfaceRepository.Departamento;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Repositories.Departamento
{
    public class BuscarDepartamentoRepository : IBuscarDepartamentoRepository
    {
        private readonly SqlContext _context;
        public BuscarDepartamentoRepository(SqlContext context) 
        {
            _context = context;
        }
        public async Task<bool> VerificarNomeDepartamento(string nomeDepartamento)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var departamento = await connection.QueryFirstOrDefaultAsync<int>(
                    sql: @"SELECT Count(*) FROM dbo.Departamento WHERE Nome = @Nome",
                    param: new
                    {
                        Nome = nomeDepartamento
                    });

                return departamento > 0;
            }
        }
    }
}
