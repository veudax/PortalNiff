Select ie.codintnf, a.Idarquivei, round(ie.valortotalitensnf + ie.valoripiitensnf,2),
ia.valoricms ICMSArquivo, ie.valoricmsitensnf ICMSGlobus, ia.aliquotaicms AliquotaArquivo, ie.aliquotaicmsitensnf
     , ia.valoricmsst ICMSSTArquivo, ie.valoricmssubsitensnf, ia.valoripi IPIArquivo, ie.valoripiitensnf IPIGlobus
     , ia.desconto DescontoArquivo, ie.valordescontoitensnf DescontoGlobus, ia.seguro SeguroArquivo, ie.valorseguroitensnf SeguroGlobus
     , ia.outrasdespesas ODespesasArquivo, ie.vlroutrasdespesasitnf ODespesaGlobus, ia.valorfrete FreteArquivo, ie.valorfreteitensnf FreteGlobus
     , ia.valortotal TotalArquivo, ie.valortotalitensnf TotalGlobus
     , ia.cst, ia.csticms, ie.codsittributaria, ia.cfop, ie.codclassfisc, ia.cce
  From Bgm_Notafiscal n
     , Est_Itensnf ie
     , Niff_Fis_Arquivei a
     , Niff_Fis_Itensarquivei ia
     , Ctr_Empautorizadas          e
     , Ctr_Filial                  l
     , Niff_Fis_Parametrosarquivei p
 Where a.Idarquivei = ia.Idarquivei
   And n.codintnf = ie.codintnf
   And n.Chavedeacessonfe = a.Chavedeacesso
   And n.Codtpdoc = 'DAN'
   And e.Codintempaut = l.Codintempaut
   And l.Codigoempresa = n.Codigoempresa
   And l.Codigofl = n.Codigofl
   And p.Idempresa = a.Idempresa
--   And round(ie.valortotalitensnf + ie.valoripiitensnf,2) = ia.valortotal(+)
   And ia.Idarquivei = 2039
   And a.Dataemissao Between To_Date('01/05/2018', 'dd/mm/yyyy') And
       To_Date('05/05/2018', 'dd/mm/yyyy')      