Create Or Replace View PBI_MotoristasDoFiscal As

Select m.Linha, m.Motoristas, f.fiscais, f.dtdigit Data, m.CodigoMotorista, f.CodigoFiscal 
From 
(Select l.codigolinha || ' - ' || l.nomelinha linha, -- Traz os Motoristas
       f.CODFUNC || ' - ' || f.NOMEFUNC Motoristas,
       dh.codigoveic, dh.servicodigit,
       l.tipolinha, d.dtdigit, dh.entradigit, dh.saidadigit,
       f.codintfunc CodigoMotorista
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
   And f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
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
       dh.codigoveic, dh.servicodigit,f.codintfunc,
       d.dtdigit, l.tipolinha, dh.entradigit, dh.saidadigit ) m,  
  
(Select l.codigolinha || ' - ' || l.nomelinha linha,  -- Traz os Fiscais
       f.CODFUNC || ' - ' || f.NOMEFUNC Fiscais,
       dh.codigoveic, dh.servicodigit,
       d.dtdigit, dh.entradigit, dh.saidadigit,
       f.codintfunc CodigoFiscal       
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
   And f.CODFUNCAO in (111,112,282,16,82,9,236) --fiscais
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
       d.dtdigit, dh.entradigit, dh.saidadigit, f.codintfunc ) F
       
Where m.TipoLinha = f.servicodigit
  And m.entradigit Between f.entradigit And f.saidadigit
  And m.saidadigit Between f.entradigit And f.saidadigit  
  And m.dtdigit = f.dtdigit
  
Order By Fiscais, Linha, motoristas, Data 