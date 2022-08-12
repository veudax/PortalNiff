Create Or Replace View Pbi_Radar_Financeiro As
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       valor,
       Percentual,
       Ordem
  From (Select 'Receita Financeira Total' Grupo, 1 Ordem, 
                To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                EMPFIL,
                Sum(nvl(Abs(SaldoAcumulado),0)) Valor,
                0 Percentual
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       s.periodosaldo,
                       decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                             , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                             , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                       From CTBsaldo S1
                                      Where s1.nroplano = s.nroplano
                                        And s1.codcontactb = s.codcontactb
                                        And s1.codigoempresa = s.codigoempresa
                                        And s1.codigofl = s.codigofl
                                        And s1.codigoempresa || s1.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                        And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado
                  From Ctbsaldo s, ctbconta c 
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.classificador Like '5.2.1.01%'
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                          s.periodosaldo )
         Group By EmpFil, To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') 
         Union All
        Select 'Receita Operacional Total' Grupo, 1 Ordem, 
                To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                EMPFIL,
                Sum(nvl(Abs(SaldoAcumulado),0)) Valor,
                0 Percentual
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       s.periodosaldo,
                       Sum(s.vldebitosaldo - s.vlcreditosaldo) MovtoMes,
                       decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                             , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                             , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                       From CTBsaldo S1
                                      Where s1.nroplano = s.nroplano
                                        And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                        And s1.codcontactb = s.codcontactb
                                        And s1.codigoempresa = s.codigoempresa
                                        And s1.codigofl = s.codigofl
                                        And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado
                  From Ctbsaldo s, ctbconta c 
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And (c.classificador Like '3.1.1.04%'
                    Or c.classificador Like '3.1.1.06%'
                    Or c.classificador Like '3.1.2.07%'
                    Or c.codcontactb In (30236,30240,30227,30461,30217,30228,30229,30330))
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                          s.periodosaldo)
         Group By EmpFil, To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') 
         Union All
        Select 'Folha Pagto Total' Grupo, 3 Ordem,
               Data,
               EmpFil,
               Sum(Valor) Valor,
               Decode(Sum(SaldoAcumulado), 0, 0, (Sum(Valor)/Abs(Sum(SaldoAcumulado)))*100) Percentual
          From (Select LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy') data,
                       Sum(Decode(v.tipoeven, 'D', -1, 'P', 1, 0) * e.valorficha) Valor,
                       0 SaldoAcumulado,
                       0 Percentual
                  From flp_fichafinanceira f, Flp_Fichaeventos e, flp_eventos v, Vw_Funcionarios fu
                 Where f.competficha Between  (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                   And fu.CODINTFUNC = f.codintfunc
                   And f.codintfunc = e.codintfunc
                   And f.tipofolha = e.tipofolha
                   And f.competficha = e.competficha
                   And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And v.codevento = e.codevento
                   And v.Tipoeven In ('D','P')
                   And fu.SITUACAOFUNC = 'A'
                 Group By LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0'),
                          To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy')
                 Union All
                 Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                        To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                        0 Valor,
                        decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                              , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                              , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                        From CTBsaldo S1
                                       Where s1.nroplano = s.nroplano
                                         And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                         And s1.codcontactb = s.codcontactb
                                         And s1.codigoempresa = s.codigoempresa
                                         And s1.codigofl = s.codigofl
                                         And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado,
                        0 Percentual
                   From Ctbsaldo s, ctbconta c 
                  Where s.nroplano = 10
                    And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                    And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                    And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                    And (c.classificador Like '3.1.1.04%'
                     Or c.classificador Like '3.1.1.06%'
                     Or c.classificador Like '3.1.2.07%'
                     Or c.codcontactb In (30236,30240,30227,30461,30217,30228,30229,30330))
                    And s.nroplano = c.nroplano
                    And s.codcontactb = c.codcontactb
                    And c.lancamento = 'S'
                  Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                           s.periodosaldo)           
         Group By Data, EmpFil
         Union All
        Select 'Folha Pagto Operação' Grupo, 4 Ordem,
               Data,
               EmpFil,
               Sum(Valor) Valor,
               Decode(Sum(SaldoAcumulado), 0, 0, (Sum(Valor)/Abs(Sum(SaldoAcumulado)))*100) Percentual
          From (Select LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy') data,
                       Sum(Decode(v.tipoeven, 'D', -1, 'P', 1, 0) * e.valorficha) Valor,
                       0 SaldoAcumulado,
                       0 Percentual
                  From flp_fichafinanceira f, Flp_Fichaeventos e, flp_eventos v, Vw_Funcionarios fu
                 Where f.competficha Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                   And fu.CODINTFUNC = f.codintfunc
                   And f.codintfunc = e.codintfunc
                   And f.tipofolha = e.tipofolha
                   And f.competficha = e.competficha
                   And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And v.codevento = e.codevento
                   And v.Tipoeven In ('D','P')
                   And fu.SITUACAOFUNC = 'A'
                   And fu.CODAREA = 40
                 Group By LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0'),
                          To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy')
                 Union All
                 Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                        To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                        0 Valor,
                        decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                              , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                              , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                        From CTBsaldo S1
                                       Where s1.nroplano = s.nroplano
                                         And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                         And s1.codcontactb = s.codcontactb
                                         And s1.codigoempresa = s.codigoempresa
                                         And s1.codigofl = s.codigofl
                                         And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado,
                        0 Percentual
                   From Ctbsaldo s, ctbconta c 
                  Where s.nroplano = 10
                    And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                    And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                    And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                    And (c.classificador Like '3.1.1.04%'
                     Or c.classificador Like '3.1.1.06%'
                     Or c.classificador Like '3.1.2.07%'
                     Or c.codcontactb In (30236,30240,30227,30461,30217,30228,30229,30330))
                    And s.nroplano = c.nroplano
                    And s.codcontactb = c.codcontactb
                    And c.lancamento = 'S'
                  Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                           s.periodosaldo)           
         Group By Data, EmpFil         Union All
        Select 'Folha Pagto Manutenção' Grupo, 5 Ordem,
               Data,
               EmpFil,
               Sum(Valor) Valor,
               Decode(Sum(SaldoAcumulado), 0, 0, (Sum(Valor)/Abs(Sum(SaldoAcumulado)))*100) Percentual
          From (Select LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy') data,
                       Sum(Decode(v.tipoeven, 'D', -1, 'P', 1, 0) * e.valorficha) Valor,
                       0 SaldoAcumulado,
                       0 Percentual
                  From flp_fichafinanceira f, Flp_Fichaeventos e, flp_eventos v, Vw_Funcionarios fu
                 Where f.competficha Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                   And fu.CODINTFUNC = f.codintfunc
                   And f.codintfunc = e.codintfunc
                   And f.tipofolha = e.tipofolha
                   And f.competficha = e.competficha
                   And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And v.codevento = e.codevento
                   And v.Tipoeven In ('D','P')
                   And fu.SITUACAOFUNC = 'A'
                   And fu.CODAREA = 30
                 Group By LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0'),
                          To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy')
                 Union All
                 Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                        To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                        0 Valor,
                        decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                              , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                              , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                        From CTBsaldo S1
                                       Where s1.nroplano = s.nroplano
                                         And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                         And s1.codcontactb = s.codcontactb
                                         And s1.codigoempresa = s.codigoempresa
                                         And s1.codigofl = s.codigofl
                                         And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado,
                        0 Percentual
                   From Ctbsaldo s, ctbconta c 
                  Where s.nroplano = 10
                    And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                    And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                    And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                    And (c.classificador Like '3.1.1.04%'
                     Or c.classificador Like '3.1.1.06%'
                     Or c.classificador Like '3.1.2.07%'
                     Or c.codcontactb In (30236,30240,30227,30461,30217,30228,30229,30330))
                    And s.nroplano = c.nroplano
                    And s.codcontactb = c.codcontactb
                    And c.lancamento = 'S'
                  Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                           s.periodosaldo)           
         Group By Data, EmpFil        
         Union All
        Select 'Folha Pagto Administração' Grupo, 6 Ordem,
               Data,
               EmpFil,
               Sum(Valor) Valor,
               Decode(Sum(SaldoAcumulado), 0, 0, (Sum(Valor)/Abs(Sum(SaldoAcumulado)))*100) Percentual
          From (Select LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy') data,
                       Sum(Decode(v.tipoeven, 'D', -1, 'P', 1, 0) * e.valorficha) Valor,
                       0 SaldoAcumulado,
                       0 Percentual
                  From flp_fichafinanceira f, Flp_Fichaeventos e, flp_eventos v, Vw_Funcionarios fu
                 Where f.competficha Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                   And fu.CODINTFUNC = f.codintfunc
                   And f.codintfunc = e.codintfunc
                   And f.tipofolha = e.tipofolha
                   And f.competficha = e.competficha
                   And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And v.codevento = e.codevento
                   And v.Tipoeven In ('D','P')
                   And fu.SITUACAOFUNC = 'A'
                   And fu.CODAREA = 20
                 Group By LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0'),
                          To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy')
                 Union All
                 Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                        To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                        0 Valor,
                        decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                              , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                              , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                        From CTBsaldo S1
                                       Where s1.nroplano = s.nroplano
                                         And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                         And s1.codcontactb = s.codcontactb
                                         And s1.codigoempresa = s.codigoempresa
                                         And s1.codigofl = s.codigofl
                                         And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado,
                        0 Percentual
                   From Ctbsaldo s, ctbconta c 
                  Where s.nroplano = 10
                    And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                    And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                    And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                    And (c.classificador Like '3.1.1.04%'
                     Or c.classificador Like '3.1.1.06%'
                     Or c.classificador Like '3.1.2.07%'
                     Or c.codcontactb In (30236,30240,30227,30461,30217,30228,30229,30330))
                    And s.nroplano = c.nroplano
                    And s.codcontactb = c.codcontactb
                    And c.lancamento = 'S'
                  Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                           s.periodosaldo)           
         Group By Data, EmpFil
         Union All
        Select 'Horas Extras (Oper+Manut)' Grupo, 7 Ordem, 
                To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy') Data,
                EMPFIL,
                Sum(qtd_ocorr) Valor,
                0 Percentual
          From (Select count(fu.nomefunc) qtd_ocorr, 
                       trunc(t.dthist) Data, 
                       LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0') EMPFIL
                  From flp_historico t, frq_ocorrencia f, vw_funcionarios fu
                 Where t.dthist BETWEEN  (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                   And f.codocorr = t.codocorr
                   And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And fu.situacaofunc = 'A'
                   And f.codocorr In (505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527)
                   And t.codintfunc = fu.codintfunc
                   And fu.CODAREA In (30,40)
                 Group By Trunc(t.dthist), 
                          LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0'))   
         Group By EmpFil, To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy')
         Union All
        Select 'Receita por carro' Grupo, 8 Ordem, 
                Data,
                EMPFIL,
                Abs(Decode(Sum(QtdCarros), 0, 0, Sum(saldoAcumulado)/Sum(QtdCarros))) VaLor,
                0 Percentual
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                             , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                             , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                       From CTBsaldo S1
                                      Where s1.nroplano = s.nroplano
                                        And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                        And s1.codcontactb = s.codcontactb
                                        And s1.codigoempresa = s.codigoempresa
                                        And s1.codigofl = s.codigofl
                                        And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado,
                       0 QtdCarros
                  From Ctbsaldo s, ctbconta c 
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And (c.classificador Like '3.1.1.04%'
                    Or c.classificador Like '3.1.1.06%'
                    Or c.classificador Like '3.1.2.07%'
                    Or c.codcontactb In (30236,30240,30227,30461,30217,30228,30229,30330))
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                          s.periodosaldo
                 Union All
                Select LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                       To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                       0 Valor,
                       Count(Distinct v.cod_veiculo) QtdCarros
                  From t_Arr_Guia g, 
                       t_arr_viagens_guia v,
                       frt_cadveiculos c
                  Where v.cod_seq_guia = g.cod_seq_guia
                    And c.codigoempresa || c.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                    And c.codigoveic = v.cod_veiculo
                    And c.codigotpfrota Not In (7,9,10,52)
                    And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                    And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                  Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                           LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0'))   
         Group By EmpFil, Data       
         Union All         
        Select 'Custo Folha por carro' Grupo, 9 Ordem, 
               Data,
               EmpFil,
               Decode(Sum(QtdCarros), 0, 0, Sum(Valor)/Sum(QtdCarros)) Valor,
               0 Percentual
          From (Select LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy') data,
                       Sum(Decode(v.tipoeven, 'D', -1, 'P', 1, 0) * e.valorficha) Valor,
                       0 QtdCarros
                  From flp_fichafinanceira f, Flp_Fichaeventos e, flp_eventos v, Vw_Funcionarios fu
                 Where f.competficha Between  (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                   And fu.CODINTFUNC = f.codintfunc
                   And f.codintfunc = e.codintfunc
                   And f.tipofolha = e.tipofolha
                   And f.competficha = e.competficha
                   And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And v.codevento = e.codevento
                   And v.Tipoeven In ('D','P')
                   And fu.SITUACAOFUNC = 'A'
                 Group By LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0'),
                          To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy')
                 Union All
                Select LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                       To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                       0 Valor,
                       Count(Distinct v.cod_veiculo) QtdCarros
                  From t_Arr_Guia g, 
                       t_arr_viagens_guia v,
                       frt_cadveiculos c
                  Where v.cod_seq_guia = g.cod_seq_guia
                    And c.codigoempresa || c.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                    And c.codigoveic = v.cod_veiculo
                    And c.codigotpfrota Not In (7,9,10,52)
                    And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                    And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                  Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                           LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0'))   
         Group By EmpFil, Data       
         Union All                  
        Select 'Margem financeiro por carro' Grupo, 10 Ordem, 
                Data,
                EMPFIL,
                Abs(Sum(Valor)),
                0 Percentual
          From ((Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                             , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                       From CTBsaldo S1
                                      Where s1.nroplano = s.nroplano
                                        And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                        And s1.codcontactb = s.codcontactb
                                        And s1.codigoempresa = s.codigoempresa
                                        And s1.codigofl = s.codigofl
                                        And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) Valor
                  From Ctbsaldo s, ctbconta c 
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
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
                       Data,
                       Decode(Sum(QtdCarros), 0, 0, Sum(Valor)/Sum(QtdCarros)) Valor
                  From (Select LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0') EmpFil,
                               To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy') data,
                               Sum(Decode(v.tipoeven, 'D', -1, 'P', 1, 0) * e.valorficha) Valor,
                               0 QtdCarros
                          From flp_fichafinanceira f, Flp_Fichaeventos e, flp_eventos v, Vw_Funcionarios fu
                         Where f.competficha Between  (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                           And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                           And fu.CODINTFUNC = f.codintfunc
                           And f.codintfunc = e.codintfunc
                           And f.tipofolha = e.tipofolha
                           And f.competficha = e.competficha
                           And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                           And v.codevento = e.codevento
                           And v.Tipoeven In ('D','P')
                           And fu.SITUACAOFUNC = 'A'
                         Group By LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(Fu.CodigoFl,3,'0'),
                                  To_date('01/' || To_char(f.competficha,'mm/yyyy'), 'dd/mm/yyyy')
                         Union All
                        Select LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                               To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                               0 Valor,
                               Count(Distinct v.cod_veiculo) QtdCarros
                          From t_Arr_Guia g, 
                               t_arr_viagens_guia v,
                               frt_cadveiculos c
                          Where v.cod_seq_guia = g.cod_seq_guia
                            And c.codigoempresa || c.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                            And c.codigoveic = v.cod_veiculo
                            And c.codigotpfrota Not In (7,9,10,52)
                            And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                          Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                                   LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0'))   
                 Group By EmpFil, Data)
                 Union All                                
               (Select EMPFIL,
                       Data,
                       Abs(Decode(Sum(QtdCarros), 0, 0, Sum(saldoAcumulado)/Sum(QtdCarros))) VaLor
                  From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                               To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                               decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                               From CTBsaldo S1
                                              Where s1.nroplano = s.nroplano
                                                And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                                And s1.codcontactb = s.codcontactb
                                                And s1.codigoempresa = s.codigoempresa
                                                And s1.codigofl = s.codigofl
                                                And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado,
                               0 QtdCarros
                          From Ctbsaldo s, ctbconta c 
                         Where s.nroplano = 10
                           And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                           And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                           And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                           And (c.classificador Like '3.1.1.04%'
                            Or c.classificador Like '3.1.1.06%'
                            Or c.classificador Like '3.1.2.07%'
                            Or c.codcontactb In (30236,30240,30227,30461,30217,30228,30229,30330))
                           And s.nroplano = c.nroplano
                           And s.codcontactb = c.codcontactb
                           And c.lancamento = 'S'
                         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                                  s.periodosaldo
                         Union All
                        Select LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                               To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                               0 Valor,
                               Count(Distinct v.cod_veiculo) QtdCarros
                          From t_Arr_Guia g, 
                               t_arr_viagens_guia v,
                               frt_cadveiculos c
                          Where v.cod_seq_guia = g.cod_seq_guia
                            And c.codigoempresa || c.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                            And c.codigoveic = v.cod_veiculo
                            And c.codigotpfrota Not In (7,9,10,52)
                            And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                          Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                                   LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0'))   
                 Group By EmpFil, Data ))                  
         Group By EmpFil, Data         
         Union All 
        Select 'Custo Manutenção Frota' Grupo, 11 Ordem, 
                Data,
                EmpFil,
                Abs(Sum(saldoAcumulado)) Valor,
                0 Percentual
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                             , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                             , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                       From CTBsaldo S1
                                      Where s1.nroplano = s.nroplano
                                        And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                        And s1.codcontactb = s.codcontactb
                                        And s1.codigoempresa = s.codigoempresa
                                        And s1.codigofl = s.codigofl
                                        And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado
                  From Ctbsaldo s, ctbconta c 
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (43705,43695,43696,42108,42106,42107,42103,42104,43617,43697,43672,42112)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                          s.periodosaldo)
         Group By Empfil, Data
         Union All                
        Select 'Despesa Total' Grupo, 12 Ordem, 
                Data,
                EMPFIL,
                Sum(SaldoAcumulado) Valor,
                0 Percentual
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                             , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                             , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                       From CTBsaldo S1
                                      Where s1.nroplano = s.nroplano
                                        And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                        And s1.codcontactb = s.codcontactb
                                        And s1.codigoempresa = s.codigoempresa
                                        And s1.codigofl = s.codigofl
                                        And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))) saldoAcumulado
                  From Ctbsaldo s, ctbconta c 
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (50449,50434,50184,50178,50464,50179,50195,50188,50658,50194,50408,50431,50432,50678,50224,50456,50221,
                                         50200,50577,50398,50389,50641,50400,50264,50213,50263,50489,50455,50471,50252,50251,50249,50253,50457,
                                         50412,50454,50191,50493,50183,50410,50218,50187,50217,50209,50212,50671,50401,50553,50413,50409,50399,
                                         50437,50436,50679,50407,50403,50459,50411,50451,50682,50630,50657,50659,50681,42162,50650,50555,50538,50390)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                          s.periodosaldo)
         Group By EmpFil, Data
         Union All  
        Select 'EBITDA' Grupo, 13 Ordem, 
                Data,
                EMPFIL,
                Abs(Sum(Margem)) - Abs(Sum(Despesas)) Valor,
                0 Percentual
          From ((Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       Nvl(decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                             , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                       From CTBsaldo S1
                                      Where s1.nroplano = s.nroplano
                                        And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                        And s1.codcontactb = s.codcontactb
                                        And s1.codigoempresa = s.codigoempresa
                                        And s1.codigofl = s.codigofl
                                        And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))),0) Despesas,
                       0 margem
                  From Ctbsaldo s, ctbconta c 
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (50449,50434,50184,50178,50464,50179,50195,50188,50658,50194,50408,50431,50432,50678,50224,50456,50221,
                                         50200,50577,50398,50389,50641,50400,50264,50213,50263,50489,50455,50471,50252,50251,50249,50253,50457,
                                         50412,50454,50191,50493,50183,50410,50218,50187,50217,50209,50212,50671,50401,50553,50413,50409,50399,
                                         50437,50436,50679,50407,50403,50459,50411,50451,50682,50630,50657,50659,50681,42162,50650,50555,50538,50390)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                          s.periodosaldo)
                 Union All
               (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       0 despesas,
                       Nvl(decode( s.periodosaldo, To_Char(trunc(Sysdate,'rr'),'yyyymm')
                             , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                                       From CTBsaldo S1
                                      Where s1.nroplano = s.nroplano
                                        And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                                        And s1.codcontactb = s.codcontactb
                                        And s1.codigoempresa = s.codigoempresa
                                        And s1.codigofl = s.codigofl
                                        And S1.Periodosaldo Between To_Char(trunc(Sysdate,'rr'),'yyyymm') And s.Periodosaldo ))),0) margem
                  From Ctbsaldo s, ctbconta c 
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm') 
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (43705,43695,43696,42108,42106,42107,42103,42104,43617,43697,43672,42092,42093,50626,50321,43615,43616,
                                         50215,42138,42155,50391,43614,50391,41953,41975,41965,41955,41992,41988,41983,41963,43711,43679,42005,
                                         42013,42007,42001,42009,42011,42139,43698,41990,41977,43693,41979,43694)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                          s.periodosaldo))                
         Group By EmpFil, Data
)