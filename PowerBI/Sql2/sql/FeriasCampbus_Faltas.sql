
Select LPad(CodigoEmpresa,3,'0') || '/' || Lpad(CodigoFl,3,'0') Empresa,
       codfunc, NOMEFUNC,
       DTNASCTOFUNC, nrdocto, 
       Gozoinifer, gozofinfer, aquiinifer, aquifinfer,
       Sum(QtdInjustificada) Injustificada,
       (Sum(QtdInjustificada) * 22.68) DescontarInjutificadas,
       Sum(QtdJustificada) QtdJustificada,
       Sum(QtdSuspensao) QtdSuspensao
From (

Select codintfunc, codfunc, NOMEFUNC,
       Gozoinifer, gozofinfer, aquiinifer,
       aquifinfer, DTNASCTOFUNC, nrdocto,
       CodigoEmpresa, CodigoFl,
       Sum(QtdInjustificada) QtdInjustificada,
       Sum(QtdInjustReal) QtdInjustReal,
       44 valorInjustificada,
       Sum(QtdJustificada) QtdJustificada,
       Sum(QtdJustReal) QtdJustReal,
       22 ValorJustificada,
       Sum(QtdSuspensao) QtdSuspensao
  From (
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               f.Gozoinifer, f.gozofinfer, f.aquiinifer,
               f.aquifinfer, d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               (Case When d.dtdigit > '30-apr-2017' Then 1 Else 0 End) QtdInjustReal,
               1 QtdInjustificada,
               0 QtdJustReal,
               0 QtdJustificada,
               0 QtdSuspensao
          From Flp_Ferias f,
               Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Codintfunc = f.Codintfunc
           And f.Gozoinifer Between '01-dec-2017' And '31-dec-2017'--Last_day(Sysdate+30)
           And Fu.Situacaofunc = 'A'
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And fu.CODINTFUNC = d.codintfunc
           And trunc(d.dtdigit) Between f.aquiinifer And f.aquifinfer
           And fu.CODIGOEMPRESA In (9)
           And fu.CODIGOFL In (1,2)
           And d.codocorr In (583,561)
           And f.statusferias = 'N'
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
        Union All
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               f.Gozoinifer, f.gozofinfer, f.aquiinifer,
               f.aquifinfer, d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               0 QtdInjustReal,
               0 QtdInjustificada,
               (Case When d.dtdigit > '30-apr-2017' Then 1 Else 0 End) QtdJustReal,
               1 QtdJustificada,
               0 QtdSuspensao             
          From Flp_Ferias f,
               Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Codintfunc = f.Codintfunc
           And f.Gozoinifer Between '01-dec-2017' And '31-dec-2017'--Last_day(Sysdate+30)
           And Fu.Situacaofunc = 'A'
           And fu.CODINTFUNC = d.codintfunc
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And trunc(d.dtdigit) Between f.aquiinifer And f.aquifinfer
           And fu.CODIGOEMPRESA In (9)
           And fu.CODIGOFL In (1,2)
           And f.statusferias = 'N'
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And d.codocorr In (560) 
         Union All
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               f.Gozoinifer, f.gozofinfer, f.aquiinifer,
               f.aquifinfer, d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               0 QtdInjustReal,
               0 QtdInjustificada,
               (Case When d.dtdigit > '30-apr-2017' Then 1 Else 0 End) QtdJustReal,
               0 QtdJustificada,
               1 QtdSuspensao
          From Flp_Ferias f,
               Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Codintfunc = f.Codintfunc
           And f.Gozoinifer Between '01-dec-2017' And '31-dec-2017'--Last_day(Sysdate+30)
           And Fu.Situacaofunc = 'A'
           And fu.CODINTFUNC = d.codintfunc
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And trunc(d.dtdigit) Between f.aquiinifer And f.aquifinfer
           And fu.CODIGOEMPRESA In (9)
           And fu.CODIGOFL In (1,2)
           And f.statusferias = 'N'
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And d.codocorr In (553)            )
        Group By codintfunc, codfunc, NOMEFUNC,
               CodigoEmpresa, CodigoFl,
               Gozoinifer, gozofinfer, aquiinifer,
               aquifinfer, DTNASCTOFUNC, nrdocto
             )
  Group By codintfunc, codfunc, NOMEFUNC, 
  Gozoinifer, gozofinfer, aquiinifer, aquifinfer,
       DTNASCTOFUNC, nrdocto, LPad(CodigoEmpresa,3,'0') || '/' || Lpad(CodigoFl,3,'0')
 Order By Empresa, GozoIniFer
