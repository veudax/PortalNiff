Select Nrodoctocpg,
       Nroparcelacpg,
       Seriedoctocpg,
       d.Codtpdoc,
       Cf.Codcontactb,
       Fc_Cpg_Vlrliquido(d.Coddoctocpg) As Vlr_Liquido_Real,
       Entradacpg Entradacpg,
       Emissaocpg,
       Vencimentocpg,
       Statusdoctocpg,
       Pagamentocpg,
       Descontocpg,
       Acrescimocpg,
       Vlrinsscpg,
       Vlrirrfcpg,
       Obsdoctocpg,
       Sum(Valoritemdoc) Total_Docto,
       Nrforn,
       Rsocialforn,
       Nfantasiaforn,
       m.Codbanco,
       m.Docmovtobco,
       d.Codigofl,
       Vlrpiscpg,
       Vlrcofinscpg,
       Vlrcslcpg,
       Vlrisscpg,
       t.Substituitpdoc,
       Nvl(Valor_Adto, 0) Valor_Adto,
       Nvl(Valor_Devol, 0) Valor_Devol,
       d.Coddoctocpg,
       d.Usuariocpg_Exc,
       d.Datahoracpg_Exc,
       Case
          When Substr(Nrodoctocpg, 1, 3) = 'DV-' Then
           Decode(d.Coddoctocpg_Devol,
                  Null,
                  'N',
                  'S')
          Else
           Decode(d.Coddoctocpg_Adto, Null, 'N', 'S')
        End As Dv_Adt_Associada,
       Vlrsestsenatcpg,
       c.Classificador,
       c.Nomeconta,
       Case
          When (Substr(Nrodoctocpg, 1, 3) = 'DV-') And
               (d.Coddoctocpg_Devol <> 0) Then
           (Select Sum(Fc_Cpg_Vlrliquido(d.Coddoctocpg))
              From Cpgdocto a
             Where a.Coddoctocpg = d.Coddoctocpg_Devol
               And ((a.Pagamentocpg >
                   To_Date('30/09/2019 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /*                                      PENTRADAFIN */
                   ) Or (a.Pagamentocpg Is Null))
               And a.Entradacpg <=
                   To_Date('30/09/2019 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /*                                    PENTRADAFIN */
               And a.Codtpdoc Not In ('DEP', 'BOL', 'CTT', 'PRE', 'AD', 'DEB', 'REL', 'DRF', 'DES', 'GPS', 'GRC', 'IPV', 'DPV', 'RPA', 'IND', 'BOS', 'GAR', 'REC')
               And a.Statusdoctocpg <> 'C')
          Else
           0
        End As Dv_Desconto,
       (Select Count(D2.Coddoctocpg)
          From Cpgdocto D2
         Inner Join Cpgitdoc I2 On I2.Coddoctocpg = D2.Coddoctocpg
         Inner Join (Select Codtpdespesa, Nroplano, Codcontactb
                      From Cpgtpdes_Ctbconta
                     Where Nroplano = 10 /*                                         PPLANO */
                    ) Dc2 On Dc2.Codtpdespesa = I2.Codtpdespesa
         Inner Join Ctbconta C2 On C2.Nroplano = Dc2.Nroplano
         Where D2.Coddoctocpg = d.Coddoctocpg
           And C2.Classificador Between '2.1.2.01.01.001.0000' /*                             P_CODCLASSCPINI */
               And '2.1.2.01.01.001.9999' /*                 P_CODCLASSCPFIN */
        ) Cp
  From Cpgdocto               d,
       Cpgitdoc               i,
       Bgm_Fornecedor         f,
       Bcomovto               m,
       Cprtpdoc               t,
       Cpgcontactb_Fornecedor Cf,
       Ctbconta               c
 Where d.Codtpdoc Not In ('DEP', 'BOL', 'CTT', 'PRE', 'AD', 'DEB', 'REL', 'DRF', 'DES', 'GPS', 'GRC', 'IPV', 'DPV', 'RPA', 'IND', 'BOS', 'GAR', 'REC')
   And Emissaocpg Between
       To_Date('01/01/2016 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /*             PEMISSAOINI */
       And To_Date('31/12/2999 00:00:00', 'DD/MM/YYYY HH24:MI:SS')/*             PEMISSAOFIN */
   And Entradacpg Between
       To_Date('01/01/1990 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /*             PENTRADAINI */
       And To_Date('30/09/2019 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /*             PENTRADAFIN */
   And ((Substr(d.Nrodoctocpg, 1, 3) = 'DV-') And
       ((d.Coddoctocpg_Devol Is Null) Or
       ((Select Count(Coddoctocpg)
             From Cpgdocto Doc
            Where Doc.Coddoctocpg = d.Coddoctocpg_Devol
              And ((Doc.Pagamentocpg >
                  To_Date('30/09/2019 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /*                                     PENTRADAFIN */
                  ) Or                  
                  (Doc.Pagamentocpg Is Null))) <> 0)) Or
       (Substr(d.Nrodoctocpg, 1, 3) <> 'DV-' And
       (Pagamentocpg Is Null Or
       Pagamentocpg >
       To_Date('30/09/2019 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /*               PENTRADAFIN */
       )))
   And Statusdoctocpg <> 'C'
   And d.Coddoctocpgsubst Is Null
   And d.Codigoempresa = 3 /*             PEMPRESA */
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
   And c.Classificador Between '2.1.2.01.01.001.0000' /*             PCLASINI */
       And '2.1.2.01.01.001.9999' /* PCLASFIN */
 Group By c.Classificador,
          Rsocialforn,
          Nfantasiaforn,
          Nrforn,
          d.Codigofl,
          Seriedoctocpg,
          Nrodoctocpg,
          Nroparcelacpg,
          d.Codtpdoc,
          Emissaocpg,
          Entradacpg,
          Vencimentocpg,
          Pagamentocpg,
          Statusdoctocpg,
          Descontocpg,
          Acrescimocpg,
          Vlrinsscpg,
          Vlrirrfcpg,
          Obsdoctocpg,
          m.Codbanco,
          m.Docmovtobco,
          c.Nomeconta,
          Cf.Codcontactb,
          Contabil_Matserv_Mesant,
          d.Usuariocpg_Exc,
          d.Datahoracpg_Exc,
          Vlrpiscpg,
          Vlrcofinscpg,
          Vlrcslcpg,
          Vlrisscpg,
          Valor_Adto,
          Valor_Devol,
          Substituitpdoc,
          d.Coddoctocpg,
          d.Coddoctocpg_Devol,
          d.Coddoctocpg_Adto,
          Vlrsestsenatcpg
 Order By Classificador