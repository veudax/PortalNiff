Create Or Replace View Pbi_ValoresGastosGrupoDesesas As
Select Sum(Nvl(((Nvl(Io.Vlrunitariooutpedido, 0) +
           Nvl(Io.Vlripioutpedido, 0) + Nvl(Io.Vlrsegurooutpedido, 0) +
           Nvl(Io.Vlrfreteoutpedido, 0) -
           Nvl(Io.Vlrdescontooutpedido, 0) +
           Nvl(Io.Vlricmssubstoutpedido, 0)) * Io.Qtdeitoutpedido), 0)) As Valor,
       Last_day(Io.Dataaprovoutpedido) Data,
       Lpad(p.codigoempresa,3,'0') || '/' || Lpad(p.CodigoFl,3,'0') EmpFil,
       f.NOMEFUNC NomeFuncionario,
       D.Codigogrd || '-' || d.Descricaogrd Grupo
  From Cpr_Pedidocompras     p,
       Cpr_Itensoutrospedido Io,
       Cpr_Cadmaterialavulso Av,
       vw_funcionarios F,
       est_grupodespesas D
 Where Io.Numeropedido = p.Numeropedido
   And Io.Codigomatavulso = Av.Codigomatavulso
   And Av.Sequencia = 1
   And p.Codigoempresa || p.codigofl In (11,12,21,31,315,337,41,51,61,91,131,261,263) /* P_EMPRESA */
   And Io.Dataaprovoutpedido Between Trunc(SYSDATE,'rr') And Last_Day(Sysdate)
   And Io.Statusoutpedido In ('P', 'F') 
   And f.CODINTFUNC = io.codintfunc
   And d.codigogrd = av.codigogrd
 Group By Lpad(p.codigoempresa,3,'0') || '/' || Lpad(p.CodigoFl,3,'0'),
          Last_day(Io.Dataaprovoutpedido),
          f.NOMEFUNC,
          D.Codigogrd || '-' || d.Descricaogrd