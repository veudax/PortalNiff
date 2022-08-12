
Select s.Rowid, f.codfunc,  s.*
  From frq_digitacao s, vw_funcionarios f
Where /*s.codintfunc In (Select codintfunc From flp_funcionarios f Where codigoempresa = 1 And codigofl = 1)
  And */s.dtdigit In ('01-may-2020', '25-may-2020')
--  And s.codocorr In (99,606)
  And s.tipodigit = 'F'
  And s.tipodigit = 'F'
  And s.extradigit + s.excessodigit < 7.2
  And f.codintfunc = s.codintfunc
  And codigoempresa = 1 And codigofl = 1
  And codarea = 40