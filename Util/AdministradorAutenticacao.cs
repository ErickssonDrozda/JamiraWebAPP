using FEL_JAMIRA_WEB_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace FEL_JAMIRA_WEB_APP.Util
{
    public class AdministradorAutenticacao
    {
        /// <summary>
        /// Salva os cookies com os dados do usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="SaveUser"></param>
        /// <param name="KeyCookieIP"></param>
        public static void SetCookieParaUsuario(Usuario usuario, string KeyCookieIP)
        {
            try
            {
                var oTimeOut = DateTime.Now.AddMinutes(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CookiesTimeOut"].ToString()));
                //Serializando dados do usuário
                string nome = Helpers.Encrypt(Helpers.Encrypt(Helpers.Encrypt(new JavaScriptSerializer().Serialize(usuario))));
                var authTicket = new FormsAuthenticationTicket(1, nome, DateTime.Now, oTimeOut, false, ".");
                var encTicket = FormsAuthentication.Encrypt(authTicket);
                string voucher = usuario.Id.ToString() + "-" + usuario.IdPessoa.ToString() + "-" + usuario.Level.ToString()
                  + "-" + usuario.Login.ToString() + "-" + Helpers.Encrypt(usuario.Senha);
                   
                FormsAuthentication.SetAuthCookie(voucher, true);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
                {
                    Value = encTicket,
                    Expires = DateTime.Now.AddMinutes(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CookiesTimeOut"].ToString()))
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
                AddMyCookie("KeyCookieIP", KeyCookieIP);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Adiciona um metodo generico de cookie
        /// </summary>
        /// <param name="CookieName"></param>
        /// <param name="CookieValue"></param>
        /// <returns></returns>
        public static bool AddMyCookie(string CookieName, string CookieValue)
        {
            try
            {
                var cookie = new HttpCookie(CookieName)
                {
                    Value = CookieValue,
                    Expires = DateTime.Now.AddMinutes(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CookiesTimeOut"].ToString()))
                };

                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Response.Cookies.Set(cookie);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Invalidate session to user
        /// </summary>
        public static void RemoverCookieDeUsuario()
        {
            try
            {
                //LogOut User
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Request.Cookies["authCookie"] != null)
                        HttpContext.Current.Response.Cookies["authCookie"].Expires = DateTime.Now.AddDays(-1);

                    if (HttpContext.Current.Session != null)
                    {
                        HttpContext.Current.Session.RemoveAll();
                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session.Abandon();
                    }
                    FormsAuthentication.SignOut();
                    HttpContext.Current.Application.Clear();
                    HttpContext.Current = null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void LimparCookies(string CookieName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(CookieName))
                {
                    var keys = HttpContext.Current.Request.Cookies.AllKeys.ToList();

                    foreach (var item in keys)
                    {
                        if (!item.Equals("ssnUser") && !item.Equals("__RequestVerificationToken")
                            && !item.Equals(".ASPXAUTH"))
                        {
                            HttpContext.Current.Request.Cookies.Remove(item);
                            AddMyCookie(item, null);
                        }
                    }
                }
                else if (HttpContext.Current.Request.Cookies.Get(CookieName) != null)
                {
                    if (HttpContext.Current.Request.Cookies.Get(CookieName).Value != string.Empty)
                    {
                        HttpContext.Current.Request.Cookies.Remove(CookieName);
                        AddMyCookie(CookieName, null);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}