Select c.Idchamado, u.nome
                       From Niff_Chm_Chamado c, niff_chm_usuarios u,
                           (Select Max(data) data, Idchamado
                             From niff_chm_histochamado 
                             Group By IdChamado )h
                      Where trunc(c.DataAvaliacaoDoSolicitante) > trunc(h.Data)+2
                        And c.Idchamado = h.idchamado
                        And c.status = 'F'
                        And u.Idusuario = c.Idusuario
                        And nvl(c.AvaliacaoSolicitante,0) > 0;

Update Niff_Chm_Chamado 
   Set AvaliouNoPrazoAtendente = 'N' 
 Where IdChamado In (Select c.Idchamado 
                       From Niff_Chm_Chamado c, 
                           (Select Max(data) data, Idchamado
                             From niff_chm_histochamado
                             Group By IdChamado )h
                      Where trunc(c.DataAvaliacaoDoSolicitante) > trunc(h.Data)+2
                        And c.Idchamado = h.idchamado
                        And c.status = 'F'
                        And nvl(c.AvaliacaoSolicitante,0) > 0);
                        
                        