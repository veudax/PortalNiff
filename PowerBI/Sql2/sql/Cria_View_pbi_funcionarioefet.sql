create or replace view pbi_funcionarioefet as
select '001/001 - EOVG DUTRA' empresa, count(distinct f.nomefunc) total_func, ff.competficha data
  from vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 1
   and f.codigofl = 1
   and ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   and (ff.tipofolha = 1)
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--   And f.SITUACAOFUNC <> 'F'
--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '001/002 - EOVG LAVRAS' empresa, count(distinct f.nomefunc || f.codfunc) total_func, ff.competficha data
  from vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 1
   and f.codigofl = 2
   and ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   and (ff.tipofolha = 1)
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--   And f.SITUACAOFUNC <> 'F'
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '002/001 - ABC TRANSPORTES' empresa, count(distinct f.nomefunc ) total_func, ff.competficha data
  from vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 2
   and f.codigofl = 1
   and ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   and (ff.tipofolha = 1)
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--   And f.SITUACAOFUNC <> 'F'
--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '003/001 - RAPIDO D OESTE' empresa, count(distinct f.nomefunc  || f.codfunc) total_func, ff.competficha data
  from   vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 3
   and f.codigofl = 1
   and ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   and (ff.tipofolha = 1)
--   And f.SITUACAOFUNC <> 'F'
   And f.CODFUNCAO Not In (519,616,686,794,838,846)

--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '004/001 - CISNE BRANCO' empresa, count(distinct f.nomefunc ) total_func, ff.competficha data
  from vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 4
   and f.codigofl = 1
   and ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   and (ff.tipofolha = 1)
--   And f.SITUACAOFUNC <> 'F'
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '005/001 - NIFF' empresa, count(distinct f.nomefunc ) total_func, ff.competficha data
  from vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 5
   and f.codigofl = 1
   and ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   and (ff.tipofolha = 1)
--   And f.SITUACAOFUNC <> 'F'
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '006/001 - VIA��O ARUJ�' empresa, count(distinct f.nomefunc ) total_func, ff.competficha data
  from vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 6
   and f.codigofl = 1
   and ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   and (ff.tipofolha = 1)
--   And f.SITUACAOFUNC <> 'F'
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '009/001 - CAMPIBUS LTDA' empresa, count(distinct f.nomefunc  || f.codfunc) total_func, ff.competficha data
  From vw_funcionarios f, flp_fichafinanceira ff
 Where f.codigoempresa = 9
   And f.codigofl = 2
   And ff.competficha between '01-jan-2015' and sysdate
   And ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   And (ff.tipofolha = 1)
--   And f.SITUACAOFUNC <> 'F'
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '013/001 - RIBE TRANSPORTES' empresa, count(distinct f.nomefunc ) total_func, ff.competficha data
  from vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 13
   And f.codigofl = 1
   And ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
--   And f.SITUACAOFUNC <> 'F'
   and (ff.tipofolha = 1)
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '026/001 - VUG DUTRA' empresa, count(distinct f.nomefunc ) total_func, ff.competficha data
  from vw_funcionarios f, flp_fichafinanceira ff
 where f.codigoempresa = 26
   And f.codigofl = 1
   And ff.competficha between '01-jan-2015' and sysdate
   and ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   and (ff.tipofolha = 1)
--   And f.SITUACAOFUNC <> 'F'
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--and      ff.codintfunc = 11038
group by ff.competficha
UNION ALL
 ----------------------------------------------------------------------------
select '026/003 - VUG BEBEDOURO' empresa, count(distinct f.nomefunc ) total_func, ff.competficha
  From  vw_funcionarios f, flp_fichafinanceira ff
 Where f.codigoempresa = 26
   And f.codigofl = 3
   And ff.competficha between '01-jan-2015' and sysdate
   And ff.codintfunc = f.codintfunc
   and ff.situacaoffinan <> 'F'
   And ff.tipofolha = 1
--   And f.SITUACAOFUNC <> 'F'
   And f.CODFUNCAO Not In (519,616,686,794,838,846)
--and      ff.codintfunc = 11038
group by ff.competficha

