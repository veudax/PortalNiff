/*
Delete frq_bancohoras_even t
Where t.codintfunc In (Select codintfunc From vw_funcionarios f 
                        Where codigoempresa = 26
                          And codigofl = 1
                          -- And f.CODAREA = 40
                       )
And t.competencia >= '01-jun-2020' ;*/


select t.*, t.rowid from frq_bancohoras t
Where t.codintfunc In (Select codintfunc From vw_funcionarios f Where codigoempresa = 26 And codigofl = 1)-- And f.CODAREA = 40)
And t.competencia >= '01-jun-2020' ;


select t.*, t.rowid from frq_bancohoras_even t
Where t.codintfunc In (Select codintfunc From vw_funcionarios f 
                        Where codigoempresa = 26
                        And codigofl = 1
                        -- And f.CODAREA = 40
                        )
And t.competencia >= '01-jun-2020' ;
