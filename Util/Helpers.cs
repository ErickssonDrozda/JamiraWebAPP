using FEL_JAMIRA_WEB_APP.Controllers;
using FEL_JAMIRA_WEB_APP.Models.Areas.ContaDeposito;
using FEL_JAMIRA_WEB_APP.Models.Areas.Localizacao;
using FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema;
using FEL_JAMIRA_WEB_APP.Models.Areas.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FEL_JAMIRA_WEB_APP.Util
{
    public class Helpers
    {

        /// <summary>
        /// Encrypt string
        /// </summary>
        /// <param name="DecryptedStr"></param>
        /// <returns></returns>
        public static string Encrypt(string DecryptedStr)
        {
            Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(DecryptedStr);
            string EncryptedStr = Convert.ToBase64String(b);
            return EncryptedStr;
        }

        /// <summary>
        /// Decrypt string
        /// </summary>
        /// <param name="CryptedStr"></param>
        /// <returns></returns>
        public static string Decrypt(string CryptedStr)
        {
            Byte[] b = Convert.FromBase64String(CryptedStr);
            string DecryptedStr = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return DecryptedStr;
        }
        /// <summary>
        /// Lista para que retorne um conjunto de valores de seleção.
        /// </summary>
        /// <param name="TituloRetorno"></param>
        /// <returns></returns>
        public static SelectList GetSelectList(string TituloRetorno, AcessoToken token = null)
        {
            switch (TituloRetorno)
            {
                case "Cidades":
                    using (BaseController<List<Cidade>> b1 = new BaseController<List<Cidade>>())
                    {
                        ResponseViewModel<List<Cidade>> retornoCidades = new ResponseViewModel<List<Cidade>>();
                        var task = Task.Run(async () => {
                            ResponseViewModel<List<Cidade>> returnTask = await b1.GetObjectAsync("Cidades/GetAll");
                            retornoCidades = returnTask;
                        });
                        task.Wait();

                        List<SelectListItem> selectListItem = new List<SelectListItem>();
                        SelectListItem selectedListItem = new SelectListItem { Selected = true, Text = "Selecione", Value = "99999" };
                        selectListItem.Add(selectedListItem);
                        
                        foreach (var item in retornoCidades.Data)
                        {
                            SelectListItem selectTempListItem = new SelectListItem { Selected = false, Text = item.NomeCidade, Value = item.Id.ToString() };
                            selectListItem.Add(selectTempListItem);
                        }
                        SelectList selectList = new SelectList(selectListItem, "Text", "Value", 99999);
                        return selectList;

                    }
                case "Estados":
                    using (BaseController<List<Estado>> b2 = new BaseController<List<Estado>>())
                    {
                        ResponseViewModel<List<Estado>> retornoEstados = new ResponseViewModel<List<Estado>>();

                        var task = Task.Run(async () => {
                            ResponseViewModel<List<Estado>> returnTask = await b2.GetObjectAsync("Estados/GetAll");
                            retornoEstados = returnTask;
                        });
                        task.Wait();

                        List<SelectListItem> selectListItem = new List<SelectListItem>();
                        SelectListItem selectedListItem = new SelectListItem { Selected = true, Text = "Selecione", Value = "99999" };
                        selectListItem.Add(selectedListItem);

                        foreach (var item in retornoEstados.Data)
                        {
                            SelectListItem selectTempListItem = new SelectListItem { Selected = false, Text = item.Sigla, Value = item.Id.ToString() };
                            selectListItem.Add(selectTempListItem);
                        }
                        SelectList selectList = new SelectList(selectListItem, "Text", "Value", 99999);
                        return selectList;
                    }
                case "Marcas":
                    using (BaseController<List<Marca>> b2 = new BaseController<List<Marca>>())
                    {
                        ResponseViewModel<List<Marca>> retornoMarca = new ResponseViewModel<List<Marca>>();

                        var task = Task.Run(async () => {
                            ResponseViewModel<List<Marca>> returnTask = await b2.GetObjectAsyncWithToken("Marcas/GetAll", token);
                            retornoMarca = returnTask;
                        });
                        task.Wait();

                        List<SelectListItem> selectListItem = new List<SelectListItem>();
                        SelectListItem selectedListItem = new SelectListItem { Selected = true, Text = "Selecione", Value = "99999" };
                        selectListItem.Add(selectedListItem);

                        foreach (var item in retornoMarca.Data)
                        {
                            SelectListItem selectTempListItem = new SelectListItem { Selected = false, Text = item.NomeMarca, Value = item.Id.ToString() };
                            selectListItem.Add(selectTempListItem);
                        }
                        SelectList selectList = new SelectList(selectListItem, "Text", "Value", 99999);
                        return selectList;
                    }
                case "CategoriaFaleConosco":
                    using (BaseController<List<CategoriaFaleConosco>> b2 = new BaseController<List<CategoriaFaleConosco>>())
                    {
                        ResponseViewModel<List<CategoriaFaleConosco>> retornoEstados = new ResponseViewModel<List<CategoriaFaleConosco>>();

                        var task = Task.Run(async () => {
                            ResponseViewModel<List<CategoriaFaleConosco>> returnTask = await b2.GetObjectAsyncWithToken("CategoriaFaleConosco/GetAll", token);
                            retornoEstados = returnTask;
                        });
                        task.Wait();

                        List<SelectListItem> selectListItem = new List<SelectListItem>();
                        SelectListItem selectedListItem = new SelectListItem { Selected = true, Text = "Selecione", Value = "99999" };
                        selectListItem.Add(selectedListItem);

                        foreach (var item in retornoEstados.Data)
                        {
                            SelectListItem selectTempListItem = new SelectListItem { Selected = false, Text = item.NomeCategoria, Value = item.Id.ToString() };
                            selectListItem.Add(selectTempListItem);
                        }
                        SelectList selectList = new SelectList(selectListItem, "Text", "Value", 99999);
                        return selectList;
                    }
                case "Banco":
                    using (BaseController<List<Banco>> b2 = new BaseController<List<Banco>>())
                    {
                        ResponseViewModel<List<Banco>> retornoEstados = new ResponseViewModel<List<Banco>>();

                        var task = Task.Run(async () => {
                            ResponseViewModel<List<Banco>> returnTask = await b2.GetObjectAsync("Estacionamentos/GetBancos");
                            retornoEstados = returnTask;
                        });
                        task.Wait();

                        List<SelectListItem> selectListItem = new List<SelectListItem>();
                        SelectListItem selectedListItem = new SelectListItem { Selected = true, Text = "Selecione", Value = "99999" };
                        selectListItem.Add(selectedListItem);

                        foreach (var item in retornoEstados.Data)
                        {
                            SelectListItem selectTempListItem = new SelectListItem { Selected = false, Text = item.Nome, Value = item.Id.ToString() };
                            selectListItem.Add(selectTempListItem);
                        }
                        SelectList selectList = new SelectList(selectListItem, "Text", "Value", 99999);
                        return selectList;
                    }
                case "TipoConta":
                    using (BaseController<List<TipoConta>> b2 = new BaseController<List<TipoConta>>())
                    {
                        ResponseViewModel<List<TipoConta>> retornoEstados = new ResponseViewModel<List<TipoConta>>();

                        var task = Task.Run(async () => {
                            ResponseViewModel<List<TipoConta>> returnTask = await b2.GetObjectAsync("Estacionamentos/GetTipoContas");
                            retornoEstados = returnTask;
                        });
                        task.Wait();

                        List<SelectListItem> selectListItem = new List<SelectListItem>();
                        SelectListItem selectedListItem = new SelectListItem { Selected = true, Text = "Selecione", Value = "99999" };
                        selectListItem.Add(selectedListItem);

                        foreach (var item in retornoEstados.Data)
                        {
                            SelectListItem selectTempListItem = new SelectListItem { Selected = false, Text = item.Nome, Value = item.Id.ToString() };
                            selectListItem.Add(selectTempListItem);
                        }
                        SelectList selectList = new SelectList(selectListItem, "Text", "Value", 99999);
                        return selectList;
                    }
                default:
                    return new SelectList(null);
            }
        }
        /// <summary>
        /// Busca o IP do Usuário
        /// </summary>
        /// <returns>string</returns>
        public static string GetIPAddress()
        {
            try
            {
                HttpContext context = HttpContext.Current;
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                var IpAddress = context.Request.ServerVariables["REMOTE_ADDR"];

                if (!string.IsNullOrEmpty(IpAddress))
                    return IpAddress;
                else
                {
                    string[] addresses = ipAddress.Split(',');
                    return addresses[0];
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter Endereço IP - " + e.Message);
            }
        }
    }
}