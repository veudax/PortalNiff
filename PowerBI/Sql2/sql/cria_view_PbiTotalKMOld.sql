create or replace view pbi_totalkm as
Select empresa, EmpFil, Trunc(Last_day(DtHist)) DtHist, TotalKm, To_char(DtHist, 'mm/yyyy') mesAno
From (
select '001/001 - EOVG DUTRA' empresa,
        '001/001' EmpFil,dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 1
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '001/002 - EOVG LAVRAS' empresa,
        '001/002' EmpFil,a.dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 1
    and b.codigofl = 2
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '002/001 - ABC TRANSPORTES' empresa,
        '002/001' EmpFil,a.dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 2
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '003/001 - RAPIDO D OESTE' empresa,
        '003/001' EmpFil, a.dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 3
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
   )a
UNION ALL
----------------------------------------------------------------------------
select '004/001 - CISNE BRANCO' empresa,
        '004/001' EmpFil,a.dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 4
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '005/001 - NIFF' empresa,
        '005/001' EmpFil, a.dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 5
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '006/001 - VIAÇÃO ARUJÁ' empresa,
        '006/001' EmpFil,a.dthist,totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 6
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '009/001 - CAMPIBUS LTDA' empresa,
        '009/001' EmpFil, a.dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 9
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '013/001 - RIBE TRANSPORTES' empresa,
        '013/001' EmpFil,a.dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 13
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '026/001 - VUG DUTRA' empresa,
        '026/001' EmpFil,a.dthist, totalkm
From (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 26
    and b.codigofl = 1
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
UNION ALL
----------------------------------------------------------------------------
select '026/003 - VUG BEBEDOURO' empresa,
        '026/003' EmpFil,a.dthist,  totalkm
From  (
 Select To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
   from bgm_velocimetro b
  where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
    and b.codigoempresa = 26
    and b.codigofl = 3
  group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy')
  )a
)

