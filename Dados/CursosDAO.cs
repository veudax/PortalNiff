using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CursosDAO
    {
        IDataReader dataReader;

        public List<Cursos> Listar(int IdColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Cursos> _lista = new List<Cursos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idcurso, idcolaborador, descricao, valor, duracao, inicio, fim, obrigatorio, opniao");
                query.Append("  From NIFF_Ads_Cursos C");
                query.Append(" Where c.IdColaborador = " + IdColaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Cursos _tipo = new Cursos();

                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idcurso"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        }
                        catch { }

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Duracao = Convert.ToInt32(dataReader["Duracao"].ToString());

                        _tipo.Inicio = Convert.ToDateTime(dataReader["Inicio"].ToString());

                        try
                        {
                            _tipo.Fim = Convert.ToDateTime(dataReader["Fim"].ToString());
                        }
                        catch { }

                        _tipo.Obrigatorio = dataReader["obrigatorio"].ToString() == "S";
                        _tipo.Opniao = dataReader["Opniao"].ToString();

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

        public bool Gravar(List<Cursos> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = _lista.Count() == 0;
            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();

                    query.Append("Insert into NIFF_Ads_Cursos");
                    query.Append(" ( idcurso, idcolaborador, descricao, valor, duracao, inicio, fim, obrigatorio, opniao )");
                    query.Append(" Values ( SQ_NIFF_ADSIdCurso.NextVal ");
                    query.Append("        , " + item.IdColaborador);
                    query.Append("        , '" + item.Descricao + "'");
                    query.Append("        , " + item.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        ," + item.Duracao);
                    query.Append("        , To_date('" + item.Inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                    if (item.Fim == DateTime.MinValue)
                        query.Append("        , null");
                    else
                        query.Append("        , To_date('" + item.Fim.ToShortDateString() + "', 'dd/mm/yyyy')");

                    query.Append("        , '" + (item.Obrigatorio? "S" : "N") + "'");
                    query.Append("        , '" + item.Opniao + "'");
                    query.Append(" )");

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

        public bool Excluir(int IdColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete NIFF_Ads_Cursos");
                query.Append(" Where IdColaborador = " + IdColaborador);
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
