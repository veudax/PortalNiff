create or replace view pbi_radar_financeiro_receitas as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select Grupo, Ordem,
               Data,
               EmpFil,
               Valor,
               Percentual,
               Tipo
          From pbi_radar_finan_receita
         Union All
        Select Grupo, Ordem,
               Data,
               EmpFil,
               Valor,
               Percentual,
               Tipo
          From pbi_radar_finan_Rec_Oper)

