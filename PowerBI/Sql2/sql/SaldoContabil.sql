Select Round(Sum(Nvl(Saldoacumulado, 0)), 2) Valor, codtplnc, Periodosaldo
  From (Select Sum(s.Vldebitosaldo) - Sum(s.Vlcreditosaldo) Saldoacumulado, s.codtplnc, Periodosaldo
          From Ctbsaldo s,
               Ctbconta c,
               (Select Distinct Mc.Modalidade,
                                Mc.Codigoforn,
                                Mc.Idempresa,
                                Mc.Numeroplano,
                                Codigocontacurtoprevisto,
                                Codigocontalongoprevisto
                  From Niff_Ctb_Parametrosendividamento Mc) Mc
         Where s.Nroplano = Mc.Numeroplano
           And s.Periodosaldo Between 202001 And 202006
           And s.Nroplano = c.Nroplano
           And s.Codcontactb = c.Codcontactb
           And c.Codcontactb = Mc.Codigocontacurtoprevisto
           And c.Nroplano = Mc.Numeroplano
           And Mc.Idempresa = 5
           And Mc.Codigoforn = 8555
           And Mc.Modalidade = 'Capital de Giro'
           And s.Codigoempresa = 3
           And (s.Codigofl = 1)-- Or s.Codigofl = 15)
           Group By  s.codtplnc, Periodosaldo
           Union All
         Select Sum(s.vldebantsaldo) - Sum(s.vlcredantsaldo) Saldoacumulado, s.codtplnc, Periodosaldo
          From Ctbsaldo s,
               Ctbconta c,
               (Select Distinct Mc.Modalidade,
                                Mc.Codigoforn,
                                Mc.Idempresa,
                                Mc.Numeroplano,
                                Codigocontacurtoprevisto,
                                Codigocontalongoprevisto
                  From Niff_Ctb_Parametrosendividamento Mc) Mc
         Where s.Nroplano = Mc.Numeroplano
           And s.Periodosaldo = 202001
           And s.Nroplano = c.Nroplano
           And s.Codcontactb = c.Codcontactb
           And c.Codcontactb = Mc.Codigocontacurtoprevisto
           And c.Nroplano = Mc.Numeroplano
           And Mc.Idempresa = 5
           And Mc.Codigoforn = 8555
           And Mc.Modalidade = 'Capital de Giro'
           And s.Codigoempresa = 3
           And (s.Codigofl = 1)-- Or s.Codigofl = 15)
           Group By  s.codtplnc, Periodosaldo           
           )
Group By codtplnc, Periodosaldo