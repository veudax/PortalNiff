using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class SubCompetenciasDAO
    {
        IDataReader dadosReader;

        public List<SubCompetencias> Listar(bool apenasAtivos, int idCompetencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<SubCompetencias> _lista = new List<SubCompetencias>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select IdSubComp, idcompetencia, descricao, pontuacao, ativo, ExibeNaAutoAvaliacao, ExibeNaAvaliacaoRH, ExibeNaAvaliacaoGestor");
                query.Append("  from NIFF_ADS_SubCompetencias");
                query.Append(" Where idcompetencia = " + idCompetencia);
                if (apenasAtivos)
                    query.Append("   and ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        SubCompetencias _tipo = new SubCompetencias();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dadosReader["IdSubComp"].ToString());
                        _tipo.IdCompetencia = Convert.ToInt32(dadosReader["idcompetencia"].ToString());
                        _tipo.Pontuacao = Convert.ToDecimal(dadosReader["pontuacao"].ToString());
                        _tipo.Descricao = dadosReader["Descricao"].ToString();
                        _tipo.Ativo = dadosReader["Ativo"].ToString() == "S";

                        _tipo.ExibeNaAutoAvaliacao = dadosReader["ExibeNaAutoAvaliacao"].ToString() == "S";
                        _tipo.ExibeNaAvaliacaoRH = dadosReader["ExibeNaAvaliacaoRH"].ToString() == "S";
                        _tipo.ExibeNaAvaliacaoGestor = dadosReader["ExibeNaAvaliacaoGestor"].ToString() == "S";

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
        
        public bool Gravar(List<SubCompetencias> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            if (_lista.Count == 0)
                return true;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();

                    if (item.Existe)
                    {
                        query.Append("Update NIFF_ADS_SubCompetencias");
                        query.Append("   set descricao = '" + item.Descricao + "'");
                        query.Append("     , pontuacao = " + item.Pontuacao.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , ativo = '" + (item.Ativo ? "S" : "N") + "'");
                        query.Append("     , ExibeNaAutoAvaliacao = '" + (item.ExibeNaAutoAvaliacao ? "S" : "N") + "'");
                        query.Append("     , ExibeNaAvaliacaoRH = '" + (item.ExibeNaAvaliacaoRH ? "S" : "N") + "'");
                        query.Append("     , ExibeNaAvaliacaoGestor = '" + (item.ExibeNaAvaliacaoGestor ? "S" : "N") + "'");
                        query.Append(" Where IdSubComp = " + item.Id);
                    }
                    else
                    {
                        query.Append("Insert into NIFF_ADS_SubCompetencias");
                        query.Append(" ( IdSubComp, idcompetencia, descricao, pontuacao, ativo, ExibeNaAutoAvaliacao, ExibeNaAvaliacaoRH, ExibeNaAvaliacaoGestor )");
                        query.Append(" Values ( SQ_NIFF_ADSIdSubComp.NextVal");
                        query.Append("        , " + item.IdCompetencia);
                        query.Append("        , '" + item.Descricao + "'");
                        query.Append("        , " + item.Pontuacao.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , '" + (item.Ativo ? "S" : "N") + "'");
                        query.Append("        , '" + (item.ExibeNaAutoAvaliacao ? "S" : "N") + "'");
                        query.Append("        , '" + (item.ExibeNaAvaliacaoRH ? "S" : "N") + "'");
                        query.Append("        , '" + (item.ExibeNaAvaliacaoGestor ? "S" : "N") + "'");
                        query.Append(" )");
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

        public bool Excluir(int idCompetencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete NIFF_ADS_SubCompetencias");
                query.Append(" Where idCompetencia = " + idCompetencia);
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
