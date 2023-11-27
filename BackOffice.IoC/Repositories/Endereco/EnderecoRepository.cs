using BackOffice.Data.Contexto;
using BackOffice.Domain.DTO.Endereco;
using BackOffice.Domain.DTO.Usuario;
using BackOffice.Domain.InterfaceRepository.Endereco;
using Dapper;

namespace BackOffice.Data.Repositories.Endereco
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly SqlContext _context;
        public EnderecoRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<int> CadastrarEndereco(BuscaEnderecoDTO buscaEnderecoDTO)
        {
            using (var conexao = _context.CreateConnection())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var rowsAffected = await transacao.Connection.ExecuteAsync(
                            sql: @"INSERT INTO [dbo].[Endereco] (
                                    Cep,
                                    Localidade,
                                    Logradouro,
                                    Siafi,
                                    Ibge,
                                    Gia,
                                    Bairro,
                                    UF,
                                    Complemento,
                                    DDD,
                                    CriadoEm)
                                    VALUES (@Cep, @Localidade, @Logradouro, @Siafi, @Ibge, @Gia, @Bairro, @UF, @Complemento, @DDD, @CriadoEm);",
                        param: new
                        {
                            Cep = buscaEnderecoDTO.Cep,
                            Localidade = buscaEnderecoDTO.Localidade,
                            Logradouro = buscaEnderecoDTO.Logradouro,
                            Siafi = buscaEnderecoDTO.Siafi,
                            Ibge = buscaEnderecoDTO.Ibge,
                            Gia= buscaEnderecoDTO.Gia,
                            Bairro = buscaEnderecoDTO.Bairro,
                            UF = buscaEnderecoDTO.UF,
                            Complemento = buscaEnderecoDTO.Complemento,
                            DDD = buscaEnderecoDTO.DDD,
                            CriadoEm = DateTime.Now
                        },
                        transaction: transacao);

                        transacao.Commit();

                        return rowsAffected;
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        return 0;
                    }
                    finally
                    {
                        conexao.Close();
                    }
                }
            }
        }

        public async Task<int> RetornarEndereco(string cep)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var departamento = await connection.QueryFirstOrDefaultAsync<int>(
                    sql: @"SELECT Id FROM dbo.Endereco WHERE Cep = @Cep",
                    param: new
                    {
                        Cep = cep
                    });

                return departamento ;
            }
        }
    }
}
