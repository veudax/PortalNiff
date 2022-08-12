create or replace view pbi_radar_finan_receitacarro as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select 'Receita por carro' Grupo, 8 Ordem,
                Data,
                EMPFIL,
                Abs(Decode(Sum(QtdCarros), 0, 0, Sum(saldoAcumulado)/Sum(QtdCarros))) VaLor,
                0 Percentual,
                'F' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       Last_day(To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy')) Data,
                       Sum(s.vlcreditosaldo)- Sum(s.vldebitosaldo) saldoAcumulado,
                       0 QtdCarros
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
                          To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy')
                 Union All
               (Select EmpFil,
                       Last_day(Data) ,
                       0 Valor,
                       Sum(Valor) QtdCarros
                  From Pbi_Radar_Operacional
                 Where grupo = 'Frota Operacional'
                 Group By Last_day(Data), EmpFil) )
         Group By EmpFil, Data )

