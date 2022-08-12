using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class OperacionalBO
    {
        #region Indicadores
        public List<Operacional.Indicadores> ListarIndicadores(bool somenteAtivos)
        {
            return new OperacionalDAO().Listar(somenteAtivos);
        }

        public Operacional.Indicadores ConsultarIndicadores(int codigo)
        {
            return new OperacionalDAO().Consulta(codigo);
        }

        public bool Gravar(Operacional.Indicadores item)
        {
            return new OperacionalDAO().Grava(item);
        }

        public bool ExcluirIndicadores(int Codigo)
        {
            return new OperacionalDAO().Exclui(Codigo);
        }

        public int Proximo()
        {
            return new OperacionalDAO().Proximo();
        }
        #endregion

        #region Setor
        public List<Operacional.Setor> ListarSetores(bool somenteAtivos, int empresa)
        {
            return new OperacionalDAO().ListarSetores(somenteAtivos, empresa);
        }

        public Operacional.Setor ConsultaSetor(int codigo, int empresa)
        {
            return new OperacionalDAO().ConsultaSetor(codigo, empresa);
        }

        public bool Grava(Operacional.Setor item, List<Operacional.Vigencia> vigencia)
        {
            return new OperacionalDAO().Grava(item, vigencia);
        }

        public bool ExcluirSetor(int Codigo)
        {
            return new OperacionalDAO().ExcluiSetor(Codigo);
        }

        public int ProximoSetor()
        {
            return new OperacionalDAO().ProximoSetor();
        }

        public int ProximoCodigoSetorEmpresa(int empresa)
        {
            return new OperacionalDAO().ProximoCodigoSetorEmpresa(empresa);
        }

        public bool ExcluirLinhaDaVigencia(int id)
        {
            return new OperacionalDAO().ExcluiLinhaDaVigencia(id);
        }

        public bool ExcluirVigencia(DateTime vigencia)
        {
            return new OperacionalDAO().ExcluiVigencia(vigencia);
        }

        public List<Operacional.Vigencia> ListarVigencias(int setor, bool grid = false)
        {
            return new OperacionalDAO().ListarVigencias(setor, grid);
        }

        public List<Operacional.Vigencia> ListarLinhasVigencias(int setor, DateTime vigencia)
        {
            return new OperacionalDAO().ListarLinhasVigencias(setor, vigencia);
        }
        #endregion

        #region Metas
        public List<Operacional.Metas> ListarMetas(bool somenteAtivos)
        {
            return new OperacionalDAO().ListarMetas(somenteAtivos);
        }

        public List<Operacional.Metas> ListarMetas(int empresa, int indicador)
        {
            return new OperacionalDAO().ListarMetas(empresa, indicador);
        }

        public Operacional.Metas ConsultarMetas(int empresa, int indicador, DateTime vigencia, bool maiorVigencia = false)
        {
            return new OperacionalDAO().Consulta(empresa, indicador, vigencia, maiorVigencia);
        }

        public bool Gravar(Operacional.Metas item)
        {
            return new OperacionalDAO().Grava(item);
        }

        public bool ExcluirMetas(int Codigo)
        {
            return new OperacionalDAO().ExcluiMetas(Codigo);
        }

        #endregion

        #region Pontuacao
        public List<Operacional.Pontuacao> Listar(bool somenteAtivos, int empresa)
        {
            return new OperacionalDAO().Listar(somenteAtivos, empresa);
        }

        public List<Operacional.VigenciaPontuacao> ListarVigenciasPontuacao(int id)
        {
            return new OperacionalDAO().ListarVigenciasPontuacao(id);
        }

        public Operacional.Pontuacao Consultar(int codigo, int empresa)
        {
            return new OperacionalDAO().Consulta(codigo, empresa);
        }

        public Operacional.VigenciaPontuacao Consultar(int id, DateTime vigencia)
        {
            return new OperacionalDAO().Consultar(id, vigencia);
        }

        public bool Gravar(Operacional.Pontuacao item, Operacional.VigenciaPontuacao vigencia)
        {
            return new OperacionalDAO().Grava(item, vigencia);
        }

        public bool ExcluirPontuacao(int Codigo)
        {
            return new OperacionalDAO().ExcluiPontuacao(Codigo);
        }

        public int ProximaPontuacao(int empresa)
        {
            return new OperacionalDAO().ProximaPontuacao(empresa);
        }

        public bool ExcluirVigenciaPontuacao(int id)
        {
            return new OperacionalDAO().ExcluiVigenciaPontuacao(id);
        }
        #endregion

        #region valores
        public List<Operacional.Valores> Listar(int empresa)
        {
            return new OperacionalDAO().Listar(empresa);
        }

        public Operacional.Valores Consultar(int empresa, DateTime data, int indicador, string periodo, int linha)
        {
            return new OperacionalDAO().Consultar(empresa, data, indicador, periodo, linha);
        }

        public Operacional.Valores Consultar(int id)
        {
            return new OperacionalDAO().Consultar(id);
        }

        public bool Gravar(Operacional.Valores item)
        {
            return new OperacionalDAO().Grava(item);
        }

        public bool ExcluirValores(int Codigo)
        {
            return new OperacionalDAO().ExcluiValores(Codigo);
        }
        #endregion

        #region Linhas
        public List<Operacional.Linhas> ListarLinhas(int IdLinha)
        {
            return new OperacionalDAO().ListarLinhas(IdLinha);
        }

        public bool Gravar(List<Operacional.Linhas> linhas)
        {
            return new OperacionalDAO().Grava(linhas);
        }

        public bool ExcluirLinhas(int Codigo)
        {
            return new OperacionalDAO().ExcluiLinhas(Codigo);
        }

        public bool ExcluirLinhasAssociada(int Codigo, int linha)
        {
            return new OperacionalDAO().ExcluiLinhasAssociada(Codigo, linha);
        }
        #endregion

        #region Consultas
        public List<Operacional.Demonstrativo> ListarDemonstrativo(int empresa, DateTime inicio, DateTime Fim)
        {
            return new OperacionalDAO().ListarDemonstrativo(empresa, inicio, Fim);
        }

        public List<Operacional.Demonstrativo> ListarDemonstrativoMensal(int empresa, DateTime inicio, DateTime Fim)
        {
            return new OperacionalDAO().ListarDemonstrativoMensal(empresa, inicio, Fim);
        }

        public List<Operacional.IQO> ListarIQO(int empresa, DateTime inicio, DateTime Fim)
        {
            return new OperacionalDAO().ListarIQO(empresa, inicio, Fim);
        }

        public int QuantidadeFuncionariosEscalados(int empresa, DateTime data, int linha, string periodo)
        {
            return new OperacionalDAO().QuantidadeFuncionariosEscalados(empresa, data, linha, periodo);
        }

        public int QuantidadeVeiculosEscalados(int empresa, DateTime data, int linha, string periodo)
        {
            return new OperacionalDAO().QuantidadeVeiculosEscalados(empresa, data, linha, periodo);
        }

        public int FCV(int empresa, DateTime data, int linha, string periodo)
        {
            return new OperacionalDAO().FCV(empresa, data, linha, periodo);
        }

        public int? SOS(int empresa, DateTime data, int linha)
        {
            return new OperacionalDAO().SOS(empresa, data, linha);
        }

        public int? RA(int empresa, DateTime data, int linha)
        {
            return new OperacionalDAO().RA(empresa, data, linha);
        }

        public int PAX(int empresa, DateTime data, int linha)
        {
            return new OperacionalDAO().PAX(empresa, data, linha);
        }

        public decimal ReceitaMunicipal(int empresa, DateTime data, int linha)
        {
            return new OperacionalDAO().ReceitaMunicipal(empresa, data, linha);
        }

        public decimal Receita(int empresa, DateTime data, int linha)
        {
            return new OperacionalDAO().Receita(empresa, data, linha);
        }

        public decimal KM(int empresa, DateTime data, int linha)
        {
            return new OperacionalDAO().KM(empresa, data, linha);
        }

        public decimal Consumo(int empresa, DateTime data, int linha)
        {
            return new OperacionalDAO().Consumo(empresa, data, linha);
        }
        #endregion
    }
}
