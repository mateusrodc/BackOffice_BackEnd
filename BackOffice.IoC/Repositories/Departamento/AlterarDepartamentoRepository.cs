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
    public class AlterarDepartamentoRepository : IAlterarDepartamentoRepository
    {
        private readonly SqlContext _context;
        public AlterarDepartamentoRepository(SqlContext context) 
        { 
            _context = context;
        }

        public async Task<Resultado> AlterarDepartamento(AlterarDepartamentoDTO alterarDepartamentoDTO)
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
                            sql: @"UPDATE [dbo].[Departamento] 
                                    SET
                                        Nome = @Nome,
                                        ResponsavelId = @ResponsavelId,
                                        AtualizadoEm = @AtualizadoEm
                                    WHERE Id = @Id",
                            param: new
                            {
                                Id = alterarDepartamentoDTO.Id,
                                Nome = alterarDepartamentoDTO.Nome,
                                ResponsavelId = alterarDepartamentoDTO.ResponsavelId,
                                AtualizadoEm = DateTime.Now
                            },
                            transaction: transacao);

                        transacao.Commit();

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();

                        resultado.AdicionarErro("Nao foi possivel alterar o departamento");
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
