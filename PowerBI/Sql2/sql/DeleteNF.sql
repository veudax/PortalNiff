Delete
From Est_Itensmovto f
Where f.seqmovto In (10011637, 10011638);

Delete
 From Est_Movto f 
 Where f.codintnf = 287520 ;

Delete 
From Est_Associaccustofinancitensnf i
Where i.codintnf = 287520;

Delete
 From est_itensnf i
Where i.codintnf = 287520;

Delete
 From Est_Venctonf i
Where i.codintnf = 287520;
 

Delete
From Est_Pedidoitensnf f
Where f.codintnf = 287520;

Delete
From Est_Pedidonf f
Where codintnf = 287520;

Delete
From bgm_confirmamanifestdest f
Where f.codintnf_bgm = 287520;

Delete
From Bgm_Notafiscal n
--Where n.numeronf = '0000462126'
Where codintnf = 287520;

