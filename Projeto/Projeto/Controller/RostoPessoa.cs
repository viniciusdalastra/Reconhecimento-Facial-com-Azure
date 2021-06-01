using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Projeto.Controller
{
    class RostoPessoa
    {
        private static String getPersonGroupID()
        {
            return "professores";
        }
        private static String getChave()
        {
            return "0936a2a9e73c484ab54317c982e95697";
        }
        private static String getURI(String idPessoa)
        {
            return "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0/persongroups/"+ getPersonGroupID()+ "/persons/"+ idPessoa + "/persistedFaces";
        }

        public static async Task<Boolean> adicionaFaceAsync(String idPessoa, object imagens)
        {
            var client = new HttpClient();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", getChave());

            var uri = getURI(idPessoa);

            HttpResponseMessage response;
            ImageConverter converter = new ImageConverter();
            byte[] byteData = (byte[])converter.ConvertTo(imagens, typeof(byte[]));

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                responseStream = await response.Content.ReadAsStreamAsync();
                using (StreamReader responseReader = new StreamReader(responseStream))
                {
                    String sResponse = responseReader.ReadToEnd();
                    return sResponse.Contains("persistedFaceId");
                }
            }
        }
    }
}
