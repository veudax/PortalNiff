create or replace view pbi_radar_finan_Rec_Oper as
  (Select 'Receita Operacional Total' grupo, 1 Ordem,
                To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                A.EMPFIL,
                Sum(nvl(Abs(SaldoAcumulado),0)) + Sum(d.Valor) Valor,
                0 Percentual,
                'F' Tipo
          From (Select Sum(Decode(grupo, 'Subs�dio D�bito', -1,1) * Valor) Valor, 
                       To_char(Data,'yyyymm') periodo,
                       EmpFil
                  From Pbi_Radar_Operacional
                 Where Tipo = 'Financeiro'
                   And Grupo Like 'Subs�dio%'
                 Group By To_char(Data,'yyyymm'), EmpFil ) D,
                (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                        s.periodosaldo,
                        Sum(s.vlcreditosaldo)- Sum(s.vldebitosaldo) saldoAcumulado
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
                          s.periodosaldo) A
          Where A.EmpFil = D.EmpFil
            And A.periodosaldo = D.periodo 
          Group By To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy'), A.EMPFIL  
         ) 