using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AutoAvaliacaoBO
    {
        public List<AutoAvaliacao> Listar(int mesReferencia)
        {
            return new AutoAvaliacaoDAO().Listar(mesReferencia);
        }

        public List<AutoAvaliacao> Listar(string tipo, int idColaborador, string inicio = "", string fim = "")
        {
            return new AutoAvaliacaoDAO().Listar(tipo, idColaborador, inicio, fim);
        }

        public List<AutoAvaliacao> Listar(int idColaborador, string radar = "", int ano = 0)
        {
            return new AutoAvaliacaoDAO().Listar(idColaborador, radar, ano);
        }
        
        public List<AutoAvaliacao> Listar(int mesReferencia, string tipoCargo, string tipo, int idSuperior = 0)
        {
            return new AutoAvaliacaoDAO().ListarAndamento(mesReferencia, tipoCargo, tipo, idSuperior);
        }

        public List<Colaboradores> Listar(int idColaborador, int idCargo, string referencia, int idEmpresa)
        {
            return new AutoAvaliacaoDAO().Listar(idColaborador, idCargo, referencia, idEmpresa);
        }

        public List<ItensDaAutoAvaliacao> Listar(int idCargo, int idColaborador, string referencia, Publicas.TipoPrazos tipoAvaliacao)
        {
            return new ItensDaAutoAvaliacaoDAO().Listar(idCargo, idColaborador, referencia, tipoAvaliacao);
        }

        public List<ItensAvaliacaoMetas> Listar(int idCargo, int idColaborador, string referencia, int idEmpresa, string refereciaFim = "", bool naoCadastrado = false)
        {
            return new ItensAvaliacaoMetasDAO().Listar(idCargo, idColaborador, referencia, refereciaFim, idEmpresa, naoCadastrado);
        }

        public List<ItensAvaliacaoMetas> ListarContratoMetas(string referencia, int idEmpresa)
        {
            return new ItensAvaliacaoMetasDAO().Listar(referencia, idEmpresa);
        }

        public List<AutoAvaliacao> Listar(int mesReferencia, int mesReferenciaFim, bool incluirQualitativas, bool incluirNumericas, string tipoConsulta = "RH")
        {
            return new AutoAvaliacaoDAO().Listar(mesReferencia, mesReferenciaFim, incluirQualitativas, incluirNumericas, tipoConsulta);
        }

        public List<AutoAvaliacao> ListarDetalhe(int mesReferencia, int mesReferenciaFim, bool incluirQualitativas, bool incluirNumericas)
        {
            return new AutoAvaliacaoDAO().ListarDetalhe(mesReferencia, mesReferenciaFim, incluirQualitativas, incluirNumericas);
        }

        public List<AutoAvaliacao> ListarNotas(string tipo, int idSuperior)
        {
            return new AutoAvaliacaoDAO().ListarNotas(tipo, idSuperior);
        }

        public List<AutoAvaliacao> MaiorReferencia(int mesReferencia, int mesReferenciaFim)
        {
            return new AutoAvaliacaoDAO().MaiorReferencia(mesReferencia, mesReferenciaFim);
        }

        public bool SubCompetenciaEmUso(int IdSub)
        {
            return new ItensDaAutoAvaliacaoDAO().SubCompetenciaEmUso(IdSub);
        }

        public AutoAvaliacao Consultar(int codigo, string mesReferencia, string tipo, int idEmpresa)
        {
            return new AutoAvaliacaoDAO().Consulta(codigo, mesReferencia, tipo, idEmpresa);
        }

        public bool Gravar(AutoAvaliacao tipo, List<ItensDaAutoAvaliacao> _lista, List<ItensAvaliacaoMetas> _metas = null)
        {
            return new AutoAvaliacaoDAO().Gravar(tipo, _lista, _metas);
        }

        public bool Excluir(int codigo)
        {
            return new AutoAvaliacaoDAO().Excluir(codigo);
        }

        public bool FeedBackGestor(int codigo, string texto)
        {
            return new AutoAvaliacaoDAO().FeedBackGestor(codigo, texto);
        }

        public bool FeedBackColaborador(int codigo, string texto)
        {
            return new AutoAvaliacaoDAO().FeedBackColaborador(codigo, texto);
        }
    }
}
