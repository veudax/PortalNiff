using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RadarBIBO
    {
        public List<RadarBI> Listar(string empresa, string rubrica)
        {
            return new RadarBIDAO().Listar(empresa, rubrica);
        }

        public RadarBI Consultar(string empresa, string rubrica, DateTime data)
        {
            return new RadarBIDAO().Consultar(empresa, rubrica, data);
        }

        public bool Gravar(RadarBI radar)
        {
            return new RadarBIDAO().Gravar(radar);
        }

        public bool Excluir(string empresa, string rubrica, DateTime data)
        {
            return new RadarBIDAO().Excluir(empresa, rubrica, data);
        }
    }
}
