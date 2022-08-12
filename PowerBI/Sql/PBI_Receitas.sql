CREATE OR REPLACE VIEW PBI_ARR_RECEITA AS
Select Empresa, EmpFil, Last_day(Data) Data,
       COD_TIPOPAGTO, NOM_DESCRICAO, --GRUPO,
       Sum(VLR_FATURAMENTO) VLR_FATURAMENTO,
       Sum(PASSAGEIROS) PASSAGEIROS
From (
(Select -- 2 ano do ano atual
  '001/001 - EOVG DUTRA' empresa,
  '001/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  --p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
  --pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 1 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO)--,  p.GRUPO )
 ---------------
Union all
 ---------------
(SELECT
  '001/002 - EOVG LAVRAS' empresa,
  '001/002' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  --p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 1 AND G.CODIGOFL = 2
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
  --p.GRUPO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '002/001 - ABC TRANSPORTES' empresa,
  '002/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  --p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 2 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
--  p.GRUPO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
/*(SELECT
  '003/001 - RAPIDO D OESTE' empresa,
  '003/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  13515.37 VLR_FATURAMENTO,
  5159 PASSAGEIROS
FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO P
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND '31-JAN-2015'
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND '31-jan-2015'
  AND G.COD_EMPRESA = 3 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
--------------------
union all*/
(SELECT
  '003/001 - RAPIDO D OESTE' empresa,
  '003/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
--  p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB) + Sum(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_SEGURO)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN '01-FEB-2015' AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 3 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
--  p.GRUPO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '004/001 - CISNE BRANCO' empresa,
  '004/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
--  p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 4 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
--  p.GRUPO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '006/001 - VIAÇÃO ARUJÁ' empresa,
  '006/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
--  p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 6 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
--  p.GRUPO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '009/001 - CAMPIBUS LTDA' empresa,
  '009/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
--  p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 9 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
--  p.GRUPO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '013/001 - RIBE TRANSPORTES' empresa,
  '013/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
--  p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 13 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
--  p.GRUPO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '026/001 - VUG DUTRA' empresa,
  '026/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
---  p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 26 AND G.CODIGOFL = 1
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
---  p.GRUPO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '026/003 - VUG BEBEDOURO' empresa,
  '026/003' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
--  p.GRUPO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
--  pbi_grupostipopagto_bi P,
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND trunc(sysdate)
  AND G.COD_EMPRESA = 26 AND G.CODIGOFL = 3
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  P.COD_TIPOPAGTO,
--  p.GRUPO,
  P.NOM_DESCRICAO )
)
Group By Empresa, EmpFil, Last_day(Data),
       COD_TIPOPAGTO, NOM_DESCRICAO--, GRUPO

