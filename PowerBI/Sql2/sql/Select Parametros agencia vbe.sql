Select a.*, p.perguntaparametro From Pvt_Paramresposta_Agencia a, pvt_parampergunta p
Where a.codigoempresa = 4
And p.codparametro = a.codparametro
And p.localparametro = 'AGENCIA'
And p.perguntaparametro Like '%Lote%'