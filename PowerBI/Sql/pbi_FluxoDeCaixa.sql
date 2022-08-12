create or replace view pbi_fluxodecaixa as
Select CLASS,
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Media,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual) QtdDiasAtual,
         Max(QtdDiasAnterior) QtdDiasAnterior,
         Max(QtdDiasSequinte) QtdDiasSequinte,
         coddoctocpg,
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) - Nvl((Select Round(Sum(i1.valoritemdoc),2) Valor
                                From cpgdocto d1, Cpgitdoc i1
                               Where d1.coddoctocpg_adto = c.coddoctocpg
                                 And i1.codtpdespesa = d.codtpdespesa
                                 And d1.coddoctocpg = i1.coddoctocpg
                                 And d1.codmovtobco Is Not Null
                               Group By i1.codtpdespesa ),0) ValorPago, -- Pago
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 c.coddoctocpg,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                 LAST_DAY(C.PAGAMENTOCPG) data,
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,
                 f.CLASS
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d,
                 pbi_Classif_FluxoCaixa F
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And d.codtpdespesa = f.CODTPDESPESA
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('BOS')
             And c.codmovtobco Is Not Null
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -36)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA, D.DESCTPDESPESA,
                    c.coddoctocpg,
                    F.Class
           Union All
          Select Abs(Sum(c.vlmovtobco)) ValorPago, -- Pago
                           0 ValorAPagar,
                           0 MesAnterior,
                           0 Projecao,
                           0 ProximoMes,
                           0 QtdDiasAtual,
                           0 QtdDiasAnterior,
                           0 QtdDiasSequinte,
                           c.codmovtobco,
                           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                           Last_Day(C.Dtmovtobco) data,
                           lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,
                           f.CLASS
                      From BCOMovto C,
                           Cpgtpdes d,
                           pbi_Classif_FluxoCaixa F
                     Where d.codtpdespesa = c.codtpdespesa
                       And d.codtpdespesa = f.CODTPDESPESA
                       And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                       And c.statusmovtobco <> 'C'
                       And c.sistema = 'BCO'
                       And c.codmovtobco Is Not Null
                       And c.Dtmovtobco between Last_day(Add_Months(trunc(Sysdate),-2))+1 And Last_day(Add_Months(trunc(Sysdate),-1))
                     Group By Last_Day(C.Dtmovtobco),
                              lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                              D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA,
                              c.codmovtobco,
                              F.Class
           Union All
          Select 0 ValorPago, -- a Pagar
                 Sum(i.valoritemdoc) - Nvl((Select Round(Sum(i1.valoritemdoc),2) Valor
                                From cpgdocto d1, Cpgitdoc i1
                               Where d1.coddoctocpg_adto = c.coddoctocpg
                                 And i1.codtpdespesa = d.codtpdespesa
                                 And d1.coddoctocpg = i1.coddoctocpg
                                 And d1.codmovtobco Is Not Null
                               Group By i1.codtpdespesa ),0) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 c.coddoctocpg,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                 LAST_DAY(C.Vencimentocpg) data,
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,
                 f.CLASS
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d,
                 pbi_Classif_FluxoCaixa F
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And d.codtpdespesa = f.CODTPDESPESA
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('BOS')
             And c.pagamentocpg Is Null
             -- inicio de 2 anos atraz até último dia mês atual
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -36)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
           Group By LAST_DAY(C.Vencimentocpg),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA, D.DESCTPDESPESA,
                    c.coddoctocpg,
                    F.Class
           Union All
          Select 0 ValorPago, -- ProximoMes
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc)  - Nvl((Select Round(Sum(i1.valoritemdoc),2) Valor
                                From cpgdocto d1, Cpgitdoc i1
                               Where d1.coddoctocpg_adto = c.coddoctocpg
                                 And i1.codtpdespesa = d.codtpdespesa
                                 And d1.coddoctocpg = i1.coddoctocpg
                                 And d1.codmovtobco Is Not Null
                               Group By i1.codtpdespesa ),0) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 c.coddoctocpg,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1) data,
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil,
                 f.CLASS
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d,
                 pbi_Classif_FluxoCaixa F
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And d.codtpdespesa = f.CODTPDESPESA
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')
             -- inicio de 2 anos atraz até último dia mês seguinte
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -35)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA, D.DESCTPDESPESA,
                    c.coddoctocpg,
                    F.Class
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 coddoctocpg,
                 a.DESPESA,
                 data,
                 EmpFil,
                 f.Class
            From pbi_Classif_FluxoCaixa F,
                 ((Select Sum(i.valoritemdoc) - Nvl((Select Round(Sum(i1.valoritemdoc),2) Valor
                                From cpgdocto d1, Cpgitdoc i1
                               Where d1.coddoctocpg_adto = c.coddoctocpg
                                 And i1.codtpdespesa = d.codtpdespesa
                                 And d1.coddoctocpg = i1.coddoctocpg
                                 And d1.codmovtobco Is Not Null
                               Group By i1.codtpdespesa ),0) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,
                          0 QtdDiasAnterior ,
                          c.coddoctocpg,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d,
                          pbi_Classif_FluxoCaixa F
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And d.codtpdespesa = f.CODTPDESPESA
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('BOS')
                      And c.codmovtobco Is Not Null
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -39)) -- inicio de 2 ano atraz + 3 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             c.coddoctocpg,
                             D.CODTPDESPESA, D.DESCTPDESPESA
                    Union All
                    Select Abs(Sum(c.vlmovtobco)) Mes1, -- Pago
                           0 Mes2,
                           0 Mes3,
                           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                           D.CODTPDESPESA,
                           Add_Months(Last_day(Trunc(C.Dtmovtobco)),3) data,
                           0 QtdDiasAnterior ,
                           c.codmovtobco,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                      From BCOMovto C,
                           Cpgtpdes d,
                           pbi_Classif_FluxoCaixa F
                     Where d.codtpdespesa = c.codtpdespesa
                       And d.codtpdespesa = f.CODTPDESPESA
                       And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                       And c.statusmovtobco <> 'C'
                       And c.sistema = 'BCO'
                       And c.codmovtobco Is Not Null
                       And c.Dtmovtobco between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -39)) -- inicio de 2 ano atraz + 3 meses antes
                       and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3)
                     Group By Add_Months(Last_day(Trunc(C.Dtmovtobco)),3),
                              lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                              D.CODTPDESPESA, D.DESCTPDESPESA,
                              c.codmovtobco)
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc)  - Nvl((Select Round(Sum(i1.valoritemdoc),2) Valor
                                From cpgdocto d1, Cpgitdoc i1
                               Where d1.coddoctocpg_adto = c.coddoctocpg
                                 And i1.codtpdespesa = d.codtpdespesa
                                 And d1.coddoctocpg = i1.coddoctocpg
                                 And d1.codmovtobco Is Not Null
                               Group By i1.codtpdespesa ),0) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,
                          0 QtdDiasAnterior,
                          c.coddoctocpg,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d,
                          pbi_Classif_FluxoCaixa F
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And d.codtpdespesa = f.CODTPDESPESA
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codmovtobco Is Not Null
                      And c.codtpdoc Not In ('BOS')
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -38)) -- inicio de 2 ano atraz + 2 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             c.coddoctocpg,
                             D.CODTPDESPESA, D.DESCTPDESPESA
                    Union All
                    Select 0 Mes1,
                           Abs(Sum(c.vlmovtobco)) Mes2, -- Pago
                           0 Mes3,
                           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                           D.CODTPDESPESA,
                           Add_Months(Last_day(Trunc(C.Dtmovtobco)),2) data,
                           0 QtdDiasAnterior ,
                           c.codmovtobco,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                      From BCOMovto C,
                           Cpgtpdes d,
                           pbi_Classif_FluxoCaixa F
                     Where d.codtpdespesa = c.codtpdespesa
                       And d.codtpdespesa = f.CODTPDESPESA
                       And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                       And c.statusmovtobco <> 'C'
                       And c.sistema = 'BCO'
                       And c.codmovtobco Is Not Null
                       And c.Dtmovtobco between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -38)) -- inicio de 2 ano atraz + 3 meses antes
                       and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2)
                     Group By Add_Months(Last_day(Trunc(C.Dtmovtobco)),2),
                              lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                              D.CODTPDESPESA, D.DESCTPDESPESA,
                              c.codmovtobco)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) - Nvl((Select Round(Sum(i1.valoritemdoc),2) Valor
                                From cpgdocto d1, Cpgitdoc i1
                               Where d1.coddoctocpg_adto = c.coddoctocpg
                                 And i1.codtpdespesa = d.codtpdespesa
                                 And d1.coddoctocpg = i1.coddoctocpg
                                 And d1.codmovtobco Is Not Null
                               Group By i1.codtpdespesa ),0) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,
                          c.coddoctocpg,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d,
                          pbi_Classif_FluxoCaixa F
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And d.codtpdespesa = f.CODTPDESPESA
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('BOS')
                      And c.codmovtobco Is Not Null
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -37)) -- inicio de 2 ano atraz + 1 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             c.coddoctocpg,
                             D.CODTPDESPESA, D.DESCTPDESPESA
                    Union All
                    Select 0 Mes1,
                           0 Mes2,
                           Abs(Sum(c.vlmovtobco)) Mes3, -- Pago
                           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                           D.CODTPDESPESA,
                           Add_Months(Last_day(Trunc(C.Dtmovtobco)),1) data,
                           0 QtdDiasAnterior ,
                           c.codmovtobco,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                      From BCOMovto C,
                           Cpgtpdes d,
                           pbi_Classif_FluxoCaixa F
                     Where d.codtpdespesa = c.codtpdespesa
                       And d.codtpdespesa = f.CODTPDESPESA
                       And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                       And c.statusmovtobco <> 'C'
                       And c.sistema = 'BCO'
                       And c.codmovtobco Is Not Null
                       And c.Dtmovtobco between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -40)) -- inicio de 2 ano atraz + 3 meses antes
                       and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1)
                     Group By Add_Months(Last_day(Trunc(C.Dtmovtobco)),1),
                              lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                              D.CODTPDESPESA, D.DESCTPDESPESA,
                              c.codmovtobco) ) a
                Where f.CODTPDESPESA = a.codTpDespesa
                Group By Data, Empfil, a.Despesa, CodDoctoCpg, Class)
   Group By Class, Data, Empfil, Despesa, CodDoctoCpg

