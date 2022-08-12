Select a.competacumu, f.CODFUNC, f.NOMEFUNC, f.DescArea,
Count(CodEvento) Quantidade
From frq_acumulado a, Vw_Funcionarios f
Where codevento = 375
  And f.codintfunc= a.codintfunc
  And f.codigoEmpresa = &empresa
  And f.CodigoFl = &filial
  And a.competacumu Between &dataIni And &dataFim
Group By a.competacumu, f.CODFUNC, f.NOMEFUNC, f.DescArea