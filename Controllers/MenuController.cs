using FEL_JAMIRA_WEB_APP.Models;
using FEL_JAMIRA_WEB_APP.Models.Areas.Cliente;
using FEL_JAMIRA_WEB_APP.Models.Areas.Estacionamento;
using FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema;
using FEL_JAMIRA_WEB_APP.Models.Areas.MultiModelação;
using FEL_JAMIRA_WEB_APP.Models.Areas.Util;
using FEL_JAMIRA_WEB_APP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FEL_JAMIRA_WEB_APP.Controllers
{
    [Authorize]
    public class MenuController : BaseController<Usuario>
    {
        [Authorize]
        public ActionResult FaleConosco()
        {
            Cliente cliente = new Cliente();
            Usuario usuario = new Usuario();
            Estacionamento estacionamento = new Estacionamento();
            List<Solicitantes> solicitacoes = new List<Solicitantes>();
            List<Solicitantes> solicitacoes2 = new List<Solicitantes>();
            List<Solicitantes> solicitacoes3 = new List<Solicitantes>();

            int level = GetLevel();

            var task1 = Task.Run(async () => {
                this.token_ = await GetToken();

                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), token_);
                    usuario = valorRetorno.Data;
                }

                if (level == 2)
                {
                    using (BaseController<Cliente> bUsuario = new BaseController<Cliente>())
                    {

                        var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Clientes/Detalhes/" + GetIdPessoa(), token_);
                        cliente = valorRetorno.Data;
                    }
                }
                else 
                {
                    using (BaseController<Estacionamento> bUsuario = new BaseController<Estacionamento>())
                    {

                        var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Estacionamentos/EstacionamentoPorPessoa?IdPessoa=" + GetIdPessoa(), token_);
                        estacionamento = valorRetorno.Data;
                    }

                    using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                    {
                        var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetSolicitacoesEmAberto?idUsuario=" + GetIdPessoa(), await GetToken());
                        solicitacoes = valorRetorno.Data;
                    }

                    using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                    {
                        var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetSolicitacoesParaFinalizar?idUsuario=" + GetIdPessoa(), await GetToken());
                        solicitacoes2 = valorRetorno.Data;
                    }

                    using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                    {
                        var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetUsuariosAtivosSolicitacao?idUsuario=" + GetIdPessoa(), await GetToken());
                        solicitacoes3 = valorRetorno.Data;
                    }
                }
            });
            task1.Wait();
            if (level == 2)
            {
                ViewBag.Nickname = cliente.Nome;
                ViewBag.InsereAlerta = !cliente.TemCarro;
                ViewBag.InsereAlerta2 = false;
                ViewBag.InsereAlerta3 = false;
                ViewBag.Level = 2;
            }
            else
            {
                ViewBag.InsereAlerta = !estacionamento.TemEstacionamento;
                ViewBag.InsereAlerta2 = solicitacoes.Count > 0 && solicitacoes.First().NomeCliente != null ? true : false;
                ViewBag.InsereAlerta3 = solicitacoes2.Count > 0 && solicitacoes2.First().NomeCliente != null ? true : false;
                ViewBag.Nickname = estacionamento.Proprietario.Nome;
                ViewBag.Level = 1;
            }
            ViewData["foto"] = usuario.Foto;
            ViewBag.Categorias = Helpers.GetSelectList("CategoriaFaleConosco", token_);
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult CadastrarFaleConosco(FaleConosco faleConosco)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseViewModel<FaleConosco> responseViewModel = new ResponseViewModel<FaleConosco>();
                    faleConosco.IdPessoa = GetIdPessoa();
                    faleConosco.DataCriacao = DateTime.Now;

                    FaleConosco retorno = new FaleConosco();
                    var task = Task.Run(async () =>
                    {
                        using (BaseController<FaleConosco> bUsuario = new BaseController<FaleConosco>())
                        {
                            var valorRetorno = await bUsuario.PostWithToken(faleConosco, "FaleConosco/Cadastrar", await GetToken());
                            responseViewModel = valorRetorno;
                        }
                    });
                    task.Wait();
                    return Json(responseViewModel, JsonRequestBehavior.AllowGet);
                }
                else
                    throw new Exception("Valores inválidos.");
            }
            catch (Exception e)
            {
                ResponseViewModel<FaleConosco> responseViewModel = new ResponseViewModel<FaleConosco>
                {
                    Data = faleConosco,
                    Mensagem = "There was an error processing your request.",
                    Serializado = true,
                    Sucesso = false
                };

                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize(Roles = "Cliente")]
        public ActionResult Cliente()
        {
            var task1 = Task.Run(async () => {
                this.token_ = await GetToken();
            });
            task1.Wait();

            GetUsuario();

            List<TransacoesDoCliente> transacoesDoClientes = new List<TransacoesDoCliente>();
            List<EstacionamentosDoCliente> estacionamentosDoClientes = new List<EstacionamentosDoCliente>();
            //Transações do Cliente e Estacionamentos do Cliente
            Cliente cliente = new Cliente();
            Usuario usuario = new Usuario();
            StatusCliente statusCliente = new StatusCliente();

            var task = Task.Run(async () => {
                using (BaseController<Cliente> bUsuario = new BaseController<Cliente>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Clientes/Detalhes/" + _usuario.IdPessoa, token_);
                    cliente = valorRetorno.Data;
                }

                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + _usuario.Id, token_);
                    usuario = valorRetorno.Data;
                }

                using (BaseController<List<TransacoesDoCliente>> bUsuario = new BaseController<List<TransacoesDoCliente>>())
                {

                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("CompraCreditos/GetTransacoesDoCliente?idUsuario=" + _usuario.IdPessoa, token_);
                    transacoesDoClientes = valorRetorno.Data;
                }

                using (BaseController<List<EstacionamentosDoCliente>> bUsuario = new BaseController<List<EstacionamentosDoCliente>>())
                {

                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetEstacionamentosDoCliente?idUsuario=" + _usuario.IdPessoa, token_);
                    estacionamentosDoClientes = valorRetorno.Data;
                }
                using (BaseController<StatusCliente> bUsuario = new BaseController<StatusCliente>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetStatusCliente?idCliente=" + GetIdPessoa(), await GetToken());
                    statusCliente = valorRetorno.Data;
                }
            });
            task.Wait();
            if (string.IsNullOrEmpty(statusCliente.Status))
            {
                statusCliente.Status = "Offline";
            }

            ViewBag.Transacoes = transacoesDoClientes.OrderByDescending(x => x.DataTransacao).ToList() as List<TransacoesDoCliente>;
            ViewBag.Estacionamentos = estacionamentosDoClientes.OrderByDescending(x => x.PeriodoDe).ToList() as List<EstacionamentosDoCliente>;
            ViewBag.InsereAlerta = !cliente.TemCarro;
            ViewBag.InsereAlerta2 = false;
            ViewBag.InsereAlerta3 = false;
            ViewBag.Nickname = cliente.Nome;
            ViewBag.StatusCliente = statusCliente;
            ViewBag.Cadastrar = "You need to register a car. click here.";
            ViewBag.Saldo = string.Format("{0:N}", cliente.Saldo);
            ViewBag.Level = 2;
            ViewData["foto"] = usuario.Foto;

            return View();
        }

        [Authorize(Roles = "Estacionamento")]
        public ActionResult Estacionamento()
        {
            var task1 = Task.Run(async () => {
                this.token_ = await GetToken();
            });
            task1.Wait();

            GetUsuario();
            Usuario usuario = new Usuario();
            List<Recebimentos> recebimentos = new List<Recebimentos>();
            List<RecebimentosFiltrados> recebimentosFiltrados = new List<RecebimentosFiltrados>();
            List<UsuariosDoEstacionamento> usuariosDoEstacionamento = new List<UsuariosDoEstacionamento>();
            List<Solicitantes> solicitacoes = new List<Solicitantes>();
            List<Solicitantes> solicitacoes2 = new List<Solicitantes>();
            List<Solicitantes> solicitacoes3 = new List<Solicitantes>();

            //Recebimentos e Usuarios do Estacionamento
            Estacionamento estacionamento = new Estacionamento();
            var task = Task.Run(async () => {

                using (BaseController<Estacionamento> bUsuario = new BaseController<Estacionamento>())
                {

                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Estacionamentos/EstacionamentoPorPessoa?IdPessoa=" + _usuario.IdPessoa, token_);
                    estacionamento = valorRetorno.Data;
                }

                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + _usuario.Id, token_);
                    usuario = valorRetorno.Data;
                }

                using (BaseController<List<Recebimentos>> bUsuario = new BaseController<List<Recebimentos>>())
                {

                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetRecebimentos?idUsuario=" + _usuario.IdPessoa, token_);
                    recebimentos = valorRetorno.Data;
                }

                using (BaseController<List<UsuariosDoEstacionamento>> bUsuario = new BaseController<List<UsuariosDoEstacionamento>>())
                {

                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetUsuariosDoEstacionamento?idUsuario=" + estacionamento.Id, token_);
                    usuariosDoEstacionamento = valorRetorno.Data;
                }

                using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetSolicitacoesEmAberto?idUsuario=" + GetIdPessoa(), await GetToken());
                    solicitacoes = valorRetorno.Data;
                }

                using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetSolicitacoesParaFinalizar?idUsuario=" + GetIdPessoa(), await GetToken());
                    solicitacoes2 = valorRetorno.Data;
                }

                using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetUsuariosAtivosSolicitacao?idUsuario=" + GetIdPessoa(), await GetToken());
                    solicitacoes3 = valorRetorno.Data;
                }

            });
            task.Wait();
            foreach (var item in recebimentos)
            {
                var it = item.MesAno.Split('-');
                RecebimentosFiltrados recebimentos1 = new RecebimentosFiltrados();
                recebimentos1.Mes = int.Parse(it[0]);
                recebimentos1.Ano = int.Parse(it[1]);
                recebimentos1.MesAno = recebimentos1.Mes + recebimentos1.Ano;
                recebimentos1.Valor = item.Valor;
                recebimentosFiltrados.Add(recebimentos1);
            }
            if (solicitacoes3.Count == 1 && solicitacoes3.First().NomeCliente == null)
                solicitacoes3 = new List<Solicitantes>();
            ViewBag.UsuariosAtivos = solicitacoes3.Count;
            ViewBag.ValorHora = estacionamento.ValorHora;
            ViewBag.Recebimentos = recebimentosFiltrados?.OrderByDescending(x => x.MesAno).ToList() as List<RecebimentosFiltrados>;
            ViewBag.Usuarios = usuariosDoEstacionamento?.OrderByDescending(x => x.PeriodoDe).ToList() as List<UsuariosDoEstacionamento>;
            ViewBag.InsereAlerta = !estacionamento.TemEstacionamento;
            ViewBag.InsereAlerta2 = solicitacoes.Count > 0 && solicitacoes.First().NomeCliente != null ? true : false;
            ViewBag.InsereAlerta3 = solicitacoes2.Count > 0 && solicitacoes2.First().NomeCliente != null ? true : false;
            ViewBag.Nickname = estacionamento.Proprietario.Nome;
            ViewBag.Cadastrar = "You need to register an address for your parking. click here.";
            ViewBag.Level = 1;
            ViewData["foto"] = usuario.Foto;
            return View();
        }

        [Authorize]
        public ActionResult MeusDados()
        {
            int meuLevel = GetLevel();
            if (meuLevel.Equals(1))
                return RedirectToAction("MeusDados", "Estacionamento");
            else if (meuLevel.Equals(2))
                return RedirectToAction("MeusDados", "Cliente");
            else
                return RedirectToAction("Login", "Autenticacao");
        }

        [Authorize]
        public ActionResult DirecionalMenu()
        {
            int meuLevel = GetLevel();
            if (meuLevel.Equals(1))
                return RedirectToAction("Estacionamento");
            else if (meuLevel.Equals(2))
                return RedirectToAction("Cliente");
            else
                return RedirectToAction("Login", "Autenticacao");
        }

        [Authorize]
        public ActionResult DirecionalCadastro()
        {
            int meuLevel = GetLevel();
            if (meuLevel.Equals(1))
                return RedirectToAction("Endereco", "Estacionamento");
            else if(meuLevel.Equals(2))
                return RedirectToAction("Carro", "Cliente");
            else
                return RedirectToAction("Login", "Autenticacao");
        }
    }
}