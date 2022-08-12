Create Or Replace View PBI_FiscaisDosInspetores As
Select f.Linha,
       f.Fiscais,
       i.Inspetores,
       i.Dtdigit Data,
       i.Entradigit EntradaInspetor,
       i.Saidadigit SaidaInspetor,
       f.Entradigit EntradaFiscal,
       f.Saidadigit SaidaFiscal,
       f.CodigoFiscal,
       i.CodigoInspetor

  From (Select l.Codigolinha || ' - ' || l.Nomelinha Linha, -- Traz os Fiscais
               f.Codfunc || ' - ' || f.Nomefunc Fiscais,
               Dh.Codigoveic,
               Dh.Servicodigit,
               l.Codintlinha,
               l.Tipolinha,
               d.Dtdigit,
               Dh.Entradigit,
               Dh.Saidadigit, f.CODINTFUNC CodigoFiscal
          From Frq_Digitacao          d,
               Frq_Digitacaomovimento Dh,
               Vw_Funcionarios        f,
               Flp_Funcao             c,
               Bgm_Cadlinhas          l
         Where d.Dtdigit Between '01-may-2017' And '31-may-2017'
           And d.Dtdigit = Dh.Dtdigit(+)
           And d.Tipodigit = Dh.Tipodigit(+)
           And d.Codintfunc = Dh.Codintfunc(+)
           And f.Codintfunc = d.Codintfunc
           And f.Codfuncao In (111, 112, 282, 16, 82, 9, 236) --fiscais
           And f.Codarea = 40
           And f.Codfuncao = c.Codfuncao
           And Dh.Iddigit = 1
           And Dh.Usuexcldigit Is Null
           And d.Tipodigit = 'F'
           And Dh.Codocorr In (96, 99, 98)
           And f.Codigoempresa = 1
           And f.Codigofl = 1
           And l.Codintlinha = Dh.Codintlinha
         Group By l.Codigolinha || ' - ' || l.Nomelinha,
                  f.Codfunc || ' - ' || f.Nomefunc,
                  Dh.Codigoveic,
                  Dh.Servicodigit,
                  l.Codintlinha,
                  d.Dtdigit,
                  l.Tipolinha,
                  Dh.Entradigit,
                  Dh.Saidadigit,
                  f.CODINTFUNC) f,
       
       (Select l.Codigolinha || ' - ' || l.Nomelinha Linha,
               f.Codfunc || ' - ' || f.Nomefunc Inspetores,
               Dh.Codigoveic,
               Dh.Servicodigit,
               l.Codintlinha,
               d.Dtdigit,
               Dh.Entradigit,
               Dh.Saidadigit, 
               f.CODINTFUNC CodigoInspetor
          From Frq_Digitacao          d,
               Frq_Digitacaomovimento Dh,
               Vw_Funcionarios        f,
               Flp_Funcao             c,
               Bgm_Cadlinhas          l
         Where d.Dtdigit Between '01-may-2017' And '31-may-2017'
           And d.Dtdigit = Dh.Dtdigit(+)
           And d.Tipodigit = Dh.Tipodigit(+)
           And d.Codintfunc = Dh.Codintfunc(+)
           And f.Codintfunc = d.Codintfunc
           And f.Codfuncao In (294, 259, 260, 45, 187, 563, 586)
           And f.Codarea = 40
           And f.Codfuncao = c.Codfuncao
           And Dh.Iddigit = 1
           And Dh.Usuexcldigit Is Null
           And d.Tipodigit = 'F'
           And Dh.Codocorr In (96, 99, 98)
           And f.Codigoempresa = 1
           And f.Codigofl = 1
           And l.Codintlinha = Dh.Codintlinha
        
         Group By l.Codigolinha || ' - ' || l.Nomelinha,
                  f.Codfunc || ' - ' || f.Nomefunc,
                  Dh.Codigoveic,
                  Dh.Servicodigit,
                  l.Codintlinha,
                  d.Dtdigit,
                  Dh.Entradigit,
                  Dh.Saidadigit,
                  f.CODINTFUNC) i

 Where f.Codintlinha = i.Codintlinha
   And f.Entradigit Between i.Entradigit And i.Saidadigit
      --  And i.saidadigit Between f.entradigit And f.saidadigit  
   And f.Dtdigit = f.Dtdigit

 Order By Fiscais, Linha, i.Inspetores, Data