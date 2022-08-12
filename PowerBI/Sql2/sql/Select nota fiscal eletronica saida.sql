Select s.*, Rowid From esfentra s
Where s.codigoempresa = 1 
And s.codigofl = 1
And s.nrdocentra = '0000527637'
And s.coddoctoesf In (3393640, 3445046);*/

Select s.*, Rowid From esfsaida s
Where s.coddoctoesf = 5301975
Where s.codigoempresa = 26
And s.codigofl = 1
And s.nrdocsaida = '000002117';    
 
Select l.*, Rowid From bgm_notafiscal_eletronica_log l
Where l.Id_Nfe = 11794;

Select f.*, Rowid From bgm_notafiscal_eletronica f
Where f.numeronfe = 4837  
And f.empresa = 1 And FILIAL = 1;    
   
Select e.*, Rowid From bgm_numero_nf e
Where e.codintnf_bgm = 307528; 

Select e.*,Rowid From Bgm_Notafiscal e 
/*Where e.numeronf = '0000527637'
And e.codigoempresa = 26;*/
Where e.codintnf = 310732; 
   
  
Select e.*, Rowid From est_itensnf e 
Where e.codintnf = 311168;    

Select e.*, Rowid From Est_Nfservico e
Where e.codintnf = 311168;    
 
Select e.*, Rowid From esfnotafiscal e
Where e.codintnf = 49218; 

Select e.*, Rowid From esfnotafiscal_item e
Where e.codintnf = 49218;   

Select * From est_nfmaterialavulso a
Where a.numeronf = '0000001500'

Select a.*, Rowid From EST_MATERIALAVULSONF a
Where a.codintnfavul = 80

select t.*, t.rowid from pne_vendapneu t
Where T.codintnf = 46899;   