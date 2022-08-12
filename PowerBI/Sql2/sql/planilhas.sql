select m.descricao, t.previsto, t.realizado, t.rowid from niff_ads_valoresmetas t, niff_ads_metas m
Where t.referencia = '201806'
And idempresa = 10
And m.Idmetas = t.idmetas
Order By m.descricao
