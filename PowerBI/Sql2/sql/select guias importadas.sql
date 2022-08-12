Select Distinct
  g.dat_viagem_guia data, 
  g.cod_guia guia,
  i.dat_importacao dataimportado, 
  i.nom_arqtexto arquivo
FROM
  T_ARR_GUIA G, t_Arr_Importacao_Bilheteria i,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO P
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
  G.DAT_VIAGEM_GUIA   BETWEEN '12-Aug-2017' AND trunc(sysdate)
  AND G.COD_EMPRESA = 4 AND G.CODIGOFL = 1
  And i.chave = g.cod_dvguia
Order By Data, guia, dataimportado