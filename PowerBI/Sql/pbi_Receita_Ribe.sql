create or replace view pbi_arr_receita_ribe as
Select -- 2 ano do ano atual
  '013/001 - RIBE TRANSPORTES' empresa,
  '013/001' EmpFil,
  g.dat_prest_contas Data,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS,
  decode(d.valor, 0, 0, nvl(Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) / d.valor,2),0)) MediaPassageiros,
  decode(d.valor, 0, 0, nvl(Round(SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB) / d.valor,2),0)) MediaFaturamento

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
                          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12' -- considera sempre domingo
                                  , 0
                                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12' -- considera sempre domingo
                                          , 0
                                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)))))
          , decode(t.dataPonte, g.dat_prest_contas -- ponte de feriado considera no sabado
                  , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                  , 0)) FatSabado

  , Decode(To_char(g.dat_prest_contas,'D'), 7 -- é sabado?
          , decode(To_char(dat_prest_contas,'dd/mm'), '25/12' -- natal considera sempre domingo
                  , 0
                  , decode(To_char(dat_prest_contas,'dd/mm'), '01/01' -- ano novo considera sempre domingo
                          , 0
                          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12' -- considera sempre domingo
                                  , 0
                                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12' -- considera sempre domingo
                                          , 0
                                          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS)))))
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
          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12'
                  , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12'
                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                          , Decode(f.dataferiado, g.dat_prest_contas -- Feriado considera domingo
                                  , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
                                          , 0
                                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                                          )
                                  , 0)))) FatDomingo

  , Decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?
          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS)
          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12'
                  , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS)
                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12'
                          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS)
                          , Decode(f.dataferiado, g.dat_prest_contas -- feriado considera domingo
                                  , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
                                          , 0
                                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.QTD_PASSAG_TRANS)
                                          )
                                  , 0)))) PassDomingo

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
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO P,
  FINFERIA_EMPRESAFILIAL F,
  (select Distinct t.valor, t.data From pbi_radar_operacional t where grupo = 'Dias uteis' And t.empfil = '003/001') d,
  (select t.Data dataPonte, t.empfil EmpFilPonte From pbi_radar_operacional t where grupo = 'Ponte de Feriado' And t.empfil = '003/001') t
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  G.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  g.dat_prest_contas   BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND '31-dec-2018'
  AND G.COD_EMPRESA = 13 AND G.CODIGOFL = 1
  And Last_day(d.data(+)) = Last_Day(g.dat_prest_contas)
  And t.dataPonte(+) = g.dat_prest_contas
  And f.codigoempresa(+) = g.cod_empresa
  And f.codigofl(+) = g.codigoFl
  And f.dataferiado(+) = g.dat_prest_contas
GROUP BY
  g.dat_prest_contas,
  f.dataferiado,
  t.dataPonte,
  d.valor
Union All
Select Distinct  '013/001 - RIBE TRANSPORTES' empresa
     , '013/001' EmpFil
     , Data
     , 0 VLR_FATURAMENTO
     , 0 PASSAGEIROS
     , 0 MediaPassageiros
     , 0 MediaFaturamento
     , decode(To_char(Data,'D'), 7 -- é Sabado?
           , 1
           , 0 ) +
        decode(t.dataPonte, Data -- ponte considera sabado
              , 1
              , decode(To_char(Data,'dd/mm'), '24/12' --considera sempre domingo
                      , decode(To_char(Data,'D'), 7 -- é Sabado?
                              , -1
                              , 0)
                      , decode(To_char(Data,'dd/mm'), '25/12' -- natal considera sempre domingo
                              , decode(To_char(Data,'D'), 7 -- é Sabado?
                                      , -1
                                      , 0)
                              , decode(To_char(Data,'dd/mm'), '31/12' -- considera sempre domingo
                                      , decode(To_char(Data,'D'), 7 -- é Sabado?
                                              , -1
                                              , 0)
                                      , decode(To_char(Data,'dd/mm'), '01/01' -- ano novo considera sempre domingo
                                              , decode(To_char(Data,'D'), 7 -- é Sabado?
                                                      , -1
                                                      , 0)
                                              , 0)))))  QtdSAB
     , 0 FatSabado
     , 0 PassSabado
     ,  decode(To_char(Data,'D'), 1 -- é Domingo?
           , 1
           , 0) +
        Decode(f.dataferiado, Data -- feriado considera domingo
              , decode(To_char(Data,'D'), 7 -- é Sabado?
                      , 0
                      , decode(To_char(Data,'D'), 1 -- é Domingo?
                              , 0
                              , 1))
              , decode(To_char(Data,'dd/mm'), '24/12'
                      , decode(To_char(Data,'D'), 1 -- é Domingo?
                              , 0
                              , 1)
                      , decode(To_char(Data,'dd/mm'), '31/12'
                              , decode(To_char(Data,'D'), 1 -- é Domingo?
                                      , 0
                                      , 1)
                              , 0)))  QtdDOM
     , 0 FatDomingo
     , 0 PassDomingo
     ,  decode(To_char(Data,'D'), 1 -- é Domingo?
           , 0
           , Decode(To_char(data,'D'), 7
                   , 0
                   , decode(t.dataPonte, data
                           , 0
                           , Decode(f.dataferiado, data
                                   , 0
                                   , 1 ))))  QtdDu
     , 0 FatDU
     , 0 PassDU
  From Niff_Calendario,
      (select t.Data dataPonte, t.empfil EmpFilPonte
         From pbi_radar_operacional t
        where grupo = 'Ponte de Feriado'
          And empfil = '003/001') t,
       FINFERIA_EMPRESAFILIAL F
 Where Data Not In (Select g.dat_prest_contas
                     From t_Arr_Guia g
                    Where g.dat_prest_contas Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) And '31-dec-2018'
                      And Cod_empresa = 13
                      And CodigoFl = 1)
  And Data  Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) AND '31-dec-2018'
  And Data = f.dataferiado(+)

