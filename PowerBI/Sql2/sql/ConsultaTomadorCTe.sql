Select a.rem_rz_social rSocial -- Remetente Tomador
     , a.rem_endereco Endereco
     , a.rem_cgc Cnpj
     , a.rem_insc_est Ie
     , a.conhecimento
     , a.serie
     , a.data_emissao
     , a.tipo_docto
     , a.Total_Prest
     , a.tipo_frete
     , l.desc_localidade
  From Fta001 a, Fta011 b, exa002 l
 Where a.Conhecimento = b.Conhecimento
   And a.Serie = b.Serie
   And a.Empresa = b.Empresa
   And a.Filial = b.Filial
   And a.Garagem = b.Garagem
   And a.Tipo_Docto = b.Tipo_Docto
   And a.Data_Emissao Between '01-jan-2017' And '31-dec-2017'
   And a.Tipo_Docto = 57
   And a.Empresa = 3
   And a.rem_localidade = l.cod_localidade
   And l.desc_localidade = 'JABOTICABAL'
   And l.cod_uf = 'SP'
   And (a.tipo_frete = 'C' Or b.frete_a_vista_remetente = 'S')
   And a.contrib_rem = 'S'
 Union All   
Select a.Dest_rz_social rSocial -- destinatario Tomador
     , a.Dest_endereco Endereco
     , a.Dest_cgc Cnpj
     , a.Dest_insc_est Ie
     , a.conhecimento
     , a.serie
     , a.data_emissao
     , a.tipo_docto
     , a.Total_Prest
     , a.Tipo_Frete
     , l.desc_localidade
  From Fta001 a, Fta011 b, exa002 l
 Where a.Conhecimento = b.Conhecimento
   And a.Serie = b.Serie
   And a.Empresa = b.Empresa
   And a.Filial = b.Filial
   And a.Garagem = b.Garagem
   And a.Tipo_Docto = b.Tipo_Docto
   And a.Data_Emissao Between '01-jan-2017' And '31-dec-2017'
   And a.Tipo_Docto = 57
   And a.Empresa = 3
   And a.Dest_localidade = l.cod_localidade
   And l.desc_localidade = 'JABOTICABAL'
   And l.cod_uf = 'SP'
   And (a.tipo_frete = 'F' Or b.Frete_a_Vista_Destinatario = 'S')
   And a.contrib_Dest = 'S' 
 Order By data_emissao, Conhecimento  