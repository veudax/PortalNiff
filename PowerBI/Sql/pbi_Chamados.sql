create or replace view pbi_chamados as
Select g.descricao categoria, c.numero, d.descricao departamento,
       Decode(c.status,'F','Finalizado','N','Aberto','E','Aguardando Analista','P','Aguardando Solicitante',
                       'D','Em Desenvolvimento','C','Cancelado','A','Aguardando Terceiros','U', 'Aguardando Autorização', 'G', 'Aguardando Cronograma', 'S', 'Aguardando Conserto', 'Reaberto') Status
     , t.nome tela, m.nome Modulo
     , SUBSTR( u.nome, 1, INSTR( u.nome,' ')-1 ) || Substr( u.nome, INSTR( u.nome, ' ', -1)) Solicitante
     , SUBSTR( a.nome, 1, INSTR( a.nome,' ')-1 ) || Substr( a.nome, INSTR( a.nome, ' ', -1)) Atendente
     , nvl(c.avaliacao,0) avaliacao , Nvl(c.avaliacaosolicitante,0) avaliacaoSolicitante
     , decode(c.status, 'F', Avg(c.Avaliacao), Null) mediaAtendente
     , decode(c.status, 'F', Avg(c.avaliacaosolicitante), Null) mediaSolicitante
     , 1 Quantidade
     , tc.DataInicio, tc.DataFinal
     , trunc(c.Data) Data
     , To_char(tc.DataInicio,'mm/yyyy') MesAno
     , To_char(tc.DataInicio,'yyyymm') AnoMes
     , To_char(tc.DataInicio,'yyyy') Ano
     , To_char(tc.DataFinal,'mm/yyyy') MesAnoFim
     , To_char(tc.DataFinal,'yyyymm') AnoMesFim
     , To_char(tc.DataFinal,'yyyy') AnoFim
     , trunc( tc.DataFinal - tc.DataInicio) DiasCorridos
     , trunc( tc.DataFinal - tc.DataInicio) - fc_niff_diasnaouteis(tc.DataInicio, tc.DataFinal) DiasLiquido
     , Decode(c.prioridade,'C','Crítica','A','Alta','M','Média','Baixa') Prioridade
     , Decode(c.tipochamado,'I','Implementação', 'E', 'Erro', 'D','Duvida','A','Acesso','J','Ajustes','S','Solicitação','Projeto') Tipo
     , e.nomeabreviado Empresa
     , eu.nomeabreviado EmpresaSolicitante
     , Sum(Decode(c.status,'F',1,0)) Encerrados
     , Sum(decode(c.problemaresolvido,'S',1,0)) QuantidadeAnalistaProblemaOK
     , Sum(decode(c.dentrodoprazo,'S',1,0)) QuantidadeAnalistaNoPrazo
     , Sum(decode(c.atendentefoicortez,'S',1,0)) QuantidadeAnalistaCortez
     , Sum(Decode(c.status,'F',decode(c.AvaliouNoPrazoAtendente,'S',1,0),0)) QtdeAvaliouNoPrazoAtendente
     , Sum(decode(c.solicitanteabriucorretamente,'S',1,0)) QuantidadeAbertosOk
     , Sum(decode(c.solicitanterespdentrodeprazo,'S',1,0)) QuantidadeSolicitanteNoPrazo
     , Sum(decode(c.solicitantefoicortez,'S',1,0)) QuantidadeSocilitanteCortez
     , Sum(Decode(c.status,'F',Decode(c.AvaliouNoPrazoSolicitante,'S',1,0),0)) QuantidadeAvaliouNoPrazo
     , Sum(Decode(c.status,'F',decode(c.avaliacao,Null,1, 0, 1, 0),0)) AnalistaNaoAvaliado
     , Sum(Decode(c.status,'F',decode(c.avaliacaosolicitante,Null,1, 0, 1, 0),0)) SolicitanteNaoAvaliado
     , Max(fc_niff_TempoAnalistaCorridos(c.Idchamado)) DiasCorridoAtendente
     , Max(fc_niff_TempoAnalistaLiquido(c.Idchamado)) DiasLiquidoAtendente
     , Max(fc_niff_TempoAnalistaLiquido2(c.Idchamado)) DiasLiquido2Atendente
     , Max(fc_niff_TempoSolicCorridos(c.Idchamado)) DiasCorridoSolicitante
     , Max(fc_niff_TempoSolicLiquido(c.Idchamado)) DiasLiquidoSolicitante
     , Max(fc_niff_TempoSolicLiquido2(c.Idchamado)) DiasLiquido2Solicitante
     , decode(status, 'P', 0, 'A' , 0, 'F', 0, 'C', 0, (Case When Max(SLA) <= (tc.DataFinal) Then
                                                               Case When (tc.DataFinal+Decode(c.prazodesenvolvimento,0,2,c.prazodesenvolvimento)) < Sysdate Then 1 Else 0 End
                                                             When MAx(SLA) < Sysdate Then 1 Else 0 End )) ChamadosAtrasadosAtendente
     , decode(status, 'P', (Case When Max(SLA) <= (tc.DataFinal) Then Case When (tc.DataFinal+Decode(c.prazodesenvolvimento,0,2,c.prazodesenvolvimento)) < Sysdate Then 1 Else 0 End
                                 When MAx(SLA) < Sysdate Then 1 Else 0 End ), 0) ChamadosAtrasadosSolicitante
     , decode(status, 'C', 0, (Case When Max(SLA) <= (tc.DataFinal) Then Case When (tc.DataFinal+Decode(c.prazodesenvolvimento,0,2,c.prazodesenvolvimento)) < Sysdate Then 1 Else 0 End
                                    When MAx(SLA) < Sysdate Then 1 Else 0 End )) ChamadosAtrasados
     , decode(status, 'F', (Case When Max(SLA) <= (tc.DataFinal) Then Case When (tc.DataFinal+Decode(c.prazodesenvolvimento,0,2,c.prazodesenvolvimento)) < Sysdate Then 1 Else 0 End
                                 When MAx(SLA) < DataFinal Then 1 Else 0 End ),0) ChamadosFinalizadoAtrasados
     , Sum(Decode(c.status,'F',decode(To_char(tc.DataFinal,'mmyyyy'),To_char(tc.DataInicio,'mmyyyy'),0,1),0)) QuantidadeEncerradosForaMes
     , Sum(r.Quantidade) QtdeReaberto
  From niff_chm_chamado c
     , niff_chm_categorias g
     , niff_chm_telas t
     , niff_chm_modulos m
     , niff_chm_usuarios u
     , niff_chm_empresas e
     , niff_chm_empresas eu
     , Niff_Chm_Departamento d
     , (Select h.idchamado, trunc(h.Data) Data, 1 Quantidade
          From niff_chm_histochamado h
         Where h.status = 'R') r
     , (Select Distinct h.idchamado, uh.Nome
          From niff_chm_histochamado h
             , niff_chm_usuarios uh
             , niff_chm_chamado cc
         Where h.Idusuario = uh.idusuario
           And (uh.tipo = 'A' Or h.tipousuario = 'A')
           And h.Idchamado = cc.Idchamado
           --And h.idusuario != cc.idusuario
           And h.privado = 'N'
           And (h.Data, h.idchamado) In (Select Max(h.Data), h.idchamado
                           From niff_chm_histochamado h
                              , niff_chm_chamado cc
                              , niff_chm_usuarios uh
                          Where h.Idusuario = uh.idusuario
                            And (uh.tipo = 'A' Or h.tipousuario = 'A')
                            And h.Idchamado = cc.Idchamado
                            --And h.idusuario != cc.idusuario
                            And h.privado = 'N'
                          Group By h.Idchamado   )
           ) a
     , (Select IdChamado, Max(data) DataFinal, Min(Data) DataInicio, Max(SLA) SLA
          From niff_chm_histochamado
         Group By IdChamado ) Tc
 Where g.Idcategoria = c.Idcategoria
   And t.Idtela(+) = c.Idtela
   And m.Idmodulo(+) = t.idmodulo
   And u.idusuario = c.Idusuario
   And a.idchamado(+) = c.Idchamado
   And tc.idChamado = c.IdChamado
   And c.Idempresa = e.Idempresa
   And eu.idempresa(+) = c.IdEmpresaSolicitante
--   And c.idusuarioagrupou Is Null
   And d.Iddepartamento = u.Iddepartamento
   And c.idchamado = r.IdChamado(+)

 Group By g.descricao, c.status, t.nome, m.nome, u.nome, a.nome, c.avaliacao, c.avaliacaosolicitante
      , tc.DataInicio, tc.DataFinal, c.numero, c.prioridade,c.tipochamado
      , e.nomeabreviado,      c.Data, c.prazodesenvolvimento, d.descricao
      , eu.nomeabreviado

