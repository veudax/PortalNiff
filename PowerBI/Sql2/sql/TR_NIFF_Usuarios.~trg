Create Or Replace Trigger TR_NIFF_Usuarios

  After Insert Or Update On Ctr_Cadastrodeusuarios For Each Row

Declare

  V_QTDE Number  :=0 ;
  Nome Varchar(255);
  CPF Varchar(20);
  DataNascimento Date;
  idEmpresa Number;
Begin 

  If INSERTING Then 

     If Pos('2',:New.Usuario) <> Length(:New.Usuario) Then

       Select Count(*) 
         Into V_QTDE 
         From Niff_Chm_Usuarios 
        Where usuarioacesso = :New.Usuario;
    
       If (NVL(V_QTDE,0) <= 0) Then
    
        If :New.CodIntFunc = 0 Then
          Insert Into Niff_Chm_Usuarios (idusuario, 
                                         nome, 
                                         ativo, 
                                         usuarioacesso, 
                                         senha)
                               Values (SQ_NIFF_IDUsuario.Nextval,
                                       :New.NomeUsuario,
                                       'S',
                                       :New.Usuario,
                                       '123123');
        Else
          Select f.nomefunc, f.dtnasctofunc, d.nrdocto, e.Idempresa
            Into Nome, DataNascimento, CPF, idEmpresa
            From Flp_Funcionarios f,
                 Flp_Documentos d,
                 niff_chm_empresas e
           Where f.codintfunc = :New.CodIntFunc
             And d.codintfunc = f.codintfunc
             And d.tipodocto = 'CPF'
             And e.codigoglobus = lpad(f.codigoempresa, 3, '0') || '/' || lPad(f.Codigofl, 3, '0') ;
        
          Insert Into Niff_Chm_Usuarios (idusuario, 
                                         nome, 
                                         ativo, 
                                         usuarioacesso, 
                                         senha,
                                         DtNascimento,
                                         Idempresa,
                                         Cpf)
                               Values (SQ_NIFF_IDUsuario.Nextval,
                                       Nome,
                                       'S',
                                       :New.Usuario,
                                       subStr(CPF,1,6),
                                       DataNascimento,
                                       IdEmpresa,
                                       cpf);    
        End If;
      End If;
    End If;
  Else
    Update Niff_Chm_Usuarios
       Set Ativo = :New.Ativo
     Where usuarioacesso = :Old.Usuario;
  END IF;

END TR_NIFF_Usuarios;
/
