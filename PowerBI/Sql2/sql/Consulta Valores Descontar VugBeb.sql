select empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto, 
Sum(t.QtdInjustificada), Sum(t.DescontarInjutificadas) from pbi_descticket_VugBeb t
Where t.Data Between '01-aug-2018' And '31-Aug-2018'
Group By empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto