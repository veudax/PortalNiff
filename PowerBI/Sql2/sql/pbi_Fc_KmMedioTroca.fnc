CREATE OR REPLACE Function pbi_Fc_KmMedioTroca (pEmpresa Number,
                                                pFilial Number,
                                                pVeiculo Number,
                                                pMaterial Number,
                                                pMesAno Varchar2)                                                 
                  Return Number Is

  vKmAnterior Number;
  vKmAtual Number;
  vKmMedio Number;
  vKmCalcAnterior Number;
  vQuantidade Number;
  vMaterial Number;
  vMesAno Varchar2(7);
  vMesFin Varchar2(7);
  vEmpresa Number;
  vFilial Number;
  vVeiculo Number;
  vKm Number;

  Cursor cTroca  Is
     Select m.codigointernomaterial, Count(*)qd, min(o.kmexecucaoos) menorKm, Max(o.Kmexecucaoos) maiorKm,
           To_char(e.datarq,'yyyymm') mesano, 
           x.menormesano,
           v.prefixoveic, m.codigomatint,
           e.CodigoEmpresa, e.CodigoFl, v.codigoveic
      From man_OS o,
           est_requisicao e,
           Est_Cadmaterial m,
           est_itensrequisicao i,
           frt_cadveiculos v,
           ( Select m.codigomatint, v.codigoveic,
                    Min( To_char(e.datarq,'yyyymm') ) menormesano
               From man_OS o,
                    est_requisicao e,
                    Est_Cadmaterial m,
                    est_itensrequisicao i,
                    frt_cadveiculos v
              Where e.codintos = o.codintos
                And e.datarq Between Trunc(SYSDATE,'rr') And Sysdate
                And i.codigomatint = m.codigomatint 
                And i.numerorq = e.numerorq
                And e.codigoveic = v.codigoveic
              Group By m.codigomatint,v.codigoveic  ) x   
    Where e.codintos = o.codintos
      And e.datarq Between Trunc(SYSDATE,'rr') And Sysdate
      And i.codigomatint = m.codigomatint
      And i.numerorq = e.numerorq
      And e.codigoveic = v.codigoveic
      And e.codigoempresa = pEmpresa
      And e.codigofl = pFilial
      And v.codigoveic = pVeiculo
      And m.codigomatint = pMaterial
      And To_char(e.datarq,'yyyymm') = pMesAno
      And x.codigoveic = v.codigoveic
      And x.codigomatint = m.codigomatint
    Group By m.codigointernomaterial, 
             To_char(e.datarq,'yyyymm'), v.prefixoveic, m.codigomatint,
             e.CodigoEmpresa, e.CodigoFl, v.codigoveic, x.menormesano           
    Order By e.CodigoEmpresa, e.CodigoFl, v.CodigoVeic, m.codigomatint, MesAno;
  
  
  Cursor cUltimoKm Is
    Select o.Kmexecucaoos inicial,0 atual
    From man_OS o,
         est_requisicao e,
         Est_Cadmaterial m,
         est_itensrequisicao i,
         frt_cadveiculos v
   Where e.codintos = o.codintos
     And e.codigoempresa = vEmpresa
     And e.codigofl = vFilial
     And v.CodigoVeic = vVeiculo
     And m.codigomatint = vmaterial
     And i.codigomatint = m.codigomatint 
     And i.numerorq = e.numerorq
     And e.codigoveic = v.codigoveic
     And e.datarq = ( Select Max(e.datarq)
                        From man_OS o,
                             est_requisicao e,
                             Est_Cadmaterial m,
                             est_itensrequisicao i,
                             frt_cadveiculos v
                       Where e.codintos = o.codintos
                         And e.codigoempresa = vEmpresa
                         And e.Codigofl = vFilial
                         And To_char(e.datarq,'yyyymm') < vMesAno
                         And v.CodigoVeic = vVeiculo
                         And m.codigomatint = vmaterial
                         And i.codigomatint = m.codigomatint 
                         And i.numerorq = e.numerorq
                         And e.codigoveic = v.codigoveic )
   Union All          
  Select Distinct 0 inicial, o.Kmexecucaoos Atual
    From man_OS o,
         est_requisicao e,
         Est_Cadmaterial m,
         est_itensrequisicao i,
         frt_cadveiculos v
   Where e.codintos = o.codintos
     And e.codigoempresa = vEmpresa
     And e.codigofl = vFilial
     And v.CodigoVeic = vVeiculo
     And m.codigomatint = vmaterial
     And i.codigomatint = m.codigomatint 
     And i.numerorq = e.numerorq
     And e.codigoveic = v.codigoveic
     And To_char(e.datarq,'yyyymm') Between vMesAno And vMesFin;
  
  Procedure populaVariaveis (material Number, Empresa Number,  Filial Number,  MesAno Varchar2, Veiculo Number, MesFim Varchar2) Is
  Begin
     vMaterial := material;
     vEmpresa := Empresa;
     vFilial := Filial;
     vMesAno := MesAno;
     vMesFin := MesFim;
     vVeiculo := Veiculo;
  End;
  
Begin
   vKmAnterior := 0;
   vMaterial := 0;
   vKmMedio := 0;
   vKm := 0;
   vKmCalcAnterior := 0;
   vQuantidade := 0;
   
   For rTroca In ctroca Loop
      
      populaVariaveis (rTroca.Codigomatint, rTroca.Codigoempresa, rTroca.Codigofl, rTroca.Menormesano, rtroca.Codigoveic, rtroca.Mesano);

      For rKm In cUltimoKm Loop
      
         vKmAtual := rKm.Atual;

         If vKmAnterior = 0 Then
         
           vKmAnterior := rKm.Inicial;
         
         Else
         
           vQuantidade := vQuantidade + 1;
           
           vKm := Abs(vKmAtual - vKmAnterior);
           
           vKmAnterior := vKmAtual;
         
           If (vQuantidade = 0) Then
              vKmMedio := (vKm + vKmCalcAnterior) / 1;
           Else
              vKmMedio := (vKm + vKmCalcAnterior) / vQuantidade;
           End If;
      
           vKmCalcAnterior := vKmCalcAnterior + vKm;         
         
         End If;         
         
        End Loop;
        
   End Loop;
      
   vKm := Abs(vKmAtual - vKmAnterior);
     
   vKmAnterior := vKmAtual;
   
   If (vQuantidade = 0) Then
      vKmMedio := (vKm + vKmCalcAnterior) / 1;
   Else
      vKmMedio := (vKm + vKmCalcAnterior) / vQuantidade;
   End If;
      
   Return vKmMedio;                                      
End;
/
