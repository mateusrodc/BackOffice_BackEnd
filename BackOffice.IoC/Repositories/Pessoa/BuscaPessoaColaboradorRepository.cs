using BackOffice.Data.Contexto;
using BackOffice.Domain.InterfaceRepository.Pessoa;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Repositories.Pessoa
{
    public class BuscaPessoaColaboradorRepository : IBuscaPessoaColaboradorRepository
    {
        private readonly SqlContext _context;
        public BuscaPessoaColaboradorRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<bool> BuscaPessoaColaborador(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var qualificacao = await connection.QueryFirstOrDefaultAsync<string>(
                    sql: @"SELECT Qualificacao FROM dbo.Pessoa WHERE Id = @Id",
                    param: new
                    {
                        Id = id
                    });

                return qualificacao.ToLower().Contains("colaborador");
            }
        }
    }
}
