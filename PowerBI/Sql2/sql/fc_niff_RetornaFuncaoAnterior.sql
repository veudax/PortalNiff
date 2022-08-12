CREATE OR REPLACE Function fc_Niff_RetornaFuncaoAnterior (CodigoInterno In Number, AnoMes in  Number ) Return Number Is
  area Number;
Begin

  begin
    Select CodFuncao
      Into area
      From Flp_Historicosalarial
     Where Dthistsal = (Select Max(Dthistsal)
                          From Flp_Historicosalarial x
                         Where To_number(To_Char(Dthistsal,'yyyymm')) <= AnoMes
                           --And Dthistsalant Is Not Null
                           And x.Statushistsal = 'N'
                           And Codintfunc = CodigoInterno)
       --And Dthistsalant Is Not Null
       And Statushistsal = 'N'
       And Codintfunc = CodigoInterno;
  exception
    when no_data_found then
      Select CodFuncao
        Into Area
        From PBI_vwFuncionarios
       Where CodIntFunc = CodigoInterno;
  end;
  

  Return area;
End;
