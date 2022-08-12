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
              historico, 
              Numero)
     Values ( (Select Nvl(Max(idrepos),0)+1 From Niff_Reposcontabil)
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
            , :New.historico
            , (Select Nvl(Max(numero),0)+1 From Niff_Reposcontabil));
  Else
     Update niff_reposcontabil
        Set Situacao = SituacaoNew
      Where coditemlanca = :New.coditemlanca
        And CodLanca = :New.Codlanca;  
  End If;
End tr_Niff_ImportaCigam;
