using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MetasBO
    {
        public List<Metas> Listar(bool apenasAtivos)
        {
            return new MetasDAO().Listar(apenasAtivos);
        }

        public List<MetasBIItens> Listar(int IdMetas)
        {
            return new MetasDAO().Listar(IdMetas);
        }

        public Metas Consultar(int codigo)
        {
            return new MetasDAO().Consulta(codigo);
        }

        public int Proximo()
        {
            return new MetasDAO().Proximo();
        }

        public bool Gravar(Metas tipo, List<MetasBIItens> _lista, List<MetasContasContabeis> _contas, List<MetasBIItens> _listaDesmarcados)
        {
            return new MetasDAO().Gravar(tipo, _lista, _contas, _listaDesmarcados);
        }

        public bool Excluir(Metas tipo)
        {
            return new MetasDAO().Excluir(tipo);
        }

        public List<ValoresDasMetas> Listar(bool apenasAtivos, int idEmpresa, string referencia, string referenciaInicial = "", int idMeta = 0)
        {
            return new MetasDAO().Listar(apenasAtivos, idEmpresa, referencia, referenciaInicial, idMeta);
        }

        public List<ValoresDasMetas> ListarDRE(bool apenasAtivos, int idEmpresa, string referencia, string referenciaInicial = "", int idMeta = 0)
        {
            return new MetasDAO().ListarDRE(apenasAtivos, idEmpresa, referencia, referenciaInicial, idMeta);
        }

        public CalculoMetas Consultar(bool apenasAtivos, int idEmpresa, string referencia, int idMeta)
        {
            return new MetasDAO().Consultar(apenasAtivos, idEmpresa, referencia, idMeta);
        }

        public List<MesesUsadoNoCalculo> ListarMesesUtilizados(int IdCalculo)
        {
            return new MetasDAO().ListarMesesUtilizados(IdCalculo);
        }

        public bool Gravar(CalculoMetas tipo, List<MesesUsadoNoCalculo> mesesSelecionados)
        {
            return new MetasDAO().Gravar(tipo, mesesSelecionados);
        }

        public bool ExcluirCalculoMetas(int id)
        {
            return new MetasDAO().ExcluiCalculoMetas(id);
        }

        public decimal FeriasBase(int idEmpresa, string ano, int idMeta)
        {
            return new MetasDAO().FeriasBase(idEmpresa, ano, idMeta);
        }

        public BSCEmEdicao Consultar(int empresa, string referencia)
        {
            return new MetasDAO().Consulta(empresa, referencia);
        }

        public bool Gravar(BSCEmEdicao tipo)
        {
            return new MetasDAO().Gravar(tipo);
        }

        public bool Excluir(BSCEmEdicao tipo)
        {
            return new MetasDAO().Excluir(tipo);
        }

        public List<MetasContasContabeis> ListarContasMetas(int idEmpresa, int plano, int idMetas)
        {
            return new MetasDAO().ListarContasMetas(idEmpresa, plano, idMetas);
        }

        public List<MetasContasContabeis> ListarContasMetas(int id)
        {
            return new MetasDAO().ListarContasMetas(id);
        }

        public bool ExcluiConta(int id)
        {
            return new MetasDAO().ExcluiConta(id);
        }


    }
}
