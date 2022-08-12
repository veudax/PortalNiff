Select Sum(Qtd_Ocorr) qtd,  CodIntFunc, to_char(dtHist,'yyyy/mm') mes
From 
(
Select Count(Fu.CodIntFunc) Qtd_Ocorr, 
       t.Dthist,-- fu.CODFUNC,
       Fu.CodIntFunc
--,       Trunc(dm.dtdigit) , trunc(t.dthist)
  From Flp_Historico t, 
       Frq_Ocorrencia f, 
       Vw_Funcionarios Fu,
--       bgm_cadlinhas l,
      (Select d.codintfunc, d.codocorr, d.dtdigit
           ,  decode(d.codintlinha, Null, p.codintlinha, d.codintlinha) linha,
             decode(d.codigoveic, Null, p.Codigoveic, d.codigoveic) Veiculo
        From frq_digitacaomovimento d
           , (Select dp.codintfunc, dp.dtdigit, dp.codigoveic, dp.codintlinha
                From frq_digitacaomovimento dp
               Where dp.statusdigit = 'N'
                 And dp.tipodigit = 'P') p
       Where p.dtdigit(+) = d.dtdigit
         And p.codintfunc(+) = d.codintfunc
         And d.tipodigit = 'F'
         And d.statusdigit = 'N') dm
 Where t.Dthist Between Add_Months(trunc(Sysdate,'rr'),-12) And Sysdate
   And f.Codocorr = t.Codocorr
   And Fu.Situacaofunc = 'A'

   And Trunc(dm.dtdigit) = trunc(t.dthist)
   And dm.codintfunc = t.codintfunc
   And dm.codocorr = t.codocorr
   And f.Codocorr In
       (505, 504, 506, 507, 521, 526, 534, 535, 541, 542, 579, 543, 582, 579, 560, 584, 580, 581, 583, 592, 593, 527, 562)
   And t.Codintfunc = Fu.Codintfunc
   And Fu.Codarea In (40)
--   And fu.CODINTFUNC = 126
 Group By t.Dthist, 
          Fu.CodIntFunc
)
Group By CodIntFunc, To_char(dthist,'yyyy/mm')
Order By CodIntFunc, mes