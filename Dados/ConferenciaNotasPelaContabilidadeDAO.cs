using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ConferenciaNotasPelaContabilidadeDAO
    {
        IDataReader dataReader;
        IDataReader dataReaderAux;
        IDataReader dataReader2;
        string codcontaCtb = "";
        string nomeContaCtb = "";
        bool conferido = false;

        #region Grupo Despesas
        public List<ConferenciaNotasPelaContabilidade.GrupoDespesas> ListarGrupo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ConferenciaNotasPelaContabilidade.GrupoDespesas> lista = new List<ConferenciaNotasPelaContabilidade.GrupoDespesas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("select codigogrd, descricaogrd");
                query.Append("  from est_grupodespesas g");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ConferenciaNotasPelaContabilidade.GrupoDespesas _tipo = new ConferenciaNotasPelaContabilidade.GrupoDespesas();
                        _tipo.Existe = true;
                        _tipo.Codigo = Convert.ToInt32(dataReader["codigogrd"].ToString());
                        _tipo.Descricao = dataReader["descricaogrd"].ToString();
                        
                        lista.Add(_tipo);
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
            return lista;
        }

        public ConferenciaNotasPelaContabilidade.GrupoDespesas ConsultarGrupo(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            ConferenciaNotasPelaContabilidade.GrupoDespesas _tipo = new ConferenciaNotasPelaContabilidade.GrupoDespesas();

            try
            {
                query.Append("select codigogrd, descricaogrd");
                query.Append("  from est_grupodespesas g");
                query.Append(" where codigogrd = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Codigo = Convert.ToInt32(dataReader["codigogrd"].ToString());
                        _tipo.Descricao = dataReader["descricaogrd"].ToString();
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
        #endregion
        
        #region Parametros
        public List<ConferenciaNotasPelaContabilidade.Parametros> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ConferenciaNotasPelaContabilidade.Parametros> lista = new List<ConferenciaNotasPelaContabilidade.Parametros>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("select t.id, t.codgrupodespesa, t.codtpdespesa, t.nroplano, t.codcontactb");
                query.Append("     , g.descricaogrd, d.desctpdespesa, c.classificador || ' - '  || c.nomeconta conta");
                query.Append("  from niff_ctb_paramnotas t, est_grupodespesas g, Cpgtpdes d, ctbconta c");
                query.Append(" Where t.codgrupodespesa = g.codigogrd");
                query.Append("   and t.codtpdespesa = d.codtpdespesa");
                query.Append("   And t.nroplano = c.nroplano");
                query.Append("   And t.codcontactb = c.codcontactb");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ConferenciaNotasPelaContabilidade.Parametros _tipo = new ConferenciaNotasPelaContabilidade.Parametros();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.CodigoGrupo = Convert.ToInt32(dataReader["codgrupodespesa"].ToString());

                        _tipo.CodigoTipo = dataReader["codtpdespesa"].ToString();
                        _tipo.Grupo = _tipo.CodigoGrupo + " - " + dataReader["descricaogrd"].ToString();
                        _tipo.NomeTipo = _tipo.CodigoTipo + " - " + dataReader["desctpdespesa"].ToString();

                        try
                        {
                            _tipo.NumeroPlano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        }
                        catch { }
                        _tipo.CodigoConta = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        _tipo.NomeConta = _tipo.CodigoConta + " - " + dataReader["Conta"].ToString();

                        _tipo.CodigoContaOriginal = _tipo.CodigoConta;
                        _tipo.CodigoTipoOriginal = _tipo.CodigoTipo;
                        _tipo.NumeroPlanoOriginal = _tipo.NumeroPlano;
                        lista.Add(_tipo);
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
            return lista;
        }

        public bool Gravar(List<ConferenciaNotasPelaContabilidade.Parametros> _lista)
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
                    if (!item.Existe)
                    {
                        query.Append("Insert into niff_ctb_paramnotas");
                        query.Append(" ( id, codgrupodespesa, codtpdespesa, nroplano, codcontactb");
                        query.Append(") Values ( (Select nvl(Max(id),0) +1 from niff_ctb_paramnotas) ");
                        query.Append("        , " + item.CodigoGrupo);
                        query.Append("        , '" + item.CodigoTipo + "'");
                        query.Append("        , " + item.NumeroPlano);
                        query.Append("        , " + item.CodigoConta);
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update niff_ctb_paramnotas");
                        query.Append("   set codtpdespesa = '" + item.CodigoTipo + "'");
                        query.Append("     , nroplano = " + item.NumeroPlano);
                        query.Append("     , codcontactb = " + item.CodigoConta);
                        query.Append(" where id = " + item.Id);
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

        public bool ExcluirTipo(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ctb_paramnotas");
                query.Append(" Where Id = " + id);
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

        public bool ExcluirGrupo(int grupo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ctb_paramnotas");
                query.Append(" Where codgrupodespesa = " + grupo);
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

        #region Conferencia
        private ConferenciaNotasPelaContabilidade.ItensConferencia BuscaValidado(decimal material)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ConferenciaNotasPelaContabilidade.ItensConferencia tipo = new ConferenciaNotasPelaContabilidade.ItensConferencia();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Min(c.datavalidado) Data, c.Idusuariovalidado");
                query.Append("     , c.referencia, c.idempresa, e.codigoglobus");
                query.Append("     ,  SUBSTR(uv.nome, 1, INSTR(uv.nome, ' ') - 1) || Substr(uv.nome, INSTR(uv.nome, ' ', -1)) validador");
                query.Append("  From niff_ctb_conferencianotas c, Niff_Chm_Usuarios uv, niff_chm_empresas e");
                query.Append(" Where codmaterial = " + material);
                query.Append("   And validado = 'S'");
                query.Append("   And uv.Idusuario = c.Idusuariovalidado");
                query.Append("   And e.idempresa = c.idempresa");
                query.Append(" Group By c.Idusuariovalidado, uv.nome, c.referencia, c.idempresa, e.codigoglobus");


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader2 = executar.ExecuteQuery();

                using (dataReader2)
                {
                    if (dataReader2.Read())
                    {
                        tipo.Existe = true;
                        tipo.Validado = true;
                        tipo.UsuarioValidador = dataReader2["validador"].ToString();
                        tipo.IdEmpresa = Convert.ToInt32(dataReader2["IdEmpresa"].ToString());
                        tipo.ReferenciaValidado = Convert.ToInt32(dataReader2["referencia"].ToString());
                        tipo.CodigoGlobus = dataReader2["codigoglobus"].ToString();

                        try
                        {
                            tipo.IdUsuarioValidador = Convert.ToInt32(dataReader2["Idusuariovalidado"].ToString());
                        }
                        catch { }

                        try
                        {
                            tipo.DataValidado = Convert.ToDateTime(dataReader2["Data"].ToString());
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
            return tipo;
        }

        public List<ConferenciaNotasPelaContabilidade.Conferencia> Listar(int idEmpresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ConferenciaNotasPelaContabilidade.Conferencia> lista = new List<ConferenciaNotasPelaContabilidade.Conferencia>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select n.codintnf, To_Number(n.numeronf) numeronf, n.coddoctoesf, 0 codissint");
                query.Append("     , n.codtpdoc, trunc(n.entradasaidanf) entrada");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , n.valortotalnf valortotal");
                query.Append("     , d.obsdoctocpg, d.CodDoctoCPG");
                query.Append("     , decode(n.codlanca, Null, 'Não Integrado', l.documentolanca || ' - ' || to_char(l.dtlanca, 'dd/mm/yyyy')) ctb");
                query.Append("     , 'EST' Origem");
                query.Append("  From Bgm_Notafiscal N");
                query.Append("     , Bgm_Fornecedor f");
                query.Append("     , cpgdocto d");
                query.Append("     , Niff_Chm_Empresas e");
                query.Append("     , ctblanca l");
                query.Append(" Where f.codigoforn = n.codigoforn");
                query.Append("   And d.coddoctocpg = n.coddoctocpg(+)");
                query.Append("   And to_char(n.entradasaidanf, 'yyyymm') = '" + referencia + "'");
                query.Append("   And e.Idempresa = " + idEmpresa);
                query.Append("   And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = e.codigoglobus");
                //query.Append("   And n.codintnf = 368656");
                query.Append("   And l.codlanca(+) = n.codlanca");

                query.Append(" Union all ");
                query.Append("Select n.codintnf, To_Number(n.numeronf) numeronf, n.coddoctoesf, 0 codissint ");
                query.Append("     , n.codtpdoc, trunc(n.datahoraentsai) entrada");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , n.valortotal");
                query.Append("     , d.obsdoctocpg, d.CodDoctoCPG");
                query.Append("     , decode(n.codlanca, Null, 'Não Integrado', l.documentolanca || ' - ' || to_char(l.dtlanca, 'dd/mm/yyyy')) ctb");
                query.Append("     , 'ESF' Origem");
                query.Append("  From Esfnotafiscal N");
                query.Append("     , Bgm_Fornecedor f");
                query.Append("     , cpgdocto d");
                query.Append("     , Niff_Chm_Empresas e");
                query.Append("     , ctblanca l");
                query.Append(" Where f.codigoforn = n.codigoforn");
                query.Append("   And n.coddoctoesf = d.coddoctoesf(+)");
                query.Append("   And to_char(n.datahoraentsai,'yyyymm') = '" + referencia + "'");
                query.Append("   And e.Idempresa = " + idEmpresa);
                query.Append("   And lpad(n.codigoempresa,3,'0') || '/' || lpad(n.codigoFl, 3, '0') = e.codigoglobus");
                //query.Append("   And n.codintnf = 368656");
                query.Append("   And l.codlanca(+) = n.codlanca");

                query.Append(" Union all ");// Livro ISS
                query.Append("Select n.codintnf, To_Number(n.documentoini) numeronf, 0 coddoctoesf, n.codissint");
                query.Append("     , n.codtpdoc, trunc(n.entrada) entrada");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , n.vlrservico");
                query.Append("     , d.obsdoctocpg, d.CodDoctoCPG");
                query.Append("     , decode(n.codlanca, Null, 'Não Integrado', l.documentolanca || ' - ' || to_char(l.dtlanca, 'dd/mm/yyyy')) ctb");
                query.Append("     , 'ESF' Origem");
                query.Append("  From Esfiss_Entra n");
                query.Append("     , bgm_fornecedor f");
                query.Append("     , Cpgdocto d");
                query.Append("     , niff_chm_empresas e");
                query.Append("     , ctblanca l");
                query.Append(" Where f.codigoforn = n.codigoforn");
                query.Append("   And n.coddoctocpg = d.coddoctocpg(+)");
                query.Append("   And e.Idempresa = " + idEmpresa); 
                query.Append("   And lpad(n.codigoempresa,3,'0') || '/' || lpad(n.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And l.codlanca(+) = n.codlanca");
                query.Append("   And to_char(n.entrada,'yyyymm') = '" + referencia + "'");
                query.Append("   And n.sistema = 'ESF'");
                query.Append("   And (n.observacoes is null or n.observacoes not like '%Integrado pela NIFF%')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ConferenciaNotasPelaContabilidade.Conferencia _tipo = new ConferenciaNotasPelaContabilidade.Conferencia();
                        _tipo.Existe = true;
                        _tipo.Conferida = true;

                        try
                        {
                            _tipo.CodIntNF = Convert.ToDecimal(dataReader["codintnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CodDoctoESF = Convert.ToDecimal(dataReader["coddoctoesf"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodISSInt = Convert.ToDecimal(dataReader["codissint"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodDoctoCPG = Convert.ToDecimal(dataReader["CodDoctoCPG"].ToString());
                        }
                        catch { }

                        _tipo.Fornecedor = dataReader["Fornecedor"].ToString();
                        _tipo.NumeroNF = Convert.ToDecimal(dataReader["NumeroNF"].ToString());
                        _tipo.Entrada = Convert.ToDateTime(dataReader["Entrada"].ToString());
                        _tipo.CodTipoDocto = dataReader["CodTpDoc"].ToString();
                        _tipo.ObservacaoCPG = dataReader["obsdoctocpg"].ToString();
                        _tipo.ObservacaoCPGOriginal = _tipo.ObservacaoCPG;
                        _tipo.Documento = dataReader["CTB"].ToString();
                        _tipo.Origem = dataReader["Origem"].ToString();

                        _tipo.Valor = Convert.ToDecimal(dataReader["ValorTotal"].ToString());

                        lista.Add(_tipo);
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
            return lista;
        }

        private void BuscaContaDoLancamentoContabil(decimal codlanca, string conta, int plano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("select i.vritemlanca, i.itemconferido, c.codcontactb, c.Nomeconta ");
                query.Append("  from ctbitlnc i, ctbconta c");
                query.Append(" Where i.codlanca = " + codlanca);
                query.Append("   And c.nroplano = i.nroplano");
                query.Append("   And c.codcontactb = i.codcontactb");
                query.Append("   And i.debitocreditoitemlanca = 'D'");
                query.Append("   And i.codcontactb = " + conta);
                query.Append("   And i.nroplano = " + plano);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReaderAux = executar.ExecuteQuery();

                using (dataReaderAux)
                {
                    if (dataReaderAux.Read())
                    {
                        codcontaCtb = dataReaderAux["codcontactb"].ToString();

                        if (codcontaCtb != "")
                        {
                            nomeContaCtb = codcontaCtb + " - " + dataReaderAux["Nomeconta"].ToString();
                            conferido = dataReaderAux["itemconferido"].ToString() == "S";
                        }
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

        }

        private void BuscaContaDoLancamentoContabil(decimal codlanca, int plano, decimal valor)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("select i.vritemlanca, c.codcontactb, c.Nomeconta, i.itemconferido ");
                query.Append("  from ctbitlnc i, ctbconta c");
                query.Append(" Where i.codlanca = " + codlanca);
                query.Append("   And c.nroplano = i.nroplano");
                query.Append("   And c.codcontactb = i.codcontactb");
                query.Append("   And i.debitocreditoitemlanca = 'D'");
                query.Append("   And i.nroplano = " + plano);
                query.Append("   And i.vritemlanca = " + Math.Round(valor,2).ToString().Replace(".", "").Replace(",", "."));

                Query executar = sessao.CreateQuery(query.ToString());

                dataReaderAux = executar.ExecuteQuery();

                using (dataReaderAux)
                {
                    if (dataReaderAux.Read())
                    {
                        codcontaCtb = dataReaderAux["codcontactb"].ToString();

                        if (codcontaCtb != "")
                        {
                            nomeContaCtb = codcontaCtb + " - " + dataReaderAux["Nomeconta"].ToString();
                            conferido = dataReaderAux["itemconferido"].ToString() == "S";
                        }
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
        }

        public List<ConferenciaNotasPelaContabilidade.ItensConferencia> ListarItens(int idEmpresa, string referencia, int plano, bool apenasNaoConferidas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ConferenciaNotasPelaContabilidade.ItensConferencia _tipoValidado = new ConferenciaNotasPelaContabilidade.ItensConferencia();
            List<ConferenciaNotasPelaContabilidade.ItensConferencia> lista = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();
            Publicas.mensagemDeErro = string.Empty;
            List<ConferenciaNotasPelaContabilidade.Parametros> listaParametros = Listar();

            try
            {
                query.Append("Select n.codintnf, To_Number(n.numeronf) numeronf, n.coddoctoesf, 0 codissint ");
                query.Append("     , n.codtpdoc, trunc(n.entradasaidanf) entrada");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , m.codigomatint codigomatavulso, m.codigointernomaterial || ' - ' || m.descricaomat material");
                query.Append("     , i.valortotalitensnf valor, g.codigogrd");
                query.Append("     , g.codigogrd || ' - ' || g.descricaogrd Grupo");
                query.Append("     , m.codtpdespesa TpDespesaMaterial");
                query.Append("     , m.codtpdespesa TpDespesaNota");
                query.Append("     , ge.codcontactb ContaMaterial");
                query.Append("     , ge.codcontactb ContaNota");
                query.Append("     , null codcustoctb");
                query.Append("     , d.obsdoctocpg, d.CodDoctoCPG");
                query.Append("     , c.conferido");
                query.Append("     , c.Idusuario");
                query.Append("     , c.validado");
                query.Append("     , c.Idusuariovalidado");
                query.Append("     , To_number(to_char(n.entradasaidanf, 'yyyymm')) Referencia");
                query.Append("     , e.IdEmpresa");
                query.Append("     , c.Id");
                query.Append("     , c.dataconferido");
                query.Append("     , c.Datavalidado");
                query.Append("     , SUBSTR(uc.nome, 1, INSTR(uc.nome, ' ') - 1) || Substr(uc.nome, INSTR(uc.nome, ' ', -1)) Conferente");
                query.Append("     , SUBSTR(uv.nome, 1, INSTR(uv.nome, ' ') - 1) || Substr(uv.nome, INSTR(uv.nome, ' ', -1)) validador");
                query.Append("     , cm.nomeconta NomeContaMaterial");
                query.Append("     , cn.nomeconta NomeContaNota");
                query.Append("     , n.codlanca, ge.nroplano, 'EST' Origem, e.codigoglobus ");
                query.Append("  From Bgm_Notafiscal N");
                query.Append("     , Bgm_Fornecedor f");
                query.Append("     , cpgdocto d");
                query.Append("     , Niff_Chm_Empresas e");
                query.Append("     , Est_Itensnf i");
                query.Append("     , Est_Cadmaterial m");
                query.Append("     , est_grupodespesas g, Est_Ctbestoque ge");
                query.Append("     , Niff_Ctb_ConferenciaNotas c");
                query.Append("     , Niff_Chm_Usuarios uc");
                query.Append("     , Niff_Chm_Usuarios  uv");
                query.Append("     , ctbconta cm");
                query.Append("     , ctbconta cn");
                query.Append(" Where f.codigoforn = n.codigoforn");
                query.Append("   And d.coddoctocpg = n.coddoctocpg(+)");
                query.Append("   And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And n.codintnf = i.codintnf");
                query.Append("   And m.codigomatint = i.codigomatint");
                query.Append("   And m.codigogrd = g.codigogrd(+)");
                query.Append("   And m.codigogrcon = ge.codigogrcon");
                query.Append("   And c.Idempresa(+) = e.Idempresa");
                query.Append("   And c.Codintnf(+) = n.codintnf");
                query.Append("   And c.codmaterial(+) = m.codigomatint");
                query.Append("   And c.referencia(+) = To_number(to_char(n.entradasaidanf, 'yyyymm'))");
                query.Append("   And to_char(n.entradasaidanf, 'yyyymm') = '" + referencia + "'");
                query.Append("   And e.Idempresa = " + idEmpresa);
                //query.Append("   And n.codintnf = 369026");
                query.Append("   And uc.idusuario(+) = c.idusuario");
                query.Append("   And uv.idusuario(+) = c.Idusuariovalidado");
                query.Append("   And cm.nroplano(+) = ge.nroplano");
                query.Append("   And cm.codcontactb(+) = ge.codcontactb");
                query.Append("   And cn.nroplano(+) = ge.nroplano");
                query.Append("   And cn.codcontactb(+) = ge.codcontactb");
                query.Append("   And ge.nroplano = " + plano);

                query.Append(" Union all "); // Estoque com Servico
                query.Append("Select n.codintnf, To_Number(n.numeronf) numeronf, n.coddoctoesf, 0 codissint ");
                query.Append("     , n.codtpdoc, trunc(n.entradasaidanf) entrada");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , m.codigomatavulso, m.Codigomatavulso_est || ' (' || m.codigomatavulso || ') - ' || m.descricaomatavulso material");
                query.Append("     , i.valornfservico valor, g.codigogrd");
                query.Append("     , g.codigogrd || ' - ' || g.descricaogrd Grupo");
                query.Append("     , m.codtpdespesa TpDespesaMaterial");
                query.Append("     , i.codtpdespesa TpDespesaNota");
                query.Append("     , m.codcontactb ContaMaterial");
                query.Append("     , i.codcontactb ContaNota");
                query.Append("     , i.codcusto codcustoctb");
                query.Append("     , d.obsdoctocpg, d.CodDoctoCPG");
                query.Append("     , c.conferido");
                query.Append("     , c.Idusuario");
                query.Append("     , c.validado");
                query.Append("     , c.Idusuariovalidado");
                query.Append("     , To_number(to_char(n.entradasaidanf, 'yyyymm')) Referencia");
                query.Append("     , e.IdEmpresa");
                query.Append("     , c.Id");
                query.Append("     , c.dataconferido");
                query.Append("     , c.Datavalidado");
                query.Append("     , SUBSTR(uc.nome, 1, INSTR(uc.nome, ' ') - 1) || Substr(uc.nome, INSTR(uc.nome, ' ', -1)) Conferente");
                query.Append("     , SUBSTR(uv.nome, 1, INSTR(uv.nome, ' ') - 1) || Substr(uv.nome, INSTR(uv.nome, ' ', -1)) validador");
                query.Append("     , cm.nomeconta NomeContaMaterial");
                query.Append("     , cn.nomeconta NomeContaNota");
                query.Append("     , n.codlanca, i.nroplano, 'EST' Origem, e.codigoglobus ");
                query.Append("  From Bgm_Notafiscal N");
                query.Append("     , Bgm_Fornecedor f");
                query.Append("     , cpgdocto d");
                query.Append("     , Niff_Chm_Empresas e");
                query.Append("     , Est_Nfservico i");
                query.Append("     , cpr_cadmaterialavulso m");
                query.Append("     , est_grupodespesas g");
                query.Append("     , Niff_Ctb_ConferenciaNotas c");
                query.Append("     , Niff_Chm_Usuarios uc");
                query.Append("     , Niff_Chm_Usuarios  uv");
                query.Append("     , ctbconta cm");
                query.Append("     , ctbconta cn");
                query.Append(" Where f.codigoforn = n.codigoforn");
                query.Append("   And d.coddoctocpg = n.coddoctocpg(+)");
                query.Append("   And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And n.codintnf = i.codintnf");
                query.Append("   And m.Codigomatavulso_EST = i.codigomatavulso");
                query.Append("   And m.codigogrd = g.codigogrd(+)");
                query.Append("   And c.Idempresa(+) = e.Idempresa");
                query.Append("   And c.Codintnf(+) = n.codintnf");
                query.Append("   And c.codmaterial(+) = m.codigomatavulso");
                query.Append("   And c.referencia(+) = To_number(to_char(n.entradasaidanf, 'yyyymm'))");
                query.Append("   And to_char(n.entradasaidanf, 'yyyymm') = '" + referencia + "'");
                query.Append("   And e.Idempresa = " + idEmpresa);
                query.Append("   And i.nroplano = " + plano);
                //query.Append("   And n.codintnf = 369026");
                query.Append("   And uc.idusuario(+) = c.idusuario");
                query.Append("   And uv.idusuario(+) = c.Idusuariovalidado");
                query.Append("   And cm.nroplano(+) = m.nroplano");
                query.Append("   And cm.codcontactb(+) = m.codcontactb");
                query.Append("   And cn.nroplano(+) = i.nroplano");
                query.Append("   And cn.codcontactb(+) = i.codcontactb"); 
                
                query.Append(" Union all "); // ESF 
                query.Append("Select n.codintnf, To_Number(n.numeronf) numeronf, n.coddoctoesf, 0 codissint ");
                query.Append("     , n.codtpdoc, trunc(n.datahoraentsai) entrada");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , m.codigomatavulso, m.codigomatavulso || ' - ' || m.descricaomatavulso material");
                query.Append("     , i.vlrtotal, g.codigogrd");
                query.Append("     , g.codigogrd || ' - ' || g.descricaogrd Grupo");
                query.Append("     , m.codtpdespesa TpDespesaMaterial");
                query.Append("     , i.codtpdespesa TpDespesaNota");
                query.Append("     , m.codcontactb ContaMaterial");
                query.Append("     , i.codcontactb ContaNota");
                query.Append("     , i.codcustoctb");
                query.Append("     , d.obsdoctocpg, d.CodDoctoCPG");
                query.Append("     , c.conferido");
                query.Append("     , c.Idusuario");
                query.Append("     , c.validado");
                query.Append("     , c.Idusuariovalidado");
                query.Append("     , To_number(to_char(n.datahoraentsai, 'yyyymm')) Referencia");
                query.Append("     , e.IdEmpresa");
                query.Append("     , c.Id");
                query.Append("     , c.dataconferido");
                query.Append("     , c.Datavalidado");
                query.Append("     , SUBSTR(uc.nome, 1, INSTR(uc.nome, ' ') - 1) || Substr(uc.nome, INSTR(uc.nome, ' ', -1)) Conferente");
                query.Append("     , SUBSTR(uv.nome, 1, INSTR(uv.nome, ' ') - 1) || Substr(uv.nome, INSTR(uv.nome, ' ', -1)) validador");
                query.Append("     , cm.nomeconta NomeContaMaterial");
                query.Append("     , cn.nomeconta NomeContaNota");
                query.Append("     , n.codlanca, i.nroplano, 'ESF' Origem, e.codigoglobus ");
                query.Append("  From Esfnotafiscal N");
                query.Append("     , Bgm_Fornecedor f");
                query.Append("     , cpgdocto d");
                query.Append("     , Niff_Chm_Empresas e");
                query.Append("     , Esfnotafiscal_Item i");
                query.Append("     , cpr_cadmaterialavulso m");
                query.Append("     , est_cadmaterialavulso me");
                query.Append("     , est_grupodespesas g");
                query.Append("     , Niff_Ctb_ConferenciaNotas c");
                query.Append("     , Niff_Chm_Usuarios uc");
                query.Append("     , Niff_Chm_Usuarios  uv");
                query.Append("     , ctbconta cm");
                query.Append("     , ctbconta cn");
                query.Append(" Where f.codigoforn = n.codigoforn");
                query.Append("   And n.coddoctoesf = d.coddoctoesf(+)");
                query.Append("   And lpad(n.codigoempresa,3,'0') || '/' || lpad(n.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And n.codintnf = i.codintnf");
                query.Append("   And me.codigomatavulso = i.codproduto");
                query.Append("   And m.codigomatavulso_est = me.codigomatavulso");
                query.Append("   And m.codigogrd = g.codigogrd(+)");
                query.Append("   And c.Idempresa(+) = e.Idempresa");
                query.Append("   And c.Codintnf(+) = n.codintnf");
                query.Append("   And c.codmaterial(+) = m.codigomatavulso");
                query.Append("   And c.referencia(+) = To_number(to_char(n.datahoraentsai, 'yyyymm'))");
                query.Append("   And to_char(n.datahoraentsai,'yyyymm') = '" + referencia + "'");
                //query.Append("   And n.codintnf = 369026");
                query.Append("   And e.Idempresa = " + idEmpresa);
                query.Append("   And uc.idusuario(+) = c.idusuario");
                query.Append("   And uv.idusuario(+) = c.Idusuariovalidado");
                query.Append("   And cm.nroplano(+) = m.nroplano");
                query.Append("   And cm.codcontactb(+) = m.codcontactb");
                query.Append("   And cn.nroplano(+) = i.nroplano");
                query.Append("   And cn.codcontactb(+) = i.codcontactb");
                query.Append("   And i.nroplano = " + plano);

                query.Append(" Union all "); // ESF ISS
                query.Append("Select n.Codintnf, To_Number(n.documentoini) Numeronf, 0 Coddoctoesf, n.codissint");
                query.Append("     , n.Codtpdoc, Trunc(n.entrada) Entrada, f.Nrforn || ' - ' || f.Rsocialforn Fornecedor");
                query.Append("     , 0 Codigomatavulso, t.codtpdespesa || ' - ' || t.desctpdespesa Material");
                query.Append("     , i.vlunit * i.qtdiss vlrtotal, g.Codigogrd, g.Codigogrd || ' - ' || g.Descricaogrd Grupo");
                query.Append("     , t.Codtpdespesa Tpdespesamaterial, i.Codtpdespesa Tpdespesanota, tc.Codcontactb Contamaterial");
                query.Append("     , i.Codcontactb Contanota, CodCusto Codcustoctb, d.Obsdoctocpg, d.Coddoctocpg, c.Conferido, c.Idusuario");
                query.Append("     , c.Validado, c.Idusuariovalidado, To_Number(To_Char(n.entrada, 'yyyymm')) Referencia");
                query.Append("     , e.Idempresa, c.Id, c.Dataconferido, c.Datavalidado");
                query.Append("     , Substr(Uc.Nome, 1, Instr(Uc.Nome, ' ') - 1) || Substr(Uc.Nome, Instr(Uc.Nome, ' ', -1)) Conferente");
                query.Append("     , Substr(Uv.Nome, 1, Instr(Uv.Nome, ' ') - 1) || Substr(Uv.Nome, Instr(Uv.Nome, ' ', -1)) Validador");
                query.Append("     , Cm.Nomeconta Nomecontamaterial, Cn.Nomeconta Nomecontanota, n.Codlanca, i.Nroplano, 'ESF' Origem, e.Codigoglobus");
                query.Append("  From Esfiss_Entra n, Bgm_Fornecedor f, Cpgdocto d, Niff_Chm_Empresas e, esfitemissentra i");
                query.Append("     , Niff_Ctb_Conferencianotas c, Niff_Chm_Usuarios Uc, Niff_Chm_Usuarios Uv, Cpgtpdes t");
                query.Append("     , cpgtpdes_ctbconta tc, Ctbconta Cn, Ctbconta Cm, Est_Grupodespesas g, niff_ctb_paramnotas p");
                query.Append(" Where f.Codigoforn = n.Codigoforn");
                query.Append("   And n.coddoctocpg = d.coddoctocpg(+)");
                query.Append("   And Lpad(n.Codigoempresa, 3, '0') || '/' || Lpad(n.Codigofl, 3, '0') = e.Codigoglobus");
                query.Append("   And n.codissint = i.codissint");
                query.Append("   And p.codgrupodespesa = g.Codigogrd(+)");
                query.Append("   And c.Idempresa(+) = e.Idempresa");
                query.Append("   And c.codissint(+) = n.codissint");
                query.Append("   And c.Referencia(+) = To_Number(To_Char(n.entrada, 'yyyymm'))");
                query.Append("   And To_Char(n.entrada, 'yyyymm') = '" + referencia + "'");
                query.Append("   And e.Idempresa = " + idEmpresa);
                query.Append("   And Uc.Idusuario(+) = c.Idusuario");
                query.Append("   And Uv.Idusuario(+) = c.Idusuariovalidado");
                query.Append("   And Cm.Nroplano(+) = tc.Nroplano");
                query.Append("   And Cm.Codcontactb(+) = tc.Codcontactb");
                query.Append("   And Cn.Nroplano(+) = i.Nroplano");
                query.Append("   And Cn.Codcontactb(+) = i.Codcontactb");
                query.Append("   And i.Nroplano = " + plano);
                query.Append("   And t.codtpdespesa = i.codtpdespesa");
                query.Append("   And t.codtpdespesa = tc.codtpdespesa");
                query.Append("   And tc.nroplano = i.nroplano");
                query.Append("   And p.codtpdespesa = i.codtpdespesa(+)");
                query.Append("   And p.nroplano = i.nroplano");
                query.Append("   And n.sistema = 'ESF'");
                query.Append("   And(n.observacoes is null or n.observacoes not like '%Integrado pela NIFF%')");


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ConferenciaNotasPelaContabilidade.ItensConferencia _tipo = new ConferenciaNotasPelaContabilidade.ItensConferencia();
                        _tipo.Existe = true;

                        try
                        {
                            _tipo.CodIntNF = Convert.ToDecimal(dataReader["codintnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CodDoctoESF = Convert.ToDecimal(dataReader["coddoctoesf"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodISSInt = Convert.ToDecimal(dataReader["codissint"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodLanca = Convert.ToDecimal(dataReader["codlanca"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Plano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.CodDoctoCPG = Convert.ToDecimal(dataReader["CodDoctoCPG"].ToString());
                        }
                        catch { }

                        _tipo.Fornecedor = dataReader["Fornecedor"].ToString();
                        _tipo.NumeroNF = Convert.ToDecimal(dataReader["NumeroNF"].ToString());
                        _tipo.Entrada = Convert.ToDateTime(dataReader["Entrada"].ToString());
                        _tipo.CodTipoDocto = dataReader["CodTpDoc"].ToString();
                        _tipo.ObservacaoCPG = dataReader["obsdoctocpg"].ToString();
                        _tipo.ObservacaoCPGOriginal = dataReader["obsdoctocpg"].ToString();

                        _tipo.UsuarioConferido = dataReader["Conferente"].ToString();
                        _tipo.UsuarioValidador = dataReader["validador"].ToString();

                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        _tipo.Existe = _tipo.Id != 0;

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Referencia = Convert.ToInt32(dataReader["Referencia"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.IdUsuarioConferido = Convert.ToInt32(dataReader["Idusuario"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuarioValidador = Convert.ToInt32(dataReader["Idusuariovalidado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataConferido = Convert.ToDateTime(dataReader["Dataconferido"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataValidado = Convert.ToDateTime(dataReader["Datavalidado"].ToString());
                        }
                        catch { }

                        _tipo.CodMaterial = Convert.ToDecimal(dataReader["Codigomatavulso"].ToString());

                        _tipo.Material = dataReader["Material"].ToString();
                        _tipo.GrupoDespesa = dataReader["Grupo"].ToString();
                        try
                        {
                            _tipo.CodGrupo = Convert.ToInt32(dataReader["codigogrd"].ToString());
                        }
                        catch { }

                        _tipo.TipoDespesaItem = dataReader["TpDespesaMaterial"].ToString();
                        _tipo.TipoDespesaNota = dataReader["TpDespesaNota"].ToString();
                        
                        _tipo.ContaContabilItem = dataReader["ContaMaterial"].ToString();
                        _tipo.ContaContabilNota = dataReader["ContaNota"].ToString();

                        if (_tipo.ContaContabilItem != "")
                            _tipo.NomeContaContabilItem = _tipo.ContaContabilItem + " - " + dataReader["NomeContaMaterial"].ToString();
                        else
                        {
                            Financeiro.ContasContabeisDespesasReceitaGlobus _contaTpDesp = new FinanceiroDAO().Consultar(_tipo.TipoDespesaItem,"D", plano);
                            _tipo.ContaContabilItem = _contaTpDesp.ContaContabil.ToString();
                            if (_tipo.ContaContabilItem != "0")
                                _tipo.NomeContaContabilItem = _tipo.ContaContabilItem + " - " + _contaTpDesp.NomeConta;
                        }

                        if (_tipo.ContaContabilNota != "")
                            _tipo.NomeContaContabilNota = _tipo.ContaContabilNota + " - " + dataReader["NomeContaNota"].ToString();

                        _tipo.CentroCusto = dataReader["codcustoctb"].ToString();

                        _tipo.Conferido = dataReader["Conferido"].ToString() == "S";
                        _tipo.Validado = dataReader["validado"].ToString() == "S";

                        _tipo.Valido1 = _tipo.Valido1 || _tipo.TipoDespesaItem == _tipo.TipoDespesaNota;
                        _tipo.Valido3 = _tipo.Valido3 || _tipo.ContaContabilNota == _tipo.ContaContabilItem;

                        foreach (var item in listaParametros.Where(w => w.CodigoGrupo == _tipo.CodGrupo))
                        {
                            _tipo.Valido2 = _tipo.Valido2 || item.CodigoTipo == _tipo.TipoDespesaNota;
                        }

                        codcontaCtb = "";
                        nomeContaCtb = "";

                        BuscaContaDoLancamentoContabil(_tipo.CodLanca, _tipo.ContaContabilNota, _tipo.Plano);

                        _tipo.ContaContabilCTB = codcontaCtb;
                        _tipo.NomeContaContabilCTB = nomeContaCtb;
                        _tipo.Conferido = conferido;
                        _tipo.Valido4 = _tipo.Valido4 || _tipo.ContaContabilNota == _tipo.ContaContabilCTB;
                        _tipo.ValidadoOriginal = true;

                        if (!_tipo.Validado)
                        {
                            _tipoValidado = BuscaValidado(_tipo.CodMaterial);
                            if (_tipoValidado.Existe)
                            {
                                _tipo.Validado = true;
                                _tipo.ValidadoOriginal = false;
                                _tipo.DataValidado = _tipoValidado.DataValidado;
                                _tipo.IdUsuarioValidador = _tipoValidado.IdUsuarioValidador;
                                _tipo.UsuarioValidador = _tipoValidado.UsuarioValidador;
                                _tipo.CodigoGlobus = _tipoValidado.CodigoGlobus;
                                _tipo.ReferenciaValidado = _tipoValidado.ReferenciaValidado;
                            }
                        }
                        else
                        {
                            _tipo.CodigoGlobus = dataReader["CodigoGlobus"].ToString();
                            _tipo.ReferenciaValidado = _tipo.Referencia;
                        }

                        if (_tipo.CodDoctoESF != 0)
                        {
                            query.Clear();
                            query.Append("Select e.docconciliado");
                            query.Append("  from esfentra e");
                            query.Append(" where  e.coddoctoesf = " + _tipo.CodDoctoESF );

                            executar = sessao.CreateQuery(query.ToString());
                            dataReader2 = executar.ExecuteQuery();

                            using (dataReader2)
                            {
                                if (dataReader2.Read())
                                {
                                    _tipo.ConferidoESF = dataReader2["docconciliado"].ToString() == "S";
                                }
                            }
                        }

                        if (!_tipo.Conferido && _tipo.Validado) // chamado 201909-0222 jul/2020
                            _tipo.Conferido = true;

                        // armazena apenas as notas não conferidas, quando marcado para trazer apenas as nao conferidas. Se não marcado traz todas
                        if ((apenasNaoConferidas && !conferido) || !apenasNaoConferidas)
                            lista.Add(_tipo);
                    }
                }

                foreach (var item in lista.Where(w => w.ContaContabilCTB == "")
                                          .GroupBy(g => new { g.CodIntNF, g.ContaContabilNota, g.Plano, g.CodLanca, g.ContaContabilCTB, g.NomeContaContabilCTB, g.Conferido, g.CodDoctoESF, g.CodISSInt }))
                {
                    decimal valor = lista.Where(w => w.CodIntNF == item.Key.CodIntNF &&
                                                           w.CodLanca == item.Key.CodLanca &&
                                                           w.CodDoctoESF == item.Key.CodDoctoESF &&
                                                           w.CodISSInt == item.Key.CodISSInt &&
                                                           w.ContaContabilNota == item.Key.ContaContabilNota &&
                                                           w.Plano == item.Key.Plano)
                                                .Sum(s => s.Valor);

                    BuscaContaDoLancamentoContabil(item.Key.CodLanca, item.Key.Plano, valor);

                    foreach (var itemA in lista.Where(w => w.CodIntNF == item.Key.CodIntNF &&
                                                           w.CodLanca == item.Key.CodLanca &&
                                                           w.CodDoctoESF == item.Key.CodDoctoESF &&
                                                           w.CodISSInt == item.Key.CodISSInt &&
                                                           w.ContaContabilNota == item.Key.ContaContabilNota &&
                                                           w.Plano == item.Key.Plano))
                    {
                        itemA.ContaContabilCTB = codcontaCtb;
                        itemA.NomeContaContabilCTB = nomeContaCtb;
                        itemA.Conferido = conferido;
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
            return lista;
        }

        public bool Gravar(List<ConferenciaNotasPelaContabilidade.ItensConferencia> _lista, List<ConferenciaNotasPelaContabilidade.Conferencia> _notas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = _lista.Count() == 0;

            try
            {
                foreach (var item in _notas)
                {
                    foreach (var itemI in _lista.Where(w => w.CodDoctoESF == item.CodDoctoESF && w.CodIntNF == item.CodIntNF && w.CodISSInt == item.CodISSInt))
                        itemI.ObservacaoCPG = item.ObservacaoCPG;
                }

                foreach (var item in _lista)
                {
                    query.Clear();
                    if (!item.Existe)
                    {
                        query.Append("Insert into Niff_Ctb_Conferencianotas");
                        query.Append(" ( id, idempresa, referencia, codintnf, coddoctoesf, codmaterial, conferido");
                        query.Append(" , idusuario, dataconferido, validado, idusuariovalidado, datavalidado, CodISSInt");
                        query.Append(") Values ( (Select nvl(Max(id),0) +1 from Niff_Ctb_Conferencianotas) ");
                        query.Append("        , " + item.IdEmpresa);
                        query.Append("        , " + item.Referencia);
                        query.Append("        , " + item.CodIntNF);
                        query.Append("        , " + item.CodDoctoESF);
                        query.Append("        , " + item.CodMaterial);
                        query.Append("        , '" + (item.Conferido ? "S" : "N") + "'");
                        if (item.Conferido)
                        {
                            query.Append("        , " + item.IdUsuarioConferido);
                            query.Append("        , sysdate ");
                        }
                        else
                        {
                            query.Append("        , null, null");
                        }
                        
                        query.Append("        , '" + (item.Validado ? "S" : "N") + "'");

                        if (item.Validado)
                        {
                            query.Append("        , " + item.IdUsuarioValidador);
                            query.Append("        , sysdate ");
                        }
                        else
                            query.Append("        , null, null");

                        query.Append("      , " + item.CodISSInt);
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update Niff_Ctb_Conferencianotas");
                        query.Append("   set conferido = '" + (item.Conferido ? "S" : "N") + "'");

                        if (item.Conferido && item.DataConferido == DateTime.MinValue)
                        {
                            query.Append("     , idusuario = " + item.IdUsuarioConferido);
                            query.Append("    , dataconferido = sysdate ");
                        }
                        else
                        {
                            if (!item.Conferido)
                            {
                                query.Append("    , dataconferido = null");
                                query.Append("     , idusuario = null");
                            }
                        }
                        query.Append("     , validado = '" + (item.Validado ? "S" : "N") + "'");

                        if (item.Validado && item.DataValidado == DateTime.MinValue)
                        {
                            query.Append("   , datavalidado = sysdate ");
                            query.Append("     , idusuariovalidado = " + item.IdUsuarioValidador);
                        }
                        else
                        {
                            if (!item.Validado)
                            {
                                query.Append("     , idusuariovalidado = null");
                                query.Append("   , datavalidado = null");
                            }
                        }
                        query.Append(" where id = " + item.Id);
                    }
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }

                if (retorno)
                {
                    // Atualiza na contabilidade
                    foreach (var item in _lista.GroupBy(g => new { g.CodLanca, g.ContaContabilCTB, g.Conferido, g.Plano, g.ObservacaoCPG, g.ObservacaoCPGOriginal}))
                    {

                        query.Clear();
                        query.Append("Update ctbitlnc");
                        query.Append("   set itemConferido = '" + (item.Key.Conferido ? "S" : "N") + "'");
                        if (item.Key.ObservacaoCPG != item.Key.ObservacaoCPGOriginal)
                            query.Append("     , HISTORICOITEMLANCA = HISTORICOITEMLANCA || ' ' || '" + item.Key.ObservacaoCPG + "'");
                        query.Append(" where codlanca = " + item.Key.CodLanca);
                        query.Append("   and nroplano = " + item.Key.Plano);
                        query.Append("   and CodContaCtb = " + item.Key.ContaContabilCTB);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (!retorno)
                          break;
                    }

                    // Atualiza no CPG a observação
                    foreach (var item in _lista.GroupBy(g => new { g.CodDoctoCPG, g.ObservacaoCPG, g.ObservacaoCPGOriginal }))
                    {
                        if (item.Key.ObservacaoCPG != item.Key.ObservacaoCPGOriginal)
                        {
                            query.Clear();
                            query.Append("Update CPGDocto");
                            query.Append("   set OBSDOCTOCPG = '" + item.Key.ObservacaoCPG + "'");
                            query.Append(" where CodDoctoCPG = " + item.Key.CodDoctoCPG);

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

        #endregion
    }
}
