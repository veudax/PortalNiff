using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CFOPGlobusBO
    {
        public List<CFOPGlobus> ListarCFOP()
        {
            return new CFOPGlobusDAO().ListarCFOP();
        }

        public List<CFOPGlobus> ListarCFOPEntradas()
        {
            return new CFOPGlobusDAO().ListarCFOPEntradas();
        }

        public List<CFOPGlobus> ListarCFOPSaidas()
        {
            return new CFOPGlobusDAO().ListarCFOPSaidas();
        }

        public CFOPGlobus ConsultaCFOP(int Codigo)
        {
            return new CFOPGlobusDAO().ConsultaCFOP(Codigo);
        }

        public List<CSTGlobus> ListarCST()
        {
            return new CFOPGlobusDAO().ListarCST();
        }

        public CSTGlobus ConsultaCST(int Codigo)
        {
            return new CFOPGlobusDAO().ConsultaCST(Codigo);
        }

        public List<OperacaoGlobus> ListarOperacao()
        {
            return new CFOPGlobusDAO().ListarOperacao();
        }

        public OperacaoGlobus ConsultaOperacao(int Codigo)
        {
            return new CFOPGlobusDAO().ConsultaOperacao(Codigo);
        }

        public List<LeiGlobus> ListarLeisGlobus()
        {
            return new CFOPGlobusDAO().ListarLeisGlobus();
        }

        public LeiGlobus ConsultarLeisGlobus(int codigo)
        {
            return new CFOPGlobusDAO().ConsultarLeisGlobus(codigo);
        }
    }
}
