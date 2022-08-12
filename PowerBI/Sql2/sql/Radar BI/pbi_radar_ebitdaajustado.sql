create or replace view pbi_radar_ebitdaajustado as
Select 'EBITDA Ajustado' Grupo,
       Decode(EmpFil, '009/002', '009/001', empFil) EmpFil,
       Data,
       Round(valor) / 1000 Valor,
       decode(Percentual,0,Null,Round(Percentual,2)) Percentual,
       Percentual PercentualN,
       Ordem,
       Tipo
  From ( Select 'EBITDA' Grupo, 13 Ordem,
               Data,
               Decode(EmpFil, '003/015', '003/001', empFil) EmpFil,
               Sum(valor) + Sum(despesa) valor,
               Decode(Sum(RecOp), 0, 0, ((Sum(valor) + Sum(despesa))/Abs(Sum(RecOp)))*100) Percentual,
               /*decode(Least(Sum(valor), 0), Sum(Valor)
                     , Sum(Valor) - Sum(Despesa)
                     , Sum(valor) + Sum(despesa)) valor,*/
               'E' Tipo
           From (Select EmpFil,
                        Data ,
                        Valor,
                        0 Despesa,
                        0 RecOp
                   From pbi_radar_finan_ebitda
                 Union All
                 Select EmpFil,
                        To_date('01/' || substr(PeriodoSaldo,5,2) || '/' || Substr(PeriodoSAldo,1,4),'dd/mm/yyyy') Data,
                        0 Valor,
                        ProcessosJuridicos Despesa,
                        0 RecOp
                    From pbi_radar_EbidaProcJuridicos
                 Union All
                Select EmpFil,
                       Data,
                       0 MargemDistribuicao,
                       0 Despesas,
                       Sum(Valor) RecOp
                  From pbi_Radar_finan_Rec_Oper
                 Group By EmpFil, Data                           )
          Group By EmpFil, Data
                          )

