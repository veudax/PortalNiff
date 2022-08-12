create or replace view pbi_radar_finan_ebitda as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Round(valor) Valor,
       decode(Percentual,0,Null,Round(Percentual,2)) Percentual,
       Percentual PercentualN,
       Ordem,
       Tipo
  From (Select 'EBITDA' Grupo, 13 Ordem,
               To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
               Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
               Sum(MargemDistribuicao) - Sum(Despesas) Valor,
               Decode(Sum(RecOp), 0, 0, ((Sum(MargemDistribuicao) - Sum(Despesas))/Abs(Sum(RecOp)))*100) Percentual,
               'F' Tipo
          From (Select EmpFil,
                       PeriodoSaldo,
                       Sum(ReceitaLiquida) - Sum(CustoServicosPrestados) MargemDistribuicao,
                       0 Despesas,
                       0 RecOp
                  From (
                     Select EmpFil,
                            periodosaldo,
                            Sum(ReceitaBruta) - Sum(Impostos) - Sum(OrgaoGestor) ReceitaLiquida,
                            0 CustoServicosPrestados
                       From ((Select Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
                                     To_char(Data,'yyyymm') periodosaldo,
                                     Valor ReceitaBruta,
                                     0 Impostos,
                                     0 OrgaoGestor
                                From pbi_radar_finan_receita)
                               Union All
                             (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                     s.periodosaldo,
                                     0 ReceitaBruta,
                                     Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) Impostos,
                                     0 OrgaoGestor
                                From Ctbsaldo s, ctbconta c
                               Where s.nroplano = 10

                                 And s.periodosaldo Between '201705'
                                 And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')

                                 And ((s.codigoempresa || s.codigofl in (11,12,21,41,51,263)
                                 And c.codcontactb In (30223,30202,30198,30199))

                                  Or (s.codigoempresa || s.codigofl in (31,315,61,91,131)
                                 And c.codcontactb In (30223,30224,30198,30199))

                                  Or (s.codigoempresa || s.codigofl in (261)
                                 And c.codcontactb In (30223,30198,30199))
                                 )

                                 And s.nroplano = c.nroplano
                                 And s.codcontactb = c.codcontactb

                               Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                        s.periodosaldo)
                               Union All
                             (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                     s.periodosaldo,
                                     0 ReceitaBruta,
                                     0 Impostos,
                                     Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) OrgaoGestor
                                From Ctbsaldo s, ctbconta c
                               Where s.nroplano = 10
                                 And s.periodosaldo Between '201705'
                                 And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')

                                 And ((s.codigoempresa || s.codigofl in (11,12,21,41,51)
                                 And c.codcontactb In (42135,50458,50623,50620,50503,43639,50499,43635,43636,43637,50446,50445,50461,50447,50621))

                                  Or (s.codigoempresa || s.codigofl in (31,315)
                                 And c.codcontactb In (42135,50321,50458,50623,50620,50503,43639,50499,43635,43636,43637,30689,50445,50670,50461,50447))

                                 Or (s.codigoempresa || s.codigofl in (61)
                                 And c.codcontactb In (42135,50321,50458,50623,50620,50621,50503,43639,50499,43635,43636,43637,50445,50446,50461,50447))

                                 Or (s.codigoempresa || s.codigofl in (91,261)
                                 And c.codcontactb In (42135,/*50321,*/50458,50623,50620,50503,43639,50499,43635,43636,43637,50445,50446,50461,50447))

                                 Or (s.codigoempresa || s.codigofl in (131)
                                 And c.codcontactb In (50447))

                                 -- 263 sem contas para orgão gestor
                                 )

                                 And s.nroplano = c.nroplano
                                 And s.codcontactb = c.codcontactb

                               Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                        s.periodosaldo))
                          Group By EmpFil, PeriodoSaldo
                          Union All
                         Select EmpFil,
                                periodosaldo,
                                0 ReceitaLiquida,
                                CustoServicosPrestados
                           From (Select EmpFil,
                                        PeriodoSaldo,
                                        Sum(CustoFixoDireto) + Sum(CustoVariavelDireto) + Sum(CustoFixoIndireto) CustoServicosPrestados
                                   From ((Select Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
                                                PeriodoSaldo,
                                                Sum(FolhaOper) + Sum(OutrasDesp) + Sum(GestaoFrota) CustoFixoDireto,
                                                0 CustoVariavelDireto,
                                                0 CustoFixoIndireto
                                           From (Select EmpFil,
                                                        To_char(Data, 'yyyymm') PeriodoSaldo,
                                                        Valor FolhaOper,
                                                        0 OutrasDesp,
                                                        0 GestaoFrota
                                                    From Pbi_Radar_Finan_Totfolhaop
                                                   Union All
                                                 (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                         s.periodosaldo,
                                                         0 FolhaOper,
                                                         Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) OutrasDesp,
                                                         0 GestaoFrota
                                                    From Ctbsaldo s, ctbconta c
                                                   Where s.nroplano = 10
                                                     And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                                                     And s.periodosaldo Between '201705'
                                                     And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                     And c.codcontactb In (42092,42093)
                                                     And s.nroplano = c.nroplano
                                                     And s.codcontactb = c.codcontactb
                                                   Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                            s.periodosaldo)
                                                   Union All
                                                 (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                         s.periodosaldo,
                                                         0 FolhaOper,
                                                         0 OutrasDesp,
                                                         Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) GestaoFrota
                                                    From Ctbsaldo s, ctbconta c
                                                   Where s.nroplano = 10
                                                     And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                                                     And s.periodosaldo Between '201705'
                                                     And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                     And c.codcontactb In (50626,/*50321,*/43615,43616,50215,42138,42155,50391,43614)
                                                     And s.nroplano = c.nroplano
                                                     And s.codcontactb = c.codcontactb
                                                   Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                            s.periodosaldo ))
                                           Group By EmpFil, PeriodoSaldo )
                                           Union All
                                         (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                 PeriodoSAldo Data,
                                                 0 CustoFixoDireto,
                                                 Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) CustoVariavelDireto,
                                                 0 CustoFixoIndireto
                                            From Ctbsaldo s, ctbconta c
                                           Where s.nroplano = 10
                                             And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                                             And s.periodosaldo Between '201705'
                                             And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                             And c.codcontactb In (43705,43695,43696,42108,42106,42107,42103,42104,43617,43697,43672,42112)
                                             And s.nroplano = c.nroplano
                                             And s.codcontactb = c.codcontactb
                                           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                    s.periodosaldo)
                                           Union All
                                         (Select Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
                                                 PeriodoSaldo,
                                                 0 CustoFixoDireto,
                                                 0 CustoVariavelDireto,
                                                 Sum(FolhaManutencao) + Sum(OutrasDespPessoal) + Sum(OutrasDespesasOper) + Sum(FrotaApoio) + Sum(DespEquipFerramentas) CustoFixoIndireto
                                            From (Select EmpFil,
                                                        To_char(Data, 'yyyymm') PeriodoSaldo,
                                                        Valor FolhaManutencao,
                                                        0 OutrasDespPessoal,
                                                        0 OutrasDespesasOper,
                                                        0 FrotaApoio,
                                                        0 DespEquipFerramentas
                                                    From Pbi_Radar_Finan_Totfolhaman
                                                    Union All
                                                  (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                          s.periodosaldo,
                                                          0 FolhaManutencao,
                                                          Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo)  OutrasDespPessoal,
                                                          0 OutrasDespesasOper,
                                                          0 FrotaApoio,
                                                          0 DespEquipFerramentas
                                                     From Ctbsaldo s, ctbconta c
                                                    Where s.nroplano = 10
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between '201705'
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (42113,42088,42089)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                             s.periodosaldo)
                                                    Union All
                                                  (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                          s.periodosaldo,
                                                          0 FolhaManutencao,
                                                          0 OutrasDespPessoal,
                                                          Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) OutrasDespesasOper,
                                                          0 FrotaApoio,
                                                          0 DespEquipFerramentas
                                                     From Ctbsaldo s, ctbconta c
                                                    Where s.nroplano = 10
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between '201705'
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (43675,43702,43703,43671,43640,43676)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                             s.periodosaldo)
                                                    Union All
                                                   (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                          s.periodosaldo,
                                                          0 FolhaManutencao,
                                                          0 OutrasDespPessoal,
                                                          0 OutrasDespesasOper,
                                                          Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) FrotaApoio,
                                                          0 DespEquipFerramentas
                                                     From Ctbsaldo s, ctbconta c
                                                    Where s.nroplano = 10
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between '201705'
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (50498,50528,50527,50529,50530)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                             s.periodosaldo)
                                                    Union All
                                                   (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                          s.periodosaldo,
                                                          0 FolhaManutencao,
                                                          0 OutrasDespPessoal,
                                                          0 OutrasDespesasOper,
                                                          0 FrotaApoio,
                                                          Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) DespEquipFerramentas
                                                     From Ctbsaldo s, ctbconta c
                                                    Where s.nroplano = 10
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between '201705'
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (43704,43707,42281,43677,43681,43682)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                             s.periodosaldo)                          )
                                                    Group By EmpFil, PeriodoSaldo
                                          ))
                                  Group By EmpFil, PeriodoSaldo )  )
                 Group By EmpFil, PeriodoSaldo
                 Union All
                Select Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
                       PeriodoSaldo,
                       0 MargemDistribuicao,
                       Sum(FolhaAdm) + Sum(ProcessosJuridicos) + Sum(DespesasAdm) Despesas,
                       0 RecOp
                  From (Select EmpFil,
                               To_char(Data, 'yyyymm') PeriodoSaldo,
                               Valor FolhaAdm,
                               0 ProcessosJuridicos,
                               0 DespesasAdm
                           From Pbi_Radar_Finan_Totfolhaadm
                         Union All
                       (Select EmpFil,
                               PeriodoSaldo,
                               ProcessosJuridicos,
                               0 ProcessosJuridicos,
                               0 DespesasAdm
                           From pbi_radar_EbidaProcJuridicos     )
                         Union All
                       (Select Empfil,
                               To_char(Data, 'yyyymm') PeriodoSaldo,
                               0 FolhaAdm,
                               0 ProcessosJuridicos,
                               Valor DespesasAdm
                          From pbi_radar_finan_despesas) )
                   Group By EmpFil, Periodosaldo
                   Union All
                  Select EmpFil,
                         To_char(Data,'yyyymm'),
                         0 MargemDistribuicao,
                         0 Despesas,
                         Sum(Valor) RecOp
                    From pbi_Radar_finan_Rec_Oper
                   Group By EmpFil, Data  )
           Group By EmpFil, To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') )

