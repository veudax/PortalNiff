Select codintfunc, codfunc, NOMEFUNC,
               Gozoinifer, gozofinfer, aquiinifer,
               aquifinfer, nrdocto,
       Sum(QtdInjustReal) Qtd
  From (              
Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
               f.Gozoinifer, f.gozofinfer, f.aquiinifer,
               f.aquifinfer, d.dtdigit, fu.DTNASCTOFUNC,
               dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               (Case When d.dtdigit > '30-apr-2017' Then 1 Else 0 End) QtdInjustReal
          From Flp_Ferias f,
               Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d
         Where Fu.Codintfunc = f.Codintfunc
           And f.Gozoinifer Between '01-aug-2019' And '31-aug-2019'--Last_day(Sysdate+30)
           And Fu.Situacaofunc = 'A'
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And fu.CODINTFUNC = d.codintfunc
           And trunc(d.dtdigit) Between f.aquiinifer And f.aquifinfer
           And fu.CODIGOEMPRESA In 9
           And d.codocorr In (507,560,553,583)
           And f.statusferias = 'N'
           And d.tipodigit = 'F'
           And d.statusdigit = 'N' )
Group By  codintfunc, codfunc, NOMEFUNC,
               Gozoinifer, gozofinfer, aquiinifer,
               aquifinfer, nrdocto           