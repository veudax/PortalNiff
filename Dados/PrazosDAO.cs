using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class PrazosDAO
    {
        IDataReader dataReader;

        public List<Prazos> Listar(bool apenasAtivos, bool apenasReferencia, bool apenasAno)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Prazos> _lista = new List<Prazos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (apenasAno)
                    query.Append("Select Distinct Substr(Lpad(Referencia, 6, '0'), 3, 4) Ano");
                else
                if (apenasReferencia)
                {
                    query.Append("Select Distinct Substr(Lpad(Referencia, 6, '0'), 1, 2) || '/' || Substr(Lpad(Referencia, 6, '0'), 3, 4) referencia");
                    query.Append("     , Substr(Lpad(Referencia, 6, '0'), 3, 4) || Substr(Lpad(Referencia, 6, '0'), 1, 2) ordem");
                }
                else
                    query.Append("Select idprazos, referencia, tipo, ativo, inicio, fim, EnvioEmail");

                query.Append("  from Niff_ADS_Prazos");

                if (apenasAtivos)
                {
                    query.Append(" Where ativo = 'S'");

                    if (apenasReferencia || apenasAno)
                    {
                        query.Append("   and inicio <= Sysdate");
                        query.Append(" Order By " + (apenasReferencia ? "ordem" : "Ano") + " Desc");
                    }
                }
                else
                {
                    if (apenasReferencia)
                    {
                        query.Append(" Where inicio <= Sysdate");
                        query.Append(" Order By " + (apenasReferencia ? "ordem" : "Ano") + " Desc");
                    }
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Prazos _tipo = new Prazos();

                        _tipo.Existe = true;

                        if (apenasReferencia)
                            _tipo.Referencia = dataReader["referencia"].ToString();
                        else
                        if (apenasAno)
                            _tipo.Referencia = dataReader["Ano"].ToString();
                        else
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idprazos"].ToString());
                            _tipo.Referencia = dataReader["referencia"].ToString().PadLeft(6, '0');
                            _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                         (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                         (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                         (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                         (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                         (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado :
                                         Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

                            _tipo.Inicio = Convert.ToDateTime(dataReader["Inicio"].ToString());
                            _tipo.Fim = Convert.ToDateTime(dataReader["Fim"].ToString());
                            _tipo.DescricaoTipo = Publicas.GetDescription(_tipo.Tipo, "");
                            _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                            _tipo.EnvioEmail = dataReader["EnvioEmail"].ToString();
                        }


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

        public Prazos Consulta(int refencia, Publicas.TipoPrazos tipo, int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Prazos _tipo = new Prazos();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idprazos, referencia, tipo, ativo, inicio, fim, EnvioEmail");
                query.Append("  from Niff_ADS_Prazos");

                if (codigo != 0)
                    query.Append(" Where idprazos = " + codigo);
                else
                {
                    query.Append(" Where referencia = " + refencia);
                    query.Append("   and Tipo = '" + (tipo == Publicas.TipoPrazos.AutoAvaliacao ? "AA" :
                        (tipo == Publicas.TipoPrazos.FeedbackGestor ? "FG" :
                        (tipo == Publicas.TipoPrazos.MetasNumericas ? "MN" :
                        (tipo == Publicas.TipoPrazos.AvaliacaoDoGestor ? "AG" :
                        (tipo == Publicas.TipoPrazos.AvaliacaoRH ? "AR" :
                        (tipo == Publicas.TipoPrazos.FeedbackAvaliado ? "FA" : "PD")))))) + "'");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idprazos"].ToString());
                        _tipo.Referencia = dataReader["referencia"].ToString().PadLeft(6, '0');
                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                     (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                     (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                     (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                     (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                     (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado :
                                        Publicas.TipoPrazos.PlanoDeDesenvolvimento ))))));

                        _tipo.Inicio = Convert.ToDateTime(dataReader["Inicio"].ToString());
                        _tipo.Fim = Convert.ToDateTime(dataReader["Fim"].ToString());
                        _tipo.DescricaoTipo = Publicas.GetDescription(_tipo.Tipo, "");
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.EnvioEmail = dataReader["EnvioEmail"].ToString();
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

        public Prazos Consulta(DateTime data, string tipo, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Prazos _tipo = new Prazos();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idprazos, referencia, tipo, ativo, inicio, fim, EnvioEmail");
                query.Append("  from Niff_ADS_Prazos");

                query.Append(" Where ativo = 'S'");

                if (referencia != "")
                    query.Append("   and referencia = " + referencia);
                else
                    query.Append("   and To_date('" + data.ToShortDateString() + "', 'dd/mm/yyyy') between inicio and fim");

                query.Append("   and Tipo = '" + tipo + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idprazos"].ToString());
                        _tipo.Referencia = dataReader["referencia"].ToString().PadLeft(6, '0');
                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                     (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                     (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                     (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                     (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                     (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado :
                                     Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

                        _tipo.Inicio = Convert.ToDateTime(dataReader["Inicio"].ToString());
                        _tipo.Fim = Convert.ToDateTime(dataReader["Fim"].ToString());
                        _tipo.DescricaoTipo = Publicas.GetDescription(_tipo.Tipo, "");
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.EnvioEmail = dataReader["EnvioEmail"].ToString();
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

        public Prazos ConsultaCicloAvaliacao(int referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Prazos _tipo = new Prazos();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Min(inicio) Inicio, Max(Fim) Fim");
                query.Append("  from Niff_ADS_Prazos");

                query.Append(" Where ativo = 'S'");
                query.Append("   and referencia = " + referencia);
                query.Append("   and Tipo in ('AA','AG','AR','PD') ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                       
                        _tipo.Inicio = Convert.ToDateTime(dataReader["Inicio"].ToString());
                        _tipo.Fim = Convert.ToDateTime(dataReader["Fim"].ToString());
                       
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

        public bool Gravar(Prazos tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_ADS_Prazos");
                    query.Append(" ( idprazos, referencia, tipo, ativo, inicio, fim, EnvioEmail )");
                    query.Append(" Values ( SQ_NIFF_ADSIdPrazos.NextVal");
                    query.Append("        ,'" + tipo.Referencia + "'");
                    query.Append("        ,'" + (tipo.Tipo == Publicas.TipoPrazos.AutoAvaliacao ? "AA" :
                        (tipo.Tipo == Publicas.TipoPrazos.FeedbackGestor ? "FG" :
                        (tipo.Tipo == Publicas.TipoPrazos.MetasNumericas ? "MN" :
                        (tipo.Tipo == Publicas.TipoPrazos.AvaliacaoDoGestor ? "AG" :
                        (tipo.Tipo == Publicas.TipoPrazos.AvaliacaoRH ? "AR" :
                        (tipo.Tipo == Publicas.TipoPrazos.FeedbackAvaliado ? "FA" : "PD")))))) + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("        , To_date('" + tipo.Inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , To_date('" + tipo.Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , '" + tipo.EnvioEmail + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_ADS_Prazos");
                    query.Append("   set Ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , Inicio = To_date('" + tipo.Inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , Fim = To_date('" + tipo.Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , EnvioEmail = '" + tipo.EnvioEmail + "'");
                    
                    query.Append(" Where idprazos = " + tipo.Id);
                }

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

        public bool Excluir(Prazos tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_ADS_Prazos");
                query.Append(" Where idprazos = " + tipo.Id);
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
