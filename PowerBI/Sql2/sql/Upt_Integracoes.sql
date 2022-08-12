Create Table Niff_Integracoes (
             Origem Varchar2(3),
             Destino Varchar2(3),
             Data Date,
             TipoOperacao Varchar2(1),
             usuario Varchar2(50),
             CodigoEmpresa Number(3),
             CodigoFl Number(3),
             CodigoAgencia Varchar2(5),
             dataPrestacaoContas Date, 
             CodigoIntegrado Number) Tablespace Globus_Table;
                     