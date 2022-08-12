select a.EmpFil, a.qtd_ocorr, a.dthist, a.ocorrencia, To_char(a.DtHist,'mm/yyyy') mesAno, codintfunc, codfunc
    From (select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia,fu.codintfunc, fu.codfunc,
                 lpad(fu.codigoempresa,3,'0') || '/' || lPad(Decode(fu.codigoempresa,9, decode(fu.codigofl, 2, 1, fu.codigofl),fu.codigofl),3,'0') EmpFil
            from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
           where t.dthist BETWEEN '01-dec-2017' and '31-dec-2017'
             and f.codocorr = t.codocorr
             and fu.codigoempresa || fu.codigofl In 12--(11,12,21,31,41,51,61,92,131,261,263)
             and f.codocorr In 594
             and t.codintfunc = fu.codintfunc
           group by t.dthist, f.codocorr || ' - ' || f.descocorr, fu.codigoempresa, fu.codigofl,fu.codintfunc, fu.codfunc
 ) a

