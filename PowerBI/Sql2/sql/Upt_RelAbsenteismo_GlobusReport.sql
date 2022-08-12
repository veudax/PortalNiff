create Table Niff_FRQ_Absenteismo ( 
    Ip               Varchar2(15),
    CodigoEmpresa    Number(3),
    CodigoFl         Number(3),
    CodIntFunc       Number,
    Data1            Date,
    Qtd1             Number,
    Data2            Date,
    Qtd2             Number,
    Data3            Date,
    Qtd3             Number,
    Data4            Date,
    Qtd4             Number,
    Data5            Date,
    Qtd5             Number,
    Data6            Date,
    Qtd6             Number,
    Data7            Date,
    Qtd7             Number,
    Data8            Date,
    Qtd8             Number,
    Data9            Date,
    Qtd9             Number,
    Data10           Date,
    Qtd10            Number,
    Data11           Date,
    Qtd11            Number,
    Data12           Date,
    Qtd12            Number,
    QtdTotal         Number ) TableSpace Globus_Table;
    
    
alter table Niff_FRQ_Absenteismo add DataInicial Date;    
alter table Niff_FRQ_Absenteismo add DataFinal Date;
alter table Niff_FRQ_Absenteismo add Colaborador varchar2(100);
alter table Niff_FRQ_Absenteismo add Empresa varchar2(100);
alter table Niff_FRQ_Absenteismo add Filial varchar2(100);
Alter Table Niff_FRQ_Absenteismo add CodFunc Varchar2(6);
