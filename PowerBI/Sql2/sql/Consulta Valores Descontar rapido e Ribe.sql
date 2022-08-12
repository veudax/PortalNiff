select empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto, 
Sum(t.QtdInjustificada), Sum(t.DescontarInjutificadas) from pbi_descticketperiodoanterior t
Where t.Data Between '01-aug-2018' And '31-aug-2018'
And t.QtdInjustificada > 0
Group By empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto
