Select Class, Despesa, Data,
       CodDoctoCpg, CodigoEmpresa, CodigoFl,
       Valor_P, Valor_V,
       To_Char(Mes_passado, 'FM999G999G999D90', 'nls_numeric_characters='',.''') Mes_passado,
       To_Char(Projecao, 'FM999G999G999D90', 'nls_numeric_characters='',.''') Projecao
 From (       
(Select '01 - FOLHA DE PAGAMENTO' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                           
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4))
                            and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                              
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                             and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                           Union All
                         (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                            ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl 
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0)
Union All 
---- 
(Select '02 - MANUTENCAO DA FROTA' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                           
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                            and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)  
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                             and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                           Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)  ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0 )
Union All
--- 
(Select '03 - GESTAO DA FROTA' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                            and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                            and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                              
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                             and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                                             From CpgDocto d, cpgitdoc i
                                                                            Where d.codigoempresa = 4
                                                                              And d.codigofl = 1
                                                                              And d.coddoctocpg = i.coddoctocpg
                                                                              And i.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                                                                              And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0)
Union All
---- 
(Select '04 - DESPESAS C/ EQUIPAMENTOS E FERRAMENTAS' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                                
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)   ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0 )
Union All
----
(Select '05 - FROTA DE APOIO' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22401,22403,22404,22405,22406)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22401,22403,22404,22405,22406)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22401,22403,22404,22405,22406)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22401,22403,22404,22405,22406)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22401,22403,22404,22405,22406)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22401,22403,22404,22405,22406)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                            
                           Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (22401,22403,22404,22405,22406)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22401,22403,22404,22405,22406)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22401,22403,22404,22405,22406)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22401,22403,22404,22405,22406)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22401,22403,22404,22405,22406)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22401,22403,22404,22405,22406)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22401,22403,22404,22405,22406)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22401,22403,22404,22405,22406)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22401,22403,22404,22405,22406)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0 )
 ----
 Union All
(Select '06 - IMPOSTOS' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)    
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0)
---
 Union All
(Select '07 - JUROS E MULTAS' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (23401,23402)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (23401,23402)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (23401,23402)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                              
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (23401,23402)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (23401,23402)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (23401,23402)
                                                     And d.coddoctocpg_devol Is Not Null)
                           GROUP BY d.desctpdespesa) 
                           Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (23401,23402)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                           Union All
                         (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1  Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (23401,23402)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (23401,23402)
                                                     And d.coddoctocpg_devol Is Not Null)
                           GROUP BY d.desctpdespesa)) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (23401,23402)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (23401,23402)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (23401,23402)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (23401,23402)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (23401,23402)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (23401,23402)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0 )
---
 Union All
(Select '08 - DESPESAS ADMINISTRATIVAS' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                            
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                           Union All
                         (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                              
                           ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                          23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                          24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                          23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                          22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                          24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                          22132,24703,22307,21109,24616,24575)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                          23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                          24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                          23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                          22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                          24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                          22132,24703,22307,21109,24616,24575)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                                                      23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                                                      24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                                                      23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                                                      22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                                                      24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                                                      22132,24703,22307,21109,24616,24575)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0 )
---
 Union All
(Select '09 - DESPESAS JURIDICAS' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                              
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                           Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                            ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0 )
---   
 Union All
(Select '10 - ORGAO GESTOR' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (21103,21125,24217,24689)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21103,21125,24217,24689)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (21103,21125,24217,24689)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (21103,21125,24217,24689)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21103,21125,24217,24689)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (21103,21125,24217,24689)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                          
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (21103,21125,24217,24689)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (21103,21125,24217,24689)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (21103,21125,24217,24689)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                              ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (21103,21125,24217,24689)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (21103,21125,24217,24689)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (21103,21125,24217,24689)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (21103,21125,24217,24689)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (21103,21125,24217,24689)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (21103,21125,24217,24689)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0 )
---
 Union All
(Select '11 - INVESTIMENTOS' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0)
---
 Union All
(Select '12 - OUTRAS DESPESAS' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22128)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22128)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22128)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (22128)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22128)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22128)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (22128)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (22128)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (22128)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                               ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22128)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22128)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22128)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (22128)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (22128)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (22128)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0)
---
 Union All
