Select Rowid, s.*
  From flp_historico_escala s
Where s.codintfunc = (Select codintfunc From flp_funcionarios f Where codfunc = '000530' And codigoempresa = 26 And codigofl = 1);

Select Rowid, s.*
  From flp_ferias s
Where s.codintfunc = (Select codintfunc From flp_funcionarios f Where codfunc = '000344' And codigoempresa = 26 And codigofl = 1);
  