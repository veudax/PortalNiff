Select u.usuarioacesso, f.Nomefunc, u.nome, e.nomeabreviado
From Niff_Chm_Usuarios u, flp_funcionarios f, niff_chm_empresas e
Where f.codintfunc = u.codfunc
And f.situacaofunc = 'D'
And u.ativo = 'S'
And e.Idempresa = u.idempresa