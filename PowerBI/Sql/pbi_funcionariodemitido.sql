create or replace view pbi_funcionariodemitido as
select 'X' empresa
     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , count(distinct f.nomefunc) total_func
     , q.dtdesligquita Data
     , f.codarea
  from PBI_vwFuncionarios f, flp_quitacao q
 where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
   and q.dtdesligquita between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and sysdate
   and q.codintfunc = f.codintfunc and q.statusquita = 'N'
 group by q.dtdesligquita, f.codarea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')

