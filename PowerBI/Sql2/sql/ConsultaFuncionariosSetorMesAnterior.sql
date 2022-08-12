Select f.Codsetor, f.Descsetor, Count(*) Quantidade
  From Vw_Funcionarios f, Flp_Fichafinanceira Ff
 Where Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl, 3, '0') =
       '003/001'
   And Ff.Competficha = To_Date('28/02/2019', 'dd/mm/yyyy')
   And Ff.Codintfunc = f.Codintfunc
   And Ff.Situacaoffinan = 'A'
   And (Ff.Tipofolha = 1)
--   And f.Codfuncao Not In (838)
 Group By f.Codsetor, f.Descsetor
 Union All
Select f.Codsetor, f.Descsetor, Count(*) Quantidade  
  from vw_funcionarios f
 where Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl, 3, '0') =   '003/001'
   And f.dtadmfunc between '01-mar-2019' and '31-mar-2019'
--   And f.Codfuncao Not In (838)   
 Group By f.Codsetor, f.Descsetor
 Union All
Select f.Codsetor, f.Descsetor, Count(*) Quantidade  
  from vw_funcionarios f
 where Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl, 3, '0') =   '003/001'
   And f.dttransffunc between '01-mar-2019' and '31-mar-2019'
--   And f.Codfuncao Not In (838)   
 Group By f.Codsetor, f.Descsetor
 Union All
Select f.Codsetor, f.Descsetor, Count(*)*-1 Quantidade 
  from vw_funcionarios f, flp_quitacao q
  where Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl, 3, '0') =   '003/001'
    and q.dtdesligquita between '01-mar-2019' and '31-mar-2019'
    and q.codintfunc = f.codintfunc and q.statusquita = 'N'
--   And f.Codfuncao Not In (838)  
 Group By f.Codsetor, f.Descsetor 
