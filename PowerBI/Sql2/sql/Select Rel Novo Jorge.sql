Create Or Replace View pbi_RetiradaMateriaisPorOS As
  Select v.prefixoveic,  
         lpad(e.codigoempresa, 3, '0') || '/' || lPad(e.Codigofl, 3, '0') EmpFil,
         e.numerorq , 
         m.codigointernomaterial || '-' || m.descricaomat material, 
         o.numeroos, 
         e.datarq, 
         To_char(e.datarq,'mm/yyyy') mesano,
         im.valortotalitensmovto * im.qtdeitensmovto Valor, 
         im.qtdeitensmovto Qtd
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
     And e.datarq Between Trunc(SYSDATE,'rr') And Sysdate     
