create or replace view pbi_fluxocaixaperiodo as
 select To_char(Data,'mm/yyyy') MesAno, 
        Max(QtdDiasAnterior) QtdDiasAnterior,
        Max(QtdDiasSequinte) QtdDiasSequinte
   from pbi_fluxodecaixa t
  Group By To_char(Data,'mm/yyyy')

