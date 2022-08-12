CREATE OR REPLACE Function TiraEComercial(pString In Varchar2) Return varchar2 is

  vString varchar2(1000);
  vCaracteresEspeciais varchar2(60);
  vContador Integer;
  vPos INTEGER;
  vTamanho Integer;
  vCaracter varchar2(1);

Begin

  If pString Is Null Then
    Return(Null);
  End If;

  vString              := '';
  vCaracteresEspeciais := '&''"';
  vTamanho             := Length(pString);

  For vContador In 1 .. vTamanho Loop
    vCaracter := Substr(pString, vContador, 1);
    FOR vPos IN 1 .. 60 LOOP
      If vCaracter =  Substr(vCaracteresEspeciais, vPos, 1) THEN
        vCaracter := Substr('', vPos, 1);
      End If;
    END LOOP;

    vString := vString || vCaracter;
  End Loop;

  return(vString);

End;
/
