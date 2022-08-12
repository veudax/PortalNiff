create or replace view pbi_radar_finan_customanfrota as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Round(valor) Valor,
       decode(Percentual,0,Null,Round(Percentual,2)) Percentual,
       Percentual PercentualN,
       Ordem,
       Tipo
  From (Select 'Custo Manutenção Frota' Grupo, 11 Ordem,
                Data,
                Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
                Abs(Sum(saldoAcumulado)) Valor,
                Decode(Sum(RecOp), 0, 0, (Sum(SaldoAcumulado)/Abs(Sum(RecOp)))*100)  Percentual,
                'F' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                       0 RecOp
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                   And s.periodosaldo Between '201705'
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (43705,43695,43696,42108,42106,42107,42103,42104,43617,43697,43672,42112)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo
                 Union All
                Select EmpFil,
                       Data,
                       0 SaldoAcumulado,
                       Sum(Valor) RecOp
                  From pbi_Radar_finan_Rec_Oper
                 Group By EmpFil, Data )
         Group By Empfil, Data )

