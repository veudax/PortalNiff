create or replace view pbi_radar as
Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Financeiro_Receitas
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Financeiro_Horaextra
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Financeiro_Totfolha
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Totfolhaop
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Totfolhaman
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Totfolhaadm
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Custofolha
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Customanfrota
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Margemfincarro
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Receitacarro
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Despesas
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Finan_Ebitda
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Rh
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Planejamento
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Manutencao
   Union All
  Select Grupo, EmpFil, Data, Valor, Percentual, Ordem, Tipo
    From Pbi_Radar_Operacao

