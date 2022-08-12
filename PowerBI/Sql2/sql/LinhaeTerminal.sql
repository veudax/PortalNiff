Select t.* From niff_pbi_linhasporservico s, Bgm_Cadlinhas l, niff_pbi_terminallinhas t
Where l.codintlinha = s.codintlinha
And l.codigolinha = '281'
And t.idterminal = s.Idterminal