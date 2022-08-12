Select Distinct s.codigomatavulso || '-' || s.descricaomatavulso Material,
--       Decode(g.descricao, Null, Null, s.codigogenero || '-' || g.descricao) Genero,
--       s.codncm NCM,
--       s.cod_ident_cst CEST,
       Decode(s.tipo, 'P', 'Produto', 'Serviço') Classificacao,
       ESF.codtpdespesa, d.desctpdespesa, m.codigogrd
  From EST_CADMATERIALAVULSO  s 
     ,  ESFMatAvulso ESF
     , Est_Generoitem g
     , Cpgtpdes D
     , est_cadmaterial m
 Where g.codigogenero(+) = s.codigogenero
 And s.CODIGOMATAVULSO = ESF.CODIGOMATAVULSO
 And esf.codtpdespesa = d.codtpdespesa
 And esf.codtpdespesa = m.codtpdespesa
 Order By material