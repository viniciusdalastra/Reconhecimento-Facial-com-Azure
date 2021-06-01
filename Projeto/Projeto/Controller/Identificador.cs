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
using Npgsql;

namespace Projeto.Controller
{
    class Identificador
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
            return "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0/identify";
        }
        public static async Task<Boolean> reconhece(NpgsqlConnection conexao, string sResponse,object imagem, FormHistorico telaHistorico)
        {
            string sFaceId = "";
            Boolean first = false;
            Boolean bInclui = false;
            var client = new HttpClient();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", getChave());

            var uri = getURI();

            string json = sResponse;
            JArray aResponse = JArray.Parse(json);

            foreach (JObject oResponse in aResponse.Children<JObject>())
            {
                foreach (JProperty propriedade in oResponse.Properties())
                {
                    string nome = propriedade.Name;

                    if (nome == "faceId")
                    {
                        if (first)
                        {
                            sFaceId = sFaceId + ",\"" + (string)propriedade.Value + "\"";
                        }
                        else
                        {
                            sFaceId = sFaceId + "\"" + (string)propriedade.Value + "\"";
                        }
                    }
                    first = true;
                }
            }

            HttpResponseMessage response;
            String body = "{\"PersonGroupId\": \"" + getPersonGroupID() + "\"," +
                           "\"faceIds\" : [" + sFaceId + "]," +
                           "\"maxNumOfCandidatesReturned\": 1," +
                           "\"confidenceThreshold\": 0.55}";

            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                responseStream = await response.Content.ReadAsStreamAsync();
                using (StreamReader responseReader = new StreamReader(responseStream))
                {
                    String sRetorno = responseReader.ReadToEnd();

                    if (sRetorno.Contains("error")) {
                        return false;
                    }

                    JArray aRetorno = JArray.Parse(sRetorno);

                    foreach (JObject oRetorno in aRetorno.Children<JObject>())
                    {
                        Boolean inicial = true;
                        string faceId = "";
                        string identificador = "";
                        foreach (JProperty propriedadeRetorno in oRetorno.Properties())
                        {
                            string nome = propriedadeRetorno.Name;

                            if (nome == "faceId")
                            {
                                faceId = (string)propriedadeRetorno.Value;
                            }
                            else if (nome == "candidates")
                            {
                                JArray aCandidatos = (JArray)propriedadeRetorno.Value;

                                foreach (JObject oCandidato in aCandidatos.Children<JObject>())
                                {
                                    foreach (JProperty propriedadeCandidato in oCandidato.Properties())
                                    {
                                        string nomeCandidato = propriedadeCandidato.Name;

                                        if (nomeCandidato == "personId")
                                        {
                                            identificador = (string)propriedadeCandidato.Value;
                                            bInclui = ControllerReconhecimento.setIncluiAsync(conexao, identificador);
                                        }
                                    }
                                }
                            }
                            if (!inicial && telaHistorico != null)
                            {
                                adicionaRostoHistorico(conexao, faceId, identificador, sResponse, imagem, telaHistorico);
                            }
                            inicial = false;
                        }
                    }

                    return bInclui;
                }
            }
        }

        private static void adicionaRostoHistorico(NpgsqlConnection conexao,string faceId, string identificador,string sResponse,object imagem, FormHistorico telaHistorico)
        {
            string nomePessoa = ControllerPessoaIdentificador.getNomePessoa(conexao, identificador);
            string json = sResponse;
            JArray aResponse = JArray.Parse(json);

            foreach (JObject oResponse in aResponse.Children<JObject>())
            {
                foreach (JProperty propriedade in oResponse.Properties())
                {
                    string nome = propriedade.Name;

                    if (nome == "faceId" && (string)propriedade.Value != faceId)
                    {
                        break;
                    }
                    else if (nome == "faceRectangle")
                    {
                        Retangulo retangulo = propriedade.Value.ToObject<Retangulo>();
                        Bitmap myBitmap = (Bitmap)imagem;
                        Rectangle cloneRect = new Rectangle(retangulo.left,retangulo.top, (retangulo.width + 20),(retangulo.height+20));
                        Bitmap BitmapCortado = myBitmap.Clone(cloneRect, myBitmap.PixelFormat);

                        Graphics grafico = Graphics.FromImage(BitmapCortado);
                        grafico.DrawString(nomePessoa, new System.Drawing.Font("Tahoma", 10), Brushes.Purple, new PointF(0, 0));

                        telaHistorico.pictureBox4.Image = telaHistorico.pictureBox3.Image;
                        telaHistorico.pictureBox3.Image = telaHistorico.pictureBox2.Image;
                        telaHistorico.pictureBox2.Image = telaHistorico.pictureBox1.Image;
                        telaHistorico.pictureBox1.Image = BitmapCortado;
                    }
                }
            }
        }
    }
}
