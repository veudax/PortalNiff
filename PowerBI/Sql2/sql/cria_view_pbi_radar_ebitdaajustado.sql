create or replace view pbi_radar_ebitdaajustado as
Select 'EBITDA Ajustado' Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Round(valor) / 1000 Valor,
       decode(Percentual,0,Null,Round(Percentual,2)) Percentual,
       Percentual PercentualN,
       Ordem,
       Tipo
  From ( Select 'EBITDA' Grupo, 13 Ordem,
               Data,
               EmpFil,
               Sum(valor) + Sum(despesa) valor,
               Decode(Sum(RecOp), 0, 0, ((Sum(valor) + Sum(despesa))/Abs(Sum(RecOp)))*100) Percentual,
               /*decode(Least(Sum(valor), 0), Sum(Valor)
                     , Sum(Valor) - Sum(Despesa)
                     , Sum(valor) + Sum(despesa)) valor,*/
               'E' Tipo
           From (Select EmpFil,
                        Data ,
                        Valor,
                        0 Despesa,
                        0 RecOp
                   From pbi_radar_finan_ebitda
                 Union All
                 Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                        To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                        0 valor,
                       (Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo)) Despesa,
                       0 RecOp
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                   And s.periodosaldo Between '201705'
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (42863,43673,50561,50546,50211)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo
                 Union All
                Select EmpFil,
                       Data,
                       0 MargemDistribuicao,
                       0 Despesas,
                       Sum(Valor) RecOp
                  From pbi_Radar_finan_Rec_Oper
                 Group By EmpFil, Data                           )
          Group By EmpFil, Data
                          )

