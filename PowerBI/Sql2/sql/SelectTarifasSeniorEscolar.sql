Select codigolinha, nomelinha
     , dat_viagem_guia
     , Sum(QtdSenior) QtdSenior
     , Sum(Senior) Senior
     , Sum(SeniorValor) SeniorValor
     , Sum(QtdEscolar) QtdEscolar
     , Sum(Escolar) Escolar
     , Sum(EscolarValor) EscolarValor
     , Sum(SeniorValor) + Sum(EscolarValor) Total
     From (
Select codigolinha
     , dat_viagem_guia
     , nomelinha
     , Quantidade QtdSenior
     , t.Tarifa Senior
     , t.Tarifa * Quantidade SeniorValor
     , 0 QtdEscolar
     , 0 Escolar
     , 0 EscolarValor
  From (
Select d.cod_tipopagtarifa, Sum(d.qtd_passag_trans ) Quantidade, d.codintlinha, l.codigolinha
     , g.dat_viagem_guia, l.nomelinha
     , (Select (Select t.vlr_tarifa 
                 From t_Trf_Tarifa_Tipopagto t
                    , t_Trf_Tipopagto tp
                Where t.cod_tarifa = s.cod_tarifa
                  And tp.cod_tipopagto = t.cod_tipopagtarifa
                  And tp.flg_normal = 'S' 
                  And (g.dat_viagem_guia Between t.dat_iniciovigencia And t.dat_finalvigencia
                   Or (t.dat_finalvigencia Is Null And t.dat_iniciovigencia <= g.dat_viagem_guia))) Tarifa
          From bgm_cadlinhas l
             , t_trf_secao s
             , t_Trf_Linha_Secao ls
         Where l.codintlinha = ls.cod_linha
           And s.cod_seq_secao = ls.cod_seq_secao     
           And l.codintlinha  = d.codintlinha
           And ls.flg_secao_default = 'S') tarifa
  From t_Arr_Guia g, t_Arr_Detalhe_Guia d, bgm_cadlinhas L
 Where g.cod_seq_guia = d.cod_seq_guia
   And d.cod_tipopagtarifa = 55 
   And g.dat_prest_contas = '20-Dec-2018'
   And g.cod_Empresa = 1
   And g.codigofl = 1
   And l.codintlinha = d.codintlinha
Group By d.cod_tipopagtarifa   , d.codintlinha, l.codigolinha, g.dat_viagem_guia, l.nomelinha ) T
Union All
Select codigolinha
     , dat_viagem_guia
     , nomelinha
     , 0 QtdSenior
     , 0 Senior
     , 0 SeniorValor
     , Quantidade QtdEscolar
     , t.tarifa/2 escolar
     , Round(t.tarifa/2  * Quantidade,2) EscolarValor
  From (
Select d.cod_tipopagtarifa, Sum(d.qtd_passag_trans ) Quantidade, d.codintlinha, l.codigolinha
     , g.dat_viagem_guia, l.nomelinha
     , (Select (Select t.vlr_tarifa 
                 From t_Trf_Tarifa_Tipopagto t
                    , t_Trf_Tipopagto tp
                Where t.cod_tarifa = s.cod_tarifa
                  And tp.cod_tipopagto = t.cod_tipopagtarifa
                  And tp.flg_normal = 'S' 
                  And (g.dat_viagem_guia Between t.dat_iniciovigencia And t.dat_finalvigencia
                   Or (t.dat_finalvigencia Is Null And t.dat_iniciovigencia <= g.dat_viagem_guia))) Tarifa
          From bgm_cadlinhas l
             , t_trf_secao s
             , t_Trf_Linha_Secao ls
         Where l.codintlinha = ls.cod_linha
           And s.cod_seq_secao = ls.cod_seq_secao     
           And l.codintlinha  = d.codintlinha
           And ls.flg_secao_default = 'S') tarifa
  From t_Arr_Guia g, t_Arr_Detalhe_Guia d, bgm_cadlinhas L
 Where g.cod_seq_guia = d.cod_seq_guia
   And d.cod_tipopagtarifa = 56
   And g.dat_prest_contas = '20-Dec-2018'
   And g.cod_Empresa = 1
   And g.codigofl = 1
   And l.codintlinha = d.codintlinha
Group By d.cod_tipopagtarifa   , d.codintlinha, l.codigolinha, g.dat_viagem_guia, l.nomelinha ) T ) V
Group By codigolinha, dat_viagem_guia, nomelinha