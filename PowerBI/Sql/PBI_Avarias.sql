create or replace view pbi_avarias as
select a.EmpFil, a.qtd_ocorr, a.dthist, a.ocorrencia, To_char(a.DtHist,'mm/yyyy') mesAno, nomefunc
    From (select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia,
                 lpad(fu.codigoempresa,3,'0') || '/' || lPad(Decode(fu.codigoempresa,9, decode(fu.codigofl, 2, 1, fu.codigofl),fu.codigofl),3,'0') EmpFil,
                 fu.codfunc || '-' ||fu.nomefunc nomeFunc
            from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
           where t.dthist BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and sysdate
             and f.codocorr = t.codocorr
             and fu.codigoempresa || fu.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
             and f.codocorr In (514,106,241,594,595)
             and t.codintfunc = fu.codintfunc
           group by t.dthist, f.codocorr || ' - ' || f.descocorr, fu.codigoempresa, fu.codigofl, fu.codfunc || '-' ||fu.nomefunc
 ) a

