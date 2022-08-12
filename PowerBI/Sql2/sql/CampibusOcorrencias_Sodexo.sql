Select descocorr, codfunc, NOMEFUNC, nrdocto,
       Sum(Qtd) Qtd
  From (              
Select o.descocorr,
       fu.codintfunc, fu.codfunc, fu.NOMEFUNC,
       dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,
               (Case When d.dtdigit > '30-apr-2017' Then 1 Else 0 End) Qtd
          From Vw_Funcionarios Fu,
               Flp_Documentos dc,
               Frq_Digitacaomovimento d,
               frq_ocorrencia o
         Where d.dtdigit Between '21-jun-2019' And '20-jul-2019'--Last_day(Sysdate+30)
           And Fu.Situacaofunc = 'A'
           And dc.codintfunc = fu.CODINTFUNC
           And dc.tipodocto = 'CPF'
           And fu.CODINTFUNC = d.codintfunc
           And fu.CODIGOEMPRESA In 9
--           And fu.CODIGOFL In (1,2)
           And d.codocorr In (583,560,553, 561)
           And d.tipodigit = 'F'
           And d.statusdigit = 'N'
           And o.codocorr = d.codocorr )
 Group By descocorr, codfunc, NOMEFUNC, nrdocto