create or replace view vw_niff_relmutuosoc_apagar as
Select EmpFil, To_date('01/' || SubStr(Periodosaldo,5,2) || '/' || SubStr(PeriodoSaldo,1,4),'dd/mm/yyyy') Data
     , Abs(Sum(Neida)) Neida
     , Abs(Sum(JoseRoberto)) JoseRoberto
     , Abs(Sum(Thiago)) Thiago
     , Abs(Sum(Roque)) Roque
     , Abs(Sum(Christiane)) Christiane
     , Abs(Sum(Neida)) + Abs(Sum(JoseRoberto)) + Abs(Sum(Thiago)) +
       Abs(Sum(Roque)) + Abs(Sum(Christiane)) Consolidado
     , 'À Pagar' Tipo
     , Ordem
 From (-- Coluna Doutor Thiago
        Select 'ABC' Empfil,
               s.Periodosaldo,
               0 Neida,
               0 JoseRoberto,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Thiago,
               0 Roque,
               0 Christiane,
               1 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm')
           And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 11842
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
        Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 Neida,
               0 JoseRoberto,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Thiago,
               0 Roque,
               0 Christiane,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm')
           And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 11842
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
        Union All -- Coluna Dr Jose Roberto
        Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 Neida,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) JoseRoberto,
               0 Thiago,
               0 Roque,
               0 Christiane,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm')
           And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 11841
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo

            )
 Group By EmpFil, To_date('01/' || SubStr(Periodosaldo,5,2) || '/' || SubStr(PeriodoSaldo,1,4),'dd/mm/yyyy'), Ordem

