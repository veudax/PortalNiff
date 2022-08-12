create or replace view pbi_radar_financeiro_horaextra as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Round(valor) Valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Percentual PercentualN,
       Ordem,
       Tipo
  From (Select 'Horas Extras (Oper+Manut)' Grupo, 7 Ordem,
                To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
                Sum(saldoAcumulado) Valor,
                0 Percentual,
                'F' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       s.periodosaldo,
                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                       0 RecOp
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                   And s.periodosaldo Between '201705'
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (41955,42025)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo )
         Group By EmpFil, To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') )

