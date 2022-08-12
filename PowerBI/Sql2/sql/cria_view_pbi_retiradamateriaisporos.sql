create or replace view pbi_retiradamateriaisporos as
Select v.prefixoveic,
         lpad(e.codigoempresa, 3, '0') || '/' || lPad(e.Codigofl, 3, '0') EmpFil,
         e.numerorq ,
         m.codigointernomaterial || '-' || m.descricaomat material,
         m.descricaomat,
         o.numeroos,
         e.datarq,
         To_char(e.datarq,'mm/yyyy') mesano,
         im.valoritensmovto vlUnitario,
         im.valortotalitensmovto Valor,
         im.qtdeitensmovto Qtd,
         Round(pbi_Fc_KmMedioTroca(e.codigoempresa, e.codigofl, v.Codigoveic, m.codigomatint, To_char(e.datarq,'yyyymm')),3) KmMedio
    From frt_cadveiculos v
       , est_requisicao e
       , EST_ITENSREQUISICAO i
       , Est_Cadmaterial m
       , man_os o
       , Est_Itensmovto im
       , Est_Movto mv
   Where i.numerorq = e.numerorq
     And e.codigoveic = v.codigoveic
     And i.codigomatint = m.codigomatint
     And o.codintos = e.codintos
     And e.codintos Is Not Null
     And im.datamovto = mv.datamovto
     And im.seqmovto = mv.seqmovto
     And mv.numerorq = e.numerorq
     And im.codigomatint = m.codigomatint
     And im.codigomarcamat = i.codigomarcamat
     And Upper(m.descricaomat) Not Like Upper('%Pneu%')
     And e.datarq Between Trunc(SYSDATE,'rr') And Sysdate
