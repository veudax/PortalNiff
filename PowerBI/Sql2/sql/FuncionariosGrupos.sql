Select p.codgrupo, p.descgrupo, 
g.codintfunc,
f.codfunc, f.nomefunc, f.DESCAREA, f.DESCSETOR, f.DESCSECAO, f.DESCDEPTO
 From Flp_Funcionarios_Grupo g, vw_funcionarios f, Flp_Grupo p
Where g.Codintfunc(+) = f.CODINTFUNC
And p.codgrupo(+) = g.codgrupo
And f.CODIGOEMPRESA = 3
And f.SITUACAOFUNC <> 'D'
Order By CodintFunc Desc, CodFunc, DescGrupo