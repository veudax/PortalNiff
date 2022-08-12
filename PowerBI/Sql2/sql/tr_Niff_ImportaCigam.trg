create or replace trigger tr_Niff_ImportaCigam before insert on Niff_Reposcontabil_Aux
  for each row
declare
  SituacaoOld Varchar2(1);
  SituacaoNew Varchar2(1);
  
Begin

  begin
    Select Modificado
      Into situacaoOld
      From niff_reposcontabil
     Where CoditemLanca = :New.CoditemLanca
       And CodLanca = :New.Codlanca;
  exception
    when no_data_found then
      situacaoOld := 'N';
  end;

  If (SituacaoOld = 'I' And :New.Modificado = 'S') Then
     situacaoNew := 'M';
  Else
     situacaoNew := 'I';
  End If;

  If situacaoOld = 'N' Then
     Insert Into Niff_Reposcontabil (idrepos,
              codigoempresa,
              codigofl,
              codlanca,
              coditemlanca,
              data,
              lote,
              origem,
              documento,
              situacao,
              modificado,
              usuariocriacao,
              datacriacao,
              contacontabil,
              contrapartida,
              valor,
              codigocusto,
              debcred,
              status,
              historico)
     Values ( (Select Max(idrepos)+1 From Niff_Reposcontabil)
            , :New.CodigoEmpresa
            , :New.CodigoFl
            , :New.codlanca
            , :New.coditemlanca
            , :New.data
            , :New.lote
            , :New.origem
            , :New.documento
            , 'I'
            , :New.modificado
            , :New.usuariocriacao
            , :New.datacriacao
            , :New.contacontabil
            , :New.contrapartida
            , :New.valor
            , :New.codigocusto
            , :New.debcred
            , :New.status
            , :New.historico);
  Else
     Update niff_reposcontabil
        Set Situacao = SituacaoNew
      Where coditemlanca = :New.coditemlanca
        And CodLanca = :New.Codlanca;  
  End If;

 /* For rMov In cMovto Loop
    begin
      Select 'S'
        Into Existe
        From niff_reposcontabil_Aux
       Where CoditemLanca = rMov.CoditemLanca
         And CodLanca = :New.Codlanca;
    exception
      when no_data_found then
        Existe := 'N';
    end;
    
    If Existe = 'N' Then
      Update niff_reposcontabil
          Set Situacao = 'E'
        Where coditemlanca = rMov.CoditemLanca
          And CodLanca = :New.Codlanca;
    End If;
  End Loop;*/

End tr_Niff_ImportaCigam;
/
