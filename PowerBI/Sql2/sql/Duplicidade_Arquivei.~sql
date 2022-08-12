select t.numeronf, lPad(cnpjemitente,20,'0'), t.idempresa, Count(*) from niff_fis_arquivei t
Group By t.numeronf, lPad(cnpjemitente,20,'0'), t.Idempresa
Having Count(*) > 1
