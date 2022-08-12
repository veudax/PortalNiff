create or replace view pbi_dashabsenteismoaux as
Select Empresa, EmpFil, Sum(total_Admitidos) total_Admitidos, Sum(total_Demitidos) total_Demitidos, Data, CodArea, descArea
         From (select 'X' empresa
                    , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    , count(distinct f.nomefunc) total_Admitidos
                    , 0 total_Demitidos
                    , Last_day(f.dtadmfunc) Data
                    , f.codarea, descArea
                 from PBI_vwFuncionarios f
                where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                  And f.dtadmfunc Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
                group by f.dtadmfunc, f.codarea, descArea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
                union all
               select 'X' empresa
                    , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    , count(distinct f.nomefunc) total_Admitidos
                    , 0 total_Demitidos
                    , Last_Day(f.dttransffunc) Data
                    , f.codarea, descArea
                 from PBI_vwFuncionarios f
                where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                  and f.dttransffunc Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
                group by f.dttransffunc, f.codarea, descArea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
                Union All
               select 'X' empresa
                    , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    , 0 total_Admitidos
                    , count(distinct f.nomefunc) total_Demitidos
                    , Last_day(q.dtdesligquita) Data
                    , f.codarea, descArea
                 From PBI_vwFuncionarios f, flp_quitacao q
                where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                  and q.dtdesligquita Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
                  and q.codintfunc = f.codintfunc and q.statusquita = 'N'
                group by q.dtdesligquita, f.codarea, descArea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') )
          Group By Empresa, EmpFil, Data, CodArea, descArea

