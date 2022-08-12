using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PontuacaoBO
    {
        public List<Pontuacao> Listar()
        {
            return new PontuacaoDAO().Listar();
        }

        public List<PontuacaoFatorEmpresa> Listar(int Codigo)
        {
            return new PontuacaoFatorEmpresaDAO().Listar(Codigo);
        }

        public Pontuacao Consultar(int referencia)
        {
            return new PontuacaoDAO().Consultar(referencia);
        }

        public Pontuacao ConsultarMaiorReferencia(int referencia)
        {
            return new PontuacaoDAO().ConsultarMaiorReferencia(referencia);
        }

        public PontuacaoFatorEmpresa Consultar(int Codigo, int empresa)
        {
            return new PontuacaoFatorEmpresaDAO().Consultar(Codigo, empresa);
        }

        public bool Gravar(Pontuacao _pontuacao, List<PontuacaoFatorEmpresa> _fatores)
        {
            return new PontuacaoDAO().Gravar(_pontuacao, _fatores);
        }

        public bool Excluir(int codigo)
        {
            return new PontuacaoDAO().Excluir(codigo);
        }
    }
}
