Select '01 - FOLHA DE PAGAMENTO' CLASS, 
       Sum(ValorPago) Valor_P,
       Sum(ValorAPagar) Valor_V,
       Sum(MesAnterior) Mes_passado,
       Sum(Projecao) Projecao,
       Sum(ProximoMes) ProximoMes,
       EmpFil,
       Data,
       Despesa
  From (Select Sum(i.valoritemdoc) ValorPago,
               0 ValorAPagar,
               0 MesAnterior,
               0 Projecao,
               0 ProximoMes,
               D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
               To_Char(C.PAGAMENTOCPG,'MM/yyyy') data,    
               lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
          From CPGDocto C,
               Cpgitdoc I,
               Cpgtpdes d
         Where c.coddoctocpg = i.coddoctocpg
           And d.codtpdespesa = i.codtpdespesa
           And c.codigoempresa = 1
           And c.codigofl = 1
           And c.statusdoctocpg <> 'C'
           And c.codtpdoc Not In ('BOS')  
           And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz
           and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
           And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
         Group By To_Char(C.PAGAMENTOCPG,'MM/yyyy'), 
                  lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                  D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA
         Union All
        Select 0 ValorPago,
               Sum(i.valoritemdoc) ValorAPagar,
               0 MesAnterior,
               0 Projecao,
               0 ProximoMes,
               D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
               To_Char(C.Vencimentocpg,'MM/yyyy') data,    
               lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
          From CPGDocto C,
               Cpgitdoc I,
               Cpgtpdes d
         Where c.coddoctocpg = i.coddoctocpg
           And d.codtpdespesa = i.codtpdespesa
           And c.codigoempresa = 1
           And c.codigofl = 1
           And c.statusdoctocpg <> 'C'
           And c.codtpdoc Not In ('BOS')  
           And c.pagamentocpg Is Null
           And c.VencimentoCPG between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24)) -- inicio de 2 ano atraz 
           and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0)) -- ultimo dia mes atual
           And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
         Group By To_Char(C.Vencimentocpg,'MM/yyyy'), 
                  lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                  D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA           
         Union All
        Select 0 ValorPago,
               0 ValorAPagar,
               0 MesAnterior,
               0 Projecao,
               Sum(i.valoritemdoc) ProximoMes,
               D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
               To_Char(Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1),'MM/yyyy')  data,    
               lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
          From CPGDocto C,
               Cpgitdoc I,
               Cpgtpdes d
         Where c.coddoctocpg = i.coddoctocpg
           And d.codtpdespesa = i.codtpdespesa
           And c.codigoempresa = 1
           And c.codigofl = 1
           And c.statusdoctocpg <> 'C'
           And c.codtpdoc Not In ('BOS')  
           And c.VencimentoCPG between (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE))+1, 0)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
           And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
         Group By To_Char(Add_Months(Last_day(Trunc(C.VencimentoCPG)),-1),'MM/yyyy'), 
                  lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                  D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA                     
         Union All
        Select 0 ValorPago,
               0 ValorAPagar,
               Sum(Mes1) MesAnterior,
               (Sum(Mes1)+Sum(Mes2)+Sum(Mes3))/3 Projecao,
               0 ProximoMes,
               DESPESA,  
               data,    
               EmpFil
          From ((Select Sum(i.valoritemdoc) Mes1, 0 Mes2, 0 Mes3,
                        D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                        To_Char(Add_Months(Last_day(Trunc(C.pagamentocpg)),1),'MM/yyyy') data,    
                        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                   From CPGDocto C,
                        Cpgitdoc I,
                        Cpgtpdes d
                  Where c.coddoctocpg = i.coddoctocpg
                    And d.codtpdespesa = i.codtpdespesa
                    And c.codigoempresa = 1
                    And c.codigofl = 1
                    And c.statusdoctocpg <> 'C'
                    And c.codtpdoc Not In ('BOS')  
                    And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                    and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
                    And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                  Group By To_Char(Add_Months(Last_day(Trunc(C.pagamentocpg)),1),'MM/yyyy'), 
                           lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
        
                  Union All
                (Select 0 Mes1, Sum(i.valoritemdoc) Mes2, 0 Mes3,
                        D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                        To_Char(Add_Months(Last_day(Trunc(C.pagamentocpg)),2),'MM/yyyy') data,    
                        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                   From CPGDocto C,
                        Cpgitdoc I,
                        Cpgtpdes d
                  Where c.coddoctocpg = i.coddoctocpg
                    And d.codtpdespesa = i.codtpdespesa
                    And c.codigoempresa = 1
                    And c.codigofl = 1
                    And c.statusdoctocpg <> 'C'
                    And c.codtpdoc Not In ('BOS')  
                    And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                    and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
                    And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                  Group By To_Char(Add_Months(Last_day(Trunc(C.pagamentocpg)),2),'MM/yyyy'), 
                           lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA)
                  Union All
                (Select 0 Mes1, 0 Mes2, Sum(i.valoritemdoc) Mes3,
                        D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA DESPESA,  
                        To_Char(Add_Months(Last_day(Trunc(C.pagamentocpg)),3),'MM/yyyy') data,    
                        lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0') EmpFil
                   From CPGDocto C,
                        Cpgitdoc I,
                        Cpgtpdes d
                  Where c.coddoctocpg = i.coddoctocpg
                    And d.codtpdespesa = i.codtpdespesa
                    And c.codigoempresa = 1
                    And c.codigofl = 1
                    And c.statusdoctocpg <> 'C'
                    And c.codtpdoc Not In ('BOS')  
                    And c.pagamentocpg between (ADD_MONTHS(Trunc(SYSDATE,'rr'), -27)) -- inicio de 2 ano atraz
                    and (Trunc(SYSDATE)-1) --Dia anterior (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
                    And d.codtpdespesa In (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,23318,23309,22116,22138,23131,22105,24711,22131)
                  Group By To_Char(Add_Months(Last_day(Trunc(C.pagamentocpg)),3),'MM/yyyy'), 
                           lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
              Group By Data, Empfil, Despesa)
 Group By Data, Empfil, Despesa              
Order By Data, despesa