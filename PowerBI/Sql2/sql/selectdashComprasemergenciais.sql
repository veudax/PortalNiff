Select EmpFil
     , Round(Sum(Valor),2) Realizado
     , Round(Sum(previsto),2) Previsto
     , Sum(MaxRealizado) MaxRealizado
     , Sum(MinRealizado) MinRealizado
  From (
Select Sum(i.qtdepedido * i.valorunitariopedido) valor
     , Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
     , 0 previsto
     , 0 MaxRealizado
     , 0 MinRealizado
  From cpr_pedidocompras c
     , cpr_itensdepedido i, est_cadmaterial m, flp_funcionarios f
 Where i.codigomatint = m.codigomatint
   And i.codintFunc = f.codintfunc
   And m.codigogrd In (500,510)
   And i.numeropedido = c.numeropedido
   And c.datapedido Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
   And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64) -- Diferente de Robson
   And c.Statuspedido = 'F' --In ('P','F')
 Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0')   
 Union All  
Select Sum(o.qtdeitoutpedido * o.vlrunitariooutpedido) valor
     , Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
     , 0 previsto
     , 0 MaxRealizado
     , 0 MinRealizado     
  From cpr_pedidocompras c
     , cpr_itensoutrospedido o, cpr_cadmaterialavulso m, flp_funcionarios f
 Where o.codigomatavulso = m.codigomatavulso
   And o.codintFunc = f.codintfunc    
   And m.codigogrd In (500,510)   
   And c.datapedido Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
   And o.numeropedido = c.numeropedido
   And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64)  -- Diferente de Robson
   And c.Statuspedido = 'F' --In ('P','F')   
 Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0')   
 Union All
Select 0 Valor
     , e.CodigoGlobus EmpFil
     , Sum(Meta) Previsto 
     , 0 MaxRealizado
     , 0 MinRealizado     
  From niff_sup_metasaprovadores m, niff_chm_empresas e
 Where m.IdEmpresa = e.Idempresa
   And referencia = to_char(Sysdate,'yyyymm')
 Group By e.codigoglobus 
 
 Union All
 Select 0 Valor
      , EmpFil
      , 0 Previsto
      , 0 MaxRealizado
      , Min(MinRealizado)
   From (
     Select Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
         , Sum(o.qtdeitoutpedido * o.vlrunitariooutpedido) MinRealizado     
         , to_char(c.datapedido) referencia
      From cpr_pedidocompras c
         , cpr_itensoutrospedido o, cpr_cadmaterialavulso m, flp_funcionarios f
     Where o.codigomatavulso = m.codigomatavulso
       And o.codintFunc = f.codintfunc    
       And m.codigogrd In (500,510)   
       And c.datapedido Between Add_Months(Sysdate, -12) And Sysdate
       And o.numeropedido = c.numeropedido
       And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64)  -- Diferente de Robson
       And c.Statuspedido = 'F' --In ('P','F')   
     Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') 
         , to_char(c.datapedido) )
    Group By EmpFil    

 Union All
 Select 0 Valor
      , EmpFil
      , 0 Previsto
      , Max(MaxRealizado) MaxRealizado
      , 0 MinRealizado
   From ( 
     Select Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
         , Sum(o.qtdeitoutpedido * o.vlrunitariooutpedido) MaxRealizado
         , to_char(c.datapedido) referencia
      From cpr_pedidocompras c
         , cpr_itensoutrospedido o, cpr_cadmaterialavulso m, flp_funcionarios f
     Where o.codigomatavulso = m.codigomatavulso
       And o.codintFunc = f.codintfunc    
       And m.codigogrd In (500,510)   
       And c.datapedido Between Add_Months(Sysdate, -12) And Sysdate
       And o.numeropedido = c.numeropedido
       And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64)  -- Diferente de Robson
       And c.Statuspedido = 'F' --In ('P','F')   
     Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') 
         , to_char(c.datapedido))
  Group By empFil 
 ) Group By EmpFil