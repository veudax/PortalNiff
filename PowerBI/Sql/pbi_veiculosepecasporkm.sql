create or replace view pbi_veiculosepecasporkm as
Select v.codigoveic, v.prefixoveic, mc.descricaomarchassi, m.descricaomodchassi, o.Marca, o.Descricaomat, o.KM, o.codigomatint, o.codigointernomaterial
     , v.codigoempresa, v.codigofl, o.Quantidade
  From frt_cadveiculos v
  , frt_modchassi m
  , frt_marcachassi mc
  , pbi_PecasObrigatorias o
  Where v.condicaoveic = 'A'
    And v.codigomodchassi = m.codigomodchassi
    And m.codigomarchassi = mc.codigomarchassi
    And mc.codigomarchassi = 81
    And o.Marca = 'VW'
 Union All
Select v.codigoveic, v.prefixoveic, mc.descricaomarchassi, m.descricaomodchassi, o.Marca, o.Descricaomat, o.KM, o.codigomatint, o.codigointernomaterial
     , v.codigoempresa, v.codigofl, o.Quantidade
  From frt_cadveiculos v
  , frt_modchassi m
  , frt_marcachassi mc
  , pbi_PecasObrigatorias o
  Where v.condicaoveic = 'A'
    And v.codigomodchassi = m.codigomodchassi
    And m.codigomarchassi = mc.codigomarchassi
    And mc.codigomarchassi = 80
    And o.Marca = 'Mercedes'

