create or replace view pbi_acidentes as
Select EmpFil, Trunc(Last_day(DtHist)) Dthist, To_char(DtHist, 'mm/yyyy') mesAno, qtd_ocorr, ocorrencia
  From (select a.EmpFil, a.qtd_ocorr, a.dthist, a.ocorrencia, To_char(a.DtHist,'mm/yyyy') mesAno
          From (select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia,
                       lpad(fu.codigoempresa,3,'0') || '/' || lPad(Decode(fu.codigoempresa,9, decode(fu.codigofl, 2, 1, fu.codigofl),fu.codigofl),3,'0') EmpFil
                  from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
                 where t.dthist BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and sysdate
                   and f.codocorr = t.codocorr
                   and fu.codigoempresa || fu.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                   and f.codocorr In (502,501,257,226,86,84,503,511,512,103,228,603)
                   and t.codintfunc = fu.codintfunc
                 group by t.dthist, f.codocorr || ' - ' || f.descocorr, fu.codigoempresa, fu.codigofl ) a
)

