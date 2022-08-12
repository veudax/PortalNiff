create or replace view pbi_radar_manutencao as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select 'Custo Manutenção-Peças' Grupo, 1 Ordem,
               Data,
               EMPFIL,
               Sum(SaldoAcumulado) VaLor,
               Decode(Sum(RecOp), 0, 0, (Sum(SaldoAcumulado)/Abs(Sum(RecOp)))*100) Percentual,
               'M' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_Date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                       0 RecOp
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (43705,43695,43696,42108)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo
                 Union All
                Select EmpFil,
                       Data,
                       0 SaldoAcumulado,
                       Sum(Valor) RecOp
                  From pbi_Radar_finan_Rec_Oper
                 Group By EmpFil, Data )
         Group By EmpFil, Data
         Union All
        Select 'Custo Mantenção-Pneus' Grupo, 2 Ordem,
               Data,
               EMPFIL,
               Sum(SaldoAcumulado) Valor,
               Decode(Sum(RecOp), 0, 0, (Sum(SaldoAcumulado)/Abs(Sum(RecOp)))*100) Percentual,
               'M' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_Date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                       0 RecOp
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (42106,42107)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo
                 Union All
                Select EmpFil,
                       Data,
                       0 SaldoAcumulado,
                       Sum(Valor) RecOp
                  From pbi_Radar_finan_Rec_Oper
                 Group By EmpFil, Data )
         Group By EmpFil, Data
         Union All
        Select 'MKBF' Grupo, 3 Ordem,
                Data,
                EmpFil,
                Decode((sum(totalra)+sum(totalsos)),0, 0, sum(totalkm) / (sum(totalra)+sum(totalsos))) Valor,
                0 Percentual,
                'M' tipo
          From (Select Lpad(m.codigoEmpresa,3,'0') || '/' || lPad(m.codigoFl,3,'0') EmpFil,
                       Last_Day(m.dataaberturaos) data,
                       count(distinct m.numeroos) totalra,
                       0 totalsos,
                       0 totalkm
                  From man_os m
                 Where m.dataaberturaos Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                   And m.codorigos in (2)
                   And m.codigoempresa || m.codigofl In (11,12,21,31,41,51,61,91,13,261,263)
                 Group by Last_Day(m.dataaberturaos) , Lpad(m.codigoEmpresa,3,'0') || '/' || lPad(m.codigoFl,3,'0')
                 Union All
                Select Lpad(m.codigoEmpresa,3,'0') || '/' || lPad(m.codigoFl,3,'0') EmpFil,
                       Last_day(m.dataaberturaos) data,
                       0 totalra,
                       count(distinct m.numeroos) totalsos,
                       0 totalkm
                  From man_os m
                 where m.dataaberturaos Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                   And m.codorigos in (3)
                   And m.codigoempresa || m.codigofl In (11,12,21,31,41,51,61,91,13,261,263)
                 Group By Last_day(m.dataaberturaos), Lpad(m.codigoEmpresa,3,'0') || '/' || lPad(m.codigoFl,3,'0')
                 Union All
                 Select EmpFil,
                        Data,
                        0 saldoAcumulado,
                        0 Totalsos,
                        Km
                  From pbi_radar_manut_Km  )  
         Group By Data, EmpFil            
         Union All
        Select 'Custo Diesel/km' Grupo, 4 Ordem,
               Data,
               EMPFIL,
               Round(Decode(Sum(Km),0, 0, Sum(SaldoAcumulado)/Sum(km)),2) Valor,
               0 Percentual,
               'M' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       Last_day(To_Date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy')) Data,
                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                       0 Km
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (42103)--,42104)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo
                 Union All
                 Select EmpFil,
                        Data,
                        0 saldoAcumulado,
                        Km
                  From pbi_radar_manut_Km)
         Group By EmpFil, Data
         Union All
        Select 'Custo folha Manut/Carro' Grupo, 5 Ordem,
               Data,
               EMPFIL,
               Decode(Sum(QtdCarros), 0, 0, Sum(Valor)/Sum(QtdCarros)) Valor,
               0 Percentual,
               'M' Tipo
          From (Select EmpFil,
                       Last_day(Data) Data,
                       valor,
                       0 QtdCarros
                  From Pbi_Radar_Finan_TotFolhaMan
                 Union All
                Select EmpFil,
                       Last_day(Data) ,
                       0 Valor,
                       Sum(Valor) QtdCarros
                  From Pbi_Radar_Operacional
                 Where grupo = 'Frota Operacional'
                 Group By Last_day(Data), EmpFil)
         Group By EmpFil, Data
         Union All
        Select 'Custo Peças/Carro' Grupo,6 Ordem,
               Data,
               EMPFIL,
               Decode(Sum(QtdCarros), 0, 0, Sum(saldoAcumulado)/Sum(QtdCarros)) Valor,
               0 Percentual,
               'M' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       Last_day(To_Date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy')) Data,
                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                       0 QtdCarros
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (43705,43695,43696,42108)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo
                 Union All
                Select EmpFil,
                       Last_day(Data) ,
                       0 Valor,
                       Sum(Valor) QtdCarros
                  From Pbi_Radar_Operacional
                 Where grupo = 'Frota Operacional'
                 Group By Last_day(Data), EmpFil)
         Group By EmpFil, Data
         Union All
        Select 'Custo Pneus/Carro' Grupo, 7 Ordem,
               Data,
               EMPFIL,
               Decode(Sum(QtdCarros), 0, 0, Sum(saldoAcumulado)/Sum(QtdCarros)) Valor,
               0 Percentual,
               'M' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       Last_day(To_Date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy')) Data,
                       Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado,
                       0 QtdCarros
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (42106,42107)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo
                 Union All
                Select EmpFil,
                       Last_day(Data) ,
                       0 Valor,
                       Sum(Valor) QtdCarros
                  From Pbi_Radar_Operacional
                 Where grupo = 'Frota Operacional'
                 Group By Last_day(Data), EmpFil  )
         Group By EmpFil, Data
         Union All
        Select 'Revisão Preventiva Vencida' Grupo, 8 Ordem,
                To_date('01/06/2017','dd/mm/yyyy') Data,
               '001/002' EMPFIL,
               0 Valor,
               0 Percentual,
               'M' Tipo
          From Dual
         Union All
        Select 'Troca de Óleo Vencida' Grupo, 9 Ordem,
                To_date('01/06/2017','dd/mm/yyyy') Data,
                '009/001' EMPFIL,
                0 valor, --Vencidas Valor,
                0 Percentual,
               'M' Tipo
          From dual /*(Select EmpFil,
                       Data,
                       Vencidas
                  From Pbi_Radar_Manut_TrocaOleo)*/
         Union All
        Select Grupo, 10 Ordem,
               Data,
               EMPFIL,
               Percentual Valor,
               0 Percentual,
               'M' Tipo
          From Pbi_Radar_Operacional
         Where Tipo = 'Manutenção'
)

