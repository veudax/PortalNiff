Select s.codigointernomaterial codigo, s.descricaomat, s.codigogrd grpDespesa, d.descricaogrd descGrpDespesa, s.codtpdespesa tpDespesa, a.desctpdespesa descTpDespesa,
s.codigogrcon GrpContabil, c.descricaogrcon DescGrpContabil
  From est_cadmaterial s, est_grupodespesas d, cpgtpdes a, est_grupocontabil c
 Where d.codigogrd = s.codigogrd  
   And a.codtpdespesa = s.codtpdespesa
   And c.codigogrcon = s.codigogrcon
   
Order By TpDespesa, descTpDespesa, GrpDespesa, DescGrpDespesa
