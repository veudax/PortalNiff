
select l.codlanca, l.documentolanca, l.dtlanca from ctblanca l
where sistema = 'CGS'
AND L.DOCUMENTOLANCA LIKE 'SQ%'
--AND l.dtlanca in ('05-apr-2017', '06-apr-2017','10-apr-2017');

select l.dtlanca, l.documentolanca, i.codlanca from ctblanca l, fta001_integracao_ctb i
where sistema = 'CGS'
AND l.dtlanca in ('05-apr-2017', '06-apr-2017','10-apr-2017')
and i.codlanca = l.codlanca(+)
order by 1,2 ; 