(Select '13 - AUDITORIA' CLASS, Despesa, Sum(Mes_Passado) Mes_Passado, Sum(Projecao) Projecao, Data, CodDoctoCPG, CodigoEmpresa, CodigoFl, Sum(Valor_P) Valor_P, Sum(Valor_V) Valor_V
 From (
select  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MES_PASSADO,  
        PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d,
       (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
               (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
               movtoflp.despesa
          From (Select movtoflp.despesa,
                       sum(movtoflp.valor_m1) vlr_periodo_1,
                       sum(movtoflp.valor_m2) vlr_periodo_2,
                       sum(movtoflp.valor_m3) vlr_periodo_3
                  From ((select sum(iflp.valoritemdoc) Valor_m1,
                                0 Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa 
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (23201)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select sum(i.valoritemdoc)*-1 VALOr_m1, 
                                0 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.VencimentoCpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (23201)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (23201)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                             
                          Union All 
                        (select 0 Valor_m1,
                                sum(iflp.valoritemdoc) Valor_m2,
                                0 Valor_m3,
                                dflp.desctpdespesa Despesa                          
                           From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                          where cflp.coddoctocpg = iflp.coddoctocpg
                            and iflp.codtpdespesa = dflp.codtpdespesa
                            and cflp.codigoempresa = 4 
                            and cflp.codigofl = 1
                            and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and cflp.statusdoctocpg <> 'C' 
                            and cflp.codtpdoc not in ('AD','BOS')
                            and dflp.codtpdespesa in (23201)
                          group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                          Union All
                        (Select 0 VALOr_m1, 
                                sum(i.valoritemdoc)*-1 Valor_m2,
                                0 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (23201)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (23201)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                          
                          Union All 
                         (Select 0 Valor_m1,
                                 0 Valor_m2,
                                 sum(iflp.valoritemdoc) Valor_m3,
                                 dflp.desctpdespesa Despesa                          
                            From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                           where cflp.coddoctocpg = iflp.coddoctocpg
                             and iflp.codtpdespesa = dflp.codtpdespesa
                             and cflp.codigoempresa = 4 
                             and cflp.codigofl = 1
                             and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                             and cflp.statusdoctocpg <> 'C' 
                             and cflp.codtpdoc not in ('AD','BOS')
                             and dflp.codtpdespesa in (23201)
                           group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                          Union All
                        (Select 0 VALOr_m1, 
                                0 Valor_m2,
                                sum(i.valoritemdoc)*-1 Valor_m3,
                                d.desctpdespesa Despesa 
                           from  cpgdocto c, cpgitdoc i, cpgtpdes d
                          where c.coddoctocpg = i.coddoctocpg
                            And i.codtpdespesa = d.codtpdespesa
                            and c.codigoempresa = 4 and c.codigofl = 1
                            and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -5)) 
                                                      and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -4))
                            and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('BOS')
                            and i.codtpdespesa In (23201)
                            And c.coddoctocpg In (Select d.coddoctocpg_devol
                                                    From CpgDocto d, cpgitdoc i
                                                   Where d.codigoempresa = 4
                                                     And d.codigofl = 1
                                                     And d.coddoctocpg = i.coddoctocpg
                                                     And i.codtpdespesa In (23201)
                                                     And d.coddoctocpg_devol Is Not Null)
                            GROUP BY d.desctpdespesa)                              ) movtoflp
         group By movtoflp.despesa) movtoflp) MOVTOFLP
 where c.coddoctocpg = i.coddoctocpg
   and movtoflp.despesa = d.desctpdespesa
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (23201)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.PAGAMENTOCPG,c.coddoctocpg, c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.PAGAMENTOCPG DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        sum(i.valoritemdoc)*-1 VALOR_P, 
        0 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (23201)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (23201)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.PAGAMENTOCPG, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl
  Union All          -- vencimentos
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V
  from  cpgdocto c, cpgitdoc i, cpgtpdes d
 where c.coddoctocpg = i.coddoctocpg
   and i.codtpdespesa = d.codtpdespesa
   and c.codigoempresa = 4 and c.codigofl = 1
   and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
   and c.statusdoctocpg <> 'C' and c.codtpdoc not in ('AD','BOS')
   and d.codtpdespesa in (23201)
 Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, C.Vencimentocpg ,c.coddoctocpg, c.codigoempresa, c.codigofl
 Union All -- Traz os valores devolvidos ara subitrair
 Select D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        0 MES_PASSADO,  
        0 PROJECAO, 
        C.Vencimentocpg DATA, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl, 
        0 VALOR_P, 
        sum(i.valoritemdoc)*-1 VALOR_V 
   from cpgdocto c, cpgitdoc i, Cpgtpdes d
  where c.coddoctocpg = i.coddoctocpg
    And d.codtpdespesa = i.codtpdespesa
    and c.codigoempresa = 4 
    and c.codigofl = 1
    and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
    and c.statusdoctocpg <> 'C' 
    and c.codtpdoc not in ('BOS')
    and i.codtpdespesa in (23201)
    And c.coddoctocpg In (Select d.coddoctocpg_devol
                            From CpgDocto d, cpgitdoc i
                           Where d.codigoempresa = 4
                             And d.codigofl = 1
                             And d.coddoctocpg = i.coddoctocpg
                             And i.codtpdespesa in (23201)
                             And d.coddoctocpg_devol Is Not Null)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA,  
        C.Vencimentocpg, 
        c.coddoctocpg, 
        c.codigoempresa, 
        c.codigofl  ) x
 Group By Despesa, CodigoEmpresa, CodigoFl, CodDoctoCPG, Data        
 Having Sum(Valor_P) > 0 Or Sum(valor_V) > 0) )
  