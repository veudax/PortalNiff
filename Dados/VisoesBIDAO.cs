using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class VisoesBIDAO
    {
        IDataReader dataReader;

        public List<VisoesBI> Listar(bool ApenasOsAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<VisoesBI> _lista = new List<VisoesBI>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, descricao, ativo, especificodeumaconta");
                query.Append("  from Niff_Ads_BI");

                if (ApenasOsAtivos) 
                    query.Append(" Where Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        VisoesBI _tipo = new VisoesBI();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.EspeficoDeUmaConta = dataReader["especificodeumaconta"].ToString() == "S";

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

        public VisoesBI Consultar(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            VisoesBI _tipo = new VisoesBI();

            try
            {
                query.Append("Select id, descricao, ativo, especificodeumaconta");
                query.Append("  from Niff_Ads_BI");
                query.Append(" Where Id = " + id);

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.EspeficoDeUmaConta = dataReader["especificodeumaconta"].ToString() == "S";
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

        public bool Grava(VisoesBI times, List<DetalheVisoes> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                if (!times.Existe)
                {

                    query.Clear();
                    query.Append("Insert into Niff_Ads_BI");
                    query.Append("   (id, descricao, ativo, especificodeumaconta");

                    query.Append("  ) Values ( " + times.Id);
                    query.Append(", '" + times.Descricao + "'");
                    query.Append(", '" + (times.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + (times.EspeficoDeUmaConta ? "S" : "N") + "'");
                    
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Ads_BI");
                    query.Append("   set Descricao = '" + times.Descricao + "'");
                    query.Append("     , Ativo = '" + (times.Ativo ? "S" : "N") + "'");
                    query.Append("     , especificodeumaconta = '" + (times.EspeficoDeUmaConta ? "S" : "N") + "'");
                    query.Append(" Where Id = " + times.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    _lista.ForEach(u => u.Id = times.Id);

                    retorno = GravaDetalhes(_lista);
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

        public bool Exclui(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!ExcluiDetalhe(id))
                    return false;
                else
                {
                    query.Append("Delete Niff_Ads_BI");
                    query.Append(" Where id = " + id);

                    return sessao.ExecuteSqlTransaction(query.ToString());
                }
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

        public bool GravaDetalhes(List<DetalheVisoes> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var times in _lista)
                {
                    query.Clear();

                    if (!times.Existe)
                    {

                        query.Append("Insert into Niff_Ads_DetalheBI");
                        query.Append("   (id, IdBI, descricao");

                        query.Append("  ) Values ( (Select nvl(Max(Id),0)+1 From Niff_Ads_DetalheBI ) ");
                        query.Append(", " + times.Id);    
                        query.Append(", '" + times.Descricao + "'");
                        query.Append(") ");
                    }
                    else
                    {
                        if (times.Excluir)
                        {
                            query.Append("Delete Niff_Ads_DetalheBI");
                            query.Append(" Where id = " + times.IdDetalhe);
                        }
                        else
                        {
                            query.Clear();
                            query.Append("Update Niff_Ads_DetalheBI");
                            query.Append("   set Descricao = '" + times.Descricao + "'");
                            query.Append(" Where Id = " + times.IdDetalhe);
                        }
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

        public bool ExcluiDetalhe(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ads_DetalheBI");
                query.Append(" Where idBI = " + id);

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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int id = 1;

            query.Clear();
            query.Append("(Select nvl(Max(Id),0)+1 next From Niff_Ads_BI )");
            Query executar = sessao.CreateQuery(query.ToString());

            dataReader = executar.ExecuteQuery();

            using (dataReader)
            {
                if (dataReader.Read())
                    id = Convert.ToInt32(dataReader["next"].ToString());
            }

            return id;
        }

        public List<DetalheVisoes> Listar(int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DetalheVisoes> _lista = new List<DetalheVisoes>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, IdBI, Descricao");
                query.Append("  from Niff_Ads_DetalheBI");
                query.Append(" Where IdBI = " + Id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        DetalheVisoes _tipo = new DetalheVisoes();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdBI"].ToString());
                        _tipo.IdDetalhe = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();

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


    }
}
