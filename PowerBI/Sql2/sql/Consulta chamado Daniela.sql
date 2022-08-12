Select * From cpgDocto d
Where codigoempresa = 1 And codigofl = 2
And d.codigoforn = (Select f.codigoforn From Bgm_Fornecedor f Where f.nrforn = '004376')
And d.seriedoctocpg = '0'
And d.nrodoctocpg = 'PR-0520/17'
And d.pagamentocpg = '02-oct-2017';

Select * From Bcomovto m
Where m.docmovtobco = 'PE-000004'
And m.codigoempresa = 1
And m.codigofl = 2
And m.codbanco = 341
And m.codagencia = 554
And m.codcontabco = '8300'
And m.dtmovtobco = '29-sep-2017';
--Where m.codmovtobco = 1289821;

Select * From Ctblanca l
Where l.codlanca = 2293970; -- inclusão

Select * From ctblanca l
Where l.codlanca In (2300342, 2300336); --pagamento