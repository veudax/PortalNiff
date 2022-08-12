create or replace function Pos(pPalavra In Varchar2, pTexto in Varchar2) Return NUMBER is

  vContador       NUMBER;
  vTamanhoTexto   NUMBER;
  vTamanhoPalavra NUMBER;

  vPalavra2 	  Long;

begin

  If pPalavra Is Null Then
    Return(Null);
  End If;

  vContador       := 1;
  vTamanhoTexto   := Length(pTexto);
  vTamanhoPalavra := Length(pPalavra);

  While vContador <= vTamanhoTexto Loop

    vPalavra2 := Substr(pTexto, vContador, vTamanhoPalavra);


    If upper(trim(vPalavra2)) =  upper(trim(pPalavra)) Then
      Return (vContador);
      exit;
    End If;

    INC(vContador);
  End Loop;

  return(0);
end;


