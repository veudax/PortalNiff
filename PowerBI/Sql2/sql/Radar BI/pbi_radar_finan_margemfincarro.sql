create or replace view pbi_radar_finan_margemfincarro as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Round(valor) Valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Percentual PercentualN,
       Ordem,
       Tipo
  From (Select 'Margem financeiro por carro' Grupo, 10 Ordem,
                Data,
                Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
                Sum(Valor) Valor,
                0 Percentual,
                'F' Tipo
          From (Select Data, EmpFil, Valor
                  From Pbi_Radar_Finan_ReceitaCarro
                 Union All
                Select Data, EmpFil,
                       Decode(Sum(QtdCarros), 0, 0, Sum(Valor)/Sum(QtdCarros))*-1
                  From (Select Last_day(Data) Data, EmpFil, Valor, 0 QtdCarros
                          From Pbi_Radar_Finan_CustoManFrota
                         Union All
                        Select Last_day(Data), EmpFil, 0 Valor, Sum(Valor) QtdCarros
                          From Pbi_Radar_Operacional
                         Where grupo = 'Frota Operacional'
                         Group By Last_day(Data), EmpFil)
                 Group By Data, EmpFil
                 Union All
                Select Last_day(Data), EmpFil, Valor*-1
                  From Pbi_Radar_Finan_CustoFolha
                 Union All
                Select Data, EmpFil,
                       Decode(Sum(QtdCarros), 0, 0, Sum(Valor)/Sum(QtdCarros))*-1
                  From (Select Last_day(Data) Data, EmpFil, Valor, 0 QtdCarros
                          From Pbi_Radar_Finan_Despesas
                         Union All
                        Select Last_day(Data), EmpFil, 0 Valor, Sum(Valor) QtdCarros
                          From Pbi_Radar_Operacional
                         Where grupo = 'Frota Operacional'
                         Group By Last_day(Data), EmpFil)
                 Group By Data, EmpFil                  )
         Group By Data, EmpFil )

