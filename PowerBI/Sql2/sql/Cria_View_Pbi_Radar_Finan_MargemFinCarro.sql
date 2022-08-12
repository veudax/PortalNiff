create or replace view pbi_radar_finan_margemfincarro as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select 'Margem financeiro por carro' Grupo, 10 Ordem,
                Data,
                EMPFIL,
                Sum(Valor) Valor,
                0 Percentual,
                'F' Tipo
          From (Select Data, EmpFil, Valor
                  From Pbi_Radar_Finan_ReceitaCarro
                 Union All
                Select Data, EmpFil, Valor
                  From Pbi_Radar_Finan_CustoManFrota
                 Union All
                Select Data, EmpFil, Valor
                  From Pbi_Radar_Finan_CustoFolha  )
         Group By Data, EmpFil )

