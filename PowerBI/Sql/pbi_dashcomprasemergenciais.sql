create or replace view pbi_dashcomprasemergenciais as
Select EmpFil
         , Round(Sum(Valor)/1000,0) Realizado
         , Round(Sum(previsto)/1000,0) Previsto
         , Round(Sum(MaxRealizado)/1000,0) MaxRealizado
         , Round(Sum(MinRealizado)/1000,0) MinRealizado
      From (
    Select (Sum(i.qtdepedido * i.valorunitariopedido) +
           Sum(i.qtdepedido * i.valoripipedido)  +
           Sum(i.qtdepedido * i.valorfretepedido) +
           Sum(i.qtdepedido * i.valoricmssubstpedido) ) - Sum(i.qtdepedido * i.valordescontopedido) valor
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
       And i.dataaprovpedido Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
       And f.codigoempresa Not In (5,29)
--       And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64) -- Diferente de Robson
       And c.Statuspedido = 'F' --In ('P','F')
     Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0')
     Union All
    Select (Sum(o.qtdeitoutpedido * o.vlrunitariooutpedido) +
          Sum(o.qtdeitoutpedido * o.vlripioutpedido)  +
          Sum(o.qtdeitoutpedido * o.vlrfreteoutpedido) +
          Sum(o.qtdeitoutpedido * o.vlricmssubstoutpedido) ) - Sum(o.qtdeitoutpedido * o.vlrdescontooutpedido) valor
         , Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
         , 0 previsto
         , 0 MaxRealizado
         , 0 MinRealizado
      From cpr_pedidocompras c
         , cpr_itensoutrospedido o, cpr_cadmaterialavulso m, flp_funcionarios f
     Where o.codigomatavulso = m.codigomatavulso
       And o.codintFunc = f.codintfunc
       And m.codigogrd In (500,510)
       And o.dataaprovoutpedido Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
       And o.numeropedido = c.numeropedido
       And f.codigoempresa Not In (5,29)
--       And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64)  -- Diferente de Robson
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
          , Round(Min(Valor),2) MinRealizado
       From (Select (Sum(i.qtdepedido * i.valorunitariopedido) +
                    Sum(i.qtdepedido * i.valoripipedido)  +
                    Sum(i.qtdepedido * i.valorfretepedido) +
                    Sum(i.qtdepedido * i.valoricmssubstpedido) ) - Sum(i.qtdepedido * i.valordescontopedido) valor
                  , Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
                  , to_char(i.dataaprovpedido, 'yyyymm') referencia
               From cpr_pedidocompras c, cpr_itensdepedido i, est_cadmaterial m, flp_funcionarios f
              Where i.codigomatint = m.codigomatint
               And i.codintFunc = f.codintfunc
               And m.codigogrd In (500,510)
               And i.numeropedido = c.numeropedido
               And i.dataaprovpedido Between Add_Months(Sysdate, -12)  and Sysdate
               And f.codigoempresa Not In (5,29)
--               And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64) -- Diferente de Robson
               And c.Statuspedido = 'F' --In ('P','F')
             Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0')
                 , to_char(i.dataaprovpedido, 'yyyymm')
             Union All
            Select (Sum(o.qtdeitoutpedido * o.vlrunitariooutpedido) +
                  Sum(o.qtdeitoutpedido * o.vlripioutpedido)  +
                  Sum(o.qtdeitoutpedido * o.vlrfreteoutpedido) +
                  Sum(o.qtdeitoutpedido * o.vlricmssubstoutpedido) ) - Sum(o.qtdeitoutpedido * o.vlrdescontooutpedido) valor
                    , Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
                    , to_char(o.dataaprovoutpedido, 'yyyymm') referencia
                  From cpr_pedidocompras c
                     , cpr_itensoutrospedido o, cpr_cadmaterialavulso m, flp_funcionarios f
                 Where o.codigomatavulso = m.codigomatavulso
                   And o.codintFunc = f.codintfunc
                   And m.codigogrd In (500,510)
                   And o.dataaprovoutpedido Between Add_Months(Sysdate, -12)  and Sysdate
                   And o.numeropedido = c.numeropedido
                   And f.codigoempresa Not In (5,29)
            --       And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64)  -- Diferente de Robson
                   And c.Statuspedido = 'F' --In ('P','F')
                 Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0')
                     , to_char(o.dataaprovoutpedido, 'yyyymm')
            )
        Group By EmpFil

     Union All
     Select 0 Valor
          , EmpFil
          , 0 Previsto
          , Round(Max(Valor),2) MaxRealizado
          , 0 MinRealizado
       From ( Select (Sum(i.qtdepedido * i.valorunitariopedido) +
                     Sum(i.qtdepedido * i.valoripipedido)  +
                     Sum(i.qtdepedido * i.valorfretepedido) +
                     Sum(i.qtdepedido * i.valoricmssubstpedido) ) - Sum(i.qtdepedido * i.valordescontopedido)valor
                  , Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
                  , to_char(i.dataaprovpedido, 'yyyymm') referencia
               From cpr_pedidocompras c, cpr_itensdepedido i, est_cadmaterial m, flp_funcionarios f
              Where i.codigomatint = m.codigomatint
               And i.codintFunc = f.codintfunc
               And m.codigogrd In (500,510)
               And i.numeropedido = c.numeropedido
               And i.dataaprovpedido Between Add_Months(Sysdate, -12)  and Sysdate
               And f.codigoempresa Not In (5,29)
--And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64) -- Diferente de Robson
               And c.Statuspedido = 'F' --In ('P','F')
             Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0')
                 , to_char(i.dataaprovpedido, 'yyyymm')
             Union All
            Select (Sum(o.qtdeitoutpedido * o.vlrunitariooutpedido) +
                  Sum(o.qtdeitoutpedido * o.vlripioutpedido)  +
                  Sum(o.qtdeitoutpedido * o.vlrfreteoutpedido) +
                  Sum(o.qtdeitoutpedido * o.vlricmssubstoutpedido) ) - Sum(o.qtdeitoutpedido * o.vlrdescontooutpedido) valor
                     , Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0') EmpFil
                    , to_char(o.dataaprovoutpedido, 'yyyymm') referencia
                  From cpr_pedidocompras c
                     , cpr_itensoutrospedido o, cpr_cadmaterialavulso m, flp_funcionarios f
                 Where o.codigomatavulso = m.codigomatavulso
                   And o.codintFunc = f.codintfunc
                   And m.codigogrd In (500,510)
                   And o.dataaprovoutpedido Between Add_Months(Sysdate, -12)  and Sysdate
                   And o.numeropedido = c.numeropedido
                   And f.codigoempresa Not In (5,29)
            --       And f.codintfunc Not In (Select CodFunc From Niff_Chm_Usuarios Where idUsuario = 64)  -- Diferente de Robson
                   And c.Statuspedido = 'F' --In ('P','F')
                 Group By Lpad(c.Codigoempresa, 3, '0') || '/' || Lpad(c.Codigofl, 3, '0')
                     , to_char(o.dataaprovoutpedido, 'yyyymm')
            )
      Group By empFil
     ) Group By EmpFil

