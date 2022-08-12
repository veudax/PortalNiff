
Select f.rem_rz_social rSocial
     , f.rem_endereco Endereco
     , f.rem_cgc Cnpj
     , f.rem_insc_est Ie
     , f.conhecimento
     , f.serie
     , f.data_emissao
     , tipo_docto
     , f.Total_Prest
     , f.tipo_frete
     , l.desc_localidade
  From fta001 f, exa002 l
 Where f.empresa = 3
   And f.data_emissao Between '01-jan-2017' And '31-dec-2017'
--   And f.conhecimento = 172412
   And (f.tipo_frete = 'C' And f.rem_localidade In (select t.cod_localidade from exa002 t Where t.desc_localidade Like 'JABOTIC%' And t.cod_uf = 'SP'))
   And l.cod_localidade = f.rem_localidade
   And length(f.rem_cgc) > 11

 Union All

Select f.rem_rz_social rSocial
     , f.dest_endereco Endereco
     , f.dest_cgc Cnpj
     , f.dest_insc_est Ie
     , f.conhecimento
     , f.serie
     , f.data_emissao
     , tipo_docto
     , f.Total_Prest
     , f.tipo_frete
     , l.desc_localidade
  From fta001 f, exa002 l
 Where f.empresa = 3
   And f.data_emissao Between '01-jan-2017' And '31-dec-2017'
--   And f.conhecimento = 172412
   And (f.tipo_frete = 'F' And f.dest_localidade In (select t.cod_localidade from exa002 t Where t.desc_localidade Like 'JABOTIC%' And t.cod_uf = 'SP'))
   And l.cod_localidade = f.dest_localidade    
   And length(f.dest_cgc) > 11

   