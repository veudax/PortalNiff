Select f.codintfunc, ea.inscricaoempresa cnpj,
       f.CODFUNC, f.NOMEFUNC, formatacnpjcpf(cpf.nrdocto, 'CPF') cpf
     , decode(f.SEXOFUNC, 'F', 'Feminino','Masculino') Sexo
     , f.DESCESTCIV, f.DESCINSTR, f.DTADMFUNC, f.DTNASCTOFUNC
     , f.CODMUNIC, p.cod_esocial Pais, pn.cod_esocial paisNasc, f.CODIGOUF, f.DESCNAC
     , rc.descraca
     , dm.nomedepen NomeMae
     , (Select dp.nomedepen
          From flp_dependentes dp
         Where dp.codintfunc = f.codintfunc  
           And dp.codparen = 4
       )  nomePai
     , ct.nrdocto Ctps, ct.ctpsseriedocto, ct.codigouf
     , ric.nrdocto NRic, ric.emissordoctogeral, ric.dtdocto dtric
     , rg.nrdocto Nrg, rg.rgemissordocto, rg.dtdocto dtRg
     , pis.nrdocto Pis, pis.dtdocto dtPis
  From vw_funcionarios f, flp_documentos cpf
     , flp_dependentes dm, flp_documentos ct 
     , flp_documentos ric, flp_documentos rg
     , flp_documentos pis 
     , ctr_empautorizadas ea, ctr_filial fl
     , Flp_Racacor rc, CGS_PAIS pn, Dvs_Municipio mn, Dvs_Municipio m, Cgs_Pais p
 Where f.codigoempresa = 29  
   And (f.DTADMFUNC Between '01-jan-2020' And '31-aug-2020'
    Or f.DTTRANSFFUNC Between '01-jan-2020' And '31-aug-2020')
   And cpf.codintfunc = f.CODINTFUNC
   And cpf.tipodocto = 'CPF'
   And dm.codintfunc = f.CODINTFUNC
   And dm.codparen = 5
   And ct.codintfunc = f.CODINTFUNC
   And ct.tipodocto = 'CTPS'
   And ric.codintfunc = f.CODINTFUNC
   And ric.tipodocto = 'RIC'
   And rg.codintfunc = f.CODINTFUNC
   And rg.tipodocto = 'RG'
   And pis.codintfunc = f.CODINTFUNC
   And pis.tipodocto = 'PIS'
   And fl.codigoempresa = f.codigoempresa
   And fl.codigofl = f.codigofl
   And ea.codintempaut = fl.codintempaut
   And f.CODRACA = rc.codraca
   And mn.codmunic = f.CODMUNIC
   And p.sigla_pais = mn.sigla_pais
   And m.codmunic = f.CODMUNICNASCTO
   And pn.sigla_pais = m.sigla_pais