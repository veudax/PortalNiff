Create Or Replace View Pbi_Radar_Calendario As
  Select To_char(Data, 'mm/yyyy') mesAno, To_char(Data, 'yyyy/mm') anoMes, To_char(Data, 'yyyy') Ano
    From Dim_Ctr_Periodo 
   Where Data Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
     And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)
   Group By To_char(Data, 'mm/yyyy'), To_char(Data, 'yyyy/mm'), To_char(Data, 'yyyy')