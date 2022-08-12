Create Or Replace View Pbi_FiscaisAtivos As
Select distinct f.CODINTFUNC, f.CODFUNC, f.nomefunc,
         lPad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0') empfil,
         f.CODFUNC || '-' || f.nomefunc Fiscal
    From vw_funcionarios f
       , Niff_PBI_InspetoresFiscais c
   Where c.codintfunc_fiscal = f.CODINTFUNC
     And f.SITUACAOFUNC <> 'D'

