using FEL_JAMIRA_WEB_APP.Controllers;
using FEL_JAMIRA_WEB_APP.Models;
using FEL_JAMIRA_WEB_APP.Models.Areas.Autenticacao;
using FEL_JAMIRA_WEB_APP.Models.Areas.Util;
using FEL_JAMIRA_WEB_APP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace FEL_JAMIRA_WEB_APP.MyRoleProvider
{
    public class SiteRole : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string values)
        {
            LoginRequisicao usuarioLogin = new LoginRequisicao
            {
                Login = values.Split('-')[3],
                Senha = Helpers.Decrypt(values.Split('-')[4]),
            };
            Usuario usuario = new Usuario();

            ResponseViewModel<Usuario> loginUsuario = new ResponseViewModel<Usuario>();
            Task.Run(async () => {
                BaseController<Usuario> baseController = new BaseController<Usuario>();
                ResponseViewModel<Usuario> returnResponse = await baseController.PostObject(usuarioLogin, "Usuarios/Login");
                usuario = returnResponse.Data;
            }).Wait();

            string tipoRole = usuario.Level.Equals(2) ? "Cliente" : "Estacionamento";
            string[] result = { tipoRole };
            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}