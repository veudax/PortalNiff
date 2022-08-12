CREATE OR REPLACE VIEW PBI_ARR_RECEITA_15GRAT AS
(SELECT
  '001/001 - EOVG DUTRA' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 1 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
Union all
 ---------------
(SELECT
  '001/002 - EOVG LAVRAS' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 1 AND G.CODIGOFL = 2
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '002/001 - ABC TRANSPORTES' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 2 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
-- AND P.COD_TIPOPAGTO NOT IN ('X','956')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '003/001 - RAPIDO D OESTE' empresa,
  g.dat_viagem_guia data,
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-JAN-2015'
  AND G.COD_EMPRESA = 3 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
--------------------
union all
(SELECT
  '003/001 - RAPIDO D OESTE' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-FEB-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 3 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '004/001 - CISNE BRANCO' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 4 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '006/001 - VIA��O ARUJ�' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 6 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '009/001 - CAMPIBUS LTDA' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 9 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '013/001 - RIBE TRANSPORTES' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 13 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '026/001 - VUG DUTRA' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 26 AND G.CODIGOFL = 1
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
 ---------------
union all
 ---------------
(SELECT
  '026/003 - VUG BEBEDOURO' empresa,
  g.dat_viagem_guia data,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
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
  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND '31-dec-2015'
  AND G.COD_EMPRESA = 26 AND G.CODIGOFL = 3
  AND P.COD_TIPOPAGTO NOT IN ('X','956')
  AND P.COD_TIPOPAGTO IN ('04','11','12','13','204','205','207','211','25','308','317','318','319','320','33','34','346','43','45','51','55','604','605','607','611','614','616','622','633','903','921','931','C')
GROUP BY
  g.dat_viagem_guia,
  P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO )
