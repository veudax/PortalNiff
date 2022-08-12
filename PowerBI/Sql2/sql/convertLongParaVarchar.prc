CREATE OR REPLACE Procedure teste Is

  CURSOR cur_msg IS 
  SELECT a.feedgestornew, a.idautoavaliacao
    FROM niff_ads_avaliacao a
   Where a.feedgestornew Is Not Null; 
    
  feedback Varchar2(4000);
Begin

  For rlinha In  cur_msg Loop
     
   feedback := trim(SubStr(rLinha.Feedgestornew,1,4000));
   Update niff_ads_avaliacao a
     Set a.feedbackgestor = feedback
    Where a.idautoavaliacao = rlinha.idautoavaliacao;
   
  End Loop; 
End;
/
