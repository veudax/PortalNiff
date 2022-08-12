select 'Funcionario' tipo, CodParam, f.codfunc codigo, nomeFunc descricao, f.DESCFUNCAO funcaoFunc
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t, vw_Funcionarios f
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 9
   And t.codidentparam = f.codintfunc
   And f.situacaofunc <> 'D'
Union All
select 'Função' tipo, CodParam, to_char(f.codfuncao), descFuncao, ''
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t, Flp_Funcao f
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 7
   And t.codidentparam = f.codfuncao
Union All   
select 'Filial' tipo, CodParam, To_char(t.codidentparam), '', ''
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 2
Union All   
select 'Empresa' tipo, CodParam, To_char(t.codidentparam), '', ''
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 1
Union All   
select 'Area' tipo, CodParam, To_char(a.codarea), a.descarea, ''
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t, Flp_Area a
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 3
   And t.codidentparam = a.codarea
Union All   
select 'Depto' tipo, CodParam, To_char(d.coddepto), d.descdepto, ''
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t, Flp_Depto d
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 4
   And t.codidentparam = d.descdepto
Union All   
select 'Setor' tipo, CodParam, To_char(s.codsetor), s.descsetor, ''
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t, Flp_Setor s
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 5
   And t.codidentparam = s.codsetor
Union All   
select 'Seção' tipo, CodParam, To_char(c.codsecao), c.descsecao, ''
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t, Flp_Secao c
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 6
   And t.codidentparam = c.codsecao
Union All   
select 'Sindicato' tipo, CodParam, To_char(i.codsindi), i.descsindi, ''
--       decode(t.tipoparam, 1, 'Empresa', 2, 'Filial', 3, 'Área', 4,'Departamento', 5,'Setor', 6, 'Seção', 7, 'Função', 8,'Sindicato', 'Funcionário') tipo
  from frq_paramdestino t, Flp_Sindicatos i
 Where t.codigoempresa = 1
   And t.codigofl = 2  
   -- And t.codparam = 13
   And t.tipoparam = 8
   And t.codidentparam = i.codsindi

Order By codparam, tipo