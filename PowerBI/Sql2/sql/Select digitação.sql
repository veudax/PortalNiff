Select l.codigolinha || ' - ' || l.nomelinha linha, 
       f.CODFUNC,
       DH.SERVICODIGIT,
       dh.codigoveic,
       To_Date( '01/' ||To_Char( d.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(d.normaldigit) normaldigit,
       Sum(trunc((( (trunc(d.normaldigit) * 60 ) + ( ((d.normaldigit) - trunc(d.normaldigit) )*100) )),2)) m_real
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
--   And f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
--   And f.CODFUNCAO in (111,112,282,16,82,9,236) --fiscais
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
--   And f.CODFUNC = '000033'
   
Group By l.codigolinha, l.nomelinha, 
       dh.codigoveic, 
       To_Date( '01/' ||To_Char( d.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy'),
       DH.SERVICODIGIT,
       f.CODFUNC

Order By codfunc, l.codigolinha, dtdigit   