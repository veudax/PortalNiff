using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CompetenciasDoCargosDAO
    {
        IDataReader dataReader;

        public List<CompetenciasDoCargo> Listar(int cargo, string tipo, bool avaliacao, int colaborador, string referencia, Publicas.TipoPrazos tipoAvaliacao)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CompetenciasDoCargo> _lista = new List<CompetenciasDoCargo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            { 
                if (avaliacao) // Só traz as competencias utilizadas na avaliacao da referencia
                {
                    query.Append("Select 'S' Marcado, Descricao, a.IdCompetencia, 0 IdCargo, Tipo, textoexplicativo");
                    query.Append("  From Niff_Ads_Competencias C,");
                    query.Append("     ( Select Distinct s.Idcompetencia");
                    query.Append("         From niff_ads_avaliacao a");
                    query.Append("            , Niff_Ads_Itensavaliacao i");
                    query.Append("            , Niff_Ads_Subcompetencias s");
                    query.Append("        Where a.Idautoavaliacao = i.Idautoavaliacao");
                    query.Append("          And i.Idsubcomp = s.Idsubcomp");
                    query.Append("          And a.Idcolaborador = " + colaborador);

                    if (tipoAvaliacao == Publicas.TipoPrazos.AutoAvaliacao)
                        query.Append("           And a.Tipo = 'AA'");
                    if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoRH)
                        query.Append("           And a.Tipo = 'AR'");
                    if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoDoGestor)
                        query.Append("           And a.Tipo = 'AG'");
                    query.Append("          And a.mesreferencia = " + referencia + " ) A");
                    query.Append(" Where C.Idcompetencia = A.Idcompetencia ");
                }
                else
                {
                    query.Append("Select 'S' Marcado, c.Descricao, c.Idcompetencia, cc.Idcargo, c.tipo, c.textoexplicativo");
                    query.Append("  From niff_ads_competenciasdocargo CC");
                    query.Append("     , niff_ads_competencias C");
                    query.Append("     , (Select Distinct c1.idcompetencia");
                    query.Append("          From Niff_Ads_Subcompetencias s1");
                    query.Append("             , niff_ads_competencias c1");
                    query.Append("         Where s1.idcompetencia = c1.idcompetencia");
                    if (tipoAvaliacao == Publicas.TipoPrazos.AutoAvaliacao)
                        query.Append("           And s1.exibenaautoavaliacao = 'S'");
                    if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoRH)
                        query.Append("           And s1.exibenaavaliacaorh = 'S'");
                    if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoDoGestor)
                        query.Append("           And s1.exibenaavaliacaogestor = 'S'");
                    query.Append(") s");
                    query.Append(" Where cc.idcompetencia = c.idcompetencia");
                    query.Append("   And s.idcompetencia = c.idcompetencia");
                    query.Append("   And cc.Idcargo = " + cargo);
                    query.Append("   And c.tipo = '" + tipo + "'");
                    query.Append("   And c.ativo = 'S'");
                    query.Append(" Union All ");
                    query.Append("Select 'N' Marcado, c.Descricao, c.Idcompetencia, Null IdCargo, c.tipo, c.textoexplicativo");
                    query.Append("  From niff_ads_competencias C");
                    query.Append(" Where C.Idcompetencia Not In (Select IdCompetencia ");
                    query.Append("                                 From Niff_Ads_Competenciasdocargo cc ");
                    query.Append("                                Where cc.Idcargo = " + cargo + ")");
                    query.Append("   And c.tipo = '" + tipo + "'");
                    query.Append("   And c.ativo = 'S'");
                }
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CompetenciasDoCargo _tipo = new CompetenciasDoCargo();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Idcompetencia"].ToString());
                        _tipo.TextoExplicativo = dataReader["textoexplicativo"].ToString();

                        try
                        {
                            _tipo.IdCargo = Convert.ToInt32(dataReader["Idcargo"].ToString());
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
        
        public bool Gravar(List<CompetenciasDoCargo> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();
                    query.Append("Select idassoc from niff_ads_competenciasdocargo");
                    query.Append(" Where  idcompetencia = " + item.Id);

                    Query executar = sessao.CreateQuery(query.ToString());
                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (!dataReader.Read())
                        {
                            query.Clear();
                            query.Append("Insert into niff_ads_competenciasdocargo");
                            query.Append(" ( idassoc, idcompetencia, idcargo)");
                            query.Append(" Values ( SQ_NIFF_ADSIdCompCarg.NextVal ");
                            query.Append("        , " + item.Id);
                            query.Append("        ," + item.IdCargo);
                            query.Append(" )");

                            retorno = sessao.ExecuteSqlTransaction(query.ToString());

                            if (!retorno)
                                break;
                        }
                    }                        
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

        public bool Excluir(int cargo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ads_competenciasdocargo");
                query.Append(" Where IdCargo = " + cargo);
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
