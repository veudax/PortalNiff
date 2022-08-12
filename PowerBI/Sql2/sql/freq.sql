
Select s.Rowid, f.codfunc--, s.dtdigit, s.extradm, s.excessodm, (7.20 - (s.extradm + s.excessodm))
, s.*
  From frq_digitacaomovimento s, vw_funcionarios f
Where 
  s.dtdigit In ('1-aug-2020')
--  s.dtdigit Between '03-aug-2020' And '17-aug-2020'
  --In ('03-aug-2020', '04-aug-2020','05-aug-2020')
--  And s.codocorr In (99,606)
--  And s.codocorr = 123
  And s.tipodigit = 'F'
--  And (s.extradm + s.excessodm) < 7.20
--  And s.extradm > 0
  And f.codintfunc = s.codintfunc
  And codigoempresa = 26 And codigofl = 1
--  And codarea = 40
  And codfunc = '002324'
Order By codfunc
  