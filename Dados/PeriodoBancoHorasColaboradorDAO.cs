using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class PeriodoBancoHorasColaboradorDAO
    {
        IDataReader dadosReader;

        public List<PeriodoBancoHorasColaborador> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PeriodoBancoHorasColaborador> _lista = new List<PeriodoBancoHorasColaborador>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select IdPeriodo, IdColaborador, ReferenciaInicio, ReferenciaFim, ativo");
                query.Append("  from Niff_Pto_ColabPeriodo");
                if (apenasAtivos)
                    query.Append(" Where ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        PeriodoBancoHorasColaborador _tipo = new PeriodoBancoHorasColaborador();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dadosReader["IdPeriodo"].ToString());
                        _tipo.IdColaborador = Convert.ToInt32(dadosReader["IdColaborador"].ToString());
                        _tipo.Ativo = dadosReader["Ativo"].ToString() == "S";
                        _tipo.ReferenciaInicial = Convert.ToInt32(dadosReader["ReferenciaInicio"].ToString());
                        _tipo.ReferenciaFinal = Convert.ToInt32(dadosReader["ReferenciaFim"].ToString());

                        _tipo.ReferenciaInicioFormatada = Convert.ToInt32(dadosReader["ReferenciaInicio"].ToString()).ToString("00/00");
                        _tipo.ReferenciaFimFormatada = Convert.ToInt32(dadosReader["ReferenciaFim"].ToString()).ToString("00/00");

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

        public List<PeriodoBancoHorasColaborador> Listar(int idColaborador, bool cadastro = false)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PeriodoBancoHorasColaborador> _lista = new List<PeriodoBancoHorasColaborador>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select * from ( ");
                query.Append("Select IdPeriodo, IdColaborador, ReferenciaInicio, ReferenciaFim, ativo");
                query.Append("     , subStr(referenciainicio, 1, 2) || '/' || subStr(referenciainicio, 3, 2) || '/' ||");
                query.Append("       Case When referenciainicio = 2112 Then To_char(To_date(To_char(Sysdate,'rrrr')-1,'yyyy'),'yyyy') Else To_char(Sysdate,'rrrr') End Inicio");
                query.Append("     , subStr(referenciafim, 1, 2) || '/' || subStr(referenciafim, 3, 2) || '/' ||");
                query.Append("       Case When referenciafim = 2001 Then To_char(To_date(To_char(Sysdate,'rrrr')+1,'yyyy'),'yyyy') Else To_char(Sysdate,'rrrr') End Fim");
                query.Append("  from Niff_Pto_ColabPeriodo");
                query.Append(" Where IdColaborador = " + idColaborador);
                query.Append(" ) ");

                if (!cadastro)
                    query.Append(" where To_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy') Between To_date(inicio,'dd/mm/yyyy') And to_date(fim,'dd/mm/yyyy')");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        PeriodoBancoHorasColaborador _tipo = new PeriodoBancoHorasColaborador();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dadosReader["IdPeriodo"].ToString());
                        _tipo.IdColaborador = Convert.ToInt32(dadosReader["IdColaborador"].ToString());
                        _tipo.Ativo = dadosReader["Ativo"].ToString() == "S";
                        _tipo.ReferenciaInicial = Convert.ToInt32(dadosReader["ReferenciaInicio"].ToString());
                        _tipo.ReferenciaFinal = Convert.ToInt32(dadosReader["ReferenciaFim"].ToString());

                        _tipo.ReferenciaInicioFormatada = Convert.ToInt32(dadosReader["ReferenciaInicio"].ToString()).ToString("00/00");
                        _tipo.ReferenciaFimFormatada = Convert.ToInt32(dadosReader["ReferenciaFim"].ToString()).ToString("00/00");

                        _tipo.Inicio = Convert.ToDateTime(dadosReader["Inicio"].ToString());
                        _tipo.Fim = Convert.ToDateTime(dadosReader["Fim"].ToString());
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

        public PeriodoBancoHorasColaborador Consulta(int codigo, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            PeriodoBancoHorasColaborador _tipo = new PeriodoBancoHorasColaborador();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select IdPeriodo, IdColaborador, ReferenciaInicio, ReferenciaFim, ativo");
                query.Append("  from Niff_Pto_ColabPeriodo");
                query.Append(" Where IdColaborador = " + codigo);
                query.Append("   And ReferenciaInicio = " + referencia);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dadosReader["IdPeriodo"].ToString());
                        _tipo.IdColaborador = Convert.ToInt32(dadosReader["IdColaborador"].ToString());
                        _tipo.Ativo = dadosReader["Ativo"].ToString() == "S";
                        _tipo.ReferenciaInicial = Convert.ToInt32(dadosReader["ReferenciaInicio"].ToString());
                        _tipo.ReferenciaFinal = Convert.ToInt32(dadosReader["ReferenciaFim"].ToString());

                        _tipo.ReferenciaInicioFormatada = Convert.ToInt32(dadosReader["ReferenciaInicio"].ToString()).ToString("00/00");
                        _tipo.ReferenciaFimFormatada = Convert.ToInt32(dadosReader["ReferenciaFim"].ToString()).ToString("00/00");

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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Nvl(Max(IdPeriodo),0) +1 next From Niff_Pto_ColabPeriodo");
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                        retorno = Convert.ToInt32(dadosReader["next"].ToString());
                }
                return retorno;
            }
            catch
            {
                return retorno;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool Gravar(PeriodoBancoHorasColaborador tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_Pto_ColabPeriodo");
                    query.Append(" ( IdPeriodo, IdColaborador, ReferenciaInicio, ReferenciaFim, ativo )");
                    query.Append(" Values (" + Proximo());
                    query.Append("        ," + tipo.IdColaborador);
                    query.Append("        ," + tipo.ReferenciaInicial);
                    query.Append("        ," + tipo.ReferenciaFinal);
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Pto_ColabPeriodo");
                    query.Append("   set ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , ReferenciaInicio = " + tipo.ReferenciaInicial);
                    query.Append("     , ReferenciaFim = " + tipo.ReferenciaFinal);
                    query.Append(" Where IdPeriodo = " + tipo.Id);
                }

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

        public bool Excluir(int idPeriodo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Pto_ColabPeriodo");
                query.Append(" Where IdPeriodo = " + idPeriodo);
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
