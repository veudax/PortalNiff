Create Or Replace View pbi_FaltaInjustificadas As
select empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto, Data,
       Sum(t.QtdInjustificada) QtdInjustificada, Sum(t.DescontarInjutificadas) DescontarInjutificadas,
       Sum(t.QtdJustificada) QtdJustificada, 0 DescontarJustificadas,
       Sum(t.QtdSuspensao) QtdSuspensao, 0 DescontarSuspensao
  from pbi_descticketperiodoanterior t
 Group By empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto, Data
 Union All
select empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto, Data,
       Sum(t.QtdInjustificada) QtdInjustificada, Sum(t.DescontarInjutificadas) DescontarInjutificadas,
       Sum(t.QtdJustificada) QtdJustificada, 0 DescontarJustificadas,
       Sum(t.QtdSuspensao) QtdSuspensao, 0 DescontarSuspensao
  from pbi_descticket_VugBeb t
 Group By empresa, codfunc, nomefunc, DtNasctoFunc, NrDocto, Data  

