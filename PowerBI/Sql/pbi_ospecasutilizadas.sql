create or replace view pbi_ospecasutilizadas as
Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.kmexecucaoos, o.codigoveic, x.km,
       cm.descricaomat PecasUtilizadas, cm.codigointernomaterial, x.Marca, cm.codigomatint,
       Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
       o.codigoempresa, o.codigofl,
       o.dataaberturaos
  From Man_Os o
     , Man_Osprevisto p
     , EST_REQUISICAO r
     , EST_ITENSREQUISICAO i
     , EST_MOVTO m
     , EST_ITENSMOVTO ie
     , EST_CADMATERIAL cm
     , (Select codigoveic, x.Marca, x.KM From pbi_veiculosepecasporkm x Where x.KM = 5000) x
 Where o.codintos = p.codintos
   And o.codigoempresa = 1
   And o.codigoFl = 1
   And o.condicaoos = 'FC'
   And p.codigoplanrev = 194
   And o.tipoos = 'P'
   And r.codintos = o.codintos
   And r.numerorq = i.numerorq
   And r.numerorq = m.numerorq
   And ie.datamovto = m.datamovto
   And ie.seqmovto = m.seqmovto
   And cm.codigomatint = ie.codigomatint
   And o.codigoveic = x.codigoveic
   And o.dataaberturaos In (Select Data From niff_calendario
                             Where Quadrimestre = '3 Quadrimestre'
                               And Data Between '01-jan-2018' And Sysdate)
 Union All
Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.kmexecucaoos, o.codigoveic, x.km,
       cm.descricaomat PecasUtilizadas, cm.codigointernomaterial, x.Marca,cm.codigomatint,
       Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
       o.codigoempresa, o.codigofl,
       o.dataaberturaos
  From Man_Os o
     , Man_Osprevisto p
     , EST_REQUISICAO r
     , EST_ITENSREQUISICAO i
     , EST_MOVTO m
     , EST_ITENSMOVTO ie
     , EST_CADMATERIAL cm
     , (Select codigoveic, x.Marca, x.KM From pbi_veiculosepecasporkm x Where x.KM = 15000) x
 Where o.codintos = p.codintos
   And o.codigoempresa = 1
   And o.codigoFl = 1
   And o.condicaoos = 'FC'
   And o.tipoos = 'P'
   And p.codigoplanrev = 195
   And r.codintos = o.codintos
   And r.numerorq = i.numerorq
   And r.numerorq = m.numerorq
   And ie.datamovto = m.datamovto
   And ie.seqmovto = m.seqmovto
   And cm.codigomatint = ie.codigomatint
   And o.codigoveic = x.codigoveic
   And o.dataaberturaos In (Select Data From niff_calendario
                             Where Quadrimestre = '3 Quadrimestre'
                               And Data Between '01-jan-2018' And Sysdate)
 Union All
Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.kmexecucaoos, o.codigoveic, x.KM,
       cm.descricaomat PecasUtilizadas, cm.codigointernomaterial, x.Marca,cm.codigomatint,
       Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
       o.codigoempresa, o.codigofl,
       o.dataaberturaos
  From Man_Os o
     , Man_Osprevisto p
     , EST_REQUISICAO r
     , EST_ITENSREQUISICAO i
     , EST_MOVTO m
     , EST_ITENSMOVTO ie
     , EST_CADMATERIAL cm
     , (Select codigoveic, x.Marca, x.KM From pbi_veiculosepecasporkm x Where x.KM = 30000) x
 Where o.codintos = p.codintos
   And o.codigoempresa = 1
   And o.codigoFl = 1
   And o.condicaoos = 'FC'
   And o.tipoos = 'P'
   And p.codigoplanrev = 196
--   And o.codintos = 1461401
   And r.codintos = o.codintos
   And r.numerorq = i.numerorq
   And r.numerorq = m.numerorq
   And ie.datamovto = m.datamovto
   And ie.seqmovto = m.seqmovto
   And cm.codigomatint = ie.codigomatint
   And o.codigoveic = x.codigoveic
   And o.dataaberturaos In (Select Data From niff_calendario
                             Where Quadrimestre = '3 Quadrimestre'
                               And Data Between '01-jan-2018' And Sysdate)

