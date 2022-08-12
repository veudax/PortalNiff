create or replace view pbi_consultapneus as
Select d.EMPFIL, d.MARCA, d.MODELO, d.MEDIDA, d.NROFOGO, d.CONDICAOPNEU, d.TOTAL_KM, Last_day(d.Data) Data, d.TIPO,
       d.TipoServicos, d.Recusadoservos, d.Recapador,
       To_Char(Last_day(Data),'mm/yyyy') Mesano,
       To_Char(Last_day(Data),'yyyymm') AnoMes,
       To_Char(Last_day(Data),'yyyy') Ano,
       Sum(d.KM) Km,
       1 Quantidade
  From (
Select
  LpAd(A.CODIGOEMPRESA, 3,'0') || '/' || Lpad(A.CODIGOFL, 3,  '0') Empfil,
  B.DESCRICAOMARCAPNE Marca,
  C.CODIGOMODELOPNE || ' - ' || C.DESCRICAOMODELOPNE modelo,
  D.DESCRICAOMEDIDAPNE medida,
  A.CODIGOFOGOPNEU NroFogo,
  A.CONDICAOPNEU,
  F.Rsocialforn         Recapador,
  FC_TRAZKMCUSTOPNEU(A.CODIGOINTPNEU,99,'K',NULL, TO_DATE('31/12/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') ,'N' ,'A' ) TOTAL_KM,
  fc_pbi_DataPneus(0,A.CODIGOINTPNEU) Data,  'NOVO' Tipo,
  Fc_Trazkmcustopneu(A.Codigointpneu, 0, 'K', Null, Null, 'NCD') Km,
  Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs) Tiposerv,
  Decode(SOs.Recusadoservos
        , 'S', 'Recusado'
        , Decode(Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs)
        , 'CO', 'Conserto'
        , 'RL', 'Reclama��o'
        , 'RS', 'Ressulcagem'
        , 'GA', 'Garantia'
        , 'RE', 'Recapagem'
        , 'Outros')) TipoServicos,
  SOs.Recusadoservos
FROM
  Pne_Cadastropneu    A,
  Pne_Cadastromarca   B,
  Pne_Cadastromodelo  C,
  Pne_Cadastromedida  D,
  Pne_Os              O,
  Pne_Servicos_Os     SOs,
  Bgm_Notafiscal      Nf,
  Bgm_Fornecedor      F
WHERE A.Codigomarcapne   = B.Codigomarcapne
  and A.Codigomodelopne  = C.Codigomodelopne
  and A.Codigomedidapne  = D.Codigomedidapne
  And O.Codigoforn       = F.Codigoforn
  And SOs.Codintnf       = Nf.Codintnf(+)
  And SOs.Codigointpneu  = A.Codigointpneu
  And O.Codigofl         = SOs.Codigofl
  And O.Codigoempresa    = SOs.Codigoempresa
  And O.Numeroospneu     = SOs.Numeroospneu
  And d.Codigomedidapne = 35
  and (O.Dataospneu BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS')  and Sysdate
   or  SOs.DataretornoservicOos BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') And Sysdate )
  And ((SOs.Recusadoservos In ('N', 'S')) Or
       (Sos.Recusadoservos Is Null))
Union All
Select
  LpAd(A.CODIGOEMPRESA, 3,'0') || '/' || Lpad(A.CODIGOFL, 3,  '0') Empfil,
  B.DESCRICAOMARCAPNE Marca,
  C.CODIGOMODELOPNE || ' - ' || C.DESCRICAOMODELOPNE modelo,
  D.DESCRICAOMEDIDAPNE medida,
  A.CODIGOFOGOPNEU NroFogo,
  A.CONDICAOPNEU,
  F.Rsocialforn         Recapador,
  FC_TRAZKMCUSTOPNEU(A.CODIGOINTPNEU,99,'K',NULL, TO_DATE('31/12/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') ,'N' ,'A' ) TOTAL_KM,
  fc_pbi_DataPneus(0,A.CODIGOINTPNEU) Data,  '1�' Tipo,
  Fc_Trazkmcustopneu(A.Codigointpneu, 1, 'K', Null, Null, 'NCD') Km,
  Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs) Tiposerv,
  Decode(SOs.Recusadoservos
        , 'S', 'Recusado'
        , Decode(Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs)
        , 'CO', 'Conserto'
        , 'RL', 'Reclama��o'
        , 'RS', 'Ressulcagem'
        , 'GA', 'Garantia'
        , 'RE', 'Recapagem'
        , 'Outros')) TipoServicos,
  SOs.Recusadoservos