Union All

-- Nova tabela de feriados e emendas -- 2019 em diante
Select
    '013/001 - RIBE TRANSPORTES' empresa,
  '013/001' EmpFil,
  g.dat_prest_contas Data,
  SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)  VLR_FATURAMENTO,
  SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) PASSAGEIROS,
  decode(d.valor, 0, 0, nvl(Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) / d.valor,2),0)) MediaPassageiros,
  decode(d.valor, 0, 0, nvl(Round(SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB) / d.valor,2),0)) MediaFaturamento

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
                          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12' -- considera sempre domingo
                                  , 0
                                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12' -- considera sempre domingo
                                          , 0
                                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)))))
          , decode(t.dataPonte, g.dat_prest_contas -- ponte de feriado considera no sabado
                  , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                  , 0)) FatSabado

  , Decode(To_char(g.dat_prest_contas,'D'), 7 -- é sabado?
          , decode(To_char(dat_prest_contas,'dd/mm'), '25/12' -- natal considera sempre domingo
                  , 0
                  , decode(To_char(dat_prest_contas,'dd/mm'), '01/01' -- ano novo considera sempre domingo
                          , 0
                          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12' -- considera sempre domingo
                                  , 0
                                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12' -- considera sempre domingo
                                          , 0
                                          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS)))))
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
          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12'
                  , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12'
                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                          , Decode(f.dataferiado, g.dat_prest_contas -- Feriado considera domingo
                                  , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
                                          , 0
                                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.VLR_RECEB)
                                          )
                                  , 0)))) FatDomingo

  , Decode(To_char(g.dat_prest_contas,'D'), 1 -- é Domingo?
          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS)
          , decode(To_char(dat_prest_contas,'dd/mm'), '24/12'
                  , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS)
                  , decode(To_char(dat_prest_contas,'dd/mm'), '31/12'
                          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS)
                          , Decode(f.dataferiado, g.dat_prest_contas -- feriado considera domingo
                                  , decode(To_char(g.dat_prest_contas,'D'), 7 -- é Sabado?
                                          , 0
                                          , SUM(Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * A.QTD_PASSAG_TRANS)
                                          )
                                  , 0)))) PassDomingo

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
  T_ARR_DETALHE_GUIA A,
  T_TRF_TIPOPAGTO P,
    (select t.Data dataferiado, e.codigoglobus EmpFil
     From niff_ads_feriadosemendas t, Niff_Chm_Empresas e
    where tipo = 'F'
      And e.idEmpresa = 9
      And e.idempresa = t.IdEmpresa
      And t.Data >= '01-jan-2019') F,
  (select Distinct t.valor, t.data From pbi_radar_operacional t where grupo = 'Dias uteis' And t.empfil = '013/001') d,
  (select t.Data dataPonte, e.codigoglobus EmpFilPonte
     From niff_ads_feriadosemendas t, Niff_Chm_Empresas e
    where tipo = 'E'
      And e.idEmpresa = 9
      And e.idempresa = t.IdEmpresa
      And t.Data >= '01-jan-2019') t
