-- Altera Documento Fiscal no Modulo de escrituraçao fiscal - entrada
Select s.*, Rowid From esfentra s
Where s.codigoempresa = 1
And s.codigofl = 2
And s.nrdocentra = '0000527634';

-- Altera Documento Fiscal no Modulo de escrituração fiscal - Saída
Select s.*, Rowid From esfsaida s
Where s.codigoempresa = 26
And s.codigofl = 1
And s.nrdocsaida = '000002117';    

-- Nota fiscal emitida pela escrituração -- usar o codintNF_esfNF da tabela Bgm_notaFiscal_eletronica
Select e.*, Rowid From esfnotafiscal e
Where e.codintnf = 47916; 

-- Itens da Nota fiscal emitida pela escrituração -- usar o codintNF_esfNF da tabela Bgm_notaFiscal_eletronica
Select e.*, Rowid From esfnotafiscal_item e
Where e.codintnf = 47916;   


-- Localiza a Nota fiscal eletronica -- normalmente pra mudar o status da NF para conseguir cancelar a NF
Select f.*, Rowid From bgm_notafiscal_eletronica f
Where f.numeronfe = 527620 
And f.empresa = 1 And FILIAL = 2;    

-- Localiza e altera a Nota fiscal no Estoque -- Cabeçalho da NF - usar o CodintNF_bgm_nf da tabela Bgm_notaFiscal_eletronica
Select e.*,Rowid From Bgm_Notafiscal e 
Where e.codintnf = 308408; 
   
-- Localiza e altera os Iens da Nota Fiscal  - usar o CodintNF_bgm_nf da tabela Bgm_notaFiscal_eletronica
Select e.*, Rowid From est_itensnf e 
Where e.codintnf = 308408;    

-- Localiza e altera os Iens de servio da Nota Fiscal  - usar o CodintNF_bgm_nf da tabela Bgm_notaFiscal_eletronica
Select e.*, Rowid From Est_Nfservico e
Where e.codintnf = 308408;    

-- Localiza e altera NF de material avulso da Nota Fiscal  
Select * From est_nfmaterialavulso a
Where a.numeronf = '0000001500'

-- Localiza e altera NF de material avulso da Nota Fiscal  - usar o CodIntNfAvul da consulta acima
Select a.*, Rowid From EST_MATERIALAVULSONF a
Where a.codintnfavul = 80

-- Localiza e altera NF de venda de Pneus
select t.*, t.rowid from pne_vendapneu t
Where T.codintnf = 46899;   