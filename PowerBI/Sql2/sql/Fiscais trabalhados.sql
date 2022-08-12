Select v.codfunc, v.codintfunc, v.codigoempresa, v.CODIGOFL, d.entradigit, d.saidadigit, l.codigolinha 
From frq_digitacaomovimento d, vw_Funcionarios v, bgm_cadlinhas l
Where d.codintfunc = v.codintfunc
And v.CODAREA = 40
And v.CODFUNCAO In (112,113)
And d.dtdigit = '20-jan-2017'
And d.tipodigit = 'F'
And d.statusdigit = 'N'
And d.codocorr In (96,98,99)
And v.codigoempresa In (1,26)
And v.codigofl = 1
And l.codintlinha(+) = d.codintlinha
