
Select LPad(CodigoEmpresa,3,'0') || '/' || Lpad(CodigoFl,3,'0') Empresa,
       codfunc, NOMEFUNC, DTNASCTOFUNC, nrdocto,
       Gozoinifer, gozofinfer, aquiinifer,
       aquifinfer, 
       QtdInjustReal,
       QtdInjustReal * ValorInjustificada DescontarInjutificadasReal,
       Decode(Greatest(QtdJustReal, 4), 4, 0, QtdJustReal- 4) QtdJustReal,
       Decode(Greatest(QtdJustReal, 4), 4, 0, QtdJustReal- 4) * ValorJustificada DescontarJustificadasReal,
       (QtdInjustReal * ValorInjustificada) + (Decode(Greatest(QtdJustReal, 4), 4, 0, QtdJustReal- 4) * ValorJustificada) Total
From (

Select codintfunc, codfunc, NOMEFUNC,
       Gozoinifer, gozofinfer, aquiinifer,
       aquifinfer, DTNASCTOFUNC, nrdocto,
       CodigoEmpresa, CodigoFl,
       Sum(QtdInjustificada) QtdInjustificada,
       Sum(QtdInjustReal) QtdInjustReal,
       48.32 valorInjustificada,
       Sum(QtdJustificada) QtdJustificada,
       Sum(QtdJustReal) QtdJustReal,
       24.16 ValorJustificada
  From (
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               f.Gozoinifer, f.gozofinfer, f.aquiinifer,
               f.aquifinfer, d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               (Case When d.dtdigit > '30-apr-2017' Then 1 Else 0 End) QtdInjustReal,
               1 QtdInjustificada,
               0 QtdJustReal,
               0 QtdJustificada
          From Flp_Ferias f,
               Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Codintfunc = f.Codintfunc
           And f.Gozoinifer Between '01-aug-2019' And '31-aug-2019'
           And Fu.Situacaofunc = 'A'
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And fu.CODINTFUNC = d.codintfunc
           And trunc(d.dtdigit) Between f.aquiinifer And f.aquifinfer
           And fu.CODIGOEMPRESA In (1,6,26)
           And fu.CODIGOFL In (1,2)
           And d.codocorr In (209,534,583)
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
               1 QtdJustificada               
          From Flp_Ferias f,
               Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Codintfunc = f.Codintfunc
           And f.Gozoinifer Between '01-aug-2019' And '31-aug-2019'
           And Fu.Situacaofunc = 'A'
           And fu.CODINTFUNC = d.codintfunc
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And trunc(d.dtdigit) Between f.aquiinifer And f.aquifinfer
           And fu.CODIGOEMPRESA In (1,6,26)
           And fu.CODIGOFL In (1,2)
           And f.statusferias = 'N'
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And d.codocorr In (2,3,10,28,117,120,173,222,230,233,322,344,374,506,507,521,526,527,535,560,579,580,584,592,593,601,602,902,903) )
        Group By codintfunc, codfunc, NOMEFUNC,
               CodigoEmpresa, CodigoFl,
               Gozoinifer, gozofinfer, aquiinifer,
               aquifinfer, DTNASCTOFUNC, nrdocto )
 Order By Empresa, GozoIniFer
