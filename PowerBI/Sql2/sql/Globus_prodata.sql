Select To_char(Min(v.qtd_hora_ini),'dd/mm/yyyy hh24:mi') InicioJornada,
To_char(Max(v.qtd_hora_final),'dd/mm/yyyy hh24:mi') FimJornada
, g.cod_guia, g.cod_seq_guia
, t.cod_tipopagto_imp, t.nom_descricao, Sum(d.vlr_receb) valor
, l.codigolinha, cv.prefixoveic, d.cod_tipopagtarifa, d.qtd_passag_trans
, (Select Distinct tp.vlr_tarifa
  From t_Trf_Tarifa_Tipopagto Tp,
       Bgm_Cadlinhas          l,
       t_Trf_Linha_Secao      Ls,
       t_Trf_Secao            s
 Where Tp.Cod_Tipopagtarifa = d.cod_tipopagtarifa
   And 
   s.Cod_Tarifa = Tp.Cod_Tarifa
   And s.Cod_Seq_Secao = Ls.Cod_Seq_Secao
   And Ls.Flg_Disponivel = 'S'
   And Ls.Cod_Linha = l.Codintlinha
   And l.codintlinha = d.codintlinha
   And l.Codigoempresa = 2
   And (Tp.Dat_Iniciovigencia =
       (Select Max(Dat_Iniciovigencia)
          From t_Trf_Tarifa_Tipopagto
         Where Cod_Tipopagtarifa = d.cod_tipopagtarifa
           And Dat_Iniciovigencia <= g.dat_viagem_guia)
    Or tp.dat_finalvigencia Is Null))

  From t_arr_guia g
  , t_arr_detalhe_guia d
  , t_arr_viagens_guia v
  , t_trf_tipopagto t
  , Frt_Cadveiculos cv
  , bgm_cadlinhas l
--  , t_trf_tarifa_tipopagto tp

 Where g.dat_viagem_guia = '06-feb-2019'
 And g.cod_seq_guia = d.cod_seq_guia
 And g.cod_empresa = 2
 And d.cod_seq_viagem = v.cod_seq_viagem
 And v.cod_seq_guia = g.cod_seq_guia
 And d.cod_tipopagtarifa = t.cod_tipopagto
 And cv.codigoveic = v.cod_veiculo
  And d.codintlinha = l.codintlinha
  And g.cod_seq_guia = 11828601
 
 Group By l.codigolinha, cv.prefixoveic, d.cod_tipopagtarifa, d.qtd_passag_trans, v.qtd_hora_ini, g.dat_viagem_guia
  , v.qtd_hora_final, g.cod_guia, t.cod_tipopagto_imp, t.nom_descricao, g.cod_seq_guia, v.cod_seq_viagem, d.codintlinha