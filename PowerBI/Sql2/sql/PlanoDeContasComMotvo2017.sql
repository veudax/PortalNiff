Select Distinct c.classificador,
       c.codcontactb || '-' || c.digito conta, 
       c.Nomeconta Descricao, 
       c.lancamento Lcto, 
       c.apuracao Apu, 
       c.informacustosconta CC, 
       Case When SubStr(c.classificador,3,1) = '0' Then '1'
         When SubStr(c.classificador,5,1) = '0' Then '2'
         When SubStr(c.classificador,7,2) = '00' Then '3'             
         When SubStr(c.classificador,10,2) = '00' Then '4'
         When SubStr(c.classificador,13,3) = '000' Then '5'             
         When SubStr(c.classificador,17,4) = '0000' Then '6'             
         Else '7' End Grau,
       Decode(c.natureza, 'D', 'Devedora', 'Credora') Natureza,
       c.apulalur Lalur 
  From Ctbconta c,
       ctbsaldo s
 Where c.codcontactb = s.codcontactb
   And c.nroplano = s.nroplano
   And s.nroplano = 10
-- And s.codigoempresa = 1
--   And s.codigofl = 1
   And s.periodosaldo Between '201701' And '201812'
 Order By c.classificador    