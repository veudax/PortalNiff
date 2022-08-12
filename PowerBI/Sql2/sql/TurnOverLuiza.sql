         select                lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
              , f.NOMEFUNC
              , f.CODAREA
              , f.DESCFUNCAO
              , Last_day((Case When trunc(ff.competficha) > Trunc(Sysdate) then Trunc(Sysdate) Else trunc(ff.competficha) End)) data
           from vw_funcionarios f, flp_fichafinanceira ff
          where f.codigoempresa || f.codigofl In (11,261)
            and ff.competficha between '01-may-2018' and last_day(sysdate) -- ADD_MONTHS(Trunc(Sysdate,'rr'), -96) and sysdate
            and ff.codintfunc = f.codintfunc
            and ff.situacaoffinan = 'A'
            and (ff.tipofolha = 1)
            And f.CODFUNCAO Not In (519,616,686,794,838,846)
          group by ff.competficha, f.codarea
          , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
              , f.NOMEFUNC
                       , f.DESCFUNCAO
