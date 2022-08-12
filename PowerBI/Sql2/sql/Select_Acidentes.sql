Create Or Replace View PBI_AcidentesPorLinha As
  Select a.Qtd_Ocorr, 
         a.Dthist, 
         a.Ocorrencia, 
         a.codintlinha,
         a.Carro, 
         a.codintfunc, 
         Funcionario
    From (Select Count(Fu.Nomefunc) Qtd_Ocorr,
                 t.Dthist,
                 f.Codocorr || ' - ' || f.Desccomplhist Ocorrencia,
                 a.codintlinha, 
                 v.prefixoveic Carro,
                 fu.codintfunc, 
                 fu.codfunc || ' - ' || fu.NOMEFUNC Funcionario        
            From Flp_Historico t, 
                 Frq_Ocorrencia f, 
                 Vw_Funcionarios Fu, 
                 ACD_FUNCINFORMGERAIS ac,
                 acd_informacoesgerais A,
                 frt_cadveiculos v
           Where t.Dthist Between '01-jan-2016' And Sysdate
             And f.Codocorr = t.Codocorr
             And fu.codintfunc = ac.codintfunc
             And a.codigoempresa = ac.codigoempresa
             And a.codigofl = ac.codigofl
             And a.numerorainfger = ac.numerorainfger
             And a.codocorr = t.codocorr
             And a.Datarainfger = trunc(t.dthist)
             And fu.CODAREA = 40
             And f.Codocorr In
                 (502, 501, 257, 226, 86, 84, 503, 511, 103, 228)
             And t.Codintfunc = Fu.Codintfunc
             And v.codigoveic = a.CodigoVeic
           Group By t.Dthist, 
                    f.Codocorr || ' - ' || f.Desccomplhist, 
                    a.codintlinha, 
                    v.prefixoveic, 
                    fu.codintfunc, 
                    fu.codfunc, fu.NOMEFUNC ) a