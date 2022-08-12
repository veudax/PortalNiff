Select t.*,
       t.Rowid,
       c.Numero,
       Trunc((Mod(Datafim - Datainicio, 1) * 24) * 60) Emminutos,
       Trunc((Mod(Sysdate - Datainicio, 1) * 24) * 60) Minutos,
       c.status,
       Sysdate
  From Niff_Chm_Tempoexecucao t, Niff_Chm_Chamado c
 Where c.Idchamado = t.Idchamado
 Order By Datainicio Desc