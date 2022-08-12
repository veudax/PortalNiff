create or replace view pbi_avarias as
select '001/001 - EOVG DUTRA' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 1
    and fu.codigofl = 1
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 1 
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '001/002 - EOVG LAVRAS' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  Where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 1
    and fu.codigofl = 2
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 1 
    and b.codigofl = 2
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '002/001 - ABC TRANSPORTES' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 2
    and fu.codigofl = 1
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 2
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '003/001 - RAPIDO D OESTE' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 3
    and fu.codigofl = 1
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
   group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 3
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
   )a
UNION ALL
----------------------------------------------------------------------------
select '004/001 - CISNE BRANCO' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 4
    and fu.codigofl = 1
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 4
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '005/001 - NIFF' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 5
    and fu.codigofl = 1
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 5 
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '006/001 - VIAÇÃO ARUJÁ' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 6
    and fu.codigofl = 1
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 6 
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '009/001 - CAMPIBUS LTDA' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 9
    and fu.codigofl = 2
--   and fu.situacaofunc = 'A'
    and f.codocorr in(248,514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 9 
    and b.codigofl = 2
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '013/001 - RIBE TRANSPORTES' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 13
    and fu.codigofl = 1
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 13
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '026/001 - VUG DUTRA' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 26
    and fu.codigofl = 1
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 26 
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
UNION ALL
----------------------------------------------------------------------------
select '026/003 - VUG BEBEDOURO' empresa, a.qtd_ocorr, a.dthist, a.ocorrencia, totalkm
from
(select count(fu.nomefunc) qtd_ocorr, t.dthist, f.codocorr || ' - ' || f.descocorr ocorrencia, 0 totalkm
   from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
  where t.dthist BETWEEN '01-jan-2016' and sysdate
    and f.codocorr = t.codocorr
    and fu.codigoempresa = 26
    and fu.codigofl = 3
--   and fu.situacaofunc = 'A'
    and f.codocorr in(514,106,241,594,595)
    and t.codintfunc = fu.codintfunc
  group by t.dthist, f.codocorr || ' - ' || f.descocorr
  Union All  
 Select 0 qtd_ocorr, To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), '595 - AVARIA S/CULPA' ocorrencia,  sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 26 
    and b.codigofl = 3
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') 
  )a
