create or replace view pbi_funcionariosativosdemitido as
Select Empresa, EmpFil, Sum(total_Admitidos) total_Admitidos, Sum(total_Demitidos) total_Demitidos, Data, CodArea
         From (select 'X' empresa
                    , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    , count(distinct f.nomefunc) total_Admitidos
                    , 0 total_Demitidos
                    , Last_day(f.dtadmfunc) Data
                    , f.codarea
                 from PBI_vwFuncionarios f
                where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                  And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                          571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                          692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                  And f.dtadmfunc between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and Sysdate
                group by f.dtadmfunc, f.codarea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
                union all
               select 'X' empresa
                    , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    , count(distinct f.nomefunc) total_Admitidos
                    , 0 total_Demitidos
                    , Last_Day(f.dttransffunc) Data
                    , f.codarea
                 from PBI_vwFuncionarios f
                where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                  And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                          571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                          692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                  and f.dttransffunc between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and sysdate
                group by f.dttransffunc, f.codarea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
                Union All
               select 'X' empresa
                    , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    , 0 total_Admitidos
                    , count(distinct f.nomefunc) total_Demitidos
                    , Last_day(q.dtdesligquita) Data
                    , f.codarea
                 from PBI_vwFuncionarios f, flp_quitacao q
                where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                  and q.dtdesligquita between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and sysdate
                  and q.codintfunc = f.codintfunc and q.statusquita = 'N'
                  And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                          571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                          692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                group by q.dtdesligquita, f.codarea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') )
          Group By Empresa, EmpFil, Data, CodArea

