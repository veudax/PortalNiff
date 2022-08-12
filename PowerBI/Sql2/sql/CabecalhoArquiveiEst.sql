Select a.idarquivei, n.coddoctoesf codintnf, n.icmsvalorentra valoricmsitensnf, n.icmsaliqentra aliquotaicmsitensnf
     , n.icmssubstvalor valoricmssubsitensnf, n.ipivalorentra valorpisnfserv, 0 valordescontoitensnf, 0 valorseguroitensnf
     , 0 vlroutrasdespesasitnf, 0 valorfreteitensnf, n.vlcontabilentra valortotalitensnf, n.codsittributaria, n.codclassfisc
     , n.codoperfiscal_icmsentra, 0 qtdeitensnf
     
From esfentra N
   , Bgm_Fornecedor F
   , Niff_Fis_Arquivei a
   , Ctr_Empautorizadas e
   , Ctr_Filial l
   , Niff_Fis_Parametrosarquivei p 
Where n.codigoforn = f.codigoforn
  And n.chavedeacesso = a.chavedeacesso
  And e.codintempaut = l.codintempaut
  And l.Codigoempresa = n.codigoEmpresa
  And l.codigofl = n.codigofl
  And p.Idempresa = a.Idempresa
  And n.Coddoctoesf Not In (Select CodDoctoEsf From Esfnotafiscal ) 
  And n.Sistema <> 'EST'
;
Select a.idarquivei, n.coddoctoesf codintnf, n.icmsvalorentra valoricmsitensnf, n.icmsaliqentra aliquotaicmsitensnf
     , n.icmssubstvalor valoricmssubsitensnf, n.ipivalorentra valorpisnfserv, 0 valordescontoitensnf, 0 valorseguroitensnf
     , 0 vlroutrasdespesasitnf, 0 valorfreteitensnf, n.vlcontabilentra valortotalitensnf, n.codsittributaria, n.codclassfisc
     , n.codoperfiscal_icmsentra, 0 qtdeitensnf
     
From esfentra N
   , Bgm_Fornecedor F
   , Niff_Fis_Arquivei a
   , Ctr_Empautorizadas e
   , Ctr_Filial l
   , Niff_Fis_Parametrosarquivei p
   , Esfnotafiscal_Item ni 
   , Esfnotafiscal nn
Where n.codigoforn = f.codigoforn
  And n.chavedeacesso = a.chavedeacesso
  And e.codintempaut = l.codintempaut
  And l.Codigoempresa = n.codigoEmpresa
  And l.codigofl = n.codigofl
  And p.Idempresa = a.Idempresa
  And ni.codintNF = nn.Codintnf
  And nn.Coddoctoesf(+) = n.Coddoctoesf
--  And a.DataEmissao between '01-
  And n.Sistema <> 'EST'
