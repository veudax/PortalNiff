create or replace view pbi_radar_finan_receita as
(Select 'Receita Financeira Total' Grupo, 0 Ordem,
           To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
           Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
           Sum(nvl(Abs(SaldoAcumulado),0)) Valor,
           0 Percentual,
           'F' Tipo
      From (Select LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
                   s.periodosaldo,
                   Sum(s.vlcreditosaldo)- Sum(s.vldebitosaldo) saldoAcumulado
              From Ctbsaldo s, ctbconta c
             Where s.nroplano = 10

               And s.periodosaldo Between '201705'
               And To_Char(ADD_MONTHS(Last_day(Trunc(Sysdate)), -1),'yyyymm')

               And ((s.codigoempresa || s.codigofl = 11 -- EOVG Dutra
               And c.codcontactb In (30014,30094,30096,30211,30215,30227,30228,30229,30230,30232,
                                     30236,30237,30239,30240,30456,30457,30483,30485,30504,30530,30691))

                Or (s.codigoempresa || s.codigofl = 12 -- EOVG Lavras
               And c.codcontactb In (30211,30691))

                Or (s.codigoempresa || s.codigofl = 21 -- ABC
               And c.codcontactb In (30014,30094,30504,30227,30215))

                Or (s.codigoempresa || s.codigofl In (31,315) -- Rapido
               And c.codcontactb In (30236,30094,30239,30240,30228,30229,30230,30227,30461,30217,30690,30238))

                Or (s.codigoempresa || s.codigofl in (41) -- Cisne
                And c.codcontactb In (30236,30239,30227,30230,30228,30456,30232,30231))

                Or (s.codigoempresa || s.codigofl in (61) -- Aruja
                And c.codcontactb In (30014, 30094, 30227, 30215 ))

                Or (s.codigoempresa || s.codigofl in (91) -- Campibus
                And c.codcontactb In (30096, 30483, 30485, 30231 ))

                Or (s.codigoempresa || s.codigofl in (131) -- Ribe
                And c.codcontactb In (30014, 30227, 30215, 30230 ))

                Or (s.codigoempresa || s.codigofl in (261,263) -- VUG Dutra e Bebedouro
                And c.codcontactb In (30014, 30211, 30691, 30215))

                 Or (s.codigoempresa || s.codigofl in (51)
                And c.codcontactb In (30014,30094,30096,30211,30215,30227,30228,30229,30230,30232,
                                     30236,30237,30239,30240,30456,30457,30483,30485,30504,30530,30691))
                                      )

               And s.nroplano = c.nroplano
               And s.codcontactb = c.codcontactb

             Group By LPAD(s.CODIGOEMPRESA,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
                      s.periodosaldo)
     Group By EmpFil, To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy'))

