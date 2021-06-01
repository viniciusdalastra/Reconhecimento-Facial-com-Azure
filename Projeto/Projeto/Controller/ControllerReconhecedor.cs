using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;

namespace Projeto.Controller
{
    static class ControllerReconhecedor
    {
        static void Main()
        {
            realizaReconhecimentoImagemAsync();
        }

        private static async System.Threading.Tasks.Task realizaReconhecimentoImagemAsync()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            //@todo Pegar uma Chave com o marcondes
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{6244a7f4ec094b1d951f4ec26ec0c33}");

            // Request parameters
            queryString["returnFaceId"] = "true";
            var uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
                response = await client.PostAsync(uri, content);
            }
        }
    }
}
