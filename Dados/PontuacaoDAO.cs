using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class PontuacaoDAO
    {
        IDataReader dataReader;

        public List<Pontuacao> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Pontuacao> _lista = new List<Pontuacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idpontos, mesreferencia, base100, naoatende, atendeparc, atendeplen, supera, piso, fator, PesoNumerica");
                query.Append("     , substr(lPad(mesreferencia, 6, '0'), 3, 4) || substr(lPad(mesreferencia, 6, '0'), 1, 2) Ordem");
                query.Append("  From Niff_Ads_Pontuacao ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Pontuacao _tipo = new Pontuacao();

                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idpontos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.OrdemReferencia = Convert.ToInt32(dataReader["Ordem"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.MesReferenciaFormatada = Convert.ToInt32(dataReader["mesreferencia"].ToString()).ToString("00/0000");
                        }
                        catch { }

                        try
                        {
                            _tipo.Base100 = Convert.ToInt32(dataReader["base100"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.NaoAtende = Convert.ToInt32(dataReader["naoatende"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.AtendeParcialmente = Convert.ToInt32(dataReader["atendeparc"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.AtendePlenamente = Convert.ToInt32(dataReader["atendeplen"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Supera = Convert.ToInt32(dataReader["supera"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Piso = Convert.ToInt32(dataReader["Piso"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PesoQualitativa = Convert.ToDecimal(dataReader["Fator"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PesoNumerica = Convert.ToDecimal(dataReader["PesoNumerica"].ToString());
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

        public Pontuacao Consultar(int referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Pontuacao _tipo = new Pontuacao();

            try
            {
                query.Append("Select idpontos, mesreferencia, base100, naoatende, atendeparc, atendeplen, supera, piso, Fator, PesoNumerica");
                query.Append("     , substr(lPad(mesreferencia, 6, '0'), 3, 4) || substr(lPad(mesreferencia, 6, '0'), 1, 2) Ordem");
                query.Append("  From Niff_Ads_Pontuacao ");
                query.Append(" Where MesReferencia = " + referencia);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        
                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idpontos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.OrdemReferencia = Convert.ToInt32(dataReader["Ordem"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.MesReferenciaFormatada = Convert.ToInt32(dataReader["mesreferencia"].ToString()).ToString("00/0000");
                        }
                        catch { }

                        try
                        {
                            _tipo.Base100 = Convert.ToInt32(dataReader["base100"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.NaoAtende = Convert.ToInt32(dataReader["naoatende"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.AtendeParcialmente = Convert.ToInt32(dataReader["atendeparc"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.AtendePlenamente = Convert.ToInt32(dataReader["atendeplen"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Supera = Convert.ToInt32(dataReader["supera"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Piso = Convert.ToInt32(dataReader["Piso"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PesoQualitativa = Convert.ToDecimal(dataReader["PesoNumerica"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PesoNumerica = Convert.ToDecimal(dataReader["Fator"].ToString());
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

        public Pontuacao ConsultarMaiorReferencia(int referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Pontuacao _tipo = new Pontuacao();
            string _referencia = referencia.ToString().PadLeft(6, '0');
            try
            {
                query.Append("Select idpontos, mesreferencia, base100, naoatende, atendeparc, atendeplen, supera, piso, Fator, PesoNumerica");
                query.Append("     , substr(lPad(mesreferencia, 6, '0'), 3, 4) || substr(lPad(mesreferencia, 6, '0'), 1, 2) Ordem");
                query.Append("  From Niff_Ads_Pontuacao ");
                query.Append(" Where MesReferencia = (Select Max(mesreferencia) ");
                query.Append("                          From niff_ads_pontuacao ");
                query.Append("                         Where substr(lPad(Mesreferencia, 6, '0'), 3, 4) || substr(lPad(Mesreferencia, 6, '0'), 1, 2) <= " + _referencia.Substring(2, 4) + _referencia.Substring(0, 2) + ")" );
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idpontos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.OrdemReferencia = Convert.ToInt32(dataReader["Ordem"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.MesReferenciaFormatada = Convert.ToInt32(dataReader["mesreferencia"].ToString()).ToString("00/0000");
                        }
                        catch { }

                        try
                        {
                            _tipo.Base100 = Convert.ToInt32(dataReader["base100"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.NaoAtende = Convert.ToInt32(dataReader["naoatende"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.AtendeParcialmente = Convert.ToInt32(dataReader["atendeparc"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.AtendePlenamente = Convert.ToInt32(dataReader["atendeplen"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Supera = Convert.ToInt32(dataReader["supera"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Piso = Convert.ToInt32(dataReader["Piso"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PesoQualitativa = Convert.ToDecimal(dataReader["Fator"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PesoNumerica = Convert.ToDecimal(dataReader["PesoNumerica"].ToString());
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

        public bool Gravar(Pontuacao _pontuacao, List<PontuacaoFatorEmpresa> _fatores)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            int Id = 0;
            try
            {
                query.Clear();

                if (!_pontuacao.Existe)
                {
                    query.Append("Select SQ_NIFF_AdsIdPontos.NextVal next from dual");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            Id = Convert.ToInt32(dataReader["next"].ToString());
                    }
                    query.Clear();

                    query.Append("Insert into Niff_Ads_Pontuacao");
                    query.Append(" ( idpontos, mesreferencia, base100, naoatende, atendeparc, atendeplen, supera, piso, fator, PesoNumerica )");
                    query.Append(" Values ( " + Id);
                    query.Append("        , " + _pontuacao.MesReferencia);
                    query.Append("        , " + _pontuacao.Base100 );
                    query.Append("        , " + _pontuacao.NaoAtende);
                    query.Append("        , " + _pontuacao.AtendeParcialmente);
                    query.Append("        , " + _pontuacao.AtendePlenamente);
                    query.Append("        , " + _pontuacao.Supera);
                    query.Append("        , " + _pontuacao.Piso);
                    query.Append("        , " + _pontuacao.PesoQualitativa.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.PesoNumerica.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" )");
                }
                else
                {
                    Id = _pontuacao.Id;

                    query.Append("Update Niff_Ads_Pontuacao");
                    query.Append("   set base100 = " + _pontuacao.Base100);
                    query.Append("     , naoAtende = " + _pontuacao.NaoAtende);
                    query.Append("     , AtendeParc = " + _pontuacao.AtendeParcialmente);
                    query.Append("     , AtendePlen = " + _pontuacao.AtendePlenamente);
                    query.Append("     , Supera = " + _pontuacao.Supera);
                    query.Append("     , Piso = " + _pontuacao.Piso);
                    query.Append("     , Fator = " + _pontuacao.PesoQualitativa.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , PesoNumerica = " + _pontuacao.PesoNumerica.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" where idpontos = " + _pontuacao.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    _fatores.ForEach(u => u.Id = Id);

                    retorno = new PontuacaoFatorEmpresaDAO().Gravar(_fatores);
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
            bool retorno = false;
            try
            {
                retorno = new PontuacaoFatorEmpresaDAO().Excluir(codigo);

                if (retorno)
                {
                    query.Append("Delete Niff_Ads_Pontuacao");
                    query.Append(" Where idpontos = " + codigo);
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
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
    }
}
