create or replace view pbi_radar_finan_ebitdaranking as
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Round(valor) / 1000 Valor,
       decode(PercentualN,0,Null,Round(PercentualN,2)) Percentual,
       PercentualN PercentualN,
       Ordem,
       Tipo
  From (Select 'EBITDA Raking' Grupo, 13 Ordem,
               Empfil,
               Data,
               Valor,
               PercentualN,
               'F' Tipo
          From pbi_radar_finan_ebitda )

