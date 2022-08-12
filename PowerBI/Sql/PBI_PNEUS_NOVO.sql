CREATE OR REPLACE VIEW PBI_PNEUS_NOVO AS
Select empfil, marca, Modelo, Medida, nroFogo, CondicaoPneu, Sum(Km_atual) Km,
       To_Char(Last_day(REFERENCIA),'mm/yyyy') Mesano,
       To_Char(Last_day(REFERENCIA),'yyyymm') AnoMes,
       To_Char(Last_day(REFERENCIA),'yyyy') Ano, 1 Quantidade,
       VidaAtual,
       decode(condicaopneu
             ,'VE', 'VENDIDO'
             ,'CA', 'CARRO'
             ,'SU', 'SUCATA'
             ,'ES', 'ESTOQUE'
             ,'LI', 'LIXO'
             ,'MA', 'MANUTENCAO'
             , 'OUTROS') DescricaoCondicao

  From (
Select LpAd(Mov.CODIGOEMPRESA, 3,'0') || '/' || Lpad(Mov.CODIGOFL, 3,  '0') Empfil,
       Mov.Codigofogopneu NroFogo,
       Mov.Codigointpneu,
       Mov.Condicaopneu,
       Mov.Codigomodelopne || ' - ' || Mod.Descricaomodelopne modelo,
       Med.Descricaomedidapne medida,
       Mar.Descricaomarcapne marca,
       MOV.REFERENCIA,
       To_Char(Mov.Referencia, 'MM') Mes,
       FC_TRAZKMCUSTOPNEU(Mov.CodigoIntPneu, (SELECT MAX(NUMVIDATRPNEU) VIDA_ATUAL
                                                FROM PNE_TROCAPNEU
                                               WHERE CODIGOINTPNEU  = Mov.CodigoIntPneu
                                                 And DATATRPNEU < MOV.REFERENCIA), 'K', NULL, NULL, 'N') KM_ATUAL,
       (SELECT MAX(NUMVIDATRPNEU) VIDA_ATUAL FROM PNE_TROCAPNEU WHERE CODIGOINTPNEU  = Mov.CodigoIntPneu And DATATRPNEU < MOV.REFERENCIA) VidaAtual
  From (Select a.Valorcomprapneu,
                a.Codigofogopneu,
                a.Codigointpneu,
                a.Condicaopneu,
                a.Codigoempresa,
                a.Codigofl,
                a.Datasucatapneu Referencia,
                a.Codigomedidapne,
                a.Codigomodelopne,
                a.Codigomarcapne,
                0 Valorservico
           From Pne_Cadastropneu a
          Where a.Datasucatapneu Between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) And Sysdate
            And a.Incluinamediapneu = 'S'
            And a.Codigomedidapne = 35
         Union All
         Select 0 Valorcomprapneu,
                a.Codigofogopneu,
                a.Codigointpneu,
                a.Condicaopneu,
                a.Codigoempresa,
                a.Codigofl,
                c.Entradasaidanf Referencia,
                a.Codigomedidapne,
                a.Codigomodelopne,
                a.Codigomarcapne,
                0 Valorservico
           From Pne_Cadastropneu a, Pne_Vendapneu b, Bgm_Notafiscal c
          Where a.Codigointpneu = b.Codigointpneu
            And b.Codintnf = c.Codintnf
            And c.Entradasaidanf Between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) And Sysdate
            And a.Codigomedidapne = 35
         Union All
         Select 0 Valorcomprapneu,
                a.Codigofogopneu,
                a.Codigointpneu,
                a.Condicaopneu,
                a.Codigoempresa,
                a.Codigofl,
                b.Dataretornoservicoos Referencia,
                a.Codigomedidapne,
                a.Codigomodelopne,
                a.Codigomarcapne,
                b.Valorservicoos
           From Pne_Cadastropneu a, Pne_Servicos_Os b
          Where a.Codigointpneu = b.Codigointpneu
            And b.Dataretornoservicoos Between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) And Sysdate
            And b.Tiposervicoospneu In ('RE', 'RC')
            And b.Recusadoservos = 'N'
            And a.Codigomedidapne = 35
         ) Mov,
       Pne_Cadastromedida Med,
       Pne_Cadastromodelo Mod,
       Pne_Cadastromarca Mar
 Where Mov.Codigomedidapne = Med.Codigomedidapne
   And Mov.Codigomodelopne = Mod.Codigomodelopne
   And Mov.Codigomarcapne = Mar.Codigomarcapne
 Order By Mov.Codigoempresa,
          Mov.Codigofl,
          Mov.Codigomedidapne,
          Mov.Codigomodelopne,
          Mov.Codigomarcapne,
          Mov.Codigointpneu)
Group By  empfil, marca, Modelo, Medida, nroFogo, CondicaoPneu,
       To_Char(Last_day(REFERENCIA),'mm/yyyy'),
       To_Char(Last_day(REFERENCIA),'yyyymm'),
       To_Char(Last_day(REFERENCIA),'yyyy'),
       VidaAtual

