(Select -- 2 ano do ano atual
  '001/001 - EOVG DUTRA' empresa,
  '001/001' EmpFil,
--  g.dat_viagem_guia data,
  g.dat_prest_contas Data
  , (Select Count(*) From Dim_Ctr_Periodo 
      Where To_char(Data,'D') Not In (1,7)
        And Data Between ADD_MONTHS(Last_day(g.dat_prest_contas),-1)+1 And Last_day(g.dat_prest_contas)) QtdDu
  , (Select Count(*) From Dim_Ctr_Periodo 
      Where To_char(Data,'D') = (7)
        And Data Between ADD_MONTHS(Last_day(g.dat_prest_contas),-1)+1 And Last_day(g.dat_prest_contas)) QtdSAB
  , (Select Count(*) From Dim_Ctr_Periodo 
      Where To_char(Data,'D') = (1)
        And Data Between ADD_MONTHS(Last_day(g.dat_prest_contas),-1)+1 And Last_day(g.dat_prest_contas) +  
     Decode(f.dataferiado, g.dat_prest_contas, 1, 0)) QtdDOM
        
  , Decode(To_char(g.dat_prest_contas,'D'), 7, SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB) , 0) FatSabado
  , Decode(To_char(g.dat_prest_contas,'D'), 1, SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB) , 0) FatDomingo  
  , Decode(To_char(g.dat_prest_contas,'D'), 7, 0,
    Decode(To_char(g.dat_prest_contas,'D'), 1, 0,  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB))) FatDU

  ,P.COD_TIPOPAGTO,
  P.NOM_DESCRICAO,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS,
  nvl(Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) / d.valor,2),0) MediaPassageiros,
  nvl(Round(SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB) / d.valor,2),0) MediaFaturamento
FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO P,
  FINFERIA_EMPRESAFILIAL F,
  (select t.valor, t.data From pbi_radar_operacional t where grupo = 'Dias uteis' And t.empfil = '001/001') d
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
--  G.DAT_VIAGEM_GUIA   BETWEEN '01-JAN-2015' AND trunc(sysdate)
  g.dat_prest_contas   BETWEEN '01-dec-2017' And trunc(Sysdate)
--  ADD_MONTHS(Trunc(Sysdate,'rr'), -24) AND trunc(sysdate)
  AND G.COD_EMPRESA = 1 AND G.CODIGOFL = 1
  And Last_day(d.data(+)) = Last_Day(g.dat_prest_contas)
  And f.codigoempresa(+) = g.cod_empresa
  And f.codigofl(+) = g.codigoFl
  And f.dataferiado(+) = g.dat_prest_contas
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  f.dataferiado, 
  P.COD_TIPOPAGTO,
  d.valor,
  P.NOM_DESCRICAO )