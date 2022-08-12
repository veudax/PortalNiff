Create Or Replace View Pbi_FluxoTeste As
  Select CLASS,
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Media,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual) QtdDiasAtual,
         Max(QtdDiasAnterior) QtdDiasAnterior,
         Max(QtdDiasSequinte) QtdDiasSequinte,
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
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA,
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
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
           Group By LAST_DAY(C.Vencimentocpg),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA,
                    F.Class
           Union All
          Select 0 ValorPago, -- ProximoMes
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
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
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA,
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
                 a.DESPESA,
                 data,
                 EmpFil,
                 f.Class
            From pbi_Classif_FluxoCaixa F,
                 ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,
                          0 QtdDiasAnterior ,
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
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz + 3 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA, D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,
                          0 QtdDiasAnterior,
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
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz + 2 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA, D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,
                          d.codtpdespesa,
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,
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
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz + 1 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA, D.DESCTPDESPESA) ) a
                Where f.CODTPDESPESA = a.codTpDespesa
                Group By Data, Empfil, a.Despesa, Class)
   Group By Class, Data, Empfil, Despesa
