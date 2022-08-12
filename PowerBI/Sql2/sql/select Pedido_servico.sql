Select o.*, o.Rowid From --Cpr_Pedidocompras c, 
--cpr_itensdepedido i,
 cpr_itensoutrospedido o
Where o.numeropedido = 297542
--And i.numeropedido = c.numeropedido
--And o.numeropedido = c.numeropedido