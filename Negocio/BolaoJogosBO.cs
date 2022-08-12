using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BolaoJogosBO
    {
        public List<BolaoJogos> Listar(int Ano, bool ApenasOsEncerrados = false)
        {
            return new BolaoJogosDAO().Listar(Ano, ApenasOsEncerrados);
        }

        public List<BolaoJogos> Listar(DateTime _data)
        {
            return new BolaoJogosDAO().Listar(_data);
        }

        public List<BolaoJogos> ListarJogosSemPalpites(int Ano)
        {
            return new BolaoJogosDAO().Listar(Ano);
        }

        public BolaoJogos Consultar(DateTime _data)
        {
            return new BolaoJogosDAO().Consultar(_data);
        }

        public BolaoJogos Consultar(int _id)
        {
            return new BolaoJogosDAO().Consultar(_id);
        }

        public BolaoJogos Consultar(int _Ano, string Fase)
        {
            return new BolaoJogosDAO().Consultar(_Ano, Fase);
        }

        public bool Gravar(BolaoJogos times)
        {
            return new BolaoJogosDAO().Grava(times);
        }

        public bool Excluir(int id)
        {
            return new BolaoJogosDAO().Exclui(id);
        }
    }
}
