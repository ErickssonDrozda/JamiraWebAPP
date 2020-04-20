using FEL_JAMIRA_WEB_APP.Models;
using FEL_JAMIRA_WEB_APP.Models.Areas.ContaDeposito;
using FEL_JAMIRA_WEB_APP.Models.Areas.Estacionamento;
using FEL_JAMIRA_WEB_APP.Models.Areas.Localizacao;
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

namespace FEL_JAMIRA_WEB_APP.Controllers
{
    [Authorize(Roles = "Estacionamento")]
    public class EstacionamentoController : BaseController<Pessoa>
    {
        // GET: MeusDados
        [HttpGet]
        public ActionResult MeusDados()
        {
            Estacionamento retorno = new Estacionamento();
            Usuario usuario = new Usuario();
            List<Solicitantes> solicitacoes = new List<Solicitantes>();
            List<Solicitantes> solicitacoes2 = new List<Solicitantes>();
            ContaDeposito contaDeposito = new ContaDeposito();
            var task = Task.Run(async () => {

                using (BaseController<Estacionamento> bUsuario = new BaseController<Estacionamento>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Estacionamentos/EstacionamentoPorPessoa?IdPessoa=" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
                }

                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
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

                using (BaseController<ContaDeposito> bUsuario = new BaseController<ContaDeposito>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Estacionamentos/GetContaDepositoPorPessoa?IdPessoa=" + GetIdPessoa(), await GetToken());
                    contaDeposito = valorRetorno.Data;
                }
            });
            task.Wait();

            if (null == retorno.EnderecoEstacionamento)
            {
                retorno.EnderecoEstacionamento = new Endereco();
            }

            DadosEstacionamento dadosEstacionamento = new DadosEstacionamento {

                CNPJ = retorno.CNPJ,
                CPF = retorno.Proprietario.CPF,
                Email = GetEmail(),
                InscricaoEstadual = retorno.InscricaoEstadual,
                Nascimento = retorno.Proprietario.Nascimento,
                Nickname = usuario.Nome,
                NomeEstacionamento = retorno.NomeEstacionamento,
                NomeProprietario = retorno.Proprietario.Nome,
                RG = retorno.Proprietario.RG,
                ValorHora = retorno.ValorHora,

                Agencia = contaDeposito.Agencia,
                Conta = contaDeposito.Conta,
                IdBanco = contaDeposito.IdBanco,
                IdTipoConta = contaDeposito.IdTipoConta,

                CEPEstacionamento = retorno.EnderecoEstacionamento.CEP,
                RuaEstacionamento = retorno.EnderecoEstacionamento.Rua,
                NumeroEstacionamento = retorno.EnderecoEstacionamento.Numero,
                BairroEstacionamento = retorno.EnderecoEstacionamento.Bairro,
                ComplementoEstacionamento = retorno.EnderecoEstacionamento.Complemento,
                IdCidadeEstacionamento = retorno.EnderecoEstacionamento.IdCidade,
                IdEstadoEstacionamento = retorno.EnderecoEstacionamento.IdEstado,

                CEP = retorno.Proprietario.EnderecoPessoa.CEP,
                Bairro = retorno.Proprietario.EnderecoPessoa.Bairro,
                Complemento = retorno.Proprietario.EnderecoPessoa.Complemento,
                IdCidade = retorno.Proprietario.EnderecoPessoa.IdCidade,
                IdEstado = retorno.Proprietario.EnderecoPessoa.IdEstado,
                Numero = retorno.Proprietario.EnderecoPessoa.Numero,
                Rua = retorno.Proprietario.EnderecoPessoa.Rua
            };

            ViewBag.InsereAlerta = !retorno.TemEstacionamento;
            ViewBag.InsereAlerta2 = solicitacoes.Count > 0 && solicitacoes.First().NomeCliente != null ? true : false;
            ViewBag.InsereAlerta3 = solicitacoes2.Count > 0 && solicitacoes2.First().NomeCliente != null ? true : false;
            ViewBag.Nickname     = retorno.Proprietario.Nome;
            ViewBag.Cidade       = Helpers.GetSelectList("Cidades") as SelectList;
            ViewBag.Estado       = Helpers.GetSelectList("Estados") as SelectList;
            ViewBag.Banco        = Helpers.GetSelectList("Banco", null) as SelectList;
            ViewBag.TipoConta    = Helpers.GetSelectList("TipoConta", null) as SelectList;
            ViewBag.Level        = GetLevel();
            ViewBag.Cadastrar = "You need to register an address for your parking. click here.";           
            ViewData["foto"] = usuario.Foto;
            return View(dadosEstacionamento);
        }

