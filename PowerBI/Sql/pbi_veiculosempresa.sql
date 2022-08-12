create or replace view pbi_veiculosempresa as
Select v.Codigoveic, v.Prefixoveic, Lpad(V.codigoempresa,3,'0') || '/' || Lpad(v.codigofl,3,'0') EmpFil
  From frt_cadveiculos v
  , frt_modchassi m
  , frt_marcachassi mc
 Where v.condicaoveic = 'A'
   And v.codigomodchassi = m.codigomodchassi
   And m.codigomarchassi = mc.codigomarchassi
   And mc.codigomarchassi In (80, 81 )

