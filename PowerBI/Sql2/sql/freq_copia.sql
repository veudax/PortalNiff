Select d.*
  From frq_digitacaomovimento d
 Where d.codintfunc In (Select codintfunc From vw_funcionarios f Where codigoempresa = 1 And codigofl = 1 And codarea = 20)
   And d.dtdigit Between '21-mar-2020' And '30-apr-2020'; 

Select d.*
  From frq_digitacaomovto_historico d
 Where d.codintfunc In (Select codintfunc From vw_funcionarios f Where codigoempresa = 1 And codigofl = 1 And codarea = 20)
   And d.dtdigit Between '21-mar-2020' And '30-apr-2020';
   
Select d.*
  From frq_digitacaomovto_intervalos d
 Where d.codintfunc In (Select codintfunc From vw_funcionarios f Where codigoempresa = 1 And codigofl = 1)
   And d.dtdigit Between '21-mar-2020' And '20-sep-2020';
      
Select d.*
  From frq_digitacao d
 Where d.codintfunc In (Select codintfunc From vw_funcionarios f Where codigoempresa = 1 And codigofl = 1 And codarea = 20)
   And d.dtdigit Between '21-mar-2020' And '30-apr-2020'
Order By codintfunc, dtdigit, tipodigit;   