create or replace view pbi_pneus as
Select
      a.empfil, a.marca, a.modelo, medida, a.nrofogo, condicaopneu, total_km, km, data, tipo, a.mesano, anomes, ano
     , Upper(a.TipoServicos) TipoServicos, a.Recusadoservos, a.Recapador
     , Sum(a.Quantidade) Quantidade
     , decode(condicaopneu
             ,'VE', 'VENDIDO'
             ,'CA', 'CARRO'
             ,'SU', 'SUCATA'
             ,'ES', 'ESTOQUE'
             ,'LI', 'LIXO'
             ,'MA', 'MANUTENCAO'
             , 'OUTROS') DescricaoCondicao
  From Pbi_ConsultaPneus a
 Group By a.empfil, a.marca, a.modelo, medida, a.nrofogo, condicaopneu, total_km, km, data, tipo, a.mesano, anomes, ano
     , a.TipoServicos, a.Recusadoservos, a.Recapador

