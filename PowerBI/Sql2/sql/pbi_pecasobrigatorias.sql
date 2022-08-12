create or replace view pbi_pecasobrigatorias as
Select 30000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 1 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('10010348','05010029','04012734','10010254','10020021','07011574','05010166','04040053','04070137','05010049','05010243',
                                   '05010060','05010218','05070007','05070021')
 Union All
Select 30000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 10 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('4180','07010949')
 Union All
Select 30000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 8 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('05070018','05070019')
 Union All
Select 30000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 2 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04050063','05050013','05050010','05050012','05050007','05050006','05050005','05060009','05070123','05070124',
                                   '05060133','05060126','05060120','05060002')
 Union All
 Select 30000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 20 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('05020004')
 Union All
 Select 30000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 5 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('06010646')
 Union All
 Select 30000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 3 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('06011947')
 Union All
Select 15000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 8 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('05070018','05070019')
 Union All
Select 15000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 1 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('05070124','05070021','05010166','04040053','04070137','05010049','05010243')
 Union All
Select 5000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 1 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('05070018','05070019','05070124','05070021','05010166','04040053','04070137','05010049','05010243')
 
 Union All
 
Select 30000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 1 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('10010528','04010255','04012734','10010356','10020021','07011574','04010171','04040053','04070137','04010140','04010168',
                                   '04010947','04010170','04010783','04010084','4070009','4070125')
 Union All
Select 30000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 10 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('4180','07010949')
 Union All
Select 30000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 8 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04070041','04070023')
 Union All
Select 30000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 2 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04050008','04050007','04050016','04050001','04050012','04050010','04050014','04050002','04070004','04070155','04030033','04060038','04060035','04060070')
 Union All
 Select 30000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 20 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04020053')
 Union All
 Select 30000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 5 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('06010646')
 Union All
 Select 30000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 3 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('06011947')
 Union All
Select 15000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 1 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04070009','04070125','04070485','04040053','04070137','04010140','04010168','04010947')
 Union All
Select 15000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 8 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04070041','04070023')
 Union All
Select 5000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial, 1 Quantidade
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04070041','04070023','04070009','04070125','04070485','04040053','04070137','04010140','04010168','04010947')
 
/*Select 30000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('10010348','05010029','04012734','10010254','10020021','4180','07010949','07011574','05010166','04040053','04070137','05010049','05010243',
'05010060','05010218','04050063','05050013','05050010','05050012','05050007','05050006','05050005','05060009','05070018','05070019','05070123','05070124','05070007','05070021',
'05060133','05060126','05060120','05060002','05020004','06010646','06011947')
Select 15000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('05070018','05070019','05070124','05070021','05010166','04040053','04070137','05010049','05010243')
 Union All
Select 5000 KM, 'VW' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('05070018','05070019','05070124','05070021','05010166','04040053','04070137','05010049','05010243')*/
 
/*Select 30000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('10010528','04010255','04012734','10010356','10020021','4180','07010949','07011574','04010171','04040053','04070137','04010140','04010168',
'04010947','04010170','04010783','04010084','04050008','04050007','04050016','04050001','04050012','04050010','04050014','04050002','04070041','04070023','04070004','04070155',
'4070009','4070125','04030033','04060038','04060035','04060070','04020053','06010646','06011947')
 Union All
Select 15000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04070041','04070023','04070009','04070125','04070485','04040053','04070137','04010140','04010168','04010947')
 Union All
Select 5000 KM, 'Mercedes' Marca, m.Descricaomat, m.codigomatint, m.codigointernomaterial
  From Est_Cadmaterial m
 Where m.codigointernomaterial In ('04070041','04070023','04070009','04070125','04070485','04040053','04070137','04010140','04010168','04010947')

*/