WHERE
  P.COD_TIPOPAGTO     = A.COD_TIPOPAGTARIFA AND
  G.COD_SEQ_GUIA      = A.COD_SEQ_GUIA      AND
  g.dat_prest_contas   BETWEEN '01-jan-2019' AND trunc(sysdate)
  AND G.COD_EMPRESA = 13 AND G.CODIGOFL = 1
  And Last_day(d.data(+)) = Last_Day(g.dat_prest_contas)
  And t.dataPonte(+) = g.dat_prest_contas
  And f.EmpFil(+) = lPad(g.cod_empresa,3,'0') ||'/'||lPad(g.codigofl,3,'0')
  And f.dataferiado(+) = g.dat_prest_contas
GROUP BY
--  g.dat_viagem_guia,
  g.dat_prest_contas,
  f.dataferiado,
  t.dataPonte,
  d.valor

Union all

Select
    '013/001 - RIBE TRANSPORTES' empresa,
  '013/001' EmpFil
     , Data
     , 0 VLR_FATURAMENTO
     , 0 PASSAGEIROS
     , 0 MediaPassageiros
     , 0 MediaFaturamento
     , decode(To_char(Data,'D'), 7 -- é Sabado?
           , 1
           , 0 ) +
        decode(t.dataPonte, Data -- ponte considera sabado
              , 1
              , decode(To_char(Data,'dd/mm'), '24/12' --considera sempre domingo
                      , decode(To_char(Data,'D'), 7 -- é Sabado?
                              , -1
                              , 0)
                      , decode(To_char(Data,'dd/mm'), '25/12' -- natal considera sempre domingo
                              , decode(To_char(Data,'D'), 7 -- é Sabado?
                                      , -1
                                      , 0)
                              , decode(To_char(Data,'dd/mm'), '31/12' -- considera sempre domingo
                                      , decode(To_char(Data,'D'), 7 -- é Sabado?
                                              , -1
                                              , 0)
                                      , decode(To_char(Data,'dd/mm'), '01/01' -- ano novo considera sempre domingo
                                              , decode(To_char(Data,'D'), 7 -- é Sabado?
                                                      , -1
                                                      , 0)
                                              , 0)))))  QtdSAB
     , 0 FatSabado
     , 0 PassSabado
     ,  decode(To_char(Data,'D'), 1 -- é Domingo?
           , 1
           , 0) +
        Decode(f.dataferiado, Data -- feriado considera domingo
              , decode(To_char(Data,'D'), 7 -- é Sabado?
                      , 0
                      , decode(To_char(Data,'D'), 1 -- é Domingo?
                              , 0
                              , 1))
              , decode(To_char(Data,'dd/mm'), '24/12'
                      , decode(To_char(Data,'D'), 1 -- é Domingo?
                              , 0
                              , 1)
                      , decode(To_char(Data,'dd/mm'), '31/12'
                              , decode(To_char(Data,'D'), 1 -- é Domingo?
                                      , 0
                                      , 1)
                              , 0)))  QtdDOM
     , 0 FatDomingo
     , 0 PassDomingo
     ,  decode(To_char(Data,'D'), 1 -- é Domingo?
           , 0
           , Decode(To_char(data,'D'), 7
                   , 0
                   , decode(t.dataPonte, data
                           , 0
                           , Decode(f.dataferiado, data
                                   , 0
                                   , 1 ))))  QtdDu
     , 0 FatDU
     , 0 PassDU
  From Niff_Calendario,
     (select t.Data dataPonte, e.codigoglobus EmpFilPonte
     From niff_ads_feriadosemendas t, Niff_Chm_Empresas e
    where tipo = 'E'
      And e.idEmpresa = 9
      And e.idempresa = t.IdEmpresa
      And t.Data >= '01-jan-2019') t,
       (select t.Data dataferiado, e.codigoglobus EmpFil
     From niff_ads_feriadosemendas t, Niff_Chm_Empresas e
    where tipo = 'F'
      And e.idEmpresa = 9
      And e.idempresa = t.IdEmpresa
      And t.Data >= '01-jan-2019')  F
 Where Data Not In (Select g.dat_prest_contas
                     From t_Arr_Guia g
                    Where g.dat_prest_contas Between '01-jan-2019' AND trunc(sysdate)-1
                      And Cod_empresa = 13
                      And CodigoFl = 1)
  And Data  Between '01-jan-2019' AND trunc(Last_day(Sysdate))
  And Data = f.dataferiado(+)

