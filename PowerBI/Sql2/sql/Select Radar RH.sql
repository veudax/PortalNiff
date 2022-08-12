Create Or Replace View Pbi_Radar_RH As
Select Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Quantidade,
       Percentual,
       Ordem
  From (Select 'Exames Vencidos' Grupo, 9 Ordem,
               LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
               Trunc(M.DATAFICHA) Data,
               Count(*) Quantidade,
               0 Percentual
          From FLP_FUNCIONARIOS F,
               SRH_TIPOCONSULTA C,
               SRH_FICHAMEDICA FM,
             ( Select Max(FM.DATAFICHAMED) DATAFICHA, FM.CODINTFUNC
                 From SRH_FICHAMEDICA FM
                Where FM.TIPOCONSULTA In ('02','03','04','07')
                Group By FM.CODINTFUNC ) M
         Where F.CODINTFUNC = M.CODINTFUNC 
           And F.CODINTFUNC = FM.CODINTFUNC
           And FM.DATAFICHAMED = M.DATAFICHA
           And C.CODTIPOCONS = FM.TIPOCONSULTA
           And f.situacaofunc = 'A'
           And f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
           And Trunc(M.DataFicha) Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
           And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
         Group By LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0'),
                  Trunc(M.DATAFICHA)
         Union All
        Select 'CNH Vencidas' Grupo, 10 Ordem, 
               LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
               Trunc(D.CNHVENCTO) Data,
               Count(*) Quantidade,
               0 Percentual
          From vwflp_doctos d, 
               Vw_Funcionarios f
         Where f.CODINTFUNC = d.CODINTFUNC
           And f.SITUACAOFUNC = 'A'
           And f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
           And Trunc(D.CNHVENCTO) Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
           And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))   
         Group By LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0'),
                  Trunc(D.CNHVENCTO)
         Union All
         Select 'Absenteísmo' Grupo, 5 Ordem, 
                EMPFIL,
                Data,
                a.qtd_ocorr Quantidade,
                0 Percentual
          From (Select Count(fu.nomefunc) qtd_ocorr, 
                       trunc(t.dthist) Data, 
                       LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0') EMPFIL
                  From flp_historico t, frq_ocorrencia f, vw_funcionarios fu
                 Where t.dthist BETWEEN (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)) 
                   And f.codocorr = t.codocorr
                   And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                   And fu.situacaofunc = 'A'
                   And f.codocorr In (505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527)
                   And t.codintfunc = fu.codintfunc
                   And fu.CODAREA In (20,30,40) -- trazer só essas areas?
                 Group By Trunc(t.dthist), 
                          LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0')) a
          Union All
         Select 'Avarias por KM' Grupo, 8 Ordem, 
                EmpFil,
                Data, 
                Decode(qtd_ocorr, 0, 0, totalkm / qtd_ocorr) Quantidade,
                0 Percentual
           From (Select Sum(qtd_ocorr) qtd_ocorr, 
                        Data,
                        EmpFil,
                        Sum(totalkm) TotalKM
                   From (Select Count(fu.nomefunc) qtd_ocorr, 
                                To_Date('01/' || To_Char(t.dthist, 'mm/yyyy'), 'dd/mm/yyyy') Data, 
                                LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0') EMPFIL,
                                0 totalkm
                           From flp_historico t, 
                                frq_ocorrencia f, 
                                flp_funcionarios fu
                          Where Trunc(t.dthist) BETWEEN (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))          
                            And fu.codigoempresa || fu.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                            And fu.situacaofunc = 'A'
                            And f.codocorr = t.codocorr
                            And f.codocorr in(514,106,241,594,595)
                            And t.codintfunc = fu.codintfunc
                          Group By To_Date('01/' || To_Char(t.dthist, 'mm/yyyy'), 'dd/mm/yyyy'), 
                                   LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0')
                          Union All
                         Select 0 qtd_ocorr, 
                                To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), 
                                LPAD(b.CODIGOEMPRESA,3,'0') || '/' || Lpad(b.CodigoFl,3,'0') EMPFIL,
                                sum(b.kmpercorridoveloc) totalkm
                           From bgm_velocimetro b
                          Where b.dataveloc BETWEEN (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)) 
                            and b.codigoempresa || b.CODIGOfl in (11,12,21,31,41,51,61,92,131,261,262)
                          Group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'),
                                    LPAD(b.CODIGOEMPRESA,3,'0') || '/' || Lpad(b.CodigoFl,3,'0') ) a 
                  Group By Data, Empfil ) a
          Union All                   
         Select 'Acidentes por KM' Grupo, 7 Ordem,
                EmpFil,
                a.Data, 
                Decode(a.qtd_ocorr, 0, 0, totalkm / a.qtd_ocorr) Quantidade,
                0 Percentual
           From (Select Sum(qtd_ocorr) qtd_ocorr, 
                        Data,
                        EmpFil,
                        Sum(totalkm) TotalKM
                   From (Select Count(fu.nomefunc) qtd_ocorr, 
                                To_Date('01/' || To_Char(t.dthist, 'mm/yyyy'), 'dd/mm/yyyy') Data, 
                                LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0') EMPFIL,
                                0 totalkm
                           From flp_historico t, 
                                frq_ocorrencia f, 
                                flp_funcionarios fu
                          Where Trunc(t.dthist) BETWEEN (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))          
                            And fu.codigoempresa || fu.CODIGOfl in (11,12,21,31,41,51,61,92,131,261,262)
                            And fu.situacaofunc = 'A'
                            And f.codocorr = t.codocorr
                            And f.codocorr In (502,501,257,226,86,84,503,511,103,228)
                            And t.codintfunc = fu.codintfunc
                          Group By To_Date('01/' || To_Char(t.dthist, 'mm/yyyy'), 'dd/mm/yyyy'), 
                                   LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0')
                          Union All
                         Select 0 qtd_ocorr, 
                                To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'), 
                                LPAD(b.CODIGOEMPRESA,3,'0') || '/' || Lpad(b.CodigoFl,3,'0') EMPFIL,
                                sum(b.kmpercorridoveloc) totalkm
                           From bgm_velocimetro b
                          Where b.dataveloc BETWEEN (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))     
                            and b.codigoempresa || b.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                          Group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'),
                                   LPAD(b.CODIGOEMPRESA,3,'0') || '/' || Lpad(b.CodigoFl,3,'0') ) a 
                       Group By Data, EmpFil ) a                           
          Union All 
         Select 'Turnover' Grupo, 6 Ordem,
                LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
                ff.competficha data,
                Count(Distinct f.nomefunc) Quantidade,
                0 Percentual
           from flp_funcionarios f,
                flp_fichafinanceira ff
          where f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
            And ff.competficha between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))     
            And ff.codintfunc = f.codintfunc
            And ff.situacaoffinan = 'A'
            And (ff.tipofolha = 1)
          Group By ff.competficha,
                LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0')
         Union All
         Select 'Qtde. Total de FTE''s' Grupo, 1 Ordem, 
                LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
                ff.competficha data,
                Count(Distinct f.nomefunc) Quantidade,
                0 Percentual               
           from flp_funcionarios f,
                flp_fichafinanceira ff
          where f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
            And ff.competficha between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))     
            And ff.codintfunc = f.codintfunc
            And ff.situacaoffinan <> 'D'
            And (ff.tipofolha = 1)
          Group By ff.competficha,
                LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0')
          Union All
         Select 'Qtde. FTE Operação/Carro' Grupo, 2 Ordem,
                 EmpFil,
                 Data, 
                 Decode(QtdCarros, 0, 0, Round(Quantidade/QtdCarros)) Quantidade,
                 0 Percentual
           From (Select Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
                        To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy') Data,
                        Sum(Quantidade) Quantidade,
                        Sum(QtdCarros) QtdCarros                         
                   From (Select LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
                                ff.competficha data,
                                Count(Distinct f.nomefunc) Quantidade,
                                0 QtdCarros
                           from vw_funcionarios f,
                                flp_fichafinanceira ff
                          where f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                            And ff.competficha between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))     
                            And ff.codintfunc = f.codintfunc
                            And ff.situacaoffinan = 'A'
                            And ff.tipofolha = 1
                            And f.CODAREA = 40
                          Group By ff.competficha,
                                LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0')
                          Union All
                         Select LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                                To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
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
                  Group By Decode(EmpFil, '009/002', '009/001', empFil), 
                           To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy'))
          Union All
         Select 'Qtde. FTE Manutenção/Carro' Grupo, 2 Ordem,
                 EmpFil,
                 Data, 
                 Decode(QtdCarros, 0, 0, Round(Quantidade/QtdCarros)) Quantidade,
                 0 Percentual
           From (Select Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
                        To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy') Data,
                        Sum(Quantidade) Quantidade,
                        Sum(QtdCarros) QtdCarros
                   From (Select LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
                                ff.competficha data,
                                Count(Distinct f.nomefunc) Quantidade,
                                0 QtdCarros
                           from vw_funcionarios f,
                                flp_fichafinanceira ff
                          where f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                            And ff.competficha between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))     
                            And ff.codintfunc = f.codintfunc
                            And ff.situacaoffinan = 'A'
                            And ff.tipofolha = 1
                            And f.CODAREA = 30
                          Group By ff.competficha,
                                   LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0')
                          Union All                            
                        Select LPAD(c.CODIGOEMPRESA,3,'0') || '/' || Lpad(c.CodigoFl,3,'0') EMPFIL, 
                               To_date('01/' || To_char(g.dat_viagem_guia,'mm/yyyy'),'dd/mm/yyyy') Data, 
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
                  Group By Decode(EmpFil, '009/002', '009/001', empFil), 
                           To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy'))
          Union All
         Select 'Qtde. FTE Adm/Total FTE' Grupo, 4 Ordem,
                EMPFIL,
                data,
                Quantidade,
                Trunc((Quantidade/FTE) * 100,2) Percentual
           From (Select Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
                        To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy') Data,
                        Sum(Quantidade) Quantidade,
                        Sum(FTE) FTE
                   From (Select LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
                                ff.competficha data,
                                Count(Distinct f.nomefunc) Quantidade,
                                0 FTE
                           from vw_funcionarios f,
                                flp_fichafinanceira ff
                          where f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                            And ff.competficha between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))     
                            And ff.codintfunc = f.codintfunc
                            And ff.situacaoffinan = 'A'
                            And ff.tipofolha = 1
                            And f.CODAREA = 20
                          Group By ff.competficha,
                                LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0')
                          Union All
                         Select LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
                                ff.competficha data,
                                0 Quantidade,
                                Count(Distinct f.nomefunc) FTE
                           from vw_funcionarios f,
                                flp_fichafinanceira ff
                          where f.codigoempresa || f.codigofl in (11,12,21,31,41,51,61,92,131,261,262)
                            And ff.competficha between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
                            And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))     
                            And ff.codintfunc = f.codintfunc
                            And ff.situacaoffinan = 'A'
                            And ff.tipofolha = 1
                          Group By ff.competficha,
                                LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0')) a 
                  Group By Decode(EmpFil, '009/002', '009/001', empFil), 
                           To_date('01/' || To_char(Data,'mm/yyyy'),'dd/mm/yyyy') ) )
          