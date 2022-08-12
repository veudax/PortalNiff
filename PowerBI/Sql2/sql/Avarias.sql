Select Distinct a.Datarainfger, a.horarainfger,
                a.Numerorainfger,
                a.Codocorr,
                Ac.Codintfunc,
                fu.codfunc,
                fu.Nomefunc
  From Acd_Funcinformgerais   Ac,
       Acd_Informacoesgerais  a,
       flp_historico t, 
       frq_ocorrencia f, 
       flp_funcionarios fu
 Where a.Codigoempresa = Ac.Codigoempresa
   And a.Codigofl = Ac.Codigofl
   And a.Numerorainfger = Ac.Numerorainfger
   And Ac.Funcprincipalfuncinfger = 'S'

   and f.codocorr = t.codocorr
   and a.codocorr = t.codocorr
   and fu.codigoempresa || fu.codigofl In (12)
   and f.codocorr In (594)--,595)
   and t.codintfunc(+) = fu.codintfunc
   And ac.codintfunc(+) = fu.codintfunc
   And Trunc(a.Datarainfger) Between '01-nov-2017' And '30-nov-2017'
   And trunc(t.dthist) BETWEEN '01-nov-2017' And '30-nov-2017'