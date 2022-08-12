create or replace view pbi_retiradamateriaisporos as
Select prefixoveic,
      EmpFil,
      numerorq ,
      material,
      descricaomat,
      numeroos,
      datarq,
      mesano,
      vlUnitario,
      Valor,
      Qtd,
      Km
From (

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
           --Round(pbi_Fc_KmMedioTroca(e.codigoempresa, e.codigofl, v.Codigoveic, m.codigomatint, To_char(e.datarq,'yyyymm')),3) KmMedio
           (Select Max(Round(b.Kmacumuladoveloc))
              From Bgm_Velocimetro b
             Where Dataveloc = (Select Max(b.Dataveloc)  From Bgm_Velocimetro b
                                 Where b.dataveloc <= e.datarq
                                   And Codigoveic = v.codigoveic
                                   And b.Codigoempresa = v.codigoempresa
                                   And b.Codigofl = v.codigofl)
               And Codigoveic = v.codigoveic
               And b.Codigoempresa = v.codigoempresa
               And b.Codigofl = v.codigofl) Km
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
       And e.datarq Between (ADD_MONTHS(LAST_DAY(trunc(Sysdate)), -13)+1) And Sysdate )

Group By prefixoveic,
      EmpFil,
      numerorq ,
      material,
      descricaomat,
      numeroos,
      datarq,
      mesano,
      vlUnitario,
      Valor,
      Qtd,
      Km
Having km >  0

