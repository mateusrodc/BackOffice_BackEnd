using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO;
using BackOffice.Domain.DTO.Departamentos;
using BackOffice.Domain.InterfaceRepository.Departamento;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Repositories.Departamento
{
    public class DeletarDepartamentoRepository : IDeletarDepartamentoRepository
    {
        private readonly SqlContext _context;
        public DeletarDepartamentoRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<Resultado> DeletarDepartamento(int id)
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
                            sql: @"DELETE FROM [dbo].[Departamento] 
                                    WHERE Id = @Id",
                        param: new
                        {
                            Id = id
                        },
                        transaction: transacao);

                        transacao.Commit();

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        resultado.AdicionarErro("Erro durante a exclusao do Departamento");
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
