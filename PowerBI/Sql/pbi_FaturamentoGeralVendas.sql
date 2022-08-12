create or replace view pbi_faturamentogeralvendas as
Select Last_day(g.dat_prest_contas) Data
          , Lpad(a.cod_empresa,3,'0') || '/' || lpad(a.codigofl,3,'0') EmpFil
          , To_char(g.dat_prest_contas,'mm/yyyy') MesAno
          , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB)) FaturamentoGeral
        From t_arr_Guia g, t_arr_detalhe_guia d, bgm_cadlinhas L, t_Trf_Tipopagto p, t_Trf_Agencia a
       Where g.cod_localarr_agencia = a.cod_agencia
         And a.cod_empresa = g.cod_empresa
         And a.codigofl = g.codigofl
         And d.cod_seq_guia = g.cod_seq_guia
         And l.codintlinha = d.codintlinha
         And p.cod_tipopagto = d.cod_tipopagtarifa
         And l.codigotplinha = 0
         And p.cod_tipopagto <> 'I'
         And g.cod_empresa || g.CodigoFl In (11,12,21,31,41,61,91,131,261,263)
         And g.dat_prest_contas BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -12) AND Last_day(trunc(sysdate))
       Group By Last_day(g.dat_prest_contas)
           , a.cod_empresa
           , a.codigofl
           , To_char(g.dat_prest_contas,'mm/yyyy')

