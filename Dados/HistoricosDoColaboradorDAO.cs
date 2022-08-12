using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class HistoricosDoColaboradorDAO
    {
        IDataReader dataReader;

        public List<HistoricosDoColaborador> Listar(int IdColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<HistoricosDoColaborador> _lista = new List<HistoricosDoColaborador>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select h.idhistorico, h.data, h.idcolaborador, h.idcargo, h.iddepartamento, h.idsuperior, h.salario, h.IdEscolaridade");
                query.Append("     , c.descricao cargo, d.descricao departamento, e.Descricao escolaridade");
                query.Append("  From Niff_Ads_HistoricoColaborador h");
                query.Append("     , Niff_Ads_Cargos c, Niff_Chm_Departamento d, niff_ads_escolaridade e");
                query.Append(" Where c.Idcargo(+) = h.idcargo");
                query.Append("   And d.Iddepartamento(+) = h.iddepartamento");
                query.Append("   And E.IdEscola(+) = h.IdEscolaridade");
                query.Append("   And h.IdColaborador = " + IdColaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        HistoricosDoColaborador _tipo = new HistoricosDoColaborador();

                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idhistorico"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdCargo = Convert.ToInt32(dataReader["idcargo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(dataReader["iddepartamento"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdSuperior = Convert.ToInt32(dataReader["idsuperior"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEscolaridade = Convert.ToInt32(dataReader["IdEscolaridade"].ToString());
                        }
                        catch { }

                        _tipo.Salario = Convert.ToDecimal(dataReader["salario"].ToString());
                        
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        _tipo.DescricaoCargo = dataReader["Cargo"].ToString();
                        _tipo.DescricaoDepartamento = dataReader["Departamento"].ToString();
                        _tipo.DescricaoEscolaridade = dataReader["Escolaridade"].ToString();


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

        public bool Gravar(HistoricosDoColaborador _historico)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            
            try
            {
                query.Clear();

                query.Append("Insert into Niff_Ads_HistoricoColaborador");
                query.Append(" ( idhistorico, data, idcolaborador, idcargo, iddepartamento, idsuperior, salario, IdEscolaridade)");
                query.Append(" Values ( SQ_NIFF_ADSIdHistorico.NextVal ");
                query.Append("        , sysdate ");
                query.Append("        , " + _historico.IdColaborador);
                if (_historico.IdCargo == 0)
                    query.Append("        , null");
                else
                    query.Append("        , " + _historico.IdCargo);

                if (_historico.IdDepartamento == 0)
                    query.Append("        , null");
                else
                    query.Append("        , " + _historico.IdDepartamento);

                if (_historico.IdSuperior == 0)
                    query.Append("        , null");
                else
                    query.Append("        , " + _historico.IdSuperior);

                query.Append("        , " + _historico.Salario.ToString().Replace(".", "").Replace(",", "."));

                if (_historico.IdEscolaridade == 0)
                    query.Append("        , null");
                else
                    query.Append("        , " + _historico.IdEscolaridade);

                query.Append(" )");

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

        public bool Excluir(int IdColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ads_HistoricoColaborador");
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
