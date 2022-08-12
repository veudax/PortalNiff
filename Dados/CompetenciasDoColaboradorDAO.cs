using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CompetenciasDoColaboradorDAO
    {
        IDataReader dataReader;

        public List<CompetenciasDoColaborador> Listar(int cargo, int IdColaborador, string tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CompetenciasDoColaborador> _lista = new List<CompetenciasDoColaborador>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select 'S' Marcado, p.idcomppessoa, cc.Idassoc, p.IdColaborador, cc.idcompetencia, c.descricao, c.ativo, c.tipo");
                query.Append("  From Niff_ADS_CompetenciasDaPessoa p");
                query.Append("     , Niff_Ads_Competenciasdocargo cc");
                query.Append("     , niff_ads_competencias C");
                query.Append(" Where c.Idcompetencia = cc.Idcompetencia");
                query.Append("   And cc.Idassoc = p.Idassoc");
                //query.Append("   And cc.Idcargo = " + cargo);
                query.Append("   And c.tipo = '" + tipo + "'");
                query.Append("   And c.ativo = 'S'");
                query.Append("   And p.idcolaborador = " + IdColaborador);
                query.Append(" Union All ");
                query.Append("Select 'N' Marcado, Null, cc.Idassoc, Null IdColaborador, cc.idcompetencia, c.descricao, c.ativo, c.tipo");
                query.Append("  From niff_ads_competencias C");
                query.Append("     , Niff_Ads_Competenciasdocargo cc");
                query.Append(" Where cc.Idcompetencia Not In (Select c.Idcompetencia");
                query.Append("                            From Niff_ADS_CompetenciasDaPessoa p ");
                query.Append("                               , niff_ads_competenciasdocargo c ");
                query.Append("                           Where p.IdColaborador = " + IdColaborador );
                query.Append("                             and p.idassoc = c.idassoc )");
                query.Append("   And cc.Idcompetencia = c.Idcompetencia");
                query.Append("   And c.tipo = '" + tipo + "'");
                query.Append("   And cc.Idcargo = " + cargo);
                query.Append("   And c.ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CompetenciasDoColaborador _tipo = new CompetenciasDoColaborador();

                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["idcomppessoa"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdCompetenciasDoCargo = Convert.ToInt32(dataReader["Idassoc"].ToString());
                        }
                        catch { }

                        try  
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        }
                        catch { }

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Marcado = dataReader["Marcado"].ToString() == "S";

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

        public bool Gravar(List<CompetenciasDoColaborador> _lista)
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

                    query.Append("Insert into Niff_ADS_CompetenciasDaPessoa");
                    query.Append(" ( IdCompPessoa, IdColaborador, idassoc)");
                    query.Append(" Values ( SQ_NIFF_ADSIdCompPessoa.NextVal ");
                    query.Append("        , " + item.IdColaborador);
                    query.Append("        ," + item.IdCompetenciasDoCargo);
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
                query.Append("Delete Niff_ADS_CompetenciasDaPessoa");
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
