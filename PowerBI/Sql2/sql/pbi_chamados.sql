create or replace view pbi_chamados as
Select g.descricao categoria, c.numero,
       Decode(c.status,'F','Finalizado','N','Aberto','E','Aguardando Analista','P','Aguardando Solicitante',
                       'D','Em Desenvolvimento','C','Cancelado','A','Aguardando Terceiros','Reaberto') Status,
       t.nome tela, m.nome Modulo, u.nome Solicitante,
       a.Nome Atendente, nvl(c.avaliacao,0) avaliacao , Nvl(c.avaliacaosolicitante,0) avaliacaoSolicitante
     , decode(c.status, 'F', Avg(c.Avaliacao), Null) mediaAtendente
     , decode(c.status, 'F', Avg(c.avaliacaosolicitante), Null) mediaSolicitante
     , Count(*) Quantidade
     , tc.DataInicio, tc.DataFinal
     , To_char(tc.DataInicio,'mm/yyyy') MesAno
     , To_char(tc.DataInicio,'yyyymm') AnoMes
     , To_char(tc.DataInicio,'yyyy') Ano
     , trunc( tc.DataFinal - tc.DataInicio) DiasCorridos
     , trunc( tc.DataFinal - tc.DataInicio) - fc_niff_diasnaouteis(tc.DataInicio, tc.DataFinal) DiasLiquido
     , Decode(c.prioridade,'C','Crítica','A','Alta','M','Média','Baixa') Prioridade
     , Decode(c.tipochamado,'I','Implementação', 'E', 'Erro', 'D','Duvida','A','Acesso','J','Ajustes','Projeto') Tipo
     , e.nomeabreviado Empresa
     , Sum(Decode(c.status,'F',1,0)) Encerrados
     , Sum(decode(c.problemaresolvido,'S',1,0)) QuantidadeAnalistaProblemaOK
     , Sum(decode(c.dentrodoprazo,'S',1,0)) QuantidadeAnalistaNoPrazo
     , Sum(decode(c.atendentefoicortez,'S',1,0)) QuantidadeAnalistaCortez
     , Sum(decode(c.solicitanteabriucorretamente,'S',1,0)) QuantidadeAbertosOk
     , Sum(decode(c.solicitanterespdentrodeprazo,'S',1,0)) QuantidadeSolicitanteNoPrazo
     , Sum(decode(c.solicitantefoicortez,'S',1,0)) QuantidadeSocilitanteCortez   
     , Sum(Decode(c.status,'F',decode(c.avaliacao,Null,1, 0, 1, 0),0)) AnalistaNaoAvaliado
     , Sum(Decode(c.status,'F',decode(c.avaliacaosolicitante,Null,1, 0, 1, 0),0)) SolicitanteNaoAvaliado     
  From niff_chm_chamado c
     , niff_chm_categorias g
     , niff_chm_telas t
     , niff_chm_modulos m
     , niff_chm_usuarios u
     , niff_chm_empresas e
     , (Select Distinct h.idchamado, uh.Nome
          From niff_chm_histochamado h
             , niff_chm_usuarios uh
         Where h.Idusuario = uh.idusuario
           And uh.tipo = 'A') a
     , (Select IdChamado, Max(data) DataFinal, Min(Data) DataInicio
          From niff_chm_histochamado
         Group By IdChamado ) Tc
 Where g.Idcategoria = c.Idcategoria
   And t.Idtela(+) = c.Idtela
   And m.Idmodulo(+) = t.idmodulo
   And u.idusuario = c.Idusuario
   And a.idchamado = c.Idchamado
   And tc.idChamado = c.IdChamado
   And c.Idempresa = e.Idempresa
 Group By g.descricao, c.status, t.nome, m.nome, u.nome, a.nome, c.avaliacao, c.avaliacaosolicitante
      , tc.DataInicio, tc.DataFinal, c.numero, c.prioridade,c.tipochamado
      , e.nomeabreviado

