create Table Niff_FLPRelHistFinc ( 
    Ip               Varchar2(15),
    CodEvento        Number(5),
    Usuario          Varchar2(15),
    CodigoEmpresa    Number(3),
    CodigoFl         Number(3),
    CodIntFunc       Number,
    CodFuncao        Number(4),
    Data1            Date,
    Refer1           Number,
    Valor1           Number,
    Data2            Date,
    Refer2           Number,
    Valor2           Number,    
    Data3            Date,
    Refer3           Number,
    Valor3           Number,    
    Data4            Date,
    Refer4           Number,
    Valor4           Number,    
    Data5            Date,
    Refer5           Number,
    Valor5           Number,    
    Data6            Date,
    Refer6           Number,
    Valor6           Number,    
    Data7            Date,
    Refer7           Number,
    Valor7           Number,
    Data8            Date,
    Refer8           Number,
    Valor8           Number,    
    Data9            Date,
    Refer9           Number,
    Valor9           Number,    
    Data10           Date,
    Refer10          Number,
    Valor10          Number,    
    Data11           Date,
    Refer11          Number,
    Valor11          Number,    
    Data12           Date,
    Refer12          Number,
    Valor12          Number,         
    ReferTotal       Number,
    ValorTotal       Number ) TableSpace Globus_Table;
    
    
alter table Niff_Flprelhistfinc add DataInicial Date;    
alter table Niff_Flprelhistfinc add DataFinal Date;
alter table Niff_Flprelhistfinc add TipoFolha Number(1);
alter table Niff_Flprelhistfinc add Colaborador varchar2(100);
alter table Niff_Flprelhistfinc add Empresa varchar2(100);
alter table Niff_Flprelhistfinc add Filial varchar2(100);
alter table Niff_Flprelhistfinc add Evento varchar2(100);
alter table Niff_Flprelhistfinc add Funcao varchar2(100);
Alter Table Niff_Flprelhistfinc add CodFunc Varchar2(6);
