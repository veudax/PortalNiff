CREATE OR REPLACE Procedure pr_AcompanhamentoOSPreventiva (pEmpresa Number, pFilial Number, pKm Number) Is

  v_codIntOS Number;
  v_codintMaterial Number;
  v_codVeic Number;
  v_id Number;
  
   Cursor cObrigatorio15Mil Is
    Select Distinct o.codintos, o.numeroos OS, p.codigoplanrev, o.codigoveic, 15000 km,
           po.Descricaomat pecasobrigatorias, po.codigomatint, po.codigointernomaterial CodigoMat,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoEmpresa, o.Codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , pbi_veiculosepecasporkm po
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'P'
       And p.codigoplanrev = 195
       And po.KM = 15000
       And o.codigoveic = po.codigoveic
       And o.codigoempresa = po.codigoempresa
       And o.codigofl = po.codigofl
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                   
   Cursor cObrigatorio30Mil Is
    Select Distinct o.codintos, o.numeroos OS, p.codigoplanrev, o.codigoveic, 30000 km,
           po.Descricaomat pecasobrigatorias, po.codigomatint, po.codigointernomaterial CodigoMat,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoEmpresa, o.Codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , pbi_veiculosepecasporkm po
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'P'
       And p.codigoplanrev = 196
       And po.KM = 30000
       And o.codigoveic = po.codigoveic
       And o.codigoempresa = po.codigoempresa
       And o.codigofl = po.codigofl
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);

   Cursor cObrigUtilizadas15Mil Is
    Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, x.km,
           cm.descricaomat PecasUtilizadas, cm.codigointernomaterial Codigomat, cm.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , EST_REQUISICAO r
         , EST_ITENSREQUISICAO i
         , EST_MOVTO m
         , EST_ITENSMOVTO ie
         , EST_CADMATERIAL cm
         , (Select codigoveic, x.Marca, x.KM, x.codigoempresa, x.codigofl From pbi_veiculosepecasporkm x Where x.KM = pKm) x
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'P'
       And p.codigoplanrev = 195
       And r.codintos = o.codintos
       And r.numerorq = i.numerorq
       And r.numerorq = m.numerorq
       And ie.datamovto = m.datamovto
       And ie.seqmovto = m.seqmovto
       And cm.codigomatint = ie.codigomatint
       And o.codigoveic = x.codigoveic
       And o.codintos = v_codIntOS
       And cm.codigomatint = v_codintMaterial
       And o.codigoempresa = x.codigoempresa
       And o.codigofl = x.codigofl       
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                   
   Cursor cObrigUtilizadas30Mil Is
    Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, x.km,
           cm.descricaomat PecasUtilizadas, cm.codigointernomaterial Codigomat, cm.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , EST_REQUISICAO r
         , EST_ITENSREQUISICAO i
         , EST_MOVTO m
         , EST_ITENSMOVTO ie
         , EST_CADMATERIAL cm
         , (Select codigoveic, x.Marca, x.KM, x.codigoempresa, x.codigofl From pbi_veiculosepecasporkm x Where x.KM = pKm) x
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'P'
       And p.codigoplanrev = 196
       And r.codintos = o.codintos
       And r.numerorq = i.numerorq
       And r.numerorq = m.numerorq
       And ie.datamovto = m.datamovto
       And ie.seqmovto = m.seqmovto
       And cm.codigomatint = ie.codigomatint
       And o.codigoveic = x.codigoveic
       And o.codintos = v_codIntOS
       And cm.codigomatint = v_codintMaterial
       And o.codigoempresa = x.codigoempresa
       And o.codigofl = x.codigofl       
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                   

   Cursor cUtilizadas5Mil Is
    Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, x.km,
           cm.descricaomat PecasUtilizadas, cm.codigointernomaterial Codigomat, cm.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , EST_REQUISICAO r
         , EST_ITENSREQUISICAO i
         , EST_MOVTO m
         , EST_ITENSMOVTO ie
         , EST_CADMATERIAL cm
         , (Select codigoveic, x.Marca, x.KM, x.codigoempresa, x.codigofl From pbi_veiculosepecasporkm x Where x.KM = pKm) x
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And p.codigoplanrev = 194
       And o.tipoos = 'P'
       And r.codintos = o.codintos
       And r.numerorq = i.numerorq
       And r.numerorq = m.numerorq
       And ie.datamovto = m.datamovto
       And ie.seqmovto = m.seqmovto
       And cm.codigomatint = ie.codigomatint
       And o.codigoveic = x.codigoveic
       And o.codigoempresa = x.codigoempresa
       And o.codigofl = x.codigofl            
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);


  Cursor cUtilizadas15Mil Is
    Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, x.km,
           cm.descricaomat PecasUtilizadas, cm.codigointernomaterial Codigomat, cm.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , EST_REQUISICAO r
         , EST_ITENSREQUISICAO i
         , EST_MOVTO m
         , EST_ITENSMOVTO ie
         , EST_CADMATERIAL cm
         , (Select codigoveic, x.Marca, x.KM, x.codigoempresa, x.codigofl From pbi_veiculosepecasporkm x Where x.KM = pKm) x
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'P'
       And p.codigoplanrev = 195
       And r.codintos = o.codintos
       And r.numerorq = i.numerorq
       And r.numerorq = m.numerorq
       And ie.datamovto = m.datamovto
       And ie.seqmovto = m.seqmovto
       And cm.codigomatint = ie.codigomatint
       And o.codigoveic = x.codigoveic
       And o.codigoempresa = x.codigoempresa
       And o.codigofl = x.codigofl       
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                   
   Cursor cUtilizadas30Mil Is
    Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, x.km,
           cm.descricaomat PecasUtilizadas, cm.codigointernomaterial Codigomat, cm.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , EST_REQUISICAO r
         , EST_ITENSREQUISICAO i
         , EST_MOVTO m
         , EST_ITENSMOVTO ie
         , EST_CADMATERIAL cm
         , (Select codigoveic, x.Marca, x.KM, x.codigoempresa, x.codigofl From pbi_veiculosepecasporkm x Where x.KM = pKm) x
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'P'
       And p.codigoplanrev = 196
       And r.codintos = o.codintos
       And r.numerorq = i.numerorq
       And r.numerorq = m.numerorq
       And ie.datamovto = m.datamovto
       And ie.seqmovto = m.seqmovto
       And cm.codigomatint = ie.codigomatint
       And o.codigoveic = x.codigoveic
       And o.codigoempresa = x.codigoempresa
       And o.codigofl = x.codigofl       
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                   
   Cursor cGravados Is
     Select * From Pbi_niff_AcompOsPrev
     Where codigoEmpresa = pEmpresa
       And codigofl = pFilial
       And km = pKm;
              
   Cursor cGravadosUtilizados Is
     Select * From Pbi_niff_AcompOsPrev
     Where codigoEmpresa = pEmpresa
       And codigofl = pFilial
       And km = pKm
       And codintmaterialutil Is Not Null;
             
  Cursor cCorUtilizadas5Mil Is
    Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, x.km,
           cm.descricaomat PecasUtilizadas, cm.codigointernomaterial Codigomat, cm.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , EST_REQUISICAO r
         , EST_ITENSREQUISICAO i
         , EST_MOVTO m
         , EST_ITENSMOVTO ie
         , EST_CADMATERIAL cm
         , (Select codigoveic, x.Marca, x.KM, x.codigoEmpresa, x.CodigoFl From pbi_veiculosepecasporkm x Where x.KM = pKm) x
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'C'
       And p.codigoplanrev = 195
       And r.codintos = o.codintos
       And r.numerorq = i.numerorq
       And r.numerorq = m.numerorq
       And ie.datamovto = m.datamovto
       And ie.seqmovto = m.seqmovto
       And cm.codigomatint = ie.codigomatint
       And o.codigoveic = x.codigoveic
       And o.codigoempresa = x.codigoempresa
       And o.codigofl = x.codigofl       
       And o.Codigoveic = v_codVeic
       And cm.codigomatint = v_codintMaterial
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                                 
   Cursor cCorUtilizadas15Mil Is
    Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, x.km,
           cm.descricaomat PecasUtilizadas, cm.codigointernomaterial Codigomat, cm.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , EST_REQUISICAO r
         , EST_ITENSREQUISICAO i
         , EST_MOVTO m
         , EST_ITENSMOVTO ie
         , EST_CADMATERIAL cm
         , (Select codigoveic, x.Marca, x.KM, x.Codigoempresa, x.CodigoFl From pbi_veiculosepecasporkm x Where x.KM = pKm) x
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'C'
       And p.codigoplanrev = 195
       And r.codintos = o.codintos
       And r.numerorq = i.numerorq
       And r.numerorq = m.numerorq
       And ie.datamovto = m.datamovto
       And ie.seqmovto = m.seqmovto
       And cm.codigomatint = ie.codigomatint
       And o.codigoveic = x.codigoveic
       And o.codigoempresa = x.codigoempresa
       And o.codigofl = x.codigofl       
       And o.Codigoveic = v_codVeic
       And cm.codigomatint = v_codintMaterial
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                   
   Cursor cCorUtilizadas30Mil Is
    Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, x.km,
           cm.descricaomat PecasUtilizadas, cm.codigointernomaterial Codigomat, cm.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           o.dataaberturaos Data
      From Man_Os o
         , Man_Osprevisto p
         , EST_REQUISICAO r
         , EST_ITENSREQUISICAO i
         , EST_MOVTO m
         , EST_ITENSMOVTO ie
         , EST_CADMATERIAL cm
         , (Select codigoveic, x.Marca, x.KM, x.codigoempresa, x.codigofl From pbi_veiculosepecasporkm x Where x.KM = pKm) x
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
       And o.condicaoos = 'FC'
       And o.tipoos = 'C'
       And p.codigoplanrev = 196
       And r.codintos = o.codintos
       And r.numerorq = i.numerorq
       And r.numerorq = m.numerorq
       And ie.datamovto = m.datamovto
       And ie.seqmovto = m.seqmovto
       And cm.codigomatint = ie.codigomatint
       And o.codigoveic = x.codigoveic
       And o.codigoempresa = x.codigoempresa
       And o.codigofl = x.codigofl       
       And o.Codigoveic = v_codVeic
       And cm.codigomatint = v_codintMaterial
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                   
                                                 
   Procedure InsereUtil (pCodIntOs Number, pData Date, pNumero Varchar2, pVeic Number, pCodIntMat Number, pMat Varchar2, pDesc Varchar2) Is
   Begin
        Insert Into Pbi_niff_AcompOsPrev (id, 
                                      codintos, 
                                      data, 
                                      numero, 
                                      codigoveic, 
                                      codintmaterialutil, 
                                      materialutil, 
                                      km, 
                                      pecasutil, 
                                      codigoempresa, 
                                      codigofl)
                             Values ((Select nvl(Max(id),0)+1 From Pbi_niff_AcompOsPrev)        
                                    , pCodintos
                                    , pData
                                    , pNumero
                                    , pVeic
                                    , pCodIntMat
                                    , pMat
                                    , pkm
                                    , pDesc
                                    , pEmpresa
                                    , pFilial);  
   End;    
       
   Procedure InsereCor (pCodIntOs Number, pData Date, pNumero Varchar2, pVeic Number, pCodIntMat Number, pMat Varchar2, pDesc Varchar2) Is
   Begin
        Insert Into Pbi_niff_AcompOsCor (id, 
                                      codintos, 
                                      data, 
                                      numero, 
                                      codigoveic, 
                                      codintmaterial, 
                                      material, 
                                      km, 
                                      pecasutil, 
                                      codigoempresa, 
                                      codigofl)
                             Values ((Select nvl(Max(id),0)+1 From Pbi_niff_AcompOsCor)        
                                    , pCodintos
                                    , pData
                                    , pNumero
                                    , pVeic
                                    , pCodIntMat
                                    , pMat
                                    , pkm
                                    , pDesc
                                    , pEmpresa
                                    , pFilial);  
   End;    
              
