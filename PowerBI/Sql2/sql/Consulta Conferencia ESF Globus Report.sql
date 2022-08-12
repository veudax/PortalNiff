Select e.dtentradaentra entrada, f.nrforn, f.rsocialforn, f.inscestadualforn, formatacnpj_cpf(f.nrinscricaoforn, f.tpinscricaoforn) nrinscricaoforn,
       e.codtpdoc, e.nrdocentra, e.serieentra, e.dtemissaoentra Emissao,
       e.codclassfisc CFOP, c.Codcontactb, e.vlcontabilentra, e.icmsvalorentra, e.icmsbaseentra, 
       e.icmsaliqentra, e.icmsisentaentra, e.icmsoutrasentra,
       e.icmssubstbase, e.icmssubstvalor, e.icms_retido_base, e.icms_retido_valor, 
       e.codoperfiscal_icmsentra, e.codsittributaria, e.statusentra, e.codmodelo
  From esfentra e
     , bgm_fornecedor f
     , cpgcontactb_fornecedor c
 Where e.codigoforn = f.codigoforn
   And c.codigoforn = f.codigoforn
   And c.nroplano = 10     
   And e.Codigoempresa = 3
   And e.codigofl = 1
   And e.dtentradaentra Between '01-mar-2018' And '31-mar-2018'
