Select * From t_Arr_Importacao_Bilheteria i, t_Arr_Guia g
Where g.cod_empresa = 3
And g.dat_prest_contas = '24-jul-2017'
And g.cod_localarr_agencia = 'A14'
And g.cod_dvguia = i.chave
