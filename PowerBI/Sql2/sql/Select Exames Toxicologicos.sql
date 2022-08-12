Select f.codfunc, t.* From flp_exame_toxicologico t, flp_funcionarios f
Where t.codigoempresa = 2
And f.codintfunc = t.codintfunc