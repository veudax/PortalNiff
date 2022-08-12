Select m.dataemissaomulta Emissao, m.numeroaimulta AutoDeInfracao, 
       m.codigoinfra || '-' || i.descricaoinfra Infracao, 
       m.datahoramulta DataInfracao, 
       v.prefixoveic, m.valormulta, f.codfunc || '-' || f.nomefunc Condutor,
       n.cod_motivo_notificacao || '-' || n.desc_motivo_notificacao Motivo,
       c.cod_area_competencia || '-' || a.desc_agente_autuador AreaCompetencia,
       r.cod_responsavel_notificacao || '-' || n.desc_motivo_notificacao Responsabilidade,
       a.cod_agente_autuador || '-' || a.desc_agente_autuador AgenteAtuador,
       l.Codigolinha || '-' || l.nomelinha Linha,
       m.Instanciapublicacaodo1 DataPublicacao01,
       m.Instanciapublicacaodo2 DataPublicacao02,
       condicaorecursomulta, condicaorecursomulta2, condicaorecursomulta3,
       Decode(trunc(m.datarecursomulta), To_date('30/12/1899','dd/mm/yyyy'), '', 'Recurso em ' || To_char(m.datarecursomulta,'dd/mm/yyyy')  || ' ')  ||
       Decode(m.condicaorecursomulta, 'D', 'Deferido',
                                      'I', 'Indeferido',
                                      'J', 'Segunda instância',
                                      'P', 'Defesa Prévia', 
                                      'E', 'Em julgamento', 
                                      'M', 'Parcimalmente deferido', '') Instancia01,  
       Decode(trunc(m.datarecursomulta2), To_date('30/12/1899','dd/mm/yyyy'), '', Null, '', 'Recurso em ' || To_char(m.datarecursomulta2,'dd/mm/yyyy')  || ' ')  ||
       Decode(m.condicaorecursomulta2, 'D', 'Deferido',
                                      'I', 'Indeferido',
                                      'J', 'Segunda instância',
                                      'P', 'Defesa Prévia', 
                                      'E', 'Em julgamento', 
                                      'M', 'Parcimalmente deferido', '') Instancia02,  
       Decode(trunc(m.datarecursomulta3), To_date('30/12/1899','dd/mm/yyyy'), '', Null, '', 'Recurso em ' || To_char(m.datarecursomulta3,'dd/mm/yyyy')  || ' ')  ||
       Decode(m.condicaorecursomulta3, 'D', 'Deferido',
                                      'I', 'Indeferido',
                                      'J', 'Segunda instância',
                                      'P', 'Defesa Prévia', 
                                      'E', 'Em julgamento', 
                                      'M', 'Parcimalmente deferido', '') Instancia03                                        
  From dvs_multa m, 
       frt_cadveiculos v, 
       flp_funcionarios f, 
       dvs_motivo_notificacao n,
       dvs_agente_autuador a,
       dvs_responsavel_notificacao r,
       dvs_area_competencia c,
       dvs_infracao I,
       Bgm_Cadlinhas L
 Where v.codigoveic = m.codigoveic
   And f.codintfunc = m.codintfunc
   And n.cod_motivo_notificacao(+) = m.cod_motivo_notificacao
   And a.cod_agente_autuador(+) = m.cod_area_competencia
   And r.cod_responsavel_notificacao(+) = m.cod_responsavel_notificacao
   And c.cod_area_competencia(+) = m.cod_area_competencia
   And i.codigoinfra(+) = m.codigoinfra
   And l.Codintlinha(+) = m.Codintlinha
   And Trunc(m.dataemissaomulta) Between '01-aug-2017' And '31-aug-2017'
   And v.codigoempresa = pEmpresa
   And v.codigofl = pFilial