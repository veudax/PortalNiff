Select c.numeropedido, c.datapedido, c.statuspedido
     , SUBSTR( u.nome, 1, INSTR( u.nome,' ')-1 ) || Substr( u.nome, INSTR( u.nome, ' ', -1)) UsuarioGerou
     , f.nrforn || ' - ' || f.rsocialforn Fornecedor
  From cpr_pedidocompras c, Niff_Chm_Usuarios u
     , Bgm_Fornecedor f
Where c.datapedido > '01-dec-2019'
  And u.usuarioacesso = c.usuariogeroupedido
  And c.codigoforn = f.codigoforn;
  
Select i.qtdepedido, i.codigomatint, m.descricaomat material, i.valorunitariopedido, i.dataaprovpedido, i.codintaprovador, i.codintfunc, f.nomefunc
     , m.codigogrd
  From cpr_itensdepedido i, est_cadmaterial m, flp_funcionarios f
 Where i.numeropedido = 374631 
   And i.codigomatint = m.codigomatint
   And i.codintFunc = f.codintfunc(+) 
 Union All  
Select o.qtdeitoutpedido, o.codigomatavulso, DESCRICAOMATAVULSO, o.vlrunitariooutpedido, o.dataaprovoutpedido, o.codintaprovador, o.codintfunc, f.nomefunc
     , m.codigogrd
  From cpr_itensoutrospedido o, cpr_cadmaterialavulso m, flp_funcionarios f
 Where o.numeropedido = 374631 
   And o.codigomatavulso = m.codigomatavulso
   And o.codintFunc = f.codintfunc(+)    