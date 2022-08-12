select count(fu.nomefunc) qtd_ocorr, trunc(t.dthist)--,-- fu.CODINTFUNC,
--       f.codocorr || ' - ' || f.desccomplhist ocorrencia--, fu.CODFUNc
from flp_historico t, frq_ocorrencia f, Vw_Funcionarios fu
where trunc(t.dthist) BETWEEN '21-jan-2017' And '31-jan-2017'
   and f.codocorr = t.codocorr
   And fu.CODAREA = 40
   And fu.codigoempresa In (1,26)
   And fu.CODIGOFL = 1
--   and fu.situacaofunc = 'A'
   and f.codocorr In (514, 106, 241, 594, 595)
   and t.codintfunc = fu.codintfunc
   group by trunc(t.dthist)--, fu.CODINTFUNC, f.codocorr || ' - ' || f.desccomplhist, fu.CODFUNc
   Order By trunc(t.dthist)--, fu.CODINTFUNC