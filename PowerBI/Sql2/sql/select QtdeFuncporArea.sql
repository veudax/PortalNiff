Select Count(Distinct CodIntFunc), codArea--, CODFUNC
 From (
Select  fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
      , f.CODINTFUNC, f.CODFUNC
  From Vw_Funcionarios f, Flp_Fichafinanceira Ff
 Where f.Codigoempresa || f.Codigofl In 261
   --    (11, 12, 21, 31, 41, 51, 61, 92, 131, 261, 263)
   And Ff.Competficha Between '01-mar-2019' And '31-mar-2019'      
   And Ff.Codintfunc = f.Codintfunc
   And Ff.Situacaoffinan = 'A'
--   And f.Situacaonacompet = 'A'
   And (Ff.Tipofolha = 1)
Group By f.codintfunc, f.CODFUNC, to_number(To_char(ff.competficha,'yyyymm'))
)
--Where codarea = 20
Group By codArea--, CODFUNC