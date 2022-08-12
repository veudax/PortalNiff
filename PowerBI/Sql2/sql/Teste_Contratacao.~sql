Create Table T_Cargo (Codigo Number Not Null,
                      Descricao Varchar2(50) Not Null,
                      Ativo Varchar2(1) Default 'N' Not Null,
                      PromoveACada Number, 
                      CODPROXCARGO Number,
                      AumentoSalarioDe Number);
                      
                               
Alter Table T_Cargo Add Constraint Pk_TCargo 
  primary key (Codigo) using index 
  tablespace GLOBUS_INDEX;                                  
                      
ALTER TABLE T_Cargo
       ADD  ( CONSTRAINT fk_Cargo_Cargo1
              FOREIGN KEY (CODPROXCARGO)
                             REFERENCES T_Cargo (Codigo));
                        

Create Table T_Departamento (Codigo Number Not Null,
                             Descricao Varchar2(50) Not Null,
                             Ativo Varchar2(1) Default 'N');
 
Alter Table T_Departamento Add Constraint Pk_TDepartamento 
  primary key (Codigo) using index 
  tablespace GLOBUS_INDEX;                                 
                             
Create Table T_Motivo (Codigo Number Not Null,
                             Descricao Varchar2(50) Not Null,
                             Ativo Varchar2(1) Default 'N');

Alter Table T_Motivo Add Constraint Pk_TMotivo
  primary key (Codigo) using index 
  tablespace GLOBUS_INDEX;                                 
                             
Create Table T_Funcionario (Codigo Number Not Null, 
                            Nome Varchar2(50) Not Null,
                            CodDepto Number Not Null,
                            CodCargo Number Not Null, 
                            DataAdmissao Date Not Null,
                            DataNascto Date Not Null,
                            DataDesligamento Date Not Null,
                            Salario Number);
                           
Alter Table T_Funcionario Add Constraint Pk_TFuncionario 
  primary key (Codigo) using index 
  tablespace GLOBUS_INDEX;                                 
                            
ALTER TABLE T_Funcionario
       ADD  ( CONSTRAINT fk_Cargo_Funcionario
              FOREIGN KEY (CodCargo)
                             REFERENCES T_Cargo (Codigo));
                            
ALTER TABLE T_Funcionario
       ADD  ( CONSTRAINT fk_Depto_Funcionario
              FOREIGN KEY (CodDepto)
                             REFERENCES T_Departamento (Codigo));
                                                         
Create Table T_Afastamento (CodFunc Number Not Null,
                            DataAfastado Date Not Null,
                            CodMotivo Number Not Null,
                            DataRetorno Date Null);
                      
Alter Table T_Afastamento Add Constraint Pk_TAfastamento 
  primary key (CodFunc, DataAfastado) using index 
  tablespace GLOBUS_INDEX;                                 

                      
ALTER TABLE T_Afastamento
       ADD  ( CONSTRAINT fk_Func_Afastamento
              FOREIGN KEY (CodFunc)
                             REFERENCES T_Funcionario (Codigo));
                            
ALTER TABLE T_Afastamento
       ADD  ( CONSTRAINT fk_Motivo_Afastamento
              FOREIGN KEY (CodMotivo)
                             REFERENCES T_motivo (Codigo));                                                        
                            
Create Table T_Promocao (CodFunc Number Not Null,
                         DataPromocao Date Not Null,
                         CodCargoAtu Number Not Null,
                         CodCargoAnt Number Not Null,
                         SalarioAtu Number Not Null,
                         SalarioAnt Number Not Null,
                         Status Varchar2(1) Default 'N');

Alter Table T_Promocao Add Constraint Pk_TPromocao
  primary key (CodFunc, DataPromocao) using index 
  tablespace GLOBUS_INDEX;                                 
                            
ALTER TABLE T_Promocao
       ADD  ( CONSTRAINT fk_Func_Promocao
              FOREIGN KEY (CodFunc)
                             REFERENCES T_Funcionario (Codigo));
                                                                                 
ALTER TABLE T_Promocao
       ADD  ( CONSTRAINT fk_Cargo_Promocao1
              FOREIGN KEY (CodCargoAtu) 
                             REFERENCES T_Cargo (Codigo));                                                        
                         
ALTER TABLE T_Promocao
       ADD  ( CONSTRAINT fk_Cargo_Promocao2
              FOREIGN KEY (SalarioAnt)
                             REFERENCES T_Cargo (Codigo));                             