FROM
  Pne_Cadastropneu    A,
  Pne_Cadastromarca   B,
  Pne_Cadastromodelo  C,
  Pne_Cadastromedida  D,
  Pne_Os              O,
  Pne_Servicos_Os     SOs,
  Bgm_Notafiscal      Nf,
  Bgm_Fornecedor      F
WHERE A.Codigomarcapne   = B.Codigomarcapne
  and A.Codigomodelopne  = C.Codigomodelopne
  and A.Codigomedidapne  = D.Codigomedidapne
  And O.Codigoforn       = F.Codigoforn
  And SOs.Codintnf       = Nf.Codintnf(+)
  And SOs.Codigointpneu  = A.Codigointpneu
  And O.Codigofl         = SOs.Codigofl
  And O.Codigoempresa    = SOs.Codigoempresa
  And O.Numeroospneu     = SOs.Numeroospneu
  And d.Codigomedidapne = 35
  and (O.Dataospneu BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS')  and Sysdate
   or  SOs.DataretornoservicOos BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') And Sysdate )
  And ((SOs.Recusadoservos In ('N', 'S')) Or
       (Sos.Recusadoservos Is Null))
 Union All
Select
  LpAd(A.CODIGOEMPRESA, 3,'0') || '/' || Lpad(A.CODIGOFL, 3,  '0') Empfil,
  B.DESCRICAOMARCAPNE Marca,
  C.CODIGOMODELOPNE || ' - ' || C.DESCRICAOMODELOPNE modelo,
  D.DESCRICAOMEDIDAPNE medida,
  A.CODIGOFOGOPNEU NroFogo,
  A.CONDICAOPNEU,
  F.Rsocialforn         Recapador,
  FC_TRAZKMCUSTOPNEU(A.CODIGOINTPNEU,99,'K',NULL, TO_DATE('31/12/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') ,'N' ,'A' ) TOTAL_KM,
  fc_pbi_DataPneus(0,A.CODIGOINTPNEU) Data,  '2�' Tipo,
  Fc_Trazkmcustopneu(A.Codigointpneu, 2, 'K', Null, Null, 'NCD') Km,
  Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs) Tiposerv,
  Decode(SOs.Recusadoservos
        , 'S', 'Recusado'
        , Decode(Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs)
        , 'CO', 'Conserto'
        , 'RL', 'Reclama��o'
        , 'RS', 'Ressulcagem'
        , 'GA', 'Garantia'
        , 'RE', 'Recapagem'
        , 'Outros')) TipoServicos,
  SOs.Recusadoservos
FROM
  Pne_Cadastropneu    A,
  Pne_Cadastromarca   B,
  Pne_Cadastromodelo  C,
  Pne_Cadastromedida  D,
  Pne_Os              O,
  Pne_Servicos_Os     SOs,
  Bgm_Notafiscal      Nf,
  Bgm_Fornecedor      F
