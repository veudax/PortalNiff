using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ParcelasDoComunicadoDAO
    {
        IDataReader comunicadoReader;

        public List<ParcelasDoComunicado> Listar(int idComunicado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<ParcelasDoComunicado> _lista = new List<ParcelasDoComunicado>();
            int i = 1;
            try
            {
                query.Append("Select idparcela, idcomunicado, parcela, valor, data, TipoVencimento");
                query.Append("  from Niff_Jur_Parcela");
                query.Append(" Where idcomunicado = " + idComunicado);
                query.Append(" Order by data");

                Query executar = sessao.CreateQuery(query.ToString());

                comunicadoReader = executar.ExecuteQuery();

                using (comunicadoReader)
                {
                    while (comunicadoReader.Read())
                    {
                        ParcelasDoComunicado _parcela = new ParcelasDoComunicado();

                        try
                        {
                            _parcela.Id = Convert.ToInt32(comunicadoReader["idparcela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _parcela.IdComunicado = Convert.ToInt32(comunicadoReader["idcomunicado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _parcela.Data = Convert.ToDateTime(comunicadoReader["data"].ToString());
                        }
                        catch { }

                        _parcela.Parcela = i;

                        _parcela.Valor = Convert.ToDecimal(comunicadoReader["Valor"].ToString());

                        _parcela.Tipo = (comunicadoReader["TipoVencimento"].ToString() == "I" ? Publicas.TipoVencimento.Importado :
                                        (comunicadoReader["TipoVencimento"].ToString() == "O" ? Publicas.TipoVencimento.Original :
                                        (comunicadoReader["TipoVencimento"].ToString() == "A" ? Publicas.TipoVencimento.Antecipada : Publicas.TipoVencimento.Postergada)));
                        i++;
                        _lista.Add(_parcela);
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

        public bool Gravar(List<ParcelasDoComunicado> _listaParcelas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (ParcelasDoComunicado item in _listaParcelas)
                {

                    query.Clear();
                    query.Append("Insert into Niff_Jur_Parcela");
                    query.Append(" (idparcela, idcomunicado, parcela, valor, data, tipovencimento)");
                    query.Append(" Values( SQ_NIFF_ParIdn.NextVal " );
                    query.Append("      , " + item.IdComunicado );
                    query.Append("      , " + item.Parcela);
                    query.Append("      , " + item.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("      , To_Date('" + item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi:ss')");
                    query.Append(", '" + (item.Tipo == Publicas.TipoVencimento.Antecipada ? "A" :
                                         (item.Tipo == Publicas.TipoVencimento.Importado ? "I" :
                                         (item.Tipo == Publicas.TipoVencimento.Postergada ? "P" : "O"))) + "'");

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

        public bool Excluir(int IdComunicado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {

                query.Append("Delete Niff_Jur_Parcela");
                query.Append(" Where idComunicado = " + IdComunicado);
                retorno = sessao.ExecuteSqlTransaction(query.ToString());

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
