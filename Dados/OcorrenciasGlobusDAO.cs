using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class OcorrenciasGlobusDAO
    {
        IDataReader usuarioReader;

        public List<OcorrenciasGlobus> Lista()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<OcorrenciasGlobus> _lista = new List<OcorrenciasGlobus>();

            try
            {
                query.Clear();
                query.Append("Select CODOCORR, DESCOCORR ");
                query.Append("  from Frq_Ocorrencia");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        OcorrenciasGlobus _ocorrencia = new OcorrenciasGlobus();

                        _ocorrencia.Descricao = usuarioReader["DESCOCORR"].ToString();
                        _ocorrencia.Codigo = Convert.ToInt32(usuarioReader["CODOCORR"].ToString());
                        _ocorrencia.CodigoENome = _ocorrencia.Codigo.ToString("000") + " - " + _ocorrencia.Descricao;

                        _lista.Add(_ocorrencia);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public List<AreaGlobus> ListarArea()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<AreaGlobus> _lista = new List<AreaGlobus>();

            try
            {
                query.Clear();
                query.Append("Select CODAREA, DESCAREA ");
                query.Append("  from Flp_Area");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        AreaGlobus _ocorrencia = new AreaGlobus();

                        _ocorrencia.Descricao = usuarioReader["DESCAREA"].ToString();
                        _ocorrencia.Codigo = Convert.ToInt32(usuarioReader["CODAREA"].ToString());
                        _ocorrencia.CodigoENome = _ocorrencia.Codigo.ToString("000") + " - " + _ocorrencia.Descricao;

                        _lista.Add(_ocorrencia);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public List<FuncoesGlobus> ListarFuncoes()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<FuncoesGlobus> _lista = new List<FuncoesGlobus>();

            try
            {
                query.Clear();
                query.Append("Select CODFUNCAO, DESCFUNCAOCOMPLETA ");
                query.Append("  from Flp_Funcao");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        FuncoesGlobus _ocorrencia = new FuncoesGlobus();

                        _ocorrencia.Descricao = usuarioReader["DESCFUNCAOCOMPLETA"].ToString();
                        _ocorrencia.Codigo = Convert.ToInt32(usuarioReader["CODFUNCAO"].ToString());
                        _ocorrencia.CodigoENome = _ocorrencia.Codigo.ToString("000") + " - " + _ocorrencia.Descricao;

                        _lista.Add(_ocorrencia);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }
        
        public List<TipoDeFrota> ListarTipoDeFrota()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<TipoDeFrota> _lista = new List<TipoDeFrota>();

            try
            {
                query.Clear();
                query.Append("Select CODIGOTPFROTA, DESCRICAOTPFROTA ");
                query.Append("  from frt_tipodefrota");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        TipoDeFrota _ocorrencia = new TipoDeFrota();

                        _ocorrencia.Descricao = usuarioReader["DESCRICAOTPFROTA"].ToString();
                        _ocorrencia.Codigo = Convert.ToInt32(usuarioReader["CODIGOTPFROTA"].ToString());

                        _lista.Add(_ocorrencia);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }
    }
}
