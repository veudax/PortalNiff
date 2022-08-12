create or replace view pbi_descticketcampibus as
Select codintfunc, codfunc, NOMEFUNC, dtdigit Data,
       DTNASCTOFUNC, nrdocto, To_char(dtdigit,'mm/yyyy') mesano,
       Sum(QtdInjustificada) QtdInjustificada,
       (Sum(QtdInjustificada) * 22.68) DescontarInjutificadas,
       Sum(QtdJustificada) QtdJustificada,
       Sum(QtdSuspensao) QtdSuspensao,
--       Decode(Greatest(QtdJustificada, 4), 4, 0, QtdJustificada- 4) QtdJustificadasADescontar,
--       Decode(Greatest(QtdJustificada, 4), 4, 0, QtdJustificada- 4) * ValorJustificada DescontarJustificadas,
       LPad(CodigoEmpresa,3,'0') || '/' || Lpad(CodigoFl,3,'0') Empresa
From (

Select codintfunc, codfunc, NOMEFUNC,
       DTNASCTOFUNC, nrdocto, dtdigit,
       CodigoEmpresa, CodigoFl,
       Sum(QtdInjustificada) QtdInjustificada,
       Sum(QtdJustificada) QtdJustificada,
       Sum(QtdSuspensao) QtdSuspensao,
       0 ValorJustificada
  From (
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               1 QtdInjustificada,
               0 QtdJustificada,
               0 QtdSuspensao
          From PBI_vwFuncionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Situacaofunc = 'A'
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And fu.CODINTFUNC = d.codintfunc
           And trunc(d.dtdigit) Between '01-jul-2017' And Last_DAY(Sysdate)
           And fu.CODIGOEMPRESA In (9)
           And fu.CODIGOFL In (1,2)
           And d.codocorr In (583,561)
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
        Union All
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               0 QtdInjustificada,
               1 QtdJustificada,
               0 QtdSuspensao
          From PBI_vwFuncionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Situacaofunc = 'A'
           And fu.CODINTFUNC = d.codintfunc
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And trunc(d.dtdigit) Between '01-jul-2017' And LAST_DAY(Sysdate)
           And fu.CODIGOEMPRESA In (9)
           And fu.CODIGOFL In (1,2)
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And d.codocorr In (560)
         Union All
        Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               0 QtdInjustificada,
               0 QtdJustificada,
               1 QtdSuspensao
          From PBI_vwFuncionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Situacaofunc = 'A'
           And fu.CODINTFUNC = d.codintfunc
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And trunc(d.dtdigit) Between '01-jul-2017' And LAST_DAY(Sysdate)
           And fu.CODIGOEMPRESA In (9)
           And fu.CODIGOFL In (1,2)
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And d.codocorr In (553)
           )
        Group By codintfunc, codfunc, NOMEFUNC,
               CodigoEmpresa, CodigoFl,dtdigit,
               DTNASCTOFUNC, nrdocto )

  Group By codintfunc, codfunc, NOMEFUNC, dtdigit,
       DTNASCTOFUNC, nrdocto, To_char(dtdigit,'mm/yyyy'), LPad(CodigoEmpresa,3,'0') || '/' || Lpad(CodigoFl,3,'0')

