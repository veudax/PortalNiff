create or replace view vw_niff_diasuteis as
Select Sum(Qtde) Qtde
     , Empfil
     , MesAno
     , AnoMes
     , idempresa
 From (Select e.codigoglobus EmpFil
            , to_Char(c.data,'mm/yyyy') MesAno
            , to_Char(c.data,'yyyymm') AnoMes
            , Count(*) Qtde
            , e.idempresa
         From Niff_Calendario c, niff_chm_empresas e
        Where FiMSemana = 'N'
          And Data between ADD_MONTHS(Trunc(Sysdate,'rr'), -42) And Last_day(Sysdate)
        Group By e.codigoglobus
            , to_Char(data,'mm/yyyy')
            , to_Char(data,'yyyymm')
            , e.idempresa
       Having e.codigoglobus Is Not Null
        Union All
       Select e.codigoglobus EmpFil
            , to_Char(f.data,'mm/yyyy') MesAno
            , to_Char(f.data,'yyyymm') AnoMes
            , Count(*) *-1 qtde
            , e.idempresa
         From niff_ads_feriadosemendas f
            , niff_chm_empresas e
            , Niff_Calendario c
        Where f.Idempresa = e.idempresa
          And c.Data = f.Data
          And c.fimsemana = 'N'
          And f.Data between ADD_MONTHS(Trunc(Sysdate,'rr'), -42) And Last_day(Sysdate)
        Group By e.codigoglobus
            , to_Char(f.data,'mm/yyyy')
            , to_Char(f.data,'yyyymm')
            , e.Idempresa)
 group By  Empfil
     , MesAno
     , AnoMes
     , idempresa

