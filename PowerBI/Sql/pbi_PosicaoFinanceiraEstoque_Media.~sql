create or replace view pbi_posicaofinestoque_media as
Select EmpFil, GrupoDespesa, Material,
       Sum(MediaAnoAtual) MediaAnoAtual,
       Sum(Media1Antes) Media1Antes,
       Sum(Media2AnoAntes) Media2AnoAntes,
       Sum(Media3AnosAntes) Media3AnosAntes
  From (Select Lpad(CODIGOEMPRESA,3,'0') || '/' || Lpad(CODIGOFL,3,'0') EmpFil,
               a.CODIGOGRD || '-' || d.descricaogrd GrupoDespesa,
               Material,
               Ano,
               Decode(Ano, To_Char(Sysdate,'yyyy'), Round(Sum(ValorEntrada)/Sum(Qtd),2), 0) MediaAnoAtual,
               Decode(Ano, To_Char(Sysdate,'yyyy')-1, Round(Sum(ValorEntrada)/Sum(Qtd),2), 0) Media1Antes,
               Decode(Ano, To_Char(Sysdate,'yyyy')-2, Round(Sum(ValorEntrada)/Sum(Qtd),2), 0) Media2AnoAntes,
               Decode(ano, To_Char(Sysdate,'yyyy')-3, Round(Sum(ValorEntrada)/Sum(Qtd),2), 0) Media3AnosAntes,
               Sum(qtd) qtd
          From est_grupodespesas d
             , (Select nvl(Sum(im.valoritensmovto),0) ValorEntrada,
                       Count(*) Qtd,
                       L.CODIGOEMPRESA,
                       L.CODIGOFL,
                       TO_CHAR(M.CODIGOGRD  ,'000') CODIGOGRD,
                       To_Char( mv.datamovto,'mm/yyyy') mesAno,
                       To_Char( mv.datamovto,'yyyymm') AnoMes,
                       To_Char( mv.datamovto,'yyyy') Ano,
                       m.codigointernomaterial || ' - ' || m.descricaomat Material
                  From EST_CADMATERIAL    M,
                       CTR_CADLOCAL       L,
                       Est_Itensmovto IM,
                       Est_movto MV,
                       Est_Historicomovto H
                 Where L.CODIGOEMPRESA || L.CODIGOFL In (11,12,21,31,41,51,61,91,131,261,263)
                   And M.CODIGOGRD In (500, 510, 520, 540)
                   And IM.Seqmovto = MV.Seqmovto
                   And IM.Datamovto = Mv.Datamovto
                   And H.Codigohismov = MV.Codigohismov
                   And IM.Codigomatint = m.codigomatint
                   And im.codigolocal = l.codigolocal
                   And mv.codigoempresa = l.codigoempresa
                   And mv.codigofl = l.codigofl
                   And mv.datamovto Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) And Sysdate
                   And 'EN,EI' Like '%' || h.tipohismov || '%' -- muda conforme tipo entrada, saida e transferencia
                 Group By L.CODIGOEMPRESA,
                          L.CODIGOFL,
                          TO_CHAR(M.CODIGOGRD  ,'000') ,
                          To_Char( mv.datamovto,'mm/yyyy'),
                          To_Char( mv.datamovto,'yyyymm'),
                          To_Char( mv.datamovto,'yyyy'),
                          m.codigointernomaterial || ' - ' || m.descricaomat ) a
         Where d.codigogrd = a.codigogrd
         Group By CODIGOEMPRESA,
                  Lpad(CODIGOEMPRESA,3,'0') || '/' || Lpad(CODIGOFL,3,'0'),
                  a.CODIGOGRD || '-' || d.descricaogrd,
                  Material,
                  ano )
 Group By  EmpFil, GrupoDespesa, Material
 Having Sum(MediaAnoAtual) > 0 And
        Sum(Media1Antes) > 0 And
        Sum(Media2AnoAntes) > 0 And
        Sum(Media3AnosAntes) > 0

