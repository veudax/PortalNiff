using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.MasterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        string _usuarioLogado;

        protected void Page_Load(object sender, EventArgs e)
        {
            _usuarioLogado = "Olá, " + Publicas._usuariologado;

            navbarDropdownMenuLink.InnerText = _usuarioLogado;
        }
    }
}