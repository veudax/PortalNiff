Select u.Rowid, u.usuarioacesso, u.codfunc, ub.codintfunc, e.Idempresa, e.Nome, u.Idempresa
  From Niff_Chm_Usuarios u, ctr_cadastrodeusuarios ub
     , flp_funcionarios f, niff_chm_empresas e
 Where u.usuarioacesso = ub.usuario  
   And (u.codfunc Is Null Or u.Idempresa Is Null)
   And f.codintfunc = ub.codintfunc
   And e.codigoglobus = lpad(f.codigoempresa, 3, '0') || '/' || lPad(f.Codigofl, 3, '0') 
   