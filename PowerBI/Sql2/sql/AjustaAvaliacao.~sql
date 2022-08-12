Select 'update niff_chm_usuarios set DataAdmissao = ''' ||  f.dtadmfunc || ''' where idUsuario = ' || u.Idusuario || ';'
  From niff_chm_usuarios u, ctr_cadastrodeusuarios b, flp_funcionarios f
 Where u.usuarioacesso = b.usuario
   And b.codintfunc = f.codintfunc