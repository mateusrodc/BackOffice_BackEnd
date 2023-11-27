using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO.Pessoa;
using BackOffice.Domain.DTO.Usuario;
using BackOffice.Domain.InterfaceRepository.Pessoa;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Repositories.Pessoa
{
    public class ListarPessoaRepository : IListarPessoaRepository
    {
        private readonly SqlContext _context;
        public ListarPessoaRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<IList<ResultadoListarPessoasDTO>> ListarPessoas(int? tipoPessoa)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var usuarioRole = await connection.QueryAsync<ResultadoListarPessoasDTO>(
                    sql: @"SELECT
                                p.Id,
                                p.TipoPessoa,
                                p.Nome,
                                p.Documento,
                                p.NomeFantasia,
                                p.Qualificacao,
                                p.CriadoEm,
                                p.AtualizadoEm,
                                p.EnderecoId
                            FROM dbo.Pessoa p
                            WHERE p.TipoPessoa = @TipoPessoa",
                    param: new
                    {
                        TipoPessoa = tipoPessoa
                    });

                return usuarioRole.ToList();
            }
        }
    }
}
