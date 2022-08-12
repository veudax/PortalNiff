(Select Distinct h.CodArea, h.codintfunc 
          From Flp_Historicosalarial h, vw_funcionarios fh
         Where h.dthistsal = (Select Max(dthistsalant)
                                From flp_historicosalarial x
                               Where codintfunc = fh.codintfunc 
                                 And dthistsal = (Select Min(dthistsal)
                                                    From flp_historicosalarial x
                                                   Where codintfunc = fh.codintfunc 
                                                     And dthistsal >= '01-mar-2019'
                                                     And dthistsalant Is Not Null
                                                     And x.statushistsal = 'N' 
                                                     And codintfunc = 23322)
                                 And dthistsalant Is Not Null
                                 And x.statushistsal = 'N' )
          And h.codintfunc = fh.codintfunc
          And h.codarea <> fh.CODAREA
          And h.dthistsal <= '31-mar-2019'
          And h.dthistsalant Is Not Null
          And h.codintfunc = 23322
          And h.statushistsal = 'N')