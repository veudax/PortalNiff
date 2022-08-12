create or replace view pbi_radar_finan_ebitda as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select 'EBITDA' Grupo, 13 Ordem,
               To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
               EmpFil,
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
                       From ((Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                     s.periodosaldo,
                                     Sum(s.vlcreditosaldo)- Sum(s.vldebitosaldo) ReceitaBruta,
                                     0 Impostos,
                                     0 OrgaoGestor
                                From Ctbsaldo s, ctbconta c
                               Where s.nroplano = 10
                                 And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                 And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                 And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                 And (c.classificador Like '3.1.1.04%'
                                  Or c.classificador Like '3.1.1.06%'
                                  Or c.classificador Like '3.1.2.07.01%'
                                  Or c.codcontactb In (30236,30240,30227,30461,30228,30229,30330))
                                 And s.nroplano = c.nroplano
                                 And s.codcontactb = c.codcontactb
                                 And c.lancamento = 'S'
                               Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                        s.periodosaldo)
                               Union All
                             (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                     s.periodosaldo,
                                     0 ReceitaBruta,
                                     Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) Impostos,
                                     0 OrgaoGestor
                                From Ctbsaldo s, ctbconta c
                               Where s.nroplano = 10
                                 And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                 And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                 And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                 And (c.classificador Like '3.2.2.01.01.005%'
                                  Or c.classificador Like '3.2.2.01.01.001%'
                                  Or c.classificador Like '3.2.2.01.01.002%'
                                  Or c.codcontactb = 30223)
                                 And s.nroplano = c.nroplano
                                 And s.codcontactb = c.codcontactb
                                 And c.lancamento = 'S'
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
                                 And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                 And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                 And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                 And c.codcontactb In (42135,50321,50458,50623,50620,50503,43639,50499,43635,43636,43637,50446,50445,50461,50447)
                                 And s.nroplano = c.nroplano
                                 And s.codcontactb = c.codcontactb
                                 And c.lancamento = 'S'
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
                                   From ((Select EmpFil,
                                                PeriodoSaldo,
                                                Sum(FolhaOper) + Sum(OutrasDesp) + Sum(GestaoFrota) CustoFixoDireto,
                                                0 CustoVariavelDireto,
                                                0 CustoFixoIndireto
                                           From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                         s.periodosaldo,
                                                         Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) FolhaOper,
                                                         0 OutrasDesp,
                                                         0 GestaoFrota
                                                    From Ctbsaldo s, ctbconta c
                                                   Where s.nroplano = 10
                                                     And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                     And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                     And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                     And (c.classificador Like '4.8.1.01.01.008%'
                                                      Or c.codcontactb In (41953,41975,41955,41988,41983,41963,43711,41985,43679,42005,42013,42007,42001,42009,42011,42139,
                                                                           43698,41990,41977,43693,41979,43694,41992))
                                                     And s.nroplano = c.nroplano
                                                     And s.codcontactb = c.codcontactb
                                                     And c.lancamento = 'S'
                                                   Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                            s.periodosaldo
                                                   Union All
                                                  Select EmpFil,
                                                         periodosaldo,
                                                         Decode(Cta02, 0, 0, (Cta01 / Cta02) * Cta03) FolhaOper,
                                                         0 OutrasDesp,
                                                         0 GestaoFrota
                                                    From (Select EmpFil,
                                                                 periodosaldo,
                                                                 Sum(Cta01) Cta01,
                                                                 Sum(Cta02) Cta02,
                                                                 Sum(Cta03) Cta03
                                                            From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                                         s.periodosaldo,
                                                                         Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) Cta01,
                                                                         0 Cta02,
                                                                         0 Cta03
                                                                    From Ctbsaldo s, ctbconta c
                                                                   Where s.nroplano = 10
                                                                     And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                                     And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                                     And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                                     And c.codcontactb = 30615
                                                                     And s.nroplano = c.nroplano
                                                                     And s.codcontactb = c.codcontactb
                                                                     And c.lancamento = 'S'
                                                                   Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                                            s.periodosaldo
                                                                   Union All
                                                                  Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                                         s.periodosaldo,
                                                                         0 Cta01,
                                                                         Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                                                                         0 Cta03
                                                                    From Ctbsaldo s, ctbconta c
                                                                   Where s.nroplano = 10
                                                                     And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                                     And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                                     And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                                     And c.codcontactb In (41953,42023,50429)
                                                                     And s.nroplano = c.nroplano
                                                                     And s.codcontactb = c.codcontactb
                                                                     And c.lancamento = 'S'
                                                                   Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                                            s.periodosaldo
                                                                   Union All
                                                                  Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                                         s.periodosaldo,
                                                                         0 Cta01,
                                                                         0 Cta02,
                                                                         Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado
                                                                    From Ctbsaldo s, ctbconta c
                                                                   Where s.nroplano = 10
                                                                     And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                                     And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                                     And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                                     And c.codcontactb = 41953
                                                                     And s.nroplano = c.nroplano
                                                                     And s.codcontactb = c.codcontactb
                                                                     And c.lancamento = 'S'
                                                                   Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                                            s.periodosaldo )
                                                             Group By EmpFil, periodosaldo )
                                                   Union All
                                                 (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                         s.periodosaldo,
                                                         0 FolhaOper,
                                                         Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) OutrasDesp,
                                                         0 GestaoFrota
                                                    From Ctbsaldo s, ctbconta c
                                                   Where s.nroplano = 10
                                                     And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                     And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                     And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                     And c.codcontactb In (42092,42093)
                                                     And s.nroplano = c.nroplano
                                                     And s.codcontactb = c.codcontactb
                                                     And c.lancamento = 'S'
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
                                                     And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                     And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                     And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                     And (c.classificador Like '5.1.3.02.03.008%'
                                                      Or c.codcontactb In (50626,43615,43616,50215,42138,42155,50391,43614))
                                                     And s.nroplano = c.nroplano
                                                     And s.codcontactb = c.codcontactb
                                                     And c.lancamento = 'S'
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
                                             And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                             And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                             And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                             And c.codcontactb In (43705,43695,43696,42108,42106,42107,42103,42104,43617,43697,43672,42112)
                                             And s.nroplano = c.nroplano
                                             And s.codcontactb = c.codcontactb
                                             And c.lancamento = 'S'
                                           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                    s.periodosaldo)
                                           Union All
                                         (Select EmpFil,
                                                 PeriodoSaldo,
                                                 0 CustoFixoDireto,
                                                 0 CustoVariavelDireto,
                                                 Sum(FolhaManutencao) + Sum(OutrasDespPessoal) + Sum(OutrasDespesasOper) + Sum(FrotaApoio) + Sum(DespEquipFerramentas) CustoFixoIndireto
                                            From ((Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                          s.periodosaldo,
                                                          Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) FolhaManutencao,
                                                          0 OutrasDespPessoal,
                                                          0 OutrasDespesasOper,
                                                          0 FrotaApoio,
                                                          0 DespEquipFerramentas
                                                     From Ctbsaldo s, ctbconta c
                                                    Where s.nroplano = 10
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (42023,42045,42035,42025,42062,42058,42053,43712,42055,43680,42075,42083,42077,
                                                                            42071,42081,42043,43699,42060,42047,43700,42049,43701)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                      And c.lancamento = 'S'
                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                             s.periodosaldo
                                                    Union All
                                                   Select EmpFil,
                                                          periodosaldo,
                                                          Decode(Cta02, 0, 0, (Cta01 / Cta02) * Cta03) FolhaManutencao,
                                                          0 OutrasDespPessoal,
                                                          0 OutrasDespesasOper,
                                                          0 FrotaApoio,
                                                          0 DespEquipFerramentas
                                                     From (Select EmpFil,
                                                                  periodosaldo,
                                                                  Sum(Cta01) Cta01,
                                                                  Sum(Cta02) Cta02,
                                                                  Sum(Cta03) Cta03
                                                             From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                                          s.periodosaldo,
                                                                          Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) Cta01,
                                                                          0 Cta02,
                                                                          0 Cta03
                                                                     From Ctbsaldo s, ctbconta c
                                                                    Where s.nroplano = 10
                                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                                      And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                                      And c.codcontactb = 30615
                                                                      And s.nroplano = c.nroplano
                                                                      And s.codcontactb = c.codcontactb
                                                                      And c.lancamento = 'S'
                                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                                             s.periodosaldo
                                                                    Union All
                                                                   Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                                          s.periodosaldo,
                                                                          0 Cta01,
                                                                          Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                                                                          0 Cta03
                                                                     From Ctbsaldo s, ctbconta c
                                                                    Where s.nroplano = 10
                                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                                      And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                                      And c.codcontactb In (41953,42023,50429)
                                                                      And s.nroplano = c.nroplano
                                                                      And s.codcontactb = c.codcontactb
                                                                      And c.lancamento = 'S'
                                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                                             s.periodosaldo
                                                                    Union All
                                                                   Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                                          s.periodosaldo,
                                                                          0 Cta01,
                                                                          0 Cta02,
                                                                          Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado
                                                                     From Ctbsaldo s, ctbconta c
                                                                    Where s.nroplano = 10
                                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                                      And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                                      And c.codcontactb = 42023
                                                                      And s.nroplano = c.nroplano
                                                                      And s.codcontactb = c.codcontactb
                                                                      And c.lancamento = 'S'
                                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                                             s.periodosaldo )
                                                              Group By EmpFil, periodosaldo ) )
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
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (42113,42088,42089)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                      And c.lancamento = 'S'
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
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (43675,43702,43703,43671,43640,43676)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                      And c.lancamento = 'S'
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
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (50498,50528,50527,50529,50530)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                      And c.lancamento = 'S'
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
                                                      And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                      And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                      And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                      And c.codcontactb In (43704,43707,42281,43677,43681,43682)
                                                      And s.nroplano = c.nroplano
                                                      And s.codcontactb = c.codcontactb
                                                      And c.lancamento = 'S'
                                                    Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                             s.periodosaldo)                          )
                                                    Group By EmpFil, PeriodoSaldo
                                          ))
                                  Group By EmpFil, PeriodoSaldo )  )
                 Group By EmpFil, PeriodoSaldo
                 Union All
                Select EmpFil,
                       PeriodoSaldo,
                       0 MargemDistribuicao,
                       Sum(FolhaAdm) + Sum(ProcessosJuridicos) + Sum(DespesasAdm) Despesas,
                       0 RecOp
                  From (Select EmpFil,
                               PeriodoSaldo,
                               Sum(FolhaAdm) FolhaAdm,
                               0 ProcessosJuridicos,
                               0 DespesasAdm
                          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                       s.periodosaldo,
                                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) FolhaAdm
                                  From Ctbsaldo s, ctbconta c
                                 Where s.nroplano = 10
                                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                   And c.codcontactb In (50429,50487,50482,50426,50430,50428,50485,50416,50539,50417,50419,50418,
                                                         50424,50603,50472,50696,50486,50656,50496,50647,50488,50450,50676,50422,50677)
                                   And s.nroplano = c.nroplano
                                   And s.codcontactb = c.codcontactb
                                   And c.lancamento = 'S'
                                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                          s.periodosaldo
                                 Union All
                                Select EmpFil,
                                       periodosaldo,
                                       Decode(Cta02, 0, 0, (Cta01 / Cta02) * Cta03) FolhaAdm
                                  From (Select EmpFil,
                                               periodosaldo,
                                               Sum(Cta01) Cta01,
                                               Sum(Cta02) Cta02,
                                               Sum(Cta03) Cta03
                                          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                       s.periodosaldo,
                                                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) Cta01,
                                                       0 Cta02,
                                                       0 Cta03
                                                  From Ctbsaldo s, ctbconta c
                                                 Where s.nroplano = 10
                                                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                   And c.codcontactb = 30615
                                                   And s.nroplano = c.nroplano
                                                   And s.codcontactb = c.codcontactb
                                                   And c.lancamento = 'S'
                                                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                          s.periodosaldo
                                                 Union All
                                                Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                       s.periodosaldo,
                                                       0 Cta01,
                                                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                                                       0 Cta03
                                                  From Ctbsaldo s, ctbconta c
                                                 Where s.nroplano = 10
                                                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                   And c.codcontactb In (41953,42023,50429)
                                                   And s.nroplano = c.nroplano
                                                   And s.codcontactb = c.codcontactb
                                                   And c.lancamento = 'S'
                                                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                          s.periodosaldo
                                                 Union All
                                                Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                                                       s.periodosaldo,
                                                       0 Cta01,
                                                       0 Cta02,
                                                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado
                                                  From Ctbsaldo s, ctbconta c
                                                 Where s.nroplano = 10
                                                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                                                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                                                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                                                   And c.codcontactb = 50429
                                                   And s.nroplano = c.nroplano
                                                   And s.codcontactb = c.codcontactb
                                                   And c.lancamento = 'S'
                                                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                                          s.periodosaldo )
                                         Group By EmpFil, periodosaldo ) )
                         Group By EmpFil, periodosaldo
                         Union All
                       (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                               s.periodosaldo,
                               0 FolhaAdm,
                               Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) ProcessosJuridicos,
                               0 DespesasAdm
                          From Ctbsaldo s, ctbconta c
                         Where s.nroplano = 10
                           And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                           And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                           And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                           And c.codcontactb In (42863,43673,50561,50546,50211)
                           And s.nroplano = c.nroplano
                           And s.codcontactb = c.codcontactb
                           And c.lancamento = 'S'
                         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                  s.periodosaldo         )
                         Union All
                       (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                               s.periodosaldo,
                               0 FolhaAdm,
                               0 ProcessosJuridicos,
                               Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) DespesasAdm
                          From Ctbsaldo s, ctbconta c
                         Where s.nroplano = 10
                           And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                           And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                           And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                           And c.codcontactb In (42162,50681,50659,50657,50630,50451,43670,50459,50679,50409,50401,50671,50209,50218,50493,50191,50263,
                                                 50449,50434,50184,50178,50464,50179,50195,50188,50658,50194,50408,50431,50432,50678,50224,50456,50221,
                                                 50200,50577,50398,50389,50641,50400,50264,50213,50263,50489,50455,50471,50252,50251,50249,50253,50457,
                                                 50412,50454,50191,50493,50183,50410,50218,50187,50217,50209,50212,50671,50401,50553,50413,50409,50399,
                                                 50437,50436,50679,50407,50403,50459,50411,50451,50682,50630,50657,50659,50681,42162,50650,50555,50538,
                                                 50390,50220,50224,50432,50195)
                           And s.nroplano = c.nroplano
                           And s.codcontactb = c.codcontactb
                           And c.lancamento = 'S'
                         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                  s.periodosaldo         ) )
                   Group By EmpFil, Periodosaldo
                   Union All
                  Select EmpFil,
                         To_char(Data,'yyyymm'),
                         0 MargemDistribuicao,
                         0 Despesas,
                         Sum(Valor) RecOp
                    From pbi_Radar_finan_Rec_Oper
                   Group By EmpFil, Data

                   )
           Group By EmpFil, To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') )

