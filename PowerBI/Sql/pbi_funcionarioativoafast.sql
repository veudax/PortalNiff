create or replace view pbi_funcionarioativoafast as
select '001/001 - EOVG DUTRA' empresa, count(distinct f.nomefunc)total_func
from   flp_funcionarios f
where    f.codigoempresa = 1      and      f.codigofl = 1      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '001/002 - EOVG LAVRAS' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 1      and      f.codigofl = 2      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '002/001 - ABC TRANSPORTES' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 2      and      f.codigofl = 1      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '003/001 - RAPIDO D OESTE' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 3      and      f.codigofl = 1      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '004/001 - CISNE BRANCO' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 4      and      f.codigofl = 1      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '005/001 - NIFF' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 5      and      f.codigofl = 1      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '006/001 - VIAÇÃO ARUJÁ' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 6      and      f.codigofl = 1      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '009/001 - CAMPIBUS LTDA' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 9      and      f.codigofl = 2      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '013/001 - RIBE TRANSPORTES' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 13      and      f.codigofl = 1      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '026/001 - VUG DUTRA' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 26      and      f.codigofl = 1      and      f.situacaofunc in ('A','F')
UNION ALL
 ----------------------------------------------------------------------------
select '026/003 - VUG BEBEDOURO' empresa, count(distinct f.nomefunc)total_func
from flp_funcionarios f
where    f.codigoempresa = 26      and      f.codigofl = 3      and      f.situacaofunc in ('A','F')

