Select Count(*), trunc(c.dataabertura)
From niff_jur_comunicados c
Where trunc(c.dataabertura) > '01-mar-2018'
And c.solicitante Is Null
Group By trunc(c.dataabertura)