        [HttpPost]
        public ActionResult MeusDados(DadosEstacionamento dadosEstacionamento)
        {
            try
            {
                ResponseViewModel<DadosEstacionamento> responseViewModel = new ResponseViewModel<DadosEstacionamento>();
                if (ModelState.IsValid)
                {

                    var task = Task.Run(async () =>
                    {
                        using (BaseController<DadosEstacionamento> baseController = new BaseController<DadosEstacionamento>())
                        {
                            var retorno = await baseController.PostWithToken(dadosEstacionamento, "Estacionamentos/EditarFornecedor", await GetToken());
                            responseViewModel = retorno;
                        }
                    });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid inputs");
                    return View(dadosEstacionamento);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dadosEstacionamento);
            }
            return RedirectToAction("MeusDados");
        }

        public async Task<JsonResult> GetRecebimentos()
        {
            BaseController<Recebimentos> baseController = new BaseController<Recebimentos>();
            ResponseViewModel<Recebimentos> retorno = new ResponseViewModel<Recebimentos>();
            int UsuarioLogado = GetIdPessoa();
            retorno = await baseController.PostObject(UsuarioLogado,"Estacionamentos/GetRecebimentos");
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Endereco()
        {
            EnderecoEstacionamento enderecoEstacionamento = new EnderecoEstacionamento();
            List<Solicitantes> solicitacoes = new List<Solicitantes>();
            List<Solicitantes> solicitacoes2 = new List<Solicitantes>();
            ViewBag.Status = "";
            ViewBag.Cidade = Helpers.GetSelectList("Cidades") as SelectList;
            ViewBag.Estado = Helpers.GetSelectList("Estados") as SelectList;
            Estacionamento retorno = new Estacionamento();
            Usuario usuario = new Usuario();
            var task = Task.Run(async () => {
                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
                }
                using (BaseController<Estacionamento> bUsuario = new BaseController<Estacionamento>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Estacionamentos/EstacionamentoPorPessoa?IdPessoa=" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
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
            });
            task.Wait();

            if (retorno.IdEnderecoEstabelecimento > 0)
            {
                enderecoEstacionamento = new EnderecoEstacionamento
                {
                    Bairro = retorno.EnderecoEstacionamento.Bairro,
                    CEP = retorno.EnderecoEstacionamento.CEP,
                    Complemento = retorno.EnderecoEstacionamento.Complemento,
                    IdCidade = retorno.EnderecoEstacionamento.IdCidade,
                    IdEstado = retorno.EnderecoEstacionamento.IdEstado,
                    Numero = retorno.EnderecoEstacionamento.Numero,
                    Rua = retorno.EnderecoEstacionamento.Rua
                };
                ViewBag.Status = "Atualizar";
            }
            else
            {
                ViewBag.Status = "Cadastrar";
            }

            ViewBag.Cadastrar = "Você precisa cadastrar um endereco para seu estacionamento. clique aqui.";
            ViewBag.Nickname = retorno.Proprietario.Nome;
            ViewBag.InsereAlerta = !retorno.TemEstacionamento;
            ViewBag.InsereAlerta2 = solicitacoes.Count > 0 && solicitacoes.First().NomeCliente != null ? true : false;
            ViewBag.InsereAlerta3 = solicitacoes2.Count > 0 && solicitacoes2.First().NomeCliente != null ? true : false;
            ViewBag.Level = 1;
            ViewData["foto"] = usuario.Foto;
            return View(enderecoEstacionamento);
        }

        [HttpPost]
        public JsonResult RegistrarEstacionamento(EnderecoEstacionamento enderecoEstacionamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseViewModel<EnderecoEstacionamento> responseViewModel = new ResponseViewModel<EnderecoEstacionamento>();
                    enderecoEstacionamento.IdPessoa = GetIdPessoa();
                    var task = Task.Run(async () => {
                        using (BaseController<EnderecoEstacionamento> baseController = new BaseController<EnderecoEstacionamento>())
                        {
                            var retorno = await baseController.PostWithToken(enderecoEstacionamento, "Enderecos/Registrar", await GetToken());
                            responseViewModel = retorno;
                        }
                    });
                    task.Wait();
                    return Json(responseViewModel, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ResponseViewModel<EnderecoEstacionamento> responseViewModel = new ResponseViewModel<EnderecoEstacionamento>
                    {
                        Data = enderecoEstacionamento,
                        Mensagem = "Invalid data.",
                        Serializado = true,
                        Sucesso = false
                    };

                    return Json(responseViewModel, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                ResponseViewModel<EnderecoEstacionamento> responseViewModel = new ResponseViewModel<EnderecoEstacionamento>
                {
                    Data = enderecoEstacionamento,
                    Mensagem = "There was an error processing your request.",
                    Serializado = true,
                    Sucesso = false
                };

                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UsuariosNoEstacionamento()
        {
            List<Solicitantes> retorno = new List<Solicitantes>();
            List<Solicitantes> solicitacoes = new List<Solicitantes>();
            List<Solicitantes> solicitacoes2 = new List<Solicitantes>();
            Usuario usuario = new Usuario();
            var task = Task.Run(async () => {

                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
                }
                using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetUsuariosAtivosSolicitacao?idUsuario=" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
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
            });
            task.Wait();

            ViewBag.Cadastrar = "Você precisa cadastrar um endereco para seu estacionamento. clique aqui.";
            ViewBag.Nickname = (retorno.First()).Nickname;
            ViewBag.InsereAlerta = (retorno.First()).InsereAlerta;
            ViewBag.InsereAlerta2 = solicitacoes.Count > 0 && solicitacoes.First().NomeCliente != null ? true : false;
            ViewBag.InsereAlerta3 = solicitacoes2.Count > 0 && solicitacoes2.First().NomeCliente != null ? true : false;
            ViewBag.Level = 1;
            ViewData["foto"] = usuario.Foto;
            if (retorno.First().NomeCliente == null)
            {
                ViewBag.Solicitacoes = new List<Solicitantes>();
            }
            else
            {
                ViewBag.Solicitacoes = retorno;
            }
            return View();
        }

        [HttpGet]
        public ActionResult SolicitacoesEmAberto()
        {
            List<Solicitantes> retorno = new List<Solicitantes>();
            List<Solicitantes> solicitacoes2 = new List<Solicitantes>();
            Usuario usuario = new Usuario();
            var task = Task.Run(async () => {
                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
                }
                using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetSolicitacoesEmAberto?idUsuario=" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
                }
                using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetSolicitacoesParaFinalizar?idUsuario=" + GetIdPessoa(), await GetToken());
                    solicitacoes2 = valorRetorno.Data;
                }
            });
            task.Wait();

            ViewBag.Cadastrar = "Você precisa cadastrar um endereco para seu estacionamento. clique aqui.";
            ViewBag.Nickname = (retorno.First()).Nickname;
            ViewBag.InsereAlerta = (retorno.First()).InsereAlerta;
            ViewBag.InsereAlerta2 = retorno.Count > 0 && retorno.First().NomeCliente != null ? true : false;
            ViewBag.InsereAlerta3 = solicitacoes2.Count > 0 && solicitacoes2.First().NomeCliente != null ? true : false;
            ViewBag.Level = 1;
            ViewData["foto"] = usuario.Foto;
            if (retorno.First().NomeCliente == null)
            {
                ViewBag.Solicitacoes = new List<Solicitantes>();
            }
            else
            {
                ViewBag.Solicitacoes = retorno;
            }

            return View();
        }

