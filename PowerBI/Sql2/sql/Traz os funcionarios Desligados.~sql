Select Distinct q.codintfunc,  u.ativo, u.nome, u.usuarioacesso, c.usuario
  From flp_quitacao q, flp_funcionarios f, Niff_Chm_Usuarios u, ctr_cadastrodeusuarios c
 Where q.dtdesligquita > '01-mar-2019'
   And f.codintfunc = q.codintfunc
   And u.codfunc = f.codintfunc
   And q.dtexclquita Is Null
   And c.codintfunc = f.codintfunc
   And c.ativo = 'N'
   And u.usuarioacesso = c.usuario
   And u.Ativo = 'S'