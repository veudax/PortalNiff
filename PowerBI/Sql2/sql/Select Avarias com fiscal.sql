Select Distinct Sum(Qtd_Ocorr) Qtd_Ocorr,
                Dthist,
                Codintfunc,
                Ocorrencia,
                Codintlinha,
                Carro,
                Codfiscal,
                Funcionario
  From (
        --1  avarias dentro do horario do fiscal
        Select 1 tipo,  Qtd_Ocorr,
                a.Data Dthist,
                Horaavariaoriginal,
                Horaavaria,
                a.Hora,
                Entradigit,
                Saidadigit, 
                a.Codintfunc,
                a.Horarainfger,
                a.Ocorrencia,
                a.Codintlinha,
                a.Carro,
                a.Codfiscal,
                Funcionario, 
                0 totalkm
          From (Select Count(Fu.Nomefunc) Qtd_Ocorr,
                        a.Hora,
                        Trunc(Datarainfger) Data,
                        Horarainfger,
                        a.Codintlinha,
                        Fu.Codintfunc,
                        Ft.Codintfunc Codfiscal,
                        
                        Dmf.Entradigit,
                        Dmf.Saidadigit,
                        Dm.Entradigit Entmot,
                        Dm.Saidadigit Saimot,

                        Datetomin(To_Date(To_Char(a.Datarainfger, 'dd/mm/yyyy') || ' ' ||
                                          To_Char(a.Horarainfger, 'hh24:Mi:ss'),
                                          'dd/mm/yyyy hh24:mi:ss')) Minutosavaria,
                        
                        To_Date(To_Char(Decode(Least(Datetomin(Dm.Saidadigit),
                                                     Datetomin(Hora)),
                                               Datetomin(Hora),
                                               Dm.Saidadigit,
                                               Dmf.Saidadigit),
                                        'dd/mm/yyyy') || ' ' ||
                                To_Char(Decode(GreatEst(Datetomin(Hora),Datetomin(Dmf.Saidadigit))
                                                , Datetomin(Hora)
                                                , a.Horarainfger
                                                , dmf.EntraDigit) , 'hh24:Mi:ss'),
                                'dd/mm/yyyy hh24:mi:ss') Horaavaria,
                        
                        Horaavariaoriginal,
                        Fu.Codfunc || ' - ' || Fu.Nomefunc Funcionario,
                        f.Codocorr || ' - ' || f.Desccomplhist Ocorrencia,
                        v.Prefixoveic Carro
                   From Frq_Ocorrencia  f,
                        Vw_Funcionarios Fu,
                        Frt_Cadveiculos v,
                        
                        Niff_Pbi_Linhasporservico  Ls,
                        Niff_Pbi_Fiscaisdoterminal Ft,
                        Frq_Digitacaomovimento     Dmf,
                        Frq_Digitacaomovimento     Dm,
                        Niff_Pbi_Terminallinhas    Tl,
                        
                        (Select Distinct a.Datarainfger,
                                         a.Horarainfger,
                                         Decode(To_Char(a.Horarainfger, 'dd/mm/yyyy hh24:mi')
                                               , '30/12/1899 00:00'
                                               , d.Saidadigit - (2/24)                                                
                                               , Horarainfger) Hora
                                               , To_Date(To_Char(a.Datarainfger, 'dd/mm/yyyy') || ' ' ||
                                                 To_Char(a.Horarainfger, 'hh24:Mi:ss'), 'dd/mm/yyyy hh24:mi:ss') Horaavariaoriginal,
                                         a.Numerorainfger,
                                         a.Codocorr,
                                         Ac.Codintfunc,
                                         a.Codintlinha,
                                         a.Codigoveic
                           From Acd_Funcinformgerais   Ac,
                                Acd_Informacoesgerais  a,
                                Frq_Digitacaomovimento d
                          Where a.Codigoempresa = Ac.Codigoempresa
                            And a.Codigofl = Ac.Codigofl
                            And a.Numerorainfger = Ac.Numerorainfger
                            And Ac.Funcprincipalfuncinfger = 'S'
                            And d.Codintfunc = Ac.Codintfunc
                            And Trunc(d.Dtdigit) = a.Datarainfger
                            And d.Statusdigit = 'N'
                            And d.Tipodigit = 'F'
                            And Trunc(a.Datarainfger) Between '21-jan-2017' And '31-jan-2017'
                         ) a
                 
                  Where Fu.Codintfunc = a.Codintfunc
                    And a.Codocorr = f.Codocorr
                    And Fu.Codarea = 40
                    And f.Codocorr In
                        (514, 106, 241, 594, 595)
                    And v.Codigoveic = a.Codigoveic
                       
                    And Ls.Codintlinha = a.Codintlinha
                    And Ft.Idterminal = Ls.Idterminal
                    And Ft.Data = Trunc(Datarainfger)
                       
                    And Dmf.Codintfunc = Ft.Codintfunc
                    And Dmf.Dtdigit = Trunc(Datarainfger)
                    And Dmf.Codocorr In (96, 99, 98)
                    And Dmf.Tipodigit = 'F'
                    And Dmf.Statusdigit = 'N'
                       
                    And Dm.Codintfunc = Fu.Codintfunc
                    And Dm.Dtdigit = Trunc(Datarainfger)
                    And Dm.Codocorr In (96, 99, 98)
                    And Dm.Tipodigit = 'F'
                    And Dm.Statusdigit = 'N'
                       
                    And Tl.Idterminal = Ft.Idterminal
                    And Tl.Tipoterminal = 'TP'
                       
                    And a.Horaavariaoriginal Between Dmf.Entradigit And Dmf.Saidadigit
                 
                  Group By Trunc(Datarainfger),
                           a.Horarainfger,
                           Datarainfger,
                           a.Hora,
                           f.Codocorr || ' - ' || f.Desccomplhist,
                           a.Codintlinha,
                           Dm.Saidadigit,
                           Dm.Entradigit,
                           v.Prefixoveic,
                           Dmf.Entradigit,
                           Fu.Codintfunc,
                           Dmf.Saidadigit,
                           Horaavariaoriginal,
                           Ft.Codintfunc,
                           Fu.Codfunc,
                           Fu.Nomefunc
                 
                 ) a
        Union All
        --2 avaria com hora 00:00
        Select 2 tipo, Qtd_Ocorr,
               a.Data Dthist,
               Horaavariaoriginal,
               Horaavaria,
               a.Hora,
               Entradigit,
               Saidadigit, 
               a.Codintfunc,
               a.Horarainfger,
               a.Ocorrencia,
               a.Codintlinha,
               a.Carro,
               a.Codfiscal,
               Funcionario, 
                0 totalkm
          From (Select Count(Fu.Nomefunc) Qtd_Ocorr,
                       a.Hora,
                       Trunc(Datarainfger) Data,
                       Horarainfger,
                       a.Codintlinha,
                       Fu.Codintfunc,
                       Ft.Codintfunc Codfiscal,
                       
                       Dmf.Entradigit,
                       Dmf.Saidadigit,
                       Dm.Entradigit Entmot,
                       Dm.Saidadigit Saimot,

                       Datetomin(To_Date(To_Char(a.Datarainfger, 'dd/mm/yyyy') || ' ' ||
                                         To_Char(a.Horarainfger, 'hh24:Mi:ss'),
                                         'dd/mm/yyyy hh24:mi:ss')) Minutosavaria,
                       
                       To_Date(To_Char(Decode(Least(Datetomin(Dm.Saidadigit),
                                                    Datetomin(Hora)),
                                              Datetomin(Hora),
                                              Dm.Saidadigit,
                                              Dmf.Saidadigit),
                                       'dd/mm/yyyy') || ' ' ||
                               To_Char(a.Horarainfger, 'hh24:Mi:ss'),
                               'dd/mm/yyyy hh24:mi:ss') Horaavaria,
                       
                       Horaavariaoriginal,
                       Fu.Codfunc || ' - ' || Fu.Nomefunc Funcionario,
                       f.Codocorr || ' - ' || f.Desccomplhist Ocorrencia,
                       v.Prefixoveic Carro
                  From Frq_Ocorrencia  f,
                       Vw_Funcionarios Fu,
                       Frt_Cadveiculos v,
                       
                       Niff_Pbi_Linhasporservico  Ls,
                       Niff_Pbi_Fiscaisdoterminal Ft,
                       Frq_Digitacaomovimento     Dmf,
                       Frq_Digitacaomovimento     Dm,
                       Niff_Pbi_Terminallinhas    Tl,
                       
                       (Select Distinct a.Datarainfger,
                                        a.Horarainfger,
                                        Decode(To_Char(a.Horarainfger, 'dd/mm/yyyy hh24:mi')
                                              , '30/12/1899 00:00'
                                              , d.Saidadigit - (3/24)                                                
                                              , Horarainfger) Hora,
                                        
                                        To_Date(To_Char(a.Datarainfger, 'dd/mm/yyyy') || ' ' ||
                                                To_Char(a.Horarainfger, 'hh24:Mi:ss'), 'dd/mm/yyyy hh24:mi:ss') Horaavariaoriginal,
                                                
                                        a.Numerorainfger,
                                        a.Codocorr,
                                        Ac.Codintfunc,
                                        a.Codintlinha,
                                        a.Codigoveic
                          From Acd_Funcinformgerais   Ac,
                               Acd_Informacoesgerais  a,
                               Frq_Digitacaomovimento d
                         Where a.Codigoempresa = Ac.Codigoempresa
                           And a.Codigofl = Ac.Codigofl
                           And a.Numerorainfger = Ac.Numerorainfger
                           And Ac.Funcprincipalfuncinfger = 'S'
                           And d.Codintfunc = Ac.Codintfunc
                           And Trunc(d.Dtdigit) = a.Datarainfger
                           And d.Statusdigit = 'N'
                           And d.Tipodigit = 'F'
                           And a.Horarainfger = '30-Dec-1899'
                           And Trunc(a.Datarainfger) Between '21-jan-2017' And '31-jan-2017' 
                        ) a
                
                 Where Fu.Codintfunc = a.Codintfunc
                   And a.Codocorr = f.Codocorr
                   And Fu.Codarea = 40
                   And f.Codocorr In
                        (514, 106, 241, 594, 595)
                   And v.Codigoveic = a.Codigoveic
                      
                   And Ls.Codintlinha = a.Codintlinha
                   And Ft.Idterminal = Ls.Idterminal
                   And Ft.Data = Trunc(Datarainfger)
                      
                   And Dmf.Codintfunc = Ft.Codintfunc
                   And Dmf.Dtdigit = Trunc(Datarainfger)
                   And Dmf.Codocorr In (96, 99, 98)
                   And Dmf.Tipodigit = 'F'
                   And Dmf.Statusdigit = 'N'
                      
                   And Dm.Codintfunc = Fu.Codintfunc
                   And Dm.Dtdigit = Trunc(Datarainfger)
                   And Dm.Codocorr In (96, 99, 98)
                   And Dm.Tipodigit = 'F'
                   And Dm.Statusdigit = 'N'
                      
                   And Tl.Idterminal = Ft.Idterminal
                   And Tl.Tipoterminal = 'TP'
                      
                   And To_Char(a.Horarainfger, 'dd/mm/yyyy hh24:mi') =
                       '30/12/1899 00:00'
                   And (dm.Entradigit Between Dmf.Entradigit And Dmf.Saidadigit
                    Or Datetomin(Hora) Between Datetomin(Dmf.Entradigit) And Datetomin(Dmf.Saidadigit))
                
                 Group By Trunc(Datarainfger),
                          a.Horarainfger,
                          Datarainfger,
                          a.Hora,
                          f.Codocorr || ' - ' || f.Desccomplhist,
                          a.Codintlinha,
                          Dm.Saidadigit,
                          Dm.Entradigit,
                          v.Prefixoveic,
                          Dmf.Entradigit,
                          Fu.Codintfunc,
                          Dmf.Saidadigit,
                          Horaavariaoriginal,
                          Ft.Codintfunc,
                          Fu.Codfunc,
                          Fu.Nomefunc
                
                ) a
                
        Union All 
        ( 
        --3 avaria fora do horário do fiscal depois do final da jornada
           Select 3 tipo, Qtd_Ocorr,
                  a.Data Dthist,
                  Horaavariaoriginal,
                  Horaavaria,
                  a.Hora,
                  Entradigit,
                  Saidadigit, 
                  a.Codintfunc,
                  a.Horarainfger,
                  a.Ocorrencia,
                  a.Codintlinha,
                  a.Carro,
                  a.Codfiscal,
                  Funcionario, 
                0 totalkm
             From (Select Count(Fu.Nomefunc) Qtd_Ocorr,
                          a.Hora,
                          Trunc(Datarainfger) Data,
                          Horarainfger,
                          a.Codintlinha,
                          Fu.Codintfunc,
                          Ft.Codintfunc Codfiscal,
                          
                          Dmf.Entradigit,
                          Dmf.Saidadigit,
                          Dm.Entradigit Entmot,
                          Dm.Saidadigit Saidamot,

                          Datetomin(To_Date(To_Char(a.Datarainfger, 'dd/mm/yyyy') || ' ' || To_Char(a.Horarainfger, 'hh24:Mi:ss'),'dd/mm/yyyy hh24:mi:ss')) Minutosavaria,
                          
                          To_Date(To_Char(Decode(Least(Datetomin(Dm.Saidadigit),
                                                       Datetomin(Hora)),
                                                 Datetomin(Hora),
                                                 Dm.Saidadigit,
                                                 Dmf.Saidadigit),
                                          'dd/mm/yyyy') || ' ' ||
                                  To_Char(Decode(GreatEst(Datetomin(Hora),Datetomin(Dmf.Saidadigit))
                                                , Datetomin(Hora)
                                                , dmf.Saidadigit
                                                , a.Horarainfger )  , 'hh24:Mi:ss'),
                                  'dd/mm/yyyy hh24:mi:ss') Horaavaria,
                          
                          Horaavariaoriginal,
                          Fu.Codfunc || ' - ' || Fu.Nomefunc Funcionario,
                          f.Codocorr || ' - ' || f.Desccomplhist Ocorrencia,
                          v.Prefixoveic Carro
                     From Frq_Ocorrencia  f,
                          Vw_Funcionarios Fu,
                          Frt_Cadveiculos v,
                          
                          Niff_Pbi_Linhasporservico  Ls,
                          Niff_Pbi_Fiscaisdoterminal Ft,
                          Frq_Digitacaomovimento     Dmf,
                          Frq_Digitacaomovimento     Dm,
                          Niff_Pbi_Terminallinhas    Tl,
                          
                          (Select Distinct a.Datarainfger,
                                           a.Horarainfger,
                                           Decode(To_Char(a.Horarainfger, 'dd/mm/yyyy hh24:mi'), '30/12/1899 00:00'
                                                 , d.Saidadigit - (2/24)                                                
                                                 , Horarainfger) Hora,
                                           
                                           To_Date(To_Char(a.Datarainfger,'dd/mm/yyyy') || ' ' ||
                                                   To_Char(a.Horarainfger,'hh24:Mi:ss'),'dd/mm/yyyy hh24:mi:ss') Horaavariaoriginal,
                                           a.Numerorainfger,
                                           a.Codocorr,
                                           Ac.Codintfunc,
                                           a.Codintlinha,
                                           a.Codigoveic
                             From Acd_Funcinformgerais   Ac,
                                  Acd_Informacoesgerais  a,
                                  Frq_Digitacaomovimento d
                            Where a.Codigoempresa = Ac.Codigoempresa
                              And a.Codigofl = Ac.Codigofl
                              And a.Numerorainfger = Ac.Numerorainfger
                              And Ac.Funcprincipalfuncinfger = 'S'
                              And d.Codintfunc = Ac.Codintfunc
                              And Trunc(d.Dtdigit) = a.Datarainfger
                              And d.Statusdigit = 'N'
                              And d.Tipodigit = 'F'
                              And a.Horarainfger <> '30-Dec-1899'                              
                              And Trunc(a.Datarainfger) Between '21-jan-2017' And  '31-jan-2017' 
                    ) a
                   
                    Where Fu.Codintfunc = a.Codintfunc
                      And a.Codocorr = f.Codocorr
                      And Fu.Codarea = 40
                      And f.Codocorr In
                        (514, 106, 241, 594, 595)
                      And v.Codigoveic = a.Codigoveic
                         
                      And Ls.Codintlinha = a.Codintlinha
                      And Ft.Idterminal = Ls.Idterminal
                      And Ft.Data = Trunc(Datarainfger)
                         
                      And Dmf.Codintfunc = Ft.Codintfunc
                      And Dmf.Dtdigit = Trunc(Datarainfger)
                      And Dmf.Codocorr In (96, 99, 98)
                      And Dmf.Tipodigit = 'F'
                      And Dmf.Statusdigit = 'N'
                         
                      And Dm.Codintfunc = Fu.Codintfunc
                      And Dm.Dtdigit = Trunc(Datarainfger)
                      And Dm.Codocorr In (96, 99, 98)
                      And Dm.Tipodigit = 'F'
                      And Dm.Statusdigit = 'N'
                         
                      And Tl.Idterminal = Ft.Idterminal
                      And Tl.Tipoterminal = 'TP'
                    Group By Trunc(Datarainfger),
                             a.Horarainfger,
                             Datarainfger,
                             a.Hora,
                             f.Codocorr || ' - ' || f.Desccomplhist,
                             a.Codintlinha,
                             Dm.Saidadigit,
                             Dm.Entradigit,
                             v.Prefixoveic,
                             Dmf.Entradigit,
                             Fu.Codintfunc,
                             Dmf.Saidadigit,
                             Horaavariaoriginal,
                             Ft.Codintfunc,
                             Fu.Codfunc,
                             Fu.Nomefunc
                   
                   ) a
            Where Horaavaria Between Entradigit And Saidamot
              And Datetomin(Hora) Between Datetomin(To_Date('22:00:00', 'hh24:mi:ss')) And
                  Datetomin(To_Date('23:59:59', 'hh24:mi:ss'))
              And Trunc(Saidamot) > Trunc(Saidadigit)
              And Entmot Between Entradigit And Saidadigit
              And Horaavaria Between Entradigit And
                  Saidadigit + (2 / 24)
        )
        Union All
        ( --4 avaria fora do horário do fiscal Antes do inicio da jornada
           Select 4 tipo, Qtd_Ocorr,
                  a.Data Dthist,
                  Horaavariaoriginal,
                  Horaavaria,
                  a.Hora,
                  Entradigit,
                  Saidadigit, 
                  a.Codintfunc,
                  a.Horarainfger,
                  a.Ocorrencia,
                  a.Codintlinha,
                  a.Carro,
                  a.Codfiscal,
                  Funcionario, 
                0 totalkm
             From (Select Count(Fu.Nomefunc) Qtd_Ocorr,
                          a.Hora,
                          Trunc(Datarainfger) Data,
                          Horarainfger,
                          a.Codintlinha,
                          Fu.Codintfunc,
                          Ft.Codintfunc Codfiscal,
                          
                          Dmf.Entradigit,
                          Dmf.Saidadigit,
                          Dm.Entradigit Entmot,
                          Dm.Saidadigit Saidamot,

                          Datetomin(To_Date(To_Char(a.Datarainfger, 'dd/mm/yyyy') || ' ' || To_Char(a.Horarainfger, 'hh24:Mi:ss'),'dd/mm/yyyy hh24:mi:ss')) Minutosavaria,
                          
                          To_Date(To_Char(Decode(Least(Datetomin(Dm.Saidadigit),
                                                       Datetomin(Hora)),
                                                 Datetomin(Hora),
                                                 Dm.Saidadigit,
                                                 Dmf.Saidadigit),
                                          'dd/mm/yyyy') || ' ' ||
                                  To_Char(Decode(GreatEst(Datetomin(Hora),Datetomin(dmf.EntraDigit))
                                                , Datetomin(Hora)
                                                , a.Horarainfger
                                                , Decode(GreatEst(dm.Entradigit,hora)
                                                        , hora
                                                        , dmf.EntraDigit
                                                        , hora + (2/24)))  , 'hh24:Mi:ss'),
                                  'dd/mm/yyyy hh24:mi:ss') Horaavaria,
                          
                          Horaavariaoriginal,
                          Fu.Codfunc || ' - ' || Fu.Nomefunc Funcionario,
                          f.Codocorr || ' - ' || f.Desccomplhist Ocorrencia,
                          v.Prefixoveic Carro
                     From Frq_Ocorrencia  f,
                          Vw_Funcionarios Fu,
                          Frt_Cadveiculos v,
                          
                          Niff_Pbi_Linhasporservico  Ls,
                          Niff_Pbi_Fiscaisdoterminal Ft,
                          Frq_Digitacaomovimento     Dmf,
                          Frq_Digitacaomovimento     Dm,
                          Niff_Pbi_Terminallinhas    Tl,
                          
                          (Select Distinct a.Datarainfger,
                                           a.Horarainfger,
                                           Decode(To_Char(a.Horarainfger, 'dd/mm/yyyy hh24:mi'), '30/12/1899 00:00'
                                                 , d.Saidadigit - (2/24)                                                
                                                 , Horarainfger) Hora,
                                           
                                           To_Date(To_Char(a.Datarainfger,'dd/mm/yyyy') || ' ' ||
                                                   To_Char(a.Horarainfger,'hh24:Mi:ss'),'dd/mm/yyyy hh24:mi:ss') Horaavariaoriginal,
                                           a.Numerorainfger,
                                           a.Codocorr,
                                           Ac.Codintfunc,
                                           a.Codintlinha,
                                           a.Codigoveic
                             From Acd_Funcinformgerais   Ac,
                                  Acd_Informacoesgerais  a,
                                  Frq_Digitacaomovimento d
                            Where a.Codigoempresa = Ac.Codigoempresa
                              And a.Codigofl = Ac.Codigofl
                              And a.Numerorainfger = Ac.Numerorainfger
                              And Ac.Funcprincipalfuncinfger = 'S'
                              And d.Codintfunc = Ac.Codintfunc
                              And Trunc(d.Dtdigit) = a.Datarainfger
                              And d.Statusdigit = 'N'
                              And d.Tipodigit = 'F'
                              And a.Horarainfger <> '30-Dec-1899'                              
                              And Trunc(a.Datarainfger) Between '21-jan-2017' And  '31-jan-2017' 
                           ) a
                   
                    Where Fu.Codintfunc = a.Codintfunc
                      And a.Codocorr = f.Codocorr
                      And Fu.Codarea = 40
                      And f.Codocorr In
                        (514, 106, 241, 594, 595)
                      And v.Codigoveic = a.Codigoveic
                         
                      And Ls.Codintlinha = a.Codintlinha
                      And Ft.Idterminal = Ls.Idterminal
                      And Ft.Data = Trunc(Datarainfger)
                         
                      And Dmf.Codintfunc = Ft.Codintfunc
                      And Dmf.Dtdigit = Trunc(Datarainfger)
                      And Dmf.Codocorr In (96, 99, 98)
                      And Dmf.Tipodigit = 'F'
                      And Dmf.Statusdigit = 'N'
                         
                      And Dm.Codintfunc = Fu.Codintfunc
                      And Dm.Dtdigit = Trunc(Datarainfger)
                      And Dm.Codocorr In (96, 99, 98)
                      And Dm.Tipodigit = 'F'
                      And Dm.Statusdigit = 'N'
                         
                      And Tl.Idterminal = Ft.Idterminal
                      And Tl.Tipoterminal = 'TP'
                    Group By Trunc(Datarainfger),
                             a.Horarainfger,
                             Datarainfger,
                             a.Hora,
                             f.Codocorr || ' - ' || f.Desccomplhist,
                             a.Codintlinha,
                             Dm.Saidadigit,
                             Dm.Entradigit,
                             v.Prefixoveic,
                             Dmf.Entradigit,
                             Fu.Codintfunc,
                             Dmf.Saidadigit,
                             Horaavariaoriginal,
                             Ft.Codintfunc,
                             Fu.Codfunc,
                             Fu.Nomefunc
                   
                   ) a
            Where Entmot < Entradigit
              And HoraavariaOriginal < Entradigit
              And Horaavaria Between Entradigit And Saidadigit 
              And EntMot +(1/24) Between Entradigit And Saidadigit 
       
        )
        Union All
        (
           --5 avaria fora do horário do fiscal depois do final da jornada dia seguinte
           Select 5 tipo, Qtd_Ocorr,
                  a.Data Dthist,
                  Horaavariaoriginal,
                  Horaavaria,
                  a.Hora,
                  Entradigit,
                  Saidadigit, 
                  a.Codintfunc,
                  a.Horarainfger,
                  a.Ocorrencia,
                  a.Codintlinha,
                  a.Carro,
                  a.Codfiscal,
                  Funcionario, 
                0 totalkm
             From (Select Count(Fu.Nomefunc) Qtd_Ocorr,
                          a.Hora,
                          Trunc(Datarainfger) Data,
                          Horarainfger,
                          a.Codintlinha,
                          Fu.Codintfunc,
                          Ft.Codintfunc Codfiscal,
                          
                          Dmf.Entradigit,
                          Dmf.Saidadigit,
                          Dm.Entradigit Entmot,
                          Dm.Saidadigit Saidamot,

                          Datetomin(To_Date(To_Char(a.Datarainfger, 'dd/mm/yyyy') || ' ' || To_Char(a.Horarainfger, 'hh24:Mi:ss'),'dd/mm/yyyy hh24:mi:ss')) Minutosavaria,
                          
                          To_Date(To_Char(Decode(Least(Datetomin(Dm.Saidadigit),
                                                       Datetomin(Hora)),
                                                 Datetomin(Hora),
                                                 Dm.Saidadigit,
                                                 Dmf.Saidadigit),
                                          'dd/mm/yyyy') || ' ' ||
                                  To_Char(Decode(GreatEst(Datetomin(Hora),Datetomin(Dmf.Saidadigit))
                                                , Datetomin(Hora)
                                                , dmf.Saidadigit
                                                , a.Horarainfger )  , 'hh24:Mi:ss'),
                                  'dd/mm/yyyy hh24:mi:ss') Horaavaria,
                          
                          Horaavariaoriginal,
                          Fu.Codfunc || ' - ' || Fu.Nomefunc Funcionario,
                          f.Codocorr || ' - ' || f.Desccomplhist Ocorrencia,
                          v.Prefixoveic Carro
                     From Frq_Ocorrencia  f,
                          Vw_Funcionarios Fu,
                          Frt_Cadveiculos v,
                          
                          Niff_Pbi_Linhasporservico  Ls,
                          Niff_Pbi_Fiscaisdoterminal Ft,
                          Frq_Digitacaomovimento     Dmf,
                          Frq_Digitacaomovimento     Dm,
                          Niff_Pbi_Terminallinhas    Tl,
                          
                          (Select Distinct a.Datarainfger,
                                           a.Horarainfger,
                                           Decode(To_Char(a.Horarainfger, 'dd/mm/yyyy hh24:mi'), '30/12/1899 00:00'
                                                 , d.Saidadigit - (2/24)                                                
                                                 , Horarainfger) Hora,
                                           
                                           To_Date(To_Char(a.Datarainfger,'dd/mm/yyyy') || ' ' ||
                                                   To_Char(a.Horarainfger,'hh24:Mi:ss'),'dd/mm/yyyy hh24:mi:ss') Horaavariaoriginal,
                                           a.Numerorainfger,
                                           a.Codocorr,
                                           Ac.Codintfunc,
                                           a.Codintlinha,
                                           a.Codigoveic
                             From Acd_Funcinformgerais   Ac,
                                  Acd_Informacoesgerais  a,
                                  Frq_Digitacaomovimento d
                            Where a.Codigoempresa = Ac.Codigoempresa
                              And a.Codigofl = Ac.Codigofl
                              And a.Numerorainfger = Ac.Numerorainfger
                              And Ac.Funcprincipalfuncinfger = 'S'
                              And d.Codintfunc = Ac.Codintfunc
                              And Trunc(d.Dtdigit) = a.Datarainfger
                              And d.Statusdigit = 'N'
                              And d.Tipodigit = 'F'
                              And a.Horarainfger <> '30-Dec-1899'
                              And Trunc(a.Datarainfger) Between '21-jan-2017' And  '31-jan-2017' 
                           ) a
                   
                    Where Fu.Codintfunc = a.Codintfunc
                      And a.Codocorr = f.Codocorr
                      And Fu.Codarea = 40
                      And f.Codocorr In
                        (514, 106, 241, 594, 595)
                      And v.Codigoveic = a.Codigoveic
                         
                      And Ls.Codintlinha = a.Codintlinha
                      And Ft.Idterminal = Ls.Idterminal
                      And Ft.Data = Trunc(Datarainfger)
                         
                      And Dmf.Codintfunc = Ft.Codintfunc
                      And Dmf.Dtdigit = Trunc(Datarainfger)
                      And Dmf.Codocorr In (96, 99, 98)
                      And Dmf.Tipodigit = 'F'
                      And Dmf.Statusdigit = 'N'
                         
                      And Dm.Codintfunc = Fu.Codintfunc
                      And Dm.Dtdigit = Trunc(Datarainfger)
                      And Dm.Codocorr In (96, 99, 98)
                      And Dm.Tipodigit = 'F'
                      And Dm.Statusdigit = 'N'
                         
                      And Tl.Idterminal = Ft.Idterminal
                      And Tl.Tipoterminal = 'TP'
                    Group By Trunc(Datarainfger),
                             a.Horarainfger,
                             Datarainfger,
                             a.Hora,
                             f.Codocorr || ' - ' || f.Desccomplhist,
                             a.Codintlinha,
                             Dm.Saidadigit,
                             Dm.Entradigit,
                             v.Prefixoveic,
                             Dmf.Entradigit,
                             Fu.Codintfunc,
                             Dmf.Saidadigit,
                             Horaavariaoriginal,
                             Ft.Codintfunc,
                             Fu.Codfunc,
                             Fu.Nomefunc
                   
                   ) a
            Where Horaavaria Between Entradigit And Saidamot
              And Datetomin(Hora) Between Datetomin(To_Date('00:00:00', 'hh24:mi:ss')) And 
              Datetomin(To_Date('4:59:59', 'hh24:mi:ss'))
              And Trunc(Saidamot) > Trunc(Saidadigit) )
        )
 Group By Dthist,
          Codintfunc,
          Ocorrencia,
          Codintlinha,
          Carro,
          Codfiscal,
          Funcionario
