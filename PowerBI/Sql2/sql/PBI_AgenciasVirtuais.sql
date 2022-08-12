Create Or Replace View PBI_AgenciasVirtuais As
Select Distinct d.descexterno
  From t_arr_relacionamento_detalhe d, t_Arr_Relacionamento r, t_Trf_Agencia a
 Where r.tiporelacionamento = d.tiporelacionamento
   And r.codrelacionamento = d.codrelacionamento
   And a.cod_agencia = d.codglobus 
   And a.cod_empresa = d.codempresaglobus
   And a.codigofl = d.codfilialglobus
   And a.flg_status = 'O' 