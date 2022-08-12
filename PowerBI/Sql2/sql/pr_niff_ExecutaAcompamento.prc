CREATE OR REPLACE Procedure pr_niff_ExecutaAcompamento Is

  Cursor cEmpresa Is
    Select codigoempresa, codigofl
      From ctr_filial f
     Where f.codigoempresa In (1,26)
       And f.codigofl = 1; -- incluir as outras empresas depois que for necessitando
Begin

  For rEmp In cEmpresa Loop
    pr_AcompanhamentoOSPreventiva(rEmp.CodigoEmpresa, rEmp.CodigoFl, 5000);
    pr_AcompanhamentoOSPreventiva(rEmp.CodigoEmpresa, rEmp.CodigoFl, 15000);
    pr_AcompanhamentoOSPreventiva(rEmp.CodigoEmpresa, rEmp.CodigoFl, 30000);
  End Loop;
End;
/
