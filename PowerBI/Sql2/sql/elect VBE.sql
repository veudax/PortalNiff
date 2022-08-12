Select i.*, i.Rowid From Vbe_Estoque e, Vbe_Itensestoque i
Where e.cod_seq_agencia = (Select cod_seq_agencia 
                             From t_Trf_Agencia a
                            Where a.cod_agencia = 'A23')
  And e.operacao = 'ED'                            
  And i.Id_Vbe_Estoque = e.Id_Vbe_Estoque
  And i.codpasse = 1000;
  
Update Vbe_Itensestoque i
  Set i.id_vbe_lote = Null
 Where i.id_vbe_estoque = 434113
   