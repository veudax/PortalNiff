Select codfunc, NOMEFUNC,
       DTNASCTOFUNC, nrdocto, 
       Sum(QtdInjustificada) QtdInjustificada,
       Sum(DescontarInjutificadas) DescontarInjutificadas,
       Sum(QtdJustificada) QtdJustificada,
       Empresa
From pbi_descticketperiodoanterior d
Where d.dtdigit Between '21-jul-2017' And '20-aug-2017'
Group By codintfunc, codfunc, NOMEFUNC,
       DTNASCTOFUNC, nrdocto, empresa