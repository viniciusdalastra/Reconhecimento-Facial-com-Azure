using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Projeto.Controller
{
    public class Pessoa
    {
        private static String getPersonGroupID()
        {
            return "professores";
        }
        private static String getChave()
        {
            return "0936a2a9e73c484ab54317c982e95697";
        }
        private static String getURI()
        {
            return "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0/persongroups/" + getPersonGroupID() + "/persons?";
        }
        private static String getURIID(String id)
        {
            return "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0/persongroups/" + getPersonGroupID() + "/persons/"+id;
        }

        public static async Task<String> CriaIdPessoa(String nome)
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", getChave());

            var uri = getURI();

            HttpResponseMessage response;

            String body = "{\"name\": \""+ nome + "\"}";

            byte[] byteData = Encoding.UTF8.GetBytes(body);

            
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                responseStream = await response.Content.ReadAsStreamAsync();
                using (StreamReader responseReader = new StreamReader(responseStream))
                {
                    string sRetorno = responseReader.ReadToEnd();

                    JObject oRetorno = JObject.Parse(sRetorno);

                    foreach (JProperty propriedade in oRetorno.Properties())
                    {
                        if (propriedade.Name == "personId")
                        {
                            return (string)propriedade.Value;
                        }
                    }
                }
            }
            return "";
        }

        public static async Task removeIdPessoa(String id)
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", getChave());

            var uri = getURIID(id);

            HttpResponseMessage sRetorno = await client.DeleteAsync(uri);
        }
    }
}
