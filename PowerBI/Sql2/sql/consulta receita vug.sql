
SELECT
  lpad(g.COD_EMPRESA,3,'0') || '/' || lpad(g.codigofl,3,'0') EmpFil,
  g.dat_prest_contas , t.dataPonte, f.dataferiado, c.Data,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS
        
    -- valores sabado
  , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
           , 1
           , 0 ) +
        decode(t.dataPonte, g.dat_prest_contas -- ponte considera sabado
              , 1
              , decode(To_char(dat_prest_contas,'dd/mm'), '24/12' --considera sempre domingo
                      , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
                              , -1
                              , 0)
                      , decode(To_char(dat_prest_contas,'dd/mm'), '25/12' -- natal considera sempre domingo
                              , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
                                      , -1
                                      , 0)
                              , decode(To_char(dat_prest_contas,'dd/mm'), '31/12' -- considera sempre domingo
                                      , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
                                              , -1
                                              , 0) 
                                      , decode(To_char(dat_prest_contas,'dd/mm'), '01/01' -- ano novo considera sempre domingo
                                              , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
                                                      , -1
                                                      , 0)
                                              , 0)))))  QtdSAB

  , Decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
          , decode(To_char(dat_prest_contas,'dd/mm'), '25/12' -- natal considera sempre domingo
                  , 0
                  , decode(To_char(dat_prest_contas,'dd/mm'), '01/01' -- ano novo considera sempre domingo
                          , 0          
                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB))) 
          , decode(t.dataPonte, g.dat_prest_contas -- ponte de feriado considera no sabado
                  , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                  , 0)) FatSabado
  
  , Decode(To_char(g.dat_prest_contas,'D'), 7 -- é sabado?
          , decode(To_char(dat_prest_contas,'dd/mm'), '25/12' -- natal considera sempre domingo
                  , 0
                  , decode(To_char(dat_prest_contas,'dd/mm'), '01/01' -- ano novo considera sempre domingo
                          , 0  
                          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS))) 
          , decode(t.dataPonte, g.dat_prest_contas -- ponte de feriado considera no sabado
                  , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) 
                  , 0)) PassSabado
    
    -- valores domingo
  ,  decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?
           , 1
           , 0) +
        Decode(f.dataferiado, g.dat_prest_contas -- feriado considera domingo
              , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?                              
                      , 0
                      , decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?
                              , 0
                              , 1))
              , decode(To_char(dat_prest_contas,'dd/mm'), '24/12'
                      , decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?
                              , 0
                              , 1)
                      , decode(To_char(dat_prest_contas,'dd/mm'), '31/12'
                              , decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?                              
                                      , 0
                                      , 1)
                              , 0)))  QtdDOM
    
  , Decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?
          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB) 
          , Decode(To_char(g.dat_prest_contas,'D'), 7 -- Sabado não traz valor
                  , 0
                  , Decode(f.dataferiado, g.dat_prest_contas -- Feriado considera domingo
                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12'
                                  , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12'
                                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                                          , 0))))) FatDomingo

  , Decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?
          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) 
          , Decode(To_char(g.dat_prest_contas,'D'), 7 -- sabado não traz valor
                  , 0
                  , Decode(f.dataferiado, g.dat_prest_contas -- feriado considera domingo
                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.QTD_PASSAG_TRANS)
                          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12'
                                  , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.QTD_PASSAG_TRANS)
                                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12'
                                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.QTD_PASSAG_TRANS)
                                          , 0))))) PassDomingo
                  
    -- valores dias uteis
  ,  decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?
           , 0
           , Decode(To_char(g.dat_prest_contas,'D'), 7
                   , 0
                   , decode(t.dataPonte, g.dat_prest_contas
                           , 0
                           , Decode(f.dataferiado, g.dat_prest_contas
                                   , 0
                                   , 1 ))))  QtdDu
   
  , Decode(To_char(g.dat_prest_contas,'D'), 7 -- sabado não traz valor
          , 0
          , Decode(To_char(g.dat_prest_contas,'D'), 1 -- domingo não traz valor
                  , 0
                  , Decode(f.dataferiado, g.dat_prest_contas -- feriado não traz valor
                          , 0
                          , decode(t.dataPonte, g.dat_prest_contas -- ponte de feriado não traz valor
                                  , 0
                                  ,  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB))))) FatDU

  , Decode(To_char(g.dat_prest_contas,'D'), 7 -- sabado não traz valor
          , 0
          , Decode(To_char(g.dat_prest_contas,'D'), 1 -- domingo não traz valor
                  , 0
                  , Decode(f.dataferiado, g.dat_prest_contas -- feriado não traz valor
                          , 0 
                          , decode(t.dataPonte, g.dat_prest_contas -- ponte de feriado não traz valor 
                                  , 0
                                  , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS))))) PassDU

FROM
  T_ARR_GUIA G,
  T_ARR_VIAGENS_GUIA V,
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO P,
  FINFERIA_EMPRESAFILIAL F,
  dim_ctr_periodo c,
  (select t.Data dataPonte, t.empfil EmpFilPonte
     From pbi_radar_operacional t 
    where grupo = 'Ponte de Feriado') t
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  V.COD_SEQ_VIAGEM    = A.COD_SEQ_VIAGEM    AND
  V.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  G.COD_SEQ_GUIA      = V.COD_SEQ_GUIA      AND
  --g.dat_prest_contas   BETWEEN '01-apr-2017' And '30-apr-2017'
  c.Data BETWEEN '01-apr-2017' And '30-apr-2017'
  AND G.COD_EMPRESA || G.CODIGOFL In (11)--(11,12,21,31,41,51,61,91,131,261,263)
  And t.dataPonte(+) = g.dat_prest_contas
  And f.codigoempresa(+) = g.cod_empresa
  And f.codigofl(+) = g.codigoFl
  And f.dataferiado(+) = g.dat_prest_contas
  And c.data(+) = g.Dat_Prest_Contas
  And EmpFilPonte(+) = lpad(g.COD_EMPRESA,3,'0') || '/' || lpad(g.codigofl,3,'0')
GROUP BY
  g.dat_prest_contas,
  f.dataferiado,
  t.dataPonte, c.Data,
  lpad(g.COD_EMPRESA,3,'0') || '/' || lpad(g.codigofl,3,'0')
Order By empFil, Data