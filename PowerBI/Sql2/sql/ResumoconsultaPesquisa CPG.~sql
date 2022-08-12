Select x.Codcontactb
--     , Nrforn, Rsocialforn 
     , Sum(valorCPG) valorCPG
     , Abs(Sum(ValorCTB)) ValorCTB
  From (Select Distinct codcontactb, nroplano
          From Cpgcontactb_Fornecedor) cf
     , (Select Cf.Codcontactb, Sum(Fc_Cpg_Vlrliquido(d.Coddoctocpg)) As ValorCPG, 0 ValorCTB       
          From Cpgdocto d, Cpgitdoc i, Bgm_Fornecedor f, Bcomovto m, Cprtpdoc t, Cpgcontactb_Fornecedor Cf, Ctbconta c
         Where d.Codtpdoc Not In ('DEP', 'BOL', 'CTT', 'PRE', 'AD', 'DEB', 'REL', 'DRF', 'DES', 'GPS', 'GRC', 'IPV', 'DPV', 'RPA', 'IND', 'BOS', 'GAR', 'REC')
           And Emissaocpg Between '01-jan-2016' And '31-dec-2999'
           And Entradacpg Between '01-jan-1990' And '30-sep-2019'
           And ((Substr(d.Nrodoctocpg, 1, 3) = 'DV-') 
           And  ((d.Coddoctocpg_Devol Is Null) 
            Or   ((Select Count(Coddoctocpg)
                     From Cpgdocto Doc
                    Where Doc.Coddoctocpg = d.Coddoctocpg_Devol
                      And (Doc.Pagamentocpg > '30-sep-2019'
                       Or  Doc.Pagamentocpg Is Null)) <> 0)) 
            Or (Substr(d.Nrodoctocpg, 1, 3) <> 'DV-'
           And (Pagamentocpg Is Null 
            Or  Pagamentocpg > '30-sep-2019')))
           And Statusdoctocpg <> 'C'
           And d.Coddoctocpgsubst Is Null
           And d.Codigoempresa = 3 
           And f.Codigoforn = d.Codigoforn
           And m.Codmovtobco(+) = d.Codmovtobco
           And t.Codigoempresa = d.Codigoempresa
           And t.Codigofl = d.Codigofl
           And t.Codtpdoc = d.Codtpdoc
           And i.Coddoctocpg = d.Coddoctocpg
           And Cf.Codigoforn = f.Codigoforn
           And c.Codcontactb = Cf.Codcontactb
           And c.Nroplano = Cf.Nroplano
           And Cf.Nroplano = 10
           And c.Classificador Between '2.1.2.01.01.001.0000' And '2.1.2.01.01.001.9999'
         Group By Cf.Codcontactb, Nrforn, Rsocialforn
         Union All
        Select x.codcontactb, 0 VAlorCPG, Sum(resultado) + Sum(saldoini) ValorCTB
          From (Select ctb.codcontactb
                     , Sum(s.vldebitosaldo) - Sum(s.vlcreditosaldo) resultado
                     , 0 saldoini
                     , Sum(s.vldebitosaldo) debito
                     , Sum(s.vlcreditosaldo) credito
                  From Ctbconta ctb, ctbsaldo s
                 Where ctb.nroplano =10
                   And s.nroplano = ctb.nroplano
                   And s.periodosaldo = '201909'
                   And s.codcontactb = ctb.codcontactb
                   And s.codigoempresa = 3
                   And ctb.classificador Between '2.1.2.01.01.001.0000' And '2.1.2.01.01.001.9999'
                 Group By ctb.codcontactb
                 Union All
                Select ctb.codcontactb
                     , 0 resultado
                     , Sum(s.vldebantsaldo) - Sum(s.vlcredantsaldo) saldoini
                     , 0 debito
                     , 0 credito
                  From Ctbconta ctb, ctbsaldo s
                 Where ctb.nroplano =10
                   And s.nroplano = ctb.nroplano
                   And s.periodosaldo Between '201901' And '201909'
                   And s.codcontactb = ctb.codcontactb
                   And s.codigoempresa = 3
                   And ctb.classificador Between '2.1.2.01.01.001.0000' And '2.1.2.01.01.001.9999'
                 Group By ctb.codcontactb ) x 
     Group By codcontactb) x
 Where cf.codcontactb = x.codcontactb
   And cf.nroplano = 10    
 Group By x.Codcontactb
--     , Nrforn, Rsocialforn 