WHERE A.Codigomarcapne   = B.Codigomarcapne
  and A.Codigomodelopne  = C.Codigomodelopne
  and A.Codigomedidapne  = D.Codigomedidapne
  And O.Codigoforn       = F.Codigoforn
  And SOs.Codintnf       = Nf.Codintnf(+)
  And SOs.Codigointpneu  = A.Codigointpneu
  And O.Codigofl         = SOs.Codigofl
  And O.Codigoempresa    = SOs.Codigoempresa
  And O.Numeroospneu     = SOs.Numeroospneu
  And d.Codigomedidapne = 35
  and (O.Dataospneu BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS')  and Sysdate
   or  SOs.DataretornoservicOos BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') And Sysdate )
  And ((SOs.Recusadoservos In ('N', 'S')) Or
       (Sos.Recusadoservos Is Null))
Union All
Select
  LpAd(A.CODIGOEMPRESA, 3,'0') || '/' || Lpad(A.CODIGOFL, 3,  '0') Empfil,
  B.DESCRICAOMARCAPNE Marca,
  C.CODIGOMODELOPNE || ' - ' || C.DESCRICAOMODELOPNE modelo,
  D.DESCRICAOMEDIDAPNE medida,
  A.CODIGOFOGOPNEU NroFogo,
  A.CONDICAOPNEU,
  F.Rsocialforn         Recapador,
  FC_TRAZKMCUSTOPNEU(A.CODIGOINTPNEU,99,'K',NULL, TO_DATE('31/12/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') ,'N' ,'A' ) TOTAL_KM,
  fc_pbi_DataPneus(0,A.CODIGOINTPNEU) Data,  '3�' Tipo,
  Fc_Trazkmcustopneu(A.Codigointpneu, 3, 'K', Null, Null, 'NCD') Km,
  Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs) Tiposerv,
  Decode(SOs.Recusadoservos
        , 'S', 'Recusado'
        , Decode(Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs)
        , 'CO', 'Conserto'
        , 'RL', 'Reclama��o'
        , 'RS', 'Ressulcagem'
        , 'GA', 'Garantia'
        , 'RE', 'Recapagem'
        , 'Outros')) TipoServicos,
  SOs.Recusadoservos
FROM
  Pne_Cadastropneu    A,
  Pne_Cadastromarca   B,
  Pne_Cadastromodelo  C,
  Pne_Cadastromedida  D,
  Pne_Os              O,
  Pne_Servicos_Os     SOs,
  Bgm_Notafiscal      Nf,
  Bgm_Fornecedor      F
WHERE A.Codigomarcapne   = B.Codigomarcapne
  and A.Codigomodelopne  = C.Codigomodelopne
  and A.Codigomedidapne  = D.Codigomedidapne
  And O.Codigoforn       = F.Codigoforn
  And SOs.Codintnf       = Nf.Codintnf(+)
  And SOs.Codigointpneu  = A.Codigointpneu
  And O.Codigofl         = SOs.Codigofl
  And O.Codigoempresa    = SOs.Codigoempresa
  And O.Numeroospneu     = SOs.Numeroospneu
  And d.Codigomedidapne = 35
  and (O.Dataospneu BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS')  and Sysdate
   or  SOs.DataretornoservicOos BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') And Sysdate )
  And ((SOs.Recusadoservos In ('N', 'S')) Or
       (Sos.Recusadoservos Is Null))
Union All
Select
  LpAd(A.CODIGOEMPRESA, 3,'0') || '/' || Lpad(A.CODIGOFL, 3,  '0') Empfil,
  B.DESCRICAOMARCAPNE Marca,
  C.CODIGOMODELOPNE || ' - ' || C.DESCRICAOMODELOPNE modelo,
  D.DESCRICAOMEDIDAPNE medida,
  A.CODIGOFOGOPNEU NroFogo,
  A.CONDICAOPNEU,
  F.Rsocialforn         Recapador,
  FC_TRAZKMCUSTOPNEU(A.CODIGOINTPNEU,99,'K',NULL, TO_DATE('31/12/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') ,'N' ,'A' ) TOTAL_KM,
  fc_pbi_DataPneus(0,A.CODIGOINTPNEU) Data,  '4�' Tipo,
  Fc_Trazkmcustopneu(A.Codigointpneu, 4, 'K', Null, Null, 'NCD') Km,
  Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs) Tiposerv,
  Decode(SOs.Recusadoservos
        , 'S', 'Recusado'
        , Decode(Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs)
        , 'CO', 'Conserto'
        , 'RL', 'Reclama��o'
        , 'RS', 'Ressulcagem'
        , 'GA', 'Garantia'
        , 'RE', 'Recapagem'
        , 'Outros')) TipoServicos,
  SOs.Recusadoservos
FROM
  Pne_Cadastropneu    A,
  Pne_Cadastromarca   B,
  Pne_Cadastromodelo  C,
  Pne_Cadastromedida  D,
  Pne_Os              O,
  Pne_Servicos_Os     SOs,
  Bgm_Notafiscal      Nf,
  Bgm_Fornecedor      F
