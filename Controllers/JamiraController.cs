using FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema;
using FEL_JAMIRA_WEB_APP.Models.Areas.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FEL_JAMIRA_WEB_APP.Controllers
{
    [AllowAnonymous]
    public class JamiraController : Controller
    {
        // GET: Jamira
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SendEmail(string name, string phone, string email, string message)
        {
            try
            {
                ResponseViewModel<FaleConosco> envio = new ResponseViewModel<FaleConosco>();
                MensagemEnvio mEnvio = new MensagemEnvio();
                string mensagem = "Name: " + name + " - Phone: " + phone + " - Email to Contact: " + email + " - Message: " + message;
                mEnvio.mensagem = mensagem;
                var task = Task.Run(async () =>
                {
                    using (BaseController<FaleConosco> bUsuario = new BaseController<FaleConosco>())
                    {
                        var valorRetorno = await bUsuario.PostObject(mEnvio, "FaleConosco/FaleComigo");
                        envio = valorRetorno;
                    }
                });
                task.Wait();
                return Json(envio.Sucesso, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}