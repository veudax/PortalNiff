using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CFOPGlobusDAO
    {
        IDataReader dataReader;

        public List<CFOPGlobus> ListarCFOP()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CFOPGlobus> _lista = new List<CFOPGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodClassFisc, DescClassFisc");
                query.Append("  from EsfClass");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CFOPGlobus _cfop = new CFOPGlobus();

                        _cfop.Existe = true;
                        _cfop.CFOP = Convert.ToInt32(dataReader["CodClassFisc"].ToString());
                        _cfop.Descricao = dataReader["DescClassFisc"].ToString();

                        _lista.Add(_cfop);
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

        public List<CFOPGlobus> ListarCFOPEntradas()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CFOPGlobus> _lista = new List<CFOPGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodClassFisc, DescClassFisc");
                query.Append("  from EsfClass");
                query.Append(" Where CodClassFisc < 4000");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CFOPGlobus _cfop = new CFOPGlobus();

                        _cfop.Existe = true;
                        _cfop.CFOP = Convert.ToInt32(dataReader["CodClassFisc"].ToString());
                        _cfop.Descricao = dataReader["DescClassFisc"].ToString();

                        _lista.Add(_cfop);
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

        public List<CFOPGlobus> ListarCFOPSaidas()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CFOPGlobus> _lista = new List<CFOPGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodClassFisc, DescClassFisc");
                query.Append("  from EsfClass");
                query.Append(" Where CodClassFisc > 4999");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CFOPGlobus _cfop = new CFOPGlobus();

                        _cfop.Existe = true;
                        _cfop.CFOP = Convert.ToInt32(dataReader["CodClassFisc"].ToString());
                        _cfop.Descricao = dataReader["DescClassFisc"].ToString();

                        _lista.Add(_cfop);
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

        public CFOPGlobus ConsultaCFOP(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CFOPGlobus _cfop = new CFOPGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodClassFisc, DescClassFisc");
                query.Append("  from EsfClass");
                query.Append(" Where CodClassFisc = " + Codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _cfop.Existe = true;
                        _cfop.CFOP = Convert.ToInt32(dataReader["CodClassFisc"].ToString());
                        _cfop.Descricao = dataReader["DescClassFisc"].ToString();
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
            return _cfop;
        }

        public List<CSTGlobus> ListarCST()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CSTGlobus> _lista = new List<CSTGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodSitTributaria, DescSitTributaria");
                query.Append("  from Trr_SituacaoTributaria");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CSTGlobus _cfop = new CSTGlobus();

                        _cfop.Existe = true;
                        _cfop.Codigo = Convert.ToInt32(dataReader["CodSitTributaria"].ToString());
                        _cfop.Descricao = dataReader["DescSitTributaria"].ToString();

                        _lista.Add(_cfop);
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

        public CSTGlobus ConsultaCST(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CSTGlobus _cfop = new CSTGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodSitTributaria, DescSitTributaria");
                query.Append("  from Trr_SituacaoTributaria");
                query.Append(" Where CodSitTributaria = " + Codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _cfop.Existe = true;
                        _cfop.Codigo = Convert.ToInt32(dataReader["CodSitTributaria"].ToString());
                        _cfop.Descricao = dataReader["DescSitTributaria"].ToString();
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

            return _cfop;
        }

        public List<OperacaoGlobus> ListarOperacao()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<OperacaoGlobus> _lista = new List<OperacaoGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodOperFiscal, DescOperFiscal");
                query.Append("  from EsfOpFis");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        OperacaoGlobus _cfop = new OperacaoGlobus();

                        _cfop.Existe = true;
                        _cfop.Codigo = Convert.ToInt32(dataReader["CodOperFiscal"].ToString());
                        _cfop.Descricao = dataReader["DescOperFiscal"].ToString();

                        _lista.Add(_cfop);
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

        public OperacaoGlobus ConsultaOperacao(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            OperacaoGlobus _cfop = new OperacaoGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodOperFiscal, DescOperFiscal");
                query.Append("  from EsfOpFis");
                query.Append(" Where CodOperFiscal = " + Codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _cfop.Existe = true;
                        _cfop.Codigo = Convert.ToInt32(dataReader["CodOperFiscal"].ToString());
                        _cfop.Descricao = dataReader["DescOperFiscal"].ToString();
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

            return _cfop;
        }


        public List<LeiGlobus> ListarLeisGlobus()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<LeiGlobus> _lista = new List<LeiGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select l.*");
                query.Append("  From est_cadleisvigentes L");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        LeiGlobus _cfop = new LeiGlobus();
                        _cfop.Existe = true;
                        _cfop.Codigo = Convert.ToInt32(dataReader["CODLEISVIG"].ToString());
                        _cfop.Descricao = dataReader["DESCLEISVIG"].ToString();

                        _lista.Add(_cfop);
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

        public LeiGlobus ConsultarLeisGlobus(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            LeiGlobus _cfop = new LeiGlobus();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select l.*");
                query.Append("  From est_cadleisvigentes L");
                query.Append(" where CODLEISVIG = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _cfop.Existe = true;
                        _cfop.Codigo = Convert.ToInt32(dataReader["CODLEISVIG"].ToString());
                        _cfop.Descricao = dataReader["DESCLEISVIG"].ToString();

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

            return _cfop;
        }

    }
}
