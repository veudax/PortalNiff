using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class PontuacaoFatorEmpresaDAO
    {
        IDataReader dataReader;

        public List<PontuacaoFatorEmpresa> Listar(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PontuacaoFatorEmpresa> _lista = new List<PontuacaoFatorEmpresa>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.idpontos, f.idEmpresa, f.fator, e.Nomeabreviado");
                query.Append("  From Niff_Ads_FatorEmpresa f, niff_chm_empresas e");
                query.Append(" Where IdPontos = " + Codigo);
                query.Append("   And e.Idempresa = f.idempresa");
                query.Append("   And e.AvaliaColaboradores = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PontuacaoFatorEmpresa _tipo = new PontuacaoFatorEmpresa();

                        _tipo.Existe = true;
                        _tipo.Nome = dataReader["Nomeabreviado"].ToString();

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idpontos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Fator = Convert.ToDecimal(dataReader["Fator"].ToString());
                        }
                        catch { }

                        _lista.Add(_tipo);
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

        public PontuacaoFatorEmpresa Consultar(int Codigo, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            PontuacaoFatorEmpresa _tipo = new PontuacaoFatorEmpresa();

            try
            {
                query.Append("Select f.idpontos, f.idEmpresa, f.fator, e.Nomeabreviado");
                query.Append("  From Niff_Ads_FatorEmpresa f, niff_chm_empresas e");
                query.Append(" Where IdPontos = " + Codigo);
                query.Append("   And e.Idempresa = " + empresa);
                query.Append("   And e.Idempresa = f.idempresa");
                query.Append("   And e.AvaliaColaboradores = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        
                        _tipo.Existe = true;
                        _tipo.Nome = dataReader["Nomeabreviado"].ToString();

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idpontos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Fator = Convert.ToDecimal(dataReader["Fator"].ToString());
                        }
                        catch { }

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
            return _tipo;
        }


        public bool Gravar(List<PontuacaoFatorEmpresa> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                foreach (var _pontuacao in _lista)
                {
                    query.Clear();

                    if (!_pontuacao.Existe)
                    {
                        query.Append("Insert into niff_ads_fatorempresa");
                        query.Append(" ( idpontos, idEmpresa, fator )");
                        query.Append(" Values ( " + _pontuacao.Id);
                        query.Append("        , " + _pontuacao.IdEmpresa);
                        query.Append("        , " + _pontuacao.Fator.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update niff_ads_fatorempresa");
                        query.Append("   set Fator = " + _pontuacao.Fator.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" where idpontos = " + _pontuacao.Id);
                        query.Append("   and idEmpresa = " + _pontuacao.IdEmpresa);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }
                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool Excluir(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ads_fatorempresa");
                query.Append(" Where idpontos = " + codigo);
                return sessao.ExecuteSqlTransaction(query.ToString());
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }
    }
}
