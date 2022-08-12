select a.EmpFil, a.qtd_ocorr, a.dthist, a.ocorrencia, To_char(a.DtHist,'mm/yyyy') mesAno, codintfunc, codfunc
    From (select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, fu.codintfunc, fu.codfunc,
                 lpad(fu.codigoempresa,3,'0') || '/' || lPad(Decode(fu.codigoempresa,9, decode(fu.codigofl, 2, 1, fu.codigofl),fu.codigofl),3,'0') EmpFil
            from flp_historico t, frq_ocorrencia f, flp_funcionarios fu,
                 Acd_Funcinformgerais   Ac,
                 Acd_Informacoesgerais  b
           where t.dthist BETWEEN '01-dec-2017' and '31-dec-2017'
             and f.codocorr = t.codocorr
             and fu.codigoempresa || fu.codigofl In 12
             and f.codocorr In (594)
             and t.codintfunc = fu.codintfunc
             And ac.codintfunc = fu.codintfunc
             And t.codintfunc = ac.codintfunc
             And b.numerorainfger = ac.numerorainfger
             And trunc(t.dthist) = b.datarainfger
           group by t.dthist, f.codocorr || ' - ' || f.descocorr, fu.codigoempresa, fu.codigofl, fu.codintfunc, fu.codfunc
 ) a

