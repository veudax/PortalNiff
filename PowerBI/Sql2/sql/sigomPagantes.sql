Select 0 Tip_Id,
       'PAGANTES' Tip_Descricao,
       Sum(r.Rev_Total_Botoeira) Qtgiros,
       Sum(r.Rev_Total_Botoeira * r.Rev_Valor_Tarifa) Valor,
       l.Lin_Codificacao,
       v.Vei_Prefixo,
       Min(r.Rev_Catraca_Ini) Inicioroleta,
       Max(r.Rev_Catraca_Fim) Fimroleta,
       To_Char(Min(r.Rev_Dthora_Ope_Ini), 'dd/mm/yyyy hh24:mi') Iniciojornada,
       To_Char(Min(r.Rev_Dthora_Jornada_Ini), 'dd/mm/yyyy hh24:mi') Iniciojornada2,
       To_Char(Max(r.Rev_Dthora_Jornada_Fim), 'dd/mm/yyyy hh24:mi') Fimjornada,
       Ope_Id_Operador_Ini,
       o.Ope_Nome, r.lin_id
  From Plus.Rev_Resumo_Viagem r,
       Plus.View_Lin_Linha    l,
       Plus.View_Vei_Veiculo  v,
       Plus.View_Ope_Operador o
 Where l.Lin_Id = r.Lin_Id
   And
    r.Ent_Id In (3,5,26)
   And Trunc(r.Rev_Dthora_Jornada_Ini) Between
       To_Date('31/01/2019', 'dd/mm/yyyy') And
       To_Date('31/01/2019', 'dd/mm/yyyy')
   And r.Tab_Id Is Not Null
--   And v.Vei_Prefixo = '2516'
   And r.Vei_Id = v.Vei_Id
   And o.Ope_Id = r.Ope_Id_Operador_Ini
  And l.LIN_CODIFICACAO = '230'
 Group By l.Lin_Codificacao, 
 v.Vei_Prefixo, Ope_Id_Operador_Ini, o.Ope_Nome, r.lin_id
 Order By Iniciojornada, Iniciojornada2, Vei_Prefixo, Lin_Codificacao