
Declare
Begin

  Dbms_Scheduler.create_job(job_name => 'SCH_NIFF_AcompanhaExcecucaoOs',
  job_type => 'STORED_PROCEDURE',
  job_action => 'pr_niff_ExecutaAcompamento',
  start_date => '22-OCT-08 04.00.00 AM',
  repeat_interval => 'FREQ=DAILY; BYHOUR=4;',  
  enabled => True, 
  comments => 'Grava OS Preventiva/Corretiva no Quadrimestre das empresas todos os dias as 4am');
                        
End;