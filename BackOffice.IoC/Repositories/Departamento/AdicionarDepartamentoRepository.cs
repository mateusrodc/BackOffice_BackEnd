using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO.Departamentos;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.InterfaceRepository.Departamento;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Repositories.Departamento
{
    public class AdicionarDepartamentoRepository : IAdicionarDepartamentoRepository
    {
        private readonly SqlContext _context;
        public AdicionarDepartamentoRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<ResultadoAdicionarDepartamentoDTO> AdicionarDepartamento(AdicionarDepartamentoDTO adicionarDepartamentoDTO)
        {
            var resultado = new ResultadoAdicionarDepartamentoDTO();
            using (var conexao = _context.CreateConnection())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {

                        var rowsAffected = await transacao.Connection.ExecuteAsync(
                            sql: @"INSERT INTO [dbo].[Departamento] 
                                    (Nome, ResponsavelId, CriadoEm)
                                    OUTPUT INSERTED.Id
                                    VALUES (@Nome, @ResponsavelId, @CriadoEm);",
                        param: new
                        {
                            Nome = adicionarDepartamentoDTO.Nome,
                            ResponsavelId = adicionarDepartamentoDTO.ResponsavelId,
                            CriadoEm = DateTime.Now
                        },
                        transaction: transacao);

                        transacao.Commit();

                        resultado.Id = rowsAffected;

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        resultado.AdicionarErro("Erro durante o cadastro do Departamento");
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
