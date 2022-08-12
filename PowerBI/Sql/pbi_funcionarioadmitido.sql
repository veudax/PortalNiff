create or replace view pbi_funcionarioadmitido as
select 'X' empresa
     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , count(distinct f.nomefunc) total_func
     , f.dtadmfunc Data
     , f.codarea
  from PBI_vwFuncionarios f
 where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
   And f.CODFUNCAO Not In (794,519,846)
   And f.dtadmfunc between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and Sysdate
 group by f.dtadmfunc, f.codarea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
 union all
select 'X' empresa
     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , count(distinct f.nomefunc)total_func
     , f.dttransffunc Data
     , f.codarea
  from PBI_vwFuncionarios f
 where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
   And f.CODFUNCAO Not In (794,519,846)
   and f.dttransffunc between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and sysdate
 group by f.dttransffunc, f.codarea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')

