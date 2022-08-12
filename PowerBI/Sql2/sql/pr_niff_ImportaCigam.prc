CREATE OR REPLACE Procedure pr_niff_ImportaCigam(DATAINI Date, DATAFIM Date)  Is
  Existe Varchar2(1);

  Cursor cMovto is
    Select CoditemLanca, codlanca, Situacao, idrepos
      from niff_reposcontabil e
      Where e.data Between DATAINI and DATAFIM;
--    Where codigoempresa = 16;

Begin
   --verifica se tem dados excluido do globus
  For rMov In cMovto Loop
    begin
      Select 'S'
        Into Existe
        From niff_reposcontabil_Aux
       Where CoditemLanca = rMov.CoditemLanca
         And CodLanca = rMov.Codlanca;
    exception
      when no_data_found then
        Existe := 'N';
    end;

    If Existe = 'N' Then
      Update niff_reposcontabil e
          Set Situacao = 'E'
        Where e.idrepos = rMov.IdRepos;
    End If;
  End Loop;
  
  Commit;
End;
/
