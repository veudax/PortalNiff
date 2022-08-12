create or replace view pbi_valoresgastosgrupodesesas as
Select Sum(Valor) Valor, Data, EmpFil, NomeFuncionario, Grupo
  From (Select Sum((Nvl(i.Valorunitariopedido, 0) + Nvl(i.Valoripipedido, 0) +
                    Nvl(i.Valorseguropedido, 0) + Nvl(i.Valorfretepedido, 0) -
                    Nvl(i.Valordescontopedido, 0) + Nvl(i.Valoricmssubstpedido, 0)) * i.Qtdepedido) As Valor,
               Last_day(I.Dataaprovpedido) Data,
               Lpad(s.codigoempresa,3,'0') || '/' || Lpad(s.CodigoFl,3,'0') EmpFil,
               f.NOMEFUNC NomeFuncionario,
               D.Codigogrd || '-' || d.Descricaogrd Grupo

          From Cpr_Itensdepedido i,
               Cpr_Pedidocompras s,
               Est_Cadmaterial m,
               PBI_vwFuncionarios F,
               est_grupodespesas D
         Where i.Numeropedido = s.Numeropedido
           And i.Codigomatint = m.Codigomatint
           And s.Codigoempresa || s.codigofl In (11,12,21,31,315,337,41,51,61,91,131,261,263) /* P_EMPRESA */
           And i.Dataaprovpedido Between Trunc(SYSDATE,'rr') And Last_Day(Sysdate)
           And i.Statuspedido In ('P', 'F')
           And f.CODINTFUNC = i.codintfunc
           And d.codigogrd = m.codigogrd
         Group By  Last_day(I.Dataaprovpedido),
               Lpad(s.codigoempresa,3,'0') || '/' || Lpad(s.CodigoFl,3,'0'),
               f.NOMEFUNC,
               D.Codigogrd || '-' || d.Descricaogrd
         Union All
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
               PBI_vwFuncionarios F,
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
                  D.Codigogrd || '-' || d.Descricaogrd )
Group By Data, EmpFil, NomeFuncionario, Grupo

