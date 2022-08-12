Create Table Niff_PBI_TerminalLinhas (IdTerminal Number,
                                      Codigo Varchar2(5),
                                      Descricao Varchar2(25)) Tablespace Globus_table;
                                      
Alter Table Niff_PBI_TerminalLinhas Add Constraint Pk_PBI_Terminal 
  primary key (IdTerminal) using index 
  tablespace GLOBUS_INDEX; 
                                       
Alter Table Niff_PBI_TerminalLinhas Add TipoTerminal Varchar2(2);

Create Table Niff_PBI_LinhasPorServico (CodintLinha Number(5),
                                        IdTerminal Number);
                                          
Alter Table Niff_PBI_LinhasPorServico Add Constraint Pk_PBI_LinhasServico 
  primary key (CodintLinha, IdTerminal) using index 
  tablespace GLOBUS_INDEX;                                           
                            
                                           
Create Table Niff_PBI_InspetoresFiscais (CodIntFunc_Inspetor Number,
                                        CodIntFunc_Fiscal Number,
                                        Data Date) Tablespace Globus_Table;
                                        
Alter Table Niff_PBI_InspetoresFiscais Add Constraint Pk_PBIInspetFisc 
  primary key (CodIntFunc_Inspetor, CodIntFunc_Fiscal, Data) using index 
  tablespace GLOBUS_INDEX; 
                                        
Create Table Niff_PBI_CoordInspetores (CodIntFunc_Inspetor Number, 
                                      CodIntFunc_Coord Number,
                                        Data Date) Tablespace Globus_Table;
                                        
Alter Table Niff_PBI_CoordInspetores Add Constraint Pk_PBICoordInsp 
  primary key (CodIntFunc_Inspetor, CodIntFunc_Coord, Data) using index 
  tablespace GLOBUS_INDEX;                                         
  
Create Table Niff_PBI_FiscaisDoTerminal (IdTerminal Number, 
                                         CodintFunc Number,
                                         Data Date);
                                          
Alter Table Niff_PBI_FiscaisDoTerminal Add Constraint Pk_PBI_FiscaisTerm 
  primary key (CodintFunc, IdTerminal, Data) using index 
  tablespace GLOBUS_INDEX;    