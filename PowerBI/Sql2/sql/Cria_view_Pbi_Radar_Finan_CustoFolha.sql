create or replace view pbi_radar_finan_custofolha as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Round(valor) Valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select 'Custo Folha por carro' Grupo, 9 Ordem,
               Data,
               EmpFil,
               Decode(Max(QtdCarros), 0, 0, Sum(saldoAcumulado)/Max(QtdCarros)) Valor,
               0 Percentual,
                'F' Tipo
          From (Select EmpFil,
                       Last_day(Data) Data,
                       0 saldoAcumulado,
                       Sum(Valor) QtdCarros
                  From Pbi_Radar_Operacional
                 Where grupo = 'Frota Operacional'
                 Group By Last_day(Data), EmpFil
                 Union All
                Select EmpFil,
                       Last_day(Data),
                       Sum(Valor) saldoAcumulado,
                       0 QtdCarros
                  From Pbi_Radar_Financeiro_Totfolha
                 Group By EmpFil, Data)
         Group By EmpFil, Data )

