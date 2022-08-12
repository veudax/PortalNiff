Create Or Replace View PBI_FluxoDeCaixa As
Select Class, despesa, 
       trunc(Mes_passado,2) mes_Passado, 
       trunc(Projecao,2) Projecao, 
       Data, 0 CodDoctoCPG,
       empfil, 
       Trunc(valor_p,2) valor_P, 
       trunc(valor_V,2) Valor_V,
       trunc(ProximoMes) ProximoMes
   From (
 select '01 - FOLHA DE PAGAMENTO' CLASS, 
        D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO MES_PASSADO,  
        MOVTOFLP.PROJECAO PROJECAO, 
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data,          
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('BOS')
                       and dflp.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('BOS')
                       and dflp.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('BOS')
                       and dflp.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                     Group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)) movtoflp
            Group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    Where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('BOS')
      and d.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
  GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  Union All 
 select '01 - FOLHA DE PAGAMENTO' CLASS, 
        D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO,  
        MOVTOFLP.PROJECAO, 
        To_char(C.VENCIMENTOCPG,'mm/yyyy') DATA,          
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        movtoflp.ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa, 
         codigoempresa, codigofl, vlr_proximoMes ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  Sum(movtoflp.Valor_M4) vlr_ProximoMes
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('BOS')
                       and dflp.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('BOS')
                       and dflp.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                    (select 0 Valor_m1,
                            0 Valor_m2,
                            sum(iflp.valoritemdoc) Valor_m3,
                            0 Valor_m4,
                            dflp.desctpdespesa Despesa,
                            cflp.codigoempresa, cflp.codigofl                          
                       From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                      where cflp.coddoctocpg = iflp.coddoctocpg
                        and iflp.codtpdespesa = dflp.codtpdespesa
                        and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                        and cflp.statusdoctocpg <> 'C' 
                        and cflp.codtpdoc not in ('BOS')
                        and dflp.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                      group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                      Union All 
                    (select 0 Valor_m1,
                            0 Valor_m2,
                            0 Valor_m3,
                            sum(iflp.valoritemdoc) Valor_m4,
                            dflp.desctpdespesa Despesa,
                            cflp.codigoempresa, cflp.codigofl                          
                       From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                      where cflp.coddoctocpg = iflp.coddoctocpg
                        and iflp.codtpdespesa = dflp.codtpdespesa
                        and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                        and cflp.statusdoctocpg <> 'C' 
                        and cflp.codtpdoc not in ('BOS')
                        and dflp.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                      group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
            group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('BOS')
      and d.codtpdespesa IN (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, MovtoFLP.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl 
  Union All 
 select '02 - MANUTENCAO DA FROTA' CLASS, 
        D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO, 
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa, codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
            group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
    Union All 
   select '02 - MANUTENCAO DA FROTA' CLASS, 
          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
          MOVTOFLP.MES_PASSADO,
          MOVTOFLP.PROJECAO, 
          To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
           
          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
          0 VALOR_P, 
          sum(i.valoritemdoc) VALOR_V,
          MovtoFlp.ProximoMes
     from cpgdocto c, cpgitdoc i, cpgtpdes d,
    (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
            (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
            movtoflp.despesa,
            codigoempresa, codigofl,
            vlr_ProximoMes ProximoMes
       From (Select movtoflp.despesa, codigoempresa, codigofl,
                    sum(movtoflp.valor_m1) vlr_periodo_1,
                    sum(movtoflp.valor_m2) vlr_periodo_2,
                    sum(movtoflp.valor_m3) vlr_periodo_3,
                    Sum(Movtoflp.Valor_m4) vlr_ProximoMes
               From ((select sum(iflp.valoritemdoc) Valor_m1,
                             0 Valor_m2,
                             0 Valor_m3,
                             0 Valor_m4,
                             dflp.desctpdespesa Despesa,
                             cflp.codigoempresa, cflp.codigofl
                        From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                       where cflp.coddoctocpg = iflp.coddoctocpg
                         and iflp.codtpdespesa = dflp.codtpdespesa
                         and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                         and cflp.statusdoctocpg <> 'C' 
                         and cflp.codtpdoc not in ('AD','BOS')
                         and dflp.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                       group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                       Union All 
                     (select 0 Valor_m1,
                             sum(iflp.valoritemdoc) Valor_m2,
                             0 Valor_m3,
                             0 Valor_m4,
                             dflp.desctpdespesa Despesa,
                             cflp.codigoempresa, cflp.codigofl                          
                        From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                       where cflp.coddoctocpg = iflp.coddoctocpg
                         and iflp.codtpdespesa = dflp.codtpdespesa
                         and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                         and cflp.statusdoctocpg <> 'C' 
                         and cflp.codtpdoc not in ('AD','BOS')
                         and dflp.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                       group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                       Union All 
                     (select 0 Valor_m1,
                             0 Valor_m2,
                             sum(iflp.valoritemdoc) Valor_m3,
                             0 Valor_m4,
                             dflp.desctpdespesa Despesa,
                             cflp.codigoempresa, cflp.codigofl
                        From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                       where cflp.coddoctocpg = iflp.coddoctocpg
                         and iflp.codtpdespesa = dflp.codtpdespesa
                         and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                         and cflp.statusdoctocpg <> 'C' 
                         and cflp.codtpdoc not in ('AD','BOS')
                         and dflp.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                       group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                       Union All
                     (select 0 Valor_m1,
                             0 Valor_m2,
                             sum(iflp.valoritemdoc) Valor_m3,
                             0 Valor_m4,
                             dflp.desctpdespesa Despesa,
                             cflp.codigoempresa, cflp.codigofl
                        From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                       where cflp.coddoctocpg = iflp.coddoctocpg
                         and iflp.codtpdespesa = dflp.codtpdespesa
                         and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                         and cflp.statusdoctocpg <> 'C' 
                         and cflp.codtpdoc not in ('AD','BOS')
                         and dflp.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                       group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp                       
            group By movtoflp.despesa, codigoempresa, codigofl ) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, Movtoflp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl 
  Union All 
 select '03 - GESTAO DA FROTA' CLASS, 
        D.CODTPDESPESA ||' - ' || D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
    Union All 
    select '03 - GESTAO DA FROTA' CLASS, 
           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
           MOVTOFLP.MES_PASSADO, 
           MOVTOFLP.PROJECAO,
           To_char(C.VENCIMENTOCPG,'mm/yyyy') DATA, 
            
           lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
           0 VALOR_P, 
           sum(i.valoritemdoc) VALOR_V,
           Sum(MovtoFlp.ProximoMes) ProximoMes
      From cpgdocto c, cpgitdoc i, cpgtpdes d,
    (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
           (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
           movtoflp.despesa, codigoempresa, codigofl,
           vlr_ProximoMes  ProximoMEs         
       From (Select movtoflp.despesa, codigoempresa, codigofl,
                    sum(movtoflp.valor_m1) vlr_periodo_1,
                    sum(movtoflp.valor_m2) vlr_periodo_2,
                    sum(movtoflp.valor_m3) vlr_periodo_3,
                    Sum(Movtoflp.Valor_m4) vlr_ProximoMes
               From ((select sum(iflp.valoritemdoc) Valor_m1,
                             0 Valor_m2,
                             0 Valor_m3,
                             0 Valor_m4,
                             dflp.desctpdespesa Despesa,
                             cflp.codigoempresa, cflp.codigofl 
                        From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                       where cflp.coddoctocpg = iflp.coddoctocpg
                         and iflp.codtpdespesa = dflp.codtpdespesa
                         and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                         and cflp.statusdoctocpg <> 'C' 
                         and cflp.codtpdoc not in ('AD','BOS')
                         and dflp.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                       group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                       Union All 
                     (select 0 Valor_m1,
                             sum(iflp.valoritemdoc) Valor_m2,
                             0 Valor_m3,
                             0 Valor_m4,
                             dflp.desctpdespesa Despesa,
                             cflp.codigoempresa, cflp.codigofl                          
                        From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                       where cflp.coddoctocpg = iflp.coddoctocpg
                         and iflp.codtpdespesa = dflp.codtpdespesa
                         and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                         and cflp.statusdoctocpg <> 'C' 
                         and cflp.codtpdoc not in ('AD','BOS')
                         and dflp.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                       group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                       Union All 
                     (select 0 Valor_m1,
                             0 Valor_m2,
                             sum(iflp.valoritemdoc) Valor_m3,
                             0 Valor_m4,
                             dflp.desctpdespesa Despesa,
                             cflp.codigoempresa, cflp.codigofl                          
                        From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                       where cflp.coddoctocpg = iflp.coddoctocpg
                         and iflp.codtpdespesa = dflp.codtpdespesa
                         and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                         and cflp.statusdoctocpg <> 'C' 
                         and cflp.codtpdoc not in ('AD','BOS')
                         and dflp.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                       group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                       Union All 
                     (select 0 Valor_m1,
                             0 Valor_m2,
                             0 Valor_m3,
                             sum(iflp.valoritemdoc) Valor_m4,
                             dflp.desctpdespesa Despesa,
                             cflp.codigoempresa, cflp.codigofl                          
                        From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                       where cflp.coddoctocpg = iflp.coddoctocpg
                         and iflp.codtpdespesa = dflp.codtpdespesa
                         and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                         and cflp.statusdoctocpg <> 'C' 
                         and cflp.codtpdoc not in ('AD','BOS')
                         and dflp.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                       group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoFlp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl 
  Union All 
 select '04 - DESPESAS C/ EQUIPAMENTOS E FERRAMENTAS' CLASS, 
        D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO, 
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                            0 Valor_m2,
                            0 Valor_m3,
                            dflp.desctpdespesa Despesa,
                            cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '04 - DESPESAS C/ EQUIPAMENTOS E FERRAMENTAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        Sum(ProximoMes) ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa,
         codigoempresa, codigofl, vlr_proximoMes ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) vlr_proximoMes
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                    (select 0 Valor_m1,
                            0 Valor_m2,
                            sum(iflp.valoritemdoc) Valor_m3,
                            0 Valor_m4,
                            dflp.desctpdespesa Despesa,
                            cflp.codigoempresa, cflp.codigofl                         
                       From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                      where cflp.coddoctocpg = iflp.coddoctocpg
                        and iflp.codtpdespesa = dflp.codtpdespesa
                        and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                        and cflp.statusdoctocpg <> 'C' 
                        and cflp.codtpdoc not in ('AD','BOS')
                        and dflp.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
                      Group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     Union All 
                    (select 0 Valor_m1,
                            0 Valor_m2,
                            0 Valor_m3,
                            sum(iflp.valoritemdoc) Valor_m4,
                            dflp.desctpdespesa Despesa,
                            cflp.codigoempresa, cflp.codigofl                         
                       From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                      where cflp.coddoctocpg = iflp.coddoctocpg
                        and iflp.codtpdespesa = dflp.codtpdespesa
                        and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                        and cflp.statusdoctocpg <> 'C' 
                        and cflp.codtpdoc not in ('AD','BOS')
                        and dflp.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
                      Group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl ) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and   d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22211,24122,24608,24627,24628,54580,54581)
    Group BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, MovtoFLP.ProximoMEs, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl 
  Union All 
 select '05 - FROTA DE APOIO' CLASS,
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V, 
        ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl, Vlr_proximoMes ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) vlr_proximoMes                  
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22401,22403,22404,22405,22406)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22401,22403,22404,22405,22406)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                      
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22401,22403,22404,22405,22406)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     Union All
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                      
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22401,22403,22404,22405,22406)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp                     
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22401,22403,22404,22405,22406)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, movtoflp.ProximoMes, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  Union All
 select '05 - FROTA DE APOIO' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        0 ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa,
         codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22401,22403,22404,22405,22406)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22401,22403,22404,22405,22406)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22401,22403,22404,22405,22406)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22401,22403,22404,22405,22406)
    Group BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl
  Union All 
  select '06 - IMPOSTOS' CLASS, 
          D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
          MOVTOFLP.MES_PASSADO, 
          MOVTOFLP.PROJECAO,
          To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
           
          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
          sum(i.valoritemdoc) VALOR_P, 
          0 VALOR_V, 
          0 ProximoMes
    from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa,
         codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                    group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                    Union All 
                  (select 0 Valor_m1,
                          0 Valor_m2,
                          sum(iflp.valoritemdoc) Valor_m3,
                          dflp.desctpdespesa Despesa,
                          cflp.codigoempresa, cflp.codigofl                                                    
                     FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                    where cflp.coddoctocpg = iflp.coddoctocpg
                      and iflp.codtpdespesa = dflp.codtpdespesa
                      and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                      and cflp.statusdoctocpg <> 'C' 
                      and cflp.codtpdoc not in ('AD','BOS')
                      and dflp.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                    group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '06 - IMPOSTOS' CLASS, 
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA, 
         MOVTOFLP.MES_PASSADO, 
         MOVTOFLP.PROJECAO,
         To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
          
         lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
         0 VALOR_P, 
         sum(i.valoritemdoc) VALOR_V,
         ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl, vlr_ProximoMEs ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) vlr_proximoMes                  
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311, 24714,24715,24720)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311, 24714,24715,24720)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311, 24714,24715,24720)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311, 24714,24715,24720)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311, 24714,24715,24720)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoFlp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl
  Union All 
 select '07 - JUROS E MULTAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa, 
          codigoempresa, codigofl, Vlr_proximoMes ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) vlr_proximoMes
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23401,23402)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23401,23402)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,                          
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23401,23402)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,                          
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23401,23402)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (23401,23402)
    Group By  D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, movtoFlp.ProximoMes, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
  select '07 - JUROS E MULTAS' CLASS, 
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
         MOVTOFLP.MES_PASSADO, 
         MOVTOFLP.PROJECAO,
         To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
          
         lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,    
         0 VALOR_P, 
         sum(i.valoritemdoc) VALOR_V,
         0 ProximoMes
    From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23401,23402)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23401,23402)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23401,23402)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (23401,23402)
    Group BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl  
  Union All 
 Select '08 - DESPESAS ADMINISTRATIVAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO,
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           codigoempresa, codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                      and iflp.codtpdespesa = dflp.codtpdespesa
                      and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                      and cflp.statusdoctocpg <> 'C' 
                      and cflp.codtpdoc not in ('AD','BOS')
                      and dflp.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                    group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                    Union All 
                  (select 0 Valor_m1,
                          sum(iflp.valoritemdoc) Valor_m2,
                          0 Valor_m3,
                          dflp.desctpdespesa Despesa,
                          cflp.codigoempresa, cflp.codigofl                          
                     From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                    where cflp.coddoctocpg = iflp.coddoctocpg
                      and iflp.codtpdespesa = dflp.codtpdespesa
                      and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                      and cflp.statusdoctocpg <> 'C' 
                      and cflp.codtpdoc not in ('AD','BOS')
                      and dflp.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                    group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                    UNION All 
                  (select 0 Valor_m1,
                          0 Valor_m2,
                          sum(iflp.valoritemdoc) Valor_m3,
                          dflp.desctpdespesa Despesa,
                          cflp.codigoempresa, cflp.codigofl                          
                     From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                    where cflp.coddoctocpg = iflp.coddoctocpg
                      and iflp.codtpdespesa = dflp.codtpdespesa
                      and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                      and cflp.statusdoctocpg <> 'C' 
                      and cflp.codtpdoc not in ('AD','BOS')
                      and dflp.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575, 23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                    group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,
23132,24616,24633,24638,24654,24660,23115,23125,24575, 23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
    Group By D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '08 - DESPESAS ADMINISTRATIVAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        proximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa,
         codigoempresa, codigofl, ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) Proximomes
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575, 23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                        
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575, 23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     Union All 
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                        
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575, 23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp                     
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,
23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoFlp.ProximoMEs, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl  
  Union All 
 select '09 - DESPESAS JURIDICAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa, 
         codigoempresa, codigofl, ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMes
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     Union All 
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                      and iflp.codtpdespesa = dflp.codtpdespesa
                      and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                      and cflp.statusdoctocpg <> 'C' and cflp.codtpdoc not in ('AD','BOS')
                      and dflp.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
                    group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP 
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '09 - DESPESAS JURIDICAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa, codigoempresa, codigofl, ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMEs
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                    UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                      and iflp.codtpdespesa = dflp.codtpdespesa
                      and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                      and cflp.statusdoctocpg <> 'C' and cflp.codtpdoc not in ('AD','BOS')
                      and dflp.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
                    group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (814,22111,22125,23203,24212,24219,24509,23317)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoflp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl
  UNION ALL  
 select '10 - ORGAO GESTOR' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa,
         codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21103,21125,24217,24689)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21103,21125,24217,24689)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21103,21125,24217,24689)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (21103,21125,24217,24689)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
  select '10 - ORGAO GESTOR' CLASS, 
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
         MOVTOFLP.MES_PASSADO, 
         MOVTOFLP.PROJECAO,
         To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
          
         lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
         0 VALOR_P, 
         sum(i.valoritemdoc) VALOR_V,
         ProximoMes
    from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl, ProximoMEs
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMes
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21103,21125,24217,24689)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21103,21125,24217,24689)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21103,21125,24217,24689)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (21103,21125,24217,24689)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (21103,21125,24217,24689)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, MovtoFlp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl  
  UNION ALL  
 select '11 - INVESTIMENTOS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa,
         codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                            0 Valor_m2,
                            0 Valor_m3,
                            dflp.desctpdespesa Despesa,
                            cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      And i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '11 - INVESTIMENTOS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa,
         codigoempresa, codigofl, ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMEs                  
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (24104,24105,24106,24109,24110,24113,24619,24646)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoflp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl  
  UNION ALL  
 select '12 - OUTRAS DESPESAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data,
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMEs
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select  MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
           (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
           movtoflp.despesa,
           codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22128)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22128)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22128)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22128)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '12 - OUTRAS DESPESAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        ProximoMEs
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl, ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMEs                  
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22128)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22128)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22128)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (22128)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (22128)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO,  movtoflp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl  
  UNION ALL  
 select '13 - AUDITORIA' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data,
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
       and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (23201)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
 UNION ALL
 select '13 - AUDITORIA' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        ProximoMEs
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO,
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO,
          movtoflp.despesa, 
          codigoempresa, codigofl, ProximoMes
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMEs
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      And d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (23201)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoflp.ProximoMEs, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl    
  UNION ALL  
 select '14 - OUTORGA' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa 
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (24201)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '14 - OUTORGA' CLASS, 
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
         MOVTOFLP.MES_PASSADO, 
         MOVTOFLP.PROJECAO,
         To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
          
         lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,  
         0 VALOR_P, 
         sum(i.valoritemdoc) VALOR_V,
         ProximoMEs
    From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl, ProximoMEs
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) proximoMes
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24201)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (24201)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoFlp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl
  UNION ALL  
 select '15 - DESPESAS BANCARIAS' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23303,23404)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23303,23404)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23303,23404)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (23303,23404)
     GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
  select '15 - DESPESAS BANCARIAS' CLASS, 
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
         MOVTOFLP.MES_PASSADO, 
         MOVTOFLP.PROJECAO,
         To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
          
         lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
         0 VALOR_P, 
         sum(i.valoritemdoc) VALOR_V,
         ProximoMEs
    from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl, ProximoMEs
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMEs                  
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23303,23404)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23303,23404)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23303,23404)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23303,23404)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp              
               group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (23303,23404)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, MovtoFlp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl        
  UNION ALL  
 select '16 - FINAME' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        sum(i.valoritemdoc) VALOR_P, 
        0 VALOR_V,
        0 ProximoMes
   from cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                         
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '16 - FINAME' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
         
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        ProximoMes
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
         (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
         movtoflp.despesa,
         codigoempresa, codigofl, ProximoMEs
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMes                  
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoflp.ProximoMes, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl  
  UNION ALL  
 Select '17 - CAPITAL DE GIRO' CLASS, 
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
         MOVTOFLP.MES_PASSADO, 
         MOVTOFLP.PROJECAO,
         To_Char(C.PAGAMENTOCPG,'MM/yyyy') data, 
          
         lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,   
         sum(i.valoritemdoc) VALOR_P, 
         0 VALOR_V,
         0 ProximoMEs
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl)movtoflp)MOVTOFLP
    where c.coddoctocpg = i.coddoctocpg
      and movtoflp.despesa = d.desctpdespesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)  
      And c.Codigoempresa < 100
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      and d.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, To_Char(C.PAGAMENTOCPG,'MM/yyyy'), c.codigoempresa, c.codigofl, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO
  UNION ALL
 select '17 - CAPITAL DE GIRO' CLASS, 
        D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,  
        MOVTOFLP.MES_PASSADO, 
        MOVTOFLP.PROJECAO,
        To_char(C.VENCIMENTOCPG,'mm/yyyy') data, 
        
        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,    
        0 VALOR_P, 
        sum(i.valoritemdoc) VALOR_V,
        ProximoMEs
   From cpgdocto c, cpgitdoc i, cpgtpdes d,
  (Select MOVTOFLP.VLR_PERIODO_1 MES_PASSADO, 
          (vlr_periodo_1 + vlr_periodo_2 + vlr_periodo_3)/3 PROJECAO, 
          movtoflp.despesa,
          codigoempresa, codigofl, ProximoMEs
     From (Select movtoflp.despesa, codigoempresa, codigofl,
                  sum(movtoflp.valor_m1) vlr_periodo_1,
                  sum(movtoflp.valor_m2) vlr_periodo_2,
                  sum(movtoflp.valor_m3) vlr_periodo_3,
                  sum(movtoflp.valor_m4) ProximoMEs                  
             From ((select sum(iflp.valoritemdoc) Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl 
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           sum(iflp.valoritemdoc) Valor_m2,
                           0 Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      FROM cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -3)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -2))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) 
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           sum(iflp.valoritemdoc) Valor_m3,
                           0 Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.pagamentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -4)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -3))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa)
                     UNION ALL
                   (select 0 Valor_m1,
                           0 Valor_m2,
                           0 Valor_m3,
                           sum(iflp.valoritemdoc) Valor_m4,
                           dflp.desctpdespesa Despesa,
                           cflp.codigoempresa, cflp.codigofl                          
                      From cpgdocto cflp, cpgitdoc iflp, cpgtpdes dflp 
                     where cflp.coddoctocpg = iflp.coddoctocpg
                       and iflp.codtpdespesa = dflp.codtpdespesa
                       and cflp.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1))
                       and cflp.statusdoctocpg <> 'C' 
                       and cflp.codtpdoc not in ('AD','BOS')
                       and dflp.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                     group by cflp.codigoempresa, cflp.codigofl, dflp.codtpdespesa,dflp.desctpdespesa) ) movtoflp
              group By movtoflp.despesa, codigoempresa, codigofl) movtoflp) MOVTOFLP  
    where c.coddoctocpg = i.coddoctocpg
      and d.desctpdespesa = movtoflp.despesa
      and i.codtpdespesa = d.codtpdespesa
      and c.codigoempresa = movtoflp.codigoempresa
      and c.codigofl = movtoflp.codigofl
      and c.vencimentocpg between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +0))
      and c.statusdoctocpg <> 'C' 
      and c.codtpdoc not in ('AD','BOS')
      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263) 
      and d.codtpdespesa IN (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
    GROUP BY D.CODTPDESPESA ||' - '||D.DESCTPDESPESA, MOVTOFLP.MES_PASSADO, MOVTOFLP.PROJECAO, movtoflp.ProximoMEs, To_char(C.VENCIMENTOCPG,'mm/yyyy'),   c.codigoempresa, c.codigofl  
) 
