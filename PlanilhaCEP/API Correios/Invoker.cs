using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace PlanilhaCEP.API_Correios
{
    public class Invoker
    {
        public Invoker()
        {

        }
 
        public string InvokerJson(string cep)
        {
            string backStr = "";
            // link viacep.com.br/ws/79112150/json/
            string url = $"http://viacep.com.br/ws/{cep}/json/";
            var request = (HttpWebRequest)WebRequest.Create(url);

            //RESPONSE
            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.Default);
            backStr = sr.ReadToEnd();

            return backStr;
        }

        public string NomeDaCidade(string cep)
        {
            var InvokerResponse = InvokerJson(cep);
            var myDeserializedVar = JsonConvert.DeserializeObject<CEP>(InvokerResponse);

            if (myDeserializedVar.erro)
            {
                return "Cidade N/A";
            }
            else
            {
                //INFORMAÇÕES
                var Cep = myDeserializedVar.cep.ToString();
                var Cidade = myDeserializedVar.localidade.ToString();
                var UF = myDeserializedVar.uf.ToString();

                Console.WriteLine($"{Cep} : {Cidade} - {UF}");

                return Cidade;
            }
        }
    }
}
