Update t_arr_detalhe_guia d
  Set d.codlanca = Null
 Where d.cod_seq_guia In (Select g.cod_seq_guia 
                            From t_arr_Guia g
                               , t_Arr_Detalhe_Guia d1
                           Where g.cod_seq_guia = d1.cod_seq_guia
                             And g.dat_prest_contas Between '01-jul-2017' And '31-jul-2017'
                             And d1.codlanca = 0
                             And g.cod_empresa = 3)
