create or replace view pbi_custoveiculossubstituidos as
Select prefixoveic,
      EmpFil,
      numerorq ,
      material,
      descricaomat,
      numeroos,
      datarq,
      mesano,
      anoMes,
      Ano,
      vlUnitario,
      Valor,
      Valor/50 media,
      Qtd
From (

  Select v.prefixoveic,
           lpad(e.codigoempresa, 3, '0') || '/' || lPad(e.Codigofl, 3, '0') EmpFil,
           e.numerorq ,
           m.codigointernomaterial || '-' || m.descricaomat material,
           m.descricaomat,
           o.numeroos,
           e.datarq,
           To_char(e.datarq,'mm/yyyy') mesano,
           To_char(e.datarq,'yyyymm') anoMes,
           To_char(e.datarq,'yyyy') ano,
           im.valoritensmovto vlUnitario,
           im.valortotalitensmovto Valor,
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
       And Upper(m.descricaomat) Not Like Upper('%Pneu%')
       And m.codigogrd In (500,510)
       And v.prefixoveic In ('0030443'	,	'0030484'	,
'0030444'	,	'0030486'	,
'0030445'	,	'0030487'	,
'0030446'	,	'0030488'	,
'0030447'	,	'0030489'	,
'0030448'	,	'0030501'	,
'0030449'	,	'0030502'	,
'0030460'	,	'0030503'	,
'0030462'	,	'0030504'	,
'0030463'	,	'0030505'	,
'0030464'	,	'0030506'	,
'0030465'	,	'0030508'	,
'0030466'	,	'0030509'	,
'0030467'	,	'0030510'	,
'0030469'	,	'0030511'	,
'0030470'	,	'0030512'	,
'0030472'	,	'0030513'	,
'0030473'	,	'0030514'	,
'0030474'	,	'0030515'	,
'0030476'	,	'0030516'	,
'0030477'	,	'0030517'	,
'0030478'	,	'0030518'	,
'0030480'	,	'0030519'	)
       And e.datarq Between (ADD_MONTHS(LAST_DAY(trunc(Sysdate)), -13)+1) And (ADD_MONTHS(LAST_DAY(trunc(Sysdate)), -1)) )

Group By prefixoveic,
      EmpFil,
      numerorq ,
      material,
      descricaomat,
      numeroos,
      datarq,
      mesano,
      anoMes,
      Ano,
      vlUnitario,
      Valor,
      Qtd


--Grant Select On globus.Pbi_CustoVeiculosSubstituidos To AcessoBi With Grant Option

