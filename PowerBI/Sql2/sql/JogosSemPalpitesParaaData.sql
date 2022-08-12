Select c.Nome
  From niff_ads_colaboradores c, niff_chm_usuarios u, Ctr_Cadastrodeusuarios b
 Where c.idcolaborador Not In (
                                Select p.idcolaborador
                                From niff_bol_jogos s, niff_bol_palpites p
                                Where trunc(s.data) = '14-jun-2018'
                                And s.Id = p.idjogo)
   And u.usuarioacesso = b.usuario
   And b.codintfunc = c.codintfunc   
   And u.participabolaocopa = 'S'      
   And U.ATIVO = 'S'                       
