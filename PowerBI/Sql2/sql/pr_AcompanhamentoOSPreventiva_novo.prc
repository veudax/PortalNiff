CREATE OR REPLACE Procedure pr_AcompanhamentoOSPreventiva (pEmpresa Number, pFilial Number, pKm Number) Is

  v_codPlano Number;
  v_codIntOS Number;
  v_codintMaterial Number;
  v_codVeic Number;
  v_id Number;
  v_data Date;
  
   Cursor cObrigatorio15Mil Is
    Select Distinct o.codintos, o.numeroos OS, p.codigoplanrev, o.codigoveic, pKM km,
           po.Descricaomat pecasobrigatorias, po.codigomatint, po.codigointernomaterial CodigoMat,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoEmpresa, o.Codigofl,
           o.dataaberturaos Data, po.Quantidade
      From Man_Os o
         , Man_Osprevisto p
         , pbi_veiculosepecasporkm po
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
--       And o.condicaoos = 'FC'
       And o.tipoos = 'P'
       And p.codigoplanrev = 195
       And po.KM = pkm
       And o.codigoveic = po.codigoveic
       And o.codigoempresa = po.codigoempresa
       And o.codigofl = po.codigofl
--       And o.codintos = 1477651 -- apenas para teste
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);
                                   
   Cursor cObrigatorio30Mil Is
    Select Distinct o.codintos, o.numeroos OS, p.codigoplanrev, o.codigoveic, pKm km,
           po.Descricaomat pecasobrigatorias, po.codigomatint, po.codigointernomaterial CodigoMat,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoEmpresa, o.Codigofl,
           o.dataaberturaos Data, po.Quantidade
      From Man_Os o
         , Man_Osprevisto p
         , pbi_veiculosepecasporkm po
     Where o.codintos = p.codintos
       And o.codigoempresa = pEmpresa
       And o.codigoFl = pFilial
--       And o.condicaoos = 'FC'
       And o.tipoos = 'P'
       And p.codigoplanrev = 196
       And po.KM = pkm
       And o.codigoveic = po.codigoveic
       And o.codigoempresa = po.codigoempresa
       And o.codigofl = po.codigofl
