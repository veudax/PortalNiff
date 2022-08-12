Select Data, horas, hora2, tipo, horasdia,
  extra, Incompletas, 
 ConverteDecimalEmHorasString(extra - decode(extra,0,0,DescontarEntrada + DescontarSaida )) ExtraS,
 ConverteDecimalEmHorasString(Incompletas) IncompletasS,
 entrada, saida, DescontarEntrada, DescontarSaida
From (
Select Data, horas, hora2, tipo, horasDia,
       Case When (tipo = 1 Or tipo = 7) And horas = 0 Then 0
            When (tipo = 1 Or tipo = 7) And horas > 0 Then horas
            When greatest(horasDia, horas) > horasDia Then horas - horasDia            
       Else 
        0 End Extra,
        
        
       Case When (tipo = 1 Or tipo = 7) And horas = 0 Then 0
            When Least(horasDia, horas) < horasDia Then horasDia - horas Else 
        0 End InCompletas, entrada, saida,
        
        Case When entrada > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '07:54', 'dd/mm/yyyy hh24:mi') And
                Entrada < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '08:00', 'dd/mm/yyyy hh24:mi') Then
                Round(QtdHr(entrada, To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '08:00', 'dd/mm/yyyy hh24:mi')),2) Else 0 End DescontarEntrada
,
        Case When Saida <= To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:05', '18:05'), 'dd/mm/yyyy hh24:mi') Then
                Round(QtdHr(To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi'),saida  ),2) Else  0 End DescontarSaida
        
  From (
Select Round(QtdHr( entrada, Saida) - QtdHr( SaidaAlmoco, VoltaAlmoco),2) horas, Data
     , ConverteDecimalEmHorasString(Round(TOTALHRS( entrada, Saida, SaidaAlmoco, VoltaAlmoco,6),2)) hora2
     , to_char(Data,'D') tipo, decode(to_char(Data,'D') , 6, 8, 9) horasDia
     , entrada
     , saida
  From (
Select Min(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.entradigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) entrada
     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.Intidigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) SaidaAlmoco
     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.Intfdigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) VoltaAlmoco
     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.saidadigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) Saida
     , f.dtdigit Data

From frq_digitacaomovimento f
Where f.codintfunc = 24125
--And f.dtdigit = '19-feb-2018'
And f.dtdigit Between '21-jan-2018' And '20-mar-2018'
And statusdigit = 'N'
Group By f.dtdigit ) ) )
Order By Data