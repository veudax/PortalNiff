create or replace view pbi_radar_finan_rec_oper as
(Select 'Receita Operacional Total' grupo, 1 Ordem,
                To_date('01/' || substr(a.Periodo,5,2) || '/' || Substr(a.Periodo,1,4),'dd/mm/yyyy') Data,
                Decode(a.EmpFil, '003/015', '003/001', a.empFil) EmpFil,
                Sum(nvl(Abs(SaldoAcumulado),0)) + Sum(d.Valor) Valor,
                0 Percentual,
                'O' Tipo
          From (Select Sum(Decode(grupo, 'Subsídio Débito', -1,1) * Valor) Valor,
                       To_char(Data,'yyyymm') periodo,
                       EmpFil
                  From Pbi_Radar_Operacional
                 Where Tipo = 'Financeiro'
                   And Grupo Like 'Subsídio%'
                 Group By To_char(Data,'yyyymm'), EmpFil ) D,
                (Select Grupo, Ordem,
                        To_char(Data,'yyyymm') periodo,
                        EmpFil,
                        Valor SaldoAcumulado,
                        Percentual,
                        Tipo
                   From pbi_radar_finan_receita) A
          Where A.EmpFil = D.EmpFil
            And A.periodo = D.periodo
          Group By To_date('01/' || substr(a.Periodo,5,2) || '/' || Substr(a.Periodo,1,4),'dd/mm/yyyy'), A.EMPFIL
         )

