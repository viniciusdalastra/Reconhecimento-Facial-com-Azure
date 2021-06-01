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
using System.Net;

namespace Projeto.Controller
{
    class Treinar
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
            return "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0/persongroups/" + getPersonGroupID() + "/train?";
        }

        public static async Task<Boolean> treinarApiAsync()
        {
            var client = new HttpClient();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", getChave());

            var uri = getURI();

            HttpResponseMessage response;
            byte[] byteData = Encoding.UTF8.GetBytes("{}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                return response.StatusCode == HttpStatusCode.Accepted;
            }
        }
    }
}
