select empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto, 
Sum(t.QtdInjustificada), Sum(t.DescontarInjutificadas),
Sum(t.QtdJustificada), Sum(t.QtdSuspensao)
 from pbi_descticketcampibus t
Where t.Data Between '21-nov-2017' And '20-dec-2017'
  And t.Empresa = '009/002'
Group By empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto