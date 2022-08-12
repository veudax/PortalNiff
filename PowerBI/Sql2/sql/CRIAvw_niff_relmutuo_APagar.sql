create or replace view vw_niff_relmutuo_apagar as
Select EmpFil, To_date('01/' || SubStr(Periodosaldo,5,2) || '/' || SubStr(PeriodoSaldo,1,4),'dd/mm/yyyy') Data
     , Abs(Sum(EOVG)) EOVG, Abs(Sum(Rapido)) Rapido, Abs(Sum(Campibus)) Campibus
     , Abs(Sum(Aruja)) Aruja, Abs(Sum(ABC)) ABC
     , Abs(Sum(RIBE)) Ribe, Abs(Sum(VGI)) VGI
     , Abs(Sum(Cisne)) Cisne, Abs(Sum(VUG)) VUG
     , Abs(Sum(EOG)) EOG
     , Abs(Sum(Jibrail)) Jibrail
     , Abs(Sum(EOVG)) + Abs(Sum(Rapido)) + Abs(Sum(Campibus)) + Abs(Sum(jibrail)) +
       Abs(Sum(Aruja)) + Abs(Sum(ABC)) + Abs(Sum(RIBE)) + Abs(Sum(Cisne)) + Abs(Sum(VGI)) + Abs(Sum(VUG)) + Abs(Sum(EOG)) Consolidado
     , Abs(Sum(EOVG_C)) EOVG_C
     , Abs(Sum(Rapido_C)) Rapido_C
     , Abs(Sum(Ribe_C)) Ribe_C
     , Abs(Sum(EOG_C)) EOG_C
     , Abs(Sum(VUG_C)) VUG_C
     , Abs(Sum(Felbek_C)) Felbek_C
     , Abs(Sum(EOVG)) + Abs(Sum(Rapido)) + Abs(Sum(Campibus)) + Abs(Sum(jibrail)) +
       Abs(Sum(Aruja)) + Abs(Sum(ABC)) + Abs(Sum(RIBE)) + Abs(Sum(Cisne)) + Abs(Sum(VGI)) + Abs(Sum(VUG)) + Abs(Sum(EOG)) +
       Abs(Sum(EOVG_C)) + Abs(Sum(Rapido_C)) + Abs(Sum(Ribe_C)) + Abs(Sum(EOG_C)) + Abs(Sum(VUG_C)) + Abs(Sum(Felbek_C)) Total
     , '� Pagar' Tipo
     , Ordem
  From (-- Coluna EOVG
        Select 'Cisne' Empfil,
               s.Periodosaldo,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm')
           And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20773
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
        Select 'VGI' Empfil,
               s.Periodosaldo,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C ,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20781
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
        Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
        Select 'ABC' Empfil,
               s.Periodosaldo,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               4 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20772
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
        Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
        Select 'R�pido' Empfil,
               s.Periodosaldo,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                               From CTBsaldo S1
                              Where s1.nroplano = s.nroplano
                                And s1.codcontactb = s.codcontactb
                                And s1.codigoempresa = s.codigoempresa
                                And s1.codigofl = s.codigofl
                                And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               5 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20775
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
        Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Ribe' Empfil,
                 s.Periodosaldo,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                 From CTBsaldo S1
                                Where s1.nroplano = s.nroplano
                                  And s1.codcontactb = s.codcontactb
                                  And s1.codigoempresa = s.codigoempresa
                                  And s1.codigofl = s.codigofl
                                  And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG,
                 0 rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                 6 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 24485
             And s.codigoempresa = 1
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
          Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'Aruj�' Empfil,
                 s.Periodosaldo,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                 From CTBsaldo S1
                                Where s1.nroplano = s.nroplano
                                  And s1.codcontactb = s.codcontactb
                                  And s1.codigoempresa = s.codigoempresa
                                  And s1.codigofl = s.codigofl
                                  And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG,
                 0 rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                 7 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 20779
             And s.codigoempresa = 1
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
          Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'EOG' Empfil,
                 s.Periodosaldo,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                 From CTBsaldo S1
                                Where s1.nroplano = s.nroplano
                                  And s1.codcontactb = s.codcontactb
                                  And s1.codigoempresa = s.codigoempresa
                                  And s1.codigofl = s.codigofl
                                  And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG,
                 0 rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C ,
                 9 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 25139
             And s.codigoempresa = 1
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
          Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'Campibus' Empfil,
                 s.Periodosaldo,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                 From CTBsaldo S1
                                Where s1.nroplano = s.nroplano
                                  And s1.codcontactb = s.codcontactb
                                  And s1.codigoempresa = s.codigoempresa
                                  And s1.codigofl = s.codigofl
                                  And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG,
                 0 rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                11 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 25254
             And s.codigoempresa = 1
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
          Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'VGI' Empfil,
                 s.Periodosaldo,
                 0 EOVG,
                 0 rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                3 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 25254
             And s.codigoempresa = 1
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
          Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
        Union All
         -- Coluna Rapido
        Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C ,
               1 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20774
           And s.codigoempresa = 3
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Cisne' Empfil,
                 s.Periodosaldo,
                 0 EOVG,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                From CTBsaldo S1
                               Where s1.nroplano = s.nroplano
                                 And s1.codcontactb = s.codcontactb
                                 And s1.codigoempresa = s.codigoempresa
                                 And s1.codigofl = s.codigofl
                                 And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                 2 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 20773
             And s.codigoempresa = 3
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
        Select 'VGI' Empfil,
               s.Periodosaldo,
               0 EOVG,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20781
           And s.codigoempresa = 3
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
        Select 'ABC' Empfil,
               s.Periodosaldo,
               0 EOVG,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               4 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20772
           And s.codigoempresa = 3
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Ribe' Empfil,
                 s.Periodosaldo,
                 0 EOVG,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                From CTBsaldo S1
                               Where s1.nroplano = s.nroplano
                                 And s1.codcontactb = s.codcontactb
                                 And s1.codigoempresa = s.codigoempresa
                                 And s1.codigofl = s.codigofl
                                 And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                 6 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb In (24485,23380)
             And s.codigoempresa = 3
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'Aruj�' Empfil,
                 s.Periodosaldo,
                 0 EOVG,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                From CTBsaldo S1
                               Where s1.nroplano = s.nroplano
                                 And s1.codcontactb = s.codcontactb
                                 And s1.codigoempresa = s.codigoempresa
                                 And s1.codigofl = s.codigofl
                                 And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                 7 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 20779
             And s.codigoempresa = 3
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'EOG' Empfil,
                 s.Periodosaldo,
                 0 EOVG,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                From CTBsaldo S1
                               Where s1.nroplano = s.nroplano
                                 And s1.codcontactb = s.codcontactb
                                 And s1.codigoempresa = s.codigoempresa
                                 And s1.codigofl = s.codigofl
                                 And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                 9 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 25139
             And s.codigoempresa = 3
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'VUG' Empfil,
                 s.Periodosaldo,
                 0 EOVG,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                From CTBsaldo S1
                               Where s1.nroplano = s.nroplano
                                 And s1.codcontactb = s.codcontactb
                                 And s1.codigoempresa = s.codigoempresa
                                 And s1.codigofl = s.codigofl
                                 And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                10 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 24553
             And s.codigoempresa = 3
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'Campibus' Empfil,
                 s.Periodosaldo,
                 0 EOVG,
                 decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                       , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                       , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                                From CTBsaldo S1
                               Where s1.nroplano = s.nroplano
                                 And s1.codcontactb = s.codcontactb
                                 And s1.codigoempresa = s.codigoempresa
                                 And s1.codigofl = s.codigofl
                                 And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                11 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 25254
             And s.codigoempresa = 3
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         Select 'Jibrail' Empfil,
                 s.Periodosaldo,
                 0 EOVG,
                 0 rapido,
                 0 Campibus,
                 0 Aruja,
                 0 ABC,
                 0 RIBE,
                 0 VGI,
                 0 CISNE,
                 0 VUG,
                 0 EOG,
                 0 Jibrail,
                 0 EOVG_C,
                 0 Rapido_C,
                 0 Ribe_C,
                 0 EOG_C,
                 0 VUG_C,
                 0 Felbek_C,
                12 Ordem
            From Ctbsaldo s, ctbconta c
           Where s.nroplano = 10
             And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
             And c.codcontactb = 25254
             And s.codigoempresa = 3
             And s.nroplano = c.nroplano
             And s.codcontactb = c.codcontactb
             And c.lancamento = 'S'
           Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                    s.periodosaldo
         Union All
         -- Coluna Campibus
         Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               Sum(s.Vldebitosaldo) - Sum(s.Vlcreditosaldo) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               1 Ordem
          From Ctbsaldo s, Ctbconta c
         Where s.Nroplano = 10
           And s.Codigoempresa = 9
           And s.Periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm')
           And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.Codcontactb = 20774
           And s.Nroplano = c.Nroplano
           And s.Codcontactb = c.Codcontactb
           And c.Lancamento = 'S'
         Group By Lpad(s.Codigoempresa, 3, '0') || '/' || Lpad(s.Codigofl, 3, '0'),
                  s.Periodosaldo
         Union All
         Select 'Cisne' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20773
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VGI' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20781
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'ABC' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               4 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20772
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'R�pido' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               5 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20775
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Ribe' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               6 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24485
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Aruj�' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               7 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20779
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'EOG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25139
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VUG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              10 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24553
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Jibrail' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              12 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24553
           And s.codigoempresa = 9
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         -- Coluna Aruj�
         Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 EOVG_C,
               0 Jibrail,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               1 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20774
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Cisne' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20773
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VGI' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20781
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'ABC' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               4 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20772
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'R�pido' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               5 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20775
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Ribe' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               6 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24485
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'EOG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25139
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VUG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              10 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24553
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Campibus' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              11 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Jibrail' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              12 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 6
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         -- Coluna ABC
         Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               1 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20774
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Cisne' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20773
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VGI' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20781
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'R�pido' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               5 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20775
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Ribe' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               6 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24485
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Aruj�' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               7 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20779
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'EOG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25139
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VUG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              10 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24553
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Campibus' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 EOVG_C,
               0 Jibrail,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              11 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Jibrail' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 EOVG_C,
               0 Jibrail,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              11 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 2
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         -- Coluna RIBE
         Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               1 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20774
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Cisne' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20773
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VGI' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20781
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'ABC' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               4 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20772
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'R�pido' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               5 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20775
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Aruj�' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               7 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20779
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'EOG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25139
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VUG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              10 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24553
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Campibus' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              11 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Jibrail' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              12 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 13
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         -- Coluna VGI
         Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select Min( periodosaldo) From ctbsaldo c  Where c.codigoempresa = s.codigoempresa And c.codcontactb = s.codcontactb)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo) +
                                    Sum(Decode(s1.periodosaldo, (Select Min( periodosaldo) From ctbsaldo c  Where c.codigoempresa = s.codigoempresa And c.codcontactb = s.codcontactb)
                                              , s1.vlcredantsaldo - s1.vldebantsaldo, 0))
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc c
                                                             Where c.codigoempresa = s.codigoempresa) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               1 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20774
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By s.codigoempresa, s.codcontactb,
                  s.periodosaldo
         Union All
         Select 'Cisne' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20773
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'ABC' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               4 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20772
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'R�pido' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               5 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 10722
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Ribe' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               6 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24485
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Aruj�' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               7 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20779
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'EOG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25139
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VUG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              10 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24553
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Campibus' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              11 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Jibrail' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              12 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 7
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         -- Coluna Cisne
         Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               1 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20774
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VGI' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20781
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'ABC' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C ,
               4 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20772
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'R�pido' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               5 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20775
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Ribe' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               6 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24485
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Aruj�' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               7 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20779
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'EOG' Empfil,
                s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25139
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VUG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              10 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24553
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Campibus' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              11 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
        Union All
         Select 'Jibrail' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              12 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 4
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         -- Coluna VUG
         Select 'Cisne' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               2 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20773
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VGI' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20781
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'ABC' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               4 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20772
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'R�pido' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               5 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20775
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Ribe' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               6 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24485
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Aruj�' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               7 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20779
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'EOG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C ,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24439
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Campibus' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              11 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'Jibrail' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              12 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 25254
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         -- Coluna Jibrail
         Union All
         Select 'VGI' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
               3 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 20722
           And s.codigoempresa = 28
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         -- Coluna RIBE ap�s consolidado
         Union All
         Select 'VUG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               0 Felbek_C,
              10 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24406
           And s.codigoempresa = 1
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         -- Coluna VUG
         Union All
         Select 'EOVG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG_C,
               0 Felbek_C,
               1 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24439
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'EOG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) VUG_C,
               0 Felbek_C,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 26658
           And s.codigoempresa = 26
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         -- Coluna Felbek
         Union All
         Select 'EOG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Felbek_C,
               9 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24178
           And s.codigoempresa = 15
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
         Union All
         Select 'VUG' Empfil,
               s.Periodosaldo,
               0 EOVG,
               0 rapido,
               0 Campibus,
               0 Aruja,
               0 ABC,
               0 RIBE,
               0 VGI,
               0 CISNE,
               0 VUG,
               0 EOG,
               0 Jibrail,
               0 EOVG_C,
               0 Rapido_C,
               0 Ribe_C,
               0 EOG_C,
               0 VUG_C,
               decode( s.periodosaldo, (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc)
                     , Sum(s.vlcreditosaldo - s.vldebitosaldo)
                     , Sum( (Select Sum(s1.vlcreditosaldo - s1.vldebitosaldo)
                              From CTBsaldo S1
                             Where s1.nroplano = s.nroplano
                               And s1.codcontactb = s.codcontactb
                               And s1.codigoempresa = s.codigoempresa
                               And s1.codigofl = s.codigofl
                               And S1.Periodosaldo Between (Select To_Char(Min(data_ini_ex),'yyyymm') From ctbexerc) And s.Periodosaldo ))) Felbek_C,
              10 Ordem
          From Ctbsaldo s, ctbconta c
         Where s.nroplano = 10
           And s.periodosaldo Between To_Char(Add_Months(Trunc(Sysdate, 'rr'), -12), 'yyyymm') And To_Char(Last_Day(Trunc(Sysdate)), 'yyyymm')
           And c.codcontactb = 24431
           And s.codigoempresa = 15
           And s.nroplano = c.nroplano
           And s.codcontactb = c.codcontactb
           And c.lancamento = 'S'
         Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                  s.periodosaldo
  ) Group By EmpFil, PeriodoSAldo, Ordem

