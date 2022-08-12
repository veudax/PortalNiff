using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Dados
{
    public class ConciliacaoContabilDAO
    {
        public class Ativo
        {
            IDataReader dataReader;
            IDataReader dataReaderAux;
            string campo = @"\1.\2.\3.\4";

            public List<ConciliacaoContabil.Ativo.Resumo> Listar(int idEmpresa, string empresa, string referenciaIni, string referenciaAtual,
                                                                 DateTime inicio, DateTime fim, int plano, bool naoConfirmados, bool consolidar)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Ativo.Resumo> lista = new List<ConciliacaoContabil.Ativo.Resumo>();

                Publicas.mensagemDeErro = string.Empty;
                
                try
                {   //ANALISANDO
                    // valores no ativo
                    query.Append("Select DISTINCT Sum(aquisicao) aquisicao, Sum(Correcao) Correcao");
                    query.Append("     , Sum(Baixa) Baixa, Sum(Depreciacao) Depreciacao");
                    query.Append("     , Sum(DepreciacaoAcumulada) DepreciacaoAcumulada");
                    query.Append("     , Sum(Correcao) - Sum(DepreciacaoAcumulada) SaldoATF");
                    query.Append("     , decode(g.CodigoGrupo, Null, ContaItem, CodigoGrupo || ' G') grupo");
                    query.Append("     , decode(g.CodigoGrupo, Null, m.Descricao, g.Descricao) Descricao");
                    query.Append("     , c.Id, c.idempresa, c.idusuario, c.referencia, c.textoexplicativo, c.confirmado");
                    query.Append("  From niff_ctb_conciliacaoatf c");
                    query.Append("     , Niff_Chm_Empresas e");
                    query.Append("     , (Select a.codigogrupo, a.descricao");
                    query.Append("             , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("          From niff_ctb_contasativo a, niff_chm_empresas e, Atfitem i");
                    query.Append("         Where e.Idempresa = a.Idempresaativo");
                    query.Append("           And i.codigo = a.codigoativo");
                    query.Append("           And e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')");
                    if (!consolidar)
                        query.Append("           And e.idempresa = " + idEmpresa + ") g");
                    else
                    {
                        if (idEmpresa == 2 || idEmpresa == 3)
                            query.Append("           And (e.idempresa = 2 or e.idempresa = 3)) g");
                        else
                        {
                            if (idEmpresa == 10 || idEmpresa == 11)
                                query.Append("           And (e.idempresa = 10 or e.idempresa = 11)) g");
                            else
                                query.Append("           And e.idempresa = " + idEmpresa + ") g");
                        }
                    }

                    query.Append("     , (Select sum(i.aquisvalor) aquisicao, 0 correcao, Sum(m.depracumuladaemvlr) Baixa");
                    query.Append("             , '" + referenciaAtual + "' referencia");
                    query.Append("             , 0 Depreciacao, 0 DepreciacaoAcumulada");
                    query.Append("             , regexp_replace(LPAD(Substr(i.conta, 1, length(i.conta) - 4) || '0000', 10), '([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})', '" + campo + "')  contaItem");
                    query.Append("             , i.descricao");
                    query.Append("          From atfmovto m, atfitem i");
                    query.Append("         Where m.Data between To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("           And To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("           And i.codigoempresa = m.codigoempresa");
                    query.Append("           And i.codigofl = m.codigofl");
                    query.Append("           And i.codigo = m.codigo");
                    query.Append("           And m.tipo = 'BT'");

                    if (!consolidar)
                        query.Append("           And lpad(m.codigoempresa, 3, '0') || '/' || lpad(m.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(m.codigoempresa, 3, '0') = '" + empresa.Substring(0,3) + "'");

                    query.Append("           Group By Substr(i.conta,1,length(i.conta)-4), i.descricao");

                    query.Append("        Union All ");

                    query.Append("        Select sum(i.aquisvalor) aquisicao, Sum(m.valorcorrmesatu) correcao, 0 Baixa");
                    query.Append("             , '" + referenciaAtual + "' referencia");
                    query.Append("             , Sum(m.deprnomesemvlr) Depreciacao, Sum(m.depracumuladaemvlr) DepreciacaoAcumulada");
                    query.Append("             , regexp_replace(LPAD(Substr(i.conta, 1, length(i.conta) - 4) || '0000', 10), '([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})', '" + campo + "')  contaItem");
                    query.Append("             , i.descricao");
                    query.Append("          From atfmovto m, atfitem i");
                    query.Append("         Where m.Data between To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("           And To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("           And i.codigoempresa = m.codigoempresa");
                    query.Append("           And i.codigofl = m.codigofl");
                    query.Append("           And i.codigo = m.codigo");
                    query.Append("           And m.tipo = 'DP'");

                    if (!consolidar)
                        query.Append("           And lpad(m.codigoempresa, 3, '0') || '/' || lpad(m.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(m.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("           Group By Substr(i.conta,1,length(i.conta)-4), i.descricao");

                    query.Append("        Union All ");

                    query.Append("        Select sum(i.aquisvalor) aquisicao, Sum(i.aquisvalor) correcao, 0 Baixa");
                    query.Append("             , '" + referenciaAtual + "' referencia");
                    query.Append("             , 0 depreciacao, 0 DepreciacaoAcumulada");
                    query.Append("             , regexp_replace(LPAD(Substr(i.conta, 1, length(i.conta) - 4) || '0000', 10), '([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})', '" + campo + "')  contaItem");
                    query.Append("             , i.descricao");
                    query.Append("          From atfitem i");
                    query.Append("         Where i.iniciodeprec Is Null");                    
                    query.Append("           And i.aquisvalor > 0");
                    query.Append("           And i.taxadeprec = 0");
                    query.Append("           And i.databaixa Is Null");
                    if (!consolidar)
                        query.Append("           And lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(i.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("           Group By Substr(i.conta,1,length(i.conta)-4), i.descricao");
                    
                    query.Append("     ) M");
                    query.Append(" Where m.contaItem = g.Conta(+)");
                    query.Append("   And c.IdEmpresa(+) = e.Idempresa");
                    query.Append("   And c.Referencia(+) = m.Referencia");
                    query.Append("   And c.conta(+) = Decode(g.Codigogrupo, Null, Contaitem, Codigogrupo)");
                    
                    // não consolidar aqui, pois duplica
                    query.Append("           And e.idempresa = " + idEmpresa );
                    

                    query.Append(" Group By decode(g.CodigoGrupo, Null, ContaItem, CodigoGrupo || ' G')");
                    query.Append("     , decode(g.CodigoGrupo, Null, m.Descricao, g.Descricao)");
                    query.Append("     , c.Id, c.idempresa, c.idusuario, c.referencia, c.textoexplicativo, c.confirmado");

                    if (naoConfirmados)
                        query.Append(" Having (c.confirmado = 'N' Or c.confirmado Is Null)");

                    query.Append(" Order By grupo Desc ");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            if (lista.Where(w => w.ContaItem.Contains(dataReader["Grupo"].ToString())).Count() != 0)
                                continue;

                            ConciliacaoContabil.Ativo.Resumo _tipo = new ConciliacaoContabil.Ativo.Resumo();

                            try
                            {
                                _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Referencia = Convert.ToInt32(dataReader["Referencia"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Explicacao = dataReader["textoexplicativo"].ToString();
                            }
                            catch { }

                            try
                            {
                                _tipo.Conferido = dataReader["confirmado"].ToString() == "S";
                            }
                            catch { }

                            _tipo.Existe = _tipo.Id != 0;

                            _tipo.Correcao = Convert.ToDecimal(dataReader["Correcao"].ToString());
                            _tipo.DepreciacaoAcumulada = Convert.ToDecimal(dataReader["DepreciacaoAcumulada"].ToString());
                            _tipo.SaldoATF = _tipo.Correcao - _tipo.DepreciacaoAcumulada;

                            _tipo.DescricaoConta = dataReader["Descricao"].ToString();

                            _tipo.ContaItem = dataReader["Grupo"].ToString();
                            _tipo.Grupo = dataReader["Grupo"].ToString();

                            //armadilha Grupo
                            if (_tipo.Grupo == "01.01.00.0000 G")
                            {
                                Console.WriteLine("teste");
                            }

                            // valores na contabilidade
                            query.Clear();
                            //ANALISANDO
                            query.Append("Select Sum(SaldoFin) SaldoFin, Grupo");
                            query.Append("  from (Select Distinct saldoFin, grupo");
                            query.Append("          from (");
                            query.Append("                Select sum(saldoini) + Sum(resultado) saldofin");
                            query.Append("                     , decode(g.CodigoGrupo, Null, m.Conta, CodigoGrupo || ' G') grupo");
                            query.Append("                  From (Select a.codigogrupo");
                            query.Append("                             , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                            query.Append("                          From niff_ctb_contasativo a, niff_chm_empresas e, Atfitem i");
                            query.Append("                         Where e.Idempresa = a.Idempresaativo");
                            query.Append("                           And i.codigo = a.codigoativo");
                            query.Append("                           And e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')");
                            if (!consolidar)
                                query.Append("           And e.idempresa = " + idEmpresa + ") g");
                            else
                            {
                                if (idEmpresa == 2 || idEmpresa == 3)
                                    query.Append("           And (e.idempresa = 2 or e.idempresa = 3)) g");
                                else
                                {
                                    if (idEmpresa == 10 || idEmpresa == 11)
                                        query.Append("           And (e.idempresa = 10 or e.idempresa = 11)) g");
                                    else
                                        query.Append("           And e.idempresa = " + idEmpresa + ") g");
                                }
                            }
                            query.Append("                     , (Select Sum(Vldebantsaldo) - Sum(Vlcredantsaldo) Saldoini");
                            query.Append("                             , 0 Resultado, 0 Debito, 0 Credito");
                            query.Append("                             , Conta, Classificador, Codcontactb ");
                            query.Append("                          From (Select Distinct s.Vldebantsaldo, s.Vlcredantsaldo");
                            query.Append("                                     , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                            query.Append("                                     , cc.classificador, cc.codcontactb, s.periodosaldo");
                            query.Append("                                  From Atfitem i, atfitem_contactb c, ctbsaldo s, ctbconta cc");
                            query.Append("                                 Where c.codigoempresa = i.codigoempresa");
                            query.Append("                                   And c.codigofl = i.codigofl");
                            query.Append("                                   And c.codigo = i.codigo");
                            query.Append("                                   And c.nroplano = " + plano);
                            query.Append("                                   And s.nroplano = c.nroplano");
                            query.Append("                                   And s.codcontactb = c.codcontactb_creditobaixa");
                            query.Append("                                   And s.codigoempresa = c.codigoempresa");
                            query.Append("                                   And s.periodosaldo Between '" + referenciaIni + "' And '" + referenciaAtual + "'");

                            if (!consolidar)
                                query.Append("           And lpad(s.codigoempresa, 3, '0') || '/' || lpad(s.codigoFl, 3, '0') = '" + empresa + "'");
                            else
                                query.Append("           And lpad(s.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                            query.Append("                                   And cc.codcontactb = s.codcontactb");
                            query.Append("                                   And cc.nroplano = s.nroplano");
                            query.Append("                                   And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'");
                            query.Append("                       ) Group By conta, classificador, codcontactb ");
                                                          
                            query.Append("                          Union All ");

                            query.Append("                         Select 0 Saldoini");
                            query.Append("                              , Sum(vldebitosaldo) - Sum(vlcreditosaldo) resultado");
                            query.Append("                              , Sum(vldebitosaldo) dedito, Sum(vlcreditosaldo) credito");
                            query.Append("                              , Conta, Classificador, Codcontactb ");
                            query.Append("                           From (Select Distinct s.vldebitosaldo, s.vlcreditosaldo");
                            query.Append("                                      , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                            query.Append("                                      , cc.classificador, cc.codcontactb, s.periodosaldo");
                            query.Append("                                   From Atfitem i, atfitem_contactb c, ctbsaldo s, ctbconta cc");
                            query.Append("                                  Where c.codigoempresa = i.codigoempresa");
                            query.Append("                                    And c.codigofl = i.codigofl");
                            query.Append("                                    And c.codigo = i.codigo");
                            query.Append("                                    And c.nroplano = " + plano);
                            query.Append("                                    And s.nroplano = c.nroplano");
                            query.Append("                                    And s.codcontactb = c.codcontactb_creditobaixa");
                            query.Append("                                    And s.codigoempresa = c.codigoempresa");
                            query.Append("                                    And s.periodosaldo = '" + referenciaAtual + "'");

                            if (!consolidar)
                                query.Append("           And lpad(s.codigoempresa, 3, '0') || '/' || lpad(s.codigoFl, 3, '0') = '" + empresa + "'");
                            else
                                query.Append("           And lpad(s.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                            query.Append("                                    And cc.codcontactb = s.codcontactb");
                            query.Append("                                    And cc.nroplano = s.nroplano");
                            query.Append("                                    And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'");
                                                                   
                            query.Append("                          ) Group By conta, classificador, codcontactb            ");
                                                                   
                            query.Append("                            Union All ");

                            query.Append("                           Select Sum(Vldebantsaldo) - Sum(Vlcredantsaldo) Saldoini");
                            query.Append("                                , 0 Resultado, 0 Debito, 0 Credito");
                            query.Append("                                , Conta, Classificador, Codcontactb ");
                            query.Append("                             From (Select Distinct s.Vldebantsaldo, s.Vlcredantsaldo");
                            query.Append("                                        , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                            query.Append("                                        , cc.classificador, cc.codcontactb, s.periodosaldo");
                            query.Append("                                     From Atfitem i, atfitem_contactb c, ctbsaldo s, ctbconta cc");
                            query.Append("                                    Where c.codigoempresa = i.codigoempresa");
                            query.Append("                                      And c.codigofl = i.codigofl");
                            query.Append("                                      And c.codigo = i.codigo");
                            query.Append("                                      And c.nroplano = " + plano);
                            query.Append("                                      And s.nroplano = c.nroplano");
                            query.Append("                                      And s.codcontactb = c.contactbdpc");
                            query.Append("                                      And s.codigoempresa = c.codigoempresa");
                            query.Append("                                      And s.periodosaldo Between '" + referenciaIni + "' And '" + referenciaAtual + "'");

                            if (!consolidar)
                                query.Append("           And lpad(s.codigoempresa, 3, '0') || '/' || lpad(s.codigoFl, 3, '0') = '" + empresa + "'");
                            else
                                query.Append("           And lpad(s.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                            query.Append("                                      And cc.codcontactb = s.codcontactb");
                            query.Append("                                      And cc.nroplano = s.nroplano");
                            query.Append("                                      And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'");
                            query.Append("                          ) Group By conta, classificador, codcontactb ");
                                                          
                            query.Append("                            Union All ");

                            query.Append("                           Select 0 Saldoini");
                            query.Append("                                , Sum(vldebitosaldo) - Sum(vlcreditosaldo) resultado");
                            query.Append("                                , Sum(vldebitosaldo) dedito, Sum(vlcreditosaldo) credito");
                            query.Append("                                , Conta, Classificador, Codcontactb ");
                            query.Append("                             From (Select Distinct s.vldebitosaldo, s.vlcreditosaldo");
                            query.Append("                                        , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                            query.Append("                                        , cc.classificador, cc.codcontactb, s.periodosaldo");
                            query.Append("                                     From Atfitem i, atfitem_contactb c, ctbsaldo s, ctbconta cc");
                            query.Append("                                    Where c.codigoempresa = i.codigoempresa");
                            query.Append("                                      And c.codigofl = i.codigofl");
                            query.Append("                                      And c.codigo = i.codigo");
                            query.Append("                                      And c.nroplano = " + plano);
                            query.Append("                                      And s.nroplano = c.nroplano");
                            query.Append("                                      And s.codcontactb = c.contactbdpc");
                            query.Append("                                      And s.codigoempresa = c.codigoempresa");
                            query.Append("                                      And s.periodosaldo = '" + referenciaAtual + "'");
                                                          
                            if (!consolidar)
                                query.Append("           And lpad(s.codigoempresa, 3, '0') || '/' || lpad(s.codigoFl, 3, '0') = '" + empresa + "'");
                            else
                                query.Append("           And lpad(s.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                            query.Append("                                      And cc.codcontactb = s.codcontactb");
                            query.Append("                                      And cc.nroplano = s.nroplano");
                            query.Append("                                      And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'");
                            query.Append("                        )  Group By conta, classificador, codcontactb            ");
                            query.Append("                     ) M ");
                            query.Append("                 Where m.conta = g.Conta(+) ");
                            query.Append("                 Group By decode(g.CodigoGrupo, Null, m.Conta, CodigoGrupo || ' G')");
                            query.Append("                     , classificador, m.conta");
                            query.Append("    ) )");

                            query.Append("  Where Grupo = '" + _tipo.ContaItem + "'");
                            query.Append("  Group By Grupo ");

                            executar = sessao.CreateQuery(query.ToString());

                            dataReaderAux = executar.ExecuteQuery();

                            using (dataReaderAux)
                            {
                                if (dataReaderAux.Read())
                                {
                                    try
                                    {
                                        _tipo.SaldoCTB = Convert.ToDecimal(dataReaderAux["saldofin"].ToString());
                                    }
                                    catch { }
                                }
                            }

                            _tipo.Diferenca = _tipo.SaldoCTB - _tipo.SaldoATF;
                            _tipo.Diferencas = _tipo.Diferenca != 0;
                            _tipo.ContaItem = _tipo.ContaItem.Replace(" G", "") + " - " + _tipo.DescricaoConta;


                            //Armadilha Grupo
                            if (_tipo.Grupo == "01.01.01.0000 G")
                            {
                                Console.WriteLine("TESTE");
                            }

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

            public List<ConciliacaoContabil.Ativo.DetalheATF> ListarDetalheATF(int idEmpresa, string empresa, string referenciaIni, string referenciaAtual,
                                                                 DateTime inicio, DateTime fim, int plano, bool consolidar)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Ativo.DetalheATF> lista = new List<ConciliacaoContabil.Ativo.DetalheATF>();

                Publicas.mensagemDeErro = string.Empty;
                
                try
                { //ANALISANDO
                    // valores no ativo
                    query.Append("Select Sum(aquisicao) aquisicao, Sum(Correcao) Correcao");
                    query.Append("     , Sum(Baixa) Baixa, Sum(Depreciacao) Depreciacao");
                    query.Append("     , Sum(DepreciacaoAcumulada) DepreciacaoAcumulada");
                    query.Append("     , Sum(Correcao) - Sum(DepreciacaoAcumulada) SaldoATF");
                    query.Append("     , decode(g.CodigoGrupo, Null, contaItem, CodigoGrupo || ' G') grupo");
                    query.Append("     , decode(g.CodigoGrupo, Null, m.Descricao, g.Descricao) Descricao");
                    query.Append("     , m.contaItem , m.Descricao nomeConta");
                    query.Append("     , regexp_replace(LPAD(m.conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("  From (Select a.codigogrupo, a.descricao");
                    query.Append("             , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("          From niff_ctb_contasativo a, niff_chm_empresas e, Atfitem i");
                    query.Append("         Where e.Idempresa = a.Idempresaativo");
                    query.Append("           And i.codigo = a.codigoativo");
                    query.Append("           And e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')");
                    if (!consolidar)
                        query.Append("           And e.idempresa = " + idEmpresa + ") g");
                    else
                    {
                        if (idEmpresa == 2 || idEmpresa == 3)
                            query.Append("           And (e.idempresa = 2 or e.idempresa = 3)) g");
                        else
                        {
                            if (idEmpresa == 10 || idEmpresa == 11)
                                query.Append("           And (e.idempresa = 10 or e.idempresa = 11)) g");
                            else
                                query.Append("           And e.idempresa = " + idEmpresa + ") g");
                        }
                    }
                    query.Append("     , (Select sum(i.aquisvalor) aquisicao, 0 correcao, SuM(m.valor) Baixa");
                    query.Append("             , Sum(m.depracumuladaemvlr) Depreciacao, 0 DepreciacaoAcumulada");
                    query.Append("             , regexp_replace(LPAD(Substr(i.conta, 1, length(i.conta) - 4) || '0000', 10), '([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})', '" + campo + "')  contaItem");
                    query.Append("             , i.descricao, i.conta");
                    query.Append("          From atfmovto m, atfitem i");
                    query.Append("         Where m.Data between To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("           And To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("           And i.codigoempresa = m.codigoempresa");
                    query.Append("           And i.codigofl = m.codigofl");
                    query.Append("           And i.codigo = m.codigo");
                    query.Append("           And m.tipo = 'BT'");

                    if (!consolidar)
                        query.Append("           And lpad(m.codigoempresa, 3, '0') || '/' || lpad(m.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(m.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("           Group By i.Conta, i.descricao");

                    query.Append("        Union All ");

                    query.Append("        Select sum(i.aquisvalor) aquisicao, Sum(m.valorcorrmesatu) correcao, 0 Baixa");
                    query.Append("             , Sum(m.deprnomesemvlr) Depreciacao, Sum(m.depracumuladaemvlr) DepreciacaoAcumulada");
                    query.Append("             , regexp_replace(LPAD(Substr(i.conta, 1, length(i.conta) - 4) || '0000', 10), '([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})', '" + campo + "')  contaItem");
                    query.Append("             , i.descricao, i.conta");
                    query.Append("          From atfmovto m, atfitem i");
                    query.Append("         Where m.Data between To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("           And To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("           And i.codigoempresa = m.codigoempresa");
                    query.Append("           And i.codigofl = m.codigofl");
                    query.Append("           And i.codigo = m.codigo");
                    query.Append("           And m.tipo = 'DP'");

                    if (!consolidar)
                        query.Append("           And lpad(m.codigoempresa, 3, '0') || '/' || lpad(m.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(m.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("           Group By i.Conta, i.descricao");

                    query.Append("        Union All ");

                    query.Append("        Select sum(i.aquisvalor) aquisicao, Sum(i.aquisvalor) correcao, 0 Baixa");
                    query.Append("             , 0 depreciacao, 0 DepreciacaoAcumulada");
                    query.Append("             , regexp_replace(LPAD(Substr(i.conta, 1, length(i.conta) - 4) || '0000', 10), '([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})', '" + campo + "')  contaItem");
                    query.Append("             , i.descricao, i.conta");
                    query.Append("          From atfitem i");
                    query.Append("         Where i.iniciodeprec Is Null");
                    query.Append("           And i.aquisvalor > 0");
                    query.Append("           And i.taxadeprec = 0");
                    query.Append("           And i.databaixa Is Null");

                    if (!consolidar)
                        query.Append("           And lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(i.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("           Group By i.Conta, i.descricao");

                    query.Append("     ) M");
                    query.Append(" Where m.contaItem = g.Conta(+)");
                    query.Append(" Group By decode(g.CodigoGrupo, Null, contaItem, CodigoGrupo || ' G')");
                    query.Append("     , decode(g.CodigoGrupo, Null, m.Descricao, g.Descricao), contaItem");
                    query.Append("     , m.conta, m.Descricao");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Ativo.DetalheATF _tipo = new ConciliacaoContabil.Ativo.DetalheATF();

                            _tipo.Conta = dataReader["contaItem"].ToString();
                            _tipo.ContaItem = dataReader["Conta"].ToString() + " - " + dataReader["nomeConta"].ToString();
                            _tipo.Grupo = dataReader["Grupo"].ToString();
                            _tipo.Aquisicao = Convert.ToDecimal(dataReader["Aquisicao"].ToString());
                            _tipo.Correcao = Convert.ToDecimal(dataReader["Correcao"].ToString());
                            _tipo.Baixa = Convert.ToDecimal(dataReader["Baixa"].ToString());
                            _tipo.Depreciacao = Convert.ToDecimal(dataReader["Depreciacao"].ToString());
                            _tipo.DepreciacaoAcumulada = Convert.ToDecimal(dataReader["DepreciacaoAcumulada"].ToString());
                            _tipo.SaldoATF = _tipo.Correcao - _tipo.DepreciacaoAcumulada;

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

            public List<ConciliacaoContabil.Ativo.DetalheCTB> ListarDetalheCTB(int idEmpresa, string empresa, string referenciaIni, string referenciaAtual,
                                                                int plano, bool consolidar)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Ativo.DetalheCTB> lista = new List<ConciliacaoContabil.Ativo.DetalheCTB>();

                Publicas.mensagemDeErro = string.Empty;
                
                try
                { 
                // valores na contabilidade
                    query.Clear();
                    query.Append("Select Distinct saldoFin, SaldoIni, Debito, Credito, grupo, Classificador");
                    query.Append("  from (");
                    query.Append("        Select sum(saldoini) + Sum(resultado) saldofin");
                    query.Append("             , sum(saldoini) SaldoIni, Sum(Debito) debito, Sum(Credito) Credito");

                    query.Append("             , decode(g.CodigoGrupo, Null, m.Conta, CodigoGrupo || ' G') grupo");
                    query.Append("             , m.conta, m.classificador");
                    query.Append("          From (Select a.codigogrupo");
                    query.Append("                     , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("                  From niff_ctb_contasativo a, niff_chm_empresas e, Atfitem i");
                    query.Append("                 Where e.Idempresa = a.Idempresaativo");
                    query.Append("                   And i.codigo = a.codigoativo");
                    query.Append("                   And e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')");
                    if (!consolidar)
                        query.Append("           And e.idempresa = " + idEmpresa + ") g");
                    else
                    {
                        if (idEmpresa == 2 || idEmpresa == 3)
                            query.Append("           And (e.idempresa = 2 or e.idempresa = 3)) g");
                        else
                        {
                            if (idEmpresa == 10 || idEmpresa == 11)
                                query.Append("           And (e.idempresa = 10 or e.idempresa = 11)) g");
                            else
                                query.Append("           And e.idempresa = " + idEmpresa + ") g");
                        }
                    }

                    query.Append("             , (Select Sum(Vldebantsaldo) - Sum(Vlcredantsaldo) Saldoini");
                    query.Append("                     , 0 Resultado, 0 Debito, 0 Credito");
                    query.Append("                     , Conta, Classificador, Codcontactb ");
                    query.Append("                  From (Select Distinct s.Vldebantsaldo, s.Vlcredantsaldo"); 
                    query.Append("                             , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("                             , cc.classificador, cc.codcontactb, s.periodosaldo");
                    query.Append("                          From Atfitem i, atfitem_contactb c, ctbsaldo s, ctbconta cc");
                    query.Append("                         Where c.codigoempresa = i.codigoempresa");
                    query.Append("                           And c.codigofl = i.codigofl");
                    query.Append("                           And c.codigo = i.codigo");
                    query.Append("                           And c.nroplano = " + plano);
                    query.Append("                           And s.nroplano = c.nroplano");
                    query.Append("                           And s.codcontactb = c.codcontactb_creditobaixa");
                    query.Append("                           And s.codigoempresa = c.codigoempresa");
                    query.Append("                           And s.periodosaldo Between '" + referenciaIni + "' And '" + referenciaAtual + "'");
                                                  
                    if (!consolidar)
                        query.Append("           And lpad(s.codigoempresa, 3, '0') || '/' || lpad(s.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(s.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                           And cc.codcontactb = s.codcontactb");
                    query.Append("                           And cc.nroplano = s.nroplano");
                    query.Append("                           And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'");

                    query.Append("                 )  Group By conta, classificador, codcontactb ");

                    query.Append("                Union All ");

                    query.Append("                Select 0 Saldoini");
                    query.Append("                     , Sum(vldebitosaldo) - Sum(vlcreditosaldo) resultado");
                    query.Append("                     , Sum(vldebitosaldo) dedito, Sum(vlcreditosaldo) credito");
                    query.Append("                     , Conta, Classificador, Codcontactb ");
                    query.Append("                  From (Select Distinct s.vldebitosaldo, s.vlcreditosaldo"); 
                    query.Append("                             , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("                             , cc.classificador, cc.codcontactb, s.periodosaldo");
                    query.Append("                          From Atfitem i, atfitem_contactb c, ctbsaldo s, ctbconta cc");
                    query.Append("                         Where c.codigoempresa = i.codigoempresa");
                    query.Append("                           And c.codigofl = i.codigofl");
                    query.Append("                           And c.codigo = i.codigo");
                    query.Append("                           And c.nroplano = " + plano);
                    query.Append("                           And s.nroplano = c.nroplano");
                    query.Append("                           And s.codcontactb = c.codcontactb_creditobaixa");
                    query.Append("                           And s.codigoempresa = c.codigoempresa");
                    query.Append("                           And s.periodosaldo = '" + referenciaAtual + "'");

                    if (!consolidar)
                        query.Append("           And lpad(s.codigoempresa, 3, '0') || '/' || lpad(s.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(s.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                           And cc.codcontactb = s.codcontactb");
                    query.Append("                           And cc.nroplano = s.nroplano");
                    query.Append("                           And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'");

                    query.Append("                      )  Group By conta, classificador, codcontactb            ");

                    query.Append("                Union All ");

                    query.Append("                Select Sum(Vldebantsaldo) - Sum(Vlcredantsaldo) Saldoini");
                    query.Append("                     , 0 Resultado, 0 Debito, 0 Credito");
                    query.Append("                     , Conta, Classificador, Codcontactb ");
                    query.Append("                  From (Select Distinct s.Vldebantsaldo, s.Vlcredantsaldo");
                    query.Append("                             , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("                             , cc.classificador, cc.codcontactb, s.periodosaldo");
                    query.Append("                          From Atfitem i, atfitem_contactb c, ctbsaldo s, ctbconta cc");
                    query.Append("                         Where c.codigoempresa = i.codigoempresa");
                    query.Append("                           And c.codigofl = i.codigofl");
                    query.Append("                           And c.codigo = i.codigo");
                    query.Append("                           And c.nroplano = " + plano);
                    query.Append("                           And s.nroplano = c.nroplano");
                    query.Append("                           And s.codcontactb = c.contactbdpc");
                    query.Append("                           And s.codigoempresa = c.codigoempresa");
                    query.Append("                           And s.periodosaldo Between '" + referenciaIni + "' And '" + referenciaAtual + "'");

                    if (!consolidar)
                        query.Append("           And lpad(s.codigoempresa, 3, '0') || '/' || lpad(s.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(s.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                          And cc.codcontactb = s.codcontactb");
                    query.Append("                          And cc.nroplano = s.nroplano");
                    query.Append("                          And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'");
                    query.Append("                      ) Group By conta, classificador, codcontactb ");

                    query.Append("                Union All ");

                    query.Append("                Select 0 Saldoini");
                    query.Append("                     , Sum(vldebitosaldo) - Sum(vlcreditosaldo) resultado");
                    query.Append("                     , Sum(vldebitosaldo) dedito, Sum(vlcreditosaldo) credito");
                    query.Append("                     , Conta, Classificador, Codcontactb ");
                    query.Append("                  From (Select Distinct s.vldebitosaldo, s.vlcreditosaldo");
                    query.Append("                             , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("                             , cc.classificador, cc.codcontactb, s.periodosaldo");
                    query.Append("                          From Atfitem i, atfitem_contactb c, ctbsaldo s, ctbconta cc");
                    query.Append("                         Where c.codigoempresa = i.codigoempresa");
                    query.Append("                           And c.codigofl = i.codigofl");
                    query.Append("                           And c.codigo = i.codigo");
                    query.Append("                           And c.nroplano = " + plano);
                    query.Append("                           And s.nroplano = c.nroplano");
                    query.Append("                           And s.codcontactb = c.contactbdpc");
                    query.Append("                           And s.codigoempresa = c.codigoempresa");
                    query.Append("                           And s.periodosaldo = '" + referenciaAtual + "'");
                                                     
                    if (!consolidar)
                        query.Append("           And lpad(s.codigoempresa, 3, '0') || '/' || lpad(s.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(s.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                           And cc.codcontactb = s.codcontactb");
                    query.Append("                           And cc.nroplano = s.nroplano");
                    query.Append("                           And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'");
                    query.Append("                     )    Group By conta, classificador, codcontactb            ");
                    query.Append("             ) M ");
                    query.Append("         Where m.conta = g.Conta(+) ");
                    query.Append("         Group By decode(g.CodigoGrupo, Null, m.Conta, CodigoGrupo || ' G')");
                    query.Append("             , classificador, m.conta");
                    query.Append("    ) ");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Ativo.DetalheCTB _tipo = new ConciliacaoContabil.Ativo.DetalheCTB();

                            _tipo.Grupo = dataReader["Grupo"].ToString();

                            //armadilha grupo
                            if (_tipo.Grupo == "01.01.00.0000 G")
                            {
                                Console.WriteLine("teste");
                            }
                            _tipo.Classificador = dataReader["Classificador"].ToString();

                            try
                            {
                                _tipo.SaldoCTB = Convert.ToDecimal(dataReader["saldofin"].ToString());
                                _tipo.SaldoIni = Convert.ToDecimal(dataReader["saldoini"].ToString());
                                _tipo.Debito = Convert.ToDecimal(dataReader["Debito"].ToString());
                                _tipo.Credito = Convert.ToDecimal(dataReader["Credito"].ToString());

                            }
                            catch { }
                            
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

            public List<ConciliacaoContabil.Ativo.DetalheESF> ListarDetalheESF(string empresa, DateTime dataIni, DateTime dataFim, bool consolidar)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Ativo.DetalheESF> lista = new List<ConciliacaoContabil.Ativo.DetalheESF>();

                Publicas.mensagemDeErro = string.Empty;

                try
                {
                    // Notas de compra
                    query.Clear();
                    query.Append("Select n.numeronf, n.serienf, n.dataemissaonf, n.entradasaidanf, n.valortotalnf totalNF, n.codclassfisc, n.codtpDoc");
                    query.Append("     , m.descricaomat material, i.qtdeitensnf qtde, 'EST' origem, 'Compra' Tipo, i.valortotalitensnf totalitem");
                    query.Append("  From Bgm_Notafiscal n, est_itensnf i, Est_Cadmaterial m");
                    query.Append(" Where n.dataemissaonf  Between To_Date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   And To_Date('" + dataFim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   And n.codclassfisc In(1551, 1406, 2551, 2406)");
                    query.Append("   And i.codintnf = n.codintnf");
                    query.Append("   And i.codclassfisc In(1551, 1406, 2551, 2406)");
                    query.Append("   And m.codigomatint = i.codigomatint");

                    if (!consolidar)
                        query.Append("           And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(n.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append(" Union All ");
                    query.Append("Select n.numeronf, n.serienf, n.dataemissaonf, n.entradasaidanf, n.valortotalnf TotalNF, n.codclassfisc, n.codtpDoc");
                    query.Append("     , ma.descricaomatavulso material, ia.qtdenfservico qtde, 'EST' origem, 'Compra' Tipo, ia.valornfservico totalitem");
                    query.Append("  From Bgm_Notafiscal n, Est_Nfservico ia, Est_Cadmaterialavulso ma");
                    query.Append(" Where n.dataemissaonf Between To_Date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   And To_Date('" + dataFim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("  And n.codclassfisc In(1551, 1406, 2551, 2406 )");
                    query.Append("  And ia.codintnf = n.codintnf");
                    query.Append("  And ia.codclassfisc In(1551, 1406, 2551, 2406 )");
                    query.Append("  And ma.codigomatavulso = ia.codigomatavulso");

                    if (!consolidar)
                        query.Append("           And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(n.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append(" Union All ");
                    query.Append("Select n.numeronf, n.serienf, n.Dataemissao dataemissaonf, n.datahoraentsai entradasaidanf, n.valortotalnf TotalNF, n.codclassfisc, n.codtpDoc");
                    query.Append("     , m.descricaomatavulso materia, i.qtde, 'ESF' Origem, 'Compra' Tipo,  i.vlrtotal totalitem");
                    query.Append("  From Esfnotafiscal n, Esfnotafiscal_Item i, Esfmatavulso m");
                    query.Append(" Where n.dataemissao Between To_Date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   And To_Date('" + dataFim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   And n.codclassfisc In(1551, 1406, 2551, 2406 )");
                    query.Append("   And n.codintnf = i.codintnf");
                    query.Append("   And m.codigomatavulso = i.codproduto");
                    query.Append("   And I.cfop In(1551, 1406, 2551, 2406 )");

                    if (!consolidar)
                        query.Append("           And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(n.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append(" Union All ");
                    query.Append("Select n.numeronf, n.serienf, n.dataemissaonf, n.entradasaidanf, n.valortotalnf TotalNf, n.codclassfisc, n.codtpDoc");
                    query.Append("     , m.descricaomat Materia, i.qtdeitensnf qtde , 'EST' origem, 'Venda' Tipo, i.valortotalitensnf totalitem");
                    query.Append("  From Bgm_Notafiscal n, est_itensnf i, Est_Cadmaterial m");
                    query.Append(" Where n.dataemissaonf  Between To_Date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   And To_Date('" + dataFim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   And n.codclassfisc In(5551, 6551 )");
                    query.Append("   And i.codintnf = n.codintnf");
                    query.Append("   And i.codclassfisc In(5551, 6551 )");
                    query.Append("   And m.codigomatint = i.codigomatint");

                    if (!consolidar)
                        query.Append("           And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(n.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append(" Union All ");
                    query.Append("Select n.numeronf, n.serienf, n.dataemissaonf, n.entradasaidanf, n.valortotalnf TotalNF, n.codclassfisc, n.codtpDoc");
                    query.Append("     , decode(Ma.Descricaomatavulso, Null, ia.descricaotipobem, Ma.Descricaomatavulso) Materia");
                    query.Append("     , ia.qtdenfservico qtde, 'EST' origem, 'Venda' Tipo, ia.valornfservico totalitem");
                    query.Append("  From Bgm_Notafiscal n, Est_Nfservico ia, Est_Cadmaterialavulso ma");
                    query.Append(" Where n.dataemissaonf Between To_Date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   And To_Date('" + dataFim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   And n.codclassfisc In(5551, 6551 )");
                    query.Append("   And ia.codintnf = n.codintnf");
                    query.Append("   And ia.codclassfisc In(5551, 6551  )");
                    query.Append("   And ma.codigomatavulso(+) = ia.codigomatavulso");

                    if (!consolidar)
                        query.Append("           And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(n.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append(" Union All ");
                    query.Append("Select n.numeronf, n.serienf, n.Dataemissao dataemissaonf, n.datahoraentsai entradasaidanf, n.valortotalnf TotalNF, n.codclassfisc, n.codtpDoc");
                    query.Append("     , m.descricaomatavulso materia, i.qtde, 'ESF' Origem, 'Venda' Tipo,  i.vlrtotal totalitem");
                    query.Append("  From Esfnotafiscal n, Esfnotafiscal_Item i, Esfmatavulso m");
                    query.Append(" Where n.dataemissao Between To_Date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   And To_Date('" + dataFim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   And n.codclassfisc In(5551, 6551  )");
                    query.Append("   And n.codintnf = i.codintnf");
                    query.Append("   And m.codigomatavulso = i.codproduto");
                    query.Append("   And I.cfop In(5551, 6551  )");

                    if (!consolidar)
                        query.Append("           And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = '" + empresa + "'");
                    else
                        query.Append("           And lpad(n.codigoempresa, 3, '0') = '" + empresa.Substring(0, 3) + "'");

                    Query executar = sessao.CreateQuery(query.ToString());

                    //analisar



                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Ativo.DetalheESF _tipo = new ConciliacaoContabil.Ativo.DetalheESF();

                            _tipo.Numero = dataReader["numeronf"].ToString();
                            _tipo.Serie = dataReader["serienf"].ToString();
                            _tipo.Material = dataReader["Material"].ToString();
                            _tipo.Origem = dataReader["Origem"].ToString();
                            _tipo.Tipo = dataReader["Tipo"].ToString();
                            _tipo.TpDocto = dataReader["codtpDoc"].ToString();

                            /*
                            //armadilha pega documento
                            if (_tipo.Docto == "0000015593")
                            {
                                Console.WriteLine("teste");
                            }
                            */

                            _tipo.TotalItem = Convert.ToDecimal(dataReader["TotalItem"].ToString());
                                _tipo.TotalNF = Convert.ToDecimal(dataReader["TotalNF"].ToString());
                                _tipo.CFOP = Convert.ToInt32(dataReader["CodClassFisc"].ToString());
                                _tipo.Quantidade = Convert.ToInt32(dataReader["Qtde"].ToString());
                            
                            _tipo.Emissao = Convert.ToDateTime(dataReader["dataemissaonf"].ToString());
                            _tipo.Entrada = Convert.ToDateTime(dataReader["entradasaidanf"].ToString());

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


            public bool Gravar(List<ConciliacaoContabil.Ativo.Resumo> _lista)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                Publicas.mensagemDeErro = string.Empty;
                bool retorno = _lista.Count() == 0;

                OracleParameter parametro = new OracleParameter();
                List<OracleParameter> parametros = new List<OracleParameter>();

                try
                {
                    foreach (var item in _lista.Where(w => w.Conferido || w.Existe))
                    {
                        query.Clear();
                        if (!item.Existe)
                        {
                            query.Append("Insert into Niff_CTB_ConciliacaoATF");
                            query.Append(" ( id, idempresa, referencia, idusuario, Conta");
                            query.Append(" , ValorATF, ValorCTB");
                            query.Append(" , confirmado, textoexplicativo");
                            query.Append(") Values ( (Select nvl(Max(id),0) +1 from Niff_CTB_ConciliacaoATF) ");
                            query.Append("        , " + item.IdEmpresa );
                            query.Append("        , " + item.Referencia);
                            query.Append("        , " + Publicas._usuario.Id);
                             
                            query.Append("        , '" + item.Grupo.Replace(" G","") + "'");

                            query.Append("        , " + item.SaldoATF.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.SaldoCTB.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("        , '" + (item.Conferido ? "S" : "N") + "'");
                            query.Append("        , :texto");

                            query.Append(" )");
                        }
                        else
                        {
                            query.Append("Update Niff_CTB_ConciliacaoATF");
                            query.Append("   set ValorATF = " + item.SaldoATF.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , ValorCTB = " + item.SaldoCTB.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("     , Confirmado = '" + (item.Conferido ? "S" : "N") + "'");
                            query.Append("     , TextoExplicativo = :texto");

                            query.Append(" Where Id = " + item.Id);
                        }

                        try
                        {
                            parametros.Clear();
                            parametro = new OracleParameter();
                            parametro.ParameterName = ":texto";
                            try
                            {
                                parametro.Value = item.Explicacao.Replace("'", "");
                            }
                            catch
                            {
                                try
                                {
                                    if (item.Explicacao == null)
                                        parametro.Value = " ";
                                    else
                                        parametro.Value = item.Explicacao;
                                }
                                catch { parametro.Value = " "; }
                            }
                            parametro.OracleType = OracleType.VarChar;
                            parametros.Add(parametro);
                        }
                        catch { }
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

            #region parametros

            public List<ConciliacaoContabil.Ativo.ItemAtivo> Listar(int idEmpresa, bool consolidar)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Ativo.ItemAtivo> lista = new List<ConciliacaoContabil.Ativo.ItemAtivo>();
                Publicas.mensagemDeErro = string.Empty;
                string campo = @"\1.\2.\3.\4";

                try
                {
                    query.Append("Select Distinct codigo, trim(Descricao) Descricao");
                    query.Append("     , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("     , lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0') empresa");
                    query.Append("     , e.idEmpresa");
                    query.Append("  From Atfitem i, Niff_Chm_Empresas e");
                    query.Append(" Where e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')");

                    if (!consolidar)
                        query.Append("   And e.Idempresa = " + idEmpresa);
                    else
                    {
                        if (idEmpresa == 2 || idEmpresa == 3)
                            query.Append("   And e.Idempresa in (2,3)");
                        else
                        {
                            if (idEmpresa == 10 || idEmpresa == 11)
                                query.Append("   And e.Idempresa in (10,11)");
                            else
                            {
                                query.Append("   And e.Idempresa = " + idEmpresa);
                            }
                        }
                    }

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Ativo.ItemAtivo _tipo = new ConciliacaoContabil.Ativo.ItemAtivo();

                            _tipo.Existe = true;
                            _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                            _tipo.Conta = dataReader["Conta"].ToString();
                            _tipo.Nome = dataReader["Descricao"].ToString();
                            _tipo.Empresa = dataReader["Empresa"].ToString();
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());

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

            public ConciliacaoContabil.Ativo.ItemAtivo Consultar(int idEmpresa, int codigo, bool consolidar, string empresaGlobus)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                ConciliacaoContabil.Ativo.ItemAtivo _tipo = new ConciliacaoContabil.Ativo.ItemAtivo();

                Publicas.mensagemDeErro = string.Empty;
                string campo = @"\1.\2.\3.\4";

                try
                {
                    query.Append("Select codigo, Descricao");
                    query.Append("     , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    query.Append("     , lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0') empresa");
                    query.Append("     , e.idEmpresa");

                    query.Append("  From Atfitem i");
                    query.Append("     , Niff_Chm_Empresas e");

                    query.Append(" Where i.codigo = " + codigo);
                    if (!consolidar)
                    {
                        query.Append("   And e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')");
                        query.Append("   And e.Idempresa = " + idEmpresa);
                    }
                    else
                    {
                        query.Append("   And lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0') = '" + empresaGlobus + "'");
                        query.Append("   And e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')");
                    }

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                        {
                            _tipo.Existe = true;
                            _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                            _tipo.Conta = dataReader["Conta"].ToString();
                            _tipo.Nome = dataReader["Descricao"].ToString();
                                                        _tipo.Empresa = dataReader["Empresa"].ToString();
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());

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

                    query.Append("Select nvl(Max(Codigo),0) + 1 next From niff_ctb_contasativo");
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

            public List<ConciliacaoContabil.Ativo.Parametros> ListarParametros(int idEmpresa, bool paraTelaDePesquisa, bool consolidar)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Ativo.Parametros> lista = new List<ConciliacaoContabil.Ativo.Parametros>();
                Publicas.mensagemDeErro = string.Empty;
                string campo = @"\1.\2.\3.\4";

                try
                {
                    if (!paraTelaDePesquisa)
                    {
                        query.Append("Select t.Id, t.idempresa, t.codigoAtivo, t.CodigoGrupo, t.Descricao, t.Codigo, t.idempresaAtivo");
                        query.Append("     , i.descricao NomeConta");
                        query.Append("     , regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','" + campo + "') conta");
                    }
                    else
                        query.Append("Select Distinct t.idempresa, t.CodigoGrupo, t.Descricao, t.Codigo");

                    query.Append("  From Atfitem i, Niff_Chm_Empresas e, niff_ctb_contasativo T");
                    query.Append(" Where e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')");
                    query.Append("   And i.codigo = t.codigoativo");
                    query.Append("   And t.IdempresaAtivo = e.idempresa");

                    if (!consolidar)
                        query.Append("   And e.Idempresa = " + idEmpresa);
                    else
                    {
                        if (idEmpresa == 2 || idEmpresa == 3)
                            query.Append("   And e.Idempresa in (2,3)");
                        else
                        {
                            if (idEmpresa == 10 || idEmpresa == 11)
                                query.Append("   And e.Idempresa in (10,11)");
                            else
                            {
                                query.Append("   And e.Idempresa = " + idEmpresa);
                            }
                        }
                    }

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Ativo.Parametros _tipo = new ConciliacaoContabil.Ativo.Parametros();

                            _tipo.Existe = true;
                            try
                            {
                                _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                            }
                            catch { }

                            _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                            _tipo.IdEmpresaAtivo = Convert.ToInt32(dataReader["idempresaAtivo"].ToString());

                            try
                            {
                                _tipo.CodigoAtivo = Convert.ToInt32(dataReader["codigoAtivo"].ToString());
                            }
                            catch { }

                            _tipo.Descricao = dataReader["Descricao"].ToString();

                            _tipo.Grupo = dataReader["CodigoGrupo"].ToString() + " - " + _tipo.Descricao;
                            _tipo.CodigoGrupo = dataReader["CodigoGrupo"].ToString();

                            try
                            {
                                _tipo.NomeAtivo = dataReader["Conta"].ToString() + " - " + dataReader["NomeConta"].ToString();
                            }
                            catch { }
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

            public bool Gravar(List<ConciliacaoContabil.Ativo.Parametros> _lista)
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
                            query.Append("Insert into niff_ctb_contasativo");
                            query.Append(" ( id, idempresa, codigoAtivo, codigoGrupo, descricao, codigo, idempresaAtivo");
                            query.Append(") Values ( (Select nvl(Max(id),0) +1 from niff_ctb_contasativo) ");
                            query.Append("        , " + item.IdEmpresa);
                            query.Append("        , " + item.CodigoAtivo);
                            query.Append("        , '" + item.CodigoGrupo + "'");
                            query.Append("        , '" + item.Descricao + "'");
                            query.Append("        , " + item.Codigo);
                            query.Append("        , " + item.IdEmpresaAtivo);

                            query.Append(" )");
                        }
                        else
                        {
                            query.Append("Update niff_ctb_contasativo");
                            query.Append("   set codigoGrupo = '" + item.CodigoGrupo + "'");
                            query.Append("     , Descricao = '" + item.Descricao + "'");

                            query.Append(" Where Id = " + item.Id);
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

            public bool ExcluirItem(int id)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                Publicas.mensagemDeErro = string.Empty;
                
                try
                {

                    query.Append("Delete niff_ctb_contasativo");
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

            public bool ExcluirTudo(int codigo, int idEmpresa)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                Publicas.mensagemDeErro = string.Empty;

                try
                {

                    query.Append("Delete niff_ctb_contasativo");
                    query.Append(" Where Codigo = " + codigo);
                    query.Append("   and idEmpresa = " + idEmpresa);

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
        
        public class Bancaria
        {
            IDataReader dataReader;
            
            public List<ConciliacaoContabil.Bancaria.Resumo> Listar(string empresa, int plano, 
                                                                    string referencia, string referenciaIni, string referenciaAnterior,
                                                                    DateTime dataIni, DateTime dataFin, bool consolidar, bool naoConfirmados,
                                                                    int idEmpresa)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Bancaria.Resumo> lista = new List<ConciliacaoContabil.Bancaria.Resumo>();
                Publicas.mensagemDeErro = string.Empty;

                try
                {
                    query.Append("Select m.codbanco, m.codagencia, m.codcontabco, m.codcontactb");
                    query.Append("     , c.Id, c.idempresa, c.idusuario, c.referencia, c.textoexplicativo, c.confirmado");
                    query.Append("     , Sum(m.saldoIni) SaldoiniCTB");
                    query.Append("     , Sum(m.Resultado) Resultado");
                    query.Append("     , Sum(m.Debito) debitoCTB");
                    query.Append("     , Sum(m.credito) creditoCTB");
                    query.Append("     , Sum(m.saldoIni) + Sum(resultado) SaldoFimCTB");
                    query.Append("     , Sum(m.InicialBCO) SaldoIniBCO");
                    query.Append("     , Sum(m.debitoBCO)  DebitoBCO");
                    query.Append("     , Sum(m.CreditoBCo) creditoBCO");
                    query.Append("     , Sum(m.FinalBCO) SalfoFimBCO");
                    query.Append("  From Niff_CTB_ConciliacaoBCO c");
                    query.Append("     , Niff_Chm_Empresas e");
                    query.Append("     , ( Select c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb, '" + referencia + "' periodosaldo");
                    query.Append("              , 0 saldoIni");
                    query.Append("              , Sum(s.vldebitosaldo) - Sum(s.vlcreditosaldo) resultado");
                    query.Append("              , Sum(s.vldebitosaldo) debito");
                    query.Append("              , Sum(s.vlcreditosaldo) credito");
                    query.Append("              , 0 InicialBCo");
                    query.Append("              , 0 DebitoBCo");
                    query.Append("              , 0 CreditoBco");
                    query.Append("              , 0 FinalBCo");
                    query.Append("           From bcoconta_contactb ctb,  Bcoconta C, ctbsaldo s");
                    query.Append("          Where ctb.nroplano = " + plano);
                    query.Append("            And c.codbanco = ctb.codbanco");
                    query.Append("            And c.codagencia = ctb.codagencia");
                    query.Append("            And c.codcontabco = ctb.codcontabco");
                    query.Append("            And c.contacaixa = 'N'");
                    query.Append("            And s.nroplano = ctb.nroplano");
                    query.Append("            And s.periodosaldo = '" + referencia + "'");
                    query.Append("            And s.codcontactb = ctb.codcontactb");
                    query.Append("            And c.codigoempresa = s.codigoempresa");
                    //query.Append("                      And c.codcontabco = '8300'");

                    if (!consolidar)
                        query.Append("            And lpad(c.codigoempresa,3,'0') || '/' || lpad(c.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("            And lpad(c.codigoempresa,3,'0') = '" + empresa.Substring(0,3) + "'");

                    query.Append("          Group By c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb");
                    query.Append("          Union All");
                    query.Append("         Select c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb, '" + referencia + "' periodosaldo");
                    query.Append("              , Sum(s.vldebantsaldo) - Sum(s.vlcredantsaldo) saldoIni");
                    query.Append("              , 0 resultado");
                    query.Append("              , 0 debito");
                    query.Append("              , 0 credito");
                    query.Append("              , 0 InicialBCo");
                    query.Append("              , 0 DebitoBCo");
                    query.Append("              , 0 CreditoBco");
                    query.Append("              , 0 FinalBCo");
                    query.Append("           From bcoconta_contactb ctb,  Bcoconta C, ctbsaldo s");
                    query.Append("          Where ctb.nroplano = " + plano);
                    query.Append("            And c.codbanco = ctb.codbanco");
                    query.Append("            And c.codagencia = ctb.codagencia");
                    query.Append("            And c.codcontabco = ctb.codcontabco");
                    query.Append("            And c.contacaixa = 'N'");
                    query.Append("            And s.nroplano = ctb.nroplano");
                    query.Append("            And s.periodosaldo between '" + referenciaIni + "' and '" + referencia + "'" );
                    query.Append("            And s.codcontactb = ctb.codcontactb");
                    query.Append("            And c.codigoempresa = s.codigoempresa");
                    //query.Append("                      And c.codcontabco = '8300'");

                    if (!consolidar)
                        query.Append("            And lpad(c.codigoempresa,3,'0') || '/' || lpad(c.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("            And lpad(c.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("          Group By c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb");
                    query.Append("          Union All");
                    query.Append("          Select codbanco, codagencia, codcontabco, codcontactb, periodosaldo");
                    query.Append("               , 0 saldoIni");
                    query.Append("               , 0 resultado");
                    query.Append("               , 0 debito");
                    query.Append("               , 0 credito");
                    query.Append("               , Sum(SaldoAnterior) InicialBCo");
                    query.Append("               , Sum(Debito) DebitoBCo");
                    query.Append("               , Sum(credito) CreditoBco");
                    query.Append("               , Sum(SaldoAnterior) + Sum(Debito) + Sum(credito) FinalBCo");
                    query.Append("            From ( Select SUM(VLMOVTOBCO) + c.saldoinicialcontabco saldoAnterior");
                    query.Append("                        , 0 Debito");
                    query.Append("                        , 0 Credito");
                    query.Append("                        , c.saldoinicialcontabco");
                    query.Append("                        , c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb");
                    query.Append("                        , '" + dataIni.ToString("yyyyMM") + "' periodosaldo");
                    query.Append("                     From bcoconta_contactb ctb, BCOMOVTO M, Bcoconta C");
                    query.Append("                    Where M.CODBANCO = c.Codbanco");
                    query.Append("                      And M.CODAGENCIA  = c.Codagencia");
                    query.Append("                      And M.CODCONTABCO = c.codcontabco");
                    query.Append("                      And M.DTEFETIVAMOVTOBCO < to_date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                      And M.STATUSMOVTOBCO = 'N'");
                    query.Append("                      And M.CONCILIADOMOVTOBCO = 'S'");
                    query.Append("                      And c.contacaixa = 'N'");
                    //query.Append("                      And c.codcontabco = '8300'");

                    if (!consolidar)
                        query.Append("            And lpad(c.codigoempresa,3,'0') || '/' || lpad(c.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("            And lpad(c.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                      And c.codbanco = ctb.codbanco");
                    query.Append("                      And c.codagencia = ctb.codagencia");
                    query.Append("                      And c.codcontabco = ctb.codcontabco");
                    query.Append("                    Group By c.codbanco, c.codagencia, c.codcontabco, c.saldoinicialcontabco, c.saldo_acm_ate_data, ctb.codcontactb");
                    query.Append("                    Union All ");
                    query.Append("                   Select 0 saldoAnterior");
                    query.Append("                        , SUM(VLMOVTOBCO) Debito");
                    query.Append("                        , 0 Credito");
                    query.Append("                        , 0 saldoinicialcontabco");
                    query.Append("                        , c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb");
                    query.Append("                        , '" + dataIni.ToString("yyyyMM") + "' periodosaldo");
                    query.Append("                     From bcoconta_contactb ctb, BCOMOVTO M, Bcoconta C");
                    query.Append("                    Where M.CODBANCO = c.Codbanco");
                    query.Append("                      And M.CODAGENCIA  = c.Codagencia");
                    query.Append("                      And M.CODCONTABCO = c.codcontabco");
                    query.Append("                      And M.DTEFETIVAMOVTOBCO between to_date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') and to_date('" + dataFin.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                      And M.STATUSMOVTOBCO = 'N'");
                    query.Append("                      And M.CONCILIADOMOVTOBCO = 'S'");
                    query.Append("                      And c.contacaixa = 'N'");
                    //query.Append("                      And c.codcontabco = '8300'");

                    if (!consolidar)
                        query.Append("            And lpad(c.codigoempresa,3,'0') || '/' || lpad(c.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("            And lpad(c.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                      And c.codbanco = ctb.codbanco");
                    query.Append("                      And c.codagencia = ctb.codagencia");
                    query.Append("                      And c.codcontabco = ctb.codcontabco");
                    query.Append("                      And m.VLMOVTOBCO < 0");
                    query.Append("                    Group By c.codbanco, c.codagencia, c.codcontabco, c.saldoinicialcontabco, c.saldo_acm_ate_data, ctb.codcontactb");
                    query.Append("                    Union All ");
                    query.Append("                   Select 0 saldoAnterior");
                    query.Append("                        , 0 Debito");
                    query.Append("                        , SUM(VLMOVTOBCO) Credito");
                    query.Append("                        , 0 saldoinicialcontabco");
                    query.Append("                        , c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb");
                    query.Append("                        , '" + dataIni.ToString("yyyyMM") + "' periodosaldo");
                    query.Append("                     From bcoconta_contactb ctb, BCOMOVTO M, Bcoconta C");
                    query.Append("                    Where M.CODBANCO = c.Codbanco");
                    query.Append("                      And M.CODAGENCIA  = c.Codagencia");
                    query.Append("                      And M.CODCONTABCO = c.codcontabco");
                    query.Append("                      And M.DTEFETIVAMOVTOBCO between to_date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') and to_date('" + dataFin.ToShortDateString() + "','dd/mm/yyyy')");

                    query.Append("                      And M.STATUSMOVTOBCO = 'N'");
                    query.Append("                      And M.CONCILIADOMOVTOBCO = 'S'");
                    query.Append("                      And c.contacaixa = 'N'");
                    
                    if (!consolidar)
                        query.Append("            And lpad(c.codigoempresa,3,'0') || '/' || lpad(c.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("            And lpad(c.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                      And c.codbanco = ctb.codbanco");
                    query.Append("                      And c.codagencia = ctb.codagencia");
                    query.Append("                      And c.codcontabco = ctb.codcontabco");
                    query.Append("                      And m.VLMOVTOBCO > 0");

                    //query.Append("                      And c.codcontabco = '8300'");
                    query.Append("                    Group By c.codbanco, c.codagencia, c.codcontabco, c.saldoinicialcontabco, c.saldo_acm_ate_data, ctb.codcontactb");
                    query.Append("                 ) Group By codbanco, codagencia, codcontabco, codcontactb, periodosaldo");
                    query.Append("       ) m ");
                    query.Append("   Where c.codbanco(+) = m.codbanco                ");
                    query.Append("     And c.codAgencia(+) = m.CodAgencia");
                    query.Append("     And c.Codcontabco(+) = m.Codcontabco");
                    query.Append("     And c.Idempresa(+) = e.Idempresa");
                    query.Append("     And c.Referencia(+) = m.periodosaldo");
                    query.Append("     And e.idempresa = " + idEmpresa);

                    //query.Append("     And m.CodContaBCo = '001205'");
                    query.Append("   Group By m.codbanco, m.codagencia, m.codcontabco, m.codcontactb");
                    query.Append("       , c.Id, c.idempresa, c.idusuario, c.referencia, c.textoexplicativo, c.confirmado");

                    if (naoConfirmados)
                        query.Append(" Having (c.confirmado = 'N' Or c.confirmado Is Null)");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Bancaria.Resumo _tipo = new ConciliacaoContabil.Bancaria.Resumo();

                            try
                            {
                                _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                            }
                            catch { }

                            try
                            { 
                                _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                            }
                            catch { }

                            try
                            { 
                                _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                            }
                            catch { }

                            try
                            { 
                                _tipo.Referencia = Convert.ToInt32(dataReader["Referencia"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Explicacao = dataReader["textoexplicativo"].ToString();
                            }
                            catch { }

                            try
                            {
                                _tipo.Conferido = dataReader["confirmado"].ToString() == "S";
                            }
                            catch { }

                            _tipo.Existe = _tipo.Id != 0;

                            _tipo.Banco = Convert.ToInt32(dataReader["Codbanco"].ToString());
                            _tipo.Agencia = Convert.ToInt32(dataReader["CodAgencia"].ToString());
                            _tipo.Conta = dataReader["Codcontabco"].ToString();
                            
                            _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());

                            RateioBeneficios.ContasContabeis _conta = new RateioBeneficios.ContasContabeis();
                            try
                            {
                                _conta = new RateioBeneficiosDAO().Consulta(plano, _tipo.ContaContabil);
                                _tipo.Descricao = _conta.Codigo + " " + _conta.Nome;
                            }
                            catch { }

                            _tipo.CreditoBCO = Math.Abs(Convert.ToDecimal(dataReader["creditoBCO"].ToString()));
                            _tipo.CreditoCTB = Convert.ToDecimal(dataReader["creditoCTB"].ToString());
                            _tipo.DebitoBCO = Math.Abs(Convert.ToDecimal(dataReader["DebitoBCO"].ToString()));
                            _tipo.DebitoCTB = Convert.ToDecimal(dataReader["debitoCTB"].ToString());
                            _tipo.SaldoInicialBCO = Convert.ToDecimal(dataReader["SaldoIniBCO"].ToString());
                            _tipo.SaldoInicialCTB = Convert.ToDecimal(dataReader["SaldoiniCTB"].ToString());
                            _tipo.SaldoFinalBCO = Convert.ToDecimal(dataReader["SalfoFimBCO"].ToString());
                            _tipo.SaldoFinalCTB = Convert.ToDecimal(dataReader["SaldoFimCTB"].ToString());

                            _tipo.Diferencas = _tipo.SaldoFinalCTB != _tipo.SaldoFinalBCO;
                            _tipo.Diferença = _tipo.SaldoFinalCTB - _tipo.SaldoFinalBCO;

                            if (_tipo.DebitoBCO != 0 || _tipo.CreditoBCO != 0 || _tipo.SaldoFinalBCO != 0 || _tipo.SaldoInicialBCO != 0)
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

            public List<ConciliacaoContabil.Bancaria.Detalhe> Listar(string empresa, int plano,
                                                                    DateTime dataIni, DateTime dataFin, bool consolidar)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Bancaria.Detalhe> lista = new List<ConciliacaoContabil.Bancaria.Detalhe>();
                Publicas.mensagemDeErro = string.Empty;

                try
                {
                    query.Append("Select SUM(VLMOVTOBCO) Valor");
                    query.Append("     , c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb");
                    query.Append("     , m.Conciliadomovtobco, m.dtmovtobco, m.dtefetivamovtobco");
                    query.Append("     , m.docmovtobco, m.codmovtobco");
                    query.Append("     , m.sistema, h.DESCHISTOBCO, m.HistMovtoBCo");
                    query.Append("     , decode(m.Codlanca, Null, r.codlancactb, m.Codlanca) codlanca");
                    query.Append("     , decode(l.documentolanca, null, r.documentolanca, l.documentolanca) documentolanca");

                    query.Append("  From bcoconta_contactb ctb, BCOMOVTO M, Bcoconta C, Ctblanca l, BCOHisto H");
                    query.Append("     , (Select r.cod_lanca codlancaCTB, r.codmovtobco, l.documentolanca");
                    query.Append("         From t_trf_despesa r, Ctblanca l");
                    query.Append("      Where r.cod_lanca = l.codlanca");
                    query.Append("      Union All");
                    query.Append("     Select r.cod_lanca codlancaCTB, r.codmovtobco, l.documentolanca");
                    query.Append("       From t_trf_receita r, Ctblanca l");
                    query.Append("      Where r.cod_lanca = l.codlanca ) r");

                    query.Append(" Where M.CODBANCO = c.Codbanco");
                    query.Append("   And M.CODAGENCIA  = c.Codagencia");
                    query.Append("   And M.CODCONTABCO = c.codcontabco");
                    //query.Append("   And M.DTEFETIVAMOVTOBCO between to_date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') and to_date('" + dataFin.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And (M.DTEFETIVAMOVTOBCO between to_date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') and to_date('" + dataFin.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("    Or (M.Dtefetivamovtobco > to_date('" + dataFin.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And M.dtmovtobco between to_date('" + dataIni.ToShortDateString() + "','dd/mm/yyyy') and to_date('" + dataFin.ToShortDateString() + "','dd/mm/yyyy')))");

                    query.Append("   And M.STATUSMOVTOBCO = 'N'");
                    query.Append("   And c.contacaixa = 'N'");
                    query.Append("   And m.CodHistoBCO = h.CodhistoBCO");
                    query.Append("   And m.CodigoEmpresa = h.CodigoEmpresa");
                    query.Append("   And m.CodigoFl = h.CodigoFl");

                    query.Append("   And r.codmovtobco(+) = m.codmovtobco");
                    //query.Append("                      And c.codcontabco = '8300'");

                    if (!consolidar)
                        query.Append("            And lpad(c.codigoempresa,3,'0') || '/' || lpad(c.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("            And lpad(c.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("   And c.codbanco = ctb.codbanco");
                    query.Append("   And c.codagencia = ctb.codagencia");
                    query.Append("   And c.codcontabco = ctb.codcontabco");
                    query.Append("   And l.codlanca(+) = m.codlanca");
                    query.Append(" group by c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb");
                    query.Append("     , m.Conciliadomovtobco, m.dtmovtobco, m.dtefetivamovtobco");
                    query.Append("     , m.codlanca, m.docmovtobco, m.codmovtobco, l.documentolanca");
                    query.Append("     , m.sistema, h.DESCHISTOBCO, m.HistMovtoBCo, r.codlancactb");
                    query.Append("     , r.documentolanca");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Bancaria.Detalhe _tipo = new ConciliacaoContabil.Bancaria.Detalhe();
                            
                            _tipo.Banco = Convert.ToInt32(dataReader["Codbanco"].ToString());
                            _tipo.Agencia = Convert.ToInt32(dataReader["CodAgencia"].ToString());

                            _tipo.Data = Convert.ToDateTime(dataReader["Dtmovtobco"].ToString());
                            _tipo.DataConciliado = Convert.ToDateTime(dataReader["dtefetivamovtobco"].ToString());

                            _tipo.Conta = dataReader["Codcontabco"].ToString();
                            _tipo.DoctoBCO = dataReader["docmovtobco"].ToString();
                            _tipo.DoctoCTB = dataReader["documentolanca"].ToString();
                            _tipo.Origem = dataReader["sistema"].ToString();
                            _tipo.CodMovtoBCO = Convert.ToDecimal(dataReader["codmovtobco"].ToString());

                            try
                            {
                                _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.CodLanca = Convert.ToDecimal(dataReader["CodLanca"].ToString());
                            }
                            catch { }

                            if (_tipo.DataConciliado > dataFin)
                                _tipo.Conciliado = "NÃO";
                            else
                            {
                                _tipo.Conciliado = (dataReader["Conciliadomovtobco"].ToString() == "S" ? "SIM" : "NÃO");
                                if (_tipo.Conciliado == "NÃO" && _tipo.DataConciliado == _tipo.Data)
                                    _tipo.DataConciliado = null;
                            }

                            if (_tipo.Conciliado == "NÃO")
                            {
                                if (_tipo.CodLanca == 0)
                                    _tipo.Conciliado = "NÃO - CTB NÃO";
                                else
                                    _tipo.Conciliado = "NÃO - CTB SIM";
                            }
                            else
                            {
                                if (_tipo.CodLanca == 0)
                                    _tipo.Conciliado = "SIM - CTB NÃO";
                                else
                                    _tipo.Conciliado = "SIM - CTB SIM";
                            }

                            _tipo.Historico = dataReader["DESCHISTOBCO"].ToString() + " " + 
                                dataReader["HistMovtoBCo"].ToString();

                            _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());

                            _tipo.DoctoCTB = dataReader["documentolanca"].ToString();

                            lista.Add(_tipo);
                        }
                    }

                    
                    try
                    {
                        // busca os lançamentos contabeis para mostrar apenas os que não vieram da consulta acima. 
                        query.Clear();
                        query.Append("Select l.documentolanca, l.dtlanca, l.sistema, i.historicoitemlanca, l.codlanca");
                        query.Append("     , cc.codbanco, cc.codagencia, cc.codcontabco,  c.codcontactb");
                        query.Append("     , Sum(decode(i.debitocreditoitemlanca, 'D', 1, -1) * i.vritemlanca) valorctb ");
                        query.Append("  from ctbLanca l, ctbitlnc i, ctbconta c, bcoconta_contactb bc, bcoconta cc");
                        query.Append(" Where l.codlanca = i.codlanca");
                        query.Append("  And c.nroplano = " + plano);
                        query.Append("  And c.nroplano = i.nroplano");
                        query.Append("  And c.codcontactb = i.codcontactb");
                        query.Append("  And l.dtlanca Between to_date('" + dataIni.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And to_date('" + dataFin.ToShortDateString() + "', 'dd/mm/yyyy')");

                        query.Append("  And bc.codcontactb = c.codcontactb");
                        query.Append("  And bc.nroplano = c.nroplano");
                        //query.Append("  And cc.codcontabco = '8300'");

                        query.Append("  And cc.codigoempresa = l.codigoempresa");
                        query.Append("  And cc.codbanco = bc.codbanco");
                        query.Append("  And cc.codagencia = bc.codagencia");
                        query.Append("  And cc.codcontabco = bc.codcontabco");

                        if (!consolidar)
                            query.Append("   And lpad(l.codigoempresa,3,'0') || '/' || lpad(l.codigoFl,3,'0') = '" + empresa + "'");
                        else
                            query.Append("   And lpad(l.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                        query.Append(" group by l.documentolanca, l.dtlanca, l.sistema, i.historicoitemlanca, l.codlanca");
                        query.Append("     , cc.codbanco, cc.codagencia, cc.codcontabco,  c.codcontactb");

                        executar = sessao.CreateQuery(query.ToString());

                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            while (dataReader.Read())
                            {
                                decimal codlanca = Convert.ToDecimal(dataReader["CodLanca"].ToString());
                                bool encontrou = false;

                                foreach (var item in lista.Where(w => w.CodLanca == codlanca))
                                {
                                    encontrou = true;
                                }

                                if (!encontrou)
                                {
                                    ConciliacaoContabil.Bancaria.Detalhe _tipo = new ConciliacaoContabil.Bancaria.Detalhe();

                                    _tipo.CodLanca = codlanca;
                                    _tipo.Conciliado = "Apenas CTB";
                                    _tipo.Banco = Convert.ToInt32(dataReader["Codbanco"].ToString());
                                    _tipo.Agencia = Convert.ToInt32(dataReader["CodAgencia"].ToString());
                                    _tipo.Conta = dataReader["Codcontabco"].ToString();

                                    _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                                    _tipo.DoctoCTB = dataReader["documentolanca"].ToString();
                                    _tipo.Valor = Convert.ToDecimal(dataReader["valorctb"].ToString());
                                    _tipo.Historico = dataReader["historicoitemlanca"].ToString();
                                    _tipo.Origem = dataReader["Sistema"].ToString();
                                    _tipo.Data = Convert.ToDateTime(dataReader["dtlanca"].ToString());

                                    lista.Add(_tipo);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Publicas.mensagemDeErro = ex.Message;
                        return lista;
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


            public bool Gravar(List<ConciliacaoContabil.Bancaria.Resumo> _lista)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                Publicas.mensagemDeErro = string.Empty;
                bool retorno = _lista.Count() == 0;

                OracleParameter parametro = new OracleParameter();
                List<OracleParameter> parametros = new List<OracleParameter>();

                try
                {
                    foreach (var item in _lista.Where(w => w.Conferido || w.Existe))
                    {
                        query.Clear();
                        if (!item.Existe)
                        {
                            query.Append("Insert into Niff_CTB_ConciliacaoBCO");
                            query.Append(" ( id, idempresa, referencia, idusuario, codbanco, codagencia, codcontabco");
                            query.Append(" , saldoinibco, debitobco, creditobco, saldofinbco, saldoinictb, debitoctb");
                            query.Append(" , creditoctb, saldofinctb, confirmado, textoexplicativo");
                            query.Append(") Values ( (Select nvl(Max(id),0) +1 from Niff_CTB_ConciliacaoBCO) ");
                            query.Append("        , " + item.IdEmpresa);
                            query.Append("        , " + item.Referencia);
                            query.Append("        , " + Publicas._usuario.Id);

                            query.Append("        , " + item.Banco);
                            query.Append("        , " + item.Agencia);
                            query.Append("        , '" + item.Conta + "'");

                            query.Append("        , " + item.SaldoInicialBCO.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.DebitoBCO.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.CreditoBCO.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.SaldoFinalBCO.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("        , " + item.SaldoInicialCTB.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.DebitoCTB.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.CreditoCTB.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.SaldoFinalCTB.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("        , '" + (item.Conferido ? "S" : "N") + "'");
                            query.Append("        , :texto");

                            query.Append(" )");
                        }
                        else
                        {
                            query.Append("Update Niff_CTB_ConciliacaoBCO");
                            query.Append("   set SaldoIniBCO = " + item.SaldoInicialBCO.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , DebitoBCO = " + item.DebitoBCO.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , CreditoBCO = " + item.CreditoBCO.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , SaldoFinBCO = " + item.SaldoFinalBCO.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("     , SaldoIniCTB = " + item.SaldoInicialCTB.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , DebitoCTB = " + item.DebitoCTB.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , CreditoCTB = " + item.CreditoCTB.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , SaldoFinCTB = " + item.SaldoFinalCTB.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("     , Confirmado = '" + (item.Conferido ? "S" : "N") + "'");
                            query.Append("     , TextoExplicativo = :texto");

                            query.Append(" Where Id = " + item.Id);
                        }

                        try
                        {
                            parametros.Clear();
                            parametro = new OracleParameter();
                            parametro.ParameterName = ":texto";
                            try
                            {
                                parametro.Value = item.Explicacao.Replace("'", "");
                            }
                            catch
                            {
                                try
                                {
                                    if (item.Explicacao == null)
                                        parametro.Value = " ";
                                    else
                                        parametro.Value = item.Explicacao;
                                }
                                catch { parametro.Value = " "; }
                            }
                            parametro.OracleType = OracleType.VarChar;
                            parametros.Add(parametro);
                        }
                        catch { }
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

        }

        public class Fornecedores
        {
            IDataReader dataReader;
            IDataReader dataReaderAux;

            public List<ConciliacaoContabil.Fornecedores.Resumo> Listar(string empresa, int plano,
                                                                    string referencia, string referenciaIni, string tipoDocto,
                                                                    string classIni, string classFin,
                                                                    DateTime dataEntrada, DateTime dataEmissao, bool consolidar, bool naoConfirmados,
                                                                    int idEmpresa, bool incluirDoctoSubstituidos)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Fornecedores.Resumo> lista = new List<ConciliacaoContabil.Fornecedores.Resumo>();
                Publicas.mensagemDeErro = string.Empty;

                

                try
                {
                    query.Append("Select x.Codcontactb");
                    query.Append("     , cc.Id, cc.idempresa, cc.idusuario, cc.referencia, cc.textoexplicativo, cc.confirmado");
                    query.Append("     , Sum(x.valorCPG) valorCPG, Sum(x.ValorCTB) ValorCTB");
                    query.Append("  From NIFF_CTB_ConciliacaoForn cc");
                    query.Append("     , Niff_Chm_Empresas e");
                    query.Append("     , (Select Distinct codcontactb, nroplano");
                    query.Append("          From Cpgcontactb_Fornecedor) cf");
                    query.Append("             , (Select Codcontactb");
                    query.Append("             , Sum(decode(Substr(Nrodoctocpg, 1, 3), 'DV-', -1,1) * Total_Docto)");
                    query.Append("               - Case When EntradaDevolucao <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("                      Then Sum(Valor_devol) else 0 End ");
                    query.Append("               - Sum(decode(Substr(Nrodoctocpg, 1, 3), 'DV-', -1,1) * nvl(dv_desconto,0)) - Sum(Vlrirrfcpg) - Sum(Vlrinsscpg)  - sum(Vlrisscpg) As ValorCPG, 0 ValorCTB");
                    query.Append("                  from (Select Nrodoctocpg, Nroparcelacpg, Seriedoctocpg, d.Codtpdoc, Cf.Codcontactb");
                    query.Append("                            , Fc_Cpg_Vlrliquido(d.Coddoctocpg) As Vlr_Liquido_Real");
                    query.Append("                            , Sum(Valoritemdoc) - decode(sum(Valor_Adto), Sum(Valoritemdoc),  sum(Valor_Adto),0 ) Total_Docto");
                    query.Append("                            , d.Entradacpg Entradacpg, Emissaocpg, Vencimentocpg, Statusdoctocpg, Pagamentocpg");
                    query.Append("                            , nvl(Descontocpg, 0) Descontocpg, nvl(Acrescimocpg, 0) Acrescimocpg");
                    query.Append("                            , nvl(Vlrinsscpg, 0) Vlrinsscpg, nvl(Vlrirrfcpg, 0) Vlrirrfcpg, Obsdoctocpg, Nrforn");
                    query.Append("                            , Rsocialforn, Nfantasiaforn, m.Codbanco, m.Docmovtobco, d.Codigofl, Vlrpiscpg");
                    query.Append("                            , nvl(Vlrcofinscpg, 0) Vlrcofinscpg, nvl(Vlrcslcpg, 0) Vlrcslcpg, nvl(Vlrisscpg, 0) Vlrisscpg");
                    query.Append("                            , t.Substituitpdoc, Nvl(Valor_Adto, 0) Valor_Adto");
                    query.Append("                            , Nvl(Valor_Devol, 0) Valor_Devol, d.Coddoctocpg, d.Usuariocpg_Exc, d.Datahoracpg_Exc");
                    query.Append("                            , Case When Substr(Nrodoctocpg, 1, 3) = 'DV-' Then");
                    query.Append("                                 Decode(d.Coddoctocpg_Devol, Null, 'N', 'S')");
                    query.Append("                              Else");
                    query.Append("                                 Decode(d.Coddoctocpg_Adto, Null, 'N', 'S') End As Dv_Adt_Associada");
                    query.Append("                            , Vlrsestsenatcpg, c.Classificador, c.Nomeconta");
                    query.Append("                            , Case When(Substr(Nrodoctocpg, 1, 3) = 'DV-') And  (d.Coddoctocpg_Devol <> 0) Then");
                    query.Append("                                 (Select Sum(Fc_Cpg_Vlrliquido(d.Coddoctocpg))");
                    query.Append("                                    From Cpgdocto a ");
                    query.Append("                                   Where a.Coddoctocpg = d.Coddoctocpg_Devol");
                    query.Append("                                     And ((a.Pagamentocpg > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')) Or(a.Pagamentocpg Is Null))");
                    query.Append("                                     And a.Entradacpg <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                                     And a.Codtpdoc Not In (" + tipoDocto + ")");
                    query.Append("                                     And a.Statusdoctocpg <> 'C')");
                    query.Append("                              Else");
                    query.Append("                                 0 End As Dv_Desconto");

                    query.Append("                            , dd.entradacpg EntradaDevolucao");

                    query.Append("                         From Cpgdocto d, Cpgitdoc i, Bgm_Fornecedor f, Bcomovto m, Cprtpdoc t, Cpgcontactb_Fornecedor Cf, Ctbconta c");
                    //query.Append("                            , (Select entradacpg, coddoctocpg_devol From cpgdocto ) dd");
                    query.Append("                            , (SELECT DISTINCT TRUNC(LAST_DAY(entradacpg))entradacpg , coddoctocpg_devol FROM CPGDOCTO) dd");
                    query.Append("                        where d.Codtpdoc Not In (" + tipoDocto + ")");
                    query.Append("                          and Emissaocpg >= to_date('" + dataEmissao.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                          and d.Entradacpg <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                          and ((Substr(d.Nrodoctocpg, 1, 3) = 'DV-') ");
                    query.Append("                          and ((d.Coddoctocpg_Devol Is Null) ");
                    query.Append("                           Or   ((Select Count(Coddoctocpg) ");
                    query.Append("                                    From Cpgdocto Doc");
                    query.Append("                                   Where Doc.Coddoctocpg = d.Coddoctocpg_Devol");
                    query.Append("                                     And (Doc.Pagamentocpg > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                                      Or  Doc.Pagamentocpg Is Null)) <> 0))");
                    query.Append("                           Or (Substr(d.Nrodoctocpg, 1, 3) <> 'DV-'");
                    query.Append("                          And (Pagamentocpg Is Null ");
                    query.Append("                           Or  Pagamentocpg > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy'))))");
                    query.Append("                          And Statusdoctocpg <> 'C'");
                    if (!incluirDoctoSubstituidos)
                        query.Append("                          And d.Coddoctocpgsubst Is Null");
                                                      
                    if (!consolidar)                  
                        query.Append("                          And lpad(d.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = '" + empresa + "'");
                    else                              
                        query.Append("                          And lpad(d.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");
                                                      
                    query.Append("                          And f.Codigoforn = d.Codigoforn ");
                    query.Append("                          And m.Codmovtobco(+) = d.Codmovtobco ");
                    query.Append("                          And t.Codigoempresa = d.Codigoempresa");
                    query.Append("                          And t.Codigofl = d.Codigofl");
                    query.Append("                          And t.Codtpdoc = d.Codtpdoc");
                    query.Append("                          And i.Coddoctocpg = d.Coddoctocpg");
                    query.Append("                          And Cf.Codigoforn = f.Codigoforn");
                    query.Append("                          And c.Codcontactb = Cf.Codcontactb");
                    query.Append("                          And c.Nroplano = Cf.Nroplano");
                    query.Append("                          And Cf.Nroplano = " + plano);
                    query.Append("                          And c.Classificador Between '" + classIni + "' And '" + classFin + "'");

                    query.Append("                          And dd.coddoctocpg_devol(+) = d.coddoctocpg");

                    query.Append("                        Group By Nrodoctocpg, Nroparcelacpg, Seriedoctocpg, d.Codtpdoc, Cf.Codcontactb");
                    query.Append("                            , d.Entradacpg, Emissaocpg, Vencimentocpg, Statusdoctocpg, Pagamentocpg, Descontocpg");
                    query.Append("                            , Acrescimocpg, Vlrinsscpg, Vlrirrfcpg, Obsdoctocpg, Nrforn, Rsocialforn, Nfantasiaforn");
                    query.Append("                            , m.Codbanco, m.Docmovtobco, d.Codigofl, Vlrpiscpg, Vlrcofinscpg, Vlrcslcpg, Vlrisscpg");
                    query.Append("                            , t.Substituitpdoc, Nvl(Valor_Adto, 0), Nvl(Valor_Devol, 0), d.Coddoctocpg, d.Usuariocpg_Exc");
                    query.Append("                            , d.Datahoracpg_Exc, d.Coddoctocpg_Devol, d.Coddoctocpg_Adto, Vlrsestsenatcpg, c.Classificador, c.Nomeconta, dd.entradacpg");
                    query.Append("              ) group by CodContaCTB, Entradadevolucao");

                    query.Append("                 Union All");
                    query.Append("                Select x.codcontactb, 0 VAlorCPG, Sum(resultado) + Sum(saldoini) ValorCTB            ");
                    query.Append("                  From (Select ctb.codcontactb");
                    query.Append("                             , Sum(s.vlcreditosaldo) - Sum(s.vldebitosaldo) resultado");
                    query.Append("                             , 0 saldoini");
                    query.Append("                             , Sum(s.vldebitosaldo) debito");
                    query.Append("                             , Sum(s.vlcreditosaldo) credito");
                    query.Append("                          From Ctbconta ctb, ctbsaldo s");
                    query.Append("                         Where ctb.nroplano = " + plano);
                    query.Append("                           And s.nroplano = ctb.nroplano ");
                    query.Append("                           And s.periodosaldo = '" + referencia + "'");
                    query.Append("                           And s.codcontactb = ctb.codcontactb");

                    if (!consolidar)
                        query.Append("                           And lpad(s.codigoempresa,3,'0') || '/' || lpad(s.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("                           And lpad(s.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                           And ctb.classificador Between '" + classIni + "' And '" + classFin + "'");
                    query.Append("                         Group By ctb.codcontactb");
                    query.Append("                         Union All");
                    query.Append("                        Select ctb.codcontactb");
                    query.Append("                             , 0 resultado");
                    query.Append("                             , Sum(s.vlcredantsaldo) - Sum(s.vldebantsaldo) saldoini");
                    query.Append("                             , 0 debito");
                    query.Append("                             , 0 credito");
                    query.Append("                          From Ctbconta ctb, ctbsaldo s");
                    query.Append("                         Where ctb.nroplano = " + plano);
                    query.Append("                           And s.nroplano = ctb.nroplano");
                    query.Append("                           And s.periodosaldo Between '" + referenciaIni + "' And '" + referencia + "'");
                    query.Append("                           And s.codcontactb = ctb.codcontactb");

                    if (!consolidar)
                        query.Append("                           And lpad(s.codigoempresa,3,'0') || '/' || lpad(s.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("                           And lpad(s.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                           And ctb.classificador Between '" + classIni + "' And '" + classFin + "'");
                    query.Append("                         Group By ctb.codcontactb) x");
                    query.Append("      Group By codcontactb) x");
                    query.Append("      Where cf.codcontactb = x.codcontactb");
                    query.Append("        And cc.codcontactb(+) = cf.codcontactb");
                    query.Append("        And cc.nroplano(+) = cf.nroplano");
                    query.Append("        And cf.nroplano = " + plano);
                    query.Append("        And cc.Idempresa(+) = e.Idempresa");
                    query.Append("        And e.idempresa = " + idEmpresa);

                    //query.Append("   And cf.CodContaCTB = 20178");

                    query.Append(" Group By x.Codcontactb");
                    query.Append("     , cc.Id, cc.idempresa, cc.idusuario, cc.referencia, cc.textoexplicativo, cc.confirmado");
                    
                    if (naoConfirmados)
                        query.Append(" Having (cc.confirmado = 'N' Or cc.confirmado Is Null)");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Fornecedores.Resumo _tipo = new ConciliacaoContabil.Fornecedores.Resumo();

                            try
                            {
                                _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Referencia = Convert.ToInt32(dataReader["Referencia"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Explicacao = dataReader["textoexplicativo"].ToString();
                            }
                            catch { }

                            try
                            {
                                _tipo.Conferido = dataReader["confirmado"].ToString() == "S";
                            }
                            catch { }

                            _tipo.Existe = _tipo.Id != 0;

                            _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());

                            RateioBeneficios.ContasContabeis _conta = new RateioBeneficios.ContasContabeis();
                            try
                            {
                                _conta = new RateioBeneficiosDAO().Consulta(plano, _tipo.ContaContabil);
                                _tipo.Descricao = _conta.Codigo + " " + _conta.Nome;
                            }
                            catch { }

                            

                            //_tipo.ValorCPG = Math.Abs(Convert.ToDecimal(dataReader["valorCPG"].ToString()));
                            _tipo.ValorCPG = Convert.ToDecimal(dataReader["valorCPG"].ToString());
                            _tipo.ValorCTB = Convert.ToDecimal(dataReader["ValorCTB"].ToString());

                            /*
                            //armadilha pega conta                            
                            if (_conta.Codigo == 26465)
                            {
                                Console.WriteLine("teste");
                            }
                            */

                            _tipo.Diferencas = _tipo.ValorCTB != _tipo.ValorCPG;
                            _tipo.Diferenca = _tipo.ValorCPG - _tipo.ValorCTB;

                            //List<ConciliacaoContabil.Fornecedores.Resumo> listaSemDuplicidades = lista.Distinct().ToList();
                            //lista = listaSemDuplicidades;
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

            public List<ConciliacaoContabil.Fornecedores.FornAssociados> ListarFornecedores(int plano)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Fornecedores.FornAssociados> lista = new List<ConciliacaoContabil.Fornecedores.FornAssociados>();
                Publicas.mensagemDeErro = string.Empty;

                try
                {
                    // busca os fornecedores associados a contas contabeis. 
                    query.Clear();
                    query.Append("Select cf.codcontactb,  f.nrforn, f.rsocialforn, f.codigoforn ");
                    query.Append("  from Bgm_Fornecedor f, Cpgcontactb_Fornecedor Cf");
                    query.Append(" Where f.codigoforn = cf.codigoforn");
                    query.Append("  And cf.nroplano = " + plano);

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {

                            ConciliacaoContabil.Fornecedores.FornAssociados _tipo = new ConciliacaoContabil.Fornecedores.FornAssociados();

                            _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                            _tipo.Fornecedor = dataReader["NrForn"].ToString() + " - " + dataReader["Rsocialforn"].ToString();
                            _tipo.Id = Convert.ToInt32(dataReader["CodigoForn"].ToString());
                            _tipo.Plano = plano;

                            lista.Add(_tipo);

                        }
                    }

                }
                catch (Exception ex)
                {
                    Publicas.mensagemDeErro = ex.Message;
                    return lista;
                }
                return lista;

            }

            public List<ConciliacaoContabil.Fornecedores.Detalhe> ListarDetalhes(string empresa, int plano,
                                                                                string tipoDocto,
                                                                                string classIni, string classFin,
                                                                                DateTime dataEntrada, DateTime dataEmissao, bool consolidar, bool naoConfirmados,
                                                                                int idEmpresa, bool incluirDoctoSubstituidos)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Fornecedores.Detalhe> lista = new List<ConciliacaoContabil.Fornecedores.Detalhe>();
                Publicas.mensagemDeErro = string.Empty;

                try
                { 
                    query.Append("Select Nrodoctocpg, codlanca, Nroparcelacpg, Seriedoctocpg, Codtpdoc, Codcontactb, CODDOCTOESF, coddoctocpg");
                    query.Append("     , Vlr_Liquido_Real");
                    query.Append("     , decode(Substr(Nrodoctocpg, 1, 3), 'DV-', -1, 1) * Total_Docto");
                    query.Append("       - nvl(Vlrinsscpg,0) - Nvl(Vlrirrfcpg,0) - nvl(Vlrisscpg,0)");
                    query.Append("       - decode(Valor_Adto, Total_Docto, Nvl(Valor_Adto,0), 0)");
                    query.Append("       - Case When EntradaDevolucao <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("              Then Valor_devol else 0 End ");

                    query.Append("       - decode(Substr(Nrodoctocpg, 1, 3), 'DV-', -1, 1) * nvl(dv_desconto, 0) ValorCPG");
                    query.Append("     , Nvl(vlmovtobco,0) Valorbco");
                    query.Append("     , sistema, Entradacpg, Emissaocpg, Vencimentocpg, Statusdoctocpg, Pagamentocpg");
                    query.Append("     , Obsdoctocpg, Nrforn, Rsocialforn, Docmovtobco, Codbanco, codlancaBCO");
                    query.Append("  From ( ");
                    query.Append("Select Nrodoctocpg, Nroparcelacpg, Seriedoctocpg, d.Codtpdoc, Cf.Codcontactb");
                    query.Append("     , d.CODDOCTOESF, d.codlanca, d.sistema, m.Codlanca codlancaBCO");
                    query.Append("     , Fc_Cpg_Vlrliquido(d.Coddoctocpg) As Vlr_Liquido_Real");
                    query.Append("     , Sum(Valoritemdoc) - decode(sum(Valor_Adto), Sum(Valoritemdoc),  sum(Valor_Adto),0 ) Total_Docto");
                    query.Append("     , d.Entradacpg Entradacpg, Emissaocpg, Vencimentocpg, Statusdoctocpg, Pagamentocpg");
                    query.Append("     , Descontocpg, Acrescimocpg, Vlrinsscpg, Vlrirrfcpg, Obsdoctocpg, Nrforn");
                    query.Append("     , Rsocialforn, Nfantasiaforn, m.Codbanco, m.Docmovtobco, Sum(m.vlmovtobco) vlmovtobco, d.Codigofl, Vlrpiscpg");
                    query.Append("     , Vlrcofinscpg, Vlrcslcpg, Vlrisscpg, t.Substituitpdoc, Nvl(Valor_Adto, 0) Valor_Adto");
                    query.Append("     , Nvl(Valor_Devol, 0) Valor_Devol, d.Coddoctocpg, d.Usuariocpg_Exc, d.Datahoracpg_Exc");
                    query.Append("     , Case When Substr(Nrodoctocpg, 1, 3) = 'DV-' Then");
                    query.Append("          Decode(d.Coddoctocpg_Devol, Null, 'N', 'S')");
                    query.Append("       Else");
                    query.Append("          Decode(d.Coddoctocpg_Adto, Null, 'N', 'S') End As Dv_Adt_Associada");
                    query.Append("     , Vlrsestsenatcpg, c.Classificador, c.Nomeconta");
                    query.Append("     , Case When(Substr(Nrodoctocpg, 1, 3) = 'DV-') And  (d.Coddoctocpg_Devol <> 0) Then");
                    query.Append("          (Select Sum(Fc_Cpg_Vlrliquido(d.Coddoctocpg))");
                    query.Append("             From Cpgdocto a ");
                    query.Append("            Where a.Coddoctocpg = d.Coddoctocpg_Devol");
                    query.Append("              And ((a.Pagamentocpg > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')) Or(a.Pagamentocpg Is Null))");
                    query.Append("              And a.Entradacpg <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("              And a.Codtpdoc Not In (" + tipoDocto + ")");
                    query.Append("              And a.Statusdoctocpg <> 'C')");
                    query.Append("       Else");
                    query.Append("          0 End As Dv_Desconto");
                    query.Append("     , Dd.Entradacpg Entradadevolucao");
                    query.Append("  From Cpgdocto d, Cpgitdoc i, Bgm_Fornecedor f, Bcomovto m, Cprtpdoc t, Cpgcontactb_Fornecedor Cf, Ctbconta c");
                    //query.Append("     , (Select Entradacpg, Coddoctocpg_Devol From Cpgdocto) Dd");
                    query.Append("     , (SELECT DISTINCT TRUNC(LAST_DAY(entradacpg))entradacpg , coddoctocpg_devol FROM CPGDOCTO) Dd");
                    query.Append(" where d.Codtpdoc Not In (" + tipoDocto + ")");
                    query.Append("   and Emissaocpg >= to_date('" + dataEmissao.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   and d.Entradacpg <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   and ((Substr(d.Nrodoctocpg, 1, 3) = 'DV-') ");
                    query.Append("   and ((d.Coddoctocpg_Devol Is Null) ");
                    query.Append("    Or   ((Select Count(Coddoctocpg) ");
                    query.Append("             From Cpgdocto Doc");
                    query.Append("            Where Doc.Coddoctocpg = d.Coddoctocpg_Devol");
                    query.Append("              And (Doc.Pagamentocpg > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("               Or  Doc.Pagamentocpg Is Null)) <> 0))");
                    query.Append("    Or (Substr(d.Nrodoctocpg, 1, 3) <> 'DV-'");
                    query.Append("   And (Pagamentocpg Is Null ");
                    query.Append("    Or  Pagamentocpg > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy'))))");
                    query.Append("   And Statusdoctocpg <> 'C'");

                    if (!incluirDoctoSubstituidos)
                        query.Append("   And d.Coddoctocpgsubst Is Null");

                    if (!consolidar)
                        query.Append("   And lpad(d.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("   And lpad(d.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("   And f.Codigoforn = d.Codigoforn ");
                    query.Append("   And m.Codmovtobco(+) = d.Codmovtobco ");
                    query.Append("   And t.Codigoempresa = d.Codigoempresa");
                    query.Append("   And t.Codigofl = d.Codigofl");
                    query.Append("   And t.Codtpdoc = d.Codtpdoc");
                    query.Append("   And i.Coddoctocpg = d.Coddoctocpg");
                    query.Append("   And Cf.Codigoforn = f.Codigoforn");
                    query.Append("   And c.Codcontactb = Cf.Codcontactb");
                    query.Append("   And c.Nroplano = Cf.Nroplano");
                    query.Append("   And Cf.Nroplano = " + plano);
                    query.Append("   And c.Classificador Between '" + classIni + "' And '" + classFin + "'");
                    query.Append("   And Dd.Coddoctocpg_Devol(+) = d.Coddoctocpg");

                    //query.Append("   And c.CodContaCTB = 20178");
                    query.Append(" Group By Nrodoctocpg, Nroparcelacpg, Seriedoctocpg, d.Codtpdoc, Cf.Codcontactb");
                    query.Append("     , d.Entradacpg, Emissaocpg, Vencimentocpg, Statusdoctocpg, Pagamentocpg, Descontocpg");
                    query.Append("     , Acrescimocpg, Vlrinsscpg, Vlrirrfcpg, Obsdoctocpg, Nrforn, Rsocialforn, Nfantasiaforn");
                    query.Append("     , m.Codbanco, m.Docmovtobco, d.Codigofl, Vlrpiscpg, Vlrcofinscpg, Vlrcslcpg, Vlrisscpg");
                    query.Append("     , t.Substituitpdoc, Nvl(Valor_Adto, 0), Nvl(Valor_Devol, 0), d.Coddoctocpg, d.Usuariocpg_Exc");
                    query.Append("     , d.Datahoracpg_Exc, d.Coddoctocpg_Devol, d.Coddoctocpg_Adto, Vlrsestsenatcpg, c.Classificador, c.Nomeconta");
                    query.Append("     , d.CODDOCTOESF, d.codlanca, d.sistema, m.codlanca, Dd.Entradacpg ");
                    query.Append("     ) order by codcontactb, Nrodoctocpg");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            
                            ConciliacaoContabil.Fornecedores.Detalhe _tipo = new ConciliacaoContabil.Fornecedores.Detalhe();

                            _tipo.DoctoCPG = dataReader["Nrodoctocpg"].ToString();                            

                            _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                            _tipo.Docto = dataReader["Nrodoctocpg"].ToString();
                            _tipo.DoctoCPG = dataReader["Nrodoctocpg"].ToString() + "/" + dataReader["Nroparcelacpg"].ToString() + " - " + dataReader["Seriedoctocpg"].ToString();

                            try
                            {
                                _tipo.ValorCPG = Convert.ToDecimal(dataReader["ValorCPG"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ValorBCO = Convert.ToDecimal(dataReader["ValorBCO"].ToString());
                            }
                            catch { }

                            _tipo.Fornecedor = dataReader["NrForn"].ToString() + " - " + dataReader["Rsocialforn"].ToString();
                            _tipo.TipoDocto = dataReader["Codtpdoc"].ToString();

                            try
                            {
                                _tipo.DoctoBCO = dataReader["Docmovtobco"].ToString();
                            }
                            catch { }

                            _tipo.Origem = dataReader["Sistema"].ToString();
                            _tipo.Observacao = dataReader["Obsdoctocpg"].ToString();

                            _tipo.Entrada = Convert.ToDateTime(dataReader["Entradacpg"].ToString());
                            _tipo.Emissao = Convert.ToDateTime(dataReader["Emissaocpg"].ToString());
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["Vencimentocpg"].ToString());

                            try
                            {
                                _tipo.CodLanca = Convert.ToDecimal(dataReader["CodLanca"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.CodLancaBCO = Convert.ToDecimal(dataReader["CodLancaBCO"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.CodDoctoESF = Convert.ToDecimal(dataReader["CodDoctoESF"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.CodDoctoCPG = Convert.ToDecimal(dataReader["CodDoctoCPG"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Pagamento = Convert.ToDateTime(dataReader["Pagamentocpg"].ToString());
                            }
                            catch { }
                            
                            if (_tipo.CodLanca != 0)
                            {
                                query.Clear();
                                query.Append("Select documentolanca, Sum(decode(il.debitocreditoitemlanca, 'D', 1, -1) * il.vritemlanca) valorctb");
                                query.Append("  from ctblanca l, ctbitlnc il");
                                query.Append(" where l.codlanca = " + _tipo.CodLanca);
                                query.Append("   And il.codlanca = l.codlanca");
                                query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                query.Append("  group by documentolanca");

                                executar = sessao.CreateQuery(query.ToString());

                                dataReaderAux = executar.ExecuteQuery();

                                using (dataReaderAux)
                                {
                                    if (dataReaderAux.Read())
                                    {
                                        _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                        _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());

                                        if (_tipo.CodLanca != 0)
                                        {
                                            query.Clear();
                                            query.Append("Select documentolanca, Sum(decode(il.debitocreditoitemlanca, 'D', 1, -1) * il.vritemlanca) valorctb");
                                            query.Append("  from ctblanca l, ctbitlnc il");
                                            query.Append(" where l.codlanca = " + _tipo.CodLanca);
                                            query.Append("   And il.codlanca = l.codlanca");
                                            query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                            query.Append("  group by documentolanca");

                                            executar = sessao.CreateQuery(query.ToString());

                                            dataReaderAux = executar.ExecuteQuery();

                                            using (dataReaderAux)
                                            {
                                                while (dataReaderAux.Read())
                                                {
                                                    _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                    _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                    if (_tipo.ValorCPG == _tipo.ValorCTB)
                                                        break;
                                                }
                                            }
                                        }

                                        if (_tipo.ValorCPG > 0)
                                            _tipo.ValorCTB = Math.Abs(_tipo.ValorCTB);
                                    }
                                }
                                
                                if (_tipo.Pagamento > dataEntrada)
                                    _tipo.ValorCTB = 0;
                            }
                            else
                            {
                                if (_tipo.Origem == "ESF" && _tipo.CodLanca == 0)
                                {
                                    if (_tipo.CodDoctoESF != 0)
                                    {
                                        /*
                                        //armadilha - Pega codLanca
                                        if (_tipo.CodLanca == 3495305)
                                        {
                                            Console.WriteLine("teste");
                                        }
                                        */
                                        query.Clear();
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'D', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Esfnotafiscal n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctoesf = " + _tipo.CodDoctoESF);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.documentolanca, l.codlanca");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReaderAux = executar.ExecuteQuery();

                                        using (dataReaderAux)
                                        {
                                            if (dataReaderAux.Read())
                                            {
                                                _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                            }
                                            else
                                            {
                                                query.Clear();
                                                query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'D', -1, 1) * il.vritemlanca) valorctb");
                                                query.Append("  from ESFEntra n, ctblanca l, ctbitlnc il");
                                                query.Append(" where n.coddoctoesf = " + _tipo.CodDoctoESF);
                                                query.Append("   And l.codlanca = n.codlanca");
                                                query.Append("   And il.codlanca = l.codlanca");
                                                query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                                query.Append("  group by l.documentolanca, l.codlanca");

                                                executar = sessao.CreateQuery(query.ToString());

                                                dataReaderAux = executar.ExecuteQuery();

                                                using (dataReaderAux)
                                                {
                                                    if (dataReaderAux.Read())
                                                    {
                                                        _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                        _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                        _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    else
                                    {
                                        query.Clear();
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'D', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Esfiss_Entra n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctocpg = " + _tipo.CodDoctoCPG);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.documentolanca, l.codlanca");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReaderAux = executar.ExecuteQuery();

                                        using (dataReaderAux)
                                        {
                                            if (dataReaderAux.Read())
                                            {
                                                _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (_tipo.Origem == "EST" && _tipo.CodLanca == 0)
                                    {
                                        query.Clear();
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'D', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Bgm_Notafiscal n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctocpg = " + _tipo.CodDoctoCPG);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.codlanca, l.documentolanca");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReaderAux = executar.ExecuteQuery();

                                        using (dataReaderAux)
                                        {
                                            if (dataReaderAux.Read())
                                            {
                                                _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                            }
                                        }

                                        if (_tipo.ValorCTB == 0)
                                        {
                                            query.Clear();
                                            query.Append("Select documentolanca, Sum(decode(il.debitocreditoitemlanca, 'D', 1, -1) * il.vritemlanca) valorctb");
                                            query.Append("  from ctblanca l, ctbitlnc il");
                                            query.Append(" where l.codlanca = " + _tipo.CodLancaBCO);
                                            query.Append("   And il.codlanca = l.codlanca");
                                            query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                            query.Append("   And il.historicoitemlanca like '%" + _tipo.Docto + "%'");
                                            query.Append("  group by documentolanca");

                                            executar = sessao.CreateQuery(query.ToString());

                                            dataReaderAux = executar.ExecuteQuery();

                                            using (dataReaderAux)
                                            {
                                                while (dataReaderAux.Read())
                                                {
                                                    _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                    _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                    _tipo.CodLanca = _tipo.CodLancaBCO;
                                                    if (_tipo.ValorCPG == _tipo.ValorCTB)
                                                        break;
                                                }
                                            }
                                            if (_tipo.ValorCPG != _tipo.ValorCTB && (_tipo.DoctoCTB != null))
                                                _tipo.ValorCTB = _tipo.ValorCPG;
                                        }
                                    }
                                }
                            }

                            /*
                            //armadilha 
                            if (_tipo.CodDoctoCPG == 1106429)
                            {
                                Console.WriteLine("teste");
                            }
                            */


                            /*
                            //PEGA DUPLICIDADE
                            bool itemDuplicado = false;

                            foreach (var item in lista)
                            {
                                if (item.CodDoctoCPG == _tipo.CodDoctoCPG)
                                {
                                    itemDuplicado = true;
                                }
                            }

                            if (!itemDuplicado)
                            {
                                lista.Add(_tipo);
                            }
                            */
                            lista.Add(_tipo);

                            //List<ConciliacaoContabil.Fornecedores.Detalhe> listaSemDuplicidades = lista.Distinct().ToList();
                            //lista = listaSemDuplicidades;

                        }
                    }

                    try
                    {
                        // busca os lançamentos contabeis. 
                        query.Clear();
                        query.Append("Select l.documentolanca, l.dtlanca, l.sistema, i.historicoitemlanca, l.codlanca");
                        query.Append("     , decode(i.debitocreditoitemlanca, 'D', -1, 1) * i.vritemlanca valorctb,  c.codcontactb ");
                        query.Append("  from ctbLanca l, ctbitlnc i, ctbconta c");
                        query.Append(" Where l.codlanca = i.codlanca");
                        query.Append("  And c.nroplano = " + plano);
                        query.Append("  And c.nroplano = i.nroplano");
                        query.Append("  And c.codcontactb = i.codcontactb");
                        query.Append("  And c.Classificador Between '" + classIni + "' And '" + classFin + "'");
                        query.Append("  And l.dtlanca Between to_date('" + dataEntrada.AddMonths(-1).AddDays(1).ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And to_date('" + dataEntrada.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And sistema not in ('CPG','CRC')");
                        query.Append("  And debitocreditoitemlanca = 'D'");

                        if (!consolidar)
                            query.Append("   And lpad(l.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = '" + empresa + "'");
                        else
                            query.Append("   And lpad(l.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                        query.Append(" Union All  "); // incluido para atender a necessidade da Rapido lançamentos feitos no CTB a credito
                        query.Append("Select l.documentolanca, l.dtlanca, l.sistema, i.historicoitemlanca, l.codlanca");
                        query.Append("     , decode(i.debitocreditoitemlanca, 'D', -1, 1) * i.vritemlanca valorctb,  c.codcontactb ");
                        query.Append("  from ctbLanca l, ctbitlnc i, ctbconta c");
                        query.Append(" Where l.codlanca = i.codlanca");
                        query.Append("  And c.nroplano = " + plano);
                        query.Append("  And c.nroplano = i.nroplano");
                        query.Append("  And c.codcontactb = i.codcontactb");
                        query.Append("  And c.Classificador Between '" + classIni + "' And '" + classFin + "'");
                        query.Append("  And l.dtlanca Between to_date('" + dataEntrada.AddMonths(-1).AddDays(1).ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And to_date('" + dataEntrada.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And sistema in ('CTB')");
                        query.Append("  And debitocreditoitemlanca = 'C'");

                        if (!consolidar)
                            query.Append("   And lpad(l.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = '" + empresa + "'");
                        else
                            query.Append("   And lpad(l.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                        executar = sessao.CreateQuery(query.ToString());

                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            while (dataReader.Read())
                            {
                                decimal codlanca = Convert.ToDecimal(dataReader["CodLanca"].ToString());
                                bool encontrou = false;

                                foreach (var item in lista.Where(w => w.CodLanca == codlanca && w.Origem != "CTB"))
                                {
                                    encontrou = true;
                                }

                                if (!encontrou)
                                {
                                    ConciliacaoContabil.Fornecedores.Detalhe _tipo = new ConciliacaoContabil.Fornecedores.Detalhe();

                                    _tipo.CodLanca = codlanca;
                                    _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                                    _tipo.DoctoCTB = dataReader["documentolanca"].ToString();
                                    _tipo.ValorCTB = Convert.ToDecimal(dataReader["valorctb"].ToString());
                                    _tipo.Observacao = dataReader["historicoitemlanca"].ToString();
                                    _tipo.Origem = dataReader["Sistema"].ToString();
                                    _tipo.Entrada = Convert.ToDateTime(dataReader["dtlanca"].ToString());

                                    lista.Add(_tipo);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Publicas.mensagemDeErro = ex.Message;
                        return lista;
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


            public bool Gravar(List<ConciliacaoContabil.Fornecedores.Resumo> _lista)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                Publicas.mensagemDeErro = string.Empty;
                bool retorno = _lista.Count() == 0;

                OracleParameter parametro = new OracleParameter();
                List<OracleParameter> parametros = new List<OracleParameter>();

                try
                {
                    foreach (var item in _lista.Where(w => w.Conferido || w.Existe))
                    {
                        query.Clear();
                        if (!item.Existe)
                        {
                            query.Append("Insert into Niff_Ctb_Conciliacaoforn");
                            query.Append(" ( id, idempresa, referencia, idusuario, codcontactb, Nroplano");
                            query.Append(" , ValorCPG, valorCTB");
                            query.Append(" , confirmado, textoexplicativo");
                            query.Append(") Values ( (Select nvl(Max(id),0) +1 from Niff_Ctb_Conciliacaoforn) ");
                            query.Append("        , " + item.IdEmpresa);
                            query.Append("        , " + item.Referencia);
                            query.Append("        , " + Publicas._usuario.Id);

                            query.Append("        , " + item.ContaContabil);
                            query.Append("        , " + item.Plano);

                            query.Append("        , " + item.ValorCPG.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.ValorCTB.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("        , '" + (item.Conferido ? "S" : "N") + "'");
                            query.Append("        , :texto");

                            query.Append(" )");
                        }
                        else
                        {
                            query.Append("Update Niff_Ctb_Conciliacaoforn");
                            query.Append("   set ValorCPG = " + item.ValorCPG.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , ValorCTB = " + item.ValorCTB.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("     , Confirmado = '" + (item.Conferido ? "S" : "N") + "'");
                            query.Append("     , TextoExplicativo = :texto");

                            query.Append(" Where Id = " + item.Id);
                        }

                        try
                        {
                            parametros.Clear();
                            parametro = new OracleParameter();
                            parametro.ParameterName = ":texto";
                            try
                            {
                                parametro.Value = item.Explicacao.Replace("'", "");
                            }
                            catch
                            {
                                try
                                {
                                    if (item.Explicacao == null)
                                        parametro.Value = " ";
                                    else
                                        parametro.Value = item.Explicacao;
                                }
                                catch { parametro.Value = " "; }
                            }
                            parametro.OracleType = OracleType.VarChar;
                            parametros.Add(parametro);
                        }
                        catch { }
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
        }

        public class Clientes
        {
            IDataReader dataReader;
            IDataReader dataReaderAux;

            public List<ConciliacaoContabil.Clientes.Resumo> Listar(string empresa, int plano,
                                                                    string referencia, string referenciaIni, string tipoDocto,
                                                                    string classIni, string classFin,
                                                                    DateTime dataEntrada, DateTime dataEmissao, bool consolidar, bool naoConfirmados,
                                                                    int idEmpresa, bool incluirDoctoSubstituidos)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Clientes.Resumo> lista = new List<ConciliacaoContabil.Clientes.Resumo>();
                Publicas.mensagemDeErro = string.Empty;

                try
                {
                    query.Append("Select x.Codcontactb");
                    query.Append("     , cc.Id, cc.idempresa, cc.idusuario, cc.referencia, cc.textoexplicativo, cc.confirmado");
                    query.Append("     , Sum(x.valorCRC) valorCRC, Sum(x.ValorCTB) ValorCTB");
                    query.Append("  From NIFF_CTB_ConciliacaoCli cc");
                    query.Append("     , Niff_Chm_Empresas e");
                    query.Append("     , (Select Distinct codcontactb, nroplano");
                    query.Append("          From CRCContactb_Cliente) cf");
                    query.Append("             , (Select Codcontactb");
                    query.Append("             , Sum(decode(Substr(NrodoctoCRC, 1, 3), 'DV-', -1,1) * Total_Docto)");
                    query.Append("               - Sum(Valor_devol)  ");
                    query.Append("               - Sum(decode(Substr(NrodoctoCRC, 1, 3), 'DV-', -1,1) * nvl(dv_desconto,0)) - Sum(VlrirrfCRC) - Sum(VlrinssCRC)  - sum(VlrissCRC) As ValorCRC, 0 ValorCTB");
                    query.Append("                  from (Select NrodoctoCRC, NroparcelaCRC, SeriedoctoCRC, d.Codtpdoc, Cf.Codcontactb");
                    query.Append("                            , Fc_CRC_Vlrliquido(d.CoddoctoCRC) As Vlr_Liquido_Real");
                    query.Append("                            , Sum(Valoritemdoc) - decode(sum(Valor_Adto), Sum(Valoritemdoc),  sum(Valor_Adto),0 ) Total_Docto");
                    query.Append("                            , SaidaCRC SaidaCRC, EmissaoCRC, VencimentoCRC, StatusdoctoCRC, RecebimentoCRC");
                    query.Append("                            , DescontoCRC, AcrescimoCRC, VlrinssCRC, VlrirrfCRC, ObsdoctoCRC, NrCli");
                    query.Append("                            , RsocialCli, NfantasiaCli, m.Codbanco, m.Docmovtobco, d.Codigofl, VlrpisCRC");
                    query.Append("                            , VlrcofinsCRC, VlrcslCRC, VlrissCRC, t.Substituitpdoc, Nvl(Valor_Adto, 0) Valor_Adto");
                    query.Append("                            , Nvl(Valor_Devol, 0) Valor_Devol, d.CoddoctoCRC, d.UsuarioCRC_Exc, d.DatahoraCRC_Exc");
                    query.Append("                            , Case When Substr(NrodoctoCRC, 1, 3) = 'DV-' Then");
                    query.Append("                                 Decode(d.CoddoctoCRC_Devol, Null, 'N', 'S')");
                    query.Append("                              Else");
                    query.Append("                                 Decode(d.CoddoctoCRC_Adto, Null, 'N', 'S') End As Dv_Adt_Associada");
                    query.Append("                            , c.Classificador, c.Nomeconta");
                    query.Append("                            , Case When(Substr(NrodoctoCRC, 1, 3) = 'DV-') And  (d.CoddoctoCRC_Devol <> 0) Then");
                    query.Append("                                 (Select Sum(Fc_CRC_Vlrliquido(d.CoddoctoCRC))");
                    query.Append("                                    From CRCdocto a ");
                    query.Append("                                   Where a.CoddoctoCRC = d.CoddoctoCRC_Devol");
                    query.Append("                                     And ((a.RecebimentoCRC > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')) Or(a.RecebimentoCRC Is Null))");
                    query.Append("                                     And a.SaidaCRC <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                                     And a.Codtpdoc Not In (" + tipoDocto + ")");
                    query.Append("                                     And a.StatusdoctoCRC <> 'C')");
                    query.Append("                              Else");
                    query.Append("                                 0 End As Dv_Desconto");

                    query.Append("                         From CRCdocto d, CRCitdoc i, Bgm_Cliente f, Bcomovto m, Cprtpdoc t, CRCcontactb_Cliente Cf, Ctbconta c");
                    query.Append("                        where d.Codtpdoc Not In (" + tipoDocto + ")");
                    query.Append("                          and EmissaoCRC >= to_date('" + dataEmissao.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                          and SaidaCRC <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                          and ((Substr(d.NrodoctoCRC, 1, 3) = 'DV-') ");
                    query.Append("                          and ((d.CoddoctoCRC_Devol Is Null) ");
                    query.Append("                           Or   ((Select Count(CoddoctoCRC) ");
                    query.Append("                                    From CRCdocto Doc");
                    query.Append("                                   Where Doc.CoddoctoCRC = d.CoddoctoCRC_Devol");
                    query.Append("                                     And (Doc.RecebimentoCRC > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("                                      Or  Doc.RecebimentoCRC Is Null)) <> 0))");
                    query.Append("                           Or (Substr(d.NrodoctoCRC, 1, 3) <> 'DV-'");
                    query.Append("                          And (RecebimentoCRC Is Null ");
                    query.Append("                           Or  RecebimentoCRC > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy'))))");
                    query.Append("                          And StatusdoctoCRC <> 'C'");
                    if (!incluirDoctoSubstituidos)
                        query.Append("                          And d.CoddoctoCRCsubst Is Null");

                    if (!consolidar)
                        query.Append("                          And lpad(d.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("                          And lpad(d.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                          And f.codcli = d.codcli ");
                    query.Append("                          And m.Codmovtobco(+) = d.Codmovtobco ");
                    query.Append("                          And t.Codigoempresa = d.Codigoempresa");
                    query.Append("                          And t.Codigofl = d.Codigofl");
                    query.Append("                          And t.Codtpdoc = d.Codtpdoc");
                    query.Append("                          And i.CoddoctoCRC = d.CoddoctoCRC");
                    query.Append("                          And Cf.CodCli = f.CodCli");
                    query.Append("                          And c.Codcontactb = Cf.Codcontactb");
                    query.Append("                          And c.Nroplano = Cf.Nroplano");
                    query.Append("                          And Cf.Nroplano = " + plano);
                    query.Append("                          And c.Classificador Between '" + classIni + "' And '" + classFin + "'");
                    query.Append("                        Group By NrodoctoCRC, NroparcelaCRC, SeriedoctoCRC, d.Codtpdoc, Cf.Codcontactb");
                    query.Append("                            , SaidaCRC, EmissaoCRC, VencimentoCRC, StatusdoctoCRC, RecebimentoCRC, DescontoCRC");
                    query.Append("                            , AcrescimoCRC, VlrinssCRC, VlrirrfCRC, ObsdoctoCRC, NrCli, RsocialCli, NfantasiaCli");
                    query.Append("                            , m.Codbanco, m.Docmovtobco, d.Codigofl, VlrpisCRC, VlrcofinsCRC, VlrcslCRC, VlrissCRC");
                    query.Append("                            , t.Substituitpdoc, Nvl(Valor_Adto, 0), Nvl(Valor_Devol, 0), d.CoddoctoCRC, d.UsuarioCRC_Exc");
                    query.Append("                            , d.DatahoraCRC_Exc, d.CoddoctoCRC_Devol, d.CoddoctoCRC_Adto, c.Classificador, c.Nomeconta");
                    query.Append("              ) group by CodContaCTB");

                    query.Append("                 Union All");
                    query.Append("                Select x.codcontactb, 0 VAlorCRC, Sum(resultado) + Sum(saldoini) ValorCTB            ");
                    query.Append("                  From (Select ctb.codcontactb");
                    query.Append("                             , Sum(s.vldebitosaldo) - Sum(s.vlcreditosaldo) resultado");
                    query.Append("                             , 0 saldoini");
                    query.Append("                             , Sum(s.vldebitosaldo) debito");
                    query.Append("                             , Sum(s.vlcreditosaldo) credito");
                    query.Append("                          From Ctbconta ctb, ctbsaldo s");
                    query.Append("                         Where ctb.nroplano = " + plano);
                    query.Append("                           And s.nroplano = ctb.nroplano ");
                    query.Append("                           And s.periodosaldo = '" + referencia + "'");
                    query.Append("                           And s.codcontactb = ctb.codcontactb");

                    if (!consolidar)
                        query.Append("                           And lpad(s.codigoempresa,3,'0') || '/' || lpad(s.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("                           And lpad(s.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                           And ctb.classificador Between '" + classIni + "' And '" + classFin + "'");
                    query.Append("                         Group By ctb.codcontactb");
                    query.Append("                         Union All");
                    query.Append("                        Select ctb.codcontactb");
                    query.Append("                             , 0 resultado");
                    query.Append("                             , Sum(s.vldebantsaldo) - Sum(s.vlcredantsaldo) saldoini");
                    query.Append("                             , 0 debito");
                    query.Append("                             , 0 credito");
                    query.Append("                          From Ctbconta ctb, ctbsaldo s");
                    query.Append("                         Where ctb.nroplano = " + plano);
                    query.Append("                           And s.nroplano = ctb.nroplano");
                    query.Append("                           And s.periodosaldo Between '" + referenciaIni + "' And '" + referencia + "'");
                    query.Append("                           And s.codcontactb = ctb.codcontactb");

                    if (!consolidar)
                        query.Append("                           And lpad(s.codigoempresa,3,'0') || '/' || lpad(s.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("                           And lpad(s.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("                           And ctb.classificador Between '" + classIni + "' And '" + classFin + "'");
                    query.Append("                         Group By ctb.codcontactb) x");
                    query.Append("      Group By codcontactb) x");
                    query.Append("      Where cf.codcontactb = x.codcontactb");
                    query.Append("        And cc.codcontactb(+) = cf.codcontactb");
                    query.Append("        And cc.nroplano(+) = cf.nroplano");
                    query.Append("        And cf.nroplano = " + plano);
                    query.Append("        And cc.Idempresa(+) = e.Idempresa");
                    query.Append("        And e.idempresa = " + idEmpresa);

                    // query.Append("   And cf.CodContaCTB = 16012");

                    query.Append(" Group By x.Codcontactb");
                    query.Append("     , cc.Id, cc.idempresa, cc.idusuario, cc.referencia, cc.textoexplicativo, cc.confirmado");

                    if (naoConfirmados)
                        query.Append(" Having (cc.confirmado = 'N' Or cc.confirmado Is Null)");

                    // tem que ter a conta contabil associado ao cliente senão não traz no grid, mesmo tendo saldo na contabilidade
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Clientes.Resumo _tipo = new ConciliacaoContabil.Clientes.Resumo();

                            try
                            {
                                _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Referencia = Convert.ToInt32(dataReader["Referencia"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Explicacao = dataReader["textoexplicativo"].ToString();
                            }
                            catch { }

                            try
                            {
                                _tipo.Conferido = dataReader["confirmado"].ToString() == "S";
                            }
                            catch { }

                            _tipo.Existe = _tipo.Id != 0;

                            _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());

                            RateioBeneficios.ContasContabeis _conta = new RateioBeneficios.ContasContabeis();
                            try
                            {
                                _conta = new RateioBeneficiosDAO().Consulta(plano, _tipo.ContaContabil);
                                _tipo.Descricao = _conta.Codigo + " " + _conta.Nome;
                            }
                            catch { }

                            //_tipo.ValorCRC = Math.Abs(Convert.ToDecimal(dataReader["valorCRC"].ToString()));
                            _tipo.ValorCRC = Convert.ToDecimal(dataReader["valorCRC"].ToString());
                            _tipo.ValorCTB = Convert.ToDecimal(dataReader["ValorCTB"].ToString());

                            _tipo.Diferencas = _tipo.ValorCTB != _tipo.ValorCRC;
                            _tipo.Diferenca = _tipo.ValorCRC - _tipo.ValorCTB;

                            if (_tipo.ValorCTB != 0 || _tipo.ValorCRC != 0)
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

            public List<ConciliacaoContabil.Clientes.CliAssociados> ListarClientes(int plano)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Clientes.CliAssociados> lista = new List<ConciliacaoContabil.Clientes.CliAssociados>();
                Publicas.mensagemDeErro = string.Empty;

                try
                {
                    // busca os fornecedores associados a contas contabeis. 
                    query.Clear();
                    query.Append("Select cf.codcontactb,  f.nrCli, f.rsocialCli, f.CodCli ");
                    query.Append("  from Bgm_Cliente f, CRCContactb_Cliente Cf");
                    query.Append(" Where f.CodCli = cf.CodCli");
                    query.Append("  And cf.nroplano = " + plano);

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {

                            ConciliacaoContabil.Clientes.CliAssociados _tipo = new ConciliacaoContabil.Clientes.CliAssociados();

                            _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                            _tipo.Cliente = dataReader["NrCli"].ToString() + " - " + dataReader["RsocialCli"].ToString();
                            _tipo.Id = Convert.ToInt32(dataReader["CodCli"].ToString());
                            _tipo.Plano = plano;

                            lista.Add(_tipo);

                        }
                    }

                }
                catch (Exception ex)
                {
                    Publicas.mensagemDeErro = ex.Message;
                    return lista;
                }
                return lista; 

            }

            public List<ConciliacaoContabil.Clientes.Detalhe> ListarDetalhes(string empresa, int plano,
                                                                                string tipoDocto,
                                                                                string classIni, string classFin,
                                                                                DateTime dataEntrada, DateTime dataEmissao, bool consolidar, bool naoConfirmados,
                                                                                int idEmpresa, bool incluirDoctoSubstituidos)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                List<ConciliacaoContabil.Clientes.Detalhe> lista = new List<ConciliacaoContabil.Clientes.Detalhe>();
                Publicas.mensagemDeErro = string.Empty;

                try
                {
                    query.Append("Select NrodoctoCRC, codlanca, NroparcelaCRC, SeriedoctoCRC, Codtpdoc, Codcontactb, CODDOCTOESF, coddoctoCRC");
                    query.Append("     , Vlr_Liquido_Real");
                    query.Append("     , decode(Substr(NrodoctoCRC, 1, 3), 'DV-', -1, 1) * Total_Docto");
                    query.Append("       - nvl(VlrinssCRC,0) - Nvl(VlrirrfCRC,0) - nvl(VlrissCRC,0)");
                    query.Append("       - decode(Valor_Adto, Total_Docto, Nvl(Valor_Adto,0), 0)");
                    query.Append("       - nvl(Valor_devol,0) ");
                    query.Append("       - decode(Substr(NrodoctoCRC, 1, 3), 'DV-', -1, 1) * nvl(dv_desconto, 0) ValorCRC");
                    query.Append("     , Nvl(vlmovtobco,0) Valorbco");
                    query.Append("     , sistema, SaidaCRC, EmissaoCRC, VencimentoCRC, StatusdoctoCRC, RecebimentoCRC");
                    query.Append("     , ObsdoctoCRC, NrCli, RSocialCli, Docmovtobco, Codbanco, codlancaBCO");
                    query.Append("  From ( ");
                    query.Append("Select NrodoctoCRC, NroparcelaCRC, SeriedoctoCRC, d.Codtpdoc, Cf.Codcontactb");
                    query.Append("     , d.CODDOCTOESF, decode(d.Codlanca, Null, m.codlanca, d.CodLanca) codlanca, d.sistema, m.Codlanca codlancaBCO");
                    query.Append("     , Fc_CRC_Vlrliquido(d.CoddoctoCRC) As Vlr_Liquido_Real");
                    query.Append("     , Sum(Valoritemdoc) - decode(sum(Valor_Adto), Sum(Valoritemdoc),  sum(Valor_Adto),0 ) Total_Docto");
                    query.Append("     , SaidaCRC SaidaCRC, EmissaoCRC, VencimentoCRC, StatusdoctoCRC, RecebimentoCRC");
                    query.Append("     , DescontoCRC, AcrescimoCRC, VlrinssCRC, VlrirrfCRC, ObsdoctoCRC, NrCli");
                    query.Append("     , RSocialCli, NfantasiaCli, m.Codbanco, m.Docmovtobco, Sum(m.vlmovtobco) vlmovtobco, d.Codigofl, VlrpisCRC");
                    query.Append("     , VlrcofinsCRC, VlrcslCRC, VlrissCRC, t.Substituitpdoc, Nvl(Valor_Adto, 0) Valor_Adto");
                    query.Append("     , Nvl(Valor_Devol, 0) Valor_Devol, d.CoddoctoCRC, d.UsuarioCRC_Exc, d.DatahoraCRC_Exc");
                    query.Append("     , Case When Substr(NrodoctoCRC, 1, 3) = 'DV-' Then");
                    query.Append("          Decode(d.CoddoctoCRC_Devol, Null, 'N', 'S')");
                    query.Append("       Else");
                    query.Append("          Decode(d.CoddoctoCRC_Adto, Null, 'N', 'S') End As Dv_Adt_Associada");
                    query.Append("     , c.Classificador, c.Nomeconta");
                    query.Append("     , Case When(Substr(NrodoctoCRC, 1, 3) = 'DV-') And  (d.CoddoctoCRC_Devol <> 0) Then");
                    query.Append("          (Select Sum(Fc_CRC_Vlrliquido(d.CoddoctoCRC))");
                    query.Append("             From CRCdocto a ");
                    query.Append("            Where a.CoddoctoCRC = d.CoddoctoCRC_Devol");
                    query.Append("              And ((a.RecebimentoCRC > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')) Or(a.RecebimentoCRC Is Null))");
                    query.Append("              And a.SaidaCRC <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("              And a.Codtpdoc Not In (" + tipoDocto + ")");
                    query.Append("              And a.StatusdoctoCRC <> 'C')");
                    query.Append("       Else");
                    query.Append("          0 End As Dv_Desconto");

                    query.Append("  From CRCdocto d, CRCitdoc i, Bgm_Cliente f, Bcomovto m, Cprtpdoc t, CRCcontactb_Cliente Cf, Ctbconta c");
                    query.Append(" where d.Codtpdoc Not In (" + tipoDocto + ")");
                    query.Append("   and EmissaoCRC >= to_date('" + dataEmissao.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   and SaidaCRC <= to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   and ((Substr(d.NrodoctoCRC, 1, 3) = 'DV-') ");
                    query.Append("   and ((d.CoddoctoCRC_Devol Is Null) ");
                    query.Append("    Or   ((Select Count(CoddoctoCRC) ");
                    query.Append("             From CRCdocto Doc");
                    query.Append("            Where Doc.CoddoctoCRC = d.CoddoctoCRC_Devol");
                    query.Append("              And (Doc.RecebimentoCRC > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("               Or  Doc.RecebimentoCRC Is Null)) <> 0))");
                    query.Append("    Or (Substr(d.NrodoctoCRC, 1, 3) <> 'DV-'");
                    query.Append("   And (RecebimentoCRC Is Null ");
                    query.Append("    Or  RecebimentoCRC > to_date('" + dataEntrada.ToShortDateString() + "','dd/mm/yyyy'))))");
                    query.Append("   And StatusdoctoCRC <> 'C'");

                    if (!incluirDoctoSubstituidos)
                        query.Append("   And d.CoddoctoCRCsubst Is Null");

                    if (!consolidar)
                        query.Append("   And lpad(d.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = '" + empresa + "'");
                    else
                        query.Append("   And lpad(d.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");

                    query.Append("   And f.CodCli = d.CodCli ");
                    query.Append("   And m.Codmovtobco(+) = d.Codmovtobco ");
                    query.Append("   And t.Codigoempresa = d.Codigoempresa");
                    query.Append("   And t.Codigofl = d.Codigofl");
                    query.Append("   And t.Codtpdoc = d.Codtpdoc");
                    query.Append("   And i.CoddoctoCRC = d.CoddoctoCRC");
                    query.Append("   And Cf.CodCli = f.CodCli");
                    query.Append("   And c.Codcontactb = Cf.Codcontactb");
                    query.Append("   And c.Nroplano = Cf.Nroplano");
                    query.Append("   And Cf.Nroplano = " + plano);
                    query.Append("   And c.Classificador Between '" + classIni + "' And '" + classFin + "'");

                    //query.Append("   And c.CodContaCTB = 16012");
                    query.Append(" Group By NrodoctoCRC, NroparcelaCRC, SeriedoctoCRC, d.Codtpdoc, Cf.Codcontactb");
                    query.Append("     , SaidaCRC, EmissaoCRC, VencimentoCRC, StatusdoctoCRC, RecebimentoCRC, DescontoCRC");
                    query.Append("     , AcrescimoCRC, VlrinssCRC, VlrirrfCRC, ObsdoctoCRC, NrCli, RSocialCli, NfantasiaCli");
                    query.Append("     , m.Codbanco, m.Docmovtobco, d.Codigofl, VlrpisCRC, VlrcofinsCRC, VlrcslCRC, VlrissCRC");
                    query.Append("     , t.Substituitpdoc, Nvl(Valor_Adto, 0), Nvl(Valor_Devol, 0), d.CoddoctoCRC, d.UsuarioCRC_Exc");
                    query.Append("     , d.DatahoraCRC_Exc, d.CoddoctoCRC_Devol, d.CoddoctoCRC_Adto, c.Classificador, c.Nomeconta");
                    query.Append("     , d.CODDOCTOESF, d.codlanca, d.sistema, m.codlanca");
                    query.Append("     ) order by codcontactb, NrodoctoCRC");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            ConciliacaoContabil.Clientes.Detalhe _tipo = new ConciliacaoContabil.Clientes.Detalhe();

                            _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                            _tipo.Docto = dataReader["NrodoctoCRC"].ToString();
                            _tipo.DoctoCRC = dataReader["NrodoctoCRC"].ToString() + "/" + dataReader["NroparcelaCRC"].ToString() + " - " + dataReader["SeriedoctoCRC"].ToString();

                            try
                            {
                                _tipo.ValorCRC = Convert.ToDecimal(dataReader["ValorCRC"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ValorBCO = Convert.ToDecimal(dataReader["ValorBCO"].ToString());
                            }
                            catch { }

                            _tipo.Cliente = dataReader["NrCli"].ToString() + " - " + dataReader["RsocialCli"].ToString();
                            _tipo.TipoDocto = dataReader["Codtpdoc"].ToString();

                            try
                            {
                                _tipo.DoctoBCO = dataReader["Docmovtobco"].ToString();
                            }
                            catch { }

                            _tipo.Origem = dataReader["Sistema"].ToString();
                            _tipo.Observacao = dataReader["ObsdoctoCRC"].ToString();

                            _tipo.Saida = Convert.ToDateTime(dataReader["SaidaCRC"].ToString());
                            _tipo.Emissao = Convert.ToDateTime(dataReader["EmissaoCRC"].ToString());
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["VencimentoCRC"].ToString());
                             
                            try
                            {
                                _tipo.CodLanca = Convert.ToDecimal(dataReader["CodLanca"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.CodLancaBCO = Convert.ToDecimal(dataReader["CodLancaBCO"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.CodDoctoESF = Convert.ToDecimal(dataReader["CodDoctoESF"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.CodDoctoCRC = Convert.ToDecimal(dataReader["CodDoctoCRC"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Recebimento = Convert.ToDateTime(dataReader["RecebimentoCRC"].ToString());
                            }
                            catch { }

                            if (_tipo.CodLanca != 0)
                            {
                                query.Clear();
                                query.Append("Select documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', 1, -1) * il.vritemlanca) valorctb");
                                query.Append("  from ctblanca l, ctbitlnc il");
                                query.Append(" where l.codlanca = " + _tipo.CodLanca);
                                query.Append("   And il.codlanca = l.codlanca");
                                query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                query.Append("  group by documentolanca");

                                executar = sessao.CreateQuery(query.ToString());

                                dataReaderAux = executar.ExecuteQuery();

                                using (dataReaderAux)
                                {
                                    if (dataReaderAux.Read())
                                    {
                                        _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                        _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());

                                        if (_tipo.CodLanca != 0)
                                        {
                                            query.Clear();
                                            query.Append("Select documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', 1, -1) * il.vritemlanca) valorctb");
                                            query.Append("  from ctblanca l, ctbitlnc il");
                                            query.Append(" where l.codlanca = " + _tipo.CodLanca);
                                            query.Append("   And il.codlanca = l.codlanca");
                                            query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                            query.Append("  group by documentolanca");

                                            executar = sessao.CreateQuery(query.ToString());

                                            dataReaderAux = executar.ExecuteQuery();

                                            using (dataReaderAux)
                                            {
                                                while (dataReaderAux.Read())
                                                {
                                                    _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                    _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                    if (_tipo.ValorCRC == _tipo.ValorCTB)
                                                        break;
                                                }
                                            }
                                        }

                                        if (_tipo.Recebimento > dataEntrada)
                                            _tipo.ValorCTB = 0;
                                    }
                                }
                            }
                            else
                            {
                                if (_tipo.Origem == "ESF" && _tipo.CodLanca == 0)
                                {
                                    if (_tipo.CodDoctoESF != 0)
                                    {
                                        query.Clear();
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Esfnotafiscal n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctoesf = " + _tipo.CodDoctoESF);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.documentolanca, l.codlanca");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReaderAux = executar.ExecuteQuery();

                                        using (dataReaderAux)
                                        {
                                            if (dataReaderAux.Read())
                                            {
                                                _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        query.Clear();
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Esfiss n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctoCRC = " + _tipo.CodDoctoCRC);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.documentolanca, l.codlanca");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReaderAux = executar.ExecuteQuery();

                                        using (dataReaderAux)
                                        {
                                            if (dataReaderAux.Read())
                                            {
                                                _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (_tipo.Origem == "EST" && _tipo.CodLanca == 0)
                                    {
                                        query.Clear();
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Bgm_Notafiscal n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctocrc = " + _tipo.CodDoctoCRC);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.codlanca, l.documentolanca");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReaderAux = executar.ExecuteQuery();

                                        using (dataReaderAux)
                                        {
                                            if (dataReaderAux.Read())
                                            {
                                                _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                            }
                                        }

                                        if (_tipo.ValorCTB == 0)
                                        {
                                            query.Clear();
                                            query.Append("Select documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', 1, -1) * il.vritemlanca) valorctb");
                                            query.Append("  from ctblanca l, ctbitlnc il");
                                            query.Append(" where l.codlanca = " + _tipo.CodLancaBCO);
                                            query.Append("   And il.codlanca = l.codlanca");
                                            query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                            query.Append("   And il.historicoitemlanca like '%" + _tipo.Docto + "%'");
                                            query.Append("  group by documentolanca");

                                            executar = sessao.CreateQuery(query.ToString());

                                            dataReaderAux = executar.ExecuteQuery();

                                            using (dataReaderAux)
                                            {
                                                while (dataReaderAux.Read())
                                                {
                                                    _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                    _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                    _tipo.CodLanca = _tipo.CodLancaBCO;
                                                    if (_tipo.ValorCRC == _tipo.ValorCTB)
                                                        break;
                                                }
                                            }
                                            if (_tipo.ValorCRC != _tipo.ValorCTB && (_tipo.DoctoCTB != null))
                                                _tipo.ValorCTB = _tipo.ValorCRC;
                                        }
                                    }

                                    if (_tipo.Origem == "FRE" && _tipo.CodLanca == 0)
                                    {
                                        query.Clear();
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Fre_Notafiscalavulsa n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.nrdoctocrc = " + _tipo.CodDoctoCRC);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.codlanca, l.documentolanca");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReaderAux = executar.ExecuteQuery();

                                        using (dataReaderAux)
                                        {
                                            if (dataReaderAux.Read())
                                            {
                                                _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                            }
                                        }
                                    }

                                    if (_tipo.Origem == "TUR" && _tipo.CodLanca == 0)
                                    {
                                        query.Clear();
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Tur_Notafiscal n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctocrc = " + _tipo.CodDoctoCRC);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.codlanca, l.documentolanca");
                                        query.Append(" Union All ");
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Tur_Reparcelamento n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctocrc = " + _tipo.CodDoctoCRC);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.codlanca, l.documentolanca");
                                        query.Append(" Union All ");
                                        query.Append("Select l.codlanca, l.documentolanca, Sum(decode(il.debitocreditoitemlanca, 'C', -1, 1) * il.vritemlanca) valorctb");
                                        query.Append("  from Tur_Pagamentos n, ctblanca l, ctbitlnc il");
                                        query.Append(" where n.coddoctocrc = " + _tipo.CodDoctoCRC);
                                        query.Append("   And l.codlanca = n.codlanca");
                                        query.Append("   And il.codlanca = l.codlanca");
                                        query.Append("   And il.codcontactb = " + _tipo.ContaContabil);
                                        query.Append("  group by l.codlanca, l.documentolanca");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReaderAux = executar.ExecuteQuery();

                                        using (dataReaderAux)
                                        {
                                            if (dataReaderAux.Read())
                                            {
                                                _tipo.DoctoCTB = dataReaderAux["documentolanca"].ToString();
                                                _tipo.ValorCTB = Convert.ToDecimal(dataReaderAux["valorctb"].ToString());
                                                _tipo.CodLanca = Convert.ToDecimal(dataReaderAux["CodLanca"].ToString());
                                            }
                                        }
                                    }
                                }
                            }

                            lista.Add(_tipo);
                        }
                    }

                    try
                    {
                        // busca os lançamentos contabeis. 
                        query.Clear();
                        query.Append("Select l.documentolanca, l.dtlanca, l.sistema, i.historicoitemlanca, l.codlanca");
                        query.Append("     , decode(i.debitocreditoitemlanca, 'C', -1, 1) * i.vritemlanca valorctb,  c.codcontactb ");
                        query.Append("  from ctbLanca l, ctbitlnc i, ctbconta c");
                        query.Append(" Where l.codlanca = i.codlanca");
                        query.Append("  And c.nroplano = " + plano);
                        query.Append("  And c.nroplano = i.nroplano");
                        query.Append("  And c.codcontactb = i.codcontactb");
                        query.Append("  And c.Classificador Between '" + classIni + "' And '" + classFin + "'");
                        query.Append("  And l.dtlanca Between to_date('" + dataEntrada.AddMonths(-1).AddDays(1).ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And to_date('" + dataEntrada.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And sistema not in ('CRC','CPG') ");
                        query.Append("  And debitocreditoitemlanca = 'C'");

                        if (!consolidar)
                            query.Append("   And lpad(l.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = '" + empresa + "'");
                        else
                            query.Append("   And lpad(l.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");
                       // query.Append("   And c.CodContaCTB = 16012");

                        query.Append(" Union All  "); // incluido para atender a necessidade da Rapido lançamentos feitos no CTB a credito
                        query.Append("Select l.documentolanca, l.dtlanca, l.sistema, i.historicoitemlanca, l.codlanca");
                        query.Append("     , decode(i.debitocreditoitemlanca, 'C', -1, 1) * i.vritemlanca valorctb,  c.codcontactb ");
                        query.Append("  from ctbLanca l, ctbitlnc i, ctbconta c");
                        query.Append(" Where l.codlanca = i.codlanca");
                        query.Append("  And c.nroplano = " + plano);
                        query.Append("  And c.nroplano = i.nroplano");
                        query.Append("  And c.codcontactb = i.codcontactb");
                        query.Append("  And c.Classificador Between '" + classIni + "' And '" + classFin + "'");
                        query.Append("  And l.dtlanca Between to_date('" + dataEntrada.AddMonths(-1).AddDays(1).ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And to_date('" + dataEntrada.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  And sistema in ('CTB','CGS')");
                        query.Append("  And debitocreditoitemlanca = 'D'");

                        if (!consolidar)
                            query.Append("   And lpad(l.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = '" + empresa + "'");
                        else
                            query.Append("   And lpad(l.codigoempresa,3,'0') = '" + empresa.Substring(0, 3) + "'");
                        //query.Append("   And c.CodContaCTB = 16012");

                        executar = sessao.CreateQuery(query.ToString());

                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            while (dataReader.Read())
                            {
                                decimal codlanca = Convert.ToDecimal(dataReader["CodLanca"].ToString());
                                bool encontrou = false;
                                bool encontrouCGS = false;

                                foreach (var item in lista.Where(w => w.CodLanca == codlanca && w.Origem != "CTB" && w.Origem != "CGS"))
                                {
                                    encontrou = true;
                                }

                                if (!encontrou)
                                {
                                    ConciliacaoContabil.Clientes.Detalhe _tipo = new ConciliacaoContabil.Clientes.Detalhe();

                                    _tipo.CodLanca = codlanca;
                                    _tipo.ContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                                    _tipo.DoctoCTB = dataReader["documentolanca"].ToString();
                                    _tipo.ValorCTB = Convert.ToDecimal(dataReader["valorctb"].ToString());
                                    _tipo.Observacao = dataReader["historicoitemlanca"].ToString();
                                    _tipo.Origem = dataReader["Sistema"].ToString();
                                    _tipo.Saida = Convert.ToDateTime(dataReader["dtlanca"].ToString());

                                    string numeroCTR = "";

                                    // Verifica se o CTE esta associado a algum Docto do CRC
                                    if (_tipo.Origem == "CGS")
                                    {
                                        string obs = _tipo.Observacao; 

                                        if (_tipo.Observacao.Contains("RECEBIMENTO DE CTE"))
                                            numeroCTR = Publicas.OnlyNumbers(obs.Replace(@"/1",""));
                                        else
                                            numeroCTR = Publicas.OnlyNumbers(obs);

                                        foreach (var item in lista.Where(w => w.Observacao.Contains(numeroCTR) && w.Origem == "CGS"))
                                        {
                                            encontrouCGS = true;
                                            _tipo = item;
                                        }

                                        if (encontrouCGS)
                                        {
                                            lista.Remove(_tipo);
                                            continue;
                                        }

                                        try
                                        {
                                            query.Clear(); 
                                            query.Append("Select v.Conhecimento Ctrc, v.Coddoctocrc");
                                            query.Append("     , d.NrodoctoCRC, d.NroparcelaCRC, d.SeriedoctoCRC");
                                            query.Append("     , SaidaCRC, EmissaoCRC, VencimentoCRC, d.Codtpdoc, RecebimentoCRC");
                                            query.Append("     , Case v.Tipo_Docto When 1 Then 'CTRC' ");
                                            query.Append("                         When 8 Then 'NF'");
                                            query.Append("                         When 15 Then 'NFS'");
                                            query.Append("                         When 57 Then 'CTe' Else '' End Tipo_Docto");
                                            query.Append("     , Case Status When 'I' Then 'Inutilizado'");
                                            query.Append("                   When 'P' Then 'Pendente'");
                                            query.Append("                   When 'R' Then 'Rejeitado'");
                                            query.Append("                   When 'A' Then 'Autorizado'");
                                            query.Append("                   When 'C' Then 'Cancelado'");
                                            query.Append("                   When 'D' Then 'Denegado'");
                                            query.Append("                   When 'L' Then 'Lote em proc.'");
                                            query.Append("                   When 'N' Then 'Não enviado' Else '' End Autorizado");
                                            query.Append("  From Vw_Crc_Documentos_Cgs v, Crc_Doctosassoccgs a, Crcdocto d, Fta013 t");
                                            query.Append(" Where v.Coddoctocrc = t.Coddoctocrc ");
                                            query.Append("   And d.Coddoctocrc = v.CodDoctoCRC");
                                            query.Append("   And a.Empresa(+) = v.Empresa");
                                            query.Append("   And a.Filial(+) = v.Filial");
                                            query.Append("   And a.Filial(+) = v.Filial");
                                            query.Append("   And a.Garagem(+) = v.Garagem");
                                            query.Append("   And a.Serie(+) = v.Serie");
                                            query.Append("   And a.Conhecimento(+) = v.Conhecimento");
                                            query.Append("   And v.Conhecimento = " + numeroCTR);
                                            query.Append("   And lpad(v.empresa,3,'0') = '" + empresa.Substring(0, 3) +"'");
                                            
                                            executar = sessao.CreateQuery(query.ToString());

                                            dataReaderAux = executar.ExecuteQuery();

                                            using (dataReaderAux)
                                            {
                                                if (dataReaderAux.Read())
                                                {
                                                    
                                                    _tipo.Docto = dataReaderAux["NrodoctoCRC"].ToString();
                                                    _tipo.DoctoCRC = dataReaderAux["NrodoctoCRC"].ToString() + "/" + dataReaderAux["NroparcelaCRC"].ToString() + " - " + dataReaderAux["SeriedoctoCRC"].ToString();

                                                    _tipo.TipoDocto = dataReaderAux["Codtpdoc"].ToString();
                                                    _tipo.Saida = Convert.ToDateTime(dataReaderAux["SaidaCRC"].ToString());
                                                    _tipo.Emissao = Convert.ToDateTime(dataReaderAux["EmissaoCRC"].ToString());
                                                    _tipo.Vencimento = Convert.ToDateTime(dataReaderAux["VencimentoCRC"].ToString());

                                                    try
                                                    {
                                                        _tipo.Recebimento = Convert.ToDateTime(dataReaderAux["RecebimentoCRC"].ToString());
                                                    }
                                                    catch { }

                                                    if (_tipo.Saida != null && _tipo.Recebimento != null && _tipo.Saida.Value.Month == _tipo.Recebimento.Value.Month)
                                                        continue;
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Publicas.mensagemDeErro = ex.Message;
                                            return lista;
                                        }

                                    }

                                    lista.Add(_tipo);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Publicas.mensagemDeErro = ex.Message;
                        return lista;
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


            public bool Gravar(List<ConciliacaoContabil.Clientes.Resumo> _lista)
            {
                StringBuilder query = new StringBuilder();
                Sessao sessao = new Sessao();
                Publicas.mensagemDeErro = string.Empty;
                bool retorno = _lista.Count() == 0;

                OracleParameter parametro = new OracleParameter();
                List<OracleParameter> parametros = new List<OracleParameter>();

                try
                {
                    foreach (var item in _lista.Where(w => w.Conferido || w.Existe))
                    {
                        query.Clear();
                        if (!item.Existe)
                        {
                            query.Append("Insert into Niff_Ctb_ConciliacaoCli");
                            query.Append(" ( id, idempresa, referencia, idusuario, codcontactb, Nroplano");
                            query.Append(" , ValorCRC, valorCTB");
                            query.Append(" , confirmado, textoexplicativo");
                            query.Append(") Values ( (Select nvl(Max(id),0) +1 from Niff_Ctb_ConciliacaoCli) ");
                            query.Append("        , " + item.IdEmpresa);
                            query.Append("        , " + item.Referencia);
                            query.Append("        , " + Publicas._usuario.Id);

                            query.Append("        , " + item.ContaContabil);
                            query.Append("        , " + item.Plano);

                            query.Append("        , " + item.ValorCRC.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.ValorCTB.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("        , '" + (item.Conferido ? "S" : "N") + "'");
                            query.Append("        , :texto");

                            query.Append(" )");
                        }
                        else
                        {
                            query.Append("Update Niff_Ctb_ConciliacaoCli");
                            query.Append("   set ValorCRC = " + item.ValorCRC.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , ValorCTB = " + item.ValorCTB.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("     , Confirmado = '" + (item.Conferido ? "S" : "N") + "'");
                            query.Append("     , TextoExplicativo = :texto");

                            query.Append(" Where Id = " + item.Id);
                        }

                        try
                        {
                            parametros.Clear();
                            parametro = new OracleParameter();
                            parametro.ParameterName = ":texto";
                            try
                            {
                                parametro.Value = item.Explicacao.Replace("'", "");
                            }
                            catch
                            {
                                try
                                {
                                    if (item.Explicacao == null)
                                        parametro.Value = " ";
                                    else
                                        parametro.Value = item.Explicacao;
                                }
                                catch { parametro.Value = " "; }
                            }
                            parametro.OracleType = OracleType.VarChar;
                            parametros.Add(parametro);
                        }
                        catch { }
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
        }
    }
}
