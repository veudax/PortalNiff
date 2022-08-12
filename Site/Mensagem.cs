using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Suportte
{
    public static class Mensagem
    {
       
        public static void ExibirMensagens(this System.Web.UI.Page page, Publicas.TipoMensagem tipo, string mensagem)
        {
            //switch(tipo)
            //{
            //    case TipoMensagem.Alerta:
            //        mensagem = "Atenção.'" + "\r\n" + "'" + mensagem;
            //        break;
            //    case TipoMensagem.Sucesso:
            //        mensagem = "Informação.'" + "\r\n" + mensagem;
            //        break;
            //    case TipoMensagem.Erro:
            //        mensagem = "Erro. \n" + mensagem;
            //        break;
            //    case TipoMensagem.Confirmacao:
            //        mensagem = "Confirmação. \n" + mensagem;
            //        break;
            //}

            string script = "<script>alert('" + mensagem + "');</script>";
            
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), tipo.ToString(), script, false);

            //popup
            //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", script);
        }
    }
}