WHERE A.Codigomarcapne   = B.Codigomarcapne
  and A.Codigomodelopne  = C.Codigomodelopne
  and A.Codigomedidapne  = D.Codigomedidapne
  And O.Codigoforn       = F.Codigoforn
  And SOs.Codintnf       = Nf.Codintnf(+)
  And SOs.Codigointpneu  = A.Codigointpneu
  And O.Codigofl         = SOs.Codigofl
  And O.Codigoempresa    = SOs.Codigoempresa
  And O.Numeroospneu     = SOs.Numeroospneu
  And d.Codigomedidapne = 35
  and (O.Dataospneu BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS')  and Sysdate
   or  SOs.DataretornoservicOos BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') And Sysdate )
  And ((SOs.Recusadoservos In ('N', 'S')) Or
       (Sos.Recusadoservos Is Null))
Union All
Select
  LpAd(A.CODIGOEMPRESA, 3,'0') || '/' || Lpad(A.CODIGOFL, 3,  '0') Empfil,
  B.DESCRICAOMARCAPNE Marca,
  C.CODIGOMODELOPNE || ' - ' || C.DESCRICAOMODELOPNE modelo,
  D.DESCRICAOMEDIDAPNE medida,
  A.CODIGOFOGOPNEU NroFogo,
  A.CONDICAOPNEU,
  F.Rsocialforn         Recapador,
  FC_TRAZKMCUSTOPNEU(A.CODIGOINTPNEU,99,'K',NULL, TO_DATE('31/12/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') ,'N' ,'A' ) TOTAL_KM,
  fc_pbi_DataPneus(0,A.CODIGOINTPNEU) Data,  '5�' Tipo,
  Fc_Trazkmcustopneu(A.Codigointpneu, 5, 'K', Null, Null, 'NCD') Km,
  Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs) Tiposerv,
  Decode(SOs.Recusadoservos
        , 'S', 'Recusado'
        , Decode(Decode(SOs.Tiposervico2servicoOs, Null, SOs.Tiposervicoospneu, SOs.Tiposervico2servicoOs)
        , 'CO', 'Conserto'
        , 'RL', 'Reclama��o'
        , 'RS', 'Ressulcagem'
        , 'GA', 'Garantia'
        , 'RE', 'Recapagem'
        , 'Outros')) TipoServicos,
  SOs.Recusadoservos
FROM
  Pne_Cadastropneu    A,
  Pne_Cadastromarca   B,
  Pne_Cadastromodelo  C,
  Pne_Cadastromedida  D,
  Pne_Os              O,
  Pne_Servicos_Os     SOs,
  Bgm_Notafiscal      Nf,
  Bgm_Fornecedor      F
WHERE A.Codigomarcapne   = B.Codigomarcapne
  and A.Codigomodelopne  = C.Codigomodelopne
  and A.Codigomedidapne  = D.Codigomedidapne
  And O.Codigoforn       = F.Codigoforn
  And SOs.Codintnf       = Nf.Codintnf(+)
  And SOs.Codigointpneu  = A.Codigointpneu
  And O.Codigofl         = SOs.Codigofl
  And O.Codigoempresa    = SOs.Codigoempresa
  And O.Numeroospneu     = SOs.Numeroospneu
  And d.Codigomedidapne = 35
  and (O.Dataospneu BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS')  and Sysdate
   or  SOs.DataretornoservicOos BETWEEN TO_DATE('01/01/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') And Sysdate )
  And ((SOs.Recusadoservos In ('N', 'S')) Or
       (Sos.Recusadoservos Is Null)) ) D
 Where Km > 0
Group By d.EMPFIL, d.MARCA, d.MODELO, d.MEDIDA, d.NROFOGO, d.CONDICAOPNEU, d.TOTAL_KM, Last_day(d.Data), d.TIPO,
       d.TipoServicos, d.Recusadoservos, d.Recapador,
       To_Char(Last_day(Data),'mm/yyyy'),
       To_Char(Last_day(Data),'yyyymm'),
       To_Char(Last_day(Data),'yyyy')

