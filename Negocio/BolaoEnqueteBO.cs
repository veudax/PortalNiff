using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BolaoEnqueteBO
    {
        public List<BolaoEnquete> Listar(int idColaborador)
        {
            return new BolaoEnqueteDAO().Listar(idColaborador);
        }

        public bool Consultar(int IdColaborador)
        {
            return new BolaoEnqueteDAO().Consultar(IdColaborador);
        }

        public int[] QuantidadeParticipantes()
        {
            return new BolaoEnqueteDAO().ListarQuantidadeParticipantes();
        }

        public int[] QuantidadeGostouBolao()
        {
            return new BolaoEnqueteDAO().QuantidadeGostouBolao();
        }

        public int[] QuantidadeMudarDivisaoPremiacao()
        {
            return new BolaoEnqueteDAO().QuantidadeMudarDivisaoPremiacao();
        }

        public int[] QuantidadeMudarPontuacao()
        {
            return new BolaoEnqueteDAO().QuantidadeMudarPontuacao();
        }

        public int[] QuantidadeMudarArtilheiro()
        {
            return new BolaoEnqueteDAO().QuantidadeMudarArtilheiro();
        }

        public int[] QuantidadeMudarPlacarInverso()
        {
            return new BolaoEnqueteDAO().QuantidadeMudarPlacarInverso();
        }

        public bool Grava(BolaoEnquete times)
        {
            return new BolaoEnqueteDAO().Grava(times);
        }
    }
}
