Create Or Replace View pbi_radar_manut_Km As
   Select Lpad(b.codigoEmpresa,3,'0') || '/' || lPad(b.codigoFl,3,'0') EmpFil,
         last_day(b.dataveloc) data,
         0 saldoAcumulado,
         sum(b.kmpercorridoveloc) Km
    From bgm_velocimetro b
   Where b.dataveloc Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz
     And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
     And b.codigoempresa || b.codigofl In (11,12,21,31,41,51,61,91,13,261,263)
   Group By b.dataveloc, Lpad(b.codigoEmpresa,3,'0') || '/' || lPad(b.codigoFl,3,'0')
