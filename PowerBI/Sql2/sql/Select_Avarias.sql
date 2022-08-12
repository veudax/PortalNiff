Create Or Replace View PBI_AvariasPorLinha As
  Select a.Qtd_Ocorr, 
         a.Dthist, 
         a.Ocorrencia,
         a.codintlinha,
         a.carro, 
         a.codintfunc, 
         funcionario
        
    From (Select Count(Fu.Nomefunc) Qtd_Ocorr,
                 t.Dthist,
                 f.Codocorr || ' - ' || f.Descocorr Ocorrencia,
                 a.codintlinha,
                 v.prefixoveic carro,
                 fu.codintfunc, 
                 codfunc || ' - ' || fu.nomefunc funcionario ,
                 a.numerorainfger, 
                 a.datarainfger               
            From Flp_Historico t,
                 Frq_Ocorrencia f, 
                 vw_funcionarios Fu,
                 ACD_FUNCINFORMGERAIS ac,
                 acd_informacoesgerais A,
                 frt_cadveiculos v
           Where t.Dthist Between '01-jan-2016' And Sysdate
             And f.Codocorr = t.Codocorr
             And f.Codocorr In (514, 106, 241, 594, 595)
             And t.Codintfunc = Fu.Codintfunc
             And fu.codintfunc = ac.codintfunc
             And a.codigoempresa = ac.codigoempresa
             And a.codigofl = ac.codigofl
             And a.numerorainfger = ac.numerorainfger
             And a.Datarainfger = trunc(t.dthist)
             And a.codocorr = t.codocorr
             And v.codigoveic = a.codigoVeic
             And fu.SITUACAOFUNC = 'A'
             And fu.CODAREA = 40
           Group By t.Dthist, 
                    f.Codocorr || ' - ' || f.Descocorr,
                    a.codintlinha,
                    v.prefixoveic, 
                    fu.codintfunc, 
                    codfunc, fu.nomefunc,
                    a.numerorainfger, 
                    a.datarainfger) a