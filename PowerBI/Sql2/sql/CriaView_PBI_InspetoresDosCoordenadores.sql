Create Or Replace View PBI_InspetoresDosCoordenadores As
Select f.Linha,
       f.Coordenador,
       i.Inspetores,
       i.Dtdigit Data,
       i.Entradigit EntradaInspetor,
       i.Saidadigit SaidaInspetor,
       f.Entradigit EntradaFiscal,
       f.Saidadigit SaidaFiscal,
       f.CodigoCoordenador,
       i.CodigoInspetor

  From     
       (Select l.Codigolinha || ' - ' || l.Nomelinha Linha,
               f.Codfunc || ' - ' || f.Nomefunc Inspetores,
               Dh.Codigoveic,
               Dh.Servicodigit,
               l.Codintlinha,
               d.Dtdigit,
               Dh.Entradigit,
               Dh.Saidadigit, 
               f.CODINTFUNC CodigoInspetor,
               l.cod_escalagrupolinha GlInspetor
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
           And f.Codfuncao in (294, 259, 260, 45, 187, 563, 586)--fiscais
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
                  f.CODINTFUNC, l.cod_escalagrupolinha) i,
                  
       (Select l.Codigolinha || ' - ' || l.Nomelinha Linha, -- Traz os Coordenadores
               f.Codfunc || ' - ' || f.Nomefunc Coordenador,
               Dh.Codigoveic,
               Dh.Servicodigit,
               l.Codintlinha,
               l.Tipolinha,
               d.Dtdigit,
               Dh.Entradigit,
               Dh.Saidadigit, f.CODINTFUNC CodigoCoordenador,
               l.cod_escalagrupolinha GlCoordenador
              
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
           And f.Codfuncao In (67,88,176) -- coordenadores
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
                  f.CODINTFUNC, l.cod_escalagrupolinha) f
                  

 Where f.GlCoordenador = i.GlInspetor
   And f.Entradigit Between i.Entradigit And i.Saidadigit
      --  And i.saidadigit Between f.entradigit And f.saidadigit  
   And f.Dtdigit = f.Dtdigit

 Order By Coordenador, Linha, i.Inspetores, Data