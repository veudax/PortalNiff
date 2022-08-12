Select d.nrodoctocpg, d.nroparcelacpg 
     , a.nrodoctocpg NrAdto, a.nroparcelacpg ParAdto
     , f.nrforn, d.codtpdoc, d.seriedoctocpg
  From cpgDocto d, cpgdocto a, bgm_fornecedor f
 Where d.coddoctocpg = a.coddoctocpg_adto
   And d.codigoEmpresa = 1
   And d.codigofl =1
   And f.codigoforn = d.codigoforn
   And d.emissaocpg > '01-jun-2017'