create or replace view pbi_radar_ebidaprocjuridicos as
Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                               s.periodosaldo,
                               Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) ProcessosJuridicos
                          From Ctbsaldo s, ctbconta c
                         Where s.nroplano = 10
                           And s.codigoempresa || s.codigofl in (11,12,21,31,315,41,51,61,91,131,261,263)
                           And s.periodosaldo Between '201705'
                           And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')
                           And c.codcontactb In (42863,43673,50561,50546,50211)
                           And s.nroplano = c.nroplano
                           And s.codcontactb = c.codcontactb
                         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                                  s.periodosaldo

