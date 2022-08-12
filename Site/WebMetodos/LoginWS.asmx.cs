using Classes;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Site.WebMetodos
{
    /// <summary>
    /// Descrição resumida de LoginWS
    /// </summary>
    [WebService(Namespace = "http://niff.com.br")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class LoginWS : System.Web.Services.WebService
    {

        [WebMethod]
        public Publicas.ValidacaoUsuario ValidarUsuario(string usuario, string senha)
        {
            return new LoginBO().ValidarUsuario(usuario, senha);
        }

        [WebMethod]
        public bool AlterarStatusUsuario(int idUsuario, Publicas.StatusUsuario status)
        {
            return new LoginBO().AlterarStatusUsuario(idUsuario, status);
        }
    }
}
