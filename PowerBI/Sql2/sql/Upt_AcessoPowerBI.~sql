Create Table PBI_Emails (Id_Email Number,
                         Email Varchar2(80)) Tablespace Globus_Table;
                         
Alter Table PBI_Emails Add Constraint pk_PBI_Emails Primary Key (Id_Email) Using Index Tablespace Globus_Index;


Create Table PBI_AcessosPorEmpresa (EmpFil Varchar2(7),
                                    Id_Email Varchar2(80)) Tablespace Globus_Table;
                                     
Alter Table PBI_AcessosPorEmpresa Add Constraint pk_PBIAcessoPorEmpresa Primary Key (EmpFil, Id_Email) 
Using Index Tablespace Globus_Index;

Alter Table PBI_Emails Add NomeGrupo Varchar2(50);
Alter Table PBI_Emails Add Nome Varchar2(50);

Create Unique Index idx_PbiEmail On PBI_Emails (email) Tablespace Globus_Index;
