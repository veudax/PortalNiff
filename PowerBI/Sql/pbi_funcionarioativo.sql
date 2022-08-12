create or replace view pbi_funcionarioativo as
select 'X' empresa
     , count(distinct f.nomefunc)total_func
     , Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(Decode(f.Codigoempresa, 9, Decode(f.Codigofl, 2, 1, f.Codigofl), f.Codigofl), 3, '0') EmpFil
     , f.CODAREA
  from PBI_vwFuncionarios f
 where f.Codigoempresa || f.Codigofl In (11,12,21,31,41,51,61,92,131,261,263)
   and f.situacaofunc = 'A'
 Group By Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(Decode(f.Codigoempresa, 9, Decode(f.Codigofl, 2, 1, f.Codigofl), f.Codigofl), 3, '0')
     , f.CODAREA

