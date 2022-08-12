create or replace view pbi_funcionarioadmitido as
(select '001/001 - EOVG DUTRA' empresa, count(distinct f.nomefunc)total_func, f.dtadmfunc data
from   flp_funcionarios f
where    f.codigoempresa = 1      and      f.codigofl = 1      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '001/001 - EOVG DUTRA' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 1      and      f.codigofl = 1      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '001/002 - EOVG LAVRAS' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 1      and      f.codigofl = 2      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '001/002 - EOVG LAVRAS' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 1      and      f.codigofl = 2      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '002/001 - ABC TRANSPORTES' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 2      and      f.codigofl = 1      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '002/001 - ABC TRANSPORTES' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 2      and      f.codigofl = 1      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '003/001 - RAPIDO D OESTE' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 3      and      f.codigofl = 1      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '003/001 - RAPIDO D OESTE' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 3      and      f.codigofl = 1      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '004/001 - CISNE BRANCO' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 4      and      f.codigofl = 1      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '004/001 - CISNE BRANCO' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 4      and      f.codigofl = 1      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '005/001 - NIFF' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 5      and      f.codigofl = 1      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '005/001 - NIFF' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 5      and      f.codigofl = 1      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '006/001 - VIAÇÃO ARUJÁ' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 6      and      f.codigofl = 1      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '006/001 - VIAÇÃO ARUJÁ' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 6      and      f.codigofl = 1      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '009/001 - CAMPIBUS LTDA' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 9      and      f.codigofl = 2      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '009/001 - CAMPIBUS LTDA' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 9      and      f.codigofl = 2      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '013/001 - RIBE TRANSPORTES' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 13      and      f.codigofl = 1      and      f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '013/001 - RIBE TRANSPORTES' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 13      and      f.codigofl = 1      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '026/001 - VUG DUTRA' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 26      and      f.codigofl = 1      and     f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '026/001 - VUG DUTRA' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 26      and      f.codigofl = 1      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)
UNION ALL
 ----------------------------------------------------------------------------
(select '026/003 - VUG BEBEDOURO' empresa, count(distinct f.nomefunc)total_func,f.dtadmfunc data
from flp_funcionarios f
where    f.codigoempresa = 26      and      f.codigofl = 3      and     f.dtadmfunc between '01-jan-2015' and sysdate
group by f.dtadmfunc
union all
select '026/003 - VUG BEBEDOURO' empresa, count(distinct f.nomefunc)total_func,f.dttransffunc data
from flp_funcionarios f
where    f.codigoempresa = 26      and      f.codigofl = 3      and      f.dttransffunc between '01-jan-2015' and sysdate
group by f.dttransffunc)

