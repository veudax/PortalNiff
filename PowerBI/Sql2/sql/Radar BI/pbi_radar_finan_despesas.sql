create or replace view pbi_radar_finan_despesas as
Select Data,
         Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
         Sum(SaldoAcumulado) Valor
    From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                 To_date('01/' || substr(s.PeriodoSaldo,5,2) || '/' || Substr(s.PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                 Sum(s.vldebitosaldo - s.vlcreditosaldo) saldoAcumulado,
                 0 RecOp
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10

             And s.periodosaldo Between '201705'
             And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')

             And ((s.codigoempresa || s.codigofl in (11,12,21,51,61)
             And c.codcontactb In (42162,/*43670,*/50178,50179,50183,50184,50187,50188,50191,50194,50195,
                                   50209,50212,50213,50217,50218,50220,50221,50224,50249,50251,50252,
                                   50253,50263,50264,50389,50390,50398,50399,50400,50401,50403,50407,50408,
                                   50409,50410,50411,50412,50413,50431,50432,50434,50436,50437,50449,
                                   50451,50454,50455,50456,50457,/*50459,*/50464,50471,50489,50493,50538,
                                   50553,50555,50577,50630,50641,50650,50657,50658,50659,/*50661,*/50671,
                                   50678,50679,50681,50682 ))

              Or (s.codigoempresa || s.codigofl in (31,315,41,91,131,261,263)
             And c.codcontactb In (42162,50178,50179,50183,50184,50187,50188,50191,50194,50195,
                                   50209,50212,50213,50217,50218,50220,50221,50224,50249,50251,50252,
                                   50253,50263,50264,50389,50390,50398,50399,50400,50401,50403,50407,50408,
                                   50409,50410,50411,50412,50413,50431,50432,50434,50436,50437,50449,
                                   50451,50454,50455,50456,50457,50459,50464,50471,50489,50493,50538,
                                   50553,50555,50577,50630,50641,50650,50657,50658,50659,50671,
                                   50678,50679,50681,50682 )) )

             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo )
       Group By EmpFil, Data

