using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NotificacaoCorridasBO
    {
        public NotificacaoCorridas Consultar(int idCorrida)
        {
            return new NotificacaoCorridasDAO().Consulta(idCorrida);
        }

        public bool Gravar(int idCorrida)
        {
            return new NotificacaoCorridasDAO().Gravar(idCorrida);
        }
    }
}
