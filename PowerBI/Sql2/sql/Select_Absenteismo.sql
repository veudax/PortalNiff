-- original do BI
select sum(a.qtd_ocorr) Qtd, CODINTFUNC, To_char(a.dthist,'yyyy/mm') mes
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.CODINTFUNC
from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
where t.dthist BETWEEN '01-jan-2016' and sysdate
   and f.codocorr = t.codocorr
   and fu.situacaofunc = 'A'
   and f.codocorr In (505,504,506,507,521,526,534,535,541,542,579,543,582,579,560,584,580,581,583,592,593,527,562)
   and t.codintfunc = fu.codintfunc
   and fu.CODAREA in (40)
--   And fu.codfunc = '000049'

   group by t.dthist, fu.CODINTFUNC )a
--Where codintfunc = 34
Group By To_char(a.dthist,'yyyy/mm') 
, CODINTFUNC
Order By CODINTFUNC, mes