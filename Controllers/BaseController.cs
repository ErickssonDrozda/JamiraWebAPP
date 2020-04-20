using FEL_JAMIRA_WEB_APP.Models;
using FEL_JAMIRA_WEB_APP.Models.Areas.Cliente;
using FEL_JAMIRA_WEB_APP.Models.Areas.Estacionamento;
using FEL_JAMIRA_WEB_APP.Models.Areas.Util;
using FEL_JAMIRA_WEB_APP.Util;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace FEL_JAMIRA_WEB_APP.Controllers
{
    //[RequireHttps]
    //[SessionExpire]
    //[SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class BaseController<T> : Controller
    {

        string    URI = System.Configuration.ConfigurationManager.AppSettings["UrlWebAPI"]; 
        string    UrlParm = System.Configuration.ConfigurationManager.AppSettings["UrlParameter"];
        protected string idCookie;
        protected Usuario _usuario;
        protected AcessoToken token_;

        HttpClient client = new HttpClient();
        [Authorize]
        public async Task<AcessoToken> GetToken()
        {
            string url = URI + "token";
            AcessoToken responseViewModel = new AcessoToken();
            string parametroBusca = "username=" + GetEmail().Replace("@", "%40") + "&password=" + Helpers.Decrypt(GetSenha()) + "&grant_type=password";

            var client = new RestClient(url.Replace("/api",""));
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cookie", "ARRAffinity=c13d130f8c400a60bfdc01febad530e6a1d1e9e931c8df17592f4f879ee76550");
            request.AddHeader("Content-Length", parametroBusca.Length.ToString());
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", UrlParm);
            request.AddHeader("Postman-Token", "5982999d-d151-4f76-82ea-9199e1e3a19a,2ca0f27d-7897-4598-bebc-4577c9c2b8cb");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.17.1");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", parametroBusca, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            responseViewModel = JsonConvert.DeserializeObject<AcessoToken>(response.Content);
            return responseViewModel;

        }
        [Authorize]
        public async Task<ResponseViewModel<T>> PostWithToken(object oParametro, string complemento, AcessoToken token)
        {
            string url = URI + complemento;
            var data = JsonConvert.SerializeObject(oParametro);
            ResponseViewModel<T> responseViewModel = new ResponseViewModel<T>();

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", data.Length.ToString());
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", UrlParm);
            request.AddHeader("Postman-Token", "4b3481cf-d11a-4afc-a600-490c27950f27,726d7fab-e9a8-4c8f-b3db-69cc3fada191");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.17.1");
            request.AddHeader("Authorization", "Bearer " + token.access_token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", data, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel<T>>(response.Content);
            return responseViewModel;
        }
        [Authorize]
        public async Task<ResponseViewModel<T>> GetObjectAsyncWithToken(string complemento, AcessoToken token)
        {
            string url = URI + complemento;
            ResponseViewModel<T> responseViewModel = new ResponseViewModel<T>();

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cookie", "ARRAffinity=c13d130f8c400a60bfdc01febad53new JsonConvert.DeserializeObject0e6a1d1e9e931c8df17592f4f879ee76550");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", UrlParm);
            request.AddHeader("Postman-Token", "ccb102ac-d035-4074-b572-97750f6f8d66,8fae199d-1732-413c-bf3d-eef93fb60cec");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.17.1");
            request.AddHeader("Authorization", "Bearer " + token.access_token);
            IRestResponse response = client.Execute(request);

            responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel<T>>(response.Content);
            return responseViewModel;
        }
        public async Task<ResponseViewModel<T>> GetObjectAsync(string complemento)
        {
            try
            {
                string url = URI + complemento;
                var response = await client.GetStringAsync(url);
                var objectReturn = JsonConvert.DeserializeObject<ResponseViewModel<T>>(response);
                return objectReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseViewModel<T>> PostObject(object oParametro, string complemento)
        {
            try
            {
                string url = URI + complemento;
                var data = JsonConvert.SerializeObject(oParametro);
                ResponseViewModel<T> responseViewModel = new ResponseViewModel<T>();

                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Content-Length", data.Length.ToString());
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Host", UrlParm);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", data, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel<T>>(response.Content);
                return responseViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        public string GetSenha()
        {
            if (Request.Cookies["authCookie"] != null)
            {
                var decTicket = FormsAuthentication.Decrypt(Request.Cookies["authCookie"].Value);
                var senha = decTicket.Name.Split('-');
                return senha[4];
            }
            else
                return "";
        }
        [Authorize]
        public string GetEmail()
        {
            if (Request.Cookies["authCookie"] != null)
            {
                var decTicket = FormsAuthentication.Decrypt(Request.Cookies["authCookie"].Value);
                var email = decTicket.Name.Split('-');
                return email[3];
            }
            else
                return "";
        }
        [Authorize]
        public int GetLevel()
        {
            if (Request.Cookies["authCookie"] != null)
            {
                var decTicket = FormsAuthentication.Decrypt(Request.Cookies["authCookie"].Value);
                var idLevel = decTicket.Name.Split('-');
                return int.Parse(idLevel[2]);
            }
            else
                return -1;
        }
        [Authorize]
        public void GetUsuario()
        {
            if (Request.Cookies["authCookie"] != null)
            {
                var decTicket = FormsAuthentication.Decrypt(Request.Cookies["authCookie"].Value);
                var idUsuario = decTicket.Name.Split('-');
                SetarUsuarioLogado(idUsuario[0]);
            }
            else
                RedirectToAction("Login", "Autenticacao");
        }
        [Authorize]
        public int GetIdUsuario()
        {
            if (Request.Cookies["authCookie"] != null)
            {
                var decTicket = FormsAuthentication.Decrypt(Request.Cookies["authCookie"].Value);
                var idUsuario = decTicket.Name.Split('-');
                return int.Parse(idUsuario[0]);
            }
            else
                return 0;
        }
        public HttpPostedFileBase ConverterByteParaFoto(byte[] valor) 
        {
            HttpPostedFileBase objFile = (HttpPostedFileBase)new MemoryPostedFile(valor);
            return objFile;
        }
        [Authorize]
        public int GetIdPessoa()
        {
            if (Request.Cookies["authCookie"] != null)
            {
                var decTicket = FormsAuthentication.Decrypt(Request.Cookies["authCookie"].Value);
                var idPessoa = decTicket.Name.Split('-');
                return int.Parse(idPessoa[1]);
            }
            else
                return 0;
        }
        public void SetarUsuarioLogado(string id)
        {
            var task = Task.Run(async () => {
                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                { 
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + id, token_);
                    _usuario = valorRetorno.Data;
                }
            });
            task.Wait();
        }
        [Authorize]
        public async Task DeletaProdutoAsync(object produto, string complemento)
        {
            //string url = "http://www.macwebapi.somee.com/api/produtos/{0}";
            string url = URI + complemento;
            var uri = new Uri(string.Format(url));
            await client.DeleteAsync(uri);
        }
        public ActionResult DesconectarUsuario()
        {
            Util.Util.DesconectarUsuario();
            Dispose();
            return RedirectToAction("Login","Autenticacao");
        }
        public byte[] ConverterFotoParaByte(HttpPostedFileBase arquivo)
        {
            byte[] data;
            using (Stream inputStream = arquivo.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }
    }
}
