create or replace view pbi_descticketperiodoanterior as
Select codintfunc, codfunc, NOMEFUNC, dtdigit Data,
       DTNASCTOFUNC, nrdocto, To_char(dtdigit,'mm/yyyy') mesano,
       Sum(QtdInjustificada) QtdInjustificada,
       (Sum(QtdInjustificada) * 22.68) DescontarInjutificadas,
       Sum(QtdJustificada) QtdJustificada,
       Sum(QtdSuspensao) QtdSuspensao,
       Sum(QtdLicenca) QtdLicenca,
       Situacaofunc,
--       Decode(Greatest(QtdJustificada, 4), 4, 0, QtdJustificada- 4) QtdJustificadasADescontar,
--       Decode(Greatest(QtdJustificada, 4), 4, 0, QtdJustificada- 4) * ValorJustificada DescontarJustificadas,
       LPad(CodigoEmpresa,3,'0') || '/' || Lpad(CodigoFl,3,'0') Empresa
From (

Select codintfunc, codfunc, NOMEFUNC,
       DTNASCTOFUNC, nrdocto, dtdigit,
       CodigoEmpresa, Decode(codigoEmpresa, 9, 1, CodigoFl) CodigoFl,
       Sum(QtdInjustificada) QtdInjustificada,
       Sum(QtdJustificada) QtdJustificada,
       Sum(QtdSuspensao) QtdSuspensao,
       Sum(QtdLicenca) QtdLicenca,
       0 ValorJustificada,
       Situacaofunc
  From (
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               1 QtdInjustificada,
               0 QtdJustificada,
               0 QtdSuspensao,
               0 QtdLicenca,
               Fu.Situacaofunc
          From PBI_vwFuncionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And fu.CODINTFUNC = d.codintfunc
           And trunc(d.dtdigit) Between '01-jun-2017' And Trunc(Sysdate)
           And fu.CODIGOEMPRESA In (1,2,3,4,5,6,9,13,26)
           And fu.CODIGOFL In (1,2)
           And d.codocorr In (209,534,583)
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
        Union All
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               0 QtdInjustificada,
               1 QtdJustificada,
               0 QtdSuspensao,
               0 QtdLicenca ,
               Fu.Situacaofunc
          From PBI_vwFuncionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where fu.CODINTFUNC = d.codintfunc
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And trunc(d.dtdigit) Between '01-jun-2017' And Trunc(Sysdate)
           And fu.CODIGOEMPRESA In (1,2,3,4,5,6,9,13,26)
           And fu.CODIGOFL In (1,2)
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And d.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,506,507,521,526,527,535,560,579,580,584,592,593,601,602,902,903)
         Union All
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               0 QtdInjustificada,
               0 QtdJustificada,
               1 QtdSuspensao,
               0 QtdLicenca ,
               Fu.Situacaofunc
          From PBI_vwFuncionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where fu.CODINTFUNC = d.codintfunc
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And trunc(d.dtdigit) Between '01-jun-2017' And Trunc(Sysdate)
           And fu.CODIGOEMPRESA In (1,2,3,4,5,6,9,13,26)
           And fu.CODIGOFL In (1,2)
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And d.codocorr In (599,552,553)
         Union All
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               0 QtdInjustificada,
               0 QtdJustificada,
               0 QtdSuspensao,
               1 QtdLicenca ,
               Fu.Situacaofunc
          From PBI_vwFuncionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where fu.CODINTFUNC = d.codintfunc
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And trunc(d.dtdigit) Between '01-jun-2017' And Trunc(Sysdate)
           And fu.CODIGOEMPRESA In (1,2,3,4,5,6,9,13,26)
           And fu.CODIGOFL In (1,2)
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And d.codocorr In (561)
           )
        Group By codintfunc, codfunc, NOMEFUNC,
               CodigoEmpresa, CodigoFl,dtdigit,
               DTNASCTOFUNC, nrdocto, Situacaofunc )

  Group By codintfunc, codfunc, NOMEFUNC, dtdigit, Situacaofunc,
       DTNASCTOFUNC, nrdocto, To_char(dtdigit,'mm/yyyy'), LPad(CodigoEmpresa,3,'0') || '/' || Lpad(CodigoFl,3,'0')

