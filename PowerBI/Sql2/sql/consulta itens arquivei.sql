Select a.Idarquivei,
       i.Codintnf,
       i.Valoricmsitensnf,
       i.Aliquotaicmsitensnf,
       i.Valoricmssubsitensnf,
       i.Valoripiitensnf,
       i.Valordescontoitensnf,
       i.Valorseguroitensnf,
       i.Vlroutrasdespesasitnf,
       i.Valorfreteitensnf,
       i.Valortotalitensnf,
       To_Char(i.Codsittributaria) Codsittributaria,
       To_Char(i.Codclassfisc) Codclassfisc,
       i.Codoperfiscal,
       i.Qtdeitensnf,
       'EST' Origem,
       Decode(n.Statusnf,
              'C',
              'Cancelada',
              'A',
              'Ativa',
              'F',
              'Fechada',
              'Inutilizada') Statusnf,
       i.Vlrconfinsitnf,
       i.Vlrpisitnf,
       i.Vlrissitnf,
       m.Descricaomat Descricao
  From Niff_Fis_Arquivei a,
       Bgm_Notafiscal    n,
       Est_Itensnf       i,
       Niff_Chm_Empresas Em,
       Est_Cadmaterial   m
 Where n.Chavedeacessonfe = a.Chavedeacesso
   And m.Codigomatint = i.Codigomatint
   And a.Dataemissao Between To_Date('01/11/2019', 'dd/mm/yyyy') And
       To_Date('08/11/2019', 'dd/mm/yyyy')
   And a.Idempresa = 5
   And n.Codintnf = i.Codintnf
   And a.Idempresa = Em.Idempresa
   And Lpad(n.Codigoempresa, 3, '0') || '/' || Lpad(n.Codigofl, 3, '0') =
       Em.Codigoglobus
   And a.idarquivei = 59908
Union All
Select a.Idarquivei,
       e.Codintnf,
       e.Valoricmsmatavulso Valoricmsitensnf,
       e.Aliqicmsmatavulso Aliquotaicmsitensnf,
       e.Valoricmssubstituicao Valoricmssubsitensnf,
       e.Valoripi Valoripiitensnf,
       e.Vlrdescontonfserv Valordescontoitensnf,
       e.Valorseguro Valorseguroitensnf,
       e.Vlroutrasdespnfserv Vlroutrasdespesasitnf,
       e.Valorfrete Valorfreteitensnf,
       e.Valornfservico Valortotalitensnf,
       To_Char(e.Codsittributaria) Codsittributaria,
       To_Char(e.Codclassfisc) Codclassfisc,
       e.Codoperfiscal,
       e.Qtdenfservico Qtdeitensnf,
       'EST' Origem,
       Decode(n.Statusnf,
              'C',
              'Cancelada',
              'A',
              'Ativa',
              'F',
              'Fechada',
              'Inutilizada') Statusnf,
       e.Valorcofinsnfserv Vlrconfinsitnf,
       e.Valorpisnfserv Vlrpisitnf,
       e.Valorissnfserv Vlrissitnf,
       e.Descricaonfservico Descricao
  From Niff_Fis_Arquivei a,
       Bgm_Notafiscal    n,
       Est_Nfservico     e,
       Niff_Chm_Empresas Em
 Where n.Chavedeacessonfe = a.Chavedeacesso
   And a.Dataemissao Between To_Date('01/11/2019', 'dd/mm/yyyy') And
       To_Date('08/11/2019', 'dd/mm/yyyy')
   And a.Idempresa = 5
   And n.Codintnf = e.Codintnf
   And a.Idempresa = Em.Idempresa
   And Lpad(n.Codigoempresa, 3, '0') || '/' || Lpad(n.Codigofl, 3, '0') =
       Em.Codigoglobus
   And a.idarquivei = 59908       
Union All
Select a.Idarquivei,
       n.Coddoctoesf Codintnf,
       i.Valoricms Valoricmsitensnf,
       i.Aliqicms Aliquotaicmsitensnf,
       i.Valoricmssubst Valoricmssubsitensnf,
       i.Valoripi Valorpisnfserv,
       i.Vlr_Prop_Desconto Valordescontoitensnf,
       i.Vlr_Prop_Seguro Valorseguroitensnf,
       i.Vlr_Prop_Outras Vlroutrasdespesasitnf,
       i.Vlr_Prop_Frete Valorfreteitensnf,
       i.Vlrtotal Valortotalitensnf,
       i.Sittributaria Codsittributaria,
       i.Cfop,
       i.Codoperfiscal Codoperfiscal,
       i.Qtde Qtdeitensnf,
       'ESF' Origem,
       Decode(n.Statusnf, 'C', 'Cancelada', 'Ativa') Statusnf,
       Vl_Cofins Vlrconfinsitnf,
       Vl_Pis Vlrpisitnf,
       0 Vlrissitnf,
       i.Descricao
  From Niff_Fis_Arquivei           a,
       Esfnotafiscal               n,
       Esfnotafiscal_Item          i,
       Bgm_Fornecedor              f,
       Ctr_Empautorizadas          e,
       Ctr_Filial                  l,
       Niff_Fis_Parametrosarquivei p,
       Niff_Chm_Empresas           Em
 Where n.Codigoforn = f.Codigoforn
   And n.Chavedeacesso = a.Chavedeacesso
   And e.Codintempaut = l.Codintempaut
   And l.Codigoempresa = n.Codigoempresa
   And l.Codigofl = n.Codigofl
   And p.Idempresa = a.Idempresa
   And i.Codintnf = n.Codintnf
   And a.Dataemissao Between To_Date('01/11/2019', 'dd/mm/yyyy') And
       To_Date('08/11/2019', 'dd/mm/yyyy')
   And a.Idempresa = 5
   And a.Idempresa = Em.Idempresa
   And Lpad(n.Codigoempresa, 3, '0') || '/' || Lpad(n.Codigofl, 3, '0') =
       Em.Codigoglobus
   And a.idarquivei = 59908       