Select a.dataferiado, f.dataferiado, f.codigoempresa, f.codigofl
  From FINFERIA_EMPRESAFILIAL f, Finferia a
Where f.dataferiado(+) = a.dataferiado
  And f.codigoempresa =2
  And f.codigofl =1
  And a.dataferiado > '31-dec-2017'