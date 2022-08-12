Select s.CODIGOEMPRESA, s.CODIGOFL, Count(*), s.CODFUNCAO, s.CODAREA, a.descarea
From Vw_Funcionarios s, flp_area a
Where s.CODIGOEMPRESA < 99
And s.SITUACAOFUNC <> 'D'
And a.codarea = s.codarea
Group By s.CODIGOEMPRESA, s.CODIGOFL, s.CODFUNCAo, s.CODAREA , a.descarea
Order By s.CODFUNCAo, s.CODIGOEMPRESA, s.CODAREA 
