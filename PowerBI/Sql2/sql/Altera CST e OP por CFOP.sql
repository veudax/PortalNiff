Update est_itensnf i
 Set i.codoperfiscal = 90, i.codsittributaria = 90
Where i.codintnf In (
Select f.codintnf From Bgm_Notafiscal f
Where f.coddoctoesf In 
(
Select s.coddoctoesf From Esfentra s
Where s.dtentradaentra >= '01-nov-2017'
And s.codigoempresa = 3
And s.codclassfisc In (1556,2556)));


Update Est_Nfservico i
 Set i.codoperfiscal = 90, i.codsittributaria = 90
--Select i.codsittributaria, i.codoperfiscal From Est_Nfservico i
Where i.codintnf In (
Select f.codintnf From Bgm_Notafiscal f
Where f.coddoctoesf In 
(
Select s.coddoctoesf From Esfentra s
Where s.dtentradaentra >= '01-nov-2017'
And s.codigoempresa = 3
And s.codclassfisc In (1556,2556)));
--And s.codsittributaria = 90

Update  Esfentra s
Set s.codoperfiscal_icmsentra = 90, s.codsittributaria = 90
Where s.dtentradaentra >= '01-nov-2017'
And s.codigoempresa = 3
And s.codclassfisc In (1556,2556);