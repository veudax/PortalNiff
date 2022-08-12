Create Or Replace View Pbi_Radar_Planejamento As
 Select Grupo,
        EmpFil,
        Data,
        Quantidade,
        Percentual,
        Ordem
   From ( Select 'Frota Patrimonial' Grupo, 1 Ordem,
                 To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                 LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                 Count(Distinct v.cod_veiculo) Quantidade,
                 0 Percentual
            From t_Arr_Guia g, 
                 t_arr_viagens_guia v,
                 frt_cadveiculos c
            Where v.cod_seq_guia = g.cod_seq_guia
              And c.codigoempresa || c.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
              And c.codigoveic = v.cod_veiculo
              And c.codigotpfrota Not In (7,10)
              And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
              And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
            Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                  LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0')
           Union All
          Select 'Frota Operacional' Grupo, 2 Ordem,
                 To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                 LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                 Count(Distinct v.cod_veiculo) Quantidade,
                 0 Percentual
            From t_Arr_Guia g, 
                 t_arr_viagens_guia v,
                 frt_cadveiculos c
            Where v.cod_seq_guia = g.cod_seq_guia
              And c.codigoempresa || c.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
              And c.codigoveic = v.cod_veiculo
              And c.codigotpfrota Not In (7,9,10,52)
              And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
              And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
            Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                  LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0')
           Union All
          Select 'Passageiro Transportado' Grupo, 3 ordem, 
                 To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                 LPAD(g.Cod_Empresa,3,'0') || '/' || Lpad(g.CodigoFl,3,'0') EMPFIL, 
                 Sum(Decode(d.cod_tipopagtarifa, '403', 0, Decode( d.cod_tipopagtarifa,'X', -1,'956', -1, 1) * d.qtd_passag_trans)) Quantidade,
                 0 Percentual
            From t_Arr_Guia g, 
                 t_arr_detalhe_guia d
            Where d.cod_seq_guia = g.cod_seq_guia
              And g.Cod_Empresa || g.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
              And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
              And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
            Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                  LPAD(g.Cod_Empresa,3,'0') || '/' || Lpad(g.CodigoFl,3,'0')
           Union All
          Select 'IPK' Grupo, 4 Ordem,
                 Data, 
                 EmpFil,
                 0 Quantiadde,
                 Decode(kmRodado, 0, 0, Quantidade/ KmRodado) Percentual
            From (Select To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                         LPAD(g.Cod_Empresa,3,'0') || '/' || Lpad(g.CodigoFl,3,'0') EMPFIL, 
                         Sum(Decode(d.cod_tipopagtarifa, '403', 0, Decode( d.cod_tipopagtarifa,'X', -1,'956', -1, 1) * d.qtd_passag_trans)) Quantidade,
                         Sum(e.Qtd_Kmrod) KmRodado
                    From t_Arr_Guia g, 
                         t_arr_detalhe_guia d,
                         t_arr_estatguia e
                    Where d.cod_seq_guia = g.cod_seq_guia
                      And e.cod_seq_guia = g.cod_seq_guia
                      And e.cod_seq_viagem = d.cod_seq_viagem
                      And g.Cod_Empresa || g.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                      And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                      And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                    Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                          LPAD(g.Cod_Empresa,3,'0') || '/' || Lpad(g.CodigoFl,3,'0'))
           Union All
          Select 'PVD' Grupo, 5 Ordem,
                 Data, 
                 EmpFil,
                 Decode(QtdCarros, 0, 0, Round(Quantidade/QtdCarros)) Quantidade,
                 0 Percentual
            From (Select EmpFil,
                         To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy') Data,
                         Sum(Quantidade) Quantidade,
                         Sum(QtdCarros) QtdCarros                         
                    From (Select To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                                 LPAD(g.Cod_Empresa,3,'0') || '/' || Lpad(g.CodigoFl,3,'0') EMPFIL, 
                                 Sum(Decode(d.cod_tipopagtarifa, '403', 0, Decode( d.cod_tipopagtarifa,'X', -1,'956', -1, 1) * d.qtd_passag_trans)) Quantidade,
                                 0 QtdCarros
                            From t_Arr_Guia g, 
                                 t_arr_detalhe_guia d
                            Where d.cod_seq_guia = g.cod_seq_guia
                              And g.Cod_Empresa || g.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                              And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                              And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                            Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                                  LPAD(g.Cod_Empresa,3,'0') || '/' || Lpad(g.CodigoFl,3,'0')
                            Union All
                           Select To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
                                  LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                                  0 Quantidade,
                                  Count(Distinct v.cod_veiculo) QtdCarros
                             From t_Arr_Guia g, 
                                  t_arr_viagens_guia v,
                                  frt_cadveiculos c
                             Where v.cod_seq_guia = g.cod_seq_guia
                               And c.codigoempresa || c.codigofl in (11,12,21,31,41,51,61,91,131,261,263)
                               And c.codigoveic = v.cod_veiculo
                               And c.codigotpfrota Not In (7,9,10,52)
                               And g.dat_viagem_guia Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                               And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
                             Group By To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') , 
                                      LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0'))
                   Group By  EmpFil, To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy')) )
         