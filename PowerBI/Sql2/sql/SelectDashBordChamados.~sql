Create Or Replace View pbi_chamados As
Select g.descricao categoria, c.numero,
       Decode(c.status,'F','Finalizado','N','Aberto','E','Aguardando Analista','P','Aguardando Solicitante','D','Em Desenvolvimento','C','Cancelado','A','Aguardando Adequação','Reaberto') Status,
       t.nome tela, m.nome Modulo, u.nome Solicitante, 
       a.Nome Atendente, nvl(c.avaliacao,0)avaliacao , Nvl(c.avaliacaosolicitante,0) avaliacaoSolicitante
     , Count(*) Quantidade
     , tc.DataInicio, tc.DataFinal
     , trunc( tc.DataFinal - tc.DataInicio) DiasCorridos
     , trunc( tc.DataFinal - tc.DataInicio) - fc_niff_diasnaouteis(tc.DataInicio, tc.DataFinal) DiasLiquido
  From niff_chm_chamado c
     , niff_chm_categorias g
     , niff_chm_telas t
     , niff_chm_modulos m
     , niff_chm_usuarios u
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
 Group By g.descricao, c.status, t.nome, m.nome, u.nome, a.nome, c.avaliacao, c.avaliacaosolicitante
      , tc.DataInicio, tc.DataFinal, c.numero