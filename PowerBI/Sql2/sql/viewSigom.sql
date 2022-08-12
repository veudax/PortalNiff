
Select t.TIP_ID, t.TIP_DESCRICAO, Sum(rt.ret_qtdade_giros) qtGiros, sum(rt.ret_valor)/100 valor
     , l.LIN_CODIFICACAO, v.VEI_PREFIXO, Min(r.rev_catraca_ini) InicioRoleta, Max(r.rev_catraca_fim) FimRoleta
     , To_char(Min(r.rev_dthora_jornada_ini),'dd/mm/yyyy hh24:mi') InicioJornada
     , To_char(Max(r.rev_dthora_jornada_Fim),'dd/mm/yyyy hh24:mi') FimJornada  
     , ope_id_operador_fim, o.OPE_NOME 
  From plus.rev_resumo_viagem r
     , plus.view_lin_linha l
     , plus.ret_resumo_viagem_tipo rt
     , plus.view_tip_tipo_cartao t
     , plus.view_vei_veiculo v
     , plus.view_ope_operador o
 Where l.LIN_ID = r.lin_id
--   And l.LIN_ID = 154   
--   And r.vei_id = 3392
   And trunc(r.rev_dthora_jornada_ini) = '20-nov-2018'
   And r.tab_id Is Not Null
   And rt.rev_id = r.rev_id
   And rt.tip_id = t.TIP_ID
   And rt.TIP_OPE_USU = t.tip_ope_usu
   And r.vei_id = v.VEI_ID
   And o.OPE_ID = r.ope_id_operador_fim
   Group By t.TIP_ID, t.TIP_DESCRICAO
     , l.LIN_CODIFICACAO , v.VEI_PREFIXO, ope_id_operador_fim, o.OPE_NOME    
 Union All
Select t.TIP_ID, 'Integracao', sum(rt.ret_qtde_integra) qtdIntegra, sum(rt.ret_valor_integra)/100 valorIntegra
     , l.LIN_CODIFICACAO , v.VEI_PREFIXO , Min(r.rev_catraca_ini), Max(r.rev_catraca_fim)        
     , To_char(Min(r.rev_dthora_jornada_ini),'dd/mm/yyyy hh24:mi') InicioJornada
     , To_char(Max(r.rev_dthora_jornada_Fim),'dd/mm/yyyy hh24:mi') FimJornada  
     , ope_id_operador_fim, o.OPE_NOME  
  From plus.rev_resumo_viagem r
     , plus.view_lin_linha l
     , plus.ret_resumo_viagem_tipo rt
     , plus.view_tip_tipo_cartao t
     , plus.view_vei_veiculo v   
     , plus.view_ope_operador o       
 Where r.vei_id = 3392
   And l.LIN_ID = r.lin_id
   And l.LIN_ID = 154
   And trunc(r.rev_dthora_jornada_ini) = '20-nov-2018'
   And r.tab_id Is Not Null
   And rt.rev_id = r.rev_id
   And rt.tip_id = t.TIP_ID
   And rt.TIP_OPE_USU = t.tip_ope_usu
   And r.vei_id = v.VEI_ID   
   And o.OPE_ID = r.ope_id_operador_fim
 Group By t.TIP_ID, l.LIN_CODIFICACAO , v.VEI_PREFIXO, ope_id_operador_fim, o.OPE_NOME       
 Union All
Select 0 TIP_ID, 'Pagantes', Sum(r.rev_total_botoeira), Sum(r.rev_total_botoeira * r.rev_valor_tarifa) 
     , l.LIN_CODIFICACAO , v.VEI_PREFIXO , Min(r.rev_catraca_ini), Max(r.rev_catraca_fim)        
     , To_char(Min(r.rev_dthora_jornada_ini),'dd/mm/yyyy hh24:mi') InicioJornada
     , To_char(Max(r.rev_dthora_jornada_Fim),'dd/mm/yyyy hh24:mi') FimJornada  
     , ope_id_operador_fim, o.OPE_NOME   
  From plus.rev_resumo_viagem r
     , plus.view_lin_linha l
     , plus.view_vei_veiculo v     
     , plus.view_ope_operador o     
 Where r.vei_id = 3392
   And l.LIN_ID = r.lin_id
   And l.LIN_ID = 154
   And trunc(r.rev_dthora_jornada_ini) = '20-nov-2018'
   And r.tab_id Is Not Null
   And r.vei_id = v.VEI_ID   
   And o.OPE_ID = r.ope_id_operador_fim
 Group By  l.LIN_CODIFICACAO , v.VEI_PREFIXO, ope_id_operador_fim, o.OPE_NOME 

