create or replace view pbi_he_total_teste as
Select EmpFil,
       CodArea,
       Area,
       CodFuncao,
       Funcao,
       Registro,
       funcionario,
       codEvento,
       evento,
       CompetAcumu,
       referAcumu,
       minutos,
       Periodo,
       PeriodoOrdem
  From (
         Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_Jan1AnoBarras
           Union All
          Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_Jan2AnoBarras 
           Union All          
          Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_Jan3AnoBarras 
   
           Union All
           
          Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_Dez1AnoBarras
           Union All
          Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_Dez2AnoBarras 
           Union All          
          Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_Dez3AnoBarras            
           Union All
           Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_DemaisMeses1AnoBarras            
           Union All
           Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_DemaisMeses2AnoBarras                        
           Union All
           Select EmpFil,
                CodArea,
                Area,
                CodFuncao,
                Funcao,
                Registro,
                funcionario,
                codEvento,
                evento,
                CompetAcumu,
                referAcumu,
                minutos,
                Periodo,
                PeriodoOrdem
            From pbi_he_DemaisMeses3AnoBarras                    
)

