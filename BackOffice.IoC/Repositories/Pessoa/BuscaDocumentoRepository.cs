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
    public class BuscaDocumentoRepository : IBuscaDocumentoRepository
    {
        private readonly SqlContext _context;
        public BuscaDocumentoRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<bool> VerificaDocumento(string documento)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var contador = await connection.QueryFirstOrDefaultAsync<int>(
                    sql: @"SELECT COUNT(*) FROM dbo.Pessoa WHERE Documento = @Documento",
                    param: new
                    {
                        Documento = documento
                    });

                return contador > 0;
            }
        }
    }
}
