create or replace view pbi_dreintegracoes as
Select
  Last_day(g.dat_prest_contas) Data,
  lpad(g.cod_empresa,3,'0') || '/' || lPad(g.codigofl,3,'0') EmpFil,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO p
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -12) AND trunc(sysdate)

  And upper(p.nom_descricao) Like '%INT%'
  And A.VLR_RECEB = 0

GROUP BY
  P.NOM_DESCRICAO,
  lpad(g.cod_empresa,3,'0') || '/' || lPad(g.codigofl,3,'0'),
  Last_day(g.dat_prest_contas)

