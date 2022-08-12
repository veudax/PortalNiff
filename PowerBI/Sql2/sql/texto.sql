select numero, t.descricaoavaliacao,  SUBSTR( u.nome, 1, INSTR( u.nome,' ')-1 ) || Substr( u.nome, INSTR( u.nome, ' ', -1)) Solicitante
     , SUBSTR( a.nome, 1, INSTR( a.nome,' ')-1 ) || Substr( a.nome, INSTR( a.nome, ' ', -1)) Atendente
  from niff_chm_chamado t, niff_chm_usuarios u
  , (Select Distinct h.idchamado, uh.Nome
          From niff_chm_histochamado h
             , niff_chm_usuarios uh
             , niff_chm_chamado cc
         Where h.Idusuario = uh.idusuario
           And uh.tipo = 'A'
           And h.Idchamado = cc.Idchamado
           And h.idusuario != cc.idusuario
           And h.privado = 'N'
           And h.idusuario = 326
           And (h.Data, h.idchamado) In (Select Max(h.Data), h.idchamado
                           From niff_chm_histochamado h
                              , niff_chm_chamado cc
                              , niff_chm_usuarios uh
                          Where h.Idusuario = uh.idusuario
                            And uh.tipo = 'A'
                            And h.Idchamado = cc.Idchamado
                            And h.idusuario != cc.idusuario
                            And h.idusuario = 326
                          Group By h.Idchamado   )
           ) a
Where avaliacao = 5
And Data Between '01-jan-2019' And '31-jul-2019'
And descricaoavaliacao Is Not Null And descricaoavaliacao Not Like ' passou%'
And u.Idusuario = t.idusuario
   And a.idchamado(+) = t.Idchamado