Create Or Replace View pbi_Radar_QtdeFTE As
       Select Last_day(ff.competficha) data,
              LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
              Count(Distinct f.nomefunc) FTE
         from flp_funcionarios f,
              flp_fichafinanceira ff
        where f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,263)
          And ff.competficha between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz
          And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
          And ff.codintfunc = f.codintfunc
          And ff.situacaoffinan = 'A'
          And (ff.tipofolha = 1)
        Group By ff.competficha,
              LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0')                
