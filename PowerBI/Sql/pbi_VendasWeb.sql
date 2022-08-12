create or replace view pbi_vendasweb as
Select Last_day(g.dat_prest_contas) Data
     , a.cod_agencia
     , a.Nom_Agencia
     , r.Descexterno
     , Lpad(a.cod_empresa,3,'0') || '/' || lpad(a.codigofl,3,'0') EmpFil
     , To_char(g.dat_prest_contas,'mm/yyyy') MesAno
     , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB)) FaturamentoWeb

     , Decode(To_Char(Last_day(g.dat_prest_contas),'yyyy'), To_Char(Last_day(Sysdate),'yyyy'), Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB))/ To_char(Sysdate,'mm'),2), 0) MediaFatAnoAtual
     , Decode(To_Char(Last_day(g.dat_prest_contas),'yyyy'), To_Char(Last_day(Sysdate),'yyyy')-1, Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB))/12,2), 0) MediaFatAnoAnterior
     , Decode(To_Char(Last_day(g.dat_prest_contas),'yyyy'), To_Char(Last_day(Sysdate),'yyyy')-2, Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB))/12,2), 0) MediaFatAno2Anterior

  From t_arr_Guia g, t_arr_detalhe_guia d, bgm_cadlinhas L, t_Trf_Tipopagto p, t_Trf_Agencia a,
    ( Select Distinct d.descexterno, d.codempresaglobus Empresa, d.codfilialglobus Filial, d.codglobus
        From t_arr_relacionamento_detalhe d, t_Arr_Relacionamento r, t_Trf_Agencia a
       Where r.tiporelacionamento = d.tiporelacionamento
         And r.codrelacionamento = d.codrelacionamento
         And a.cod_agencia = d.codglobus
         And a.cod_empresa = d.codempresaglobus
         And a.codigofl = d.codfilialglobus
         And r.CODRELACIONAMENTO = 1
         And a.flg_status = 'O' ) R
 Where a.flg_status = 'O'
   And g.cod_localarr_agencia = a.cod_agencia
   And a.cod_empresa = g.cod_empresa
   And a.codigofl = g.codigofl
   And d.cod_seq_guia = g.cod_seq_guia
   And l.codintlinha = d.codintlinha
   And p.cod_tipopagto = d.cod_tipopagtarifa
   And l.codigotplinha = 0
   And g.dat_prest_contas BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -12) AND Last_day(trunc(sysdate))
   And g.cod_empresa In (3)
   And p.cod_tipopagto <> 'I'
--   || g.CodigoFl In (11,12,21,31,41,61,91,131,261,263)
   And g.cod_Empresa = r.Empresa
   And g.CodigoFl = r.Filial
   And a.cod_agencia = r.Codglobus
 Group By Last_day(g.dat_prest_contas)
     , a.cod_agencia
     , a.Nom_Agencia
     , Lpad(a.cod_empresa,3,'0') || '/' || lpad(a.codigofl,3,'0')
     , r.Descexterno
     , To_char(g.dat_prest_contas,'mm/yyyy')

Union All

Select Last_day(g.dat_prest_contas) Data
     , a.cod_agencia
     , a.Nom_Agencia
     , r.Descexterno
     , Lpad(a.cod_empresa,3,'0') || '/' || lpad(a.codigofl,3,'0') EmpFil
     , To_char(g.dat_prest_contas,'mm/yyyy') MesAno
     , SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB)) FaturamentoWeb

     , Decode(To_Char(Last_day(g.dat_prest_contas),'yyyy'), To_Char(Last_day(Sysdate),'yyyy'), Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB))/ To_char(Sysdate,'mm'),2), 0) MediaFatAnoAtual
     , Decode(To_Char(Last_day(g.dat_prest_contas),'yyyy'), To_Char(Last_day(Sysdate),'yyyy')-1, Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB))/12,2), 0) MediaFatAnoAnterior
     , Decode(To_Char(Last_day(g.dat_prest_contas),'yyyy'), To_Char(Last_day(Sysdate),'yyyy')-2, Round(SUM(Decode(P.COD_TIPOPAGTO, '403',0, Decode(P.COD_TIPOPAGTO, 'X', -1, '956', -1,1) * d.VLR_RECEB))/12,2), 0) MediaFatAno2Anterior

  From t_arr_Guia g, t_arr_detalhe_guia d, bgm_cadlinhas L, t_Trf_Tipopagto p, t_Trf_Agencia a,
    ( Select Distinct d.descexterno, d.codempresaglobus Empresa, d.codfilialglobus Filial, d.codglobus
        From t_arr_relacionamento_detalhe d, t_Arr_Relacionamento r, t_Trf_Agencia a
       Where r.tiporelacionamento = d.tiporelacionamento
         And r.codrelacionamento = d.codrelacionamento
         And a.cod_agencia = d.codglobus
         And a.cod_empresa = d.codempresaglobus
         And a.codigofl = d.codfilialglobus
         And r.CODRELACIONAMENTO = 2
         And a.flg_status = 'O' ) R
 Where a.flg_status = 'O'
   And g.cod_localarr_agencia = a.cod_agencia
   And a.cod_empresa = g.cod_empresa
   And a.codigofl = g.codigofl
   And d.cod_seq_guia = g.cod_seq_guia
   And l.codintlinha = d.codintlinha
   And p.cod_tipopagto = d.cod_tipopagtarifa
   And l.codigotplinha = 0
   And g.dat_prest_contas BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -12) AND Last_day(trunc(sysdate))
   And g.cod_empresa In (4)
   And p.cod_tipopagto <> 'I'
--   || g.CodigoFl In (11,12,21,31,41,61,91,131,261,263)
   And g.cod_Empresa = r.Empresa
   And g.CodigoFl = r.Filial
   And a.cod_agencia = r.Codglobus
 Group By Last_day(g.dat_prest_contas)
     , a.cod_agencia
     , a.Nom_Agencia
     , Lpad(a.cod_empresa,3,'0') || '/' || lpad(a.codigofl,3,'0')
     , r.Descexterno
     , To_char(g.dat_prest_contas,'mm/yyyy')

