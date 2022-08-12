Select l.codigolinha, cv.prefixoveic, d.cod_tipopagtarifa, d.qtd_passag_trans
     , To_char(Min(v.qtd_hora_ini),'dd/mm/yyyy hh24:mi') InicioJornada
     , To_char(Max(v.qtd_hora_final),'dd/mm/yyyy hh24:mi') FimJornada  
     , g.cod_guia
     , t.cod_tipopagto_imp, t.nom_descricao, Sum(d.qtd_passag_trans * d.vlr_total) valor
     , f.Nomefunc
  From t_arr_guia g
     , t_arr_detalhe_guia d
     , t_arr_viagens_guia v
     , bgm_cadlinhas l
     , Frt_Cadveiculos cv
     , t_trf_tipopagto t, T_ARR_TROCAS_FUNC tf, Flp_Funcionarios f
 Where g.cod_empresa = 1
   And g.codigofl = 1  
   And g.cod_seq_guia = d.cod_seq_guia
   And d.codintlinha = l.codintlinha
   And d.cod_seq_viagem = v.cod_seq_viagem
   And v.cod_seq_guia = g.cod_seq_guia
   And cv.codigoveic = v.cod_veiculo
   And d.cod_tipopagtarifa = t.cod_tipopagto
   And d.cod_seq_guia = tf.cod_seq_guia
   And d.cod_seq_viagem = tf.cod_seq_viagem
   And tf.codintfunc = f.codintfunc
   And g.dat_prest_contas = '20-nov-2018'
   And l.codigolinha = '227TRO'
   And cv.prefixoveic = '0030529'
Group By l.codigolinha, cv.prefixoveic, d.cod_tipopagtarifa, d.qtd_passag_trans, v.qtd_hora_ini, v.qtd_hora_final, g.cod_guia
     , t.cod_tipopagto_imp, t.nom_descricao
     , f.Nomefunc
     