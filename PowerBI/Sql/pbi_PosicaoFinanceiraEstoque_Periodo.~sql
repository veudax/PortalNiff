create or replace view pbi_posicaofinancperiodo as
Select l.Codigoempresa,
       l.Codigofl,
       To_Char(m.Codigogrd, '000') Codigogrd,
       Min(To_Char(Mv.Datamovto, 'mm/yyyy')) MesanoMin,
       Max(To_Char(Mv.Datamovto, 'mm/yyyy')) MesanoMAx,
       m.Codigointernomaterial Material,
       im.codigomarcamat
  From Est_Cadmaterial    m,
       Ctr_Cadlocal       l,
       Est_Itensmovto     Im,
       Est_Movto          Mv,
       Est_Historicomovto h
 Where l.Codigoempresa || l.Codigofl In 11 --(11,12,21,31,41,51,61,91,131,261,263)
   And m.Codigogrd In (500, 510, 520, 540)
   And Im.Seqmovto = Mv.Seqmovto
   And Im.Datamovto = Mv.Datamovto
   And h.Codigohismov = Mv.Codigohismov
   And Im.Codigomatint = m.Codigomatint
   And Im.Codigolocal = l.Codigolocal
   And Mv.Codigoempresa = l.Codigoempresa
   And Mv.Codigofl = l.Codigofl
   And Mv.Datamovto Between Add_Months(Trunc(Sysdate, 'rr'), -36) And
       Sysdate
   And 'EN,EI' Like '%' || h.Tipohismov || '%' -- muda conforme tipo entrada, saida e transferencia
 Group By l.Codigoempresa,
          l.Codigofl,
          To_Char(m.Codigogrd, '000'),
          To_Char(Mv.Datamovto, 'yyyy'),
          m.Codigointernomaterial,
          im.codigomarcamat

