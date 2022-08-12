create or replace function IIF(pCondicao In boolean, pVerdadeiro in varchar2, pFalso in varchar2) Return varchar2 is
begin
  begin
    if pCondicao then
      return(pVerdadeiro);
    else
      return(pFalso);
    end if;
  exception when others then
      return(pFalso);
  end;
end;


