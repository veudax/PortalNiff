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
    public class CurriculosDAO
    {
        IDataReader dataReader;

        #region Candidatos
        public List<HistoricoDoCandidato> ListaHistorico(int idCandidato, int idVaga)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<HistoricoDoCandidato> _lista = new List<HistoricoDoCandidato>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select h.id, h.IdCandidato, h.IdVaga, h.status, h.motivo, h.data, h.DataEntrevista, v.descricao");
                query.Append("  from niff_cur_historicocandidato h, niff_cur_vagas v");
                query.Append(" Where v.Id(+) = h.idvaga");

                if (idVaga != 0)
                    query.Append("   and h.IdVaga = " + idVaga);

                if (idCandidato != 0)
                    query.Append("   and h.idcandidato = " + idCandidato);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        HistoricoDoCandidato _tipo = new HistoricoDoCandidato();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());

                        try
                        {
                            _tipo.IdVaga = Convert.ToInt32(dataReader["idvaga"].ToString());
                            _tipo.DescricaoVaga = _tipo.IdVaga + " - " + dataReader["descricao"].ToString();
                        }
                        catch { }

                        _tipo.IdCandidato = Convert.ToInt32(dataReader["idCandidato"].ToString());

                        _tipo.Status = dataReader["status"].ToString();
                        _tipo.Motivo = dataReader["motivo"].ToString();

                        _tipo.DescricaoStatus = (_tipo.Status == "1" ? "Cadastrado" :
                            (_tipo.Status == "2" ? "Pré-Selecionado" :
                            (_tipo.Status == "3" ? "Contato" :
                            (_tipo.Status == "4" ? "Sem Contato" :
                            (_tipo.Status == "5" ? "Aprovado pelo Gestor" :
                            (_tipo.Status == "6" ? "Agendada 1ª Entrevista" :
                            (_tipo.Status == "7" ? "Agendada 2ª Entrevista" :
                            (_tipo.Status == "8" ? "Reagendamento 1ª Entrevista" :
                            (_tipo.Status == "9" ? "Reagendamento 2ª Entrevista" :
                            (_tipo.Status == "10" ? "Reprovado pelo Gestor" :
                            (_tipo.Status == "11" ? "" : // vago
                            (_tipo.Status == "12" ? "Cancelamento 1ª Entrevista" :
                            (_tipo.Status == "13" ? "Cancelamento 2ª Entrevista" :
                            (_tipo.Status == "14" ? "Processo Seletivo Encerrado" :
                            (_tipo.Status == "15" ? "Aprovado" :
                            (_tipo.Status == "16" ? "Reprovado" :
                            ""))))))))))))))));

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataEntrevista = Convert.ToDateTime(dataReader["DataEntrevista"].ToString());
                            _tipo.Entrevista = _tipo.DataEntrevista.ToShortDateString() + " " 
                                + _tipo.DataEntrevista.ToShortTimeString();
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

        public List<ArquivosDoCandidato> ListarArquivosDoCandidatos(int IdCandidatos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ArquivosDoCandidato> _lista = new List<ArquivosDoCandidato>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idcandidato, tipo, arquivo, Extensao");
                query.Append("  from niff_cur_arqcandidatos");
                query.Append(" Where idcandidato = " + IdCandidatos);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ArquivosDoCandidato _tipo = new ArquivosDoCandidato();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdCandidato = Convert.ToInt32(dataReader["IdCandidato"].ToString());

                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Extensao = dataReader["Extensao"].ToString();

                        try
                        {
                            _tipo.Arquivo = (byte[])(dataReader["Arquivo"]);
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

        public List<Candidatos> ListarCandidatos(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Candidatos> _lista = new List<Candidatos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, telefone, celular, email, indicado, catho");
                query.Append("     , infojobs, LinkedIn, outros, descricaooutros, empregado");
                query.Append("  from Niff_Cur_Candidatos");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Candidatos _tipo = new Candidatos();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Telefone = Convert.ToDecimal(dataReader["telefone"].ToString());
                        _tipo.Celular = Convert.ToDecimal(dataReader["celular"].ToString());

                        _tipo.TelefoneFormatado = _tipo.Telefone.ToString("(00) 0000-0000");
                        _tipo.CelularFormatado = _tipo.Celular.ToString("(00) 00000-0000");
                        _tipo.Email = dataReader["email"].ToString();

                        _tipo.Indicado = dataReader["Indicado"].ToString() == "S";
                        _tipo.Catho = dataReader["Catho"].ToString() == "S";
                        _tipo.Infojobs = dataReader["InfoJobs"].ToString() == "S";
                        _tipo.LinkedIn = dataReader["LinkedIn"].ToString() == "S";
                        _tipo.Outros = dataReader["Outros"].ToString() == "S";
                        _tipo.Contratado = dataReader["empregado"].ToString() == "S";

                        _tipo.DescricaoOutros = dataReader["descricaooutros"].ToString();

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

        public Candidatos ConsultarCandidato(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Candidatos _tipo = new Candidatos();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, telefone, celular, email, indicado, catho");
                query.Append("     , infojobs, LinkedIn, outros, descricaooutros, empregado");
                query.Append("  from Niff_Cur_Candidatos");
                query.Append(" Where id = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Telefone = Convert.ToDecimal(dataReader["telefone"].ToString());
                        _tipo.Celular = Convert.ToDecimal(dataReader["celular"].ToString());
                        _tipo.TelefoneFormatado = _tipo.Telefone.ToString("(00) 0000-0000");
                        _tipo.CelularFormatado = _tipo.Celular.ToString("(00) 00000-0000");

                        _tipo.Email = dataReader["email"].ToString();

                        _tipo.Indicado = dataReader["Indicado"].ToString() == "S";
                        _tipo.Catho = dataReader["Catho"].ToString() == "S";
                        _tipo.Infojobs = dataReader["InfoJobs"].ToString() == "S";
                        _tipo.LinkedIn = dataReader["LinkedIn"].ToString() == "S";
                        _tipo.Outros = dataReader["Outros"].ToString() == "S";
                        _tipo.Contratado = dataReader["empregado"].ToString() == "S";

                        _tipo.DescricaoOutros = dataReader["descricaooutros"].ToString();

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

        public CandidatosDaVaga ConsultarVagasDaCandidato(int IdCandidato)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CandidatosDaVaga _tipo = new CandidatosDaVaga();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idvaga, c.idcandidato, c.data, v.idEmpresa");
                query.Append("  from niff_cur_candidatosvaga c, niff_Cur_Vagas v");
                query.Append(" Where c.idCandidato = " + IdCandidato);
                query.Append("   And v.Id = c.IdVaga");
                query.Append("   And Data = (select Max(Data) from niff_cur_candidatosvaga");
                query.Append("                where idCandidato = " + IdCandidato + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdVaga = Convert.ToInt32(dataReader["IdVaga"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdCandidato = Convert.ToInt32(dataReader["IdCandidato"].ToString());

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["data"].ToString());
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

        public List<CandidatosDaVaga> ConsultarCandidatosDaVaga(int IdVaga)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CandidatosDaVaga> _lista = new List<CandidatosDaVaga>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idvaga, c.idcandidato, c.data, v.idEmpresa, a.Arquivo");
                query.Append("  from niff_cur_candidatosvaga c, niff_Cur_Vagas v");
                query.Append("     , (Select b.Arquivo, b.Idcandidato From niff_cur_arqcandidatos b Where b.tipo = 'CV') a");
                query.Append(" Where v.id = " + IdVaga);
                query.Append("   And v.Id = c.IdVaga");
                query.Append("   And a.idcandidato(+) = c.idcandidato");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CandidatosDaVaga _tipo = new CandidatosDaVaga();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdVaga = Convert.ToInt32(dataReader["IdVaga"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdCandidato = Convert.ToInt32(dataReader["IdCandidato"].ToString());

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CVArquivo = (byte[])(dataReader["Arquivo"]);
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

        public int ProximoCandidato()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select nvl(Max(id),0) +1 next From Niff_Cur_Candidatos");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        retorno = Convert.ToInt32(dataReader["next"].ToString());
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

        public bool GravarCandidato(Candidatos tipo, List<ArquivosDoCandidato> _arquivos, CandidatosDaVaga _vagas, List<HistoricoDoCandidato> _historico)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_Cur_Candidatos");
                    query.Append("     ( id, nome, telefone, celular, email, indicado, catho");
                    query.Append("     , infojobs, LinkedIn, outros, descricaooutros, empregado )");
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + tipo.Nome + "'");
                    query.Append("        , " + tipo.Telefone);
                    query.Append("        , " + tipo.Celular);
                    query.Append("        , '" + tipo.Email + "'");
                    query.Append("        , '" + (tipo.Indicado ? "S" : "N") + "'");
                    query.Append("        , '" + (tipo.Catho ? "S" : "N") + "'");
                    query.Append("        , '" + (tipo.Infojobs ? "S" : "N") + "'");
                    query.Append("        , '" + (tipo.LinkedIn ? "S" : "N") + "'");
                    query.Append("        , '" + (tipo.Outros ? "S" : "N") + "'");
                    query.Append("        , '" + tipo.DescricaoOutros + "'");
                    query.Append("        , '" + (tipo.Contratado ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Cur_Candidatos");
                    query.Append("   set nome = '" + tipo.Nome + "'");
                    query.Append("     , telefone = " + tipo.Telefone);
                    query.Append("     , celular = " + tipo.Celular);
                    query.Append("     , email = '" + tipo.Email + "'");
                    query.Append("     , Indicado = '" + (tipo.Indicado ? "S" : "N") + "'");
                    query.Append("     , Catho = '" + (tipo.Catho ? "S" : "N") + "'");
                    query.Append("     , Infojobs = '" + (tipo.Infojobs ? "S" : "N") + "'");
                    query.Append("     , LinkedIn = '" + (tipo.LinkedIn ? "S" : "N") + "'");
                    query.Append("     , Outros = '" + (tipo.Outros ? "S" : "N") + "'");
                    query.Append("     , descricaooutros = '" + tipo.DescricaoOutros + "'");
                    query.Append("     , empregado = '" + (tipo.Contratado ? "S" : "N") + "'");
                    query.Append(" Where id = " + tipo.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                    retorno = AssociarVagas(_vagas, _historico);

                if (retorno && _arquivos != null && _arquivos.Count() != 0)
                {
                    retorno = GravarArquivosCandidato(_arquivos);
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

        public bool GravarArquivosCandidato( List<ArquivosDoCandidato> _arquivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();

            bool retorno = true;

            try
            {
                foreach (var tipo in _arquivos)
                {
                    query.Clear();
                    parametros.Clear();
                    if (!tipo.Existe)
                    {
                        query.Append("Insert into niff_cur_arqcandidatos");
                        query.Append("     ( id, idcandidato, tipo, arquivo, Extensao )");
                        query.Append(" Values ( (Select nvl(Max(id),0) +1 next From niff_cur_arqcandidatos ) ");
                        query.Append("        ,'" + tipo.IdCandidato + "'");
                        query.Append("        ,'" + tipo.Tipo + "'");
                        query.Append("        , :pfoto");
                        query.Append("        ,'" + tipo.Extensao.Replace('.',' ').Trim() + "'");
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update niff_cur_arqcandidatos");
                        query.Append("   set Arquivo = :pfoto");
                        query.Append("     , Extensao = '" + tipo.Extensao.Replace('.', ' ').Trim() + "'");
                        query.Append(" Where id = " + tipo.Id);
                    }

                    if (tipo.Arquivo == null)
                        query.Append("   , null");
                    else
                    {
                        parametro.ParameterName = ":pfoto";
                        parametro.Value = tipo.Arquivo;
                        parametro.OracleType = OracleType.Blob;
                        parametros.Add(parametro);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());

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

        public bool AssociarVagas(CandidatosDaVaga tipo, List<HistoricoDoCandidato> _historicos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int id = 0;

            try
            {
                if (tipo.Existe)
                    id = tipo.Id;
                else
                {
                    query.Clear();
                    query.Append("Select nvl(Max(id),0) +1 next From niff_cur_candidatosvaga");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into niff_cur_candidatosvaga");
                    query.Append("     ( id, idvaga, idcandidato, data)");
                    query.Append(" Values (" + id);
                    query.Append("        , " + tipo.IdVaga);
                    query.Append("        , " + tipo.IdCandidato);
                    query.Append("        , sysdate " );
                    query.Append(" )");

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                }
                
                if (retorno)
                    retorno = GravarHistorico(_historicos);
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

            return retorno;
        }

        public bool GravarHistorico(List<HistoricoDoCandidato> _historicos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var tipo in _historicos)
                {
                    query.Clear();

                    if (!tipo.Existe)
                    {
                        query.Append("Insert into niff_cur_historicocandidato");
                        query.Append("     ( id, idvaga, idCandidato, status, motivo, data, dataEntrevista)");
                        query.Append(" Values ((Select nvl(Max(id),0) +1 next From niff_cur_historicocandidato) ");

                        if (tipo.IdVaga != 0)
                            query.Append("        , " + tipo.IdVaga);
                        else
                            query.Append("        , null");

                        query.Append("        , " + tipo.IdCandidato);
                        query.Append("        , '" + tipo.Status + "'");
                        query.Append("        , '" + tipo.Motivo + "'");
                        query.Append("        , SysDate");

                        if (tipo.DataEntrevista != DateTime.MinValue)
                            query.Append("        , To_date('" + tipo.DataEntrevista.ToShortDateString() + " " + tipo.DataEntrevista.ToShortTimeString() + "','dd/mm/yyyy hh24:mi')");
                        else
                            query.Append("        , null");

                        query.Append(" )");
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        query.Append("Update niff_cur_historicocandidato");
                        query.Append("   Set Motivo = '" + tipo.Motivo + "'");
                        query.Append(" Where Id = " + tipo.Id);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }
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
            return retorno;
        }

        public bool ExcluirCandidato(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                query.Append("Delete niff_cur_arqcandidatos");
                query.Append(" Where idCandidato = " + id);
                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    query.Clear();
                    query.Append("Delete niff_cur_candidatosvaga");
                    query.Append(" Where idCandidato = " + id);
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                    if (retorno)
                    {
                        query.Clear();
                        query.Append("Delete niff_cur_historicocandidato");
                        query.Append(" Where idCandidato = " + id);
                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (retorno)
                        {
                            query.Clear();
                            query.Append("Delete Niff_Cur_Candidatos");
                            query.Append(" Where id = " + id);
                            retorno = sessao.ExecuteSqlTransaction(query.ToString());
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

        
        #endregion

        #region Vagas
        public List<Vagas> ListarVagas(bool apenasAtivos, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Vagas> _lista = new List<Vagas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select v.id, v.descricao, v.abertura, v.encerramento, v.idempresa, v.idcandidato");
                query.Append("     , v.status, v.catho, v.infojobs, v.linkedin, v.outros, v.outrolocaldeanuncio");
                query.Append("     , v.confidencial, v.Detalhamento, v.IdEmpresaEnrevista, v.Endereco, v.InformacoesGerais");
                query.Append("     , e.Nomeabreviado");
                query.Append("  from Niff_Cur_Vagas v, niff_chm_empresas e");
                query.Append(" Where e.idempresa = v.Idempresa");

                if (idEmpresa != 0)
                    query.Append("  And v.IdEmpresa = " + idEmpresa);

                if (apenasAtivos)
                    query.Append("  And v.Status = 'A'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Vagas _tipo = new Vagas();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.DescricaoOutros = dataReader["outrolocaldeanuncio"].ToString();
                        _tipo.NomeEmpresa = dataReader["Nomeabreviado"].ToString();
                        _tipo.Catho = dataReader["Catho"].ToString() == "S";
                        _tipo.Infojobs = dataReader["InfoJobs"].ToString() == "S";
                        _tipo.LinkedIn = dataReader["LinkedIn"].ToString() == "S";
                        _tipo.Outros = dataReader["Outros"].ToString() == "S";
                        _tipo.Confidencial = dataReader["Confidencial"].ToString() == "S";
                        _tipo.Detalhamento = dataReader["Detalhamento"].ToString();
                        _tipo.InformacoesGerais = dataReader["InformacoesGerais"].ToString();
                        _tipo.EnderecoEntevista = dataReader["Endereco"].ToString();

                        _tipo.DescricaoStatus = (_tipo.Status.Equals("A") ? "Aberta" : (_tipo.Status.Equals("E") ? "Encerrada" : "Congelada"));
                        try
                        {
                            _tipo.Abertura = Convert.ToDateTime(dataReader["Abertura"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Encerramento = Convert.ToDateTime(dataReader["Encerramento"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdEmpresaEntrevista = Convert.ToInt32(dataReader["IdEmpresaEnrevista"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdCandidato = Convert.ToInt32(dataReader["IdCandidato"].ToString());
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

        public Vagas ConsultarVaga(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Vagas _tipo = new Vagas();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, descricao, abertura, encerramento, idempresa, idcandidato");
                query.Append("     , status, catho, infojobs, linkedin, outros, outrolocaldeanuncio");
                query.Append("     , confidencial, Detalhamento, IdEmpresaEnrevista, Endereco, InformacoesGerais");
                query.Append("  from Niff_Cur_Vagas");
                query.Append(" Where id = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.DescricaoOutros = dataReader["outrolocaldeanuncio"].ToString();
                        _tipo.Catho = dataReader["Catho"].ToString() == "S";
                        _tipo.Infojobs = dataReader["InfoJobs"].ToString() == "S";
                        _tipo.LinkedIn = dataReader["LinkedIn"].ToString() == "S";
                        _tipo.Outros = dataReader["Outros"].ToString() == "S";
                        _tipo.Confidencial = dataReader["Confidencial"].ToString() == "S";
                        _tipo.Detalhamento = dataReader["Detalhamento"].ToString();
                        _tipo.InformacoesGerais = dataReader["InformacoesGerais"].ToString();
                        _tipo.EnderecoEntevista = dataReader["Endereco"].ToString();

                        _tipo.DescricaoStatus = (_tipo.Status.Equals("A") ? "Aberta" : (_tipo.Status.Equals("E") ? "Encerrada" : "Congelada"));

                        try
                        {
                            _tipo.Abertura = Convert.ToDateTime(dataReader["Abertura"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Encerramento = Convert.ToDateTime(dataReader["Encerramento"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdEmpresaEntrevista = Convert.ToInt32(dataReader["IdEmpresaEnrevista"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdCandidato = Convert.ToInt32(dataReader["IdCandidato"].ToString());
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

        public int ProximaVaga()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(id) +1 next From Niff_Cur_Vagas");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        retorno = Convert.ToInt32(dataReader["next"].ToString());
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

        public bool GravarVaga(Vagas tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_Cur_Vagas");
                    query.Append(" ( id, descricao, abertura, idempresa");
                    query.Append(" , status, catho, infojobs, linkedin, outros, outrolocaldeanuncio, confidencial, Detalhamento ");
                    query.Append(" , IdEmpresaEnrevista, Endereco, InformacoesGerais )"); 
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + tipo.Descricao + "'");
                    query.Append("        , To_date('" + tipo.Abertura.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("        , " + tipo.IdEmpresa);
                    query.Append("        , '" + tipo.Status + "'");
                    query.Append("        , '" + (tipo.Catho ? "S" : "N") + "'");
                    query.Append("        , '" + (tipo.Infojobs ? "S" : "N") + "'");
                    query.Append("        , '" + (tipo.LinkedIn ? "S" : "N") + "'");
                    query.Append("        , '" + (tipo.Outros ? "S" : "N") + "'");
                    query.Append("        , '" + tipo.DescricaoOutros + "'");
                    query.Append("        , '" + (tipo.Confidencial ? "S" : "N") + "'");
                    query.Append("        , '" + tipo.Detalhamento + "'");
                    query.Append("        , " + (tipo.IdEmpresaEntrevista == 0 ? "null" : tipo.IdEmpresaEntrevista.ToString() ));
                    query.Append("        , '" + tipo.EnderecoEntevista + "'");
                    query.Append("        , '" + tipo.InformacoesGerais + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Cur_Vagas");
                    query.Append("   set descricao = '" + tipo.Descricao + "'");

                    if (tipo.Encerramento != DateTime.MinValue)
                        query.Append("     , Encerramento = To_date('" + tipo.Encerramento.ToShortDateString() + "','dd/mm/yyyy')");

                    if (tipo.IdCandidato != 0)
                        query.Append("     , IdCandidato = " + tipo.IdCandidato);

                    query.Append("     , IdEmpresa = " + tipo.IdEmpresa);
                    query.Append("     , Status = '" + tipo.Status + "'");
                    query.Append("     , Catho = '" + (tipo.Catho ? "S" : "N") + "'");
                    query.Append("     , Infojobs = '" + (tipo.Infojobs ? "S" : "N") + "'");
                    query.Append("     , LinkedIn = '" + (tipo.LinkedIn ? "S" : "N") + "'");
                    query.Append("     , Outros = '" + (tipo.Outros ? "S" : "N") + "'");
                    query.Append("     , Outrolocaldeanuncio = '" + tipo.DescricaoOutros + "'");
                    query.Append("     , Confidencial = '" + (tipo.Confidencial ? "S" : "N") + "'");
                    query.Append("     , Detalhamento = '" + tipo.Detalhamento + "'");
                    query.Append("     , IdEmpresaEnrevista = " + (tipo.IdEmpresaEntrevista == 0 ? "null" : tipo.IdEmpresaEntrevista.ToString()));
                    query.Append("     , Endereco = '" + tipo.EnderecoEntevista + "'");
                    query.Append("     , InformacoesGerais = '" + tipo.InformacoesGerais + "'");

                    query.Append(" Where id = " + tipo.Id);
                }

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

        public bool ExcluirVaga(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Cur_Vagas");
                query.Append(" Where id = " + id);
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
        #endregion
    }
}
