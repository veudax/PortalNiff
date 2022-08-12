create or replace view pbi_descticketperiodoferias as
Select codintfunc, codfunc, NOMEFUNC,
       Gozoinifer, gozofinfer, aquiinifer,
       aquifinfer, DTNASCTOFUNC, nrdocto,
       QtdInjustificada,
       QtdInjustificada * ValorInjustificada DescontarInjutificadas,
       QtdJustificada,
       Decode(Greatest(QtdJustificada, 4), 4, 0, QtdJustificada- 4) QtdJustificadasADescontar,
       Decode(Greatest(QtdJustificada, 4), 4, 0, QtdJustificada- 4) * ValorJustificada DescontarJustificadas,
       LPad(CodigoEmpresa,3,'0') || '/' || Lpad(CodigoFl,3,'0') Empresa
From (

Select codintfunc, codfunc, NOMEFUNC,
       Gozoinifer, gozofinfer, aquiinifer,
       aquifinfer, DTNASCTOFUNC, nrdocto,
       CodigoEmpresa, CodigoFl,
       Sum(QtdInjustificada) QtdInjustificada,
       44 valorInjustificada,
       Sum(QtdJustificada) QtdJustificada,
       22 ValorJustificada
  From (
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               f.Gozoinifer, f.gozofinfer, f.aquiinifer,
               f.aquifinfer, d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               1 QtdInjustificada,
               0 QtdJustificada
          From Flp_Ferias f,
               Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Codintfunc = f.Codintfunc
           And f.Gozoinifer Between '01-jul-2017' And '31-dec-2017'
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
               0 QtdInjustificada,
               1 QtdJustificada
          From Flp_Ferias f,
               Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Codintfunc = f.Codintfunc
           And f.Gozoinifer Between '01-jul-2017' And '31-dec-2017'
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
           And d.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,506,507,521,526,527,535,560,579,580,584,592,593,902,903) )
        Group By codintfunc, codfunc, NOMEFUNC,
               CodigoEmpresa, CodigoFl,
               Gozoinifer, gozofinfer, aquiinifer,
               aquifinfer, DTNASCTOFUNC, nrdocto )

