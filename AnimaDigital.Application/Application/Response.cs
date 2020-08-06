using Newtonsoft.Json; 
using System.Collections.Generic; 

namespace AnimaDigital.Application.Application
{
    public class Response
    {
        [JsonProperty("erros")]
        public List<string> Erros { get; private set; }

        public void AddErro(string erro)
        {
            if (Erros == null)
            {
                Erros = new List<string>();
            }

            Erros.Add(erro);
        }

        public void AddErro(List<string> erros)
        {
            if (Erros == null)
            {
                Erros = new List<string>();
            }

            Erros.AddRange(erros);
        }
    }
}
