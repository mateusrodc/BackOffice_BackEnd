using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO
{
    public class Resultado
    {
        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public string Codigo { get; set; }

        public string Trace { get; set; }

        [JsonProperty]
        public DateTime Data { get; set; }

        public Dictionary<string, HashSet<string>> Campos { get; set; }

        HashSet<string> _erros;
        public IEnumerable<string> Erros => _erros;

        HashSet<string> _avisos;
        public IEnumerable<string> Avisos => _avisos;

        public IEnumerable<string> TodasMensagens => _erros.Union(_avisos);

        [JsonProperty]
        public bool Sucesso => Erros.Any() == false;

        [JsonProperty]
        public bool ContemErros => Erros.Any();

        [JsonProperty]
        public bool ContemAvisos => Avisos.Any();

        public void Limpar()
        {
            _erros = new HashSet<string>();
            _avisos = new HashSet<string>();
        }

        public Resultado()
            : this(Guid.NewGuid().ToString())
        {
        }

        public Resultado(string codigo)
        {
            Id = Guid.NewGuid();
            Codigo = codigo == null ? "999" : codigo;
            Data = DateTime.UtcNow;
            _erros = new HashSet<string>();
            _avisos = new HashSet<string>();
        }

        public void AdicionarErro(string erro)
        {
            if (String.IsNullOrWhiteSpace(erro))
                return;

            if (_erros.Contains(erro) == false)
                _erros.Add(erro);
        }

        public void AdicionarErros(IEnumerable<string> erros)
        {
            foreach (var error in erros)
            {
                AdicionarErro(error);
            }
        }

        public void AdicionarAviso(string aviso)
        {
            if (String.IsNullOrWhiteSpace(aviso))
                return;

            if (_avisos.Contains(aviso) == false)
                _avisos.Add(aviso);
        }

        public void AdicionarAvisos(IEnumerable<string> avisos)
        {
            foreach (var warning in avisos)
            {
                AdicionarAviso(warning);
            }
        }

        public void LimparErros() => _erros?.Clear();

        public void LimparAvisos() => _avisos?.Clear();

        public string? Erro => this.Erros.Any() ? this.Erros.FirstOrDefault() : string.Empty;

        public string? Aviso => this.Avisos.Any() ? this.Avisos.FirstOrDefault() : string.Empty;

    }

    public class Resultado<T> : Resultado
    {
        public Resultado(string codigo)
            : base(codigo) { }

        public Resultado(T valor, string codigo)
            : this(codigo)
        {
            this.Valor = valor;
        }

        public T Valor { get; set; }
    }


}
