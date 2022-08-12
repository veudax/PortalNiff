
Select Count(*), c.iddemonstrativo, c.Idcoluna, c.Data  From niff_fin_coldemonstrativo c
Group By c.iddemonstrativo, c.Idcoluna, c.Data ;

