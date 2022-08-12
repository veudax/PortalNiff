-- Traz os Inspetores
Select l.codigolinha || ' - ' || l.nomelinha linha, 
       f.CODFUNC || ' - ' || f.NOMEFUNC Inspetores,
       dh.codigoveic, dh.servicodigit, 
       d.dtdigit, dh.entradigit, dh.saidadigit
  From Frq_Digitacao d,
       frq_digitacaomovimento dh,
       vw_funcionarios f,
       flp_funcao c, 
       bgm_cadlinhas L
 Where d.Dtdigit Between '01-jan-2017' And '31-dec-2017'
   And d.dtdigit = dh.dtdigit(+)
   And d.tipodigit = dh.tipodigit(+)
   And d.codintfunc = dh.codintfunc(+)
   And f.CODINTFUNC = d.codintfunc
   And f.CODFUNCAO In (294,259,260,45,187,563,586) --inspetores
   And f.CODAREA = 40
   And f.CODFUNCAO = c.codfuncao
   and dh.iddigit = 1
   And dh.usuexcldigit is null
   and d.tipodigit = 'F'
   and dh.codocorr in (96,99,98)
   And f.CODIGOEMPRESA = 1 
   And f.CODIGOFL = 1
   And l.codintlinha = dh.codintlinha
   
Group By l.codigolinha || ' - ' || l.nomelinha, 
       f.CODFUNC || ' - ' || f.NOMEFUNC,
       dh.codigoveic, dh.servicodigit,
       d.dtdigit, dh.entradigit, dh.saidadigit
Order By Inspetores, linha