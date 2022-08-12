create or replace view pbi_radar_operacao as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Valor,
       decode(Percentual,0,Null,Percentual) Percentual,
       Ordem,
       Tipo
  From (Select Grupo,
               Ordem,
               Data,
               EMPFIL,
               VaLor,
               Percentual,
               'O' Tipo
              From Pbi_Radar_Operacional
             Where Tipo = 'Operacional')

