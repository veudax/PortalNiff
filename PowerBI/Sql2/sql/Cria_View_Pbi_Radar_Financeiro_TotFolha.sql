create or replace view pbi_radar_financeiro_totfolha as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       valor,
       decode(Percentual,0,Null,Round(Percentual,2)) percentual,
       Ordem,
       Tipo
  From (Select 'Folha Pagto Total' Grupo, 3 Ordem,
               Data,
               EmpFil,
               Sum(SaldoAcumulado) Valor,
               Decode(Sum(RecOp), 0, 0, (Sum(SaldoAcumulado)/Abs(Sum(RecOp)))*100) Percentual,
               'F' Tipo
          From (Select EmpFil,
                       Data,
                       0 SaldoAcumulado,
                       Sum(Valor) RecOp
                  From pbi_Radar_finan_Rec_Oper
                 Group By EmpFil, Data
                 Union All
                Select EmpFil,
                       Data,
                       Sum(Valor) SaldoAcumulado,
                       0 RecOp
                  From Pbi_Radar_Finan_Totfolhaadm
                 Group By EmpFil, Data
                 Union All
                Select EmpFil,
                       Data,
                       Sum(Valor) SaldoAcumulado,
                       0 RecOp
                  From Pbi_Radar_Finan_Totfolhaman
                 Group By EmpFil, Data
                 Union All
                Select EmpFil,
                       Data,
                       Sum(Valor) SaldoAcumulado,
                       0 RecOp
                  From Pbi_Radar_Finan_TotfolhaOp
                 Group By EmpFil, Data  )
         Group By Data, EmpFil )

