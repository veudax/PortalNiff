Select LPad(CODIGOEMPRESA,'3',0) || '/' || LPad(codigofl,'3',0) EmpFil,
       codintfunc, codfunc, NOMEFUNC, 
       Gozoinifer, gozofinfer, aquiinifer,
       aquifinfer, DTNASCTOFUNC,
       nrdocto, 
       (valor * PercentualFaltasInjustificadas) + 
       (valor * PercentualQtdAcidentes) +
       (valor * PercentualQtdAtestado) +
       (valor * PercentualFaltaFeria) Plr,
       QtdInjustificada, QtdAcidentes, QtdAtestado, QtdFaltaFeria
  From (
Select 1300 valor,
       (Case When QtdInjustificada = 0 Then 0.25
             When QtdInjustificada <= 2 Then 0.125
             Else 0 End) PercentualFaltasInjustificadas,
             QtdInjustificada,
       (Case When QtdAcidentes = 0 Then 0.25
             When QtdAcidentes <= 2 Then 0.125
             When QtdAcidentes <= 3 Then 0.05
             Else 0 End) PercentualQtdAcidentes,
             QtdAcidentes,             
       (Case When QtdAtestado <= 4 Then 0.25
             When QtdAtestado <= 6 Then 0.125
             When QtdAtestado <= 8 Then 0.05
             Else 0 End) PercentualQtdAtestado,
             QtdAtestado,         
        (Case When QtdFaltaFeria = 0 Then 0.25
             When QtdFaltaFeria <= 2 Then 0.125
             When QtdFaltaFeria <= 3 Then 0.05
             Else 0 End) PercentualFaltaFeria,
        QtdFaltaFeria,
        codintfunc, codfunc, NOMEFUNC, 
        Gozoinifer, gozofinfer, aquiinifer,
        aquifinfer, DTNASCTOFUNC,
        nrdocto, CODIGOEMPRESA, codigofl       
  From (
Select  Sum(QtdInjustificada) QtdInjustificada,
        Sum(QtdAcidentes) QtdAcidentes,
        Sum(QtdAtestado) QtdAtestado,
        Sum(QtdFaltaFeria) QtdFaltaFeria,
        codintfunc, codfunc, NOMEFUNC, 
        Gozoinifer, gozofinfer, aquiinifer,
        aquifinfer, DTNASCTOFUNC,
        nrdocto, CODIGOEMPRESA, codigofl
  From (
-- Acidentes e Avarias com culpa 
Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, 'Acidentes/Avarias com culpa' descocorr,
       f.Gozoinifer, f.gozofinfer, f.aquiinifer,
       f.aquifinfer, fu.DTNASCTOFUNC,
       dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
       0 QtdInjustificada,
       1 QtdAcidentes,
       0 QtdAtestado,
       0 QtdFaltaFeria
  From Flp_Ferias f,
       Vw_Funcionarios Fu,
       Flp_Documentos dc,
       Frq_Digitacaomovimento d,
       frq_ocorrencia o
 Where Fu.Codintfunc = f.Codintfunc
   And f.gozofinfer Between '01-jun-2018' And '30-jun-2018'
   And Fu.Situacaofunc = 'A'
   And dc.codintfunc = fu.CODINTFUNC
   And dc.tipodocto = 'CPF'
   And fu.CODINTFUNC = d.codintfunc
   And trunc(d.dtdigit) Between '01-jan-2018' And f.aquifinfer
   And f.aquifinfer >= '01-jan-2018'
   And fu.CODIGOEMPRESA In (1,6,26)
   And fu.CODIGOFL In (1,2)
   And d.codocorr In (501,502,512,594)
   And f.statusferias = 'N'
   And d.tipodigit = 'F'
   And d.statusdigit = 'N'
   And o.Codocorr = d.codocorr           

 Union All
-- Injustificadas
Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, 'Faltas Injustificadas' descocorr,
       f.Gozoinifer, f.gozofinfer, f.aquiinifer,
       f.aquifinfer, fu.DTNASCTOFUNC,
       dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
       1 QtdInjustificada,
       0 QtdAcidentes,
       0 QtdAtestado,
       0 QtdFaltaFeria
  From Flp_Ferias f,
       Vw_Funcionarios Fu,
       Flp_Documentos dc,
       Frq_Digitacaomovimento d,
       frq_ocorrencia o
 Where Fu.Codintfunc = f.Codintfunc
   And f.gozofinfer Between '01-jun-2018' And '30-jun-2018'
   And Fu.Situacaofunc = 'A'
   And dc.codintfunc = fu.CODINTFUNC
   And dc.tipodocto = 'CPF'
   And fu.CODINTFUNC = d.codintfunc
   And trunc(d.dtdigit) Between '01-jan-2018' And f.aquifinfer
   And f.aquifinfer >= '01-jan-2018'
   And fu.CODIGOEMPRESA In (1,6,26)
   And fu.CODIGOFL In (1,2)
   And d.codocorr In (534,583)
   And f.statusferias = 'N'
   And d.tipodigit = 'F'
   And d.statusdigit = 'N'
   And o.Codocorr = d.codocorr    

 Union All
-- Atestados
Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, 'Atestado Médico' descocorr,
       f.Gozoinifer, f.gozofinfer, f.aquiinifer,
       f.aquifinfer, fu.DTNASCTOFUNC,
       dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
       0 QtdInjustificada,
       0 QtdAcidentes,
       1 QtdAtestado,
       0 QtdFaltaFeria
  From Flp_Ferias f,
       Vw_Funcionarios Fu,
       Flp_Documentos dc,
       Frq_Digitacaomovimento d,
       frq_ocorrencia o
 Where Fu.Codintfunc = f.Codintfunc
   And f.gozofinfer Between '01-jun-2018' And '30-jun-2018'
   And Fu.Situacaofunc = 'A'
   And dc.codintfunc = fu.CODINTFUNC
   And dc.tipodocto = 'CPF'
   And fu.CODINTFUNC = d.codintfunc
   And trunc(d.dtdigit) Between '01-jan-2018' And f.aquifinfer
   And f.aquifinfer >= '01-jan-2018'
   And fu.CODIGOEMPRESA In (1,6,26)
   And fu.CODIGOFL In (1,2)
   And d.codocorr In (506,507,592,593,903)
   And f.statusferias = 'N'
   And d.tipodigit = 'F'
   And d.statusdigit = 'N'
   And o.Codocorr = d.codocorr  

 Union All   
 -- falta de Féria
Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, 'falta de Féria',
       f.Gozoinifer, f.gozofinfer, f.aquiinifer,
       f.aquifinfer, fu.DTNASCTOFUNC,
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
   And a.flg_tipolanc = 'D'
Group By fu.codintfunc, fu.codfunc, fu.NOMEFUNC, 'falta de Féria',
       f.Gozoinifer, f.gozofinfer, f.aquiinifer,
       f.aquifinfer, Last_day(trunc(a.dat_lanc)), fu.DTNASCTOFUNC,
       dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl 
       
Union All
-- Todos Funcionários       
Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, 'todos', 
       f.Gozoinifer, f.gozofinfer, f.aquiinifer,
       f.aquifinfer, fu.DTNASCTOFUNC,
       dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
       0 QtdInjustificada,
       0 QtdAcidentes,
       0 QtdAtestado,
       0 QtdFaltaFeria
  From Flp_Ferias f,
       Vw_Funcionarios Fu,
       Flp_Documentos dc
 Where Fu.Codintfunc = f.Codintfunc
   And f.gozofinfer Between '01-jun-2018' And '30-jun-2018'
   And Fu.Situacaofunc = 'A'
   And dc.codintfunc = fu.CODINTFUNC
   And dc.tipodocto = 'CPF'
   And fu.CODIGOEMPRESA In (1,6,26)
   And fu.CODIGOFL In (1,2)
   And f.statusferias = 'N'

   )  
 Group By codintfunc, codfunc, NOMEFUNC,
        Gozoinifer, gozofinfer, aquiinifer,
        aquifinfer, DTNASCTOFUNC,
        nrdocto, CODIGOEMPRESA, codigofl ))
   