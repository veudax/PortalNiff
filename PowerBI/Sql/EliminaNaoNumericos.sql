create or replace function EliminaNaoNumericos(pString In Varchar2) Return varchar2 is
  vString varchar2(1000);
  vContador Integer;
  vNumeros varchar2(10);
  vPos INTEGER;
  vTamanho Integer;
  vCaracter varchar2(1);

begin

  If pString Is Null Then
    Return(Null);
  End If;

  vString := '';
  vNumeros := '0123456789';

  vTamanho := Length(pString);

  For vContador In 1 .. vTamanho Loop
    vCaracter := Substr(pString, vContador, 1);

    FOR vPos IN 1 .. 10 LOOP

      If vCaracter =  Substr(vNumeros, vPos, 1) THEN
--        vCaracter := Substr(vNumeros, vPos, 1);
        vString := vString || vCaracter;
        exit;
      End If;
    END LOOP;

  End Loop;

  IF vString = '' THEN
    vString := pString;
  END IF;

  return(vString);
end;


