select t.*, :TipoFolha as tipoFolha, :DataInicial as datainicial, :DataFinal as datafinal,
decode(data1, null, null, mesextenso(to_char(data1,'MM')) || '/' || to_char(data1,'yyyy')) mes1,
decode(data2, null, null, mesextenso(to_char(data2,'MM')) || '/' || to_char(data2,'yyyy')) mes2,
decode(data3, null, null, mesextenso(to_char(data3,'MM')) || '/' || to_char(data3,'yyyy')) mes3,
decode(data4, null, null, mesextenso(to_char(data4,'MM')) || '/' || to_char(data4,'yyyy')) mes4,
decode(data5, null, null, mesextenso(to_char(data5,'MM')) || '/' || to_char(data5,'yyyy')) mes5,
decode(data6, null, null, mesextenso(to_char(data6,'MM')) || '/' || to_char(data6,'yyyy')) mes6,
decode(data7, null, null, mesextenso(to_char(data7,'MM')) || '/' || to_char(data7,'yyyy')) mes7,
decode(data8, null, null, mesextenso(to_char(data8,'MM')) || '/' || to_char(data8,'yyyy')) mes8,
decode(data9, null, null, mesextenso(to_char(data9,'MM')) || '/' || to_char(data9,'yyyy')) mes9,
decode(data10, null, null, mesextenso(to_char(data10,'MM')) || '/' || to_char(data10,'yyyy')) mes10,
decode(data11, null, null, mesextenso(to_char(data11,'MM')) || '/' || to_char(data11,'yyyy')) mes11,
decode(data12, null, null, mesextenso(to_char(data12,'MM')) || '/' || to_char(data12,'yyyy')) mes12
from niff_flprelhistfinc t, flp_funcionarios f
where ip = func_trazterminal
and t.codigoempresa = :Empresa
and t.codigofl = :Filial
and f.codfunc between :RegInicial and :RegFinal
--and t.data1 between :DataInicial and :DataFinal
and f.codintfunc = t.codintfunc