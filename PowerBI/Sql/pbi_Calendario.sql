create or replace view pbi_calendario as
Select To_char(Data, 'mm/yyyy') mesAno, To_char(Data, 'yyyy/mm') anoMes, To_char(Data, 'yyyy') Ano
    From Dim_Ctr_Periodo
   Where Data Between ADD_MONTHS(Trunc(Sysdate,'rr'), -24)
     And Last_day(Trunc(Sysdate))
   Group By To_char(Data, 'mm/yyyy'), To_char(Data, 'yyyy/mm'), To_char(Data, 'yyyy')

