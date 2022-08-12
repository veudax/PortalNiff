create or replace view pbi_chamados_reabertos as
Select c.numero, c.Idchamado
     , SUBSTR( u.nome, 1, INSTR( u.nome,' ')-1 ) || Substr( u.nome, INSTR( u.nome, ' ', -1)) Solicitante
     , SUBSTR( a.nome, 1, INSTR( a.nome,' ')-1 ) || Substr( a.nome, INSTR( a.nome, ' ', -1)) Atendente
     , h.Data DataReabertura
     , c.Data dataAbertura
     , e.nomeabreviado Empresa
     , d.descricao departamento
     , 1 Quantidade
     , To_char(h.Data,'mm/yyyy') MesAno
     , To_char(h.Data,'yyyymm') AnoMes
     , To_char(h.Data,'yyyy') Ano
     , Decode(c.Status, 'F', tc.DataFinal, Null) DataFinal
     , Decode(c.status,'F','Finalizado','N','Aberto','E','Aguardando Analista','P','Aguardando Solicitante',
                       'D','Em Desenvolvimento','C','Cancelado','A','Aguardando Terceiros','U', 'Aguardando Autorização',
                       decode((Select status From niff_chm_histochamado h1 Where h1.Idchamado = h.Idchamado
                                                                             And h1.Data > h.data
                                                                             And h1.status = 'F'), 'F', 'Finalizado', 'Reaberto')) Status

  From niff_chm_chamado c
     , niff_chm_histochamado h
     , niff_chm_usuarios u
     , niff_chm_empresas e
     , Niff_Chm_Departamento d
     , (Select IdChamado, Max(data) DataFinal, Min(Data) DataInicio, Max(SLA) SLA
          From niff_chm_histochamado
         Group By IdChamado ) Tc
     , (Select Distinct h.idchamado, uh.Nome
          From niff_chm_histochamado h
             , niff_chm_usuarios uh
             , niff_chm_chamado cc
         Where h.Idusuario = uh.idusuario
           And uh.tipo = 'A'
           And h.Idchamado = cc.Idchamado
           --And h.idusuario != cc.idusuario
           And h.privado = 'N'
           And (h.Data, h.idchamado) In (Select Max(h.Data), h.idchamado
                           From niff_chm_histochamado h
                              , niff_chm_chamado cc
                              , niff_chm_usuarios uh
                          Where h.Idusuario = uh.idusuario
                            And uh.tipo = 'A'
                            And h.Idchamado = cc.Idchamado
                            --And h.idusuario != cc.idusuario
                          Group By h.Idchamado   )
           ) a

 Where c.Idchamado = h.Idchamado
   And h.status = 'R'
   And c.idusuario = u.Idusuario
   And a.idchamado(+) = c.Idchamado
   And d.Iddepartamento = u.Iddepartamento
   And c.Idempresa = e.Idempresa
   And tc.idChamado = c.IdChamado

