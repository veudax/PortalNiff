Create Or Replace View PBI_FluxoDeCaixa As
Select a.*, To_char(Data, 'yyyy/mm') AnoMes  
  From (
  Select '01 - FOLHA DE PAGAMENTO' CLASS, 
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
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
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
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('BOS')  
             And c.pagamentocpg Is Null
             -- inicio de 2 anos atraz até último dia mês atual
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
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
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.codigofl = 1
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('BOS')  
             -- inicio de 2 anos atraz até último dia mês seguinte
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,   
                          0 QtdDiasAnterior , 
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz + 3 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz + 2 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data, 
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz + 1 meses antes
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '02 - MANUTENCAO DA FROTA' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Media,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,    
                          0 QtdDiasAnterior ,
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
          
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,  
                          0 QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,  
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '03 - GESTAO DA FROTA' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Media,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,   
                          0 QtdDiasAnterior , 
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)          
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,   
                          0 QtdDiasAnterior,    
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,   
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,    
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '04 - DESPESAS C/ EQUIPAMENTOS E FERRAMENTAS' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,
                          0 QtdDiasAnterior ,    
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,  
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (22211,24122,24608,24627,24628,54580,54581)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '05 - FROTA DE APOIO' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22401,22403,22404,22405,22406)
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22401,22403,22404,22405,22406)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (22401,22403,22404,22405,22406)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data, 
                          0 QtdDiasAnterior ,   
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (22401,22403,22404,22405,22406)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),  
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (22401,22403,22404,22405,22406)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,    
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,   
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (22401,22403,22404,22405,22406)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1), Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All   
  Select '06 - IMPOSTOS' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data, 
                          0 QtdDiasAnterior ,   
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),  
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
          
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,  
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,24657,24672,24688,24692,24710,23311,24714,24715,24720)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1), Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '07 - JUROS E MULTAS' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (23401,23402)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (23401,23402)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (23401,23402)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,  
                          0 QtdDiasAnterior ,  
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (23401,23402)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),  
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (23401,23402)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,    
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,   
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (23401,23402)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1), Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All  
  Select '08 - DESPESAS ADMINISTRATIVAS' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
  24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
  24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
  24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
  24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
  24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
  24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data, 
                          0 QtdDiasAnterior ,   
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
  24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
  24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,
                          0 QtdDiasAnterior,       
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
  24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
  24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,  
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
  24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
  24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '09 - DESPESAS JURIDICAS' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,
                          0 QtdDiasAnterior ,    
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,  
                          0 QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,   
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,    
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (814,22111,22125,23203,24212,24219,24509,23317)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '10 - ORGAO GESTOR' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (21103,21125,24217,24689)
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (21103,21125,24217,24689)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (21103,21125,24217,24689)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,  
                          0 QtdDiasAnterior ,  
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (21103,21125,24217,24689)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (21103,21125,24217,24689)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,       
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (21103,21125,24217,24689)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '11 - INVESTIMENTOS' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,  
                          0 QtdDiasAnterior ,  
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,
                          0 QtdDiasAnterior,       
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,    
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,   
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (24104,24105,24106,24109,24110,24113,24619,24646)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '12 - OUTRAS DESPESAS' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22128)
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (22128)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (22128)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,
                          0 QtdDiasAnterior ,   
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (22128)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (22128)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,       
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (22128)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa          
   Union All
  Select '13 - AUDITORIA' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (23201)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (23201)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (23201)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,
                          0 QtdDiasAnterior ,    
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (23201)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),  
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (23201)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data, 
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (23201)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1), Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '14 - OUTORGA' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (24201)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (24201)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (24201)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,  
                          0 QtdDiasAnterior ,  
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (24201)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,  
                          0 QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (24201)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data, 
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (24201)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '15 - DESPESAS BANCARIAS' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (23303,23404)
           Group By LAST_DAY(C.PAGAMENTOCPG),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (23303,23404)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (23303,23404)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,   
                          0 QtdDiasAnterior , 
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (23303,23404)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,  
                          0 QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (23303,23404)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data, 
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (23303,23404)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '16 - FINAME' CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Projecao,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data, 
                          0 QtdDiasAnterior ,   
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data,  
                          0 QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data, 
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (24558,24670,24102,24117,24115,24558,24519,24116,24118,24670,24102)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1),  Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
   Union All
  Select '17 - CAPITAL DE GIRO'  CLASS, 
         Sum(ValorPago) Valor_P,
         Sum(ValorAPagar) Valor_V,
         Sum(MesAnterior) Mes_passado,
         Sum(Projecao) Media,
         Sum(ProximoMes) ProximoMes,
         Max(QtdDiasAtual),
         Max(QtdDiasAnterior),
         Max(QtdDiasSequinte),
         EmpFil,
         Data,
         Despesa
    From (Select Sum(i.valoritemdoc) ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.PAGAMENTOCPG) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
             and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
           Group By LAST_DAY(C.PAGAMENTOCPG), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
           Union All
          Select 0 ValorPago,
                 Sum(i.valoritemdoc) ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 LAST_DAY(C.Vencimentocpg) data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.pagamentocpg Is Null
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
             And d.codtpdespesa In (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
           Group By LAST_DAY(C.Vencimentocpg), 
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 0 MesAnterior,
                 0 Projecao,
                 Sum(i.valoritemdoc) ProximoMes,
                 0 QtdDiasAtual,
                 0 QtdDiasAnterior,
                 fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.VencimentoCPG))) QtdDiasSequintes,
                 D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                 Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1)  data,    
                 lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
            From CPGDocto C,
                 Cpgitdoc I,
                 Cpgtpdes d
           Where c.coddoctocpg = i.coddoctocpg
             And d.codtpdespesa = i.codtpdespesa
             And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
             And c.statusdoctocpg <> 'C'
             And c.codtpdoc Not In ('AD','BOS')  
             And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -23)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
             And d.codtpdespesa In (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
           Group By Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1), Last_day(Trunc(C.VencimentoCPG)),
                    lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                    D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
           Union All
          Select 0 ValorPago,
                 0 ValorAPagar,
                 Sum(Mes3) MesAnterior,
                 (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
                 0 ProximoMes,
                 0 QtdDiasAtual,
                 Max(QtdDiasAnterior) QtdDiasAnterior,
                 0 QtdDiasSequinte,
                 DESPESA,  
                 data,    
                 EmpFil
            From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),3) data,  
                          0 QtdDiasAnterior ,  
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-3) -- 3 meses antes
                      And d.codtpdespesa In (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),3),  
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)        
                    Union All
                  (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),2) data, 
                          0 QtdDiasAnterior,      
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -26)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-2) -- 2 meses antes
                      And d.codtpdespesa In (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),2), 
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                    Union All
                  (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                          D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                          Add_Months(Last_day(Trunc(C.pagamentocpg)),1) data,  
                          fc_Niff_CalculaDiasUteis(Last_day(Trunc(C.pagamentocpg))) QtdDiasAnterior,     
                          lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                     From CPGDocto C,
                          Cpgitdoc I,
                          Cpgtpdes d
                    Where c.coddoctocpg = i.coddoctocpg
                      And d.codtpdespesa = i.codtpdespesa
                      And c.codigoempresa || c.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
                      And c.statusdoctocpg <> 'C'
                      And c.codtpdoc Not In ('AD','BOS')  
                      And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -25)) -- inicio de 2 ano atraz
                      and Add_Months(LAST_DAY(Trunc(SYSDATE)),-1) -- 1 mes antes
                      And d.codtpdespesa In (23405,23408,24610,24648,24671,24686, 24637, 24617, 24671,24694,24564,24680,24696,24686,24648,24702,24699,24607,24610,23408,23407,23405)
                    Group By Add_Months(Last_day(Trunc(C.pagamentocpg)),1), Last_day(Trunc(C.pagamentocpg)),
                             lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                             D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
                Group By Data, Empfil, Despesa)
   Group By Data, Empfil, Despesa              
) A