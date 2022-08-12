create or replace view pbi_fluxodecaixa_bsp as
Select CLASS,
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Media,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual) QtdDiasAtual,
         Max(QtdDiasAnterior) QtdDiasAnterior,
         Max(QtdDiasSequinte) QtdDiasSequinte,
         0 coddoctocpg,
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago, -- Pago
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 c.coddoctocpg,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                 C.PAGAMENTOCPG data,
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
             And c.nrodoctocpg not Like 'AD-%'
             And c.codmovtobco Is Not Null
             And c.pagamentocpg between Last_day(Add_Months(trunc(Sysdate),-4))+1 And Last_day(Add_Months(trunc(Sysdate),-3))
           Group By C.PAGAMENTOCPG,
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA,
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
                           C.Dtmovtobco data,
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
                       And c.Dtmovtobco between Last_day(Add_Months(trunc(Sysdate),-4))+1 And Last_day(Add_Months(trunc(Sysdate),-3))
                     Group By C.Dtmovtobco,
                              lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                              D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA,
                              c.codmovtobco,
                              F.Class
           Union All
          Select 0 ValorPago, -- a Pagar
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 c.coddoctocpg,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                 C.Vencimentocpg data,
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
             And c.nrodoctocpg not Like 'AD-%'
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between Last_day(Add_Months(trunc(Sysdate),-4))+1 And Last_day(Add_Months(trunc(Sysdate),-3))
           Group By Vencimentocpg,
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA,
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
                 ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          t.pagamento data,
                          0 QtdDiasAnterior ,
                          c.coddoctocpg,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d,
                          pbi_Classif_FluxoCaixa F,
                          (Select d.PagamentoCPG Pagamento, d.codigoEmpresa, d.CodigoFl, i.codtpdespesa
                             From Cpgdocto d, Cpgitdoc I, pbi_Classif_FluxoCaixa F
                            Where d.codigoempresa || d.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                              And i.codtpdespesa = f.CODTPDESPESA
                              And d.coddoctocpg = i.coddoctocpg
                              And d.pagamentocpg between Add_Months(LAST_DAY(Trunc(SYSDATE)),-4)+1 And Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- mes atual
                            Group By d.PagamentoCPG, d.codigoEmpresa, d.CodigoFl, i.codtpdespesa) t
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And d.codtpdespesa = f.CODTPDESPESA
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('BOS')
                      And c.nrodoctocpg not Like 'AD-%'
                      And c.codmovtobco Is Not Null
                      And c.pagamentocpg between Add_Months(LAST_DAY(Trunc(SYSDATE)),-7)+1 And Add_Months(LAST_DAY(Trunc(SYSDATE)),-6) -- 3 meses antes
                      And t.CodigoEmpresa = c.CodigoEmpresa
                      And t.CodigoFl = c.codigofl
                      And t.codtpdespesa = i.codtpdespesa
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             c.coddoctocpg,
                             D.CODTPDESPESA, D.DESCTPDESPESA, t.Pagamento)
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          t.Pagamento data,
                          0 QtdDiasAnterior,
                          c.coddoctocpg,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d,
                          pbi_Classif_FluxoCaixa F,
                          (Select d.PagamentoCPG Pagamento, d.codigoEmpresa, d.CodigoFl, i.codtpdespesa
                             From Cpgdocto d, Cpgitdoc I, pbi_Classif_FluxoCaixa F
                            Where d.codigoempresa || d.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                              And i.codtpdespesa = f.CODTPDESPESA
                              And d.coddoctocpg = i.coddoctocpg
                              And d.pagamentocpg between Add_Months(LAST_DAY(Trunc(SYSDATE)),-4)+1 And Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- mes atual
                            Group By d.PagamentoCPG, d.codigoEmpresa, d.CodigoFl, i.codtpdespesa) t
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And d.codtpdespesa = f.CODTPDESPESA
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codmovtobco Is Not Null
                      And c.codtpdoc Not In ('BOS')
                      And c.nrodoctocpg not Like 'AD-%'
                      And c.pagamentocpg between Add_Months(LAST_DAY(Trunc(SYSDATE)),-6)+1 And Add_Months(LAST_DAY(Trunc(SYSDATE)),-5) -- 2 Meses antes
                      And t.CodigoEmpresa = c.CodigoEmpresa
                      And t.CodigoFl = c.codigofl
                      And t.codtpdespesa = i.codtpdespesa
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             c.coddoctocpg,
                             D.CODTPDESPESA, D.DESCTPDESPESA, t.Pagamento)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          t.Pagamento data,
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,
                          c.coddoctocpg,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d,
                          pbi_Classif_FluxoCaixa F,
                          (Select d.PagamentoCPG Pagamento, d.codigoEmpresa, d.CodigoFl, i.codtpdespesa
                             From Cpgdocto d, Cpgitdoc I, pbi_Classif_FluxoCaixa F
                            Where d.codigoempresa || d.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                              And i.codtpdespesa = f.CODTPDESPESA
                              And d.coddoctocpg = i.coddoctocpg
                              And d.pagamentocpg between Add_Months(LAST_DAY(Trunc(SYSDATE)),-4)+1 And Add_Months(LAST_DAY(Trunc(SYSDATE)),-3)
                            Group By d.PagamentoCPG, d.codigoEmpresa, d.CodigoFl, i.codtpdespesa) t
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And d.codtpdespesa = f.CODTPDESPESA
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('BOS')
                      And c.nrodoctocpg not Like 'AD-%'
                      And c.codmovtobco Is Not Null
                      And c.pagamentocpg between Add_Months(LAST_DAY(Trunc(SYSDATE)),-5)+1 And Add_Months(LAST_DAY(Trunc(SYSDATE)),-4) -- 1 mes antes
                      And t.CodigoEmpresa = c.CodigoEmpresa
                      And t.CodigoFl = c.codigofl
                      And t.codtpdespesa = i.codtpdespesa
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             c.coddoctocpg,
                             D.CODTPDESPESA, D.DESCTPDESPESA, t.Pagamento) ) a
                Where f.CODTPDESPESA = a.codTpDespesa
                Group By Data, Empfil, a.Despesa, CodDoctoCpg, Class)
   Group By Class, Data, Empfil, Despesa

