Select Empresa, NOMECOMPLETOFUNC, CODFUNC, CHAPAFUNC, DESCFUNCAO, Max(RG) Rg, Max(CPF) Cpf, max(carteira) Carteira, Max(Serie) serie
  From 
(
Select Decode(f.CODIGOEMPRESA, 1, Decode(codigofl,1, 'EOVG Dutra', 'EOVG Lavras'), 'VUG Dutra') Empresa,
       f.NOMECOMPLETOFUNC, f.CODFUNC, f.CHAPAFUNC, f.DESCFUNCAO, d.nrdocto RG, '' CPF, '' carteira, '' Serie
  From vw_funcionarios f
     , flp_documentos d
 Where f.CODINTFUNC = d.codintfunc        
   And codigoempresa || codigofl In (11)
   And f.CODAREA = 40
   And d.tipodocto = 'RG'
   And f.SITUACAOFUNC <> 'D'
 Union    
Select Decode(f.CODIGOEMPRESA, 1, Decode(codigofl,1, 'EOVG Dutra', 'EOVG Lavras'), 'VUG Dutra') Empresa,
       f.NOMECOMPLETOFUNC, f.CODFUNC, f.CHAPAFUNC, f.DESCFUNCAO, '' RG, d.nrdocto CPF, '' carteira, '' Serie
  From vw_funcionarios f
     , flp_documentos d
 Where f.CODINTFUNC = d.codintfunc        
   And codigoempresa || codigofl In (11)
   And f.CODAREA = 40
   And f.SITUACAOFUNC <> 'D'   
   And d.tipodocto = 'CPF'   
 Union    
Select Decode(f.CODIGOEMPRESA, 1, Decode(codigofl,1, 'EOVG Dutra', 'EOVG Lavras'), 'VUG Dutra') Empresa,
       f.NOMECOMPLETOFUNC, f.CODFUNC, f.CHAPAFUNC,f.DESCFUNCAO, '' RG, '' CPF, d.nrdocto carteira, d.ctpsseriedocto Serie
  From vw_funcionarios f
     , flp_documentos d
 Where f.CODINTFUNC = d.codintfunc        
   And codigoempresa || codigofl In (11)
   And f.CODAREA = 40
   And f.SITUACAOFUNC <> 'D'   
   And d.tipodocto = 'CTPS'      
) Where codfunc In ('000668','006443')
 Group By Empresa, NOMECOMPLETOFUNC, CODFUNC, CHAPAFUNC, DESCFUNCAO