using FEL_JAMIRA_WEB_APP.Models;
using FEL_JAMIRA_WEB_APP.Models.Areas.Autenticacao;
using FEL_JAMIRA_WEB_APP.Models.Areas.Localizacao;
using FEL_JAMIRA_WEB_APP.Models.Areas.Util;
using FEL_JAMIRA_WEB_APP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FEL_JAMIRA_WEB_APP.Controllers
{
    [AllowAnonymous]
    public class AutenticacaoController : BaseController<Usuario>
    {
        [System.Web.Http.Route("~/")]
        public ActionResult StartApplication() 
        {
            return RedirectToAction("Login");
        }
        // GET: Autenticacao/Login
        public ActionResult Login()
        {
            var pessoa = GetLevel();
            if (pessoa != -1)
                if (pessoa == 1)
                    return RedirectToAction("Estacionamento", "Menu");
                else
                    return RedirectToAction("Cliente", "Menu");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginRequisicao usuario)
        {
            if (ModelState.IsValid)
            {
                ResponseViewModel<Usuario> loginUsuario = new ResponseViewModel<Usuario>();
                Task.Run(async () => {
                    ResponseViewModel<Usuario> returnResponse = await PostObject(usuario, "Usuarios/Login");
                    loginUsuario = returnResponse;
                }).Wait();
                if (loginUsuario.Sucesso.Equals(true))
                {
                    string KeyCookieIP = Guid.NewGuid().ToString().Substring(0, 7);
                    loginUsuario.Data.Senha = usuario.Senha;

                    AdministradorAutenticacao.SetCookieParaUsuario(loginUsuario.Data, KeyCookieIP);

                    if (loginUsuario.Data.Level.Equals(1))
                        return RedirectToAction("Estacionamento", "Menu");
                    else
                        return RedirectToAction("Cliente", "Menu");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, loginUsuario.Mensagem);
                    return View(usuario);
                }
                    
            }
            else
            {
                return View(usuario);
            }
        }

        // GET: Autenticacao/RegistrarCliente
        public ActionResult RegistrarCliente()
        {
            ViewBag.Cidade = Helpers.GetSelectList("Cidades", null) as SelectList;
            ViewBag.Estado = Helpers.GetSelectList("Estados", null) as SelectList;

            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCliente(CadastroCliente cadastroCliente)
        {
            if (ModelState.IsValid)
            {
                cadastroCliente.Foto = ConverterFotoParaByte(cadastroCliente.Arquivo);
                cadastroCliente.Arquivo = null;
                ResponseViewModel<Usuario> responseViewModel = new ResponseViewModel<Usuario>();
                Task.Run(async () => {
                    ResponseViewModel<Usuario> returnResponse = await PostObject(cadastroCliente, "Clientes/CadastrarCliente");
                    responseViewModel = returnResponse;
                }).Wait();

                if (responseViewModel.Sucesso)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Cidade = Helpers.GetSelectList("Cidades", null) as SelectList;
                    ViewBag.Estado = Helpers.GetSelectList("Estados", null) as SelectList;
                    ModelState.AddModelError(string.Empty, responseViewModel.Mensagem);
                    return View(cadastroCliente);
                }
            }
            else
            {
                ViewBag.Cidade = Helpers.GetSelectList("Cidades", null) as SelectList;
                ViewBag.Estado = Helpers.GetSelectList("Estados", null) as SelectList;
                return View(cadastroCliente);
            }
        }

        // GET: Autenticacao/RegistrarEstacionamento
        public ActionResult RegistrarEstacionamento()
        {
            ViewBag.Cidade = Helpers.GetSelectList("Cidades", null) as SelectList;
            ViewBag.Estado = Helpers.GetSelectList("Estados", null) as SelectList;
            ViewBag.Banco = Helpers.GetSelectList("Banco", null) as SelectList;
            ViewBag.TipoConta = Helpers.GetSelectList("TipoConta", null) as SelectList;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarEstacionamento(CadastroEstacionamento cadastroEstacionamento)
        {
            if (ModelState.IsValid)
            {
                cadastroEstacionamento.Foto = ConverterFotoParaByte(cadastroEstacionamento.Arquivo);
                cadastroEstacionamento.Arquivo = null;

                ResponseViewModel<Usuario> responseViewModel = new ResponseViewModel<Usuario>();
                Task.Run(async () => {
                    ResponseViewModel<Usuario> returnResponse = await PostObject(cadastroEstacionamento, "Estacionamentos/CadastrarFornecedor");
                    responseViewModel = returnResponse;
                }).Wait();

                if (responseViewModel.Sucesso)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Cidade = Helpers.GetSelectList("Cidades", null) as SelectList;
                    ViewBag.Estado = Helpers.GetSelectList("Estados", null) as SelectList;
                    ViewBag.Banco = Helpers.GetSelectList("Banco", null) as SelectList;
                    ViewBag.TipoConta = Helpers.GetSelectList("TipoConta", null) as SelectList;

                    ModelState.AddModelError(string.Empty, responseViewModel.Mensagem);
                    return View(cadastroEstacionamento);
                }
            }
            else
            {
                ViewBag.Cidade = Helpers.GetSelectList("Cidades", null) as SelectList;
                ViewBag.Estado = Helpers.GetSelectList("Estados", null) as SelectList;
                ViewBag.Banco = Helpers.GetSelectList("Banco", null) as SelectList;
                ViewBag.TipoConta = Helpers.GetSelectList("TipoConta", null) as SelectList;
                return View(cadastroEstacionamento);
            }
        }
    }
}