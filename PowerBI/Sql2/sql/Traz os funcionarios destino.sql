Select f.CODINTFUNC, d.nrdocto, f.codFunc, f.Nomefunc, f.codigoempresa, f.codigofl, f.dttransffunc, f.chapafunc,  f.dtadmfunc
 From vw_funcionarios f, flp_documentos d, eso_HISTORICO_gerar e
Where f.codigoempresa = 26
  And d.codintfunc = f.codintfunc
  And d.tipodocto = 'CPF'
  And f.dttransffunc Is Not Null
  And e.nm_tabela ='S2200' 
  And ST_ENVIO =4 
  and e.codigoempresa = 26
  And f.CODINTFUNC = e.codigo
Order By nrDocto  ;