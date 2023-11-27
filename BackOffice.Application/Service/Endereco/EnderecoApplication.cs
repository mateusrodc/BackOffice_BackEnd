using BackOffice.Application.Interface.Endereco;
using BackOffice.Domain.DTO.Endereco;
using BackOffice.Domain.InterfaceRepository.Endereco;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BackOffice.Application.Service.Endereco
{
    public class EnderecoApplication : IEnderecoApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoApplication(IEnderecoRepository enderecoRepository, IConfiguration configuration)
        {
            _enderecoRepository = enderecoRepository;
            _configuration = configuration;
        }

        public async Task<int> BuscarEndereco(string cep)
        {
            var responseApi = await BuscaEnderecoApi(cep);

            if (responseApi == null)
                return 0;

            var resultado = await _enderecoRepository.CadastrarEndereco(responseApi);
            if(resultado <= 0)
                return 0;

            return await _enderecoRepository.RetornarEndereco(cep);
        }
        private async Task<BuscaEnderecoDTO> BuscaEnderecoApi(string cep)
        {
            var apiCepUrl = _configuration.GetSection("ApiCep").Value;
            var request = $"{apiCepUrl}/{cep}/json/";
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    var enderecoResponse = JsonConvert.DeserializeObject<BuscaEnderecoDTO>(data);

                    BuscaEnderecoDTO buscaEndereco = new BuscaEnderecoDTO()
                    {
                        Cep = enderecoResponse.Cep,
                        Logradouro = enderecoResponse.Logradouro,
                        Complemento = enderecoResponse.Complemento,
                        Bairro = enderecoResponse.Bairro,
                        Localidade = enderecoResponse.Localidade,
                        UF = enderecoResponse.UF,
                        Ibge = enderecoResponse.Ibge,
                        Gia = enderecoResponse.Gia,
                        DDD = enderecoResponse.DDD,
                        Siafi = enderecoResponse.Siafi
                    };

                    return buscaEndereco;
                }

                return new BuscaEnderecoDTO();
            }
        }
    }
}
