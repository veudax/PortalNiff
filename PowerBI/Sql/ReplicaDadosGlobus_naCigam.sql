CREATE OR REPLACE PROCEDURE "REPLICA_NIFF_REPOSCONTABIL_AUX" (DATAINI Date, DATAFIM Date) AS
-- v_DATAINI date := to_date(DATAINI,'dd/mm/rr');
-- v_DATAFIM date := to_date(DATAFIM,'dd/mm/rr');
 v_ERRO number;
 v_id Number;
-- v_numero Number;
-- v_data Date;
 BEGIN
   FOR V_FUNC IN
       (select rownum ,l.codigoempresa ,l.codigofl ,l.codlanca ,i.coditemlanca , l.dtlanca dtlanca,l.lotelanca ,l.sistema ,
  l.documentolanca ,'I' I , l.lctomodificado ,l.usuario , sysdate, i.codcontactb, decode(i.contrapartitemlanca, null, null, i.contrapartitemlanca) contrapartitemlanca ,
   i.vritemlanca, decode(i.codcusto, null, null, i.codcusto) codcusto ,i.debitocreditoitemlanca ,'0' O , tiraecomercial(i.historicoitemlanca) descricao
   from ctblanca l, ctbitlnc i
    where l.codlanca = i.codlanca
     and l.dtlanca between DATAINI and DATAFIM
     And l.codigoempresa = 29
     Order By dtlanca)
 LOOP
     Begin
       Select Nvl(max(IDREPOS),0)+1 
       Into v_id
       From CIGAM.NIFF_REPOSCONTABIL_AUX@CIGAMP;
       /*
       If (v_numero Is Null) Then
          v_numero := 1;
       Else
         If v_data <> V_FUNC.DTLANCA Then
           v_numero := 1;
           Else
           v_numero := v_numero + 1;
         end If;
       End If;
       v_data := v_func.dtlanca;*/
          
       INSERT INTO CIGAM.NIFF_REPOSCONTABIL_AUX@CIGAMP (
   IDREPOS,
   CODIGOEMPRESA,
   CODIGOFL,
   CODLANCA,
   CODITEMLANCA,
   DATA,
   LOTE,
   ORIGEM,
   DOCUMENTO,
   SITUACAO,
   MODIFICADO,
   USUARIOCRIACAO,
   DATACRIACAO,
   CONTACONTABIL,
   CONTRAPARTIDA,
   VALOR,
   CODIGOCUSTO,
   DEBCRED,
   STATUS,
   HISTORICO,
   Numero )
   VALUES (
   v_id,
   V_FUNC.CODIGOEMPRESA,
   V_FUNC.CODIGOFL,
   V_FUNC.CODLANCA,
   V_FUNC.CODITEMLANCA,
   V_FUNC.DTLANCA,
   V_FUNC.LOTELANCA,
   V_FUNC.SISTEMA,
   V_FUNC.DOCUMENTOLANCA,
   V_FUNC.I,
   V_FUNC.LCTOMODIFICADO,
   V_FUNC.USUARIO,
   V_FUNC.SYSDATE,
   V_FUNC.CODCONTACTB,
   V_FUNC.CONTRAPARTITEMLANCA,
   V_FUNC.VRITEMLANCA,
   V_FUNC.CODCUSTO,
   V_FUNC.DEBITOCREDITOITEMLANCA,
   V_FUNC.O,
   V_FUNC.DESCRICAO,
   v_id );
 exception
    when others then
     --erro chamada insert remoto
  v_ERRO := 1;
    raise;
 end;
end loop;
commit;
   begin
       CIGAM.PR_NIFF_IMPORTACIGAM@CIGAMP(DATAINI, DATAFIM );
   exception
   when others then
 --erro chamada procedure remota
  v_ERRO := 2;
   end;
    EXCEPTION
     WHEN OTHERS THEN
          rollback;
          DBMS_OUTPUT.PUT_LINE('Erro na execucao da procedure.');
    DBMS_OUTPUT.PUT_LINE( 'Codigo Erro: '|| v_ERRO );
          DBMS_OUTPUT.PUT_LINE( 'Codigo Oracle: ' || SQLCODE);
          DBMS_OUTPUT.PUT_LINE( 'Mensagem Oracle: ' || SQLERRM);
 END;
