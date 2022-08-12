using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class AutoAvaliacaoDAO
    {
        IDataReader dataReader;

        public List<AutoAvaliacao> Listar(int mesReferencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AutoAvaliacao> _lista = new List<AutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select A.idautoavaliacao, A.idcolaborador, A.mesreferencia, A.datainicio, A.datafim, f.Nomefunc, A.Tipo");
                query.Append("     , a.idUsuario, a.IdUsuarioAlteracao, a.dataAlteracao, a.FEEDBACKGESTOR, a.COMENTARIO");
                query.Append("  from Niff_ADS_Avaliacao a, Niff_Ads_Colaboradores c, Flp_Funcionarios f     ");
                query.Append(" Where a.MesReferencia = " + mesReferencia);
                query.Append("   And c.Idcolaborador = a.Idcolaborador");
                query.Append("   And c.codintfunc = f.codintfunc");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        AutoAvaliacao _tipo = new AutoAvaliacao();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idautoavaliacao"].ToString());
                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        }
                        catch { }

                        
                        _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        try
                        {
                            _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuario = Convert.ToInt32(dataReader["idUsuario"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuarioAlteracao = Convert.ToInt32(dataReader["IdUsuarioAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        _tipo.Colaborador = dataReader["NomeFunc"].ToString();
                        _tipo.Comentario = dataReader["Comentario"].ToString();
                        _tipo.FeedbackGestor = dataReader["FeedbackGestor"].ToString();
                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                    (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                    (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                    (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                    (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                    (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado : Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

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

        public List<AutoAvaliacao> Listar(string tipo, int idColaborador, string inicio, string fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AutoAvaliacao> _lista = new List<AutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;

            int refIni = Convert.ToInt32(inicio.PadLeft(6,'0').Substring(2, 4) + inicio.PadLeft(6, '0').Substring(0, 2));
            int refFim = Convert.ToInt32(fim.PadLeft(6, '0').Substring(2, 4) + fim.PadLeft(6, '0').Substring(0, 2));

            try
            {
                query.Append("Select A.idautoavaliacao, A.idcolaborador, A.mesreferencia, A.datainicio, A.datafim, f.Nomefunc, A.Tipo, a.Notaavaliacao");
                query.Append("     , a.idUsuario, a.IdUsuarioAlteracao, a.dataAlteracao, a.FEEDBACKGESTOR, a.COMENTARIO, a.IdEmpresa, e.Nomeabreviado");
                query.Append("     , substr(Lpad(a.mesreferencia, 6, '0'), 3, 4) || substr(Lpad(a.mesreferencia, 6, '0'), 1, 2) ordem");
                query.Append("  from Niff_ADS_Avaliacao a, Niff_Ads_Colaboradores c, Flp_Funcionarios f, Niff_Chm_Empresas e     ");
                query.Append(" Where c.Idcolaborador = a.Idcolaborador");
                query.Append("   And c.codintfunc = f.codintfunc");
                query.Append("   And e.Idempresa = a.Idempresa");
                query.Append("   And c.IdColaborador = " + idColaborador);
                query.Append("   And a.tipo = '" + tipo + "'");
                query.Append("  And substr(Lpad(a.mesreferencia, 6, '0'), 3, 4) || substr(Lpad(a.mesreferencia, 6, '0'), 1, 2) between " + refIni + " and " + refFim);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        AutoAvaliacao _tipo = new AutoAvaliacao();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idautoavaliacao"].ToString());

                        _tipo.Empresa = dataReader["Nomeabreviado"].ToString();
                        _tipo.TotalAvaliacao = Convert.ToDecimal(dataReader["NotaAvaliacao"].ToString());

                        try
                        {
                            _tipo.Ordem = Convert.ToInt32(dataReader["Ordem"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        try
                        {
                            _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuario = Convert.ToInt32(dataReader["idUsuario"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuarioAlteracao = Convert.ToInt32(dataReader["IdUsuarioAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        _tipo.Colaborador = dataReader["NomeFunc"].ToString();
                        _tipo.Comentario = dataReader["Comentario"].ToString();
                        _tipo.FeedbackGestor = dataReader["FeedbackGestor"].ToString();
                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                    (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                    (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                    (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                    (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                    (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado : Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

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

        public List<AutoAvaliacao> Listar(int idColaborador, string radar, int ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AutoAvaliacao> _lista = new List<AutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("select a.mesreferencia, substr(a.mesreferencia,3,4) ano, a.tipo ");
                query.Append("     , Round(Nvl(Sum(decode(ia.pontuacao, Null");
                query.Append("          , ia.avaliacao, 1, p.naoatende, 2, p.atendeparc, 3, p.atendeplen, p.supera");
                query.Append("          , ia.pontuacao) / q.qtd),0),2) pontuacao");
                query.Append("          , c.Idcompetencia, c.descricao");
                query.Append("  from Niff_ADS_Avaliacao a, Niff_Ads_Itensavaliacao Ia");
                query.Append("     , Niff_Ads_Subcompetencias s, Niff_Ads_Competencias c,niff_ads_pontuacao p");
                query.Append("     , (Select t.tipo, Count(s.idcompetencia) qtd, c.Idcompetencia, c.descricao, t.mesreferencia ");
                query.Append("          from niff_ads_avaliacao t, Niff_Ads_Itensavaliacao Ia");
                query.Append("             , Niff_Ads_Subcompetencias s, Niff_Ads_Competencias c");
                query.Append("         Where t.tipo In ('AA'" + (radar == "Todos" ? ", 'AR','AG'" : "") + ")");
                query.Append("           And t.Idcolaborador = " + idColaborador);
                query.Append("           And t.idautoavaliacao = ia.idautoavaliacao");
                query.Append("           And s.idsubcomp = ia.idsubcomp");
                query.Append("           And c.Idcompetencia = s.Idcompetencia");
                query.Append("         Group By t.tipo, c.Idcompetencia, t.mesreferencia, c.descricao) q");
                query.Append(" Where a.tipo In ('AA'" + (radar == "Todos" ? ", 'AR','AG'" : "") + ")");
                query.Append("   And a.Idcolaborador = " + idColaborador);
                query.Append("   And a.idautoavaliacao = ia.idautoavaliacao");
                query.Append("   And s.idsubcomp = ia.idsubcomp");
                query.Append("   And c.Idcompetencia = s.Idcompetencia");
                query.Append("   And a.tipo = q.tipo");
                query.Append("   And c.Idcompetencia = q.Idcompetencia");
                query.Append("   And p.mesreferencia = a.mesreferencia");
                query.Append("   And a.mesreferencia = q.mesreferencia");

                if (ano != 0)
                    query.Append("   And substr(a.mesreferencia, 3, 4) = " + ano);
                query.Append(" Group By a.tipo, a.mesreferencia, c.Idcompetencia, c.descricao");

                if (radar == "Media")
                {
                    query.Append(" Union all ");
                    query.Append("Select a.mesreferencia, substr(a.mesreferencia,3,4) ano, 'ME' tipo ");
                    query.Append("     , Round(Nvl(Sum((decode(ia.pontuacao, Null");
                    query.Append("          , ia.avaliacao, 1, p.naoatende, 2, p.atendeparc, 3, p.atendeplen, p.supera");
                    query.Append("          , ia.pontuacao) / q.qtd) / Qtdtipo),0),2) pontuacao");
                    query.Append("          , c.Idcompetencia, c.descricao");
                    query.Append("  from Niff_ADS_Avaliacao a, Niff_Ads_Itensavaliacao Ia");
                    query.Append("     , Niff_Ads_Subcompetencias s, Niff_Ads_Competencias c,niff_ads_pontuacao p");
                    query.Append("     , (Select t.tipo, Count(s.idcompetencia) qtd, c.Idcompetencia, c.descricao, t.mesreferencia ");
                    query.Append("          from niff_ads_avaliacao t, Niff_Ads_Itensavaliacao Ia");
                    query.Append("             , Niff_Ads_Subcompetencias s, Niff_Ads_Competencias c");
                    query.Append("         Where t.tipo In ('AR', 'AG')");
                    query.Append("           And t.Idcolaborador = " + idColaborador);
                    query.Append("           And t.idautoavaliacao = ia.idautoavaliacao");
                    query.Append("           And s.idsubcomp = ia.idsubcomp");
                    query.Append("           And c.Idcompetencia = s.Idcompetencia");
                    query.Append("         Group By t.tipo, c.Idcompetencia, t.mesreferencia, c.descricao) q");
                    query.Append("     , (Select Count(t.Tipo) Qtdtipo, t.Mesreferencia");
                    query.Append("          From Niff_Ads_Avaliacao t");
                    query.Append("         Where t.Tipo In ('AR', 'AG')");
                    query.Append("           And t.Idcolaborador = " + idColaborador);
                    query.Append("         Group By t.Mesreferencia) x");

                    query.Append(" Where a.tipo In ( 'AR', 'AG' )");
                    query.Append("   And a.Idcolaborador = " + idColaborador);
                    query.Append("   And a.idautoavaliacao = ia.idautoavaliacao");
                    query.Append("   And s.idsubcomp = ia.idsubcomp");
                    query.Append("   And c.Idcompetencia = s.Idcompetencia");
                    query.Append("   And a.tipo = q.tipo");
                    query.Append("   And c.Idcompetencia = q.Idcompetencia");
                    query.Append("   And p.mesreferencia = a.mesreferencia");
                    query.Append("   And a.mesreferencia = q.mesreferencia");
                    query.Append("   And a.mesreferencia = x.mesreferencia");
                    if (ano != 0)
                        query.Append("   And substr(a.mesreferencia, 3, 4) = " + ano);

                    query.Append(" Group By a.mesreferencia, c.Idcompetencia, c.descricao");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        AutoAvaliacao _tipo = new AutoAvaliacao();

                        _tipo.Existe = true;
                        
                        _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());
                                                
                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                    (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                    (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                    (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                    (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                    (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado :
                                    (dataReader["Tipo"].ToString() == "PD" ? Publicas.TipoPrazos.PlanoDeDesenvolvimento : Publicas.TipoPrazos.Media)))))));

                        _tipo.TotalAvaliacao = Convert.ToDecimal(dataReader["pontuacao"].ToString());
                        _tipo.Comentario = dataReader["descricao"].ToString(); // usando para não criar uma nova propriedade.
                        _tipo.Ano = Convert.ToInt32(dataReader["Ano"].ToString());
                        _tipo.ReferenciaFormatada = Convert.ToInt32(dataReader["mesreferencia"].ToString()).ToString("00/0000");
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

        public List<Colaboradores> Listar(int idColaborador, int idCargo, string referencia, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Colaboradores> _lista = new List<Colaboradores>();
            Publicas.mensagemDeErro = string.Empty;
            // usado para as metas numericas para outros colaboradores

            try
            {
                query.Append("Select Distinct  c.Idcolaborador, c.idempresa, e.NomeAbreviado Empresa, f.Nomefunc, f.codfunc");
                query.Append("  From niff_ads_colaboradores c, niff_chm_empresas e, flp_funcionarios f");
                query.Append(" Where f.codintfunc = c.codintfunc");
                query.Append("   And e.Idempresa = c.idempresa");
                //query.Append("   And c.Idcargo = " + idCargo); // por enquanto traz todos
                query.Append("   And c.Idcolaborador <> " + idColaborador);
                query.Append("   And c.Participaavaliacao = 'S'");
                query.Append("   And c.Idcolaborador Not In(Select a.Idcolaborador From niff_ads_avaliacao a Where a.mesreferencia = " + referencia + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                DateTime _dataReferencia = Convert.ToDateTime("01/" + referencia.PadLeft(6, '0').Substring(0, 2) + "/" + referencia.PadLeft(6, '0').Substring(2, 4)).AddMonths(1).AddDays(-1);
                List<EmpresaQueOColaboradorEhAvaliado> _empresas = new List<EmpresaQueOColaboradorEhAvaliado>();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Colaboradores _colaborador = new Colaboradores();

                        _colaborador.Id = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _colaborador.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _colaborador.Nome = dataReader["NomeFunc"].ToString();
                        _colaborador.Codigo = dataReader["CodFunc"].ToString();
                        _colaborador.Empresa = dataReader["Empresa"].ToString();

                        _lista.Add(_colaborador);

                        _empresas = new EmpresaQueOColaboradorEhAvaliadoDAO().Listar(_colaborador.Id);

                        foreach (var item in _empresas.Where(w => (w.Inicio != DateTime.MinValue && w.Inicio <= _dataReferencia) &&
                                                                  (w.Fim == DateTime.MinValue || w.Fim >= _dataReferencia) &&
                                                                   w.IdEmpresa != _colaborador.IdEmpresa && w.IdEmpresa != idEmpresa))
                        {
                            _colaborador = new Colaboradores();
                            Empresa _empresa = new EmpresaDAO().ConsultaEmpresa(item.IdEmpresa);

                            _colaborador.Id = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                            _colaborador.IdEmpresa = item.IdEmpresa;
                            _colaborador.Nome = dataReader["NomeFunc"].ToString();
                            _colaborador.Codigo = dataReader["CodFunc"].ToString();
                            _colaborador.Empresa = _empresa.NomeAbreviado;

                            _lista.Add(_colaborador);
                        }
                    }

                     _empresas = new EmpresaQueOColaboradorEhAvaliadoDAO().Listar(idColaborador);

                    foreach (var item in _empresas.Where(w => (w.Inicio != DateTime.MinValue && w.Inicio <= _dataReferencia) &&
                                                              (w.Fim == DateTime.MinValue || w.Fim >= _dataReferencia) &&
                                                               w.IdEmpresa != idEmpresa))
                    {
                        Colaboradores _colaboradorOR = new ColaboradoresDAO().ConsultaColaborador(idColaborador);

                        Colaboradores _colaborador = new Colaboradores();
                        Empresa _empresa = new EmpresaDAO().ConsultaEmpresa(item.IdEmpresa);

                        _colaborador.Id = idColaborador;
                        _colaborador.IdEmpresa = item.IdEmpresa;
                        _colaborador.Nome = _colaboradorOR.Nome;
                        _colaborador.Empresa = _empresa.NomeAbreviado;

                        _lista.Add(_colaborador);
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

        public List<AutoAvaliacao> Listar(int mesReferencia, int mesReferenciaFim, bool incluirQualitativas, bool incluirNumericas, string tipoConsulta)
        {
            // usado tela de notas. 

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AutoAvaliacao> _lista = new List<AutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;
            int refIni = Convert.ToInt32(mesReferencia.ToString("000000").Substring(2,4) + mesReferencia.ToString("000000").Substring(0, 2));
            int refFim = Convert.ToInt32(mesReferenciaFim.ToString("000000").Substring(2, 4) + mesReferenciaFim.ToString("000000").Substring(0, 2));

            try
            {
                query.Append("Select A.idcolaborador, A.mesreferencia, f.Nomefunc, A.Tipo, g.descricao cargo");
                query.Append("     , e.Nomeabreviado");
                query.Append("     , Round(Sum(a.Notaavaliacao) / Count(c.idEmpresa), 2) Notaavaliacao");
                query.Append("     , c.idEmpresa");
                query.Append("     , substr(Lpad(a.mesreferencia, 6, '0'), 3, 4) || substr(Lpad(a.mesreferencia, 6, '0'), 1, 2) ordem");
                query.Append("  from Niff_ADS_Avaliacao a, Niff_Ads_Colaboradores c, Flp_Funcionarios f, niff_chm_empresas e, niff_ads_cargos g     ");

                query.Append(" Where substr(Lpad(a.mesreferencia, 6, '0'), 3, 4) || substr(Lpad(a.mesreferencia, 6, '0'), 1, 2) between " + refIni + " and " + refFim);
                if (incluirNumericas && incluirQualitativas)
                    query.Append("     And a.Tipo in ('AA','AR','AG','MN')");
                else
                {
                    if (incluirNumericas)
                        query.Append("     And a.Tipo ='MN'");
                    else
                        query.Append("     And a.Tipo in ('AA','AR','AG')");
                }

                if (tipoConsulta.Equals("Colaborador"))
                    query.Append("   And c.Idcolaborador = " + Publicas._idColaborador);

                if (tipoConsulta.Equals("Gestor"))
                    query.Append("   And (c.Idcolaborador = " + Publicas._idColaborador + " or c.Idsuperior = " + Publicas._idColaborador + ")" );

                query.Append("   And c.Idcolaborador = a.Idcolaborador");
                query.Append("   And c.codintfunc = f.codintfunc");
                query.Append("   And e.Idempresa = c.Idempresa");
                query.Append("   And g.Idcargo = c.Idcargo");
                query.Append(" Group by a.Idcolaborador, a.Mesreferencia, f.Nomefunc, a.Tipo, g.Descricao, e.Nomeabreviado, Substr(Lpad(a.Mesreferencia, 6, '0'), 3, 4) || Substr(Lpad(a.Mesreferencia, 6, '0'), 1, 2)");
                query.Append("        , c.idEmpresa");

                 Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        AutoAvaliacao _tipo = new AutoAvaliacao();

                        _tipo.Existe = true;
                        
                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());

                        try
                        {
                            _tipo.Ordem = Convert.ToInt32(dataReader["Ordem"].ToString());
                        }
                        catch { }
                        
                        _tipo.Colaborador = dataReader["NomeFunc"].ToString();
                        _tipo.Empresa = dataReader["NomeAbreviado"].ToString();
                        _tipo.TotalAvaliacao = Convert.ToDecimal(dataReader["NotaAvaliacao"].ToString());
                        _tipo.Cargo = dataReader["Cargo"].ToString();

                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                    (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                    (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                    (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                    (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                    (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado : Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

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

        public List<AutoAvaliacao> ListarDetalhe(int mesReferencia, int mesReferenciaFim, bool incluirQualitativas, bool incluirNumericas)
        {
            // usado tela de notas. 

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AutoAvaliacao> _lista = new List<AutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;
            int refIni = Convert.ToInt32(mesReferencia.ToString("000000").Substring(2, 4) + mesReferencia.ToString("000000").Substring(0, 2));
            int refFim = Convert.ToInt32(mesReferenciaFim.ToString("000000").Substring(2, 4) + mesReferenciaFim.ToString("000000").Substring(0, 2));

            try
            {
                query.Append("Select A.idcolaborador, A.mesreferencia, f.Nomefunc, A.Tipo, g.descricao cargo");
                query.Append("     , e.Nomeabreviado");
                query.Append("     , Round(Sum(a.Notaavaliacao) / Count(a.idEmpresa), 2) Notaavaliacao");
                query.Append("     , a.idEmpresa");
                query.Append("     , substr(Lpad(a.mesreferencia, 6, '0'), 3, 4) || substr(Lpad(a.mesreferencia, 6, '0'), 1, 2) ordem");
                query.Append("  from Niff_ADS_Avaliacao a, Niff_Ads_Colaboradores c, Flp_Funcionarios f, niff_chm_empresas e, niff_ads_cargos g     ");

                query.Append(" Where substr(Lpad(a.mesreferencia, 6, '0'), 3, 4) || substr(Lpad(a.mesreferencia, 6, '0'), 1, 2) between " + refIni + " and " + refFim);
                if (incluirNumericas && incluirQualitativas)
                    query.Append("     And a.Tipo in ('AA','AR','AG','MN')");
                else
                {
                    if (incluirNumericas)
                        query.Append("     And a.Tipo ='MN'");
                    else
                        query.Append("     And a.Tipo in ('AA','AR','AG')");
                }
                query.Append("   And c.Idcolaborador = a.Idcolaborador");
                query.Append("   And c.codintfunc = f.codintfunc");
                query.Append("   And e.Idempresa = a.Idempresa");
                query.Append("   And g.Idcargo = c.Idcargo");
                query.Append(" Group by a.Idcolaborador, a.Mesreferencia, f.Nomefunc, a.Tipo, g.Descricao, e.Nomeabreviado, Substr(Lpad(a.Mesreferencia, 6, '0'), 3, 4) || Substr(Lpad(a.Mesreferencia, 6, '0'), 1, 2)");
                query.Append("        , a.idEmpresa");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        AutoAvaliacao _tipo = new AutoAvaliacao();

                        _tipo.Existe = true;

                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());

                        try
                        {
                            _tipo.Ordem = Convert.ToInt32(dataReader["Ordem"].ToString());
                        }
                        catch { }

                        _tipo.Colaborador = dataReader["NomeFunc"].ToString();
                        _tipo.Empresa = dataReader["NomeAbreviado"].ToString();
                        _tipo.TotalAvaliacao = Convert.ToDecimal(dataReader["NotaAvaliacao"].ToString());
                        _tipo.Cargo = dataReader["Cargo"].ToString();

                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                    (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                    (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                    (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                    (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                    (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado : Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

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

        public List<AutoAvaliacao> ListarAndamento(int mesReferencia, string tipoCargo, string tipo, int idSuperior)
        { // usado no dashboard da ela princial e botão status da tela prazos
            
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AutoAvaliacao> _lista = new List<AutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Sum(TotalIniciado) TotalIniciado, Sum(totalConcluido) totalConcluido, Sum(TotalNaoIniciado) TotalNaoIniciado, Nomefunc, email");
                query.Append("     , Empresa, Cargo, Idempresa");
                query.Append("  From ( Select Count(*) totalIniciado, 0 totalConcluido, 0 TotalNaoIniciado, f.Nomefunc, u.email");
                query.Append("              , e.codigoglobus || '-' || e.Nomeabreviado Empresa, g.descricao cargo, e.Idempresa");
                query.Append("           From Niff_Ads_Colaboradores c, niff_Ads_Avaliacao a, Niff_Ads_Cargos g, flp_funcionarios f, niff_chm_usuarios u");
                query.Append("              , Niff_Chm_Empresas e");
                query.Append("          Where c.idcolaborador = a.Idcolaborador ");
                query.Append("            And a.datafim Is Null ");
                query.Append("            And c.idcargo = g.idcargo ");
                query.Append("            And c.codintfunc = f.codintfunc ");
                query.Append("            And c.ParticipaAvaliacao = 'S'");

                if (idSuperior != 0)
                    query.Append("            And c.Idsuperior = " + idSuperior);
                
                query.Append("            And a.tipo = '" + tipo + "'");
                query.Append("            And a.mesreferencia = " + mesReferencia);
                query.Append("            And u.codfunc = f.codintfunc");
                query.Append("            And u.acessaavaliacaodesempenho = 'S'");
                query.Append("            And e.Idempresa = a.Idempresa");
                query.Append("          Group By F.Nomefunc, u.email, e.codigoglobus || '-' || e.Nomeabreviado, g.descricao, e.Idempresa ");

                query.Append("          Union All ");

                query.Append("         Select 0 totalIniciado, Count(*) totalConcluido, 0 TotalNaoIniciado, f.Nomefunc, u.email");
                query.Append("              , e.codigoglobus || '-' || e.Nomeabreviado Empresa, g.descricao cargo, e.Idempresa");
                query.Append("           From Niff_Ads_Colaboradores c, niff_Ads_Avaliacao a, Niff_Ads_Cargos g, flp_funcionarios f, niff_chm_usuarios u");
                query.Append("              , Niff_Chm_Empresas e");
                query.Append("          Where c.idcolaborador = a.Idcolaborador ");
                query.Append("            And a.datafim Is Not Null ");
                query.Append("            And c.idcargo = g.idcargo ");
                query.Append("            And c.codintfunc = f.codintfunc ");
                query.Append("            And c.ParticipaAvaliacao = 'S'");

                if (idSuperior != 0)
                    query.Append("            And c.Idsuperior = " + idSuperior);

                if (tipo == "FG" || tipo == "FA")
                { 
                    query.Append("            And a.tipo = 'AA'");
                    if (tipo == "FG")
                        query.Append("            And a.FeedBackGestor is not null");
                    else
                    {
                        if (tipo == "FA")
                            query.Append("            And a.Comentario is not null");
                    }
                }
                else
                    query.Append("            And a.tipo = '" + tipo + "'");

                query.Append("            And a.mesreferencia = " + mesReferencia);
                query.Append("            And u.codfunc = f.codintfunc");
                query.Append("            And u.acessaavaliacaodesempenho = 'S'");
                query.Append("            And e.Idempresa = a.Idempresa");
                query.Append("          Group By F.Nomefunc, u.email, e.codigoglobus || '-' || e.Nomeabreviado, g.descricao, e.Idempresa ");

                query.Append("          Union All ");

                query.Append("         Select 0 totalIniciado, 0 totalConcluido, Count(*) TotalNaoIniciado, f.Nomefunc, u.email");
                query.Append("              , e.codigoglobus || '-' || e.Nomeabreviado Empresa, g.descricao cargo, e.Idempresa");
                query.Append("           From Niff_Ads_Colaboradores c, Niff_Ads_Cargos g, flp_funcionarios f, niff_chm_usuarios u");
                query.Append("              , Niff_Ads_Empresascolavalia ce, Niff_Chm_Empresas e");
                query.Append("          Where c.idcargo = g.idcargo ");
                query.Append("            And c.codintfunc = f.codintfunc ");
                query.Append("            And c.ParticipaAvaliacao = 'S'");

                query.Append("            And c.Idcolaborador = ce.Idcolaborador");
                query.Append("            And ce.inicio <> '01-jan-0001'");
                query.Append("            And To_date(To_Char(ce.inicio,'ddmmyyyy'),'ddmmyyyy') <= To_Date('01" + mesReferencia.ToString().PadLeft(6,'0') + "', 'ddmmyyyy')");
                query.Append("            And(ce.fim = '01-jan-0001' Or To_date(To_Char(ce.fim, 'ddmmyyyy'), 'ddmmyyyy') > To_Date('01" + mesReferencia.ToString().PadLeft(6, '0') + "', 'ddmmyyyy'))");
                
                if (idSuperior != 0)
                    query.Append("            And c.Idsuperior = " + idSuperior);

                query.Append("            And (c.Idcolaborador, e.Idempresa) Not In (Select a.idcolaborador, a.idEmpresa ");
                query.Append("                                          From Niff_Ads_Avaliacao a ");
                query.Append("                                         Where a.mesreferencia = " + mesReferencia);

                if (tipo == "FG" || tipo == "FA")
                {
                    if (tipo == "FG")
                        query.Append("                                           And a.feedbackgestor Is Not Null");
                    else
                        query.Append("                                           And a.comentario Is Not Null");
                    query.Append("                                           And a.tipo = 'AA')");
                }
                else
                    query.Append("                                           And a.tipo = '" + tipo + "')");

                query.Append("            And u.codfunc = f.codintfunc");
                query.Append("            And u.acessaavaliacaodesempenho = 'S'");
                query.Append("            And e.Idempresa = ce.Idempresa");
                query.Append("          Group By F.Nomefunc, u.email, e.codigoglobus || '-' || e.Nomeabreviado, g.descricao, e.Idempresa ");
                query.Append(" )");
                query.Append(" Group By Nomefunc, email ");
                query.Append("     , Empresa, Cargo, Idempresa");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        AutoAvaliacao _tipo = new AutoAvaliacao();

                        _tipo.Existe = true;
                        
                        _tipo.MesReferencia = mesReferencia;
                        
                        _tipo.Cargo = dataReader["Cargo"].ToString();
                        _tipo.Empresa = dataReader["Empresa"].ToString();
                        _tipo.Colaborador = dataReader["NomeFunc"].ToString();
                        _tipo.Email = dataReader["Email"].ToString();
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());

                        _tipo.Status = (dataReader["totalIniciado"].ToString() != "0" ? "I" :
                                       (dataReader["totalConcluido"].ToString() != "0" ? "F" : "N"));

                        _tipo.Tipo = (tipo == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                     (tipo == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                     (tipo == "MR" ? Publicas.TipoPrazos.MetasNumericas :
                                     (tipo == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                     (tipo == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                     (tipo == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado : Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

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

        public List<AutoAvaliacao> ListarNotas(string tipo, int idSuperior)
        {
            // usado no dashboard da tela principal
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AutoAvaliacao> _lista = new List<AutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (idSuperior != 0)
                {
                    query.Append("         Select Round(Sum(a.Notaavaliacao) / Count(e.IdEmpresa),2) Notaavaliacao");
                    query.Append("              , a.mesreferencia, f.Nomefunc, a.Idcolaborador, u.email");
                    query.Append("              , e.codigoglobus || '-' || e.Nomeabreviado Empresa, g.descricao cargo, e.Idempresa, f.CodFunc");
                    query.Append("           From Niff_Ads_Colaboradores c, niff_Ads_Avaliacao a, Niff_Ads_Cargos g, flp_funcionarios f, niff_chm_usuarios u");
                    query.Append("              , Niff_Chm_Empresas e");
                    query.Append("          Where c.idcolaborador = a.Idcolaborador ");
                    //query.Append("            And a.datafim Is Not Null ");
                    query.Append("            And c.idcargo = g.idcargo ");
                    query.Append("            And c.codintfunc = f.codintfunc ");
                    query.Append("            And c.Idsuperior = " + idSuperior);
                    query.Append("            And a.tipo = '" + tipo + "'");
                    query.Append("            And c.ParticipaAvaliacao = 'S'");

                    query.Append("            And u.codfunc = f.codintfunc");
                    query.Append("            And u.Acessaavaliacaodesempenho = 'S'");
                    query.Append("            And e.Idempresa = a.Idempresa");
                    query.Append("          Group By F.Nomefunc, u.email, e.codigoglobus || '-' || e.Nomeabreviado, g.descricao, a.mesreferencia, f.CodFunc, a.Idcolaborador, e.Idempresa ");

                    query.Append("         Union all ");

                    query.Append("         Select Round(Sum(a.Notaavaliacao) / Count(e.IdEmpresa),2) Notaavaliacao");
                    query.Append("              , a.mesreferencia, f.Nomefunc, a.Idcolaborador, u.email");
                    query.Append("              , e.codigoglobus || '-' || e.Nomeabreviado Empresa, g.descricao cargo, e.Idempresa, f.CodFunc");
                    query.Append("           From Niff_Ads_Colaboradores c, niff_Ads_Avaliacao a, Niff_Ads_Cargos g, flp_funcionarios f, niff_chm_usuarios u");
                    query.Append("              , Niff_Chm_Empresas e");
                    query.Append("          Where c.idcolaborador = a.Idcolaborador ");
                    //query.Append("            And a.datafim Is Not Null ");
                    query.Append("            And c.idcargo = g.idcargo ");
                    query.Append("            And c.codintfunc = f.codintfunc ");
                    query.Append("            And c.idcolaborador = " + idSuperior);
                    query.Append("            And a.tipo = '" + tipo + "'");
                    query.Append("            And c.ParticipaAvaliacao = 'S'");

                    query.Append("            And u.codfunc = f.codintfunc");
                    query.Append("            And u.Acessaavaliacaodesempenho = 'S'");
                    query.Append("            And e.Idempresa = a.Idempresa");
                    query.Append("          Group By F.Nomefunc, u.email, e.codigoglobus || '-' || e.Nomeabreviado, g.descricao, a.mesreferencia, f.CodFunc, a.Idcolaborador, e.Idempresa ");
                }
                else
                {
                    query.Append("         Select Round(Sum(a.Notaavaliacao) / Count(e.IdEmpresa),2) Notaavaliacao");
                    query.Append("              , a.mesreferencia, f.Nomefunc, a.Idcolaborador, u.email");
                    query.Append("              , e.codigoglobus || '-' || e.Nomeabreviado Empresa, g.descricao cargo, e.Idempresa, f.CodFunc");
                    query.Append("           From Niff_Ads_Colaboradores c, niff_Ads_Avaliacao a, Niff_Ads_Cargos g, flp_funcionarios f, niff_chm_usuarios u");
                    query.Append("              , Niff_Chm_Empresas e");
                    query.Append("          Where c.idcolaborador = a.Idcolaborador ");
                    //query.Append("            And a.datafim Is Not Null ");
                    query.Append("            And c.idcargo = g.idcargo ");
                    query.Append("            And c.codintfunc = f.codintfunc ");
                    query.Append("            And a.tipo = '" + tipo + "'");
                    query.Append("            And c.ParticipaAvaliacao = 'S'");

                    query.Append("            And u.codfunc = f.codintfunc");
                    query.Append("            And u.Acessaavaliacaodesempenho = 'S'");
                    query.Append("            And e.Idempresa = a.Idempresa");
                    query.Append("          Group By F.Nomefunc, u.email, e.codigoglobus || '-' || e.Nomeabreviado, g.descricao, a.mesreferencia, f.CodFunc, a.Idcolaborador, e.Idempresa ");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        AutoAvaliacao _tipo = new AutoAvaliacao();

                        _tipo.Existe = true;

                        //_tipo.MesReferencia = mesReferencia;

                        _tipo.Cargo = dataReader["Cargo"].ToString();
                        _tipo.Empresa = dataReader["Empresa"].ToString();
                        _tipo.Colaborador = dataReader["NomeFunc"].ToString();
                        _tipo.Email = dataReader["Email"].ToString();
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdColaborador = Convert.ToInt32(dataReader["Idcolaborador"].ToString());
                        _tipo.ReferenciaFormatada = Convert.ToInt32(dataReader["mesreferencia"].ToString()).ToString("00/0000");
                        _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());
                        _tipo.Registro = dataReader["CodFunc"].ToString();

                        _tipo.TotalAvaliacao = Convert.ToDecimal(dataReader["NotaAvaliacao"].ToString());

                        _tipo.Tipo = (tipo == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                     (tipo == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                     (tipo == "MR" ? Publicas.TipoPrazos.MetasNumericas :
                                     (tipo == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                     (tipo == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                     (tipo == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado : Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

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
        
        public AutoAvaliacao Consulta(int codigo, string mesReferencia, string tipo, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            AutoAvaliacao _tipo = new AutoAvaliacao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select A.idautoavaliacao, A.idcolaborador, A.mesreferencia, A.datainicio, A.datafim, f.Nomefunc, A.Tipo");
                query.Append("     , a.idUsuario, a.IdUsuarioAlteracao, a.dataAlteracao, a.FEEDBACKGESTOR, a.COMENTARIO, a.idEmpresa, A.NotaAvaliacao");
                query.Append("  from Niff_ADS_Avaliacao a, Niff_Ads_Colaboradores c, Flp_Funcionarios f     ");
                query.Append(" Where a.MesReferencia = " + mesReferencia);
                query.Append("   And c.Idcolaborador = " + codigo);
                query.Append("   And a.tipo = '" + tipo + "'");
                query.Append("   And a.idEmpresa = " + idEmpresa);
                query.Append("   And c.Idcolaborador = a.Idcolaborador");
                query.Append("   And c.codintfunc = f.codintfunc");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idautoavaliacao"].ToString());

                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        }
                        catch { }

                        _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());
                        _tipo.TotalAvaliacao = Convert.ToDecimal(dataReader["NotaAvaliacao"].ToString());
                        
                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        try
                        {
                            _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        }
                        catch { }

                        _tipo.Tipo = (tipo == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                                             (tipo == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                                             (tipo == "MR" ? Publicas.TipoPrazos.MetasNumericas :
                                                             (tipo == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                                             (tipo == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                                             (tipo == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado : Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

                        _tipo.Colaborador = dataReader["NomeFunc"].ToString();
                        _tipo.Comentario = dataReader["Comentario"].ToString();
                        _tipo.FeedbackGestor = dataReader["FeedbackGestor"].ToString();
                        try
                        {
                            _tipo.IdUsuario = Convert.ToInt32(dataReader["idUsuario"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuarioAlteracao = Convert.ToInt32(dataReader["IdUsuarioAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
                        }
                        catch { }
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
        
        public List<AutoAvaliacao> MaiorReferencia(int mesReferencia, int mesReferenciaFim)
        {
            // usado tela de notas. 
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AutoAvaliacao> _lista = new List<AutoAvaliacao>();
            Publicas.mensagemDeErro = string.Empty;
            int refIni = Convert.ToInt32(mesReferencia.ToString("000000").Substring(2, 4) + mesReferencia.ToString("000000").Substring(0, 2));
            int refFim = Convert.ToInt32(mesReferenciaFim.ToString("000000").Substring(2, 4) + mesReferenciaFim.ToString("000000").Substring(0, 2));

            try
            {
                query.Append("Select Max(A.mesreferencia) mesreferencia, A.Tipo");
                query.Append("  from Niff_ADS_Avaliacao a, Niff_Ads_Colaboradores c, Flp_Funcionarios f, niff_chm_empresas e, niff_ads_cargos g     ");

                query.Append(" Where substr(Lpad(a.mesreferencia, 6, '0'), 3, 4) || substr(Lpad(a.mesreferencia, 6, '0'), 1, 2) between " + refIni + " and " + refFim);
                query.Append("     And a.Tipo in ('AA','AR','AG','MN')");
                query.Append("   And c.Idcolaborador = a.Idcolaborador");
                query.Append("   And c.codintfunc = f.codintfunc");
                query.Append("   And e.Idempresa = c.Idempresa");
                query.Append("   And g.Idcargo = c.Idcargo");
                query.Append(" Group by a.Tipo");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        AutoAvaliacao _tipo = new AutoAvaliacao();

                        _tipo.Existe = true;

                        _tipo.MesReferencia = Convert.ToInt32(dataReader["mesreferencia"].ToString());
                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "AA" ? Publicas.TipoPrazos.AutoAvaliacao :
                                    (dataReader["Tipo"].ToString() == "FG" ? Publicas.TipoPrazos.FeedbackGestor :
                                    (dataReader["Tipo"].ToString() == "MN" ? Publicas.TipoPrazos.MetasNumericas :
                                    (dataReader["Tipo"].ToString() == "AG" ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                    (dataReader["Tipo"].ToString() == "AR" ? Publicas.TipoPrazos.AvaliacaoRH :
                                    (dataReader["Tipo"].ToString() == "FA" ? Publicas.TipoPrazos.FeedbackAvaliado : Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

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

        public bool Gravar(AutoAvaliacao tipo, List<ItensDaAutoAvaliacao> _lista, List<ItensAvaliacaoMetas> _metas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int Id = 0;
            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Select SQ_NIFF_AdsIdAvaliacao.NextVal next from dual");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            Id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into Niff_ADS_Avaliacao");
                    query.Append(" ( idautoavaliacao, idcolaborador, mesreferencia, datainicio, datafim, tipo, idUsuario, NotaAvaliacao, idEmpresa)");
                    query.Append(" Values ( " + Id);
                    query.Append("        , " + tipo.IdColaborador );
                    query.Append("        , " + tipo.MesReferencia );
                    query.Append("        , Sysdate");

                    if (tipo.DataFim == DateTime.MinValue)
                        query.Append("        , null");
                    else
                        query.Append("        , sysdate");

                    query.Append("        ,'" + (tipo.Tipo == Publicas.TipoPrazos.AutoAvaliacao ? "AA" :
                        (tipo.Tipo == Publicas.TipoPrazos.FeedbackGestor ? "FG" :
                        (tipo.Tipo == Publicas.TipoPrazos.MetasNumericas ? "MN" :
                        (tipo.Tipo == Publicas.TipoPrazos.AvaliacaoDoGestor ? "AG" :
                        (tipo.Tipo == Publicas.TipoPrazos.AvaliacaoRH ? "AR" :
                        (tipo.Tipo == Publicas.TipoPrazos.FeedbackAvaliado ? "FA" : "PD" )))))) + "'");
                    query.Append("        , " + Publicas._idUsuario);
                    query.Append("        , " + tipo.TotalAvaliacao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + tipo.IdEmpresa);
                    query.Append(" )");
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                }
                else
                {
                    Id = tipo.Id;

                    query.Append("Update Niff_ADS_Avaliacao");
                    query.Append("   set IdUsuarioAlteracao = " + Publicas._idUsuario);
                    query.Append("     , DataAlteracao = sysDate");
                    query.Append("     , idEmpresa = " + tipo.IdEmpresa);
                    query.Append("     , NotaAvaliacao = " + tipo.TotalAvaliacao.ToString().Replace(".", "").Replace(",", "."));
                    if (tipo.DataFim != DateTime.MinValue)
                        query.Append("     , datafim = sysdate");

                    query.Append(" Where idautoavaliacao = " + tipo.Id);
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                }
                
                if (retorno)
                {
                    if (_lista != null && _lista.Count != 0)
                    {
                        if (tipo.Existe)
                        {
                            if (!new ItensDaAutoAvaliacaoDAO().Excluir(tipo.Id))
                                retorno = false;
                        }

                        _lista.ForEach(u => u.IdAutoAvaliacao = Id);
                        if (!new ItensDaAutoAvaliacaoDAO().Gravar(_lista))
                            retorno = false;
                    }

                    if (_metas != null && _metas.Count != 0)
                    {
                        if (tipo.Existe)
                        {
                            if (!new ItensAvaliacaoMetasDAO().Excluir(tipo.Id))
                                retorno = false;
                        }

                        _metas.ForEach(u => u.IdAvaliacao = Id);
                        if (!new ItensAvaliacaoMetasDAO().Gravar(_metas))
                            retorno = false;
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

        public bool Excluir(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!new ItensAvaliacaoMetasDAO().Excluir(codigo))
                    return false;

                if (!new ItensDaAutoAvaliacaoDAO().Excluir(codigo))
                    return false;

                query.Append("Delete Niff_ADS_Avaliacao");
                query.Append(" Where IdAutoAvaliacao = " + codigo);
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

        public bool FeedBackGestor(int codigo, string texto)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                
                query.Append("Update Niff_ADS_Avaliacao");
                query.Append("   set FEEDBACKGESTOR = :ptexto");
                query.Append("     , DataFeedBack = sysdate");
                query.Append(" Where IdAutoAvaliacao = " + codigo);

                OracleParameter _parametro = new OracleParameter();
                _parametro.ParameterName = "pTexto";
                _parametro.Value = texto;

                List<OracleParameter> _lista = new List<OracleParameter>();
                _lista.Add(_parametro);

                return sessao.ExecuteSqlTransaction(query.ToString(), _lista.ToArray());
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

        public bool FeedBackColaborador(int codigo, string texto)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
               
                query.Append("Update Niff_ADS_Avaliacao");
                query.Append("   set Comentario = :ptexto");
                query.Append("     , DataComentario = sysdate");
                query.Append(" Where IdAutoAvaliacao = " + codigo);

                OracleParameter _parametro = new OracleParameter();
                _parametro.ParameterName = "pTexto";
                _parametro.Value = texto;

                List<OracleParameter> _lista = new List<OracleParameter>();
                _lista.Add(_parametro);

                return sessao.ExecuteSqlTransaction(query.ToString(), _lista.ToArray());

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
