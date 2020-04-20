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

namespace FEL_JAMIRA_WEB_APP.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClienteController : BaseController<Pessoa>
    {
        // GET: MeusDados
        [HttpGet]
        public ActionResult MeusDados()
        {
            ViewBag.Cidade = Helpers.GetSelectList("Cidades", null) as SelectList;
            ViewBag.Estado = Helpers.GetSelectList("Estados", null) as SelectList;

            Usuario usuario = new Usuario();
            Cliente retorno = new Cliente();

            var task = Task.Run(async () => {

                using (BaseController<Cliente> bUsuario = new BaseController<Cliente>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Clientes/BuscarCliente/" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
                }

                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
                }
            });

            task.Wait();
            DadosCliente dadosCliente = new DadosCliente {
                Nome = retorno.Nome,
                Bairro = retorno.EnderecoPessoa.Bairro,
                CEP = retorno.EnderecoPessoa.CEP,
                Complemento = retorno.EnderecoPessoa.Complemento,
                CPF = retorno.CPF,
                Email = this.GetEmail(),
                IdCidade = retorno.EnderecoPessoa.IdCidade,
                IdEstado = retorno.EnderecoPessoa.IdEstado,
                Nascimento = retorno.Nascimento,
                Nickname = usuario.Nome,
                Numero = retorno.EnderecoPessoa.Numero,
                RG = retorno.RG,
                Rua = retorno.EnderecoPessoa.Rua
            };

            ViewBag.Cadastrar = "You need to register a car. click here.";
            ViewBag.Nickname = retorno.Nome;
            ViewBag.InsereAlerta = !retorno.TemCarro;
            ViewBag.InsereAlerta2 = false;
            ViewBag.InsereAlerta3 = false;
            ViewBag.Level = 2;
            ViewData["foto"] = usuario.Foto;

            return View(dadosCliente);
        }

        [HttpPost]
        public ActionResult MeusDados(DadosCliente dadosCliente)
        {
            try
            {
                ResponseViewModel<DadosCliente> responseViewModel = new ResponseViewModel<DadosCliente>();
                if (ModelState.IsValid)
                {

                    var task = Task.Run(async () =>
                    {
                        using (BaseController<DadosCliente> baseController = new BaseController<DadosCliente>())
                        {
                            var retorno = await baseController.PostWithToken(dadosCliente, "Clientes/EditarCliente", await GetToken());
                            responseViewModel = retorno;
                        }
                    });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid inputs");
                    return View(dadosCliente);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dadosCliente);
            }
            return RedirectToAction("MeusDados");
        }

        [HttpGet]
        public ActionResult CarEntranceSimulator()
        {
            Cliente retorno = new Cliente();
            List<DadosRapidosEstacionamento> estacionamentos = new List<DadosRapidosEstacionamento>();
            Usuario usuario = new Usuario();
            StatusCliente statusCliente = new StatusCliente();

            var task = Task.Run(async () => {

                using (BaseController<StatusCliente> bUsuario = new BaseController<StatusCliente>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetStatusCliente?idCliente=" + GetIdPessoa(), await GetToken());
                    statusCliente = valorRetorno.Data;
                }

                using (BaseController<Cliente> bUsuario = new BaseController<Cliente>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Clientes/BuscarCliente/" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
                }

               using (BaseController<List<DadosRapidosEstacionamento>> bUsuario = new BaseController<List<DadosRapidosEstacionamento>>())
               {
                   var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Estacionamentos/EstacionamentoDisponiveis", await GetToken());
                   estacionamentos = valorRetorno.Data;
               }

                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
                }
            });
            task.Wait();

            ViewBag.Estacionamentos = estacionamentos;
            ViewBag.Cadastrar = "You need to register a car. click here.";
            ViewBag.Nickname = retorno.Nome;
            ViewBag.InsereAlerta = !retorno.TemCarro;
            ViewBag.StatusCliente = statusCliente;
            ViewBag.InsereAlerta2 = false;
            ViewBag.InsereAlerta3 = false;
            ViewBag.Level = 2;
            ViewData["foto"] = usuario.Foto;
            ViewBag.ListaEstacionamentos = estacionamentos;

            return View();
        }

        [HttpGet]
        public ActionResult Carro()
        {
            CarroCliente carroCliente = new CarroCliente();
            ViewBag.Status = "";
            CarroRetorno retorno = new CarroRetorno();
            Usuario usuario = new Usuario();
            var task = Task.Run(async () => {
                token_ = await GetToken();

                using (BaseController<CarroRetorno> bCarro = new BaseController<CarroRetorno>())
                {
                    var valorRetorno = await bCarro.GetObjectAsyncWithToken("Carros/BuscarCarroCliente/" + GetIdPessoa(), token_);
                    retorno = valorRetorno.Data;
                }
                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), token_);
                    usuario = valorRetorno.Data;
                }
            });
            task.Wait();

            if (retorno != null && retorno.IdMarca > 0)
            {
                carroCliente = new CarroCliente
                {
                    IdMarca = retorno.IdMarca,
                    Modelo = retorno.Modelo,
                    Placa = retorno.Placa,
                    Porte = retorno.Porte
                };
                ViewBag.Status = "Update";
            }
            else
            {
                ViewBag.Status = "Register";
            }
            ViewBag.Cadastrar = "You need to register a car. click here.";
            ViewBag.Nickname = retorno.Nome;
            ViewBag.InsereAlerta = retorno.Alerta;
            ViewBag.InsereAlerta2 = false;
            ViewBag.InsereAlerta3 = false;
            ViewBag.Level = retorno.Level;
            ViewData["foto"] = usuario.Foto;

            ViewBag.Marcas =  Helpers.GetSelectList("Marcas", token_) as SelectList;
            return View(carroCliente);
        }

        [HttpPost]
        public JsonResult RegistrarCarro(CarroCliente carroCliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseViewModel<CarroCliente> responseViewModel = new ResponseViewModel<CarroCliente>();
                    carroCliente.IdCliente = GetIdPessoa();
                    var task = Task.Run(async () => {
                        using (BaseController<CarroCliente> baseController = new BaseController<CarroCliente>())
                        {
                            var retorno = await baseController.PostWithToken(carroCliente, "Carros/Registrar", await GetToken());
                            responseViewModel = retorno;
                        }
                    });
                    task.Wait();
                    return Json(responseViewModel);
                }
                else
                {
                    ResponseViewModel<CarroCliente> responseViewModel = new ResponseViewModel<CarroCliente>
                    {
                        Data = carroCliente,
                        Mensagem = "Invalid data.",
                        Serializado = true,
                        Sucesso = false
                    };

                    return Json(responseViewModel, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                ResponseViewModel<CarroCliente> responseViewModel = new ResponseViewModel<CarroCliente>
                {
                    Data = carroCliente,
                    Mensagem = "There was an error processing your request.",
                    Serializado = true,
                    Sucesso = false
                };

                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public ActionResult Creditos()
        {
            Cliente retorno = new Cliente();
            Usuario usuario = new Usuario();
            var task = Task.Run(async () => {

                using (BaseController<Cliente> bUsuario = new BaseController<Cliente>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Clientes/BuscarCliente/" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
                }
                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
                }
            });
            task.Wait();

            ViewBag.Cadastrar = "You need to register a car. click here.";
            ViewBag.Nickname = retorno.Nome;
            ViewBag.InsereAlerta = !retorno.TemCarro;
            ViewBag.InsereAlerta2 = false;
            ViewBag.InsereAlerta3 = false;
            ViewData["foto"] = usuario.Foto;
            ViewBag.Level = 2;

            return View();
        }

        [HttpPost]
        public JsonResult ComprarCreditos(string value)
        {
            try
            {
                if(!string.IsNullOrEmpty(value))
                {
                    CompraCreditos compraCreditos = new CompraCreditos {
                        Credito = Convert.ToDouble(value),
                        DataTransacao = DateTime.Now,
                        IdCliente = GetIdPessoa()
                    };

                    ResponseViewModel<CompraCreditos> responseViewModel = new ResponseViewModel<CompraCreditos>();
                    var task = Task.Run(async () => {
                        using (BaseController<CompraCreditos> baseController = new BaseController<CompraCreditos>())
                        {
                            var retorno = await baseController.PostWithToken(compraCreditos, "CompraCreditos/CreditarConta", await GetToken());
                            responseViewModel = retorno;
                        }
                    });
                    task.Wait();
                    return Json(responseViewModel, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ResponseViewModel<string> responseViewModel = new ResponseViewModel<string>
                    {
                        Data = value,
                        Mensagem = "The Value cannot be zero.",
                        Serializado = true,
                        Sucesso = false
                    };

                    return Json(responseViewModel, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                ResponseViewModel<string> responseViewModel = new ResponseViewModel<string>
                {
                    Data = value,
                    Mensagem = "There was an error processing your request.",
                    Serializado = true,
                    Sucesso = false
                };

                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult RequisitarEstacionamento(string idEstabelecimento)
        {
            try
            {
                if (!string.IsNullOrEmpty(idEstabelecimento))
                {
                    Solicitacao solicitacao = new Solicitacao
                    {
                        IdCliente = GetIdPessoa(),
                        IdEstacionamento = int.Parse(idEstabelecimento),
                        Status = 2,
                        ValorGanho = 0,
                        ValorTotal = 0,
                        ValorTotalEstacionamento = 0
                    };

                    ResponseViewModel<Solicitacao> responseViewModel = new ResponseViewModel<Solicitacao>();
                    var task = Task.Run(async () => {
                        using (BaseController<Solicitacao> baseController = new BaseController<Solicitacao>())
                        {
                            var retorno = await baseController.PostWithToken(solicitacao, "Solicitacao/CadastrarSolicitacao", await GetToken());
                            responseViewModel = retorno;
                        }
                    });
                    task.Wait();
                    return Json(responseViewModel, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ResponseViewModel<string> responseViewModel = new ResponseViewModel<string>
                    {
                        Data = idEstabelecimento,
                        Mensagem = "The value cannot be null or empty.",
                        Serializado = true,
                        Sucesso = false
                    };

                    return Json(responseViewModel, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                ResponseViewModel<string> responseViewModel = new ResponseViewModel<string>
                {
                    Data = idEstabelecimento,
                    Mensagem = "There was an error processing your request.",
                    Serializado = true,
                    Sucesso = false
                };

                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult FinalizarEstacionamento(string idSolicitacao)
        {
            try
            {
                ResponseViewModel<string> responseViewModelReturn = new ResponseViewModel<string>();
                if (!string.IsNullOrEmpty(idSolicitacao))
                {
                    ResponseViewModel<Solicitantes> responseView = new ResponseViewModel<Solicitantes>();
                    ResponseViewModel<Solicitacao> responseViewModel = new ResponseViewModel<Solicitacao>();
                    StatusCliente statusCliente = new StatusCliente();
                    var task = Task.Run(async () => {

                        using (BaseController<StatusCliente> bUsuario = new BaseController<StatusCliente>())
                        {
                            var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Solicitacao/GetStatusCliente?idCliente=" + GetIdPessoa(), await GetToken());
                            statusCliente = valorRetorno.Data;
                        }
                    });
                    task.Wait();

                    if (statusCliente.Status.Equals("Still in the park"))
                    {
                        var task2 = Task.Run(async () =>
                        {

                            using (BaseController<Solicitantes> baseController = new BaseController<Solicitantes>())
                            {
                                var retorno = await baseController.PostWithToken(null, "Solicitacao/RequisitarFinalizacao?IdSolicitacao=" + idSolicitacao, await GetToken());
                                responseView = retorno;
                            }
                        });
                        task2.Wait();

                        responseViewModelReturn = new ResponseViewModel<string>
                        {
                            Data = idSolicitacao,
                            Mensagem = "Wait for parking to respond.",
                            Serializado = true,
                            Sucesso = true
                        };

                    }
                    else
                    {
                        var task2 = Task.Run(async () =>
                        {
                            using (BaseController<Solicitacao> baseController = new BaseController<Solicitacao>())
                            {
                                var retorno = await baseController.PostWithToken(null, "Solicitacao/Deletar/" + idSolicitacao, await GetToken());
                                responseViewModel = retorno;
                            }
                        });
                        task2.Wait();

                        responseViewModelReturn = new ResponseViewModel<string>
                        {
                            Data = idSolicitacao,
                            Mensagem = "The Park not returned the request. the solicitation was deleted.",
                            Serializado = true,
                            Sucesso = false
                        };

                    }

                }
                else
                {
                    responseViewModelReturn = new ResponseViewModel<string>
                    {
                        Data = idSolicitacao,
                        Mensagem = "The value cannot be null or empty.",
                        Serializado = true,
                        Sucesso = false
                    };

                }
                return Json(responseViewModelReturn, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ResponseViewModel<string> responseViewModel = new ResponseViewModel<string>
                {
                    Data = idSolicitacao,
                    Mensagem = "There was an error processing your request.",
                    Serializado = true,
                    Sucesso = false
                };

                return Json(responseViewModel, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult Solicitacoes()
        {
            Cliente retorno = new Cliente();
            Usuario usuario = new Usuario();
            var task = Task.Run(async () => {

                using (BaseController<Cliente> bUsuario = new BaseController<Cliente>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Clientes/BuscarCliente/" + GetIdPessoa(), await GetToken());
                    retorno = valorRetorno.Data;
                }
                using (BaseController<Usuario> bUsuario = new BaseController<Usuario>())
                {
                    var valorRetorno = await bUsuario.GetObjectAsyncWithToken("Usuarios/Detalhes/" + GetIdUsuario(), await GetToken());
                    usuario = valorRetorno.Data;
                }
            });
            task.Wait();

            ViewBag.Cadastrar = "You need to register a car. click here.";
            ViewBag.Nickname = retorno.Nome;
            ViewBag.InsereAlerta = !retorno.TemCarro;
            ViewBag.InsereAlerta2 = false;
            ViewBag.InsereAlerta3 = false;
            ViewBag.Level = 2;
            ViewData["foto"] = usuario.Foto;

            return View();
        }
    }
}