--       And o.codintos = 1475549 -- apenas para teste       
       And o.dataaberturaos In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate);

   Cursor cObrigUtilizadas15Mil Is
    Select o.codintos, o.numeroos, o.codigoplanrev, o.codigoveic, pkm,
           r.descricaomat PecasUtilizadas, r.codigointernomaterial Codigomat, r.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           Data, Quantidade
      From (Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, o.codigoempresa, o.codigofl,
                   o.dataaberturaos Data
              From Man_OS o
                 , Man_Osprevisto p
             Where o.codintos = p.codintos
--               And o.condicaoos = 'FC'
               And o.tipoos = 'P'
               And p.codigoplanrev = 195
               And o.codintos = v_codIntOS 
               And o.codigoempresa = pEmpresa
               And o.codigoFl = pFilial
               And o.dataaberturaos In (Select Data From niff_calendario
                               Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                             From niff_calendario c
                                                            Where c.Data = trunc(Sysdate))
                                 And Data Between '01-jan-2018' And Sysdate)
               ) o 
         , (Select Sum(ie.qtdeitensmovto) - Sum(Nvl(QTDEDEVOLVIDA,0)) quantidade, cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos
              From  EST_REQUISICAO r
                   , EST_ITENSREQUISICAO B
                   , EST_MOVTO m
                   , EST_ITENSMOVTO ie
                   , EST_CADMATERIAL cm
                   , (SELECT MO.DOCUMENTO, IT.CODIGOMATINT, IT.CODIGOLOCAL, IT.CODIGOMARCAMAT, IT.QTDEITENSMOVTO QTDEDEVOLVIDA 
                        FROM EST_REQUISICAO     RE, 
                             EST_MOVTO          MO, 
                             EST_ITENSMOVTO     IT, 
                             EST_HISTORICOMOVTO HI 
                       WHERE RE.NUMERORQ     = MO.NUMERORQ     
                         AND MO.DATAMOVTO    = IT.DATAMOVTO    
                         AND MO.SEQMOVTO     = IT.SEQMOVTO     
                         AND MO.CODIGOHISMOV = HI.CODIGOHISMOV 
                         And re.codigoempresa  = pEmpresa
                         And re.codigoFl       = pFilial 
                         And re.codintos       = v_codIntOS 
                         And it.codigomatint  = v_codintMaterial
                         AND MO.DOCUMENTO    IS NOT NULL       
                         AND HI.TIPOHISMOV   = 'DS' ) DEV                   
             Where r.numerorq       = m.numerorq
               And ie.datamovto     = m.datamovto
               And ie.seqmovto      = m.seqmovto
               And cm.codigomatint  = ie.codigomatint
               And r.codigoempresa  = pEmpresa
               And r.codigoFl       = pFilial 
               And r.codintos       = v_codIntOS 
               And cm.codigomatint  = v_codintMaterial
               And r.numerorq       = b.numerorq
               And B.CODIGOMATINT   = IE.CODIGOMATINT        
               AND B.CODIGOMARCAMAT = IE.CODIGOMARCAMAT      
               AND B.CODIGOLOCAL    = IE.CODIGOLOCAL         
               AND B.CODIGOMATINT   = CM.CODIGOMATINT    
               And b.CODIGOMATINT   = DEV.CODIGOMATINT(+)   
               AND b.CODIGOLOCAL    = DEV.CODIGOLOCAL(+)    
               AND b.CODIGOMARCAMAT = DEV.CODIGOMARCAMAT(+)
               And (NVL(b.QTDEITREQ,0) - NVL(DEV.QTDEDEVOLVIDA,0)) > 0 
               And r.datarq In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate)               
             Group By cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos ) r
     Where o.codintos = r.codintos;

                                   
   Cursor cObrigUtilizadas30Mil Is
    Select o.codintos, o.numeroos, o.codigoplanrev, o.codigoveic, pkm,
           r.descricaomat PecasUtilizadas, r.codigointernomaterial Codigomat, r.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           Data, Quantidade
      From (Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, o.codigoempresa, o.codigofl,
                   o.dataaberturaos Data
              From Man_OS o
                 , Man_Osprevisto p
             Where o.codintos = p.codintos
--               And o.condicaoos = 'FC'
               And o.tipoos = 'P'
               And p.codigoplanrev = 196
               And o.codintos = v_codIntOS 
               And o.codigoempresa = pEmpresa
               And o.codigoFl = pFilial
               And o.dataaberturaos In (Select Data From niff_calendario
                               Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                             From niff_calendario c
                                                            Where c.Data = trunc(Sysdate))
                                 And Data Between '01-jan-2018' And Sysdate)
               ) o 
         , (Select Sum(ie.qtdeitensmovto) - Sum(Nvl(QTDEDEVOLVIDA,0)) quantidade, cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos
              From  EST_REQUISICAO r
                   , EST_ITENSREQUISICAO B
                   , EST_MOVTO m
                   , EST_ITENSMOVTO ie
                   , EST_CADMATERIAL cm
                   , (SELECT MO.DOCUMENTO, IT.CODIGOMATINT, IT.CODIGOLOCAL, IT.CODIGOMARCAMAT, IT.QTDEITENSMOVTO QTDEDEVOLVIDA 
                        FROM EST_REQUISICAO     RE, 
                             EST_MOVTO          MO, 
                             EST_ITENSMOVTO     IT, 
                             EST_HISTORICOMOVTO HI 
                       WHERE RE.NUMERORQ     = MO.NUMERORQ     
                         AND MO.DATAMOVTO    = IT.DATAMOVTO    
                         AND MO.SEQMOVTO     = IT.SEQMOVTO     
                         AND MO.CODIGOHISMOV = HI.CODIGOHISMOV 
                         And re.codigoempresa  = pEmpresa
                         And re.codigoFl       = pFilial 
                         And re.codintos       = v_codIntOS 
                         And it.codigomatint  = v_codintMaterial
                         AND MO.DOCUMENTO    IS NOT NULL       
                         AND HI.TIPOHISMOV   = 'DS' ) DEV                   
             Where r.numerorq       = m.numerorq
               And ie.datamovto     = m.datamovto
               And ie.seqmovto      = m.seqmovto
               And cm.codigomatint  = ie.codigomatint
               And r.codigoempresa  = pEmpresa
               And r.codigoFl       = pFilial 
               And r.codintos       = v_codIntOS 
               And cm.codigomatint  = v_codintMaterial
               And r.numerorq       = b.numerorq
               And B.CODIGOMATINT   = IE.CODIGOMATINT        
               AND B.CODIGOMARCAMAT = IE.CODIGOMARCAMAT      
               AND B.CODIGOLOCAL    = IE.CODIGOLOCAL         
               AND B.CODIGOMATINT   = CM.CODIGOMATINT    
               And b.CODIGOMATINT   = DEV.CODIGOMATINT(+)   
               AND b.CODIGOLOCAL    = DEV.CODIGOLOCAL(+)    
               AND b.CODIGOMARCAMAT = DEV.CODIGOMARCAMAT(+)
               And (NVL(b.QTDEITREQ,0) - NVL(DEV.QTDEDEVOLVIDA,0)) > 0 
               And r.datarq In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate)               
             Group By cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos ) r
     Where o.codintos = r.codintos;                                   

   Cursor cUtilizadas5Mil Is
    Select o.codintos, o.numeroos, o.codigoplanrev, o.codigoveic, pkm,
           r.descricaomat PecasUtilizadas, r.codigointernomaterial Codigomat, r.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           Data, Quantidade
      From (Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, o.codigoempresa, o.codigofl,
                   o.dataaberturaos Data
              From Man_OS o
                 , Man_Osprevisto p
             Where o.codintos = p.codintos
--               And o.condicaoos = 'FC'
               And o.tipoos = 'P'
--       And o.codintos = 1477651 -- apenas para teste               
               And p.codigoplanrev = 194
               And o.codigoempresa = pEmpresa
               And o.codigoFl = pFilial
               And o.dataaberturaos In (Select Data From niff_calendario
                               Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                             From niff_calendario c
                                                            Where c.Data = trunc(Sysdate))
                                 And Data Between '01-jan-2018' And Sysdate)
               ) o 
         , (Select Sum(ie.qtdeitensmovto) - Sum(Nvl(QTDEDEVOLVIDA,0)) quantidade, cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos
              From  EST_REQUISICAO r
                   , EST_ITENSREQUISICAO B
                   , EST_MOVTO m
                   , EST_ITENSMOVTO ie
                   , EST_CADMATERIAL cm
                   , (SELECT MO.DOCUMENTO, IT.CODIGOMATINT, IT.CODIGOLOCAL, IT.CODIGOMARCAMAT, IT.QTDEITENSMOVTO QTDEDEVOLVIDA, re.NUMERORQ 
                        FROM EST_REQUISICAO     RE, 
                             EST_MOVTO          MO, 
                             EST_ITENSMOVTO     IT, 
                             EST_HISTORICOMOVTO HI 
                       WHERE RE.NUMERORQ     = MO.NUMERORQ     
                         AND MO.DATAMOVTO    = IT.DATAMOVTO    
                         AND MO.SEQMOVTO     = IT.SEQMOVTO     
                         AND MO.CODIGOHISMOV = HI.CODIGOHISMOV 
                         And re.codigoempresa  = pEmpresa
                         And re.codigoFl       = pFilial 
                         AND MO.DOCUMENTO    IS NOT NULL       
                         AND HI.TIPOHISMOV   = 'DS' ) DEV                   
             Where r.numerorq       = m.numerorq
               And ie.datamovto     = m.datamovto
               And ie.seqmovto      = m.seqmovto
               And cm.codigomatint  = ie.codigomatint
               And r.codigoempresa  = pEmpresa
               And r.codigoFl       = pFilial 
               And r.numerorq       = b.numerorq
               And B.CODIGOMATINT   = IE.CODIGOMATINT        
               AND B.CODIGOMARCAMAT = IE.CODIGOMARCAMAT      
               AND B.CODIGOLOCAL    = IE.CODIGOLOCAL         
               AND B.CODIGOMATINT   = CM.CODIGOMATINT    
               And b.NUMERORQ       = DEV.NUMERORQ(+)
               And b.CODIGOMATINT   = DEV.CODIGOMATINT(+)   
               AND b.CODIGOLOCAL    = DEV.CODIGOLOCAL(+)    
               AND b.CODIGOMARCAMAT = DEV.CODIGOMARCAMAT(+)
               And r.codintos Is Not Null
               And (NVL(b.QTDEITREQ,0) - NVL(DEV.QTDEDEVOLVIDA,0)) > 0 
               And r.datarq In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate)               
             Group By cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos ) r
     Where o.codintos = r.codintos;


  Cursor cUtilizadas15Mil Is
    Select o.codintos, o.numeroos, o.codigoplanrev, o.codigoveic, pkm,
           r.descricaomat PecasUtilizadas, r.codigointernomaterial Codigomat, r.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           Data, Quantidade
      From (Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, o.codigoempresa, o.codigofl,
                   o.dataaberturaos Data
              From Man_OS o
                 , Man_Osprevisto p
             Where o.codintos = p.codintos
--               And o.condicaoos = 'FC'
               And o.tipoos = 'P'
               And p.codigoplanrev = 195
               And o.codigoempresa = pEmpresa
               And o.codigoFl = pFilial
--       And o.codintos = 1477651 -- apenas para teste
               And o.dataaberturaos In (Select Data From niff_calendario
                               Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                             From niff_calendario c
                                                            Where c.Data = trunc(Sysdate))
                                 And Data Between '01-jan-2018' And Sysdate)
               ) o 
         , (Select Sum(ie.qtdeitensmovto) - Sum(Nvl(QTDEDEVOLVIDA,0)) quantidade, cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos
              From  EST_REQUISICAO r
                   , EST_ITENSREQUISICAO B
                   , EST_MOVTO m
                   , EST_ITENSMOVTO ie
                   , EST_CADMATERIAL cm
                   , (SELECT MO.DOCUMENTO, IT.CODIGOMATINT, IT.CODIGOLOCAL, IT.CODIGOMARCAMAT, IT.QTDEITENSMOVTO QTDEDEVOLVIDA, re.NUMERORQ 
                        FROM EST_REQUISICAO     RE, 
                             EST_MOVTO          MO, 
                             EST_ITENSMOVTO     IT, 
                             EST_HISTORICOMOVTO HI 
                       WHERE RE.NUMERORQ     = MO.NUMERORQ     
                         AND MO.DATAMOVTO    = IT.DATAMOVTO    
                         AND MO.SEQMOVTO     = IT.SEQMOVTO     
                         AND MO.CODIGOHISMOV = HI.CODIGOHISMOV 
                         And re.codigoempresa  = pEmpresa
                         And re.codigoFl       = pFilial 
                         AND MO.DOCUMENTO    IS NOT NULL       
                         AND HI.TIPOHISMOV   = 'DS' ) DEV                   
             Where r.numerorq       = m.numerorq
               And ie.datamovto     = m.datamovto
               And ie.seqmovto      = m.seqmovto
               And cm.codigomatint  = ie.codigomatint
               And r.codigoempresa  = pEmpresa
               And r.codigoFl       = pFilial 
               And r.numerorq       = b.numerorq
               And B.CODIGOMATINT   = IE.CODIGOMATINT        
               AND B.CODIGOMARCAMAT = IE.CODIGOMARCAMAT      
               AND B.CODIGOLOCAL    = IE.CODIGOLOCAL         
               AND B.CODIGOMATINT   = CM.CODIGOMATINT   
               And r.codintos Is Not Null
               And b.NUMERORQ       = DEV.NUMERORQ(+)
               And b.CODIGOMATINT   = DEV.CODIGOMATINT(+)   
               AND b.CODIGOLOCAL    = DEV.CODIGOLOCAL(+)    
               AND b.CODIGOMARCAMAT = DEV.CODIGOMARCAMAT(+)
               And (NVL(b.QTDEITREQ,0) - NVL(DEV.QTDEDEVOLVIDA,0)) > 0 
               And r.datarq In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate)               
             Group By cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos ) r
     Where o.codintos = r.codintos;
                                   
   Cursor cUtilizadas30Mil Is
    Select o.codintos, o.numeroos, o.codigoplanrev, o.codigoveic, pkm,
           r.descricaomat PecasUtilizadas, r.codigointernomaterial Codigomat, r.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           Data, Quantidade
      From (Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, o.codigoempresa, o.codigofl,
                   o.dataaberturaos Data
              From Man_OS o
                 , Man_Osprevisto p
             Where o.codintos = p.codintos
--               And o.condicaoos = 'FC'
               And o.tipoos = 'P'
               And p.codigoplanrev = 196
--       And o.codintos = 1475549 -- apenas para teste               
               And o.codigoempresa = pEmpresa
               And o.codigoFl = pFilial
               And o.dataaberturaos In (Select Data From niff_calendario
                               Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                             From niff_calendario c
                                                            Where c.Data = trunc(Sysdate))
                                 And Data Between '01-jan-2018' And Sysdate)
               ) o 
         , (Select Sum(ie.qtdeitensmovto) - Sum(Nvl(QTDEDEVOLVIDA,0)) quantidade, cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos
              From  EST_REQUISICAO r
                   , EST_ITENSREQUISICAO B
                   , EST_MOVTO m
                   , EST_ITENSMOVTO ie
                   , EST_CADMATERIAL cm
                   , (SELECT MO.DOCUMENTO, IT.CODIGOMATINT, IT.CODIGOLOCAL, IT.CODIGOMARCAMAT, IT.QTDEITENSMOVTO QTDEDEVOLVIDA, re.NUMERORQ 
                        FROM EST_REQUISICAO     RE, 
                             EST_MOVTO          MO, 
                             EST_ITENSMOVTO     IT, 
                             EST_HISTORICOMOVTO HI 
                       WHERE RE.NUMERORQ     = MO.NUMERORQ     
                         AND MO.DATAMOVTO    = IT.DATAMOVTO    
                         AND MO.SEQMOVTO     = IT.SEQMOVTO     
                         AND MO.CODIGOHISMOV = HI.CODIGOHISMOV 
                         And re.codigoempresa  = pEmpresa
                         And re.codigoFl       = pFilial 
                         AND MO.DOCUMENTO    IS NOT NULL       
                         AND HI.TIPOHISMOV   = 'DS' ) DEV                   
             Where r.numerorq       = m.numerorq
               And ie.datamovto     = m.datamovto
               And ie.seqmovto      = m.seqmovto
               And cm.codigomatint  = ie.codigomatint
               And r.codigoempresa  = pEmpresa
               And r.codigoFl       = pFilial 
               And r.numerorq       = b.numerorq
               And B.CODIGOMATINT   = IE.CODIGOMATINT        
               AND B.CODIGOMARCAMAT = IE.CODIGOMARCAMAT      
               AND B.CODIGOLOCAL    = IE.CODIGOLOCAL         
               AND B.CODIGOMATINT   = CM.CODIGOMATINT    
               And r.codintos Is Not Null
               And b.NUMERORQ       = DEV.NUMERORQ(+)
               And b.CODIGOMATINT   = DEV.CODIGOMATINT(+)   
               AND b.CODIGOLOCAL    = DEV.CODIGOLOCAL(+)    
               AND b.CODIGOMARCAMAT = DEV.CODIGOMARCAMAT(+)
               And (NVL(b.QTDEITREQ,0) - NVL(DEV.QTDEDEVOLVIDA,0)) > 0 
               And r.datarq In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between '01-jan-2018' And Sysdate)               
             Group By cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos ) r
     Where o.codintos = r.codintos;
                                   
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
             
  Cursor cCorUtilizadas Is
    Select o.codintos, o.numeroos, o.codigoplanrev, o.codigoveic, 
           r.descricaomat PecasUtilizadas, r.codigointernomaterial Codigomat, r.codigomatint,
           Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
           o.codigoempresa, o.codigofl,
           Data, Quantidade
      From (Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.codigoveic, o.codigoempresa, o.codigofl,
                   o.dataaberturaos Data
              From Man_OS o
                 , Man_Osprevisto p
             Where o.codintos = p.codintos
--               And o.condicaoos = 'FC'
               And o.tipoos = 'C'
               And o.codigoveic = v_codVeic
               And o.codigoempresa = pEmpresa
               And o.codigoFl = pFilial
               And o.dataaberturaos In (Select Data From niff_calendario
                               Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                             From niff_calendario c
                                                            Where c.Data = trunc(Sysdate))
                                 And Data Between v_data And Sysdate)
               ) o 
         , (Select Sum(ie.qtdeitensmovto) quantidade, cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos
              From  EST_REQUISICAO r
                   , EST_ITENSREQUISICAO B
                   , EST_MOVTO m
                   , EST_ITENSMOVTO ie
                   , EST_CADMATERIAL cm
                   , (SELECT MO.DOCUMENTO, IT.CODIGOMATINT, IT.CODIGOLOCAL, IT.CODIGOMARCAMAT, IT.QTDEITENSMOVTO QTDEDEVOLVIDA 
                        FROM EST_REQUISICAO     RE, 
                             EST_MOVTO          MO, 
                             EST_ITENSMOVTO     IT, 
                             EST_HISTORICOMOVTO HI 
                       WHERE RE.NUMERORQ     = MO.NUMERORQ     
                         AND MO.DATAMOVTO    = IT.DATAMOVTO    
                         AND MO.SEQMOVTO     = IT.SEQMOVTO     
                         AND MO.CODIGOHISMOV = HI.CODIGOHISMOV 
                         AND MO.DOCUMENTO    IS NOT NULL       
                         And re.codigoempresa  = pEmpresa
                         And re.codigoFl       = pFilial 
                         And RE.codigoveic   = v_codVeic 
                         And it.codigomatint = v_codintMaterial                                        
                         AND HI.TIPOHISMOV   = 'DS' ) DEV                   
             Where r.numerorq       = m.numerorq
               And ie.datamovto     = m.datamovto
               And ie.seqmovto      = m.seqmovto
               And cm.codigomatint  = ie.codigomatint
               And r.codigoempresa  = pEmpresa
               And r.codigoFl       = pFilial 
               And cm.codigomatint  = v_codintMaterial               
               And r.codigoveic     = v_codVeic
               And r.numerorq       = b.numerorq
               And B.CODIGOMATINT   = IE.CODIGOMATINT        
               AND B.CODIGOMARCAMAT = IE.CODIGOMARCAMAT      
               AND B.CODIGOLOCAL    = IE.CODIGOLOCAL         
               AND B.CODIGOMATINT   = CM.CODIGOMATINT    
               And b.CODIGOMATINT   = DEV.CODIGOMATINT(+)   
               AND b.CODIGOLOCAL    = DEV.CODIGOLOCAL(+)    
               AND b.CODIGOMARCAMAT = DEV.CODIGOMARCAMAT(+)
               And (NVL(b.QTDEITREQ,0) - NVL(DEV.QTDEDEVOLVIDA,0)) > 0 
               And r.datarq In (Select Data From niff_calendario
                                 Where numeroquadrimestre = (Select c.numeroquadrimestre
                                                               From niff_calendario c
                                                              Where c.Data = trunc(Sysdate))
                                   And Data Between v_data And Sysdate)               
             Group By cm.descricaomat, cm.codigointernomaterial, cm.codigomatint, r.codintos ) r
     Where o.codintos = r.codintos;

                                                 
   Procedure InsereUtil (pCodIntOs Number, pData Date, pNumero Varchar2, pVeic Number, pCodIntMat Number, pMat Varchar2, pDesc Varchar2, pQtd Number) Is
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
                                      codigofl,
                                      QtdUtilizada)
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
                                    , pFilial
                                    , pQtd);  
   End;    
       
   Procedure InsereCor (pCodIntOs Number, pData Date, pNumero Varchar2, pVeic Number, pCodIntMat Number, pMat Varchar2, pDesc Varchar2, pQtd Number) Is
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
                                      codigofl,
                                      QtdUtilizada)
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
                                    , pFilial
                                    , pQtd);  
   End;    
              