        [HttpPost]
        public JsonResult AprovarSolicitacao(string id)
        {
            try
            {
                int valor = int.Parse(id);
                Solicitantes retorno = new Solicitantes();
                var task = Task.Run(async () => {

                    using (BaseController<Solicitantes> bUsuario = new BaseController<Solicitantes>())
                    {
                        var valorRetorno = await bUsuario.PostWithToken("","Solicitacao/AprovarSolicitacao?IdSolicitacao=" + valor, await GetToken());
                        retorno = valorRetorno.Data;
                    }
                });
                task.Wait();

                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                ResponseViewModel<EnderecoEstacionamento> responseViewModel = new ResponseViewModel<EnderecoEstacionamento>
                {
                    Data = null,
                    Mensagem = "There was an error processing your request." + e.Message,
                    Serializado = true,
                    Sucesso = false
                };
                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult SolicitacoesParaFinalizar()
        {
            List<Solicitantes> retorno = new List<Solicitantes>();
            List<Solicitantes> solicitacoes = new List<Solicitantes>();
            Usuario usuario = new Usuario();
            var task = Task.Run(async () => {
                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
                }
                using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetSolicitacoesParaFinalizar?idUsuario=" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
                }

                using (BaseController<List<Solicitantes>> bUsuario = new BaseController<List<Solicitantes>>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetSolicitacoesEmAberto?idUsuario=" + GetIdPessoa(), await GetToken());
                    solicitacoes = valorRetorno.Data;
                }

            });
            task.Wait();

            ViewBag.Cadastrar = "Você precisa cadastrar um endereco para seu estacionamento. clique aqui.";
            ViewBag.Nickname = (retorno.First()).Nickname;
            ViewBag.InsereAlerta = (retorno.First()).InsereAlerta;
            ViewBag.InsereAlerta2 = solicitacoes.Count > 0 && solicitacoes.First().NomeCliente != null ? true : false;
            ViewBag.InsereAlerta3 = retorno.Count > 0 && retorno.First().NomeCliente != null ? true : false;
            ViewBag.Level = 1;
            ViewData["foto"] = usuario.Foto;
            if (retorno.First().NomeCliente == null)
            {
                ViewBag.Solicitacoes = new List<Solicitantes>();
            }
            else
            {
                ViewBag.Solicitacoes = retorno;
            }
            return View();
        }

