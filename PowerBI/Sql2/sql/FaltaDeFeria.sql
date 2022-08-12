Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, 'falta de Féria',
       Sum(a.vlr_documento) Valor, a.flg_tipolanc,
       f.Gozoinifer, f.gozofinfer, f.aquiinifer,
       f.aquifinfer, last_day(trunc(a.dat_lanc)) dtdigit, fu.DTNASCTOFUNC,
       dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
       0 QtdInjustificada,
       0 QtdAcidentes,
       0 QtdAtestado,
       1 QtdFaltaFeria
  From T_ARR_LANC_FUNC A,
       Flp_Ferias f,
       Vw_Funcionarios Fu,
       Flp_Documentos dc
 Where Fu.Codintfunc = f.Codintfunc
   And f.gozofinfer Between '01-jun-2018' And '30-jun-2018'
   And Fu.Situacaofunc = 'A'
   And dc.codintfunc = fu.CODINTFUNC
   And dc.tipodocto = 'CPF'
   And fu.CODINTFUNC = a.cod_funcionario
   And trunc(a.dat_lanc) Between '01-jan-2018' And f.aquifinfer
   And f.aquifinfer >= '01-jan-2018'
   And fu.CODIGOEMPRESA In (1,6,26)
   And fu.CODIGOFL In (1,2)
   And f.statusferias = 'N'
Group By 
fu.codintfunc, fu.codfunc, fu.NOMEFUNC, 'falta de Féria',
a.flg_tipolanc,
       f.Gozoinifer, f.gozofinfer, f.aquiinifer,
       f.aquifinfer, Last_day(trunc(a.dat_lanc)), fu.DTNASCTOFUNC,
       dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl