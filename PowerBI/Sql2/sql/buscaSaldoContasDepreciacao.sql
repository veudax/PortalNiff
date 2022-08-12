Select Sum(saldoIni) saldoIni, Sum(resultado) resultado
     , conta, classificador
     , sum(saldoini) + Sum(resultado) saldofin
From (
Select Distinct Sum(s.vldebantsaldo) - Sum(s.vlcredantsaldo) saldoIni
     , 0 resultado
     , Substr(i.conta,1,length(i.conta)-4)||'0000' conta
     , cc.classificador
     , cc.codcontactb
  From Atfitem i, atfitem_contactb c, 
  ctbsaldo s, ctbconta cc
 Where c.codigoempresa = i.codigoempresa
   And c.codigofl = i.codigofl
   And c.codigo = i.codigo
   And c.nroplano = 10
   And s.nroplano = c.nroplano
   And s.codcontactb = c.contactbdpc
   And s.codigoempresa = c.codigoempresa
   And s.periodosaldo Between '201901' And '201910'
  -- And s.codcontactb = 10984
   And s.codigoempresa = 3   
   And cc.codcontactb = s.codcontactb
   And cc.nroplano = s.nroplano
Group By conta, cc.classificador, cc.codcontactb

Union All
Select Distinct 0 saldoini
     , Sum(s.vldebitosaldo) - Sum(s.vlcreditosaldo) resultado
     , Substr(i.conta,1,length(i.conta)-4)||'0000' conta
     , cc.classificador
     , cc.codcontactb
  From Atfitem i, atfitem_contactb c, 
  ctbsaldo s, ctbconta cc
 Where c.codigoempresa = i.codigoempresa
   And c.codigofl = i.codigofl
   And c.codigo = i.codigo
   And c.nroplano = 10
   And s.nroplano = c.nroplano
   And s.codcontactb = c.contactbdpc
   And s.codigoempresa = c.codigoempresa
   And s.periodosaldo = '201910'
  -- And s.codcontactb = 10984
   And s.codigoempresa = 3   
   And cc.codcontactb = s.codcontactb
   And cc.nroplano = s.nroplano
Group By conta, cc.classificador , cc.codcontactb
)      Group By conta, classificador