        [HttpPost]
        public JsonResult FinalizarSolicitacao(string id)
        {
            try
            {
                int valor = int.Parse(id);
                Solicitantes retorno = new Solicitantes();
                var task = Task.Run(async () => {

                    using (BaseController<Solicitantes> bUsuario = new BaseController<Solicitantes>())
                    {
                        var valorRetorno = await bUsuario.PostWithToken("", "Solicitacao/FinalizarSolicitacao?IdSolicitacao=" + valor, await GetToken());
                        retorno = valorRetorno.Data;
                    }
                });
                task.Wait();

                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                ResponseViewModel<EnderecoEstacionamento> responseViewModel = new ResponseViewModel<EnderecoEstacionamento>
                {
                    Data = null,
                    Mensagem = "There was an error processing your request." + e.Message,
                    Serializado = true,
                    Sucesso = false
                };
                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult ReprovarSolicitacao(string id)
        {
            try
            {
                int valor = int.Parse(id);
                Solicitantes retorno = new Solicitantes();
                var task = Task.Run(async () => {

                    using (BaseController<Solicitantes> bUsuario = new BaseController<Solicitantes>())
                    {
                        var valorRetorno = await bUsuario.PostWithToken("", "Solicitacao/ReprovarSolicitacao?IdSolicitacao=" + valor, await GetToken());
                        retorno = valorRetorno.Data;
                    }
                });
                task.Wait();

                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                ResponseViewModel<EnderecoEstacionamento> responseViewModel = new ResponseViewModel<EnderecoEstacionamento>
                {
                    Data = null,
                    Mensagem = "There was an error processing your request." + e.Message,
                    Serializado = true,
                    Sucesso = false
                };
                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }

        }
    }
}