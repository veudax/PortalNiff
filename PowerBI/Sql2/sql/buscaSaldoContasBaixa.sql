

Select Sum(saldoIni) saldoIni, Sum(resultado) resultado
     , m.conta, classificador
     , sum(saldoini) + Sum(resultado) saldofin
     , decode(g.CodigoGrupo, Null, m.Conta, CodigoGrupo || ' G') grupo
From (Select a.codigogrupo, regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4') conta
          From niff_ctb_contasativo a, niff_chm_empresas e, Atfitem i
         Where e.Idempresa = a.Idempresa  
           And i.codigo = a.codigoativo 
           And e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')
           And e.idempresa = 5 ) g 
     , (-- busca saldo baixa
Select Distinct Sum(s.vldebantsaldo) - Sum(s.vlcredantsaldo) saldoIni
     , 0 resultado 
     , regexp_replace(LPAD(Substr(i.conta,1,length(i.conta)-4)||'0000', 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4') conta
     , cc.classificador
     , cc.codcontactb
  From Atfitem i, atfitem_contactb c, 
  ctbsaldo s, ctbconta cc
 Where c.codigoempresa = i.codigoempresa
   And c.codigofl = i.codigofl
   And c.codigo = i.codigo
   And c.nroplano = 10
   And s.nroplano = c.nroplano
   And s.codcontactb = c.codcontactb_creditobaixa
   And s.codigoempresa = c.codigoempresa
   And s.periodosaldo Between '201901' And '201910'
  -- And s.codcontactb = 10984
   And s.codigoempresa = 3   
   And cc.codcontactb = s.codcontactb
   And cc.nroplano = s.nroplano
   And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'
Group By conta, cc.classificador, cc.codcontactb 
Union All
Select Distinct 0 saldoini
     , Sum(s.vldebitosaldo) - Sum(s.vlcreditosaldo) resultado 
     , regexp_replace(LPAD(Substr(i.conta,1,length(i.conta)-4)||'0000', 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4') conta
     , cc.classificador
     , cc.codcontactb
  From Atfitem i, atfitem_contactb c, 
  ctbsaldo s, ctbconta cc
 Where c.codigoempresa = i.codigoempresa
   And c.codigofl = i.codigofl
   And c.codigo = i.codigo
   And c.nroplano = 10
   And s.nroplano = c.nroplano
   And s.codcontactb = c.codcontactb_creditobaixa
   And s.codigoempresa = c.codigoempresa
   And s.periodosaldo = '201910'
  -- And s.codcontactb = 10984
   And s.codigoempresa = 3   
   And cc.codcontactb = s.codcontactb
   And cc.nroplano = s.nroplano
   And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'
Group By conta, cc.classificador, cc.codcontactb  
Union All
Select Distinct Sum(s.vldebantsaldo) - Sum(s.vlcredantsaldo) saldoIni
     , 0 resultado 
     , regexp_replace(LPAD(Substr(i.conta,1,length(i.conta)-4)||'0000', 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4') conta
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
   And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'   
Group By conta, cc.classificador, cc.codcontactb 

Union All
Select Distinct 0 saldoini
     , Sum(s.vldebitosaldo) - Sum(s.vlcreditosaldo) resultado 
     , regexp_replace(LPAD(Substr(i.conta,1,length(i.conta)-4)||'0000', 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4') conta
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
   And cc.classificador Between '1.2.3.00.00.000.0000' And '1.2.4.99.99.999.9999'   
Group By conta, cc.classificador , cc.codcontactb ) m
Where m.conta = g.Conta(+)
Group By m.conta, classificador
, decode(g.CodigoGrupo, Null, m.Conta, CodigoGrupo || ' G') 