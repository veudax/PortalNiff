create or replace view pbi_radar_finan_totfolhaop as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select 'Folha Pagto Operação' Grupo, 4 Ordem,
               To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
               EmpFil,
               Sum(SaldoAcumulado) Valor,
               Decode(Sum(RecOp), 0, 0, (Sum(SaldoAcumulado)/Abs(Sum(RecOp)))*100) Percentual,
               'F' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       s.periodosaldo,
                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                       0 RecOp
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
                       To_char(Data,'yyyymm'),
                       0 SaldoAcumulado,
                       Sum(Valor) RecOp
                  From pbi_Radar_finan_Rec_Oper
                 Group By EmpFil, Data
                 Union All
                Select EmpFil,
                       periodosaldo,
                       Decode(Cta02, 0, 0, (Cta01 / Cta02) * Cta03) SaldoAcumulado,
                       0 RecOp
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
                           Group By EmpFil, periodosaldo ) )
         Group By To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy'), EmpFil )

