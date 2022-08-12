Select Sum(total_Admitidos) total_Admitidos, Sum(total_Demitidos) total_Demitidos, DESCDEPTO, To_Char(Data,'yyyy') ano, DESCSETOR
From (
select 'X' empresa -- admissão por transferência
              , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
              , count(distinct f.nomefunc) total_Admitidos
              , 0 total_Demitidos
              , Last_Day(f.dttransffunc) Data
              , f.codarea, f.DESCDEPTO, f.DESCSETOR, f.DESCSECAO
           from vw_funcionarios f
          where f.codigoempresa || f.codigofl In 51--(11,12,21,31,41,51,61,92,131,261,263)
            And f.CODFUNCAO Not In (519,616,686,794,838,846)
            and f.dttransffunc between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and sysdate
          group by f.dttransffunc, f.codarea, f.DESCDEPTO, f.DESCSETOR, f.DESCSECAO, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
          Union All
         select 'X' empresa -- admissão
              , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
              , count(distinct f.nomefunc) total_Admitidos
              , 0 total_Demitidos
              , Last_Day(f.DTADMFUNC) Data
              , f.codarea, f.DESCDEPTO, f.DESCSETOR, f.DESCSECAO
           from vw_funcionarios f
          where f.codigoempresa || f.codigofl In 51--(11,12,21,31,41,51,61,92,131,261,263)
            And f.CODFUNCAO Not In (519,616,686,794,838,846)
            and f.DTADMFUNC between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and sysdate
          group by Last_Day(f.DTADMFUNC), f.codarea, f.DESCDEPTO, f.DESCSETOR, f.DESCSECAO, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
          Union All
         select 'X' empresa
              , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
              , 0 total_Admitidos
              , count(distinct f.nomefunc) total_Demitidos
              , Last_day(q.dtdesligquita) Data
              , f.codarea, f.DESCDEPTO, f.DESCSETOR, f.DESCSECAO
           from vw_funcionarios f, flp_quitacao q
          where f.codigoempresa || f.codigofl In 51--(11,12,21,31,41,51,61,92,131,261,263)
            and q.dtdesligquita between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and sysdate
            and q.codintfunc = f.codintfunc and q.statusquita = 'N'
            And f.CODFUNCAO Not In (519,616,686,794,838,846)
          group by q.dtdesligquita, f.codarea, f.DESCDEPTO, f.DESCSETOR, f.DESCSECAO, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') 
          ) Group By To_Char(Data,'yyyy') , DESCSETOR, DESCDEPTO