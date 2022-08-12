create or replace view pbi_radar_finan_despesas as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select 'Despesa Total' Grupo, 12 Ordem,
                Data,
                EMPFIL,
                Sum(SaldoAcumulado) Valor,
                Decode(Sum(RecOp), 0, 0, (Sum(SaldoAcumulado)/Abs(Sum(RecOp)))*100) Percentual,
                'F' Tipo
          From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                       To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                       Sum(s.vldebitosaldo - s.vlcreditosaldo) saldoAcumulado,
                       0 RecOp
                  From Ctbsaldo s, ctbconta c
                 Where s.nroplano = 10
                   And s.codigoempresa || s.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                   And s.periodosaldo Between To_char(ADD_MONTHS(Trunc(Sysdate,'rr'), -12),'yyyymm')
                   And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                   And c.codcontactb In (42863,43673,50561,50546,50211, -- processos juridicos
                                         42162,50681,50659,50657,50630,50451,43670,50459,50679,50409,50401,50671,50209,50218,50493,50191,50263,
                                         50449,50434,50184,50178,50464,50179,50195,50188,50658,50194,50408,50431,50432,50678,50224,50456,50221,
                                         50200,50577,50398,50389,50641,50400,50264,50213,50263,50489,50455,50471,50252,50251,50249,50253,50457,
                                         50412,50454,50191,50493,50183,50410,50218,50187,50217,50209,50212,50671,50401,50553,50413,50409,50399,
                                         50437,50436,50679,50407,50403,50459,50411,50451,50682,50630,50657,50659,50681,42162,50650,50555,50538,
                                         50390,50220,50224,50432,50195)
                   And s.nroplano = c.nroplano
                   And s.codcontactb = c.codcontactb
                   And c.lancamento = 'S'
                 Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                          s.periodosaldo
                 Union All
                 Select Empfil,
                        Data,
                        Valor saldoAcumulado,
                        0 RecOp
                   From Pbi_Radar_Finan_TotFolhaAdm
                 Union All
                Select EmpFil,
                       Data,
                       0 SaldoAcumulado,
                       Sum(Valor) RecOp
                  From pbi_Radar_finan_Rec_Oper
                 Group By EmpFil, Data
                 )
         Group By EmpFil, Data)

