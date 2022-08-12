Select * From vbe_bilhetes b
Where b.codserie = 'A1'
And b.nrbilhete Between 244001 And 252000
And b.flg_disponivel = 'N'
And b.cod_seq_agencia <> 9;

Update VBE_ITENSESTOQUE 
Set CODSERIE = 'A1OLD'
Where Id_Vbe_Estoque In (
Select E.Id_Vbe_Estoque 
 From Vbe_Estoque e, Vbe_Itensestoque i, t_Trf_Agencia a
Where i.id_vbe_estoque = e.id_vbe_estoque
And i.codpasse = 1000
And i.codserie = 'A1' 
And i.nrinicial1 Between 244001 And 258000
And a.cod_seq_agencia = 9 
And e.cod_seq_agencia = a.cod_seq_agencia
)
; 
