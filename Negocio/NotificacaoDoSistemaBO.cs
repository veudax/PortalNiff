using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NotificacaoDoSistemaBO
    {
        public NotificacaoDoSistema Consultar(DateTime data)
        {
            return new NotificacaoDoSistemaDAO().Consulta(data);
        }

        public NotificacaoDoSistema Consultar()
        {
            return new NotificacaoDoSistemaDAO().Consulta();
        }

        public bool Gravar(NotificacaoDoSistema tipo)
        {
            return new NotificacaoDoSistemaDAO().Gravar(tipo);
        }

        public bool Exclui(int id)
        {
            return new NotificacaoDoSistemaDAO().Exclui(id);
        }
    }
}
