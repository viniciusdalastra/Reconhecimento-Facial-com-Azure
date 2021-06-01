using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Drawing;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Projeto.Controller
{
    class Reconhecedor
    {
        private static String getChave()
        {
            return "0936a2a9e73c484ab54317c982e95697";
        }
        private static String getURI()
        {
            return "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true";
        }
        public static async Task<String> reconhece(object image)
        {
            var client = new HttpClient();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", getChave());

            var uri = getURI();

            HttpResponseMessage response;
            ImageConverter converter = new ImageConverter();
            byte[] byteData = (byte[])converter.ConvertTo(image, typeof(byte[]));

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                responseStream = await response.Content.ReadAsStreamAsync();
                using (StreamReader responseReader = new StreamReader(responseStream))
                {
                    String sResponse = responseReader.ReadToEnd();
                    
                    return sResponse;
                }
            }
        }
    }
}
