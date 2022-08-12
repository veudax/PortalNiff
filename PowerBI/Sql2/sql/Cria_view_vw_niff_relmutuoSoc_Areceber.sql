create or replace view vw_niff_relmutuoSoc_Areceber as
Select EmpFil, To_date('01/' || SubStr(Periodosaldo,5,2) || '/' || SubStr(PeriodoSaldo,1,4),'dd/mm/yyyy') Data
     , Abs(Sum(Neida)) Neida
     , Abs(Sum(JoseRoberto)) JoseRoberto
     , Abs(Sum(Thiago)) Thiago
     , Abs(Sum(Roque)) Roque
     , Abs(Sum(Christiane)) Christiane
     , Abs(Sum(Neida)) + Abs(Sum(JoseRoberto)) + Abs(Sum(Thiago)) +
       Abs(Sum(Roque)) + Abs(Sum(Christiane)) Consolidado
     , 'À Receber' Tipo
     , Ordem   
 From (-- Coluna Dona Neida
        Select 'EOVG' Empfil,
               s.Periodosaldo,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Neida,
               0 JoseRoberto,
               0 Thiago,
               0 Roque,
               0 Christiane,
               1 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20783
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo
         Union All
        Select 'VGI' Empfil,
               s.Periodosaldo,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Neida,
               0 JoseRoberto,
               0 Thiago,
               0 Roque,
               0 Christiane,
               3 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20783
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo         
         Union All
        Select 'Rápido' Empfil,
               s.Periodosaldo,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Neida,
               0 JoseRoberto,
               0 Thiago,
               0 Roque,
               0 Christiane,
               6 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20783
           And s.codigoempresa = 3
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo    
         Union All
         -- Coluna Doutor José Roberto
        Select 'VGI' Empfil,
               s.Periodosaldo,
               0 Neida,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) JoseRoberto,
               0 Thiago,
               0 Roque,
               0 Christiane,
               3 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20784
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo  
         Union All
        Select 'Rápido' Empfil,
               s.Periodosaldo,
               0 Neida,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) JoseRoberto,
               0 Thiago,
               0 Roque,
               0 Christiane,
               6 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20784
           And s.codigoempresa = 3
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo     
         Union All
         -- Coluna Doutor Thiago      
        Select 'VGI' Empfil,
               s.Periodosaldo,
               0 Neida,
               0 JoseRoberto,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Thiago,
               0 Roque,
               0 Christiane,
               3 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20786
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo           
         Union All
        Select 'VGI' Empfil,
               s.Periodosaldo,
               0 Neida,
               0 JoseRoberto,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Thiago,
               0 Roque,
               0 Christiane,
               6 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20786
           And s.codigoempresa = 3
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo   
         Union All 
         -- Coluna Doutor Roque
        Select 'VGI' Empfil,
               s.Periodosaldo,
               0 Neida,
               0 JoseRoberto,
               0 Thiago,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Roque,
               0 Christiane,
               3 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20785
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo   
         Union All
        Select 'Rápido' Empfil,
               s.Periodosaldo,
               0 Neida,
               0 JoseRoberto,
               0 Thiago,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Roque,
               0 Christiane,
               6 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20785
           And s.codigoempresa = 3
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo   
         Union All 
         -- Coluna Dona Christiane
        Select 'VGI' Empfil,
               s.Periodosaldo,
               0 Neida,
               0 JoseRoberto,
               0 Thiago,
               0 Roque,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Christiane,
               3 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20787
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo   
         Union All
        Select 'Rápido' Empfil,
               s.Periodosaldo,
               0 Neida,
               0 JoseRoberto,
               0 Thiago,
               0 Roque,
               decode( s.periodosaldo, To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm')
                     , Sum(s.vldebitosaldo - s.vlcreditosaldo) 
                     , Sum( (Select Sum(s1.vldebitosaldo - s1.vlcreditosaldo) 
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between To_Char(Add_months(trunc(Sysdate,'rr'),-36),'yyyymm') And s.Periodosaldo ))) Christiane,
               6 Ordem              
          From Ctbsaldo s, ctbconta c 
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') 
           And To_Char(Add_Months(Last_Day(Trunc(Sysdate)), -1), 'yyyymm') 
           And c.codcontactb = 20787
           And s.codigoempresa = 3
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'), 
                  s.periodosaldo   )
 Group By EmpFil, To_date('01/' || SubStr(Periodosaldo,5,2) || '/' || SubStr(PeriodoSaldo,1,4),'dd/mm/yyyy'), Ordem