Begin
  
  Delete Pbi_niff_AcompOsPrev
    Where CodigoEmpresa = pEmpresa
      And CodigoFl  = pFilial
      And km = pkm;
  
  Commit;

  If pkm = 15000 Then
    for rOb in cObrigatorio15Mil loop
       Insert Into Pbi_niff_AcompOsPrev (id, 
                                        codintos, 
                                        data, 
                                        numero, 
                                        codigoveic, 
                                        codintmaterialobrig, 
                                        codintmaterialutil, 
                                        materialobrig, 
                                        materialutil, 
                                        km, 
                                        pecasobrig, 
                                        pecasutil, 
                                        codigoempresa, 
                                        codigofl)
                               Values ((Select nvl(Max(id),0)+1 From Pbi_niff_AcompOsPrev)        
                                      , rOb.Codintos
                                      , rob.data
                                      , rob.os
                                      , rob.codigoveic
                                      , rob.codigomatint
                                      , Null
                                      , rob.codigomat
                                      , Null
                                      , rob.km
                                      , rob.pecasobrigatorias
                                      , Null
                                      , rob.codigoempresa
                                      , rob.codigofl);
      Commit;                                    
    End Loop;
  End If;
  
  If pkm = 30000 Then
    for rOb in cObrigatorio30Mil loop
       Insert Into Pbi_niff_AcompOsPrev (id, 
                                        codintos, 
                                        data, 
                                        numero, 
                                        codigoveic, 
                                        codintmaterialobrig, 
                                        codintmaterialutil, 
                                        materialobrig, 
                                        materialutil, 
                                        km, 
                                        pecasobrig, 
                                        pecasutil, 
                                        codigoempresa, 
                                        codigofl)
                               Values ((Select nvl(Max(id),0)+1 From Pbi_niff_AcompOsPrev)        
                                      , rOb.Codintos
                                      , rob.data
                                      , rob.os
                                      , rob.codigoveic
                                      , rob.codigomatint
                                      , Null
                                      , rob.codigomat
                                      , Null
                                      , rob.km
                                      , rob.pecasobrigatorias
                                      , Null
                                      , rob.codigoempresa
                                      , rob.codigofl);
      Commit;                                    
    End Loop;
  End If;
  
  If (pKm <> 5000) Then  
    For rGrava In cGravados Loop
       v_codIntOS := rGrava.Codintos;
       v_codintMaterial := rGrava.Codintmaterialobrig;
       
       -- Verifica se o Material obrigatorio foi utilizado, Se utilizado atualiza os dados 
       If (pKm = 15000) Then
         For rutil In cObrigUtilizadas15Mil Loop
           Update Pbi_niff_AcompOsPrev u
              Set u.codintmaterialutil = rGrava.Codintmaterialobrig
                , u.materialutil = rGrava.Materialobrig
                , u.pecasutil = rGrava.Pecasobrig
            Where u.codintos = v_codIntOS
              And u.codintmaterialobrig = v_codintMaterial
              And u.km = pkm;
         End Loop;    
       End If;
       
       If (pKm = 30000) Then
         For rutil In cObrigUtilizadas30Mil Loop
           Update Pbi_niff_AcompOsPrev u
              Set u.codintmaterialutil = rGrava.Codintmaterialobrig
                , u.materialutil = rGrava.Materialobrig
                , u.pecasutil = rGrava.Pecasobrig
            Where u.codintos = v_codIntOS
              And u.codintmaterialobrig = v_codintMaterial
              And u.km = pkm;
         End Loop;
       End If;
       Commit;
    End Loop;
  End If;
  
  If (pkm = 5000) Then
    For rUtil In cUtilizadas5Mil Loop
      -- verifica se os Utilizados existem, se n?o existir inclui
      begin
        Select g.id
          Into v_id
          From Pbi_niff_AcompOsPrev g
         Where g.codintos = rUtil.Codintos
           And g.codintmaterialutil = rutil.codigomatint
           And g.km = pkm;
      exception
        when no_data_found then
          v_id := 0;
      end;
      
      If (v_id = 0) Then
        InsereUtil(rUtil.Codintos
                  , rUtil.data
                  , rUtil.Numeroos
                  , rUtil.codigoveic
                  , rUtil.codigomatint
                  , rUtil.codigomat
                  , rUtil.Pecasutilizadas);

      End If;
      Commit;
    End Loop;
  End If;

  If (pkm = 15000) Then
    For rUtil In cUtilizadas15Mil Loop
      -- verifica se os Utilizados existem, se n?o existir inclui
      begin
        Select g.id
          Into v_id
          From Pbi_niff_AcompOsPrev g
         Where g.codintos = rUtil.Codintos
           And g.codintmaterialutil = rutil.codigomatint
           And g.km = pkm;
      exception
        when no_data_found then
          v_id := 0;
      end;
      
      If (v_id = 0) Then
        InsereUtil(rUtil.Codintos
                  , rUtil.data
                  , rUtil.Numeroos
                  , rUtil.codigoveic
                  , rUtil.codigomatint
                  , rUtil.codigomat
                  , rUtil.Pecasutilizadas);

      End If;
      Commit;
    End Loop;
  End If;
  
  If (pkm = 30000) Then
    For rUtil In cUtilizadas30Mil Loop
      -- verifica se os Utilizados existem, se n?o existir inclui
      begin
        Select g.id
          Into v_id
          From Pbi_niff_AcompOsPrev g
         Where g.codintos = rUtil.Codintos
           And g.codintmaterialutil = rutil.codigomatint
           And g.km = pkm;
      exception
        when no_data_found then
          v_id := 0;
      end;
      
      If (v_id = 0) Then
        InsereUtil(rUtil.Codintos
                  , rUtil.data
                  , rUtil.Numeroos
                  , rUtil.codigoveic
                  , rUtil.codigomatint
                  , rUtil.codigomat
                  , rUtil.Pecasutilizadas);

      End If;
      Commit;
    End Loop;
  End If;
    
  -- Grava as Corretivas
  
    For rGrava In cGravadosUtilizados Loop
       v_codIntOS := rGrava.Codintos;
       v_codintMaterial := rGrava.Codintmaterialutil;
       v_codVeic := rGrava.Codigoveic;
       
       If (pKm = 5000) Then
         For rutil In cCorUtilizadas5Mil Loop
           InsereCor(rUtil.Codintos
                  , rUtil.data
                  , rUtil.Numeroos
                  , rUtil.codigoveic
                  , rUtil.Codigomatint
                  , rUtil.codigomat
                  , rUtil.Pecasutilizadas);
         End Loop;    
       End If;       
       
       If (pKm = 15000) Then
         For rutil In cCorUtilizadas15Mil Loop
           InsereCor(rUtil.Codintos
                  , rUtil.data
                  , rUtil.Numeroos
                  , rUtil.codigoveic
                  , rUtil.Codigomatint
                  , rUtil.codigomat
                  , rUtil.Pecasutilizadas);
         End Loop;    
       End If;
       
       If (pKm = 30000) Then
         For rutil In cCorUtilizadas30Mil Loop
            InsereCor(rUtil.Codintos
                  , rUtil.data
                  , rUtil.Numeroos
                  , rUtil.codigoveic
                  , rUtil.Codigomatint
                  , rUtil.codigomat
                  , rUtil.Pecasutilizadas);
         End Loop;
       End If;
       Commit;
    End Loop;  
End;
/
