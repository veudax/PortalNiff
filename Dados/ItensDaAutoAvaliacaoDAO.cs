using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ItensDaAutoAvaliacaoDAO
    {
        IDataReader dataReader;


        public bool SubCompetenciaEmUso(int IdSub)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensDaAutoAvaliacao> _lista = new List<ItensDaAutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                query.Append("Select IdSubComp from Niff_Ads_Itensavaliacao Where IdSubComp = " + IdSub);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        retorno = true;
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
            return retorno;
        }


        public List<ItensDaAutoAvaliacao> Listar(int idCargo, int idColaborador, string referencia, Publicas.TipoPrazos tipoAvaliacao)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensDaAutoAvaliacao> _lista = new List<ItensDaAutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select ia.IdSubComp, s.Idcompetencia, s.descricao, ia.avaliacao pontuacao, s.ativo, ia.Comentario");
                query.Append("     , decode(ia.avaliacao, 1, p.naoatende, 2, p.Atendeparc, 3, p.Atendeplen, p.supera) Peso");
                query.Append("  From Niff_Ads_ItensAvaliacao ia, Niff_ADS_Avaliacao a, Niff_Ads_Colaboradores cl, NIFF_ADS_SubCompetencias s, Niff_ads_Pontuacao P");
                query.Append(" Where s.Idsubcomp = ia.Idsubcomp ");
                query.Append("   And ia.idautoavaliacao = a.Idautoavaliacao");
                query.Append("   And cl.idcolaborador = a.Idcolaborador");
                query.Append("   And cl.idcolaborador = " + idColaborador);
                query.Append("   And a.mesReferencia = " + referencia);
                query.Append("   And p.mesreferencia = a.mesreferencia");

                if (tipoAvaliacao == Publicas.TipoPrazos.AutoAvaliacao)
                {
                    query.Append("   and s.ExibeNaAutoAvaliacao = 'S'");
                    query.Append("   and a.tipo = 'AA'");
                }
                if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoRH)
                {
                    query.Append("   and s.ExibeNaAvaliacaoRH = 'S'");
                    query.Append("   and a.tipo = 'AR'");
                }
                if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoDoGestor)
                {
                    query.Append("   and s.ExibeNaAvaliacaoGestor = 'S'");
                    query.Append("   and a.tipo = 'AG'");
                }

                query.Append(" Union All ");
                query.Append("Select s.IdSubComp, s.idcompetencia, s.descricao,  0 pontuacao, s.ativo, null comentario, 0 Peso");
                query.Append("  from NIFF_ADS_SubCompetencias s, Niff_Ads_Competenciasdocargo cc, Niff_Ads_Competencias c");
                query.Append(" Where cc.idcargo = " + idCargo);
                query.Append("   And c.ativo = 'S'");
                query.Append("   And c.tipo = 'C'");

                if (tipoAvaliacao == Publicas.TipoPrazos.AutoAvaliacao)
                    query.Append("   and s.ExibeNaAutoAvaliacao = 'S'");
                if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoRH)
                    query.Append("   and s.ExibeNaAvaliacaoRH = 'S'");
                if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoDoGestor)
                    query.Append("   and s.ExibeNaAvaliacaoGestor = 'S'");

                query.Append("   and c.Idcompetencia = s.idcompetencia");
                query.Append("   And cc.idcompetencia = c.idcompetencia");
                query.Append("   And s.IdSubComp Not In (Select IdSubComp ");
                query.Append("                             From Niff_Ads_ItensAvaliacao ia, Niff_ADS_Avaliacao a, Niff_Ads_Colaboradores cl");
                query.Append("                            Where a.Idautoavaliacao = ia.idautoavaliacao");
                query.Append("                              And a.idcolaborador = cl.idcolaborador");
                query.Append("                              And a.mesReferencia = " + referencia);
                query.Append("                              And Cl.Idcolaborador = " + idColaborador);
                if (tipoAvaliacao == Publicas.TipoPrazos.AutoAvaliacao)
                    query.Append("                          And a.tipo = 'AA'");
                if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoRH)
                    query.Append("                          And a.tipo = 'AR'");
                if (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoDoGestor)
                    query.Append("                          And a.tipo = 'AG'");
                query.Append("                              And cl.Idcargo = " + idCargo + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensDaAutoAvaliacao _tipo = new ItensDaAutoAvaliacao();

                        _tipo.Existe = true;
                        _tipo.IdSubCompetencia = Convert.ToInt32(dataReader["IdSubComp"].ToString());
                        _tipo.IdCompetencia = Convert.ToInt32(dataReader["Idcompetencia"].ToString());
                        _tipo.Descricao = dataReader["descricao"].ToString();
                        _tipo.Avaliacao = Convert.ToInt32(dataReader["pontuacao"].ToString());
                        _tipo.Pontuacao = Convert.ToInt32(dataReader["peso"].ToString());
                        _tipo.Comentario = dataReader["Comentario"].ToString();

                        _tipo.NaoAtende = _tipo.Avaliacao == 1;
                        _tipo.AtendeParcialmente = _tipo.Avaliacao == 2;
                        _tipo.AtendePlenamente = _tipo.Avaliacao == 3;
                        _tipo.Supera = _tipo.Avaliacao == 4;
                        
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
       
        public bool Gravar(List<ItensDaAutoAvaliacao> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();

                    query.Append("Insert into Niff_Ads_ItensAvaliacao");
                    query.Append(" ( iditensauto, idautoavaliacao, idsubcomp, avaliacao, Comentario, Pontuacao)");
                    query.Append(" Values ( SQ_NIFF_AdsIdItensAval.NextVal");
                    query.Append("        , " + item.IdAutoAvaliacao );
                    query.Append("        , " + item.IdSubCompetencia );
                    query.Append("        , " + item.Avaliacao);
                    query.Append("        , '" + item.Comentario + "' ");
                    query.Append("        , " + item.Pontuacao);
                    query.Append("        )"); 
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

        public bool Excluir(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ads_ItensAvaliacao");
                query.Append(" Where idautoavaliacao = " + codigo);
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
