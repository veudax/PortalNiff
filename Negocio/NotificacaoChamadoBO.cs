using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NotificacaoChamadoBO
    {
        public NotificacaoChamado Consultar(int id, int idHistorico)
        {
            return new NotificacaoChamadoDAO().Consultar(id, idHistorico);
        }

        public List<NotificacaoChamado> Listar()
        {
            return new NotificacaoChamadoDAO().Listar();
        }

        public bool Gravar(NotificacaoChamado notificacao)
        {
            return new NotificacaoChamadoDAO().Gravar(notificacao);
        }
    }
}
