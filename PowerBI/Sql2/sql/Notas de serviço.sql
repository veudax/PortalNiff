-- itens
Select n.codintnf, n.numeronf, n.entradasaidanf, n.entradasaidanf, n.codigoforn, n.serienf, n.valortotalnf, n.aliqpisnf, n.vlrpisnf
     , n.aliqconfinsnf, n.vlrconfinsnf, n.aliqinssnf, n.vlrinssnf, n.aliqissnf, n.vlrissnf, n.aliqirnf, n.vlrimpostorendanf
     , n.aliqcsllnf, n.vlrcsllnf, m.codtpdespesa, i.qtdeitensnf, i.valorunitarioitensnf, i.valortotalitensnf, i.codsittributaria
     , i.codoperfiscal, i.codclassfisc, v.datavenctonf, c.codcontactb, i.qtdeitensnf
     , m.descricaomat, m.codigointernomaterial, o.codserv
  From Bgm_Notafiscal n
     , est_itensnf i
     , Est_Cadmaterial m
     , Cpgtpdes_Ctbconta c
     , Esfopfis o
     , (Select Min(datavenctonf) datavenctonf, codintnf From EST_VENCTONF Group By CodintNf) v 
 Where i.codintnf = n.codintnf
   And n.codtpdoc = 'NFS'
   And n.dataemissaonf > '01-mar-2018'
   And n.tipooperacaonf = 'E' 
   And m.codigomatint = i.codigomatint 
   And V.Codintnf = n.codintnf
   And c.codtpdespesa = m.codtpdespesa   
   And n.lanctointegradoesf = 'N'
   And o.codoperfiscal = i.codoperfiscal
   And c.nroplano = 10;
   
-- cabeçalho
Select Distinct n.codintnf, n.numeronf, n.entradasaidanf, n.dataemissaonf, n.codigoforn, n.serienf, n.valortotalnf, n.aliqpisnf, n.vlrpisnf
     , n.aliqconfinsnf, n.vlrconfinsnf, n.aliqinssnf, n.vlrinssnf, n.aliqissnf, n.vlrissnf, n.aliqirnf, n.vlraliqnf ValorIR 
     , n.aliqcsllnf, n.vlrcsllnf, n.codclassfisc, v.datavenctonf, f.nrforn || ' - ' || f.rsocialforn Fornecedor, n.codigoempresa, n.codigofl
--     , n.*
  From Bgm_Notafiscal n
     , Bgm_Fornecedor F
     , (Select Min(datavenctonf) datavenctonf, codintnf From EST_VENCTONF Group By CodintNf) v
 Where n.codtpdoc = 'NFS' 
   And n.dataemissaonf Between To_Date('01/03/2018', 'dd/mm/yyyy') And
       To_Date('06/03/2018', 'dd/mm/yyyy')  
   And n.tipooperacaonf = 'E' 
   And V.Codintnf = n.codintnf 
   And n.lanctointegradoesf = 'N'
--   And Lpad(n.codigoempresa,3,'0') || '/' || lPad(n.codigofl,3,'0') 
   And f.codigoforn = n.codigoforn
--      And n.codintnf = 314048 
 