Begin
  
  Delete Pbi_niff_AcompOsPrev
    Where CodigoEmpresa = pEmpresa
      And CodigoFl  = pFilial
      And km = pkm;
  
  Commit;

  If pkm = 15000 Then
    for rOb in cObrigatorio15Mil Loop
       v_codIntOS := rOb.Codintos;
       
      begin
        Select Max(p.codigoplanrev) 
          Into v_codPlano
          From Man_Os o, Man_Osprevisto p 
         Where o.codintos = p.codintos
           And o.codigoempresa = pEmpresa
           And o.codigoFl = pFilial
--           And o.condicaoos = 'FC'
           And p.codigoplanrev In (194,195,196)
           And o.tipoos = 'P'
           And o.codintos = v_codIntOS;      
        exception
        when no_data_found then
          v_codPlano := 0;
      end;  
       
      If (v_codPlano = rOb.Codigoplanrev) Then  
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
                                        codigofl,
                                        QtdObrigatoria)
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
                                      , rob.codigofl
                                      , rob.Quantidade);
         Commit;  
      End If;                                  
    End Loop;
  End If;
  
  If pkm = 30000 Then
    for rOb in cObrigatorio30Mil Loop
       v_codIntOS := rOb.Codintos;
    
      begin
        Select Max(p.codigoplanrev) 
          Into v_codPlano
          From Man_Os o, Man_Osprevisto p 
         Where o.codintos = p.codintos
           And o.codigoempresa = pEmpresa
           And o.codigoFl = pFilial
           And p.codigoplanrev In (194,195,196)
           And o.tipoos = 'P'
           And o.codintos = v_codIntOS;      
        exception
        when no_data_found then
          v_codPlano := 0;
      end;  
       
      If (v_codPlano = rOb.Codigoplanrev) Then  
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
                                        codigofl,
                                        QtdObrigatoria)
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
                                      , rob.codigofl
                                      , rob.quantidade);
        Commit;   
      End If;                                 
    End Loop;
  End If;
  
  If (pKm <> 5000) Then  
    For rGrava In cGravados Loop
       v_codIntOS := rGrava.Codintos;
       v_codintMaterial := rGrava.Codintmaterialobrig;
       
       -- Verifica se o Material obrigatorio foi utilizado, Se utilizado atualiza os dados 
       If (pKm = 15000) Then
         For rutil In cObrigUtilizadas15Mil Loop
            begin
              Select Max(p.codigoplanrev) 
                Into v_codPlano
                From Man_Os o, Man_Osprevisto p 
               Where o.codintos = p.codintos
                 And o.codigoempresa = pEmpresa
                 And o.codigoFl = pFilial
                 And p.codigoplanrev In (194,195,196)
                 And o.tipoos = 'P'
                 And o.codintos = v_codIntOS;      
              exception
              when no_data_found then
                v_codPlano := 0;
            end;  
       
           If (v_codPlano = rutil.Codigoplanrev) Then  
             Update Pbi_niff_AcompOsPrev u
                Set u.codintmaterialutil = rGrava.Codintmaterialobrig
                  , u.materialutil = rGrava.Materialobrig
                  , u.pecasutil = rGrava.Pecasobrig
                  , u.qtdutilizada = rUtil.Quantidade
              Where u.codintos = v_codIntOS
                And u.codintmaterialobrig = v_codintMaterial
                And u.km = pkm;
             Commit;                
           End If;
         End Loop;    
       End If;
       
       If (pKm = 30000) Then
         For rutil In cObrigUtilizadas30Mil Loop
            begin
              Select Max(p.codigoplanrev) 
                Into v_codPlano
                From Man_Os o, Man_Osprevisto p 
               Where o.codintos = p.codintos
                 And o.codigoempresa = pEmpresa
                 And o.codigoFl = pFilial
                 And p.codigoplanrev In (194,195,196)
                 And o.tipoos = 'P'
                 And o.codintos = v_codIntOS;      
              exception
              when no_data_found then
                v_codPlano := 0;
            end;  
       
           If (v_codPlano = rutil.Codigoplanrev) Then  
             Update Pbi_niff_AcompOsPrev u
                Set u.codintmaterialutil = rGrava.Codintmaterialobrig
                  , u.materialutil = rGrava.Materialobrig
                  , u.pecasutil = rGrava.Pecasobrig
                  , u.qtdutilizada = rUtil.Quantidade
              Where u.codintos = v_codIntOS
                And u.codintmaterialobrig = v_codintMaterial
                And u.km = pkm;
             Commit;
           End If;
         End Loop;
       End If;
    End Loop;
  End If;
  
  If (pkm = 5000) Then
    For rUtil In cUtilizadas5Mil Loop
       v_codIntOS := rUtil.Codintos;
    
      begin
        Select Max(p.codigoplanrev) 
          Into v_codPlano
          From Man_Os o, Man_Osprevisto p 
         Where o.codintos = p.codintos
           And o.codigoempresa = pEmpresa
           And o.codigoFl = pFilial
           And p.codigoplanrev In (194,195,196)
           And o.tipoos = 'P'
           And o.codintos = v_codIntOS;      
        exception
        when no_data_found then
          v_codPlano := 0;
      end;  
 
     If (v_codPlano = rutil.Codigoplanrev) Then  
        -- verifica se os Utilizados existem, se não existir inclui
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
                    , rUtil.Pecasutilizadas
                    , rUtil.Quantidade);
  
          Commit;
        End If;
      End If;
    End Loop;
  End If;

  If (pkm = 15000) Then
    For rUtil In cUtilizadas15Mil Loop
       v_codIntOS := rUtil.Codintos;
    
      begin
        Select Max(p.codigoplanrev) 
          Into v_codPlano
          From Man_Os o, Man_Osprevisto p 
         Where o.codintos = p.codintos
           And o.codigoempresa = pEmpresa
           And o.codigoFl = pFilial
           And p.codigoplanrev In (194,195,196)
           And o.tipoos = 'P'
           And o.codintos = v_codIntOS;      
        exception
        when no_data_found then
          v_codPlano := 0;
      end;  
 
     If (v_codPlano = rutil.Codigoplanrev) Then      
        -- verifica se os Utilizados existem, se não existir inclui
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
                    , rUtil.Pecasutilizadas
                    , rUtil.Quantidade);
  
          Commit;
        End If;
      End If;
    End Loop;
  End If;
  
  If (pkm = 30000) Then
    For rUtil In cUtilizadas30Mil Loop
       v_codIntOS := rUtil.Codintos;
    
      begin
        Select Max(p.codigoplanrev) 
          Into v_codPlano
          From Man_Os o, Man_Osprevisto p 
         Where o.codintos = p.codintos
           And o.codigoempresa = pEmpresa
           And o.codigoFl = pFilial
           And p.codigoplanrev In (194,195,196)
           And o.tipoos = 'P'
           And o.codintos = v_codIntOS;      
        exception
        when no_data_found then
          v_codPlano := 0;
      end;  
 
     If (v_codPlano = rutil.Codigoplanrev) Then      
        -- verifica se os Utilizados existem, se não existir inclui
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
                    , rUtil.Pecasutilizadas
                    , rUtil.Quantidade);
  
          Commit;
        End If;
      End If;
    End Loop;
  End If;
    
  -- Grava as Corretivas
  
    For rGrava In cGravadosUtilizados Loop
       v_codIntOS := rGrava.Codintos;
       v_codintMaterial := rGrava.Codintmaterialutil;
       v_codVeic := rGrava.Codigoveic;
       v_data := rGrava.Data;
       
       For rutil In cCorUtilizadas Loop
         InsereCor(rUtil.Codintos
                , rUtil.data
                , rUtil.Numeroos
                , rUtil.codigoveic
                , rUtil.Codigomatint
                , rUtil.codigomat
                , rUtil.Pecasutilizadas
                , rUtil.Quantidade);
       Commit;
       End Loop;    
       
    End Loop;  
End;
/
