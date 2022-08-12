using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ControleDeHorasDAO
    {
        IDataReader dataReader;

        public List<ControleDeHoras> Listar(int idUsuario, DateTime inicio, DateTime fim, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ControleDeHoras> _lista = new List<ControleDeHoras>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select h.idhorario, h.Idusuario, h.Idcolaborador, h.data, h.entrada, h.saidaalmoco, h.retornoalmoco, h.saida ");
                query.Append("     , extra, Incompleta, Atestado, Declaracao, Compensacao, Ausencia, Motivo");
                query.Append("  from Niff_Pto_Horario h ");
                query.Append(" Where h.idcolaborador = " +  idUsuario);
                //query.Append(" Where h.Idusuario = 87" );
                query.Append("   and h.Data Between To_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and " +
                                                   "To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ControleDeHoras _tipo = new ControleDeHoras();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idhorario"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["Idusuario"].ToString());
                        _tipo.IdColaborador = Convert.ToInt32(dataReader["Idcolaborador"].ToString());

                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());

                        FeriadoEmenda _feriado = new FeriadoDAO().Consulta(_tipo.Data, idEmpresa);

                        _tipo.DataExtensa = (_feriado.Existe && _feriado.Tipo == "F" ? "Feriado, " : Publicas.DiaDaSemana(_tipo.Data.DayOfWeek) + ", ") + _tipo.Data.ToShortDateString();

                        _tipo.Entrada = dataReader["Entrada"].ToString();
                        _tipo.Saida = dataReader["Saida"].ToString();
                        _tipo.VoltaAlmoco = dataReader["retornoalmoco"].ToString();
                        _tipo.SaidaAlmoco = dataReader["saidaalmoco"].ToString();
                        _tipo.Atestado = dataReader["Atestado"].ToString() == "S";
                        _tipo.Declaracao = dataReader["Declaracao"].ToString() == "S";
                        _tipo.Compensacao = dataReader["Compensacao"].ToString() == "S";
                        _tipo.Ausencia = dataReader["Ausencia"].ToString() == "S";
                        _tipo.Motivo = dataReader["Motivo"].ToString();

                        try
                        {
                            _tipo.Extra = Convert.ToDouble(dataReader["Extra"].ToString());
                            if (_tipo.Extra > 0)
                                _tipo.ExtraFormatada = DateTime.MinValue.AddMinutes(_tipo.Extra).ToShortTimeString();
                        }
                        catch { }

                        try
                        {
                            _tipo.Incompletas = Convert.ToDouble(dataReader["Incompleta"].ToString());
                            if (_tipo.Incompletas > 0)
                                _tipo.IncompletaFormatada = DateTime.MinValue.AddMinutes(_tipo.Incompletas).ToShortTimeString();
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

        public List<ControleDeHoras> Listar(DateTime inicio, DateTime fim, int idGerente)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ControleDeHoras> _lista = new List<ControleDeHoras>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select u.nome ");
                query.Append("     , '" + inicio.ToShortDateString() + " a " + fim.ToShortDateString() + "' periodo");
                query.Append("     , Case When Sum(extra) > Sum(Incompleta) Then Sum(extra) - Sum(Incompleta) Else Sum(Incompleta) - Sum(extra) End Minutos");
                query.Append("     , Case When Sum(extra) > Sum(Incompleta) Then 'Extras' Else 'Incompletas' End Tipo");
                query.Append("  from niff_pto_horario h, niff_chm_usuarios u ");
                query.Append(" Where u.Idusuario = h.Idusuario");
                query.Append("   and h.Data Between To_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and " +
                                                   "To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");

                query.Append("   and h.idcolaborador = " + idGerente);
                query.Append(" Group By u.nome");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ControleDeHoras _tipo = new ControleDeHoras();

                        _tipo.Existe = true;
                        string[] nome = dataReader["Nome"].ToString().Split(' ');
                        _tipo.Nome = nome[0] + " " + nome[nome.Length - 1];
                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Extra = Convert.ToDouble(dataReader["Minutos"].ToString());
                        _tipo.IncompletaFormatada = dataReader["Periodo"].ToString();

                        if (DateTime.MinValue.AddMinutes(_tipo.Extra).Day == 1)
                            _tipo.ExtraFormatada = DateTime.MinValue.AddMinutes(_tipo.Extra).ToShortTimeString();
                        else
                            _tipo.ExtraFormatada = ((24 * (DateTime.MinValue.AddMinutes(_tipo.Extra).Day - 1)) + DateTime.MinValue.AddMinutes(_tipo.Extra).Hour).ToString() + ":" + DateTime.MinValue.AddMinutes(_tipo.Extra).Minute.ToString("00");
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

        public bool Gravar(List<ControleDeHoras> horas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                foreach (var tipo in horas)
                {
                    query.Clear();
                    if (!tipo.Existe)
                    {
                        query.Append("Insert into Niff_Pto_Horario");
                        query.Append(" (  idhorario, Idusuario, Idcolaborador, data, entrada, saidaalmoco, retornoalmoco, saida ");
                        query.Append("     , extra, Incompleta, Atestado, Declaracao, Compensacao, Ausencia, Motivo )");
                        query.Append(" Values ( (Select Nvl(Max(idhorario),0) +1 next From Niff_Pto_Horario )");
                        query.Append("        , " + Publicas._usuario.Id);
                        query.Append("        , " + Publicas._idColaborador);
                        query.Append("        , To_date('" + tipo.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("        , '" + tipo.Entrada + "'");
                        query.Append("        , '" + tipo.SaidaAlmoco + "'");
                        query.Append("        , '" + tipo.VoltaAlmoco + "'");
                        query.Append("        , '" + tipo.Saida + "'");
                        query.Append("        , " + tipo.Extra);
                        query.Append("        , " + tipo.Incompletas);
                        query.Append("        , '" + (tipo.Atestado ? "S" : "N") + "'");
                        query.Append("        , '" + (tipo.Declaracao ? "S" : "N") + "'");
                        query.Append("        , '" + (tipo.Compensacao ? "S" : "N") + "'");
                        query.Append("        , '" + (tipo.Ausencia ? "S" : "N") + "'");
                        query.Append("        , '" + tipo.Motivo + "'");
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update Niff_Pto_Horario");
                        query.Append("   set entrada = '" + tipo.Entrada + "'");
                        query.Append("     , saidaAlmoco = '" + tipo.SaidaAlmoco + "'");
                        query.Append("     , RetornoAlmoco = '" + tipo.VoltaAlmoco + "'");
                        query.Append("     , Saida = '" + tipo.Saida + "'");
                        query.Append("     , Extra = " + tipo.Extra);
                        query.Append("     , Incompleta = " + tipo.Incompletas);
                        query.Append("     , Atestado = '" + (tipo.Atestado ? "S" : "N") + "'");
                        query.Append("     , Declaracao = '" + (tipo.Declaracao ? "S" : "N") + "'");
                        query.Append("     , Compensacao = '" + (tipo.Compensacao ? "S" : "N") + "'");
                        query.Append("     , Ausencia = '" + (tipo.Ausencia ? "S" : "N") + "'");
                        query.Append("     , Motivo = '" + tipo.Motivo + "'");
                        query.Append(" Where idhorario = " + tipo.Id);
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

        public bool Excluir(ControleDeHoras tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Pto_Horario");
                query.Append(" Where idhorario = " + tipo.Id);
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
