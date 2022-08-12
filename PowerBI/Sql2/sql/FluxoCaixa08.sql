Select '08 - DESPESAS ADMINISTRATIVAS' CLASS, 
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
           And c.codtpdoc Not In ('AD','BOS')  
           And c.pagamentocpg between (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE))+1, -1)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
           And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
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
           And c.codtpdoc Not In ('AD','BOS')  
           And c.pagamentocpg Is Null
           And c.VencimentoCPG between (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE))+1, -1)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), 0))
           And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
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
           And c.codtpdoc Not In ('AD','BOS')  
           And c.VencimentoCPG between (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE))+1, 0)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), +1))
           And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
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
                    And c.codtpdoc Not In ('AD','BOS')  
                    And c.pagamentocpg between (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE))+1, -2)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), -1))
                    And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
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
                    And c.codtpdoc Not In ('AD','BOS')  
                    And c.pagamentocpg between (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE))+1, -3)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), -2))
                    And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
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
                    And c.codtpdoc Not In ('AD','BOS')  
                    And c.pagamentocpg between (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE))+1, -4)) and (ADD_MONTHS(LAST_DAY(Trunc(SYSDATE)), -3))
                    And d.codtpdespesa In (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,
24308,24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,22217,22409,23122,23132,24616,24633,
24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,22132,24703,22307,21109,24616,24575)
                  Group By To_Char(Add_Months(Last_day(Trunc(C.pagamentocpg)),3),'MM/yyyy'), 
                           lpad(c.codigoempresa, 3, '0') || '/' || lPad(c.Codigofl, 3, '0'),
                           D.CODTPDESPESA ||' - '|| D.DESCTPDESPESA) )
              Group By Data, Empfil, Despesa)
 Group By Data, Empfil, Despesa              
