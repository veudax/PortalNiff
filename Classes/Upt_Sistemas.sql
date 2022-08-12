Create Table NIFF_CHM_Usuarios (IdUsuario Number Not Null,
                                Tipo Varchar2(1) Default 'S' Not Null, -- S Solicitante, A Atendente M Ambos
                                Nome Varchar2(50) Not Null,
                                Ativo Varchar2(1) Default 'N' Not Null,
                                Administrador Varchar2(1) Default 'N' Not Null,
                                IpMaquina Varchar2(20) Null,
                                NomeMaquina Varchar2(20) Null,
                                Telefone Number Not Null,
                                Ramal Number Not Null,
                                Email Varchar2(80) Not Null,
                                UsuarioAcesso Varchar2(30) Not Null,
                                Senha Varchar2(20) Not Null,
                                Cargo Varchar2(30) Null,
                                DtNascimento Date Null,
                                Foto Long Raw )  Tablespace Globus_Table;
                                
Alter Table NIFF_CHM_Usuarios Add Constraint Pk_NIFFUsuario 
  primary key (IdUsuario) using index 
  tablespace GLOBUS_INDEX;                                 

Create Table NIFF_CHM_Categorias (IdCategoria Number Not Null,
                                 Descricao Varchar2(50) Not Null,
                                 Ativo Varchar2(1) Not Null ) Tablespace Globus_Table;

Alter Table NIFF_CHM_Categorias Add Constraint Pk_NIFFCategoria 
  primary key (IdCategoria) using index 
  tablespace GLOBUS_INDEX;                                 
                                 
Create Table NIFF_CHM_UsuRespCateg (IdCategoria Number Not Null,
                                    IdUsuario Number Not Null) Tablespace Globus_Table;

Alter Table NIFF_CHM_UsuRespCateg Add Constraint Pk_NIFFUsuRespCateg 
  primary key (IdCategoria, IdUsuario) using index 
  tablespace GLOBUS_INDEX;                                 


Create Table NIFF_CHM_CategAutoUsuario (IdCategoria Number Not Null,
                                    IdUsuario Number Not Null) Tablespace Globus_Table;

Alter Table NIFF_CHM_CategAutoUsuario Add Constraint Pk_NIFFCategAutoUsu 
  primary key (IdCategoria, IdUsuario) using index 
  tablespace GLOBUS_INDEX;                                 

Create Table NIFF_CHM_Empresas ( IdEmpresa Number Not Null,
                                 Nome Varchar2(50) Not Null,
                                 Ativo Varchar2(1) Default 'N' Not Null,
                                 CodigoGlobus Varchar2(7) Null ) Tablespace Globus_Table;
                                 
Alter Table NIFF_CHM_Empresas Add Constraint Pk_NIFFEmpresas
  primary key (IdEmpresa) using index 
  tablespace GLOBUS_INDEX;                                 
                                 
Create Table NIFF_CHM_EmpAutoUsuario (IdEmpresa Number Not Null,
                                      IdUsuario Number Not Null) Tablespace Globus_Table;

Alter Table NIFF_CHM_EmpAutoUsuario Add Constraint Pk_NIFFEmpAutoUsu
  primary key (IdEmpresa, IdUsuario) using index 
  tablespace GLOBUS_INDEX;                                               
                                      
Create Table NIFF_CHM_Modulos (IdModulo Number Not Null,
                               IdCategoria Number Not Null,
                               Nome Varchar2(50) Not Null,
                               Fornecedor Varchar2(50) Null,
                               Ativo Varchar2(1) Default 'N' Not Null)  Tablespace Globus_Table;

Alter Table NIFF_CHM_Modulos Add Constraint Pk_NIFFModulos
  primary key (IdModulo) using index 
  tablespace GLOBUS_INDEX;                                               
                              
Create Table NIFF_CHM_ModAutoUsuario (IdModulo Number Not Null,
                                      IdUsuario Number Not Null )  Tablespace Globus_Table;

Alter Table NIFF_CHM_ModAutoUsuario Add Constraint Pk_NIFFModAutoUsu
  primary key (IdModulo, IdUsuario) using index 
  tablespace GLOBUS_INDEX;                                                  

Create Table NIFF_CHM_Telas (IdTela Number Not Null,
                             IdModulo Number Not Null,
                             Nome Varchar2(60) Not Null,
                             Caminho Varchar2(25) Not Null,
                             Ativo Varchar2(1) Default 'N' Not Null,
                             Tipo Varchar2(1) Default 'C' Not Null )  Tablespace Globus_Table;
                             
Alter Table NIFF_CHM_Telas Add Constraint Pk_NIFFTelas
  primary key (IdTela) using index 
  tablespace GLOBUS_INDEX;                                                                               
                             
Create Table NIFF_CHM_Chamado (IdChamado Number Not Null,
                               IdUsuario Number Not Null,
                               IdCategoria Number Not Null,
                               IdTela Number Null,
                               Assunto Varchar2(255) Not Null,
                               IdEmpresa Number Not Null,
                               Data Date Not Null,
                               Numero Varchar2(10) Not Null,
                               Satus Varchar2(1) Default 'N' Not Null,
                               Origem Varchar2(1) Default 'T' Not Null,
                               Prioridade Varchar2(1) Default 'M' Not Null, 
                               NumAdeqForn Varchar2(10) Null,
                               DataEntAdeq Date Null, 
                               Avaliacao Number(1) Null,
                               Descricao Long Not Null )  Tablespace Globus_Table;
                              
Alter Table NIFF_CHM_Chamado Add Constraint Pk_NIFFChamados
  primary key (IdChamado) using index 
  tablespace GLOBUS_INDEX;                                                                               
                               
Create Table NIFF_CHM_AnexosChamado (IdChamado Number Not Null,
                                     IdAnexo Number Not Null,
                                     Anexo Long Raw);

Alter Table NIFF_CHM_AnexosChamado Add Constraint Pk_NIFFAnexoChamado
  primary key (IdAnexo) using index 
  tablespace GLOBUS_INDEX;                                                                               
                                     
Create Table NIFF_CHM_HistoChamado (IdChamado Number Not Null,
                                    IdHistorico Number Not Null,
                                    IdUsuario Number Not Null,
                                    Descricao Long Not Null)  Tablespace Globus_Table;
                                   
Alter Table NIFF_CHM_HistoChamado Add Constraint Pk_NIFFHistoChamado
  primary key (IdHistorico) using index 
  tablespace GLOBUS_INDEX;                                                                               
                                    
Create Table NIFF_CHM_AnexosHistorico (IdHistorico Number Not Null,
                                       IdAnexo Number Not Null,
                                       Anexo Long Raw);
                                     
Alter Table NIFF_CHM_AnexosHistorico Add Constraint Pk_NIFFAnexoHistorico
  primary key (IdAnexo) using index 
  tablespace GLOBUS_INDEX;                                                                               
                                       
Create Table NIFF_CHM_Parametros (IdParam Number Not Null,
                                  UsuarioCategoria Varchar2(1) Default 'N' Not Null,
                                  PrazoReabertura Number(2) Default 7 Not Null,                                       
                                  ExigeAvaliacao Varchar2(1) Default 'N' Not Null,
                                  FormatoChamado Varchar2(1) Default 'M' Not Null,
                                  PodeAbrirChamado Varchar2(1) Default 'N' Not Null,
                                  UsuAutoCancelar Varchar2(1) Default 'A' Not Null )  Tablespace Globus_Table;
                                    
Alter Table NIFF_CHM_Parametros Add Constraint Pk_NIFFParametro
  primary key (IdParam) using index 
  tablespace GLOBUS_INDEX;                                                                               

Create Table NIFF_CHM_LOCALIZACAO (IdLocalizacao Number Not Null,
                                   Codigo Varchar2(5) Not Null,
                                   Descricao Varchar2(100) Not Null,
                                   IdEmpresa Number Not Null )  Tablespace Globus_Table;
                                
Alter Table NIFF_CHM_LOCALIZACAO Add Constraint Pk_NIFFLocalizacao
  primary key (IdLocalizacao) using index 
  tablespace GLOBUS_INDEX;                                                                               
                                

Create Table NIFF_CHM_SALAREUNIAO (IdSala Number Not Null,
                                   Codigo Varchar2(10) Not Null,
                                   IdLocalizacao Number Not Null,
                                   Capacidade Number Default 1 )  Tablespace Globus_Table;
                                  
Alter Table NIFF_CHM_SALAREUNIAO Add Constraint Pk_NIFFSalaReuniao
  primary key (IdSala) using index 
  tablespace GLOBUS_INDEX;                                                                               
                                  

Create Table NIFF_CHM_AGENDA (IdAgenda Number Not Null,
                              Data Date Not Null,
                              HoraInicio Date Not Null,                                  
                              HoraFim Date Not Null,                                  
                              TipoAgenda Varchar2(1) Default 'S' Not Null,
                              IdSala Number Null,
                              CodigoVeic Number, -- Vem do Globus
                              LocalVisita Varchar2(500) Null,
                              Hotel Varchar2(100) Null,
                              IdEmpresa Number Null, 
                              IdUsuario Number Not Null ) Tablespace Globus_Table;
                                                
Alter Table NIFF_CHM_AGENDA Add Constraint Pk_NIFFAgenda
  primary key (IdAgenda) using index 
  tablespace GLOBUS_INDEX;    

Create Table NIFF_CHM_AgendaUsu (IdAgenda Number Not Null,
                                 IdUsuario Number Not null);
                                 
Alter Table NIFF_CHM_AgendaUsu Add Constraint Pk_NIFFAgendaUsu
  primary key (IdAgenda, idUsuario) using index 
  tablespace GLOBUS_INDEX;    

Create Table NIFF_CHM_TIPODespesa (IdTipoDespesa Number Not Null,
                                   Codigo Varchar2(5) Not Null,
                                   Descricao Varchar2(100) Not Null,
                                   Tipo Varchar2(1) Default 'D' Not Null,
                                   ValorMaximo Number Default 0 Not Null,
                                   PedeAutorizacao Varchar2(1) Default 'N' ) Tablespace Globus_Table;
                                   
Alter Table NIFF_CHM_TIPODespesa Add Constraint Pk_NIFFTipoDespesa
  primary key (IdTipoDespesa) using Index;  

Create Table NIFF_CHM_Despesas (IdDespesas Number Not Null,
                               IdAgenda Number Not Null, 
                               DataReembolso Date,
                               ValorReembolso Number,
                               IdUsuarioAutoriz Number ) Tablespace Globus_Table;
                                  
Alter Table NIFF_CHM_Despesas Add Constraint Pk_NIFFDespesa
  primary key (IdDespesas) using index 
  tablespace GLOBUS_INDEX;    
                      
Create Table NIFF_CHM_ItensDespesas (IdItens Number Not Null,
                                    IdDespesas Number Not Null,
                                    IdTipoDespesa Number Not Null,
                                    Tipo Varchar2(1) Default 'D' Not Null,
                                    Data Date Not Null,
                                    Valor Number,
                                    Imagem Long Raw,
                                    IdUsuario Number ) Tablespace Globus_Table;
                      
Alter Table NIFF_CHM_ItensDespesas Add Constraint Pk_NIFFItensDespesa
  primary key (IdItens) using index 
  tablespace GLOBUS_INDEX;                                    
              
              
Create Table NIFF_CHM_Chat (IdChat Number Not Null,
                            IdUsuarioOrigem Number Not Null,
                            IdUsuarioDestino Number Not Null,
                            Mensagem Long,
                            Excluida Varchar2(1) Default 'N' Not Null,
                            DataExclusao Date Null ) Tablespace Globus_Table;

        
Alter Table NIFF_CHM_Chat Add Constraint Pk_NIFFChat
  primary key (IdChat) using index 
  tablespace GLOBUS_INDEX;                                    
               
         
Create Table NIFF_CHM_ChatAnexos (IdChat Number Not Null,
                                  IdAnexo Number Not Null,
                                  Anexo Long Raw ) Tablespace Globus_Table;

Alter Table NIFF_CHM_ChatAnexos Add Constraint Pk_NIFFChatAnexo
  primary key (IdAnexo) using index 
  tablespace GLOBUS_INDEX;                                    

Create Table NIFF_CHM_UsuarioLogado (IdUsuario Number Not Null,
                     /*DatHora*/     DataLogado Date Not Null,
                                     Status Varchar2(1) Default 'O') Tablespace Globus_Table; 

Alter Table NIFF_CHM_UsuarioLogado Add Constraint Pk_NIFFUsuarioLog
  primary key (IdUsuario,DataLogado) using index 
  tablespace GLOBUS_INDEX;                                    
                                                                               
-- Foreign Keys                                
ALTER TABLE NIFF_CHM_UsuRespCateg
       ADD  ( CONSTRAINT fk_Usuario_UsuRespCateg
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_Usuarios);
                                    
ALTER TABLE NIFF_CHM_UsuRespCateg
       ADD  ( CONSTRAINT fk_Categ_UsuRespCateg
              FOREIGN KEY (IdCategoria)
                             REFERENCES NIFF_CHM_Categorias);
                                    
                                          
ALTER TABLE NIFF_CHM_CategAutoUsuario
       ADD  ( CONSTRAINT fk_Usuario_CategAutoUsu
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_Usuarios);
                                    
ALTER TABLE NIFF_CHM_CategAutoUsuario
       ADD  ( CONSTRAINT fk_Categ_CategAutoUsu
              FOREIGN KEY (IdCategoria)
                             REFERENCES NIFF_CHM_Categorias);
                                    
ALTER TABLE NIFF_CHM_EmpAutoUsuario
       ADD  ( CONSTRAINT fk_Usuario_EmpAutoUsu
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_Usuarios);
                                    
ALTER TABLE NIFF_CHM_EmpAutoUsuario
       ADD  ( CONSTRAINT fk_Emresa_EmpAutoUsu
              FOREIGN KEY (IdEmpresa)
                             REFERENCES NIFF_CHM_Empresas);
                                    
ALTER TABLE NIFF_CHM_Modulos
       ADD  ( CONSTRAINT fk_Categ_Modulos
              FOREIGN KEY (IdCategoria)
                             REFERENCES NIFF_CHM_Categorias);
                                          
                                          
ALTER TABLE NIFF_CHM_ModAutoUsuario
       ADD  ( CONSTRAINT fk_Usuario_ModAUTOUsu
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_Usuarios);
                                    
ALTER TABLE NIFF_CHM_ModAutoUsuario
       ADD  ( CONSTRAINT fk_Modulo_modAutousu
              FOREIGN KEY (IdModulo)
                             REFERENCES NIFF_CHM_Modulos);

ALTER TABLE NIFF_CHM_Telas
       ADD  ( CONSTRAINT fk_Modulo_Telas
              FOREIGN KEY (IdModulo)
                             REFERENCES NIFF_CHM_Modulos);

ALTER TABLE NIFF_CHM_Chamado
       ADD  ( CONSTRAINT fk_Usuario_Chamado
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_Usuarios);
                             
ALTER TABLE NIFF_CHM_Chamado
       ADD  ( CONSTRAINT fk_Categ_Chamado
              FOREIGN KEY (IdCategoria)
                             REFERENCES NIFF_CHM_Categorias);
                             
ALTER TABLE NIFF_CHM_Chamado
       ADD  ( CONSTRAINT fk_Tela_Chamado
              FOREIGN KEY (IdTela)
                             REFERENCES NIFF_CHM_Telas);
                             
ALTER TABLE NIFF_CHM_Chamado
       ADD  ( CONSTRAINT fk_Empresa_Chamado
              FOREIGN KEY (IdEmpresa)
                             REFERENCES NIFF_CHM_Empresas);
                             
ALTER TABLE NIFF_CHM_AnexosChamado
       ADD  ( CONSTRAINT fk_Chamado_AnexoCham
              FOREIGN KEY (IdChamado)
                             REFERENCES NIFF_CHM_Chamado);
                             
ALTER TABLE NIFF_CHM_HistoChamado
       ADD  ( CONSTRAINT fk_Historico_Chamado
              FOREIGN KEY (IdChamado)
                             REFERENCES NIFF_CHM_Chamado);
                             
ALTER TABLE NIFF_CHM_HistoChamado
       ADD  ( CONSTRAINT fk_Usuario_Chamado
              FOREIGN KEY (Idusuario)
                             REFERENCES NIFF_CHM_Usuarios);

ALTER TABLE NIFF_CHM_AnexosHistorico
       ADD  ( CONSTRAINT fk_Historico_AnexoHisto
              FOREIGN KEY (IdHistorico)
                             REFERENCES NIFF_CHM_HistoChamado);

ALTER TABLE NIFF_CHM_LOCALIZACAO
       ADD  ( CONSTRAINT fk_Empresa_Localizacao
              FOREIGN KEY (IdEmpresa)
                             REFERENCES Niff_Chm_Empresas);
     
ALTER TABLE NIFF_CHM_SALAREUNIAO
       ADD  ( CONSTRAINT fk_Localizacao_SalaReuniao
              FOREIGN KEY (IdLocalizacao)
                             REFERENCES Niff_Chm_Localizacao);
     
    
ALTER TABLE NIFF_CHM_AGENDA
       ADD  ( CONSTRAINT fk_SalaReuniao_Agenda
              FOREIGN KEY (IdSala)
                             REFERENCES Niff_Chm_SalaReuniao);
    
ALTER TABLE NIFF_CHM_AGENDA
       ADD  ( CONSTRAINT fk_Empresa_Agenda
              FOREIGN KEY (IdEmpresa)
                             REFERENCES Niff_Chm_Empresas);

ALTER TABLE NIFF_CHM_AGENDA
       ADD  ( CONSTRAINT fk_Usuario_Agenda
              FOREIGN KEY (IdUsuario)
                             REFERENCES Niff_Chm_Usuarios);

ALTER TABLE NIFF_CHM_AgendaUsu
       ADD  ( CONSTRAINT fk_Agenda_AgendaUsu
              FOREIGN KEY (IdAgenda)
                             REFERENCES NIFF_CHM_AGENDA);
     
ALTER TABLE NIFF_CHM_AgendaUsu
       ADD  ( CONSTRAINT fk_Usuario_AgendaUsu
              FOREIGN KEY (IdUsuario)
                             REFERENCES Niff_Chm_Usuarios);
     
ALTER TABLE NIFF_CHM_Despesas
       ADD  ( CONSTRAINT fk_Agenda_Despesas
              FOREIGN KEY (IdAgenda)
                             REFERENCES NIFF_CHM_AGENDA);
                                                                    
ALTER TABLE NIFF_CHM_Despesas
       ADD  ( CONSTRAINT fk_Usuario_Despesas
              FOREIGN KEY (IdUsuarioAutoriz)
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));
                                                    
ALTER TABLE NIFF_CHM_ItensDespesas
       ADD  ( CONSTRAINT fk_Despesas_ItensDesp
              FOREIGN KEY (IdDespesas)
                             REFERENCES NIFF_CHM_Despesas (IdDespesas));
                                                 
ALTER TABLE NIFF_CHM_ItensDespesas
       ADD  ( CONSTRAINT fk_TpDespesas_ItensDesp
              FOREIGN KEY (IdTipoDespesa)
                             REFERENCES NIFF_CHM_TIPODespesa (IdTipoDespesa));
                                                               
ALTER TABLE NIFF_CHM_ItensDespesas
       ADD  ( CONSTRAINT fk_Usuario_ItensDesp
              FOREIGN KEY (IdUsuario)
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));
                                                                         
ALTER TABLE NIFF_CHM_Chat
       ADD  ( CONSTRAINT fk_Usuario_Chat1
              FOREIGN KEY (IdUsuarioOrigem)
                             REFERENCES NIFF_CHM_Usuarios (IdUsuario));                 

ALTER TABLE NIFF_CHM_Chat
       ADD  ( CONSTRAINT fk_Usuario_Chat2
              FOREIGN KEY (IdUsuarioDestino)
                             REFERENCES NIFF_CHM_Usuarios (IdUsuario));   
                             
ALTER TABLE NIFF_CHM_ChatAnexos
       ADD  ( CONSTRAINT fk_Chat_ChatAnexo
              FOREIGN KEY (IdChat)
                             REFERENCES NIFF_CHM_Chat (IdChat));                 
                                           
ALTER TABLE NIFF_CHM_UsuarioLogado
       ADD  ( CONSTRAINT fk_Usuario_UsuarioLog
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_Usuarios (IdUsuario));                 
                                                                                                        
-- Alterações de tabela
Alter Table Niff_Chm_Usuarios Add AcessaAgenda Varchar2(1) Default 'N' Not Null;                             
Alter Table Niff_Chm_Usuarios Add AcessaChat Varchar2(1) Default 'N' Not Null;                             
Alter Table Niff_Chm_Usuarios Add PermiteExcluirChat Varchar2(1) Default 'N' Not Null;                             
Alter Table Niff_Chm_Parametros Add HoraInicioAgenda Date;
Alter Table Niff_Chm_Parametros Add HoraFimAgenda Date;
Alter Table Niff_Chm_Parametros Add IdEmpresa Number;
                           
ALTER TABLE NIFF_CHM_Parametros
       ADD  ( CONSTRAINT fk_Empresa_Parametro
              FOREIGN KEY (IdEmpresa)
                             REFERENCES Niff_Chm_Empresas);
                              
--Alteração tabelas 14/08/2017
Alter Table NIFF_CHM_CHAMADO Add notificacaoLida Varchar2(1) Default 'N';

-- Criado tabela 15/08/2017
Create Table NIFF_CHM_LimpezaFrota (IdLimpeza Number Not Null,
                                    IdEmpresa Number Not Null,
                                    Data      Date Not Null,
                                    CodIntLinha Number Not Null,
                                    CodVeiculo  Number Not Null,
                                    IdUsuario   Number Not Null ) Tablespace Globus_table;
                                    
                             
Alter Table NIFF_CHM_LimpezaFrota Add Constraint Pk_NIFFLimpezaFrota 
  primary key (IdLimpeza) using index 
  tablespace GLOBUS_INDEX;                                 
                                        
                  
ALTER TABLE NIFF_CHM_LimpezaFrota
       ADD  ( CONSTRAINT fk_Usuario_LimpezaFrota
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_Usuarios (IdUsuario));                 
                  
ALTER TABLE NIFF_CHM_LimpezaFrota
       ADD  ( CONSTRAINT fk_Empresa_LimpezaFrota
              FOREIGN KEY (IdEmpresa)
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                 
                                        
Create Table NIFF_CHM_ItensVistoria (IdItens Number Not Null,
                                     Codigo Varchar2(5) Not Null,
                                     Descricao Varchar2(50) Not Null,
                                     Ativo Varchar2(1) Default 'S' Not Null) Tablespace Globus_table;                                   
                                     
Alter Table NIFF_CHM_ItensVistoria Add Constraint Pk_NIFFItensVistoria
  primary key (IdItens) using index 
  tablespace GLOBUS_INDEX;                                 
            
Create Table NIFF_CHM_ItensLimpeza (IdLimpeza Number Not Null,
                                    IdItens Number Not Null,
                                    Status Varchar2(1) Default 'N' Not Null) Tablespace Globus_table;
                                        
                             
Alter Table NIFF_CHM_ItensLimpeza Add Constraint Pk_NIFFItensLimpeza
  primary key (IdLimpeza,IdItens) using index 
  tablespace GLOBUS_INDEX;                                                                                              

ALTER TABLE NIFF_CHM_ItensLimpeza
       ADD  ( CONSTRAINT fk_Itens_LimpezaFrota
              FOREIGN KEY (IdLimpeza)
                             REFERENCES NIFF_CHM_LimpezaFrota (IdLimpeza));                 

ALTER TABLE NIFF_CHM_ItensLimpeza
       ADD  ( CONSTRAINT fk_ItensVist_LimpezaFrota
              FOREIGN KEY (IdItens)
                             REFERENCES NIFF_CHM_ItensVistoria (IdItens));                 

-- Alteraçao 15/08/2017
Alter Table NIFF_CHM_USUARIOS Add AcessaBI Varchar2(1) Default 'N' Not Null;  
Alter Table NIFF_CHM_ItensVistoria Add ItemCritico Varchar2(1) Default 'N' Not Null;  

-- Alteração 16/08/2017
Alter Table Niff_Chm_Chat Add Data Date Not Null;
Alter Table NIFF_CHM_SALAREUNIAO Add Descricao Varchar2(50) Not Null;
Alter Table NIFF_CHM_HistoChamado Add Data Date Not Null;
Alter Table NIFF_CHM_Empresas Add CodFilialGlobus Number;

-- Alteração 01/09/2017
Alter Table NIFF_CHM_USUARIOS Add PermiteIncExcFotosFesta Varchar2(1) Default 'N';

Create Table NIFF_CHM_Festa (IdFesta Number Not Null,
                             IdEmpresa Number Not Null,
                             IdUsuario Number Not Null,
                             Data Date Not Null,
                             modo Varchar2(1) Default 0) Tablespace Globus_Table;
                             
                            
Alter Table NIFF_CHM_Festa Add Constraint Pk_NIFFFesta
  primary key (IdFesta) using index 
  tablespace GLOBUS_INDEX;                                                                                              

ALTER TABLE NIFF_CHM_Festa
       ADD  ( CONSTRAINT fk_Festa_Empresa
              FOREIGN KEY (IdEmpresa)
                             REFERENCES NIFF_CHM_EMPRESAS (IdEmpresa));                 
                             
ALTER TABLE NIFF_CHM_Festa
       ADD  ( CONSTRAINT fk_Festa_Usuario
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_USUARIOS (IdUsuario));                                              
                             
Create Table NIFF_CHM_AnexoFesta (IdAnexo Number Not Null,
                                  IdFesta Number Not Null,
                                  Foto Long Raw) Tablespace Globus_Table; 

Alter Table NIFF_CHM_AnexoFesta Add Constraint Pk_NIFFAnexoFesta
  primary key (IdAnexo) using index 
  tablespace GLOBUS_INDEX;                                                                                              

ALTER TABLE NIFF_CHM_AnexoFesta
       ADD  ( CONSTRAINT fk_AnexoFesta_Festa
              FOREIGN KEY (IdFesta)
                             REFERENCES NIFF_CHM_Festa (IdFesta));
            
-- Alteração 04/09/2017                             
Alter Table NIFF_CHM_USUARIOS Add emailAcessoPowerBi Varchar2(100) Null;
Alter Table NIFF_CHM_Categorias Add possuiModulos Varchar2(1) Default 'S' Not Null;

-- Alteração 05/09/2017
Alter Table NIFF_CHM_USUARIOS Add Setor Varchar2(100) Null; 
CREATE SEQUENCE SQ_NIFF_IDModulo START WITH 1 NOMAXVALUE ORDER NOCACHE;
CREATE SEQUENCE SQ_NIFF_IDUsuario START WITH 1 NOMAXVALUE ORDER NOCACHE;

-- Alteraçao 08/09/207
Alter Table Niff_Chm_Chat Add Lida Varchar2(1) Default 'N' Not Null;

-- Alteração 11/09/2017
CREATE SEQUENCE SQ_NIFF_IDChat START WITH 1 NOMAXVALUE ORDER NOCACHE;
Alter Table NIFF_CHM_USUARIOS Add IdEmpresa Number; 

-- Alteração 12/09/2017 - Sistema SAC
Alter Table NIFF_CHM_USUARIOS Add AcessaSAC Varchar2(1) Default 'N' Not Null; 
Alter Table NIFF_CHM_USUARIOS Add TipoUsuarioSAC Varchar2(1) Default 'A' Not Null; 
Alter Table NIFF_CHM_USUARIOS Modify Email  Null; 
Alter Table NIFF_CHM_USUARIOS Add CodFunc Number Null; -- Codigo do funcionario na Folha
Alter Table Niff_Chm_Empresas Add TextoSacpadrao Long;

Create Table NIFF_CHM_Departamento (IdDepartamento Number Not Null,
                                    Descricao Varchar2(50) Not Null,
                                    Ativo Varchar2(1) Default 'S' Not Null) Tablespace Globus_table;

Alter Table NIFF_CHM_Departamento Add Constraint Pk_NIFFDepartamento
  primary key (IdDepartamento) using index 
  tablespace GLOBUS_INDEX;                                                                                              

Create Table NIFF_CHM_TipoAtendimento (IdTipoAtendimento Number Not Null,
                                       Descricao Varchar2(30) Not Null,
                                       Ativo Varchar2(1) Default 'S' Not Null)  Tablespace Globus_table;

Alter Table NIFF_CHM_TipoAtendimento Add Constraint Pk_NIFFTpAtendimento
  primary key (IdTipoAtendimento) using index 
  tablespace GLOBUS_INDEX;                                                                                              


Create Table NIFF_CHM_EMTUAtendimento (IdEmtu Number Not Null,
                                       Codigo Varchar2(5) Not Null,
                                       IdDepartamento  Number,  
                                       IdTpAtendimento Number, 
                                       Titulo Varchar2(30),
                                       Descricao Varchar2(255)) Tablespace Globus_Table;
                                    
Alter Table NIFF_CHM_EMTUAtendimento Add Constraint NIFF_CHM_EMTUAtendimento
  primary key (IdEmtu) using index 
  tablespace GLOBUS_INDEX;                                                                                              
                                    
ALTER TABLE NIFF_CHM_EMTUAtendimento
       ADD  ( CONSTRAINT fk_EmtuAtend_Departamento
              FOREIGN KEY (IdDepartamento)
                             REFERENCES NIFF_CHM_Departamento (IdDepartamento));
                                
ALTER TABLE NIFF_CHM_EMTUAtendimento 
       ADD  ( CONSTRAINT fk_EmtuAtend_tpAtendimento
              FOREIGN KEY (IdTpAtendimento)
                             REFERENCES NIFF_CHM_TipoAtendimento (IdTipoAtendimento));              

Create Table NIFF_CHM_Cliente (IdCliente Number Not Null,
                               Nome Varchar2(100) Not Null,
                               RG Varchar2(20) Null,
                               CPF Varchar2(20) Null,
                               Endereco Varchar2(100) Null,
                               Numero Number,
                               Cidade Varchar2(50) Null,
                               UF Varchar2(2) Null,
                               Email Varchar2(100) Null,
                               Telefone Number Null,
                               Celular Number Null ) Tablespace Globus_Table;
                               
Alter Table NIFF_CHM_Cliente Add Constraint NIFF_CHM_Cliente
  primary key (IdCliente) using index 
  tablespace GLOBUS_INDEX;                                                                                              

Create Table NIFF_CHM_Atendimento (IdAtendimento Number Not Null,
                                   Codigo Varchar2(15) Not Null,                           
                                   IdUsuario Number Not Null,
                                   IdTpAtendimento Number Not Null,
                                   IdCliente Number Not Null,
                                   ClienteAnonimo Varchar2(1) Default 'N' Not Null,
                                   TextoAtendimento Long,
                                   TextoResposta Varchar2(2000), 
                                   DataAbertura Date,
                                   DataResposta Date,
                                   DataFinalizado Date, 
                                   DataCancelado Date,
                                   Status Varchar2(1),
                                   Status2 Varchar2(2), 
                                   Retorno Varchar2(1),
                                   Retornou Varchar2(1),
                                   MotivoRetorno Varchar2(2000),
                                   MotivoCancelamento Varchar2(2000), 
                                   Satisfacao Varchar2(1),
                                   MovitoSatisfacao Varchar2(2000),
                                   Como Varchar2(1),
                                   CodigoLinha Number, -- codigo linha no globus
                                   IdEmtu Number,
                                   CodFunc Number)   Tablespace Globus_Table;
                                   
Alter Table NIFF_CHM_Atendimento Add Constraint NIFF_CHM_Atendimento
  primary key (IdAtendimento) using index 
  tablespace GLOBUS_INDEX;                                                                                              

  ALTER TABLE NIFF_CHM_Atendimento 
       ADD  ( CONSTRAINT fk_Atendimento_tpAtendimento
              FOREIGN KEY (IdTpAtendimento)
                             REFERENCES NIFF_CHM_TipoAtendimento (IdTipoAtendimento));              

ALTER TABLE NIFF_CHM_Atendimento
       ADD  ( CONSTRAINT fk_Atendimento_Usuariio
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_USUARIOS (IdUsuario));
                             
ALTER TABLE NIFF_CHM_Atendimento
       ADD  ( CONSTRAINT fk_Atendimento_Cliente
              FOREIGN KEY (IdCliente)
                             REFERENCES NIFF_CHM_Cliente (IdCliente));
                             
ALTER TABLE NIFF_CHM_Atendimento
       ADD  ( CONSTRAINT fk_Atendimento_EMTU
              FOREIGN KEY (IdEmtu)
                             REFERENCES NIFF_CHM_EMTUAtendimento (IdEmtu));
                             
                             
                                                                                       
-- Alterado 13/09/2017
Alter Table NIFF_CHM_USUARIOS Add IdDepartamento Number Null; 
                                                          
                                                          
-- Alterado 14/09/2017
Alter Table NIFF_CHM_EMTUAtendimento Add Ativo Varchar2(1) Default 'S';

Alter Table NIFF_CHM_Atendimento Add Nome Varchar2(100) Not Null;
Alter Table NIFF_CHM_Atendimento Add RG Varchar2(20) Null;
Alter Table NIFF_CHM_Atendimento Add CPF Varchar2(20) Null;
Alter Table NIFF_CHM_Atendimento Add Endereco Varchar2(100) Null;
Alter Table NIFF_CHM_Atendimento Add Numero Number;
Alter Table NIFF_CHM_Atendimento Add Cidade Varchar2(50) Null;
Alter Table NIFF_CHM_Atendimento Add UF Varchar2(2) Null;
Alter Table NIFF_CHM_Atendimento Add Email Varchar2(100) Null;
Alter Table NIFF_CHM_Atendimento Add Telefone Number Null; 
Alter Table NIFF_CHM_Atendimento Add Celular Number Null;
Alter Table NIFF_CHM_Atendimento Add IdUsuarioResponsavel Number Null;
Alter Table NIFF_CHM_Atendimento Add IdUsuarioFinaliza Number Null;
Alter Table NIFF_CHM_Atendimento Add IdUsuarioCancela Number Null;
Alter Table NIFF_CHM_Atendimento Add IdUsuarioRetorno Number Null;
Alter Table NIFF_CHM_Atendimento Add Origem Varchar2(1) Default 'T' Null;
Alter Table NIFF_CHM_Atendimento Add Situacao Varchar2(2)  Null;
Alter Table NIFF_CHM_Atendimento Drop Column Como;
Alter Table NIFF_CHM_Atendimento Drop Column Status2;

-- alterado 18/09/2017 
Alter Table NIFF_CHM_Empresas Add FormatoCodigoSAC Varchar2(2) ;
Alter Table NIFF_CHM_Empresas Add Separador Varchar2(1) Default '-';
Alter Table Niff_Chm_Usuarios Add CPF Number;

-- alterado 20/09/2017
Alter Table Niff_Chm_Parametros Add TempoMaximoRetornoSAC Number Default 180;
Alter Table Niff_Chm_Parametros Add TempoMaximoRetornoChamado Number Default 180;

-- alterado 21/09/2017
Alter Table Niff_Chm_Usuarios Modify foto Blob;

-- Alterado 22/09/2017
Create Table Niff_CHM_ConfUsuario (IdUsuario Number,
                                   Skin Varchar2(3)) Tablespace Globus_table;
                                   
                                
Alter Table Niff_CHM_ConfUsuario Add Constraint Pk_NIFFConfUsuario
  primary key (IdUsuario) using index 
  tablespace GLOBUS_INDEX;        
  

ALTER TABLE Niff_CHM_ConfUsuario
       ADD  ( CONSTRAINT fk_ConfUsuario_Usuariio
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_USUARIOS (IdUsuario));
          

Alter Table Niff_CHM_ConfUsuario Add NomeMaquina Varchar2(20);

-- Alterado 26/09/2017
Alter Table niff_chm_empresas Add NomeAbreviado Varchar2(20);
Alter Table niff_chm_empresas Add email Varchar2(60);
Alter Table niff_chm_empresas Add Smtp Varchar2(60);
Alter Table niff_chm_empresas Add Autentica Varchar2(1) Default 'S';
Alter Table niff_chm_empresas Add AutenticaSSL Varchar2(1) Default 'N';
Alter Table niff_chm_empresas Add PortaSMTP Number Default 25;
Alter Table niff_chm_empresas Add Senha Varchar2(20);
Alter Table niff_chm_empresas Add telefoneSAC Varchar2(15);
Alter Table Niff_Chm_Usuarios Add EmailDepto Varchar2(60);

-- Alterado 27/09/2017
Create Table NIFF_CHM_LogSAC (IdLog Number Not Null,
                           IdAtendimento Number Not Null,
                           Descricao Long Not Null,
                           Data Date Not Null,
                           IdUsuario Number Not Null)  Tablespace Globus_Table;
                           
Alter Table NIFF_CHM_LogSAC Add Constraint NIFF_CHM_LOGSac
  primary key (IdLog) using index 
  tablespace GLOBUS_INDEX;                                                                                              
                                    
ALTER TABLE NIFF_CHM_LogSAC
       ADD  ( CONSTRAINT fk_LogSac_Usuariio
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_USUARIOS (IdUsuario));
                             
CREATE SEQUENCE SQ_NIFF_IdLogSac START WITH 1 NOMAXVALUE ORDER NOCACHE;                             

-- Alterado 28/09/2017
Alter Table NIFF_CHM_Atendimento Add RespostaAoCliente Varchar2(2000) Null;
Alter Table NIFF_CHM_Atendimento Add DataRetornoAoCliente Date Null;
Alter Table NIFF_CHM_Atendimento Add IdUsuarioRetornouAoCliente Number Null;

-- Alterado 09/10/2017
Alter Table Niff_Chm_Usuarios Add AcessaDescontoBeneficio Varchar2(1) Default 'N';
Alter Table Niff_Chm_Empresas Add ValorDescFaltaJustificada Number;
Alter Table Niff_Chm_Empresas Add ValorDescFaltaInjustificada Number;

-- Alterado 10/10/2017 
Alter Table Niff_Chm_Empresas Add QtdfaltasJustificadasSuperior Number;
Alter Table Niff_Chm_Empresas Add QtdfaltasInjustifSuperior Number;

-- Alterado
Create Table Niff_Chm_log (IdLog Number Not Null,
                           Data Date Not Null, --data e hora da gravação
                           Descricao Long Not Null,
                           Tela Varchar2(100) Null,
                           IdUsuario Number Not Null ) Tablespace Globus_table;
                           
       
Alter Table Niff_Chm_log Add Constraint NIFF_CHM_LOG
  primary key (IdLog) using index 
  tablespace GLOBUS_INDEX;                                                                                              
                                    
ALTER TABLE Niff_Chm_log
       ADD  ( CONSTRAINT fk_Log_Usuariio
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_USUARIOS (IdUsuario));                            
                             
CREATE SEQUENCE SQ_NIFF_Log START WITH 1 NOMAXVALUE ORDER NOCACHE;
                             
-- alterado 23/10/17
Alter Table NIFF_CHM_Atendimento Add AguardaSatisfacaoCliente Varchar2(1) Default 'N' Not Null;                             
Alter Table NIFF_CHM_Empresas Add ExibirOsCanceladosNoSACDias Number Default 4 Not Null;
Alter Table NIFF_CHM_Empresas Add ResponderEmDiasSAC Number Default 1 Not Null;
Alter Table NIFF_CHM_Empresas Add SemretornoEmDiasSAC Number Default 4 Not Null;

-- alterado 24/10/17
Alter Table NIFF_CHM_Atendimento Add DataSatisfacao Date Null;                             

-- alterado 30/10/2017
Alter Table Niff_chm_agenda Add Texto Varchar2(500) ;
Alter Table Niff_chm_agenda Add DiaTodo Varchar2(1) Default 'N' Not Null;
Alter Table Niff_chm_agenda Add Status Varchar2(1) Default 'A' Not Null; -- Ativo, Cancelada
Alter Table Niff_chm_agenda Add Lembrar Number Default 15; -- minutos para lembrar da agenda.

Alter Table Niff_Chm_Salareuniao Add IdEmpresa Number;
Alter Table Niff_Chm_Salareuniao Add Ativa Varchar2(1) Default 'S' Not Null;
Alter Table Niff_Chm_Salareuniao Drop Column IdLocalizaCAO;
Alter Table Niff_Chm_Salareuniao Drop Column Codigo;

ALTER TABLE NIFF_CHM_SALAREUNIAO
       ADD  ( CONSTRAINT fk_Empresa_SalaReuniao
              FOREIGN KEY (IdEmpresa) 
                             REFERENCES Niff_Chm_Empresas);
     
Drop Table Niff_Chm_Localizacao;


-- alterado 31/10/2017
Alter Table Niff_chm_agenda Add DataFim Date Not Null ;
Alter Table Niff_chm_agenda Drop Column Hotel;

--alterado 01/11/2017
CREATE SEQUENCE SQ_NIFF_IDAgenda START WITH 1 NOMAXVALUE ORDER NOCACHE;
Alter Table Niff_chm_agenda Drop Column HORAINICIO;
Alter Table Niff_chm_agenda Drop Column HORAFIM;

-- alterado 08/11/2017
Create Table Niff_CHM_Aniversario (IdAniversario Number Not Null,
                                   IdUsuario Number Not Null,
                                   Data Date Not Null,
                                   MostrarMensagem Varchar2(1) Default 'S' Not Null,  
                                   Mensagem Varchar2(1000) Null) Tablespace Globus_table;
                      

Alter Table Niff_CHM_Aniversario Add Constraint NIFF_CHM_Aniversario
  primary key (IdAniversario) using index 
  tablespace GLOBUS_INDEX;                                            

ALTER TABLE Niff_CHM_Aniversario
       ADD  ( CONSTRAINT fk_Usuario_Aniversario
              FOREIGN KEY (IdUsuario) 
                             REFERENCES Niff_Chm_Usuarios);
                                        
-- Alterado 13/11/2017
Alter Table Niff_Chm_AgendaUsu Add ConviteAceito Varchar2(1) Default 'N' Not Null ;                                        

-- Alterado 17/11/2017
Alter Table niff_chm_usuarios Modify Telefone Null;
Alter Table niff_chm_usuarios Modify Ramal Null;
Alter Table Niff_Chm_Chamado Modify Numero Varchar2(11);

Alter Table Niff_Chm_Chamado Drop Column SATUS;
Alter Table Niff_Chm_Chamado Add Status Varchar2(1) Default 'N';
Alter Table Niff_Chm_Chamado Add DescricaoAvaliacao Varchar2(2000) Null;
Alter Table Niff_Chm_Chamado Add DataAvaliacao Date;
Alter Table Niff_Chm_Chamado Add TipoChamado Varchar2(1) Default 'E' Not Null;

CREATE SEQUENCE SQ_NIFF_IDChamado START WITH 1 NOMAXVALUE ORDER NOCACHE;

-- Alterado 21/11/2017
Alter Table niff_chm_parametros Drop Column TEMPOMAXIMORETORNOSAC; 
Alter Table niff_chm_parametros Drop Column TEMPOMAXIMORETORNOCHAMADO;
Alter Table niff_chm_parametros Drop Column IDEMPRESA;
Alter Table niff_chm_parametros Drop Column USUARIOCATEGORIA;
Alter Table niff_chm_parametros Drop Column PODEABRIRCHAMADO;

Alter Table niff_chm_parametros Add QtdDiasRetornoChamado Number Default 3;
Alter Table niff_chm_parametros Add ExibirCancelados Number Default 3;
Alter Table niff_chm_parametros Add EmailChamado Varchar2(60);
Alter Table niff_chm_parametros Add SMTP Varchar2(60);
Alter Table niff_chm_parametros Add Autentica Varchar2(1) Default 'S';
Alter Table niff_chm_parametros Add AutenticaSMTP Varchar2(1) Default 'N';
Alter Table niff_chm_parametros Add Porta Number Default 25;
Alter Table niff_chm_parametros Add SenhaEmail Varchar2(20);
Alter Table niff_chm_parametros Add UsuarioMesmoDepto Varchar2(1) Default 'N';
Alter Table niff_chm_parametros Add AtendenteConcluiChamado Varchar2(1) Default 'S';
Alter Table niff_chm_parametros Add AtendentePodeAbrirChamado Varchar2(1) Default 'S';
Alter Table niff_chm_parametros Add Separador Varchar2(1) Default '-';
Alter Table niff_chm_parametros Modify FORMATOCHAMADO Varchar2(2);

CREATE SEQUENCE SQ_NIFF_IdParametro START WITH 1 NOMAXVALUE ORDER NOCACHE;

-- Alterado 23/11/2017
CREATE SEQUENCE SQ_NIFF_IDHistorico START WITH 1 NOMAXVALUE ORDER NOCACHE;
Alter Table Niff_Chm_Chamado Drop Column DESCRICAO;
Alter Table Niff_Chm_Histochamado Add Status Varchar2(1) Default 'N';
Alter Table Niff_Chm_Histochamado Modify Descricao Varchar2(2000);
Drop Table NIFF_CHM_AnexosChamado;

-- Alterado 24/11/2017
CREATE SEQUENCE SQ_NIFF_IDAnexo START WITH 1 NOMAXVALUE ORDER NOCACHE;
Alter Table Niff_Chm_Anexoshistorico Modify Anexo Blob;
Alter Table Niff_Chm_Anexofesta Modify Foto Blob;
Alter Table niff_chm_chatanexos Modify Anexo Blob;

Alter Table Niff_Chm_Anexoshistorico Add NomeArquivo Varchar2(30);
Alter Table niff_chm_chatanexos  Add NomeArquivo Varchar2(30);
Alter Table Niff_Chm_Anexofesta  Add NomeArquivo Varchar2(30);

--Alterado 28/11/2017
CREATE SEQUENCE SQ_NIFF_IdAniversario START WITH 1 NOMAXVALUE ORDER NOCACHE;

--Alterado 06/12/2017
Alter Table niff_chm_usuarios Add AcessaJuridico Varchar2(1) Default 'N';


-- alterado 11/12/2017 -- Juridico
Create Table Niff_Jur_Vara (IdVara Number Not Null,
                            Nome Varchar2(50) Not Null,
                            Ativa Varchar2(1) Default 'S' Not Null) Tablespace Globus_Table;
                            

Alter Table Niff_Jur_Vara Add Constraint pkNiff_Jur_Vara
  primary key (IdVara) using index 
  tablespace GLOBUS_INDEX;    
  
Create Table Niff_Jur_Tipo (IdTipo Number Not Null,
                            Descricao Varchar2(50) Not Null,
                            Ativo Varchar(1) Default 'S' Not Null) Tablespace Globus_Table;

Alter Table Niff_Jur_Tipo Add Constraint pkNiff_Jur_Tipo
  primary key (IdTipo) using index 
  tablespace GLOBUS_INDEX;   
  
CREATE SEQUENCE SQ_NIFF_JurIdVara START WITH 1 NOMAXVALUE ORDER NOCACHE;
  
Create Table Niff_Jur_Comunicados (IdComunicado Number Not Null, 
                                   DataAbertura Date Not Null,
                                   DataConfirmacao Date Null,
                                   Status Varchar2(1) Default 'N', --Novo, Reprovado, Aprovado, Finalizado
                                   Socilitante Varchar2(60) Null,
                                   IdEmpresa Number Not Null,
                                   Processo Varchar2(50) Not Null,
                                   IdVara Number Not Null,
                                   Autor Varchar2(50) Null,
                                   TipoAutor Varchar2(1) Null, -- Pessoa Fisica, Pessoa Jurídica
                                   CPFAutor Number(20) Null,
                                   PISAutor Varchar2(50) Null,
                                   IdTipo Number Not Null,
                                   MotivoOutros Varchar2(255) Null,
                                   Reembolso Varchar2(1) Default 'N',
                                   ValorReembolso Number, 
                                   Seguro Varchar2(1) Default 'N',
                                   ValorTotal Number,
                                   QtdParcela Number,
                                   NotaFiscal Varchar2(1) Default 'N',
                                   ValorNotaFiscal Number,
                                   Observacao Long, 
                                   Favorecido Varchar2(100),
                                   CPFFavorecido Number(20),
                                   Agencia Varchar2(20),
                                   Conta Varchar2(20),
                                   Referencia Number(6),
                                   Resumo Varchar2(2000),
                                   NovoProcesso Varchar2(50),
                                   IdUsuario Number,
                                   EmailEnviado Varchar2(1) Default 'N', -- Sim, Nao
                                   CentroCusto Number ) Tablespace Globus_Table;

Alter Table Niff_Jur_Comunicados Add Constraint pkNiff_Jur_Comunicados
  primary key (IdComunicado) using index 
  tablespace GLOBUS_INDEX;                                            

ALTER TABLE Niff_Jur_Comunicados
       ADD  ( CONSTRAINT fk_Usuario_JurComunicado
              FOREIGN KEY (IdUsuario) 
                             REFERENCES Niff_Chm_Usuarios);                                   
                                   
ALTER TABLE Niff_Jur_Comunicados
       ADD  ( CONSTRAINT fk_Empresa_JurComunicado
              FOREIGN KEY (IdEmpresa) 
                             REFERENCES Niff_Chm_Empresas);                                   
                                   
ALTER TABLE Niff_Jur_Comunicados
       ADD  ( CONSTRAINT fk_JurVara_JurComunicado
              FOREIGN KEY (IdVara) 
                             REFERENCES Niff_Jur_Vara);                                   
                                   
ALTER TABLE Niff_Jur_Comunicados
       ADD  ( CONSTRAINT fk_JurTipo_JurComunicado
              FOREIGN KEY (IdTipo) 
                             REFERENCES Niff_Jur_Tipo);                                   
                                   
                            
Create Table Niff_Jur_Parcela (IdParcela Number Not Null,                             
                               IdComunicado Number Not Null,
                               Parcela Number Not Null,
                               Valor Number Not Null,
                               Data Date Not Null) Tablespace Globus_Table;
                               
Alter Table Niff_Jur_Parcela Add Constraint pkNiff_Jur_Parcela
  primary key (IdParcela) using index 
  tablespace GLOBUS_INDEX;                                            

ALTER TABLE Niff_Jur_Parcela
       ADD  ( CONSTRAINT fk_JurComunicado_JurParcela
              FOREIGN KEY (IdComunicado) 
                             REFERENCES Niff_Jur_Comunicados);                                   
                             
Create Table Niff_Jur_Email (idEmail Number Not Null,
                             IdEmpresa Number Not Null,
                             Email Varchar2(80) Not Null, 
                             Ativo Varchar2(1) Default 'S' Not Null) Tablespace Globus_Table;                             
                                                       
Alter Table Niff_Jur_Email Add Constraint pkNiff_Jur_Email
  primary key (IdEmail) using index 
  tablespace GLOBUS_INDEX;                                            

ALTER TABLE Niff_Jur_Email
       ADD  ( CONSTRAINT fk_Empresa_JurEmail
              FOREIGN KEY (IdEmpresa) 
                             REFERENCES Niff_Chm_Empresas);                              
                                   
                                    
Alter Table Niff_Jur_Vara Modify nome Varchar2(80);
Alter Table Niff_Jur_Comunicados Add Vara Varchar2(100);                                   
Alter Table Niff_Jur_Comunicados Add Banco Varchar2(100);                                   
Alter Table Niff_Jur_Comunicados Add Tipo Varchar2(100);                                                                      
Alter Table Niff_Jur_Comunicados Add Tipo Varchar2(100);  
Alter Table Niff_Jur_Comunicados Add parcelas Varchar2(2000); 
Alter Table Niff_Jur_Comunicados Modify IdVara Null;
Alter Table Niff_Jur_Comunicados Modify IdTipo Null;
Alter Table Niff_Jur_Comunicados Modify Conta Varchar2(50);
Alter Table Niff_Jur_Comunicados Modify Agencia Varchar2(50);
Alter Table Niff_Chm_Empresas Modify NOMEABREVIADO Varchar2(40);

-- alterado 12/12/2017
CREATE SEQUENCE SQ_NIFF_JurIdEmail START WITH 1 NOMAXVALUE ORDER NOCACHE;

Alter Table Niff_Jur_Email Add TipoEmail Varchar2(1) Default 'J' Not Null; -- Juridico, Financeiro, Diretoria

-- alterado 13/12/2017
Alter Table Niff_Jur_Comunicados Add IdUsuarioAprovacao Number;
Alter Table Niff_Jur_Comunicados Add DataReprovacao Date;
Alter Table Niff_Jur_Comunicados Add IdUsuarioAlteracao Number;
Alter Table Niff_Jur_Comunicados Add DataAlteracao Date;

-- Alterado 18/12/2017
Alter Table Niff_Jur_Comunicados Add IdUsuarioReprovador Number;
Alter Table Niff_Jur_Comunicados Add IdUsuarioCancela Number;
Alter Table Niff_Jur_Comunicados Add TipoFavorecido Varchar2(1);
Alter Table Niff_Jur_Comunicados Add IdUsuarioAltera Number;
Alter Table Niff_Jur_Comunicados Add IdUsuarioFinaliza Number;
Alter Table Niff_Jur_Comunicados Add DataAlteracao Date;
Alter Table Niff_Jur_Comunicados Add DataFinalizado Date;

Create Index idx_ComunicadoAber On niff_jur_comunicados (Dataabertura);

-- Alterado 22/12/2017
Alter Table Niff_Jur_Parcela Add TipoVencimento Varchar2(1) Default 'I';
CREATE SEQUENCE SQ_NIFF_JurIdComun START WITH 12073 NOMAXVALUE ORDER NOCACHE;
CREATE SEQUENCE SQ_NIFF_ParIdn START WITH 11605 NOMAXVALUE ORDER NOCACHE;
Alter Table Niff_Jur_Comunicados Add DataCancela Date;

-- Alterado 26/12/2017
Alter Table Niff_Jur_Comunicados Modify EmailEnviado Varchar2(2);

-- Alterado 02/01/2018
Alter Table niff_chm_telas Modify Caminho Varchar2(300);

-- Alterado 03/01/2018
Create Table Niff_Jur_NotifComunicado (IdNotificacao Number, 
                                       IdComunicado Number,
                                       IdUsuario Number,
                                       Status Varchar2(1)) Tablespace Globus_Table ;
                                       
Alter Table Niff_Jur_NotifComunicado Add Constraint pkNiff_Jur_NotifComunicado
  primary key (IdNotificacao) using index 
  tablespace GLOBUS_INDEX;                                            

ALTER TABLE Niff_Jur_NotifComunicado
       ADD  ( CONSTRAINT fk_Comunicado_NotifComunic
              FOREIGN KEY (IdComunicado) 
                             REFERENCES Niff_Jur_Comunicados);                              
                                         
ALTER TABLE Niff_Jur_NotifComunicado
       ADD  ( CONSTRAINT fk_Usuario_NotifComunic
              FOREIGN KEY (IdUsuario) 
                             REFERENCES Niff_Chm_Usuarios);                                                                       
                             
CREATE SEQUENCE SQ_NIFF_JurIdNotif START WITH 1 NOMAXVALUE ORDER NOCACHE;
                             
-- Alterado 09/01/2018
Alter Table Niff_Chm_Usuarios Add IdCargo Number;                             
Alter Table Niff_Chm_Usuarios Add AcessaCadastroJuridico Varchar2(1) Default 'N';
Alter Table Niff_Chm_Usuarios Add PermiteAprovarComunicado Varchar2(1) Default 'N';
Alter Table Niff_Chm_Usuarios Add PermiteReprovarComunicado Varchar2(1) Default 'N';
Alter Table Niff_Chm_Usuarios Add PermiteCancelarComunicado Varchar2(1) Default 'N';

-- Alterado 11/01/2018
Create Table Niff_CHM_NotifChamado (IdNotificacao Number, 
                                    IdChamado Number,
                                    IdUsuario Number,
                                    Status Varchar2(1)) Tablespace Globus_Table ;
                                       
Alter Table Niff_CHM_NotifChamado Add Constraint pkNiff_CHM_NotifChamado
  primary key (IdNotificacao) using index 
  tablespace GLOBUS_INDEX;                                            

ALTER TABLE Niff_CHM_NotifChamado
       ADD  ( CONSTRAINT fk_Chamado_NotifChamado
              FOREIGN KEY (IdChamado) 
                             REFERENCES Niff_Chm_Chamado (IdChamado));                              
                                         
ALTER TABLE Niff_CHM_NotifChamado
       ADD  ( CONSTRAINT fk_Usuario_NotifChamado
              FOREIGN KEY (IdUsuario)  
                             REFERENCES Niff_Chm_Usuarios);                                                                       
                             
CREATE SEQUENCE SQ_NIFF_NotifCham START WITH 1 NOMAXVALUE ORDER NOCACHE;

Alter Table Niff_Chm_Usuarios Add PermiteAlterarComunicado Varchar2(1) Default 'N';
Alter Table Niff_Chm_Usuarios Add PermiteFinalizarComunicado Varchar2(1) Default 'N';

Alter Table Niff_Jur_Comunicados Add MotivoCancelamento Varchar2(2000) Null;

-- Alterado 22/01/2018
Alter Table Niff_Chm_Chamado Add ProblemaResolvido Varchar2(1) Default 'N';
Alter Table Niff_Chm_Chamado Add DentroDoPrazo Varchar2(1) Default 'N';
Alter Table Niff_Chm_Chamado Add IdChamadoAgrupado Number;
Alter Table Niff_Chm_Chamado Add IdUsuarioAgrupou Number;
Alter Table Niff_Chm_Chamado Add DataAgrupou Date;
Alter Table Niff_Chm_Usuarios Add AcessaDashBoardChamados Varchar2(1) Default 'N';
Alter Table Niff_Chm_Parametros Add MesesConsultaDashBoardChamados Number Default 3;


-- Alterado 23/01/2018 -- Tabelas para o modulo de avaliação de desempenho

Alter Table Niff_Chm_Usuarios Add AcessaAvaliacaoDesempenho Varchar2(1) Default 'N';
Alter Table Niff_Chm_Usuarios Add AcessoDeRH Varchar2(1) Default 'N';
Alter Table Niff_Chm_Usuarios Add AcessoDeGestor Varchar2(1) Default 'N';
Alter Table Niff_Chm_Usuarios Add AcessoDeColaborador Varchar2(1) Default 'N';

Create Table Niff_ADS_Escolaridade (IdEscola Number Not Null,
                                    Descricao Varchar2(100) Not Null,
                                    Ativa Varchar2(1) Default 'S' Not Null) Tablespace Globus_Table;
                            
Alter Table Niff_ADS_Escolaridade Add Constraint pkNiff_ADS_Escolaridade
  primary key (IdEscola) using index 
  tablespace GLOBUS_INDEX;    
  
Create Table Niff_ADS_Prazos (IdPrazos Number Not Null,
                              Referencia Number Not Null,
                              Tipo Varchar2(2) Not Null,
                              Ativo Varchar2(1) Default 'N' Not Null,
                              Inicio Date Not Null,
                              Fim Date Not Null ) Tablespace Globus_Table;
   
Alter Table Niff_ADS_Prazos Add Constraint pkNiff_ADS_Prazos
  primary key (IdPrazos) using index 
  tablespace GLOBUS_INDEX;     
   
CREATE SEQUENCE SQ_NIFF_ADSIdPrazos START WITH 1 NOMAXVALUE ORDER NOCACHE;

-- Alterado 24/01/2018
Create Table Niff_ADS_Competencias (IdCompetencia Number Not Null,
                                    Descricao Varchar2(100) Not Null,
                                    Tipo Varchar2(2) Not Null,
                                    Ativo Varchar2(1) Default 'N' Not Null) Tablespace Globus_Table;
                                    
        
Alter Table Niff_ADS_Competencias Add Constraint pkNiff_ADS_Competencias
  primary key (IdCompetencia) using index 
  tablespace GLOBUS_INDEX;                                        
  
-- Alterado 25/01/2018  

Create Table NIFF_ADS_SubCompetencias (IdSubComp Number Not Null,
                                       IdCompetencia Number Not Null,
                                       Descricao Varchar2(500) Not Null,
                                       Pontuacao Number Default 1,
                                       Ativo Varchar2(1) Default 'N' Not Null) Tablespace Globus_Table;
                                       
Alter Table NIFF_ADS_SubCompetencias Add Constraint pkNIFF_ADS_SubCompetencias
  primary key (IdSubComp) using index 
  tablespace GLOBUS_INDEX;              
                        
ALTER TABLE NIFF_ADS_SubCompetencias
       ADD  ( CONSTRAINT fk_Compet_SubCompet
              FOREIGN KEY (IdCompetencia)  
                             REFERENCES Niff_ADS_Competencias (IdCompetencia));                                                                       
                                                                     
CREATE SEQUENCE SQ_NIFF_ADSIdSubComp START WITH 1 NOMAXVALUE ORDER NOCACHE;
                                                                     
Create Table NIFF_CHM_Corridas (IdCorrida Number Not Null,
                                Data Date Not Null,
                                Nome Varchar2(100) Not Null,
                                Local Varchar2(500) Not Null,
                                LinkWeb Varchar2(1000) Not Null,
                                Ativo Varchar2(1) Default 'N' Not Null,
                                Valor Number Not Null) Tablespace Globus_Table;
                                
Alter Table NIFF_CHM_Corridas Add Constraint pkNIFF_CHM_Corridas
  primary key (IdCorrida) using index 
  tablespace GLOBUS_INDEX;                                        
                             
Create Table NIFF_CHM_Distancias (IdDistancia Number Not Null,
                                  IdCorrida Number Not Null,
                                  Km Number Not Null )  Tablespace Globus_Table;
                                                                          
Alter Table NIFF_CHM_Distancias Add Constraint pkNIFF_CHM_Distancias
  primary key (IdDistancia) using index 
  tablespace GLOBUS_INDEX;                                        
            
ALTER TABLE NIFF_CHM_Distancias
       ADD  ( CONSTRAINT fk_Corrida_Distancia
              FOREIGN KEY (IdCorrida)  
                             REFERENCES NIFF_CHM_Corridas (IdCorrida));  
                             
Create Table NIFF_CHM_Participantes (IdParticipante Number Not Null,
                                     IdDistancia Number Not Null,
                                     IdUsuario Number Null,
                                     Nome Varchar2(100) Not Null,
                                     InscricaoPaga Varchar2(1) Default 'N' Not Null,
                                     TempoBruto Number,
                                     TempoLiquido Number,
                                     Ritmo Number,
                                     ClassificacaoGeral Number,
                                     Classificacao Number) Tablespace Globus_Table;
                                     
Alter Table NIFF_CHM_Participantes Add Constraint pkNIFF_CHM_Participantes
  primary key (IdParticipante) using index 
  tablespace GLOBUS_INDEX;                                        
         
ALTER TABLE NIFF_CHM_Participantes
       ADD  ( CONSTRAINT fk_Distancia_Partic
              FOREIGN KEY (IdDistancia)  
                             REFERENCES NIFF_CHM_Distancias (IdDistancia));   

ALTER TABLE NIFF_CHM_Participantes
       ADD  ( CONSTRAINT fk_Usuario_Partic
              FOREIGN KEY (IdUsuario)  
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                               
                             
-- Alterado 26/01/2018                             
Alter Table NIFF_CHM_Corridas Add PrazoInscricao Date Not Null;
CREATE SEQUENCE SQ_NIFF_CHMIdDistancia START WITH 1 NOMAXVALUE ORDER NOCACHE;


-- Alterado 29/01/2018
Alter Table NIFF_CHM_Participantes Add CPF Number;
Alter Table NIFF_CHM_Participantes Add InscricaoEmGrupo Varchar2(1) Default 'N' Not Null;
Alter Table NIFF_CHM_Participantes Add IdUsuarioGrupo Number;-- responsavel pela inscrição 
CREATE SEQUENCE SQ_NIFF_CHMIdParticipa START WITH 1 NOMAXVALUE ORDER NOCACHE;
Alter Table NIFF_CHM_USUARIOS Add NaoNotificaCorridas Varchar2(1) Default 'N' Not Null;
Alter Table NIFF_CHM_USUARIOS Add AniversariantesApenasDaEmpresa Varchar2(1) Default 'N' Not Null;

-- Alterado 31/01/2018
Create Table Niff_ADS_Cargos (IdCargo Number Not Null,
                              Descricao Varchar2(300) Not Null,
                              Ativo Varchar2(1) Default 'N' Not Null,
                              RequerExperiencia Varchar2(1) Default 'N' Not Null,
                              IdEscola Number Not Null,
                              SalarioMinimo Number,
                              SalarioMaximo Number) Tablespace Globus_Table;
                              

Alter Table Niff_ADS_Cargos Add Constraint pkNiff_ADS_Cargos
  primary key (IdCargo) using index 
  tablespace GLOBUS_INDEX;                                               
  
ALTER TABLE Niff_ADS_Cargos
       ADD  ( CONSTRAINT fk_Escolaridade_Cargos
              FOREIGN KEY (IdEscola)   
                             REFERENCES Niff_Ads_Escolaridade (IdEscola));                               
  
Create Table Niff_ADS_CompetenciasDoCargo (IdAssoc Number Not Null,
                                           IdCompetencia Number Not Null,
                                           IdCargo Number Not Null,
                                           Ativo Varchar2(1) Default 'N') Tablespace Globus_Table;
                                           

Alter Table Niff_ADS_CompetenciasDoCargo Add Constraint pkNiff_ADS_CompetenciasDoCargo
  primary key (IdAssoc) using index 
  tablespace GLOBUS_INDEX;                  

ALTER TABLE Niff_ADS_CompetenciasDoCargo
       ADD  ( CONSTRAINT fk_Competencia_CompCargo
              FOREIGN KEY (IdCompetencia)   
                             REFERENCES Niff_Ads_Competencias (IdCompetencia));                                                 

ALTER TABLE Niff_ADS_CompetenciasDoCargo
       ADD  ( CONSTRAINT fk_Cargo_CompCargo 
              FOREIGN KEY (IdCargo)   
                             REFERENCES Niff_Ads_Cargos (IdCargo));                                                                              

-- Alterado 01/2/2018
CREATE SEQUENCE SQ_NIFF_ADSIdCompCarg START WITH 1 NOMAXVALUE ORDER NOCACHE;   
Alter Table Niff_ADS_CompetenciasDoCargo Drop Column Ativo;                          

-- Alterado 01/2/2018
Alter Table NIFF_CHM_Participantes Add Sexo Varchar2(1) Default 'M' Not Null;
Alter Table NIFF_CHM_Participantes Add Visualizado Varchar2(1) Default 'N' Not Null;

-- Alterado 03/02/2018
Alter Table NIFF_CHM_Participantes Add ValorInscrito Number;
Alter Table Niff_Chm_Corridas Add ValorGrupo Number;

-- Alterado 05/02/2018
Create Table Niff_ADS_Colaboradores (IdColaborador Number Not Null,
                                     CodIntFunc Number Not Null,
                                     IdEmpresa Number Not Null,
                                     IdDepartamento Number Not Null,
                                     IdCargo Number Not Null,
                                     IdSuperior Number Null ) Tablespace Globus_Table;
                                     
Alter Table Niff_ADS_Colaboradores Add Constraint pkNiff_ADS_Colaboradores
  primary key (IdColaborador) using index 
  tablespace GLOBUS_INDEX;                  
                                     
ALTER TABLE Niff_ADS_Colaboradores
       ADD  ( CONSTRAINT fk_Empresa_Colaboradores
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));     

ALTER TABLE Niff_ADS_Colaboradores
       ADD  ( CONSTRAINT fk_Cargo_Colaboradores
              FOREIGN KEY (IdCargo)   
                             REFERENCES Niff_Ads_Cargos (IdCargo));                                     

ALTER TABLE Niff_ADS_Colaboradores
       ADD  ( CONSTRAINT fk_Departamento_Colaboradores
              FOREIGN KEY (IdDepartamento)   
                             REFERENCES Niff_Chm_Departamento (IdDepartamento));                                                                  
                             
ALTER TABLE Niff_ADS_Colaboradores
       ADD  ( CONSTRAINT fk_Colaboradores_Colaboradores
              FOREIGN KEY (IdSuperior)   
                             REFERENCES Niff_ADS_Colaboradores (IdColaborador));                                                                                               
                             
CREATE SEQUENCE SQ_NIFF_ADSIdColaborador START WITH 1 NOMAXVALUE ORDER NOCACHE;                                

-- Alterado 07/02/2018

Create Table Niff_ADS_CompetenciasDaPessoa (IdCompPessoa Number Not Null,
                                            IdColaborador Number Not Null,
                                            IdAssoc Number Not Null) Tablespace Globus_Table;
                                           
Alter Table Niff_ADS_CompetenciasDaPessoa Add Constraint pkNiff_ADSCompetenciasDaPessoa
  primary key (IdCompPessoa) using index 
  tablespace GLOBUS_INDEX;                  
 
ALTER TABLE Niff_ADS_CompetenciasDaPessoa
       ADD  ( CONSTRAINT fk_Colaborador_CompPessoa 
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_ADS_Colaboradores (IdColaborador));                                                 

ALTER TABLE Niff_ADS_CompetenciasDaPessoa
       ADD  ( CONSTRAINT fk_CompCargo_CompPessoa
              FOREIGN KEY (IdAssoc)   
                             REFERENCES Niff_Ads_CompetenciasdoCargo (IdAssoc));    
                             
CREATE SEQUENCE SQ_NIFF_ADSIdCompPessoa START WITH 1 NOMAXVALUE ORDER NOCACHE;                                
                             
Create Table NIFF_Ads_Cursos (IdCurso Number Not Null,
                              IdColaborador Number Not Null,
                              Descricao Varchar2(500) Not Null,
                              Valor Number Default 0 Not Null,
                              Duracao Number Default 0 Not Null,
                              Inicio Date Not Null,
                              Fim Date Null,
                              Obrigatorio Varchar2(1) Default 'N' Not Null,
                              Opniao Long Null) Tablespace Globus_Table;
 
Alter Table NIFF_Ads_Cursos Add Constraint pkNIFF_Ads_Cursos
  primary key (IdCurso) using index 
  tablespace GLOBUS_INDEX;                  

ALTER TABLE NIFF_Ads_Cursos
       ADD  ( CONSTRAINT fk_Colaborador_Cursos
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_ADS_Colaboradores (IdColaborador));                                                 
                             
CREATE SEQUENCE SQ_NIFF_ADSIdCurso START WITH 1 NOMAXVALUE ORDER NOCACHE;                                
                              
Create Table Niff_Ads_HistoricoColaborador (IdHistorico Number Not Null,
                                            Data Date Not Null,
                                            IdColaborador Number Not Null,
                                            IdCargo Number Null,
                                            IdDepartamento Number Null,
                                            IdSuperior Number Null,
                                            Salario Number Null) Tablespace Globus_Table;

Alter Table Niff_Ads_HistoricoColaborador Add Constraint pkNiff_AdsHistoricoColaborador
  primary key (IdHistorico) using index 
  tablespace GLOBUS_INDEX;                  
                               
ALTER TABLE Niff_Ads_HistoricoColaborador
       ADD  ( CONSTRAINT fk_Cargo_HColaboradores
              FOREIGN KEY (IdCargo)   
                             REFERENCES Niff_Ads_Cargos (IdCargo));                                     

ALTER TABLE Niff_Ads_HistoricoColaborador 
       ADD  ( CONSTRAINT fk_Departamento_HColaboradores
              FOREIGN KEY (IdDepartamento)   
                             REFERENCES Niff_Chm_Departamento (IdDepartamento));                                                                  
                             
ALTER TABLE Niff_Ads_HistoricoColaborador 
       ADD  ( CONSTRAINT fk_Colaborador_HColaborador
              FOREIGN KEY (IdSuperior)   
                             REFERENCES Niff_ADS_Colaboradores (IdColaborador));                                                                                               
                              
CREATE SEQUENCE SQ_NIFF_ADSIdHistorico START WITH 1 NOMAXVALUE ORDER NOCACHE;                                

-- Alteracao 09/02/2018 
Alter Table Niff_Ads_HistoricoColaborador Add IdEscolaridade Number;
Alter Table Niff_ADS_Colaboradores Add IdEscolaridade Number; 

-- Alteracao 14/02/2018
Create Table Niff_Chm_NotifCorrida (IdNotifCorrida Number Not Null,
                                    IdCorrida Number Not Null,
                                    IdUsuario Number Not Null) Tablespace Globus_Table;
                                
Alter Table Niff_Chm_NotifCorrida Add Constraint pkNiff_Chm_NotifCorrida
  primary key (IdNotifCorrida) using index 
  tablespace GLOBUS_INDEX;                  
                               
ALTER TABLE Niff_Chm_NotifCorrida
       ADD  ( CONSTRAINT fk_Corrida_NotifCorrida
              FOREIGN KEY (IdCorrida)   
                             REFERENCES Niff_Chm_Corridas (IdCorrida));                                     

ALTER TABLE Niff_Chm_NotifCorrida 
       ADD  ( CONSTRAINT fk_Usuario_NotifCorrida
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                        
                             
Create Table Niff_Chm_NotificacoesSistema (IdNotificacao Number Not Null,
                                       Tipo Varchar2(2) Not Null,
                                       DataAviso Date Not Null,
                                       DataAcao Date Not Null,                            
                                       IDUsuario Number Not Null,
                                       Motivo Varchar2(1000)) Tablespace Globus_Table;
                      
Alter Table Niff_Chm_NotificacoesSistema Add Constraint pkNiff_Chm_NotificacoesSistema
  primary key (IdNotificacao) using index 
  tablespace GLOBUS_INDEX;                  
                               
ALTER TABLE Niff_Chm_NotificacoesSistema 
       ADD  ( CONSTRAINT fk_Usuario_NotifSistema
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                         
                             
CREATE SEQUENCE SQ_NIFF_chmNotCorrida START WITH 1 NOMAXVALUE ORDER NOCACHE;                                
                             
-- Alteracao 15/02/2018
Create Table Niff_Ads_Metas (IdMetas Number Not Null, 
                             Ativo Varchar2(1) Default 'N' Not Null,
                             Tipo Varchar(1) Default 'C' Not Null,
                             Perspectiva Varchar2(1) Default 'C' Not Null,
                             Descricao Varchar2(1000) Not Null,
                             IdCargo Number Not Null) Tablespace Globus_Table;
                             
Alter Table Niff_Ads_Metas Add Constraint pkNiff_Ads_Metas
  primary key (IdMetas) using index 
  tablespace GLOBUS_INDEX;                   
                               
ALTER TABLE Niff_Ads_Metas  
       ADD  ( CONSTRAINT fk_Cargo_Metas
              FOREIGN KEY (IdCargo)   
                             REFERENCES Niff_Ads_Cargos (IdCargo));                                         
                             
-- Alteração 16/02/2018
Alter Table niff_ads_cargos Add TipoDoCargo Varchar2(2) Null;

Create Table Niff_ADS_MetasDoCargo (IdAssoc Number Not Null,
                                    IdMetas Number Not Null,
                                    IdCargo Number Not Null) Tablespace Globus_Table;                                           

Alter Table Niff_ADS_MetasDoCargo Add Constraint pkNiff_ADS_MetasDoCargo
  primary key (IdAssoc) using index 
  tablespace GLOBUS_INDEX;                  

ALTER TABLE Niff_ADS_MetasDoCargo
       ADD  ( CONSTRAINT fk_metas_MetasCargo
              FOREIGN KEY (IdMetas)   
                             REFERENCES Niff_Ads_Metas (IdMetas));                                                 

ALTER TABLE Niff_ADS_MetasDoCargo
       ADD  ( CONSTRAINT fk_Cargo_MetasCargo 
              FOREIGN KEY (IdCargo)    
                             REFERENCES Niff_Ads_Cargos (IdCargo));                                                                              

CREATE SEQUENCE SQ_NIFF_AdsIdMetasCargo START WITH 1 NOMAXVALUE ORDER NOCACHE;   

Alter Table niff_ads_metas Drop Column idCargo;
Alter Table niff_ads_prazos Add EnvioEmail Varchar2(30) Null;
Alter Table Niff_Ads_Competencias Add TextoExplicativo Long;

-- Alteracao 19/02/2018
Create Table Niff_ADS_Avaliacao (IdAutoAvaliacao Number Not Null,
                                     IdColaborador Number Not Null,
                                     MesReferencia Number Not Null,
                                     Tipo Varchar2(2) Not Null,
                                     DataInicio Date Not Null,
                                     DataFim Date Null ) Tablespace Globus_Table;  
                                     
Alter Table Niff_ADS_Avaliacao Add Constraint pkNiff_ADS_AutoAvaliacao
  primary key (IdAutoAvaliacao) using index 
  tablespace GLOBUS_INDEX;                  

Alter Table Niff_ADS_Avaliacao
       Add ( Constraint fk_Colaborador_AutoAvaliacao 
              Foreign Key (IdColaborador)   
                         References Niff_Ads_Colaboradores (IdColaborador));                                              

Create Table Niff_Ads_ItensAvaliacao (IdItensAuto Number Not Null,
                                      IdAutoAvaliacao Number Not Null,
                                      IdSubComp Number Not Null,
                                      Avaliacao Number(2) Not Null) Tablespace Globus_Table; 
                                          
Alter Table Niff_Ads_ItensAvaliacao Add Constraint pkNiff_Ads_ItensAutoAvaliacao
  primary key (IdItensAuto) using index 
  tablespace GLOBUS_INDEX;                  

Alter Table Niff_Ads_ItensAvaliacao
       Add ( Constraint fk_AutoAvaliacao_ItensAuto 
              Foreign Key (IdAutoAvaliacao)   
                         References Niff_ADS_Avaliacao (IdAutoAvaliacao));                                              

CREATE SEQUENCE SQ_NIFF_AdsIdAvaliacao START WITH 1 NOMAXVALUE ORDER NOCACHE;   
CREATE SEQUENCE SQ_NIFF_AdsIdItensAval START WITH 1 NOMAXVALUE ORDER NOCACHE;                            

-- Alterado 21/02/2018
Alter Table Niff_Ads_Avaliacao Add IdUsuario Number;
Alter Table Niff_Ads_Avaliacao Add IdUsuarioAlteracao Number;
Alter Table Niff_Ads_Avaliacao Add DataAlteracao Date;

-- Alterado 26/02/2018
Alter Table Niff_Ads_Avaliacao Add FeedBackGestor Varchar2(2000);
Alter Table Niff_Ads_Avaliacao Add Comentario Varchar2(2000);
Alter Table Niff_Ads_Avaliacao Add DataFeedBack Date;
Alter Table Niff_Ads_Avaliacao Add DataComentario Date;

-- Alterado 27/02/2018
Alter Table Niff_Ads_Metas Add TextoBI Varchar2(200);

Create Table Niff_Ads_ItensAvaliacaoMetas (IdItensAuto Number Not Null,
                                      IdAutoAvaliacao Number Not Null,
                                      IdMetas Number Not Null,
                                      valor Number(3) Not Null) Tablespace Globus_Table; 

Alter Table Niff_Ads_ItensAvaliacaoMetas Add Constraint pkNiff_Ads_ItensAvaliacaoMetas
  primary key (IdItensAuto) using index 
  tablespace GLOBUS_INDEX;                  

Alter Table Niff_Ads_ItensAvaliacaoMetas 
       Add ( Constraint fk_Avaliacao_ItensAutoMeta
              Foreign Key (IdAutoAvaliacao)   
                         References Niff_ADS_Avaliacao (IdAutoAvaliacao));                                               
                         
Alter Table Niff_Ads_ItensAvaliacaoMetas 
       Add ( Constraint fk_Metas_ItensAutoMeta
              Foreign Key (IdMetas)   
                         References Niff_Ads_Metas (IdMetas)); 
                         
-- alterado 01/03/2018
Alter Table Niff_Ads_ItensAvaliacao Add Comentario Varchar2(2000);
Alter Table NIFF_ADS_SubCompetencias Add ExibeNaAutoAvaliacao Varchar2(1) Default 'S' Not Null;
Alter Table NIFF_ADS_SubCompetencias Add ExibeNaAvaliacaoRH Varchar2(1) Default 'S' Not Null;
Alter Table NIFF_ADS_SubCompetencias Add ExibeNaAvaliacaoGestor Varchar2(1) Default 'S' Not Null;

-- Alterado 07/03/2018
Create Table Niff_ESF_ISSIntegrado (IdISSInt Number Not Null,
                                    CodIntNF Number Not Null,
                                    CodISSInt Number Not Null,
                                    Data Date Not Null,
                                    IdUsuario Number Not Null)  Tablespace Globus_Table;
                                    
Alter Table Niff_ESF_ISSIntegrado Add Constraint pkNiff_ESF_ISSIntegrado
  primary key (IdISSInt) using index 
  tablespace GLOBUS_INDEX;                  
                                    
ALTER TABLE Niff_ESF_ISSIntegrado  
       ADD  ( CONSTRAINT fk_Usuario_ISSIntegrado
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                        
                                    
-- Alterado 13/03/2018
Alter Table Niff_Chm_NotificacoesSistema Add Status Varchar2(1) Default 'N';
Alter Table Niff_Ads_ItensAvaliacaoMetas Add ValorEsperado Number;
Alter Table Niff_Ads_ItensAvaliacaoMetas Add Realizado Number;
Alter Table Niff_Ads_ItensAvaliacaoMetas Add Eficiencia Number;
Alter Table Niff_Ads_ItensAvaliacaoMetas Add Resultado Number;

-- Alterado 15/03/2018
Create Table Niff_Ads_Pontuacao (IdPontos Number Not Null,
                                 MesReferencia Number Not Null,
                                 Base100 Number Default 1 Not Null,
                                 NaoAtende Number Default 1 Not Null,
                                 AtendeParc Number Default 1 Not Null,
                                 AtendePlen Number Default 1 Not Null,
                                 Supera Number Not Null) Tablespace Globus_Table;


Alter Table Niff_Ads_Pontuacao Add Constraint pkNiff_Ads_Pontuacao
  primary key (IdPontos) using index 
  tablespace GLOBUS_INDEX;                                                          
  
CREATE SEQUENCE SQ_NIFF_AdsIdPontos START WITH 1 NOMAXVALUE ORDER NOCACHE;   

Alter Table Niff_Ads_ItensAvaliacao Add Pontuacao Number;

-- Alterado 16/03/2018
Alter Table Niff_Ads_Avaliacao Add NotaAvaliacao Number;

-- Alterado 19/03/2018
Alter Table Niff_Chm_Usuarios Add AcessoDeControladoria Varchar2(1) Default 'N';
Alter Table Niff_Chm_NotificacoesSistema Add DataFimAcao Date;
Alter Table Niff_Chm_NotificacoesSistema Drop Column Tipo;

CREATE SEQUENCE SQ_NIFF_AdsNotSistema START WITH 1 NOMAXVALUE ORDER NOCACHE;   

-- Alterado 20/03/2018
Alter Table Niff_Chm_Usuarios Add VisualizaRadarCompleto Varchar2(1) Default 'N';

-- Alterado 22/03/2018
Create Table Niff_Pto_ColabPeriodo (IdPeriodo Number Not Null,
                                    IdColaborador Number Not Null,
                                    ReferenciaInicio Number Not Null,
                                    ReferenciaFim Number Not Null,
                                    Ativo Varchar2(1) Default 'N' Not Null)  Tablespace Globus_Table;
                                    

Alter Table Niff_Pto_ColabPeriodo Add Constraint pkNiff_Pto_ColabPeriodo
  primary key (IdPeriodo) using index 
  tablespace GLOBUS_INDEX;                                                          
                                        
ALTER TABLE Niff_Pto_ColabPeriodo   
       ADD  ( CONSTRAINT fk_Colaborador_ColabPeriodo
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                                        
                                                                          

-- Alterado 03/04/2018
ALTER TABLE NIFF_Ads_ItensAvaliacaoMetas Modify Peso Number

-- Alterado 05/04/2018
Alter Table NIFF_CHM_Usuarios Add VisualizaBancoHorasDoDepto Varchar2(1) Default 'N' Not null;

--Alterado 09/04/2018
Alter Table Niff_Ads_Pontuacao Add Piso Number Default 0 not null;

-- Alterado 13/04/2018
Alter Table niff_chm_participantes Add NumeroPeito Number;
Alter Table niff_ads_metas Add RegraFormula Varchar2(1) Default 'A' Not Null;
Alter Table niff_ads_metas Add Formula Varchar2(100) ;

-- Alterado 17/04/2018
Alter Table Niff_Ads_Pontuacao Add Fator Number Default 0 not null;

-- Alterado 20/04/2018
Create Table Niff_Ads_PontuacaoNineBox (IdNineBox Number Not Null,
                                        Referencia Number Not Null,
                                        IdCargo Number Null,
                                        IdColaborador Number Null,
                                        Ativo Varchar2(1) Default 'N' Not Null,
                                        PontoDominancia Number,
                                        PontoExtroversao Number,
                                        PontoPaciencia Number,
                                        PontoFormalidade Number,
                                        ToleranciaDominancia Number,
                                        ToleranciaExtroversao Number, 
                                        ToleranciaPaciencia Number,
                                        ToleranciaFormalidade Number,
                                        Descontar Number) Tablespace Globus_table;
                                        
Alter Table Niff_Ads_PontuacaoNineBox Add Constraint Pk_NIFFPontuacaoNineBox 
  primary key (IdNineBox) using index 
  tablespace GLOBUS_INDEX;                                 
                
ALTER TABLE Niff_Ads_PontuacaoNineBox   
       ADD  ( CONSTRAINT fk_Colaborador_PontNineBox
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                                        
                
ALTER TABLE Niff_Ads_PontuacaoNineBox   
       ADD  ( CONSTRAINT fk_Cargo_PontNineBox
              FOREIGN KEY (IdCargo)   
                             REFERENCES Niff_Ads_Cargos (IdCargo));                                        

-- Alterado 23/04/2018                                                                                                                                                                                            
Alter Table Niff_Ads_Colaboradores Add ParticipaAvaliacao Varchar2(1) Default 'S' Not Null;
Alter Table Niff_Ads_Colaboradores Add Nome Varchar2(255);
CREATE SEQUENCE SQ_NIFF_AdsIdPontos9box START WITH 1 NOMAXVALUE ORDER NOCACHE;

-- Alterado 25/04/2018
Create Table Niff_Bib_Categorias (IdCategLivros Number Not Null,
                                  Descricao Varchar2(150) Not Null,
                                  Ativo Varchar2(1) Default 'S' Not Null) Tablespace Globus_Table;
                                  
Alter Table Niff_Bib_Categorias Add Constraint Pk_NiffBibCategorias
  primary key (IdCategLivros) using index 
  tablespace GLOBUS_INDEX;     
  
Create Table Niff_Bib_Livros (IdLivros Number Not Null,
                              IdCategLivros Number Not Null,
                              IdColaborador Number Null,
                              Nome Varchar2(255) Not Null,
                              NomeOriginal Varchar2(255) Not Null,
                              DataRegistro Date Not Null,
                              TipoCessao Varchar2(1) Default 'T' Not Null,
                              DataDevolucao Date Not Null,
                              Conservacao Varchar2(1) Default 'B' Not Null ) Tablespace Globus_Table;
                                  
Alter Table Niff_Bib_Livros Add Constraint Pk_NiffBibLivros
  primary key (IdLivros) using index 
  tablespace GLOBUS_INDEX;     
                              
ALTER TABLE Niff_Bib_Livros   
       ADD  ( CONSTRAINT fk_CategLivro_Livro
              FOREIGN KEY (IdCategLivros)   
                             REFERENCES Niff_Bib_Categorias (IdCategLivros));                                        

ALTER TABLE Niff_Bib_Livros   
       ADD  ( CONSTRAINT fk_Colaborador_Livro
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                                        


Create Table Niff_Bib_Emprestimo (IdEmprestimo Number Not Null, 
                                  IdLivros Number Not Null,
                                  IdColaborador Number Not Null,
                                  DataEmprestimo Date Not Null,
                                  DataProrrogacao Date,
                                  DataDevolucao Date,
                                  QtdeDiasEmprestimo Number Not Null,
                                  QtdeDiasProrrogacao Number Default 0,
                                  Conservacao Varchar2(1) Default 'B' Not Null) Tablespace Globus_Table;
                                  
Alter Table Niff_Bib_Emprestimo Add Constraint Pk_NiffBibEmprestimo
  primary key (IdEmprestimo) using index 
  tablespace GLOBUS_INDEX;     

ALTER TABLE Niff_Bib_Emprestimo   
       ADD  ( CONSTRAINT fk_Livro_Emprestimo
              FOREIGN KEY (IdLivros)   
                             REFERENCES Niff_Bib_Livros (IdLivros));                                        
                             
ALTER TABLE Niff_Bib_Emprestimo   
       ADD  ( CONSTRAINT fk_Colaborador_Emprestimo
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                                        
  
Create Table Niff_Bib_Resenha (IdResenha Number Not Null,
                               IdLivros Number Not Null,
                               IdColaborador Number Not Null,
                               Data Date Not Null,
                               Resenha Long,
                               Ativo Varchar2(1) Default 'N' Not Null) Tablespace Globus_Table;
                                  
Alter Table Niff_Bib_Resenha Add Constraint Pk_NiffBibResenha
  primary key (IdResenha) using index 
  tablespace GLOBUS_INDEX;     

ALTER TABLE Niff_Bib_Resenha
       ADD  ( CONSTRAINT fk_Livro_Resenha
              FOREIGN KEY (IdLivros)   
                             REFERENCES Niff_Bib_Livros (IdLivros));                                        
                             
ALTER TABLE Niff_Bib_Resenha   
       ADD  ( CONSTRAINT fk_Colaborador_Resenha
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));    
                             
Create Table Niff_Bib_Perguntas (IdPergunta Number Not Null,
                                 IdResenha Number Not Null,
                                 Pergunta Varchar2(2000) Not Null,
                                 Resposta Varchar2(2000) Not Null ) Tablespace Globus_Table;

Alter Table Niff_Bib_Perguntas Add Constraint Pk_NiffBibPerguntas
  primary key (IdPergunta) using index 
  tablespace GLOBUS_INDEX;     
  
ALTER TABLE Niff_Bib_Perguntas   
       ADD  ( CONSTRAINT fk_Resenha_Perguntas
              FOREIGN KEY (IdResenha)   
                             REFERENCES Niff_Bib_Resenha (IdResenha));    
                             
Create Table Niff_Bib_Pontuacao (IdPontuacao Number Not Null,
                                 IdLivros Number Not Null,
                                 IdColaborador Number Not Null,
                                 PontoPerguntas Number,
                                 PontoRespostas Number,
                                 PontoResenha Number ) Tablespace Globus_Table;

Alter Table Niff_Bib_Pontuacao Add Constraint Pk_NiffBibPontuacao
  primary key (IdPontuacao) using index 
  tablespace GLOBUS_INDEX;     
   
ALTER TABLE Niff_Bib_Pontuacao   
       ADD  ( CONSTRAINT fk_Livro_Pontuacao
              FOREIGN KEY (IdLivros)   
                             REFERENCES Niff_Bib_Livros (IdLivros));                                        
                             
ALTER TABLE Niff_Bib_Pontuacao   
       ADD  ( CONSTRAINT fk_Colaborador_Pontuacao
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                                        
                                 
Create Table Niff_Bib_Reserva (IdReserva Number Not Null,
                               IdLivros Number Not Null,
                               IdColaborador Number Not Null,
                               Data Date Not Null,
                               DataEmprestimo Date) Tablespace Globus_Table;

Alter Table Niff_Bib_Reserva Add Constraint Pk_NiffBibReserva
  primary key (IdReserva) using index 
  tablespace GLOBUS_INDEX;     
  
ALTER TABLE Niff_Bib_Reserva   
       ADD  ( CONSTRAINT fk_Livro_Reserva
              FOREIGN KEY (IdLivros)   
                             REFERENCES Niff_Bib_Livros (IdLivros));                                        
                             
ALTER TABLE Niff_Bib_Reserva   
       ADD  ( CONSTRAINT fk_Colaborador_Reserva
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                                        
                                 
Create Table Niff_Ads_FatorEmpresa (IdPontos Number Not Null,
                                    IdEmpresa Number Not Null,
                                    Fator Number Not Null ) Tablespace Globus_table;
                              
Alter Table Niff_Ads_FatorEmpresa Add Constraint Pk_NiffAdsfatorEmp
  primary key (IdPontos,IdEmpresa) using index 
  tablespace GLOBUS_INDEX;    

ALTER TABLE Niff_Ads_FatorEmpresa   
       ADD  ( CONSTRAINT fk_fatorEmp_Pontuacao
              FOREIGN KEY (IdPontos)   
                             REFERENCES Niff_Ads_Pontuacao (IdPontos));                                        

-- Alterado 27/04/2018
Alter Table Niff_Ads_Pontuacao Add PesoNumerica Number;
Alter Table niff_chm_empresas Add AvaliaColaboradores Varchar2(1) Default 'N' Not Null;

Create Table Niff_ads_EmpresasColAvalia (IdEmpAvalia Number Not Null,
                                         IdEmpresa Number Not null,
                                         IdColaborador Number Not Null,
                                         Inicio Date,
                                         Fim Date) Tablespace Globus_table;

Alter Table Niff_ads_EmpresasColAvalia Add Constraint Pk_NiffAdsEmpAvalia
  primary key (IdEmpAvalia) using index 
  tablespace GLOBUS_INDEX;    

ALTER TABLE Niff_ads_EmpresasColAvalia   
       ADD  ( CONSTRAINT fk_EmpAvalia_Empresa
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                        
                                         
ALTER TABLE Niff_ads_EmpresasColAvalia   
       ADD  ( CONSTRAINT fk_EmpAvalia_Colaborador
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                                                                                 

CREATE SEQUENCE SQ_NIFF_IdEmpAvalia START WITH 1 NOMAXVALUE ORDER NOCACHE;                             

-- Alterado 02/05/2018
Alter Table niff_ads_avaliacao Add IdEmpresa Number;


-- Alterado 03/05/2018 -- Bolão

Create Table Niff_bol_Times (Id Number Not Null,
                             Nome Varchar2(40) Not Null,
                             Bandeira blob,
                             Ano Number Not Null,
                             Grupo Varchar2(1) Not Null) Tablespace Globus_Table;
                             
Alter Table Niff_bol_Times Add Constraint Pk_NiffbolTimes
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;  
                                 
Create Table Niff_Bol_Jogos (Id Number Not Null,
                             Data Date Not Null,
                             IdTime1 Number Not Null,
                             IdTime2 Number Not Null, 
                             Placar1 Number Default 0 Not Null, 
                             Placar2 Number Default 0 Not Null,
                             DataLimite Date Not Null,
                             Localizacao Varchar2(100) Not Null ) Tablespace Globus_Table;
                             
Alter Table Niff_Bol_Jogos Add Constraint Pk_NiffbolJogos
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;  

ALTER TABLE Niff_Bol_Jogos   
       ADD  ( CONSTRAINT fk_BolTime1_BolJogos
              FOREIGN KEY (IdTime1)   
                             REFERENCES Niff_bol_Times (Id));                                                                                 

ALTER TABLE Niff_Bol_Jogos   
       ADD  ( CONSTRAINT fk_BolTime2_BolJogos
              FOREIGN KEY (IdTime2)   
                             REFERENCES Niff_bol_Times (Id));                                                                                 
                             
Create Table Niff_bol_Palpites (Id Number Not Null,
                                Data Date Not Null,
                                DataAlteracao Date Not Null,
                                IdColaborador Number Not Null, 
                                IdJogo Number Not Null,
                                Placar1 Number Default 0 Not Null,
                                Placar2 Number Default 0 Not Null,
                                Pontuacao Number Default 0 Not Null) Tablespace Globus_Table;
                             
Alter Table Niff_bol_Palpites Add Constraint Pk_NiffbolPalpites
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;  
                            
ALTER TABLE Niff_bol_Palpites   
       ADD  ( CONSTRAINT fk_BolJogos_BolPalpites
              FOREIGN KEY (IdJogo)    
                             REFERENCES Niff_bol_Jogos (Id));                                                                                 

ALTER TABLE Niff_bol_Palpites   
       ADD  ( CONSTRAINT fk_Colaborador_BolPapites
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                              
                             
Create Table Niff_Bol_Pontuacao (Id Number Not Null,
                                 Nome Varchar2(30) Not Null,
                                 Pontos Number ) Tablespace Globus_Table;
                             
Alter Table Niff_Bol_Pontuacao Add Constraint Pk_NiffbolPontuacao
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;  
                            
Create Table Niff_bol_PalpiteFinal (Id Number Not Null,
                                    IdColaborador Number Not Null,
                                    IdTimeVencedor Number Not Null,
                                    IdTimeVice Number Not Null,
                                    IdTime3Lugar Number Not Null,
                                    Data Date Not Null, 
                                    DataAlteracao Date Not Null) Tablespace Globus_table;
                                    
Alter Table Niff_bol_PalpiteFinal Add Constraint Pk_NiffbolPalpitesFinal
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;  
                            
ALTER TABLE Niff_bol_PalpiteFinal   
       ADD  ( CONSTRAINT fk_BolTimes_BolPalpiteFinalC
              FOREIGN KEY (IdTimeVencedor)    
                             REFERENCES Niff_bol_Times (Id));                                                                                 

ALTER TABLE Niff_bol_PalpiteFinal   
       ADD  ( CONSTRAINT fk_BolTimes_BolPalpiteFinalV
              FOREIGN KEY (IdTimeVice)    
                             REFERENCES Niff_bol_Times (Id));                                                                                 

ALTER TABLE Niff_bol_PalpiteFinal   
       ADD  ( CONSTRAINT fk_BolTimes_BolPalpiteFinal3
              FOREIGN KEY (IdTime3Lugar)    
                             REFERENCES Niff_bol_Times (Id));                                                                                 

ALTER TABLE Niff_bol_PalpiteFinal   
       ADD  ( CONSTRAINT fk_Colaborador_BolPapiteFinal
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                              
                                    
Alter Table Niff_bol_Times Add Sigla Varchar2(3) Not Null;
Alter Table Niff_Bol_Pontuacao Add Sigla Number Not Null;

CREATE SEQUENCE SQ_NIFF_IdBolPalpite START WITH 1 NOMAXVALUE ORDER NOCACHE;
CREATE SEQUENCE SQ_NIFF_IdBolPalpiteFim START WITH 1 NOMAXVALUE ORDER NOCACHE;

-- Alterado 04/05/2018
Alter Table Niff_Bol_Jogos Add Fase Varchar2(2) Default 'GP' Not Null;

-- Alterado 07/05/2018
Alter Table Niff_Bol_Jogos Add JogoEncerrado Varchar2(1) Default 'N' Not Null;

-- Alterado 08/05/2018
Alter Table niff_chm_usuarios Add ParticipaBolaoCopa Varchar2(1) Default 'S';

Create Table Niff_bol_NotificacaoJogos (Id Number Not Null,
                                        IdJogo Number Not Null,
                                        IdColaborador Number Not Null) Tablespace Globus_table;
                                        
Alter Table Niff_bol_NotificacaoJogos Add Constraint Pk_NiffbolNotificacao
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;  
                            
ALTER TABLE Niff_bol_NotificacaoJogos   
       ADD  ( CONSTRAINT fk_BolJogos_BolNotificacao
              FOREIGN KEY (IdJogo)    
                             REFERENCES Niff_bol_Jogos (Id));                                                                                 

ALTER TABLE Niff_bol_NotificacaoJogos   
       ADD  ( CONSTRAINT fk_Colaborador_BolNotificacao
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                              
                                          
CREATE SEQUENCE SQ_NIFF_IdBolNotificacao START WITH 1 NOMAXVALUE ORDER NOCACHE;

-- Alterado 09/05/2018
Alter Table niff_chm_usuarios Add AdministraBolaoCopa Varchar2(1) Default 'N' Not Null;
Alter Table niff_bol_palpitefinal Add Pontuacao Number Default 0;
Alter Table niff_bol_palpitefinal Add AcertouCampeao Varchar2(1) Default 'N' Not Null;
Alter Table niff_bol_palpitefinal Add AcertouViceCampeao Varchar2(1) Default 'N' Not Null;
Alter Table niff_bol_palpitefinal Add Acertou3Lugar Varchar2(1) Default 'N' Not Null;
Alter Table niff_bol_palpitefinal Add Encripta Varchar2(100) Null;
Alter Table niff_bol_palpites Add Encripta Varchar2(100) Null;
Alter Table Niff_Bol_Jogos Add Encripta Varchar2(100) Null;

Create Table Niff_bol_ValorArrecadado (Id Number Not Null,
                                       Data Date Not Null,
                                       IdColaborador Number Not Null,
                                       Valor Number Not Null) Tablespace Globus_table;
                                       
Alter Table Niff_bol_ValorArrecadado Add Constraint Pk_NiffbolValorArrecado
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;  
                                       
ALTER TABLE Niff_bol_ValorArrecadado   
       ADD  ( CONSTRAINT fk_Colaborador_BolValorArrec
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                              
                                       
-- Alterado 10/05/2018
Alter Table niff_bib_emprestimo Add Devolvido Varchar2(1) Default 'N' Not Null;
Alter Table niff_bib_emprestimo Add DevolvidoEm Date;

-- Alterado 24/05/2018
Alter Table Niff_Chm_Usuarios Add AdministraBiblioteca Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Chm_Usuarios Add AdministraCorridas Varchar2(1) Default 'N' Not Null;

Create Table NIFF_FIS_ParametrosArquivei (IdParametro Number Not Null,
                                          IdEmpresa Number Not Null,
                                          Diretorio Varchar2(500) Not Null,
                                          DataCadastro Date Not Null,
                                          DataAlteracao Date Null) Tablespace Globus_table;

Alter Table NIFF_FIS_ParametrosArquivei Add Constraint Pk_NIFFFISParamArquivei
  primary key (IdParametro) using index 
  tablespace GLOBUS_INDEX;                                 
  
ALTER TABLE NIFF_FIS_ParametrosArquivei   
       ADD  ( CONSTRAINT fk_Empresa_ParamArquivei
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                              
                             
Create Table NIFF_FIS_ItensParamArquivei (IdItens Number Not Null,
                                          IdParametro Number Not Null,
                                          CampoValidar Varchar2(1) Default 'N' Not Null,
                                          CampoExibir Varchar2(1) Default 'N' Not Null,
                                          NomeCampo Varchar2(100) Not Null) Tablespace Globus_Table;
                                          
Alter Table NIFF_FIS_ItensParamArquivei Add Constraint Pk_NIFFFISItensParamArquivei
  primary key (IdItens) using index 
  tablespace GLOBUS_INDEX;                                 
  
ALTER TABLE NIFF_FIS_ItensParamArquivei   
       ADD  ( CONSTRAINT fk_ParmArquivei_ItensParam
              FOREIGN KEY (IdParametro)   
                             REFERENCES NIFF_FIS_ParametrosArquivei (IdParametro));                              
 
 Alter Table NIFF_FIS_ItensParamArquivei Add Tipo Varchar2(1) Default 'C' Not Null;
 Alter Table NIFF_FIS_ParametrosArquivei Add AcaoComArquivo Varchar2(1) Default 'E' Not Null;

 Create Table NIFF_FIS_Arquivei (IdArquivei Number Not Null,
                                DataImportado Date Not Null,
                                NomeArquivo Varchar2(255) Not Null,
                                CodDoctoEsf Number Null,
                                CodIntNF Number Null,
                                IdEmpresa Number Not Null, 
                                IdUsuarioVisualizou Number Null,
                                DataVisualizou Date Null,
                                CNPJDestinatario Varchar2(20) Not Null,
                                IEDestinatario Varchar2(20) Not Null,
                                EnderecoDestinatario Varchar2(200) Not Null,
                                BairroDestinatario Varchar2(200) Not Null, 
                                CEPDestinatario  Varchar2(10) Not Null,
                                RazaoSocialDestinatario Varchar2(200) Not Null,
                                CNPJEmitente Varchar2(20) Not Null,
                                IEEmitente Varchar2(20) Not Null,
                                RazaoSocialEmitente Varchar2(200) Not Null,
                                ChaveDeAcesso Varchar2(100) Not Null,
                                DataEmissao Date Not Null,
                                NumeroNF Number Not Null,
                                ModeloNF  Varchar2(20) Not Null,
                                Serie  Varchar2(20) Not Null,
                                NaturezaOperacao  Varchar2(50) Not Null,
                                ValorTotalNF Number Not Null,
                                ValorProduto Number Not Null,
                                BaseICMS Number Not Null,
                                DadosAdicionais  Varchar2(2000) Not Null ) Tablespace Globus_Table;
                                
Alter Table NIFF_FIS_Arquivei Add Constraint Pk_NIFFFISArquivei
  primary key (IdArquivei) using index 
  tablespace GLOBUS_INDEX;                                 
  
ALTER TABLE NIFF_FIS_Arquivei   
       ADD  ( CONSTRAINT fk_Empresa_Arquivei
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                              

ALTER TABLE NIFF_FIS_Arquivei    
       ADD  ( CONSTRAINT fk_Usuario_Arquivei
              FOREIGN KEY (IdUsuarioVisualizou)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                              
                                
Create Table NIFF_FIS_ItensArquivei (IdItens Number Not Null,
                                     IdArquivei Number Not Null,
                                     ValorICMS Number Not Null,
                                     AliquotaICMS Number Not Null,
                                     ValorICMSST Number Not Null,
                                     ValorIPI Number Not Null,
                                     Desconto Number Not Null,
                                     Seguro Number Not Null,
                                     OutrasDespesas Number Not Null,
                                     ValorFrete Number Not Null,
                                     CCe varchar2(10) Not Null,
                                     CST varchar2(10) Not Null,
                                     CSTICMS varchar2(10) Not Null,
                                     CFOP Number Not Null,
                                     ValorTotal  Number Not Null) Tablespace Globus_Table;
         
Alter Table NIFF_FIS_ItensArquivei Add Constraint Pk_NIFFFISItensArquivei
  primary key (IdItens) using index 
  tablespace GLOBUS_INDEX;                                 
                                     
ALTER TABLE NIFF_FIS_ItensArquivei   
       ADD  ( CONSTRAINT fk_Arquivei_ItensArquivei
              FOREIGN KEY (IdArquivei)   
                             REFERENCES NIFF_FIS_Arquivei (IdArquivei));                              
                                                                                                                         
ALTER TABLE NIFF_FIS_Arquivei Add ComDiferencas Varchar2(1) Default 'N' Not Null;
Alter Table NIFF_FIS_Arquivei Add NumeroEndDestinatario Varchar2(10);
Alter Table NIFF_FIS_Arquivei Add Tipo Varchar2(10);
Alter Table NIFF_FIS_Arquivei Add Status Varchar2(20);
Alter Table NIFF_FIS_Arquivei Add Operacao Varchar2(20);
ALTER TABLE NIFF_FIS_ItensArquivei Add ComDiferencas Varchar2(1) Default 'N' Not Null;

CREATE SEQUENCE SQ_NIFF_IDArquivei START WITH 1 NOMAXVALUE ORDER NOCACHE;
CREATE SEQUENCE SQ_NIFF_IdItensArquivei START WITH 1 NOMAXVALUE ORDER NOCACHE;

-- Alterado 30/05/2018
Alter Table niff_fis_itensparamarquivei Add Ordem Number;

-- Alterado 01/06/2018
Create Table niff_bib_Respostas (Id Number Not Null,
                                 IdPergunta Number Not Null,
                                 IdColaborador Number Not Null,
                                 Data Date Not Null,
                                 Resposta Varchar2(2000) Null, 
                                 Aprovada Varchar2(1) Default 'N' Not Null,
                                 Pontuacao Number, 
                                 DataAprovacao Date) Tablespace Globus_table;
                                 
Alter Table niff_bib_Respostas Add Constraint Pk_niffbibRespostas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;  
                                       
ALTER TABLE niff_bib_Respostas   
       ADD  ( CONSTRAINT fk_Colaborador_BibResposta
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                              
                                       
ALTER TABLE niff_bib_Respostas   
       ADD  ( CONSTRAINT fk_Perguntas_BibResposta
              FOREIGN KEY (IdPergunta)   
                             REFERENCES Niff_Bib_Perguntas (IdPergunta));                              
                                 
Alter Table niff_bib_resenha Add DataLiberacao Date;
Alter Table Niff_Bib_Resenha Add Pontuacao Number;                                 

-- Alterado 04/06/2018
Alter Table niff_bib_respostas Add Certa Varchar2(1) Default 'N' Not Null;
Alter Table niff_bib_respostas Add idUsuario Number;
Alter Table niff_bib_resenha Add idUsuario Number;

-- Alterado 05/06/2018
Create Table Niff_Fis_CFOPCST (Id Number Not Null,
                               CfopSaida Number Not Null,
                               CfopEntrada Number Not Null,
                               CST Number Not Null,
                               Operacao Number Null) Tablespace Globus_table;
                               
Alter Table Niff_Fis_CFOPCST Add Constraint Pk_NiffFisCFOPCST
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 
                                 
-- Alterado 12/06/2018
Alter Table niff_bib_livros Add Imagem Blob;

-- Alterado 13/06/2018
Alter Table niff_bib_livros Add Sinopse Long;

-- Alterado 15/06/2018
Alter Table Niff_Fis_Parametrosarquivei Add DiretorioExportacao Varchar2(500);

--Alterado 18/06/2018
Alter Table Niff_Fis_Itensarquivei Add NCM Varchar2(10);

-- Alterado 20/06/2018
Create Table Niff_ADS_ColabDepartamento (Id Number Not Null,
                                         IdColaborador Number Not Null,
                                         IdDepartamento Number Not Null,
                                         Ativo Varchar2(1) Default 'N' ) Tablespace Globus_table;

Alter Table Niff_ADS_ColabDepartamento Add Constraint Pk_NiffADSColabDepar
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;      
  
ALTER TABLE Niff_ADS_ColabDepartamento
       ADD  ( CONSTRAINT fk_Colaborador_ColabDepart 
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_ADS_Colaboradores (IdColaborador));                                                 
                              
-- Alterado 27/06/2018
Create Table Niff_Chm_Telefone (Id Number Not Null,
                                Telefone Number Not Null,
                                Ramal Number Not Null,
								Ativo Varchar2(1) Default 'N') Tablespace Globus_Table;

Alter Table Niff_Chm_Telefone Add Constraint Pk_NiffChmTelefone
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;      

Create Table Niff_Chm_ColabTelefone (Id Number Not Null,
                                     IdTelefone Number Not Null,
                                     IdColaborador Number) Tablespace Globus_Table; 

Alter Table Niff_Chm_ColabTelefone Add Constraint PK_NiffChmColabTelefone
  primary key (Id) using index  
  tablespace GLOBUS_INDEX;      
  
ALTER TABLE Niff_Chm_ColabTelefone
       ADD  ( CONSTRAINT fk_Colaborador_ColabTelefone 
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_ADS_Colaboradores (IdColaborador));                                                 
                                 
ALTER TABLE Niff_Chm_ColabTelefone
       ADD  ( CONSTRAINT fk_Telefone_ColabTelefone 
              FOREIGN KEY (IdTelefone)   
                             REFERENCES Niff_Chm_Telefone (Id));                                                 
                                                                  
-- Alterado 28/06/2018
Alter Table niff_chm_telefone Add NomeLocal Varchar2(50) Null;
Alter Table niff_chm_colabtelefone Add Complemento Varchar2(50);

-- Alterado 02/07/2018
Alter Table niff_bol_jogos Add penalti1 Number;
Alter Table niff_bol_jogos Add penalti2 Number;

-- alterado 03/07/2018 
drop Table Niff_Chm_ColabTelefone;
drop table niff_chm_telefone;


Create Table NIFF_CHM_Telefone (Id Number Not Null,
                                IdEmpresa Number Not Null,
                                Numero Number Not Null,
                                Operadora Varchar2(20) ) Tablespace Globus_Table;

Alter Table Niff_Chm_Telefone Add Constraint Pk_NiffChmTelefone
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                      

ALTER TABLE Niff_Chm_Telefone
       ADD  ( CONSTRAINT fk_Telefone_Empresa
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                                 
  
Create Table NIFF_CHM_Ramal (Id Number Not Null,
                             IdTelefone Number Not Null,
                             Grupo Varchar2(30) Not Null,
                             Numero Number Not Null) Tablespace Globus_table;

Alter Table NIFF_CHM_Ramal Add Constraint PK_NiffChmRamal
  primary key (Id) using index  
  tablespace GLOBUS_INDEX;      

ALTER TABLE NIFF_CHM_Ramal
       ADD  ( CONSTRAINT fk_Telefone_Ramal 
              FOREIGN KEY (IdTelefone)   
                             REFERENCES Niff_Chm_Telefone (Id));                                                 
                             
Create Table NIFF_CHM_ColabRamal (Id Number Not Null,
                                  IdRamal Number Not Null,
                                  IdColaborador Number Null,
                                  Nome Varchar2(40) Not Null,
                                  Complemento Varchar2(30) Null) Tablespace Globus_table;
                                                               
Alter Table NIFF_CHM_ColabRamal Add Constraint PK_NiffChmColabRamal
  primary key (Id) using index  
  tablespace GLOBUS_INDEX;      
        
ALTER TABLE NIFF_CHM_ColabRamal
       ADD  ( CONSTRAINT fk_Colaborador_ColabRamal 
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_ADS_Colaboradores (IdColaborador));                                                 
                                 
ALTER TABLE NIFF_CHM_ColabRamal
       ADD  ( CONSTRAINT fk_Ramal_ColabRamal 
              FOREIGN KEY (IdRamal)   
                             REFERENCES NIFF_CHM_Ramal (Id));                                                 
                                                                  

-- Alterado 12/07/2018
Alter Table niff_ads_colaboradores Add sexo Varchar2(1);

-- Alterado  16/07/2018
Alter Table niff_ads_colaboradores Add Ativo Varchar2(1) Default 'S';

-- Alterado 17/07/2018
Create Table Niff_Tor_Torneio (Id Number Not Null,
                               Nome Varchar2(100) Not null,
                               Ativo Varchar2(1) Default 'N' Not Null,
                               MarioGrandPrix Varchar2(1) Default 'N' Not Null,
                               MarioBattle Varchar2(1) Default 'N' Not Null,                                                              
                               Arms Varchar2(1) Default 'N' Not Null,                               
                               Pinball Varchar2(1) Default 'N' Not Null,
                               Minimo Number,
                               Maximo Number ) Tablespace Globus_Table;
                               
Alter Table Niff_Tor_Torneio Add Constraint PK_NiffTorneio
  primary key (Id) using index  
  tablespace GLOBUS_INDEX;      
  
Create Table Niff_Tor_Torneio (Id Number Not Null,
                               Nome Varchar2(100) Not null,
                               Ativo Varchar2(1) Default 'N' Not Null,
                               MarioGrandPrix Varchar2(1) Default 'N' Not Null,
                               MarioBattle Varchar2(1) Default 'N' Not Null,                                                              
                               Arms Varchar2(1) Default 'N' Not Null,                               
                               Pinball Varchar2(1) Default 'N' Not Null,
                               Minimo Number,
                               Maximo Number ) Tablespace Globus_Table;
                               
Alter Table Niff_Tor_Torneio Add Constraint PK_NiffTorneio
  primary key (Id) using index  
  tablespace GLOBUS_INDEX;      

Create Table Niff_Tor_GrupoPartidas (Id Number Not Null,
                                     IdTorneio
                                     Grupo Number Not Null
                                     PreEliminatoria Varchar2(1) Default 'N' Not Null,
                                     IdColaborador1 Number Not Null,
                                     IdColaborador2 Number Not Null,
                                     IdColaborador3 Number Null,
                                     IdColaborador4 Number Null) Tablespace Globus_Table;
                                     
Alter Table Niff_Tor_GrupoPartidas Add Constraint PK_Niff_TorGrupoPartidas
  primary key (Id) using index  
  tablespace GLOBUS_INDEX;      

Create Table Niff_Tor_Partidas (Id Number Not Null,
                                IdGrupo Number Not Null,
                                Data Date Not Null,
                                IdColaborador Not Null, 
                                Round1 Number Default 0,
                                Round2 Number Default 0, 
                                Round3 Number Default 0, 
                                Round4 Number Default 0,
                                QVitorias Number Default 0) Tablespace Globus_Table;
                                     
Alter Table Niff_Tor_Partidas Add Constraint PK_Niff_TorPartidas
  primary key (Id) using index   
  tablespace GLOBUS_INDEX;      
                               
-- Alterado 18/07/2018
Alter Table niff_tor_grupopartidas Add separarPorSexo varchar2(1) Default 'N' Not Null;

-- Alterador 19/07/2018
Alter Table niff_bib_livros Add Ativo Varchar2(1) Default 'S' Not Null;

-- Alterado 20/07/2018
Alter Table niff_fis_arquivei Add liberado Varchar2(1) Default 'N' Not Null;
Alter Table niff_fis_arquivei Add observacaoDoFiscal Varchar2(4000) Null;

-- Alterado 23/07/2018
Alter Table niff_tor_grupopartidas Add Sexo Varchar2(1);

-- Alterado 26/07/2018
Create Table niff_fis_ImportandoArquivei (Id Number Not Null, 
                                          Importando Varchar2(1) Default 'N' Not Null,
                                          Data Date Not Null,
                                          DataFinalizado Date Not Null,
                                          IdUsuario Number Not Null,
                                          Arquivo Varchar2(1000) Not Null) Tablespace Globus_Table;
                                          
-- Alter 03/08/2018
Alter Table niff_tor_partidas Drop Column IdGrupo;
Alter Table niff_tor_partidas Drop Column QVitorias;
Alter Table niff_tor_partidas Add IdEmpresa Number;
Alter Table niff_tor_partidas Add IdTorneio Number;
Alter Table niff_tor_partidas Add Total Number;  

-- Alterado 06/08/2018
Alter Table niff_tor_partidas Add NomePartida Varchar2(100);

-- Alterado 07/08/2018
Alter Table Niff_Tor_Partidas Add DataOriginal Date;

-- Alterado 20/08/2018
Create Table NIFF_Chm_Favoritos (Id Number Not Null,
                                IdUsuario Number Not Null,
                                NomeMenu Varchar2(255) Not Null,
                                NameMenu Varchar2(255) Not Null,
                                DataFavoritou Date Not Null) Tablespace Globus_Table;
                                
Alter Table NIFF_Chm_Favoritos Add Constraint Pk_NIFFFavoritos
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE NIFF_Chm_Favoritos
       ADD  ( CONSTRAINT fk_favorito_Usuario 
              FOREIGN KEY (IdUsuario)
                             REFERENCES NIFF_CHM_USUARIOS (IdUsuario));
                                    
ALTER TABLE NIFF_Chm_Favoritos Add MenuPai Varchar2(255);                                    

-- Alterado 21/08/2018
Alter Table niff_ads_metasdocargo Add Peso Number;

-- Alterado 23/08/2018
Alter Table Niff_Ads_Metas Add UsaParaAvaliacao Varchar2(1) Default 'S' Not Null;
Alter Table Niff_Ads_Metas Add UsaParaGestao Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Ads_Metas Add PrevistoCalcularPor Varchar2(2) Default 'MA' Not Null;
Alter Table Niff_Ads_Metas Add PrevistoQtdMes Number Default 1 Not Null;
Alter Table Niff_Ads_Metas Add PrevistoAplicaDiasUteis Varchar2(1) Default 'S' Not Null;
Alter Table Niff_Ads_Metas Add PrevistoPermiteAlterar  Varchar2(1) Default 'S' Not Null;
Alter Table Niff_Ads_Metas Add RealizadoPermiteAlterar  Varchar2(1) Default 'S' Not Null;
Alter Table Niff_Ads_Metas Add UsaKmRodado Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Ads_Metas Add UsarColunaPrevistoParaCalculo Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Ads_Metas Add IdBI Number;

Create Table Niff_Ads_BI (Id Number Not Null,
                          Descricao Varchar2(255) Not Null,
                          Ativo Varchar2(1) Default 'S' Not Null,
                          EspecificoDeUmaConta Varchar2(1) Default 'N' Not Null) Tablespace Globus_Table;
                          
Alter Table Niff_Ads_BI Add Constraint Pk_NiffBI
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;     
                       
Create Table Niff_Ads_DetalheBI (Id Number Not Null,
                                 IdBI Number Not Null,
                                 Descricao varchar2(1000) Not Null) Tablespace Globus_table;
                                 
Alter Table Niff_Ads_DetalheBI Add Constraint Pk_NiffDetalheBI 
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;     

ALTER TABLE Niff_Ads_DetalheBI
       ADD  ( CONSTRAINT fk_DetalheBI_BI 
              FOREIGN KEY (IdBI)
                             REFERENCES Niff_Ads_BI (Id));
                                                            					   
Create Table Niff_Ads_MetasBIItens (Id Number Not Null,
                                    IdMetas Number Not Null,
                                    Tipo Varchar2(1) Not Null, -- Rubrica, Ocorrencia, Funcão, Area
                                    Codigo Number Not Null ) Tablespace Globus_table;

ALTER TABLE Niff_Ads_MetasBIItens Add Descricao Varchar2(1000);

Alter Table Niff_Ads_MetasBIItens Add Constraint Pk_NiffMetasBIItens
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;     

ALTER TABLE Niff_Ads_MetasBIItens
       ADD  ( CONSTRAINT fk_MetasBIItens_Metas 
              FOREIGN KEY (IdMetas)
                             REFERENCES Niff_Ads_Metas (IdMetas ));
                                    							 
																 


Create Table Niff_Ads_ValoresMetas (Id Number Not Null,
                                    IdMetas Number Not Null,
                                    Referencia Varchar(6) Not Null,
                                    Previsto Number Default 0,
                                    Realizado Number Default 0,
                                    IdUsuarioGerou Number Not Null,
                                    DataGerou Date Not Null,
                                    IdUsuarioEditou Number Null,
                                    DataEditou Date Null,
                                    AplicouContrato Varchar2(1) Default 'N' ) Tablespace Globus_Table;
                                    
Alter Table Niff_Ads_ValoresMetas Add Constraint Pk_NiffValoresMetas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;     
  
ALTER TABLE Niff_Ads_ValoresMetas
       ADD  ( CONSTRAINT fk_ValorMetas_Usuario1 
              FOREIGN KEY (IdUsuarioGerou)
                             REFERENCES NIFF_CHM_USUARIOS (IdUsuario));
                                                                            
ALTER TABLE Niff_Ads_ValoresMetas
       ADD  ( CONSTRAINT fk_ValorMetas_Usuario2
              FOREIGN KEY (IdUsuarioEditou)
                             REFERENCES NIFF_CHM_USUARIOS (IdUsuario));
                                                                            
ALTER TABLE Niff_Ads_ValoresMetas
       ADD  ( CONSTRAINT fk_ValorMetas_Metas
              FOREIGN KEY (IdMetas)
                             REFERENCES NIFF_ADS_METAS (IDMETAS));                                                                        
                                            
Alter Table Niff_Ads_ValoresMetas Add MotivoEdicao Varchar2(2000);
Alter Table Niff_Ads_ValoresMetas Add PrevistoOriginal Number;
Alter Table Niff_Ads_ValoresMetas Add RealizadoOriginal Number;
Alter Table niff_ads_valoresmetas Add IdEmpresa Number Not Null;											

-- Alterado 30/08/2018
Alter Table niff_fis_arquivei Add Conferido Varchar2(1) Default 'N' Not Null;
Alter Table niff_fis_arquivei Add DataConferido Date;

-- Alterado 04/09/2018
Alter Table niff_ads_valoresmetas Add MotivoEdicaoReal Varchar2(2000);

-- Alterado 05/09/2018
Alter Table niff_ads_valoresmetas Add QuantidadeDiasUteis Number;

-- Alterado 06/09/2018
Alter Table Niff_Bib_Emprestimo Add Pontuacao Number;

-- Alterado 10/09/2018
Alter Table niff_ads_metas Add QtdeDecimais Number(1) Default 2;

Create Table Niff_ads_CalculoMetas (Id Number Not Null,
                                    IdEmpresa Number Not Null,
                                    IdMetas Number Not Null,
                                    Referencia Varchar2(6) Not Null,
                                    Percentual Number,
                                    Aumentou Varchar2(1) Default 'N',
                                    UsouDiasUteis Varchar2(1) Default 'N',
                                    UsouDiasCorridos Varchar2(1) Default 'N',
                                    UsouPrevisto Varchar2(1) Default 'N',
                                    UsouRealizado Varchar2(1) Default 'N',
                                    PermitiuAlterar Varchar2(1) Default 'N',
                                    ValorCalculado Number,
                                    ValorResultado Number,
                                    ValorResultadoOriginal Number ) Tablespace Globus_table;

Alter Table Niff_Ads_CalculoMetas Add Constraint Pk_NiffCalculoMetas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;      

ALTER TABLE Niff_Ads_CalculoMetas
       ADD  ( CONSTRAINT fk_Metas_CalculoMetas 
              FOREIGN KEY (IdMetas)
                             REFERENCES NIFF_ADS_METAS (IDMETAS));

ALTER TABLE Niff_Ads_CalculoMetas
       ADD  ( CONSTRAINT fk_Empresa_CalculoMetas 
              FOREIGN KEY (IdEmpresa)
                             REFERENCES Niff_Chm_Empresas (idEmpresa));


Create Table Niff_Ads_RefCalculoMetas (Id Number Not Null,
                                       IdCalculo Number Not Null,
                                       IdValorMetas Number Not Null,
                                       Previsto Number,
                                       Realizado Number,
                                       DiasUteis Number,
                                       DiasCorridos Number) Tablespace Globus_table;
                                       
Alter Table Niff_Ads_RefCalculoMetas Add Constraint Pk_NiffRefCalculoMetas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;     
                                       
ALTER TABLE Niff_Ads_RefCalculoMetas
       ADD  ( CONSTRAINT fk_ValorMetas_refCalculoMetas
              FOREIGN KEY (IdValorMetas)
                             REFERENCES Niff_Ads_Valoresmetas (Id));
                                                                            
ALTER TABLE Niff_Ads_RefCalculoMetas
       ADD  ( CONSTRAINT fk_CalculoMetas_refCalcMetas 
              FOREIGN KEY (IdCalculo)
                             REFERENCES Niff_Ads_CalculoMetas (Id));
                                                                            
                                       
Alter Table Niff_ads_CalculoMetas Add DataGerou Date Not Null;
Alter Table Niff_ads_CalculoMetas Add idusuariogerou Number Not Null;
Alter Table Niff_ads_CalculoMetas Add dataeditou Date Not Null;
Alter Table Niff_ads_CalculoMetas Add idusuarioeditou Number Not Null;
Alter Table Niff_ads_CalculoMetas Add MotivoEdicao Varchar2(2000)
Alter Table Niff_Ads_Valoresmetas Add DiasCorridos Number;                        


-- Alterado 13/09/2018
Alter Table niff_bib_livros Add Fisico Varchar2(1) Default 'S' Not Null;
Alter Table niff_bib_livros Add EBook Varchar2(1) Default 'N' Not Null;
Alter Table niff_bib_livros Add AudioBook Varchar2(1) Default 'N' Not Null;
Alter Table niff_bib_livros Add LocalDeArmazenamento Varchar2(500);
Alter Table niff_bib_livros Add QDownLoad Number Default 0;
Alter Table niff_bib_livros Add NomeArquivo Varchar2(500);

Create Table niff_bib_eBook (IdLivros Number Not Null,
                             Arquivo Blob) Tablespace Globus_Table;
                             
Alter Table niff_bib_eBook Add Constraint Pk_niffeBook
  primary key (IdLivros) using index   
  tablespace GLOBUS_INDEX;     
 
ALTER TABLE niff_bib_eBook   
       ADD  ( CONSTRAINT fk_Livro_EBook
              FOREIGN KEY (IdLivros)   
                             REFERENCES Niff_Bib_Livros (IdLivros));                                        
                             
Create Table niff_bib_Leitura (Id Number Not Null,
                               IdLivros Number Not Null,
                               IdColaborador Number Not Null,
                               ParouNaPagina Number Default 0) Tablespace Globus_Table;
                             
Alter Table niff_bib_Leitura Add Constraint Pk_niffLeitura
  primary key (IdLivros) using index   
  tablespace GLOBUS_INDEX;     
 
ALTER TABLE niff_bib_Leitura   
       ADD  ( CONSTRAINT fk_Livro_Leitura
              FOREIGN KEY (IdLivros)   
                             REFERENCES Niff_Bib_Livros (IdLivros));      
                             
ALTER TABLE niff_bib_Leitura   
       ADD  ( CONSTRAINT fk_Colaborador_Leitura
              FOREIGN KEY (IdColaborador)   
                             REFERENCES Niff_Ads_Colaboradores (IdColaborador));                                   
							 
Alter Table niff_bib_Leitura Add EfetuouDownLoad Varchar2(1) Default 'N' Not Null;
Alter Table niff_bib_Leitura Add DataDownload Date ;
Alter Table niff_bib_Leitura Add UltimoAcesso Date ;
Alter Table niff_bib_Leitura Add TotalPaginas Number ;
Alter Table niff_bib_emprestimo Add Ebook Varchar2(1) Default 'N';

-- Alterado 19/09/2018
Alter Table Niff_Chm_Usuarios Add AcessaBSC Varchar2(1) Default 'N' Not Null;

Create Table Niff_Cur_Candidatos (Id Number Not Null,
                                  Nome Varchar2(255) Not Null,
                                  Telefone Number Not Null,
                                  Celular Number Null,
                                  Email Varchar2(255) Not Null,
                                  Indicado Varchar2(1) Default 'N' Not Null,
                                  Catho Varchar2(1) Default 'N' Not Null,
                                  Infojobs  Varchar2(1) Default 'N' Not Null,
                                  Outros Varchar2(1) Default 'N' Not Null,
                                  DescricaoOutros Varchar2(255) Null,
                                  Empregado Varchar2(1) Default 'N' Not Null) Tablespace Globus_Table;
                                  
Alter Table Niff_Cur_Candidatos Add Constraint Pk_NiffCandidatos
  primary key (Id) using index   
  tablespace GLOBUS_INDEX;     

Create Table Niff_Cur_Vagas (Id Number not Null,
                             Descricao Varchar2(255) Not Null,
                             Abertura Date Not Null,
                             Anunciada Varchar2(1) Not Null, -- (Catho, Infojob, Linkedin, outros)
                             Encerramento Date Null,
                             IdEmpresa Number Not Null,
                             IdCandidato Number Null, -- candidato contratado
                             Status Varchar2(1) Not null) Tablespace Globus_table;
                             
Alter Table Niff_Cur_Vagas Add Constraint Pk_NiffVagas
  primary key (Id) using index   
  tablespace GLOBUS_INDEX;     
 
ALTER TABLE Niff_Cur_Vagas   
       ADD  ( CONSTRAINT fk_Empresa_Vagas
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));      
                             
ALTER TABLE Niff_Cur_Vagas   
       ADD  ( CONSTRAINT fk_Candidato_Vagas
              FOREIGN KEY (IdCandidato)   
                             REFERENCES Niff_Cur_Candidatos (Id));      
                             
Create Table Niff_Cur_ArqCandidatos (Id Number Not Null,
                                     IdCandidato Number Not Null,
                                     Tipo Varchar2(1) Default 'C' Not Null,
                                     Arquivo Blob ) Tablespace Globus_Table;
                                     
Alter Table Niff_Cur_ArqCandidatos Add Constraint Pk_NiffArqCandidato
  primary key (Id) using index   
  tablespace GLOBUS_INDEX;     

ALTER TABLE Niff_Cur_ArqCandidatos   
       ADD  ( CONSTRAINT fk_Candidato_ArqCandidato
              FOREIGN KEY (IdCandidato)   
                             REFERENCES Niff_Cur_Candidatos (Id));      
                             
Create Table Niff_Cur_CandidatosVaga (Id Number Not Null,
                                      IdVaga Number Not Null,
                                      IdCandidato Number Not Null) Tablespace Globus_Table;
                                      
Alter Table Niff_Cur_CandidatosVaga Add Constraint Pk_NiffCandidatosVaga
  primary key (Id) using index   
  tablespace GLOBUS_INDEX;     

ALTER TABLE Niff_Cur_CandidatosVaga   
       ADD  ( CONSTRAINT fk_Candidato_CandidatosVaga
              FOREIGN KEY (IdCandidato)   
                             REFERENCES Niff_Cur_Candidatos (Id));      

ALTER TABLE Niff_Cur_CandidatosVaga   
       ADD  ( CONSTRAINT fk_Vaga_CandidatosVaga
              FOREIGN KEY (IdVaga)   
                             REFERENCES Niff_Cur_Vagas (Id));      
                             
Create Table Niff_Cur_HistoricoCandidato (Id Number Not Null,
                                          IdCandVaga Number Not Null,
                                          Status Varchar2(2) Not Null,
                                          Motivo Varchar2(4000) Null) Tablespace Globus_Table;
                                          
Alter Table Niff_Cur_HistoricoCandidato Add Constraint Pk_NiffHistCandidato
  primary key (Id) using index   
  tablespace GLOBUS_INDEX;     

ALTER TABLE Niff_Cur_HistoricoCandidato   
       ADD  ( CONSTRAINT fk_CandidatoVaga_HistCandidato 
              FOREIGN KEY (IdCandVaga)   
                             REFERENCES Niff_Cur_CandidatosVaga (Id));      

-- Alterado 20/09/2018
Alter Table niff_cur_vagas Drop Column Anunciada;
Alter Table niff_cur_vagas Add Catho Varchar2(1) Default 'N' Not Null;
Alter Table niff_cur_vagas Add InfoJobs Varchar2(1) Default 'N' Not Null;
Alter Table niff_cur_vagas Add linkeIn Varchar2(1) Default 'N' Not Null;
Alter Table niff_cur_vagas Add Outros Varchar2(1) Default 'N' Not Null;
Alter Table niff_cur_vagas Add OutroLocalDeAnuncio Varchar2(255) Default 'N' Not Null;

-- Alterado 24/09/2018
Alter Table Niff_Cur_Candidatos Add LinkedIn Varchar2(1) Default 'N' Not Null;

-- Alterado 25/09/2018
Alter Table niff_cur_arqcandidatos Add extensao Varchar2(5) ;

-- Alterado 26/09/2018
Alter Table niff_ads_valoresmetas Add FeriasBase Number Default 0;
Alter Table niff_ads_valoresmetas Add PlrPrevisto Number Default 0;
Alter Table niff_ads_valoresmetas Add PlrRealizado Number Default 0;
Alter Table niff_ads_valoresmetas Add Dissidio Number Default 0;
Alter Table niff_ads_valoresmetas Add QtdFeriado Number Default 0;
Alter Table Niff_Ads_Calculometas Add FeriasBase Number Default 0;
Alter Table Niff_Ads_Calculometas Add PlrPrevisto Number Default 0;
Alter Table Niff_Ads_Calculometas Add Dissidio Number Default 0;
Alter Table Niff_Ads_Calculometas Add QtdFeriado Number Default 0;

-- Alterado 01/10/2018
Alter Table niff_cur_historicocandidato Add Data Date Not Null;
Alter Table niff_cur_historicocandidato Drop Column IdCandVaga;
Alter Table niff_cur_historicocandidato Add IdVaga Number Null;
Alter Table niff_cur_historicocandidato Add IdCandidato Number Not Null;

ALTER TABLE Niff_Cur_HistoricoCandidato   
       ADD  ( CONSTRAINT fk_Candidato_HistCandidato 
              FOREIGN KEY (IdCandidato)   
                             REFERENCES Niff_Cur_Candidatos (Id));     

ALTER TABLE Niff_Cur_HistoricoCandidato   
       ADD  ( CONSTRAINT fk_Vaga_HistCandidato 
              FOREIGN KEY (IdVaga)   
                             REFERENCES Niff_Cur_Vagas (Id));     

-- Alterado 02/10/2018
Alter Table niff_cur_candidatosvaga Add Data Date;
Alter Table niff_cur_historicocandidato Add DataEntrevista Date;
Alter Table niff_cur_vagas Add Confidencial Varchar2(1) Default 'N' Not Null;
Alter Table niff_cur_vagas Add Detalhamento Varchar2(4000) Null;

-- Aterado 03/10/2018
Alter Table niff_cur_vagas Add IdEmpresaEnrevista Number;
Alter Table niff_cur_vagas Add Endereco Varchar2(1000);
Alter Table niff_cur_vagas Add InformacoesGerais Varchar2(2000);

-- Alterado 06/10/2018
Alter Table niff_chm_usuarios Add ACESSAMETASFINANCEIRAS Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add ACESSAMETASOPERACIONAIS Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add PERMITEBUSCARRESULTADO Varchar2(1) Default 'N' Not Null;

Alter Table Niff_Bib_Resenha Add TemResenha Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Bib_Resenha Add TemPerguntas Varchar2(1) Default 'N' Not Null;

-- Alterado 29/10/2018
Alter Table niff_chm_histochamado Add Privado Varchar2(1) Default 'N';
Alter Table niff_chm_chamado Add idchamadoAssociado Number Null;

-- Alterado 31/10/2018
Alter Table Niff_Chm_Usuarios Add PermiteAlterarBSC Varchar2(1) Default 'N' Not Null;

-- Alterado 05/11/2018
Alter Table niff_chm_notifchamado Add IDHISTORICO Number Not Null;
Alter Table niff_chm_notifchamado Add Data Date Not Null;

-- Alterado 06/11/2018
Create Table niff_pto_Horario (IdHorario Number Not Null,
                               IdUsuario Number Not Null, 
                               Data Date Not Null,
                               Entrada Date Null,
                               SaidaAlmoco Date Null,
                               RetornoAlmoco Date Null,
                               Saida Date Null) Tablespace Globus_Table;
                               
                 
Alter Table niff_pto_Horario Add Constraint pk_niffptoHorario
  primary key (IdHorario) using index   
  tablespace GLOBUS_INDEX;     

ALTER TABLE niff_pto_Horario   
       ADD  ( CONSTRAINT fk_usuario_Horario
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                    

Alter Table niff_chm_chamado Add AtendenteFoiCortez Varchar2(1) Default 'N' Not Null; 
Alter Table niff_chm_chamado Add DataAvaliacaoDoSolicitante Date;
Alter Table niff_chm_chamado Add SolicitanteAbriuCorretamente Varchar2(1) Default 'N';
Alter Table niff_chm_chamado Add SolicitanteRespDentroDePrazo Varchar2(1) Default 'N';
Alter Table niff_chm_chamado Add SolicitanteFoiCortez Varchar2(1) Default 'N';
Alter Table niff_chm_chamado Add AvaliacaoSolicitante Number(1) Null;
Alter Table niff_chm_chamado Add DescricaoAvaliacaoSolic Varchar2(2000) Null;

-- Alterado 07/11/2018
Alter Table niff_chm_chamado Add PrazoDesenvolvimento Number Default 0;

-- Alterado 13/11/2018
Alter Table niff_chm_usuarios Add TemaBlackSelecionado Varchar2(1) Default 'N';

-- Alterado 14/11/2018
Alter Table niff_pto_horario Add IdColaborador Number;

-- Alterado 16/11/2018
Alter Table Niff_Pto_Horario Add Extra Number;
Alter Table Niff_Pto_Horario Add Incompleta Number;
Alter Table Niff_Pto_Horario Add Atestado Varchar2(1) Default 'N';
Alter Table Niff_Pto_Horario Add Declaracao Varchar2(1) Default 'N';

-- Alterado 05/12/2018
Create Table Niff_Ope_Indicadores (Id Number Not Null,
                                   Ativo Varchar2(1) Default 'N' Not Null,
                                   Descricao Varchar2(300) Not Null,
                                   Valores Varchar2(1) Default 'A' Not Null ) Tablespace Globus_table;
                                   
Alter Table Niff_Ope_Indicadores Add Constraint Pk_NIFFOpeIndicadores 
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 
                                   
Create Table Niff_Ope_Setor (Id Number Not Null,
                             Ativo Varchar2(1) Default 'N' Not Null, 
                             Descricao Varchar2(300) Not Null,
                             Calculo Varchar2(1) Default 'P' Not Null) Tablespace Globus_Table;
                                                                 
Alter Table Niff_Ope_Setor Add Constraint Pk_NIFFOpeSetor
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                               
    
Create Table Niff_Ope_SetorLinhas (Id Number Not Null,
                                   IdSetor Number Not Null, 
                                   CodIntLinha Number Not Null, 
                                   Vigencia Date Not Null ) Tablespace Globus_Table;
                                                                                                 
Alter Table Niff_Ope_SetorLinhas Add Constraint Pk_NIFFOpeSetorLinha
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                               
                        
ALTER TABLE Niff_Ope_SetorLinhas   
       ADD  ( CONSTRAINT fk_Setor_SetorLinha
              FOREIGN KEY (IdSetor)   
                             REFERENCES Niff_Ope_Setor (Id));     
                                                                    
Create Table Niff_Ope_Metas (Id Number Not Null,
                             IdIndicador Number Not Null,
                             Ativo Varchar2(1) Default 'N' Not Null,
                             Peso Number Default 0 ) Tablespace Globus_Table;

Alter Table Niff_Ope_Metas Add Constraint Pk_NIFFOpeMetas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                               
                        
ALTER TABLE Niff_Ope_Metas   
       ADD  ( CONSTRAINT fk_Indicador_Metas
              FOREIGN KEY (IdIndicador)   
                             REFERENCES Niff_Ope_Indicadores (Id));     
                                                                        
Alter Table Niff_Ope_Indicadores Add Abreviado Varchar2(20) Null;
Alter Table Niff_Ope_Setor Add IdEmpresa Number Not Null;

--Alterado 10/12/2018
Alter Table Niff_Ope_Metas Add IdEmpresa Number Not Null;
Alter Table Niff_Ope_Metas Add Vigencia Date Not Null;

Create Table Niff_Ope_Pontuacao (Id Number Not Null,
                                 IdEmpresa Number Not Null,
                                 Codigo Number Not Null,
                                 Descricao Varchar2(300) Not Null,
                                 Ativo Varchar2(1) Default 'N' Not Null ) Tablespace Globus_table;
                                 
Alter Table Niff_Ope_Pontuacao Add Constraint Pk_NIFFOpePontuacao
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                               
                                 
ALTER TABLE Niff_Ope_Pontuacao   
       ADD  ( CONSTRAINT fk_Empresa_Pontuacao
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));     

ALTER TABLE Niff_Ope_Metas   
       ADD  ( CONSTRAINT fk_Empresa_MetasIndic
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                      


Create Table Niff_Ope_VigenciaPontuacao (Id Number Not Null,
                                         idPontuacao Number Not Null,
                                         Vigencia Date Not Null,
                                         Inicio Number,
                                         Fim Number ) Tablespace Globus_table;
                                         
Alter Table Niff_Ope_VigenciaPontuacao Add Constraint Pk_NIFFOpeVigPont
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                               
                                 
ALTER TABLE Niff_Ope_VigenciaPontuacao   
       ADD  ( CONSTRAINT fk_Pontuacao_VigenciaPont
              FOREIGN KEY (IdPontuacao)   
                             REFERENCES Niff_Ope_Pontuacao (Id));                                      
                                         
-- Alterado 11/12/2018
Create Table Niff_Ope_Valores (Id Number Not Null,
                                    IdEmpresa Number Not Null,
                                    Data Date Not Null,
                                    IdIndicador Number Not Null,
                                    Periodo Varchar2(1) Null, 
                                    CodIntLinha Number Not Null,
                                    Programado Number Default 0,
                                    Realizado Number Default 0,
                                    Pontuacao Number Default 0) Tablespace Globus_Table;
                                    
Alter Table Niff_Ope_Valores Add Constraint Pk_NIFFOpeValores
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                               
                                  
ALTER TABLE Niff_Ope_Valores   
       ADD  ( CONSTRAINT fk_Empresa_Valores
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                      
                                         
ALTER TABLE Niff_Ope_Valores   
       ADD  ( CONSTRAINT fk_Indicador_Valores
              FOREIGN KEY (IdIndicador)   
                             REFERENCES Niff_Ope_Indicadores (Id));                                      
                                                                                                                      
-- Alterado 12/12/2018
Alter Table niff_ope_setorlinhas Add TemCobrador Varchar2(1) Default 'N';

Create Table Niff_Ope_Linhas (Id Number Not Null,
                              CodIntLinha Number Not Null,
                              CodIntLinha2 Number Not null) Tablespace Globus_table;
                                   
Alter Table Niff_Ope_Linhas Add Constraint Pk_NiffOpeLinhas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                               
                                                   
-- Aterado 13/12/2018
Alter Table niff_chm_usuarios Add SoChamadosDesseUsuario Varchar2(1) Default 'N';

-- Alterado 14/12/2018
Alter Table niff_chm_histochamado Add Adequacao Varchar2(50) Null;
Alter Table niff_chm_histochamado Add Prazo Number Null;

  
-- Alterado 26/12/2018
Alter Table niff_chm_usuarios Add AcessaRecebedoria Varchar2(1) Default 'N';
Alter Table niff_chm_usuarios Add PodeExportarSigomExcel Varchar2(1) Default 'N';
Alter Table niff_chm_usuarios Add AcessaOperacional Varchar2(1) Default 'N';
Alter Table niff_chm_usuarios Add AcessaCadastroOperacional Varchar2(1) Default 'N';
Alter Table niff_chm_usuarios Add AcessaDemonstrativo Varchar2(1) Default 'N';
Alter Table niff_chm_usuarios Add AcessaIQO Varchar2(1) Default 'N';

-- Alterado 07/01/2019
Alter Table Niff_Ope_Metas Add Meta Number;

-- Alterado 08/01/2019
Alter Table Niff_Chm_Chamado Add LembrarDentreDeDias Number Default 0;
Alter Table Niff_Chm_Chamado Add MotivoLembrete Varchar2(200);
Alter Table Niff_Chm_Histochamado Add IdUsuarioAutorizacao Number;
Alter Table Niff_Chm_Histochamado Add MotivoAutorizacao Varchar2(200);
Alter Table Niff_Chm_Histochamado Add Autorizado Varchar2(1) Default 'N'; 
Alter Table Niff_Chm_Histochamado Add DataSolicitacaoAutorizacao Date;
Alter Table niff_chm_usuarios Add PodeFinalizarChamado Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add AssinaturaChamado Varchar2(300);

-- Alterado 09/01/2019
Create Table Niff_Rec_SIGOM (Id Number Not Null,
                             CodigoLinha Varchar2(15) Not Null,
                             Prefixo Varchar2(10) Not Null,
                             InicioJornadaGlobus Date,
                             InicioJornadaSigom Date,
                             FimJornadaGlobus Date,
                             FimJornadaSigom Date,
                             ResponsavelGlobus Varchar2(255),
                             ResponsavelSigom Varchar2(255),
                             IdTipoPagtoGlobus Varchar2(5),
                             IdTipoPagtoSigom Varchar2(5),
                             TipoPagtoGlobus Varchar2(255),
                             TipoPagtoSigom Varchar2(255),                             
                             QuantidadeGlobus Number,
                             QuantidadeSigom Number,                             
                             ValorGlobus Number,
                             ValorSigom Number,                             
                             GuiaGlobus Varchar2(100),
                             CodigoImportacaoGlobus Number,
                             DiferencaQuantidade Varchar2(1) Default 'N' Not Null,
                             DiferencaValor Varchar2(1) Default 'N' Not Null ) Tablespace Globus_table;
                             
Alter Table Niff_Rec_SIGOM Add Constraint Pk_NiffrecSigom
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                

-- Alterado 11/01/2019 
Alter Table niff_rec_sigom Add IdEmpresa Number;

-- Alterado 12/01/2019
Alter Table Niff_Pto_Horario Add Compensacao Varchar2(1) Default 'N';

-- Alterado 16/01/2019
Alter Table niff_chm_chamado Add AvaliouNoPrazoSolicitante Varchar2(1) Default 'S';
Alter Table niff_chm_chamado Add AvaliouNoPrazoAtendente Varchar2(1) Default 'S';
Alter Table niff_chm_usuarios Add DataAdmissao Date;

Create Table niff_ope_Avaliacao (Id Number Not Null,
                                 IdIndicador Number Not Null,
                                 Referencia Varchar2(6) Not Null,
                                 Meta Number,
                                 Valor Number,
                                 Pontuacao Number) Tablespace Globus_Table; 
                                 
Alter Table niff_ope_Avaliacao Add Constraint Pk_NiffOpeAvaliacao
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                

ALTER TABLE niff_ope_Avaliacao   
       ADD  ( CONSTRAINT fk_Indicador_Avaliacao
              FOREIGN KEY (IdIndicador)   
                             REFERENCES Niff_Ope_Indicadores (Id));                                      
                                                                                                                      
-- Alterado 18/01/2019
Alter Table Niff_Chm_Histochamado Add IdUsuarioAutorizado Number;
Alter Table Niff_Chm_Histochamado Add Autorizado Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Chm_Histochamado Add AguardarRetornoAuto Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Chm_Histochamado Add MotivoAutorizacao Varchar2(200);

-- Alterado 21/01/2019
Alter Table Niff_Chm_Chamado Add Reavaliar Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Chm_Chamado Add Reavaliado Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Chm_Chamado Add DataReavaliacao Date;

-- Alterado 23/01/2019
Alter Table niff_chm_chamado Add IdUsuarioAcompanhamento Number;

-- Alterado 24/01/2019
Create Table Niff_Chm_LembreteChamados (Id Number not Null, 
                                        IdChamado Number Not Null,
                                        Data Date) Tablespace Globus_Table;                                        
                                                                        
Alter Table Niff_Chm_LembreteChamados Add Constraint Pk_NiffLembreteChamados
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                

ALTER TABLE Niff_Chm_LembreteChamados   
       ADD  ( CONSTRAINT fk_Chamado_Lembrete
              FOREIGN KEY (IdChamado)   
                             REFERENCES Niff_Chm_Chamado (IdChamado));       
                                        
-- Alterado 01/02/2019
Alter Table niff_ads_valoresmetas Add DataCorteFinanceiro Date;
Alter Table niff_ads_valoresmetas Add DataCorteOperacional Date;
Alter Table niff_chm_usuarios Add AlteraBSCIndicadoresManuais Varchar2(1) Default 'N';

-- Alterado 08/02/2019 -- Fluxo de Caixa
Create Table NIFF_FIN_Colunas (Id Number Not Null,
                               Nome Varchar2(100) Not Null,
                               Tipo Varchar2(2) Default 'EN' Not Null,
                               Transferencia Varchar2(1) Default 'B' Not Null) Tablespace Globus_Table;
                               
Alter Table Niff_Fin_Colunas Add Constraint Pk_NIFFColunas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                  

Create Table NIFF_FIN_Bancos (Id Number Not Null,
                              IdEmpresa Number Not Null,
                              Nome Varchar2(255) Not Null,
                              SaldoInicial Number Default 0) Tablespace Globus_Table;

Alter Table NIFF_FIN_Bancos Add Constraint Pk_NIFFBancos
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE NIFF_FIN_Bancos   
       ADD  ( CONSTRAINT fk_Empresa_Bancos
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));       
                              
Create Table NIFF_FIN_ColunasBanco (Id Number Not Null,
                                    IdBanco Number Not Null,
                                    IdColuna Number Not Null) Tablespace Globus_Table;
                                    
Alter Table NIFF_FIN_ColunasBanco Add Constraint Pk_NIFFColBancos
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 
                                    
ALTER TABLE NIFF_FIN_ColunasBanco   
       ADD  ( CONSTRAINT fk_Bancos_ColBancos
              FOREIGN KEY (IdBanco)   
                             REFERENCES NIFF_FIN_Bancos (Id));       

ALTER TABLE NIFF_FIN_ColunasBanco   
       ADD  ( CONSTRAINT fk_Colunas_ColBancos
              FOREIGN KEY (IdColuna)   
                             REFERENCES NIFF_FIN_Colunas (Id));       

Create Table NIFF_FIN_DespRecColunas (Id Number Not Null,
                                      IdColBanco Number Not Null,
                                      CodTpDespesa Varchar2(5) Null,
                                      CodTpReceita Varchar2(5) Null) Tablespace Globus_table;                                                                      
                                      
Alter Table NIFF_FIN_DespRecColunas Add Constraint Pk_NIFFDRColuna
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 
                             
ALTER TABLE NIFF_FIN_DespRecColunas   
       ADD  ( CONSTRAINT fk_ColBancos_DRColunas
              FOREIGN KEY (IdColBanco)   
                             REFERENCES NIFF_FIN_ColunasBanco (Id));    
                             
Create Table NIFF_FIN_Variaveis (Id Number Not Null,
                                 IdEmpresa Number Not Null,
                                 IdColuna Number Not Null,
                                 CalculoMediaPor Varchar2(1) Default 'M' Not Null,
                                 QtdeRetroativo Number Not Null,
                                 FeriadoPorAno Varchar2(1) Default 'S' Not Null,
                                 ConsideraEmendaSadado Varchar2(1) Default 'N' Not Null,
                                 FeriaApenasDinheiro Varchar2(1) Default 'N' Not Null,
                                 Reduzir Varchar2(1) Default 'N' Not Null,
                                 Aumentar Varchar2(1) Default 'N' Not Null,
                                 PercentualReduzir Number Default 0,
                                 PercentualAumentar Number Default 0 ) Tablespace Globus_table;
                                 
Alter Table NIFF_FIN_Variaveis Add Constraint Pk_NIFFVariaveis 
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE NIFF_FIN_Variaveis   
       ADD  ( CONSTRAINT fk_Empresa_Variaveis
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));       
                              
ALTER TABLE NIFF_FIN_Variaveis   
       ADD  ( CONSTRAINT fk_Colunas_Variaveis
              FOREIGN KEY (IdColuna)   
                             REFERENCES NIFF_FIN_Colunas (Id));       

Create Table NIFF_FIN_Demonstrativo (Id Number Not Null, 
                                     IdEmpresa Number Not Null,
                                     IdBanco Number Not Null,
                                     IdColuna Number Not Null,
                                     Data Date Not Null,
                                     Previsto Number Default 0,
                                     Realizado Number Default 0,
                                     MotivoAlteracaoPrevisto Varchar2(4000),
                                     IdUsuarioAlterou Number  )  Tablespace Globus_Table;
                                     
Alter Table NIFF_FIN_Demonstrativo Add Constraint Pk_NIFFdemonstrativo
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                   
                                     
ALTER TABLE NIFF_FIN_Demonstrativo   
       ADD  ( CONSTRAINT fk_Empresa_Demonstrativo
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));       
                              
ALTER TABLE NIFF_FIN_Demonstrativo   
       ADD  ( CONSTRAINT fk_Colunas_Demonstrativo
              FOREIGN KEY (IdColuna)   
                             REFERENCES NIFF_FIN_Colunas (Id));       

ALTER TABLE NIFF_FIN_Demonstrativo   
       ADD  ( CONSTRAINT fk_Banco_Demonstrativo
              FOREIGN KEY (IdBanco)   
                             REFERENCES NIFF_FIN_Bancos (Id));       
                                                                                                                                      
Alter Table NIFF_FIN_Demonstrativo Add DataCalculoMedia Date Not Null;
Alter Table NIFF_FIN_Demonstrativo Add DataAlteracao Date Not Null;
Alter Table NIFF_FIN_Demonstrativo Add IdUsuarioCalculoMedia Number Not Null;
Alter Table NIFF_FIN_Demonstrativo Add SaldoAnterior Number Default 0;
Alter Table NIFF_FIN_Demonstrativo Add SaldoFinal Number Default 0;

ALTER TABLE NIFF_FIN_Demonstrativo   
       ADD  ( CONSTRAINT fk_Usuario_Demonstrativo1
              FOREIGN KEY (IdUsuarioAlterou)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));       

ALTER TABLE NIFF_FIN_Demonstrativo   
       ADD  ( CONSTRAINT fk_Usuario_Demonstrativo2
              FOREIGN KEY (IdUsuarioCalculoMedia)   
                             REFERENCES NIFF_Chm_Usuarios (IdUsuario));       

-- Alterado 11/02/2019
Alter Table Niff_Chm_Usuarios Add AcessaFinanceio Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Chm_Usuarios Add AcessaCadastroFin Varchar2(1) Default 'N' Not Null;
Alter Table niff_fin_colunas Add Ativo Varchar2(1) Default 'N';
Alter Table niff_fin_bancos Add Ativo Varchar2(1) Default 'N';
Alter Table NIFF_FIN_ColunasBanco Add Ativo Varchar2(1) Default 'N';

-- Alterado 12/02/2019
Alter Table niff_fin_bancos Add Codigo Number;

-- Alterado 13/02/2019
Alter Table Niff_Fin_Variaveis Add Nome Varchar2(300);
Alter Table Niff_Fin_Variaveis Add Ativo Varchar2(1) Default 'N';
Alter Table Niff_Fin_Variaveis Add Codigo Number;

-- Alterado 20/02/2019
Drop Table NIFF_FIN_Demonstrativo;

Create Table NIFF_FIN_Demonstrativo (Id Number Not Null,
                                     IdEmpresa Number Not Null,
                                     IdBanco Number Not Null,
                                     Referencia Varchar2(6) Not Null,
                                     SaldoInicial Number Default 0 Not Null,
                                     SaldoFinal Number Default 0 Not Null) Tablespace Globus_Table;
                                     
Alter Table NIFF_FIN_Demonstrativo Add Constraint Pk_NIFFdemonstrativo
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                   

ALTER TABLE NIFF_FIN_Demonstrativo   
       ADD  ( CONSTRAINT fk_Empresa_Demonstrativo
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));       

ALTER TABLE NIFF_FIN_Demonstrativo   
       ADD  ( CONSTRAINT fk_Banco_Demonstrativo
              FOREIGN KEY (IdBanco)   
                             REFERENCES NIFF_FIN_Bancos (Id));       

Create Table NIFF_FIN_ColDemonstrativo (Id Number Not Null,
                                        IdDemonstrativo Number Not Null,
                                        IdColuna Number Not Null,
                                        Data Date Not Null,
                                        Previsto Number Default 0 Not Null,
                                        Realizado Number Default 0 Not Null) Tablespace Globus_table;
                                     
Alter Table NIFF_FIN_ColDemonstrativo Add Constraint Pk_NIFFColDemonstrativo
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                   
                                        
ALTER TABLE NIFF_FIN_ColDemonstrativo   
       ADD  ( CONSTRAINT fk_Colunas_ColDemonstrativo
              FOREIGN KEY (IdColuna)   
                             REFERENCES NIFF_FIN_Colunas (Id));       

ALTER TABLE NIFF_FIN_ColDemonstrativo   
       ADD  ( CONSTRAINT fk_Demonst_ColDemonstrativo 
              FOREIGN KEY (IdDemonstrativo)   
                             REFERENCES NIFF_FIN_Demonstrativo (Id));       

Create Table Niff_Fin_HistoDemonstrativo (Id Number Not Null,
                                          IdColDemonst Number Not Null,
                                          DataAlteracao Date Not Null,
                                          Previsto Number Default 0 Not Null,
                                          Realizado Number Default 0 Not Null,
                                          MotivoAlteracaoPrevisto Varchar2(4000) Null,
                                          MotivoAlteracaoRealizado Varchar2(4000) Null,
                                          IdUsuario Number Not Null) Tablespace Globus_Table;
                                              

     
Alter Table Niff_Fin_HistoDemonstrativo Add Constraint Pk_NiffHistoDemonstrativo
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                   
                                                                                   
ALTER TABLE Niff_Fin_HistoDemonstrativo   
       ADD  ( CONSTRAINT fk_Usuario_HistoDemonst 
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));       
                                                                                   
ALTER TABLE Niff_Fin_HistoDemonstrativo   
       ADD  ( CONSTRAINT fk_ColDemonst_HistoDemonst 
              FOREIGN KEY (IdColDemonst)   
                             REFERENCES NIFF_FIN_ColDemonstrativo (Id));       
                                                                                   
--Alterado em 21/02/2019
Create Table niff_ads_BSCEmAlteracao (Id Number Not Null,
                                              idEmpresa Number Not Null,
                                              referencia Varchar2(6) Not Null,
                                              IdUsuario Number Not Null ) Tablespace Globus_Table;

Alter Table niff_fin_coldemonstrativo Add RealizadoBCO Number Default 0;
Alter Table niff_fin_histodemonstrativo Add RealizadoBCO Number Default 0;

-- Alterado em 25/02/2019
Alter Table niff_fin_colunas Add TipoOperacaoLinha Varchar2(1) Default 'T' Not Null; -- T Todas, M Municipal ou I Intermunicipal
Alter Table niff_fin_colunas Add Origem Varchar2(3);

-- Alterado em 27/02/2019
Alter Table niff_chm_usuarios Add AcessaContabilidade Varchar2(1) Default 'N';
Alter Table niff_chm_usuarios Add AcessaEscrituracaoFiscal Varchar2(1) Default 'N';
Alter Table Niff_Fin_Histodemonstrativo Add Data Date;

-- Alterado em 28/02/2019
Alter Table niff_chm_usuarios Add AcessaRamais Varchar2(1) Default 'N';

-- Alterado em 01/03/2019
Alter Table Niff_Chm_Empresas Add Zero800 Varchar2(1) Default 'N';

-- Alterado em 07/03/2019
Alter Table niff_rec_sigom Add Tipo Varchar2(1) Default 'S' Not Null; -- S Sigom -- P Prodata

-- Alterado em 08/03/2019
Alter Table niff_chm_usuarios Add AcessaRateio Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add AcessaCadastroBenRateio Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add AcessaBeneficioRateio Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add AcessaCalculoRateioBen Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add AcessaDepartamentoPessoal Varchar2(1) Default 'N' Not Null;

-- Alterado em 11/03/2019
Create Table Niff_CTB_Param (Id Number Not Null,
                             IdEMpresa Number Not Null,
                             NroPlano Number Not Null,
                             Lote Varchar2(3) Not Null) Tablespace Globus_Table;
                                  
Alter Table Niff_CTB_Param Add Constraint Pk_NiffCtbParam
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                   
                                                                                   
ALTER TABLE Niff_CTB_Param   
       ADD  ( CONSTRAINT fk_Empresa_CtbParam
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                
                             
Create Table Niff_CTB_ParamCustos (Id Number Not Null,
                                   IdParam Number Not Null,
                                   CodCustoCTB Number Not Null) Tablespace Globus_table;                             
                                   
                                   
Alter Table Niff_CTB_ParamCustos Add Constraint Pk_NiffParamCustos
  primary key (Id) using index  
  tablespace GLOBUS_INDEX;                                   
                                                                                   
ALTER TABLE Niff_CTB_ParamCustos
       ADD  ( CONSTRAINT fk_CtbParam_CtbParamCusto
              FOREIGN KEY (IdParam)   
                             REFERENCES Niff_CTB_Param (Id));                                

                             
Create Table Niff_CTB_AssociaCustoSetor (Id Number Not Null,
                                         IdEmpresa Number Not Null,
                                         IdParam Number Not Null,
                                         CodCustoCTB Number Not Null,
                                         CodContaCTB Number Not Null,
                                         CodSetor Number Not Null) Tablespace Globus_Table;
                                                              
Alter Table Niff_CTB_AssociaCustoSetor Add Constraint Pk_NiffAssociaCusto
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                   
                                                                                   
ALTER TABLE Niff_CTB_AssociaCustoSetor   
       ADD  ( CONSTRAINT fk_Empresa_AssociaCusto
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                
                                                                                   
ALTER TABLE Niff_CTB_AssociaCustoSetor
       ADD  ( CONSTRAINT fk_CtbParamCusto_AssociaCusto
              FOREIGN KEY (IdParam)   
                             REFERENCES Niff_CTB_Param (Id));                                

Alter Table Niff_CTB_Param Add Ativo Varchar2(1) Default 'N' Not Null;

-- Alterado 12/03/2019
Alter Table niff_fin_bancos Add CodBanco Number;
Alter Table niff_fin_bancos Add CodAgencia Number;
Alter Table niff_fin_bancos Add CodConta Varchar2(15);


--Alterado 15/03/2019
Alter Table niff_fis_arquivei Add TipoArquivo Varchar2(10);

Alter Table NIFF_FIS_ItensArquivei Add ValorPis Number;
Alter Table NIFF_FIS_ItensArquivei Add AliquotaPis Number;
Alter Table NIFF_FIS_ItensArquivei Add BasePis Number;

Alter Table NIFF_FIS_ItensArquivei Add ValorCofins Number;
Alter Table NIFF_FIS_ItensArquivei Add AliquotaCofins Number;
Alter Table NIFF_FIS_ItensArquivei Add BaseCofins Number;

Alter Table NIFF_FIS_ItensArquivei Add AliquotaIPI Number;
Alter Table NIFF_FIS_ItensArquivei Add BaseIPI Number;

Alter Table NIFF_FIS_ItensArquivei Add AliquotaISS Number;
Alter Table NIFF_FIS_ItensArquivei Add BaseISS Number;
Alter Table NIFF_FIS_ItensArquivei Add ValorISS Number;

Alter Table NIFF_FIS_ItensArquivei Add AliquotaICMSST Number;

-- Alterado 18/03/2019
create table PBI_EMAILS
(
  ID_EMAIL  NUMBER not null,
  EMAIL     VARCHAR2(80),
  NOMEGRUPO VARCHAR2(50),
  NOME      VARCHAR2(50),
  SENHA     VARCHAR2(20),
  DATA      DATE,
  ATIVO     VARCHAR2(1) default 'S' not null) tablespace GLOBUS_TABLE ;

-- Create/Recreate primary, unique and foreign key constraints 
alter table PBI_EMAILS
  add constraint PK_PBI_EMAILS primary key (ID_EMAIL)
  using index 
  tablespace GLOBUS_INDEX;
 
Create Table Niff_Pbi_UsuariosPorEmail (Id Number not Null,
                                        IdEmail Number Not Null,
                                        IdUsuario Number Not null) Tablespace Globus_table;
                                        
Alter Table Niff_Pbi_UsuariosPorEmail
  add constraint PK_NiffUsuariosPorEmail primary key (Id)
  using index 
  tablespace GLOBUS_INDEX;
 
-- Aterado 19/03/2019
Create Table niff_pbi_Acessos (Id Number Not Null,
                               IdEmail Number Not Null,
                               Data Date Not Null,
                               Quantidade Number Not Null) Tablespace Globus_Table;
                               
Alter Table niff_pbi_Acessos
  add constraint PK_NiffAcessoBI primary key (Id)
  using index 
  tablespace GLOBUS_INDEX;

Create Table niff_pbi_Relatorios (Id Number Not Null,
                                  Nome Varchar2(255) Not Null,
                                  Ativo Varchar2(1) Default 'N' Not Null) Tablespace Globus_Table;
                               
Alter Table niff_pbi_Relatorios
  add constraint PK_NiffRelatoriosBI primary key (Id)
  using index 
  tablespace GLOBUS_INDEX;
                                                              
Alter Table niff_pbi_Acessos Add IdRelatorio Number Not Null;

-- Alterado 22/03/2019
Alter Table Niff_Pto_Horario Add Motivo Varchar2(255) Null;

-- Alterado 25/03/2019
Create Table Niff_CTB_PercentualRateio (Id Number Not Null,
                                  IdEmpresa Number Not Null,
                                  Data Date Not Null,
                                  CodCusto Number Not Null,
                                  CodSetor Number Not Null,
                                  Quantidade Number Not Null,
                                  Percentual Number Not Null,
                                  IdUsuario Number Not Null,
                                  DataCalculo Date Not Null,                                  
                                  DataAlteracao Date Not Null,
                                  IdUsuarioAlteracao Number Not Null) Tablespace Globus_table;
                                  
                    
Alter Table Niff_CTB_PercentualRateio
  add constraint PK_NiffPercentualRateio primary key (Id)
  using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE Niff_CTB_PercentualRateio   
       ADD  ( CONSTRAINT fk_Empresa_PercentualRateio
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                

ALTER TABLE Niff_CTB_PercentualRateio   
       ADD  ( CONSTRAINT fk_Usuario_PercentualRateio
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                                                                                                   

ALTER TABLE Niff_CTB_PercentualRateio   
       ADD  ( CONSTRAINT fk_Usuario_PercentualRateio2
              FOREIGN KEY (IdUsuarioAlteracao)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                                                                                                   
    
Alter Table niff_ctb_associacustosetor Add CodContaCTBDestino Number;

-- Alterado 26/03/2019
Create Table Niff_CTB_Rateio (Id Number Not Null,
                              IdEmpresa Number Not Null,
                              Referencia Number Not Null,
                              IdUsuario Number Not Null,
                              DataGravacao Date Not Null,
                              GeradoArquivo Varchar2(1) Default 'N' Not Null) Tablespace Globus_Table;

     
Alter Table Niff_CTB_Rateio
  add constraint PK_NiffCTBRateio primary key (Id)
  using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE Niff_CTB_Rateio   
       ADD  ( CONSTRAINT fk_Empresa_CTBRateio
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                

ALTER TABLE Niff_CTB_Rateio   
       ADD  ( CONSTRAINT fk_Usuario_CTBRateio
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                                                                                                   

Create Table Niff_CTB_ValoresRateio (Id Number Not Null,
                                     IdRateio Number Not Null,
                                     CodContaCTB Number Not Null,
                                     ContraPartida Number Null,
                                     CodCusto Number Not Null,
                                     Debito Number,
                                     Credito Number,                                     
                                     Historico Varchar2(2000)) Tablespace Globus_table;
                                                                 

Alter Table Niff_CTB_ValoresRateio
  add constraint PK_NiffCTBValoresRateio primary key (Id)
  using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE Niff_CTB_ValoresRateio   
       ADD  ( CONSTRAINT fk_CTBRateio_ValoresRateio
              FOREIGN KEY (IdRateio)   
                             REFERENCES Niff_CTB_Rateio (Id));                                

Alter Table Niff_CTB_ValoresRateio Add Documento Varchar2(10);
Alter Table Niff_CTB_PercentualRateio Add idRateio Number Not Null;
Alter Table Niff_CTB_PercentualRateio Drop Column IdEmpresa;
Alter Table Niff_CTB_PercentualRateio Drop Column Data;
Alter Table Niff_CTB_PercentualRateio Drop Column idUsuario;
Alter Table Niff_CTB_PercentualRateio Drop Column DataCalculo;
Alter Table Niff_CTB_PercentualRateio Drop Column DataAlteracao;
Alter Table Niff_CTB_PercentualRateio Drop Column idUsuarioAlteracao;
Alter Table niff_chm_usuarios Add ProcessaArquivei Varchar2(1) Default 'N' Not Null

-- Alterado 28/032019
Alter Table niff_ctb_param Add IgnorarFuncoes Varchar2(1) Default 'N' Not Null;
Alter Table niff_ctb_param Add CodigoFuncoes Varchar2(1000) Null;
Alter Table niff_ctb_param Add IgnorarFuncSemBeneficios Varchar2(1) Default 'N' Not Null;
Alter Table niff_ctb_param Add CodContaVR Varchar2(1000) Null;
Alter Table niff_ctb_param Add CodContaVT Varchar2(1000) Null;
Alter Table niff_ctb_param Add CodContaMedico Varchar2(1000) Null;
Alter Table niff_ctb_param Add CodContaOdontologio Varchar2(1000) Null;
Alter Table niff_ctb_param Add CodContaCBasica Varchar2(1000) Null;
 Alter Table niff_ctb_param Add HistoricoPadrao Varchar2(1000) Null;

 -- Alterado 29/03/2019
 Alter Table niff_ctb_valoresrateio Add CodCustoCredito Number;

 -- Alterado 23/04/2019
 Alter Table niff_ctb_param Add CodEventoVT Varchar2(1000);
 Alter Table niff_ctb_param Add RegraEspecificaVT Varchar2(1) Default 'N' Not Null;
 Alter Table Niff_CTB_PercentualRateio Add Regra Varchar2(2) Default 'PD' Not Null;
 Alter Table niff_chm_usuarios Add Desenvolvedor Varchar2(1) Default 'N';

 -- Alterado 24/04/2019
 Create Table Niff_Dep_ProgramacaoFerias (Id Number Not Null,
                                         IdEmpresa Number Not Null,
                                         CodIntFunc Number Not Null,
                                         IdUsuario Number Not Null,
                                         IdUsuarioAutorizacao Number Not Null,
                                         DataInicio Date Not Null,
                                         DataFim Date Not Null,
                                         QuantidadeDias Number Not Null,
                                         DataAutoRep Date Null,
                                         Status Varchar2(1) Default 'G' Not Null, -- A Autorizado, G Aguardando Autorizacao, R Reprovado
                                         DataSolicitacao Date Not Null ) Tablespace Globus_table;
                                         
Alter Table Niff_Dep_ProgramacaoFerias
  add constraint PK_NiffDepProgFerias primary key (Id)
  using index 
  tablespace GLOBUS_INDEX;     
  

ALTER TABLE Niff_Dep_ProgramacaoFerias   
       ADD  ( CONSTRAINT fk_Empresas_ProgFerias
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                        
                                         
ALTER TABLE Niff_Dep_ProgramacaoFerias   
       ADD  ( CONSTRAINT fk_Usuario_ProgFerias1
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                        
                                         
ALTER TABLE Niff_Dep_ProgramacaoFerias   
       ADD  ( CONSTRAINT fk_Usuario_ProgFerias2
              FOREIGN KEY (IdUsuarioAutorizacao)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));                                        
                                         
-- Alterado em 25/04/2019
Alter Table Niff_Chm_Usuarios Add Gerente Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Chm_Usuarios Add Coordenador Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Dep_Programacaoferias Add Gozadas Varchar2(1) Default 'N' Not Null;
Alter Table niff_ads_colabdepartamento Add IdUsuario Number ;

-- alterado em 26/04/2019
Alter Table niff_dep_programacaoferias Add VizualizadoPeloGerente Varchar2(1) Default 'N' Not Null;
Alter Table niff_dep_programacaoferias Add VizualizadoPeloCoordenador Varchar2(1) Default 'N' Not Null;
Alter Table niff_dep_programacaoferias Add Vizualizado Varchar2(1) Default 'N' Not Null;

-- Alterado em 03/05/2019
Alter Table NIFF_FIS_ParametrosArquivei Add DiretorioDacte Varchar2(1000);

-- Alterado em 06/05/2019

Create Table Niff_ads_MetasContasCTB (Id Number Not Null,
                                      IdMetas Number Not Null,
                                      IdEmpresa Number Not Null,
                                      CodContaCTB Number Not Null,
                                      NroPlano Number Not Null,
                                      Tipo Varchar2(1) Default '+') Tablespace Globus_Table;
                                      
Alter Table Niff_ads_MetasContasCTB 
  add constraint PK_NiffMetasContasCTB primary key (Id)
  using index 
  tablespace GLOBUS_INDEX;      
  

ALTER TABLE Niff_ads_MetasContasCTB   
       ADD  ( CONSTRAINT fk_Empresas_MetasContasCTB
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                        

ALTER TABLE Niff_ads_MetasContasCTB   
       ADD  ( CONSTRAINT fk_Empresas_Metas
              FOREIGN KEY (IdMetas)   
                             REFERENCES Niff_Ads_Metas (IdMetas));                                        
                                                                           
-- Alterado em 07/05/2019
Alter Table Niff_Ads_Metascontasctb Add Formula Varchar2(1000) Null;

-- Alterado em 08/05/2019
Alter Table niff_ads_metas Add ExibirNoDRE Varchar2(1) Default 'N' Not Null;
Alter Table niff_ads_metas Add GrupoTotalizador Varchar2(1) Default 'N' Not Null;
Alter Table niff_ads_metas Add FormulaTotalizador Varchar2(3000) Null;

-- alterado em 09/05/2019
Alter Table niff_chm_usuarios Add AcessaDRE Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add AcessaCadastroMetas Varchar2(1) Default 'N' Not Null;

-- Alterado em 10/05/2019
Create Table NIFF_CTB_DRE (Id Number Not Null,
                           IdEmpresa Number Not Null,
                           Referencia Varchar2(6) Not Null,
                           Fechado Varchar2(1) Default 'N' Not Null,
                           DataFechamento Date Null,
                           IdUsuario Number Not Null,
                           IdUsuarioFecha Number Null) Tablespace Globus_table;
                           
Alter Table NIFF_CTB_DRE Add Constraint Pk_NIFFCTBDRE
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE NIFF_CTB_DRE   
       ADD  ( CONSTRAINT fk_Empresas_DRE
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));      
 

ALTER TABLE NIFF_CTB_DRE   
       ADD  ( CONSTRAINT fk_Usuarios_DRE
              FOREIGN KEY (IdUsuario)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));      
 
ALTER TABLE NIFF_CTB_DRE   
       ADD  ( CONSTRAINT fk_Usuarios_DRE2
              FOREIGN KEY (IdUsuarioFecha)   
                             REFERENCES Niff_Chm_Usuarios (IdUsuario));      

Alter Table niff_ads_metas Add NivelCalculo Number Default 0;

-- alterado em 17/05/2019
Create Table Niff_ads_FeriadosEmendas (Id Number Not Null,
                                       IdEmpresa Number Not Null,
                                       Data Date Not Null, -- F Feriado, E Emenda
                                       Tipo Varchar2(1) Default 'F' Not Null) Tablespace Globus_table;
                                       
                       
Alter Table Niff_ads_FeriadosEmendas Add Constraint Pk_NIFFAdsFeriado
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE Niff_ads_FeriadosEmendas   
       ADD  ( CONSTRAINT fk_Empresas_FeriadoEmenda
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));      
                                        
Alter Table Niff_Ads_FeriadosEmendas Add Descricao Varchar2(100) Null;

-- Alterado em 22/05/2019
Alter Table Niff_Ctb_Dre Add Dissidio Number Default 0;

-- Alterado em 29/05/2019
Alter Table niff_fin_bancos Add Consolidar Varchar2(1) Default 'N' Not Null;
Alter Table niff_fin_bancos Add idEmpresaConsolidar Number Null;
Alter Table niff_fin_bancos Add IdBancoConsolidar Number Null;

-- Alterado em 30/05/2019
Alter Table niff_chm_usuarios Add PermiteReabrirDRE varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add ApenasConsultaDre varchar2(1) Default 'S' Not Null;

-- Alterado em 04/06/2019
Create Table Niff_CTB_FormulaLalur (Id Number Not Null,
                                    IdEmpresa Number Not Null,
                                    Ativo Varchar2(1) Default 'N' Not Null,
                                    Totalizador Varchar2(1) Default 'N' Not Null,
                                    Ordem Number Not Null,
                                    Descricao Varchar2(500) Not Null,
                                    Formula Varchar2(2000) Not Null) Tablespace Globus_Table;
                                    

Alter Table Niff_CTB_FormulaLalur Add Constraint Pk_NIFFCtbFormulaLalur
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE Niff_CTB_FormulaLalur   
       ADD  ( CONSTRAINT fk_Empresas_FormulaLalur
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                          

Create Table Niff_CTB_ContasFormulaLalur (Id Number Not Null,
                                          idFormula Number Not Null,
                                          nroPlano Number Not Null,
                                          CodContaCTB Number Not Null) Tablespace Globus_Table;
                                                                       
Alter Table Niff_CTB_ContasFormulaLalur Add Constraint Pk_NIFFContasFormulaLalur
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE Niff_CTB_ContasFormulaLalur   
       ADD  ( CONSTRAINT fk_FormulaLalur_ContasFormula
              FOREIGN KEY (idFormula)   
                             REFERENCES Niff_CTB_FormulaLalur (Id));                                          
                                                                       
-- Alterado em 05/06/2019
Alter Table niff_chm_usuarios Add AcessaLalur Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add AcessaCadastroLalur Varchar2(1) Default 'N' Not Null;
Alter Table niff_chm_usuarios Add AcessaCalculoLalur Varchar2(1) Default 'N' Not Null;

Create Table Niff_Ctb_ParamLalur (Id Number Not Null,
                                  idEmpresa Number Not Null,
                                  PercCompBaseCalculo Number,
                                  PercCSLL Number,
                                  PercIRPJ Number,
                                  ValorParcelaIsenta Number,
                                  PercAdicionalPagar Number,
                                  PercPat Number) Tablespace Globus_Table;
                                  

Alter Table Niff_Ctb_ParamLalur Add Constraint Pk_NIFFCtbParamLalur
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE Niff_Ctb_ParamLalur   
       ADD  ( CONSTRAINT fk_Empresas_ParamLalur
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                          
                                  

Create Table Niff_Ctb_Lalur (Id Number Not Null,
                             idEmpresa Number Not Null,
                             Referencia Number,
                             PeriodoEncerrado Varchar2(1) Default 'N') Tablespace Globus_Table;
                                  

Alter Table Niff_Ctb_Lalur Add Constraint Pk_NIFFCtbLalur
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE Niff_Ctb_Lalur   
       ADD  ( CONSTRAINT fk_Empresas_ParLalur
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                          

Create Table Niff_Ctb_LalurValores (Id Number Not Null,
                                    IdLalur Number Not Null,
                                    IdFormula Number Not Null,
                                    Valor Number) Tablespace Globus_Table;
                                  

Alter Table Niff_Ctb_LalurValores Add Constraint Pk_NIFFCtbLalurValores
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE Niff_Ctb_LalurValores   
       ADD  ( CONSTRAINT fk_Lalur_LalurValor
              FOREIGN KEY (IdLalur)   
                             REFERENCES Niff_Ctb_Lalur (Id));                                          

ALTER TABLE Niff_Ctb_LalurValores   
       ADD  ( CONSTRAINT fk_Formula_LalurValor
              FOREIGN KEY (IdFormula)   
                             REFERENCES Niff_Ctb_Formulalalur (Id));                                          
                                                                                                      
-- Alterado em 06/06/2019
Alter Table Niff_Ctb_Formulalalur Add Destacar Varchar2(1) Default 'N' Not Null;


Create Table niff_ctb_LalurValorCTB (Id Number Not Null,
                                     IdLalur Number Not null,
                                     IdLalurValor Number Not Null,
                                     NroPlano Number Not Null,
                                     CodContaCTB Number Not Null,
                                     Valor Number Not Null,
                                     ValorReal Number Not Null) Tablespace Globus_table;

Alter Table niff_ctb_LalurValorCTB Add Constraint Pk_NIFFCtbLalurValoresCTB
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE niff_ctb_LalurValorCTB   
       ADD  ( CONSTRAINT fk_Lalur_LalurValorCTB
              FOREIGN KEY (IdLalur)   
                             REFERENCES Niff_Ctb_Lalur (Id));                                          
                                     
ALTER TABLE niff_ctb_LalurValorCTB   
       ADD  ( CONSTRAINT fk_LalurValor_LalurValorCTB
              FOREIGN KEY (IdLalurValor)   
                             REFERENCES Niff_Ctb_Lalurvalores (Id));                                          
                                                                          
-- alterado em 17/06/2019
Alter Table niff_chm_usuarios Add ApenasPrevistoDRE Varchar2(1) Default 'N';

-- Alterado em 18/06/2019
Alter Table niff_ads_BSCEmAlteracao Add Tela Varchar2(100);
Alter Table niff_pto_horario Add Ausencia Varchar2(1) Default 'N';

-- Alterado em 25/06/2019
Alter Table Niff_Chm_Usuarios Add AcessaResumoFluxoCaixa Varchar2(1) Default 'N';
Alter Table Niff_Chm_Usuarios Add AcessaDemonstrativoFluxoCaixa Varchar2(1) Default 'N';

-- Alterado em 02/07/2019
Alter Table niff_chm_chamado Add IdEmpresaSolicitante Number; 
Alter Table niff_chm_usuarios Add SempreMostrarListaDeChamados Varchar2(1) Default 'N';

-- Alterado em 05/07/2019
Alter Table Niff_Chm_Histochamado Add RespondeuAutorizacao Varchar2(1) Default 'N';

-- Alterado em 10/07/2019
Alter Table niff_fin_colunas Modify Origem Varchar2(4);

-- Alterado em 11/07/2019
Alter Table niff_fin_bancos Add CodBancoCartoes Number Null;
Alter Table niff_fin_bancos Add CodAgenciaCartoes Number Null;
Alter Table niff_fin_bancos Add CodContaCartoes Varchar2(15) Null;
Alter Table Niff_Fin_Variaveis Add CalcularFinaisDeSemana Varchar2(1) Default 'S';

-- Alterado em 15/07/2019
Alter Table niff_chm_agendausu Add Avisar Varchar2(1) Default 'N' Not Null;

-- Alterado em 16/07/2019
Alter Table Niff_Chm_Usuarios Add AgendaLiberaCarros varchar2(1) Default 'N';
Alter Table Niff_chm_Agenda Add dataFimReal Date;
Alter Table Niff_Ctb_Paramlalur Add LimiteIRPJ Number;
Alter Table Niff_Ctb_Paramlalur Add LimiteCSLL Number;
Alter Table Niff_Ctb_Lalurvalores Add percetual Number;

-- Alterado em 17/07/2019
Alter Table niff_chm_usuarios Add AcessaEndividamento Varchar2(1) Default 'N';

Create Table Niff_CTB_ParametrosEndividamento (Id Number Not Null,
                                               IdEmpresa Number Not Null,
                                               CodigoForn Number Not Null,
                                               CodTpDoc Varchar2(3) Not Null) Tablespace Globus_table;
                                               
Alter Table Niff_CTB_ParametrosEndividamento Add Constraint Pk_NIFFCtbParamEndividamento
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE Niff_CTB_ParametrosEndividamento   
       ADD  ( CONSTRAINT fk_Empresa_ParamEndividamento
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                          
                                               
-- Alterado em 18/07/2019
Alter Table niff_ctb_parametrosendividamento Add Modalidade Varchar2(30);
Alter Table niff_chm_agenda Add DataLembrete Date;

-- Alterado em 19/07/2019
Alter Table niff_ctb_parametrosendividamento Modify CodigoForn Null;
Alter Table niff_ctb_parametrosendividamento Modify CodTpDoc Null;
Alter Table niff_ctb_parametrosendividamento Add CodTpDespesa Varchar2(5) Null;

-- Alterado em 24/07/2019
Create Table Niff_CTB_Endividamento (Id Number Not Null,
                                     IdEmpresa Number Not Null,
                                     Referencia Varchar2(6) Not Null,
                                     CodigoForn Number Not Null,
                                     Modalidade Varchar2(30) Not Null,
                                     Tipo Varchar2(30) Not Null,
                                     Contrato Varchar2(255) Null,
                                     Encerrado Varchar2(1) Default 'N' Not Null,
                                     Previsto Number,
                                     Realizado Number,
                                     Juros Number) Tablespace Globus_Table;
                                            
Alter Table Niff_CTB_Endividamento Add Constraint Pk_NIFFCtbEndividamento
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE Niff_CTB_Endividamento   
       ADD  ( CONSTRAINT fk_Empresa_Endividamento
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));                                          
                                            
                                                               
                                            
Create Table Niff_CTB_ValoresEndividamento (Id Number Not Null,
                                            IdEndividamento Number Not Null,
                                            CodDoctoCPG Number Null,
                                            PrevistoCPG Number,
                                            Previsto Number,
                                            Realizado Number,
                                            Juros Number) Tablespace Globus_Table;
                                            
                                            
Alter Table Niff_CTB_ValoresEndividamento Add Constraint Pk_NIFFCtbValoresEndividamento
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 


ALTER TABLE Niff_CTB_ValoresEndividamento  
       ADD  ( CONSTRAINT fk_Endividamento_Valores
              FOREIGN KEY (IdEndividamento)   
                             REFERENCES Niff_CTB_Endividamento (Id));                                          

Alter Table Niff_CTB_Endividamento Drop Column Contrato;
Alter Table Niff_CTB_ValoresEndividamento Add Contrato Varchar2(255);

-- Alterado em 25/07/2019
Alter Table Niff_CTB_ParametrosEndividamento Add NumeroPlano Number;

Alter Table Niff_CTB_ParametrosEndividamento Add CodigoContaJurosDebito Number;
Alter Table Niff_CTB_ParametrosEndividamento Add CodigoContaJurosCredito Number;
Alter Table Niff_CTB_ParametrosEndividamento Add CodigoContaVariacaoDebito Number;
Alter Table Niff_CTB_ParametrosEndividamento Add CodigoContaVariacaoCredito Number;
Alter Table Niff_CTB_ParametrosEndividamento Add CodigoContaCurtoPrazo Number;
Alter Table Niff_CTB_ParametrosEndividamento Add CodigoContaMedioPrazo Number;
Alter Table Niff_CTB_ParametrosEndividamento Add CodigoContaLongoPrazo Number;

-- alterado em 26/07/2019
Alter Table niff_ctb_parametrosendividamento Add HistoricoJuros Varchar2(500) Null;
Alter Table niff_ctb_parametrosendividamento Add HistoricoVariacao Varchar2(500) Null;
Alter Table niff_ctb_parametrosendividamento Add Lote Varchar2(3) Null;

-- alterado em 30/07/2019
Alter Table niff_tor_partidas Drop Column IdColaborador;
Alter Table niff_tor_partidas Add idUsuario Number;

-- alterado em 31/07/2019
Alter Table niff_tor_partidas Add Sexo Varchar2(1);

-- Alterado em 02/08/2019
Alter Table Niff_Ads_Metas Add TipoFrota Varchar2(200);

-- Alterado em 05/08/2019
Alter Table Niff_Ctb_Parametrosendividamento Drop Column CODIGOCONTAMEDIOPRAZO;

Alter Table Niff_Ctb_Parametrosendividamento Add CodigoContaCurtoPrevisto Number;
Alter Table Niff_Ctb_Parametrosendividamento Add CodigoContaLongoPrevisto Number;

-- Alterado em 06/08/2019
Create Table NIFF_CTB_Selic (Id Number Not Null,
                             Referencia Number Not Null,
                             Valor Number) Tablespace Globus_Table;
                           
Alter Table NIFF_CTB_Selic Add Constraint Pk_NIFFSelic
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 
                             
Create Table NIFF_CTB_Contrato (Id Number Not Null,
                                IdEmpresa Number Not Null,
                                CodigoFornecedor Number Not Null,
                                Contrato Varchar2(255) Not Null,
                                Pedido Varchar2(255) Not Null,
                                Valor Number,
                                Juros Number,
                                Multa Number,
                                Total Number,
                                QtdoParcelas Number,
                                Vencimento Date ) Tablespace Globus_Table;
                                                        
Alter Table NIFF_CTB_Contrato Add Constraint Pk_NIFFCtbContrato
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 
  
ALTER TABLE NIFF_CTB_Contrato   
       ADD  ( CONSTRAINT fk_Empresa_CtbContrato
              FOREIGN KEY (IdEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));          
                               
Create Table NIFF_CTB_Parcelamento (Id Number Not Null,
                                    idContrato Number Not Null,
                                    Parcela Number Not Null,
                                    Vencimento Date Not Null,
                                    ValorParcelado Number,
                                    JurosParcelado Number,
                                    MultaParcelado Number,
                                    PercJuros Number,
                                    Selic Number,
                                    Juros Number,
                                    ValorPagar Number) Tablespace Globus_Table;                                    

Alter Table NIFF_CTB_Parcelamento Add Constraint Pk_NIFFCtbParcelamento
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;       
    
ALTER TABLE NIFF_CTB_Parcelamento   
       ADD  ( CONSTRAINT fk_Parcelamento_CtbContrato
              FOREIGN KEY (idContrato)   
                             REFERENCES NIFF_CTB_Contrato (Id));      
                             
Create Table NiFF_CTB_ArquivosContrato (Id Number Not Null,
                                        IdContrato Number Not Null, 
                                        Arquivo Blob) Tablespace Globus_table;                             
             
Alter Table NiFF_CTB_ArquivosContrato Add Constraint Pk_NIFFCtbArquivoContratato
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;         
  
ALTER TABLE NiFF_CTB_ArquivosContrato   
       ADD  ( CONSTRAINT fk_Arquivo_CtbContrato
              FOREIGN KEY (idContrato)   
                             REFERENCES NIFF_CTB_Contrato (Id));                                            

Alter Table Niff_Ctb_Parametrosendividamento Add HistoricoJurosConciliacao Varchar(500);
Alter Table Niff_Ctb_Parametrosendividamento Add HistoricoPrevisto Varchar(500);

-- Alterado em 07/08/2019
Alter Table Niff_Ctb_Contrato Add Dia Number Not Null;
Alter Table Niff_Ctb_Contrato Add PercJuros Number Not Null;
Alter Table Niff_Ctb_Contrato Add AplicaSelic Varchar2(1) Default 'S' Not Null;
Alter Table Niff_Ctb_Contrato Modify Modalidade Varchar2(50);

Alter Table Niff_Ctb_Endividamento Modify Modalidade Varchar2(50);

Alter Table Niff_Ctb_Parametrosendividamento Modify Modalidade Varchar2(50);
Alter Table niff_ctb_parametrosendividamento  Add CodCustoCTBJuros Number;
Alter Table niff_ctb_parametrosendividamento  Add CodCustoCTBVariacao Number;
Alter Table niff_ctb_parametrosendividamento  Add CodCustoCTBJurosConci Number;
Alter Table niff_ctb_parametrosendividamento  Add CodCustoCTBJurosPrev Number;

Alter Table niff_chm_usuarios Add AcessaParcelamento Varchar2(1) Default 'N' Not Null;

--Alterado em  13/08/2019
Alter Table niff_ctb_arquivoscontrato Add NomeAquivo Varchar2(300);

-- Alterado em 14/08/2019
Alter Table Niff_Ctb_Contrato Add PercJurosDif Number;
Alter Table Niff_Ctb_Contrato Add PercMultaDif Number;
Alter Table Niff_Ctb_Contrato Add PercSelicDif Number;
Alter Table Niff_Ctb_Contrato Add ParcelaMinima Number;
Alter Table Niff_Ctb_Contrato Add AplicaJurosDif Varchar2(1) Default 'N' Not Null;

Alter Table Niff_Ctb_Parcelamento Add ValorPrincipalAtual Number;
Alter Table Niff_Ctb_Parcelamento Add ValorJurosDif Number;
Alter Table Niff_Ctb_Parcelamento Add ValorMultaDif Number;

-- Alterado em 19/08/2019
Alter Table niff_ctb_parcelamento Add SaldoDevedor Number Default 0;
Alter Table niff_ctb_parcelamento Add SaldoDevedorAnt Number Default 0;
Alter Table niff_ctb_parcelamento Add CurtoPrazo Number Default 0;
Alter Table niff_ctb_parcelamento Add LongoPrazo Number Default 0;
Alter Table niff_ctb_parcelamento Add JurosMes Number Default 0;

-- Alterado em 21/08/2019
Alter Table niff_chm_chamado Add TrocouCategoria Varchar2(1) Default 'N' Not Null;

-- Alterado em 22/08/2019
Alter Table Niff_Ctb_Parametrosendividamento Add IdUsuario Number;

-- Alterado em 23/08/2019
Create Table Niff_Fis_DiferencialAliquota (Id Number Not Null,
                                           IdEmpresa Number Not Null,
                                           Referencia Number Not Null,
                                           CodDoctoEsf Number Not Null,
                                           CFOP Number Not Null,
                                           AliquotaExterna Number Not Null,
                                           Aliquota Number Not Null,
                                           Base Number Not Null,
                                           Debito Number Not Null,
                                           Credito Number Not Null,
                                           Diferenca Number Not Null) Tablespace Globus_Table;
                                           
Alter Table Niff_Fis_DiferencialAliquota Add Constraint Pk_NIFFDifAliquota
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE Niff_Fis_DiferencialAliquota   
       ADD  ( CONSTRAINT fk_DifAliq_Empresas
              FOREIGN KEY (idEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));

-- Alterado em 27/08/2019
Alter Table niff_chm_notificacoessistema Add TipoAtualzacao Varchar2(1) Default 'G' Not Null; -- G Geral todos servidores, P Parcial apenas AP01

-- Alterado em 02/09/2019
Alter Table Niff_Ctb_Selic Add UFG Number Default 0;

Alter Table Niff_Ctb_Contrato Add ZerarParcelas Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Ctb_Contrato Add AplicarUFG Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Ctb_Contrato Add ZerarDaParcela Number;
Alter Table Niff_Ctb_Contrato Add Honorarios Number Default 0;
Alter Table Niff_Ctb_Contrato Add Correcao Number Default 0;
Alter Table Niff_Ctb_Contrato Add Reducao Number Default 0;
Alter Table Niff_Ctb_Contrato Add Custas Number Default 0;

Alter Table Niff_Ctb_Parcelamento Add UFG Number Default 0;
Alter Table Niff_Ctb_Parcelamento Add ParcelaEmUFG Number Default 0;
Alter Table niff_ctb_parcelamento Add HonorariosParcelado Number Default 0;

-- alterado em 18/09/2019
Alter Table niff_chm_atendimento Add idEmpresa Number;

-- Alterado em 26/09/2019
Alter Table Niff_Chm_Usuarios Add ReprocessaParcelamento Varchar2(1) Default 'N';

-- Alterado em 27/09/2019
Alter Table niff_chm_atendimento Modify TextoResposta Varchar2(4000);
Alter Table niff_chm_atendimento Modify MotivoRetorno Varchar2(4000);
Alter Table niff_chm_atendimento Modify MotivoCancelamento Varchar2(4000);
Alter Table niff_chm_atendimento Modify MotivoSatisfacao Varchar2(4000);
Alter Table niff_chm_atendimento Modify RespostaAoCliente Varchar2(4000);

-- Alterado em 01/10/2019
Alter Table niff_chm_usuarios Add AcessaCigam Varchar2(1) Default 'N' ;

Create Table Niff_ctb_ParamNotas (Id Number Not Null,
                                  CodGrupoDespesa Number Not Null,
                                  CodTpDespesa Varchar2(5) Not Null,
                                  NroPlano Number Not Null,
                                  CodContaCTB Number Not Null) Tablespace Globus_table;
                                  
Alter Table Niff_ctb_ParamNotas Add Constraint Pk_NIFFCTBParamNotas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 
                                  
-- Alterado em 02/10/2019
Create Table Niff_Ctb_ConferenciaNotas (Id Number Not Null,
                                        IdEmpresa Number Not Null,
                                        Referencia Number Not Null,
                                        CodIntNF Number Null,
                                        CodDoctoESF Number Null,
                                        CodMaterial Number Not Null,
                                        Conferido Varchar2(1) Default 'N' Not Null,
                                        IdUsuario Number Null,
                                        DataConferido Date Null) Tablespace Globus_table;
                                        
Alter Table Niff_Ctb_ConferenciaNotas Add Constraint Pk_NIFFCTBConferenciaNotas
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 

ALTER TABLE Niff_Ctb_ConferenciaNotas   
       ADD  ( CONSTRAINT fk_ConfNF_Empresas
              FOREIGN KEY (idEmpresa)   
                             REFERENCES Niff_Chm_Empresas (IdEmpresa));
                                                                          
Alter Table Niff_Ctb_ConferenciaNotas  Add Validado Varchar2(1) Default 'N' Not Null;
Alter Table Niff_Ctb_ConferenciaNotas  Add IdUsuarioValidado Number Null;
Alter Table Niff_Ctb_ConferenciaNotas  Add DataValidado Date Null;

Alter Table Niff_Chm_Usuarios Add AcessaNotasFiscaisCTB Varchar2(1) Default 'N' Not Null;

-- alterado em 03/10/2019
Alter Table Niff_Chm_Usuarios Add AcessaCTBNotasFicais Varchar2(1) Default 'N' Not Null;

-- alterado em 04/10/2019

Create Table niff_chm_AnexosAtendimento (Id Number Not Null,
                                         IdAtendimento Number Not Null,
                                         Anexo Blob,
                                         NomeArquivo Varchar2(1000) Not Null) Tablespace Globus_Table;

ALTER TABLE niff_chm_AnexosAtendimento   
       ADD  ( CONSTRAINT fk_AnexoAtend_Atendimento
              FOREIGN KEY (IdAtendimento)   
                             REFERENCES Niff_Chm_Atendimento (IdAtendimento));                                         

-- Alterado em 15/10/2019
create table NIFF_FIS_DiferencialAliquotaCFOP
(
  ID              NUMBER not null,
  IdDiferencial   Number Not Null,
  IDEMPRESA       NUMBER not null,
  REFERENCIA      NUMBER not null,
  CODDOCTOESF     NUMBER not null,
  CFOP            NUMBER not null,
  ALIQUOTAEXTERNA NUMBER not null,
  ALIQUOTA        NUMBER not null,
  BASE            NUMBER not null,
  DEBITO          NUMBER not null,
  CREDITO         NUMBER not null,
  DIFERENCA       NUMBER not null
)
tablespace GLOBUS_TABLE
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table NIFF_FIS_DiferencialAliquotaCFOP
  add constraint PK_NIFFDIFALIQUOTACFOP primary key (ID)
  using index 
  tablespace GLOBUS_INDEX
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table NIFF_FIS_DiferencialAliquotaCFOP
  add constraint FK_DIFALIQCFOP_EMPRESAS foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);

alter table NIFF_FIS_DiferencialAliquotaCFOP
  add constraint FK_DIFALIQCFOP_DifAliq foreign key (IdDiferencial)
  references Niff_Fis_Diferencialaliquota (Id);

  create table NIFF_FIS_DIFERENCIALALIQUOTA
(
  ID              NUMBER not null,
  IDEMPRESA       NUMBER not null,
  REFERENCIA      NUMBER not null,
  DOCUMENTO       Varchar2(10) Not Null,
  SERIE           Varchar2(5) Not Null,
  CODIGOFORN      Number Not Null,
  CODTPDOC        Varchar2(3) Not Null,
  BASE            NUMBER not null,
  DEBITO          NUMBER not null,
  CREDITO         NUMBER not null,
  DIFERENCA       NUMBER not null
)
tablespace GLOBUS_TABLE
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table NIFF_FIS_DIFERENCIALALIQUOTA
  add constraint PK_NIFFDIFALIQUOTAN primary key (ID)
  using index 
  tablespace GLOBUS_INDEX
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  ( 
    initial 64K
    next 1M
    minextents 1
    maxextents Unlimited 
  );
alter table NIFF_FIS_DIFERENCIALALIQUOTA
  add constraint FK_DIFALIQN_EMPRESAS foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);

 -- Alterado em 22/10/2019
 Alter Table Niff_Chm_Histochamado Add TipoUsuario Varchar2(1) Null;

 -- Alterado em 25/10/2019
Alter Table niff_chm_usuarios Add PodeIntegrarProgramacaoFerias Varchar2(1) Default 'N';
Alter Table niff_dep_programacaoferias Add IntegradoGlobus  Varchar2(1) Default 'N';
Alter Table niff_dep_programacaoferias Add IdUsuarioIntGlobus  Number;
Alter Table niff_dep_programacaoferias Add DataIntegradoGlobus  Date;
Alter Table niff_dep_programacaoferias Add IniPeriodoAquisitivo  Date;
Alter Table niff_dep_programacaoferias Add FinPeriodoAquisitivo  Date;
Alter Table niff_dep_programacaoferias Add Limite  Date;

-- Alterado em 29/10/2019
Alter Table Niff_Chm_Usuarios Add Diretor Varchar2(1) Default 'N';
Alter Table Niff_Dep_Programacaoferias Add VisualizadoPeloDiretor Varchar2(1) Default 'N';

-- Alterado em 21/11/2019
Alter Table niff_dep_programacaoferias Add MotivoReprovacao Varchar2(4000) Null;

-- Alterado em 22/11/2019
Alter Table  Niff_Chm_Atendimento Add Cod_seq_Secao Number;

-- Alterado em 27/11/2019
Create Table niff_dep_periodoAquisitivoFerias (Id Number Not Null,
                                               IdEmpresa Number Not Null,
                                               CodIntFunc Number Not Null,
                                               Inicio Date Not Null,
                                               Fim Date Not Null,
                                               Limite Date Not Null) Tablespace Globus_table;
                                               
Alter Table niff_dep_periodoAquisitivoFerias Add Constraint Pk_NIFFPerAquisFerias 
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 
                                               
alter table niff_dep_periodoAquisitivoFerias
  add constraint FK_PerAquiFerias_EMPRESAS foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);                                               
                                               
Alter Table Niff_Chm_Usuarios Add AcessaPerAquisitoFerias Varchar2(1) Default 'N';

-- Alterado em 03/12/2019
Alter Table Niff_Ctb_Conferencianotas Add CodISSInt Number;

-- Alterado em 09/12/2019
Alter Table niff_chm_usuarios Add AcessaSuprimentos Varchar2(1) Default 'N' Not Null ;
Alter Table niff_chm_usuarios Add AcessaMetasSuprimentos Varchar2(1) Default 'N' Not Null ; 

Create Table Niff_Sup_MetasAprovadores (Id Number Not Null,
                                        IdEmpresa Number Not Null, 
                                        CodIntFunc Number Not Null,
                                        Referencia Number Not Null,
                                        Meta Number Not Null,
                                        IdUsuarioInc Number Not Null,
                                        IdUsuarioAlt Number Null,
                                        DataInc Date Not Null,
                                        DataAlt Date Null) Tablespace Globus_Table;
                                        
Alter Table Niff_Sup_MetasAprovadores Add Constraint Pk_NIFFMetasAprov
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 

Alter Table Niff_Sup_MetasAprovadores
  add constraint FK_MetasAprov_EMPRESAS foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);                                               
                                        
Alter Table Niff_Sup_MetasAprovadores
  add constraint FK_MetasAprov_Usuario1 foreign key (IdUsuarioInc)
  references Niff_Chm_Usuarios (IdUsuario);                                               
                                                                                
Alter Table Niff_Sup_MetasAprovadores
  add constraint FK_MetasAprov_Usuario2 foreign key (IdUsuarioAlt)
  references Niff_Chm_Usuarios (IdUsuario);                                               
                                                                                
-- Alterado em 10/12/2019                                        
Create Table Niff_Sup_Pedidos (Id Number Not Null,
                               IdEmpresa Number Not Null,
                               IdUsuario Number Not Null,
                               Referencia Number Not Null,
                               NumeroPedido Number Not Null,
                               TipoPedido Varchar2(30) Not Null ) Tablespace Globus_table;
                               
Alter Table Niff_Sup_Pedidos Add Constraint Pk_NIFFPedidos
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                 

Alter Table Niff_Sup_Pedidos
  add constraint FK_Pedidos_EMPRESAS foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);                                               
                                        
Alter Table Niff_Sup_Pedidos
  add constraint FK_Pedidos_Usuario foreign key (IdUsuario)
  references Niff_Chm_Usuarios (IdUsuario);                                               
                               
-- Alterado em 12/12/2019
Alter Table niff_fis_arquivei Add IETomador VARCHAR2(20) Null;
Alter Table niff_fis_arquivei Add ENDERECOTomador VARCHAR2(200) Null;
Alter Table niff_fis_arquivei Add BAIRROTomador VARCHAR2(200) Null;
Alter Table niff_fis_arquivei Add CEPTomador VARCHAR2(10) Null;
Alter Table niff_fis_arquivei Add RAZAOSOCIALTomador VARCHAR2(200) Null;
Alter Table niff_fis_arquivei Add NUMEROENDTomador VARCHAR2(100) Null;

-- Alterado em 02/01/2020
Create Index idx_Datahisto On niff_chm_histochamado (Data);
Create Index idx_DataUsuariohisto On niff_chm_histochamado (Data,IdUsuario);
Create Index idx_DataUsuarioCh On niff_chm_chamado (Data,IdUsuario,Status);

-- Alterado em 10/01/2020
Alter Table NIFF_CHM_Empresas Add AtendenteRespEmDiasSAC Number Default 2;

-- Alterado em 13/01/2020
Create Table Niff_CTB_ConciliacaoBCO (Id Number Not Null,
                                      IdEmpresa Number Not Null,
                                      Referencia Number Not Null,
                                      IdUsuario Number Not Null,
                                      CodBanco Number Not Null,
                                      CodAgencia Number Not Null,
                                      CodContaBCO Varchar2(15) Not Null,
                                      SaldoIniBCO Number Default 0 Not Null,
                                      DebitoBCO Number Default 0 Not Null,
                                      CreditoBCO Number Default 0 Not Null,
                                      SaldoFinBCO Number Default 0 Not Null,                                                                                                                  
                                      SaldoIniCTB Number Default 0 Not Null,
                                      DebitoCTB Number Default 0 Not Null,
                                      CreditoCTB Number Default 0 Not Null,
                                      SaldoFinCTB Number Default 0 Not Null,                                                                                                                                                        
                                      Confirmado Varchar2(1) Default 'N' Not Null,
                                      TextoExplicativo Long) Tablespace Globus_table;
                                      
         
Alter Table Niff_CTB_ConciliacaoBCO Add Constraint Pk_NIFFConciliacaoBCO
  primary key (Id) using index 
  tablespace GLOBUS_INDEX;                                                                             
                  
Alter Table Niff_CTB_ConciliacaoBCO
  add constraint FK_ConciliaBCO_Usuario foreign key (IdUsuario)
  references Niff_Chm_Usuarios (IdUsuario);                                       
                  
Alter Table Niff_CTB_ConciliacaoBCO
  add constraint FK_ConciliaBCO_Empresa foreign key (IdEmpresa)
  references Niff_Chm_Empresas (IdEmpresa);                                         

Create Index idx_EmpresaRef On Niff_CTB_ConciliacaoBCO (IdEmpresa,Referencia);  

Alter Table Niff_Chm_Usuarios Add AcessaConciliacaoCTB Varchar2(1) Default 'N' Not Null;

-- Alterado em 17/01/2020
-- Create table
create table NIFF_CTB_ConciliacaoForn
(
  ID               Number not null,
  IDEMPRESA        Number not null,
  REFERENCIA       Number not null,
  IDUSUARIO        Number not null,
  NROPLANO         Number Not Null,
  CODCONTACTB      Number Not Null,
  ValorCPG         Number default 0 not null,
  ValorCTB         Number default 0 not null,
  CONFIRMADO       Varchar2(1) default 'N' not null,
  TEXTOEXPLICATIVO Varchar2(4000)
)
tablespace GLOBUS_TABLE ;

-- Create/Recreate primary, unique and foreign key constraints 
alter table NIFF_CTB_ConciliacaoForn
  add constraint PK_NIFFConciliacaoForn primary key (ID)
  using index 
  tablespace GLOBUS_INDEX;
  
alter table NIFF_CTB_ConciliacaoForn
  add constraint FK_CONCILIAForn_EMPRESA foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);
alter table NIFF_CTB_ConciliacaoForn 
  add constraint FK_CONCILIAForn_USUARIO foreign key (IDUSUARIO)
  references NIFF_CHM_USUARIOS (IDUSUARIO);
  
-- Create/Recreate indexes 
create index IDX_EMPRESAREF_ConcForn on NIFF_CTB_ConciliacaoForn (IDEMPRESA, REFERENCIA)
  tablespace GLOBUS_TABLE;

-- Alterado em 21/01/2020
Create Index idx_DataStatus On Niff_Chm_Chamado (Data, Status);  
Create Index idx_HDataStatus On Niff_Chm_Histochamado (Data, Status);  

-- Alterado em 23/01/2020
Create Table Niff_CTB_ContasAtivo (Id Number Not Null,
                                   IdEmpresa Number Not Null,
                                   CodigoAtivo Number Not Null,
                                   CodigoGrupo Varchar2(20) Not Null ) Tablespace Globus_Table;
                                   
alter table Niff_CTB_ContasAtivo
  add constraint PK_NIFFContasAtivo primary key (ID)
  using index 
  tablespace GLOBUS_INDEX;                                   
 
alter table Niff_CTB_ContasAtivo
  add constraint FK_ContasAtivo_EMPRESA foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);
  
Alter Table Niff_CTB_ContasAtivo Add Descricao Varchar2(100);

-- Alterado em 28/01/2020
Create Table niff_CTB_ConciliacaoATF (Id Number Not Null,
                                      IdEmpresa Number Not Null,
                                      Referencia Number Not Null,
                                      IdUsuario Number Not Null,
                                      Conta Varchar2(20) Not Null,
                                      ValorATF Number Default 0 Not Null,
                                      ValorCTB Number Default 0 Not Null,
                                      Confirmado Varchar2(1) Default 'N' Not Null,
                                      TextoExplicativo Varchar2(2000) ) Tablespace Globus_table;

alter table niff_CTB_ConciliacaoATF
  add constraint FK_CONCILIAATF_EMPRESA foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);
alter table niff_CTB_ConciliacaoATF
  add constraint FK_CONCILIAATF_USUARIO foreign key (IDUSUARIO)
  references NIFF_CHM_USUARIOS (IDUSUARIO);                                      
                                      
-- Alterado em 03/02/2020
Alter Table niff_ctb_contasformulalalur Add Regra Varchar2(3) Default 'C-D';

-- Alterado em 12/02/2020
Alter table niff_ctb_param add RegraEspecificaConvenios varchar2(1) default 'N' not null;
Alter table niff_ctb_param add IgnorarFuncSemConvenioMedico varchar2(1) default 'N' not null;
Alter table niff_ctb_param add IgnorarFuncSemConvenioOdonto varchar2(1) default 'N' not null;

-- Alterado em 13/02/2020
create table NIFF_CTB_CONCILIACAOCLI
(
  ID               NUMBER not null,
  IDEMPRESA        NUMBER not null,
  REFERENCIA       NUMBER not null,
  IDUSUARIO        NUMBER not null,
  NROPLANO         NUMBER not null,
  CODCONTACTB      NUMBER not null,
  VALORCRC         NUMBER default 0 not null,
  VALORCTB         NUMBER default 0 not null,
  CONFIRMADO       VARCHAR2(1) default 'N' not null,
  TEXTOEXPLICATIVO VARCHAR2(4000)
)
tablespace GLOBUS_TABLE;


alter table NIFF_CTB_CONCILIACAOCLI
  add constraint PK_NIFFCONCILIACAOCLI primary key (ID)
  using index 
  tablespace GLOBUS_INDEX;

alter table NIFF_CTB_CONCILIACAOCLI
  add constraint FK_CONCILIACLI_EMPRESA foreign key (IDEMPRESA)
  references NIFF_CHM_EMPRESAS (IDEMPRESA);
  
alter table NIFF_CTB_CONCILIACAOCLI
  add constraint FK_CONCILIACLI_USUARIO foreign key (IDUSUARIO)
  references NIFF_CHM_USUARIOS (IDUSUARIO);
  

create index IDX_EMPRESAREF_CONCCLI on NIFF_CTB_CONCILIACAOCLI (IDEMPRESA, REFERENCIA)
  tablespace GLOBUS_TABLE;
  
create index IDX_USUARIO_CONCCLI on NIFF_CTB_CONCILIACAOCLI (IDUSUARIO)
  tablespace GLOBUS_INDEX;

  -- Alterado em 19/02/2020
  Alter Table niff_chm_atendimento Add ReclamacaoProcede Varchar2(1) Null;

  -- Alterado em 21/02/2020
  Alter Table niff_ctb_contasativo Add IdEmpresaAtivo Number;

  -- Alterado em 28/02/2020
  Alter Table Niff_Chm_Usuarios Add AcessaConciliacaoBCOApenasConsulta Varchar2(1) Default 'N' Not Null;

  -- Alterado em 03/3/2020
Alter Table Niff_Ctb_Parcelamento Add ReducaoParcelada Number; 
Alter Table Niff_Ctb_Parcelamento Add CorrecaoParcelada Number;
Alter Table Niff_Ctb_Parcelamento Add CustasParcelada Number;

--Alterado em 06/03/2020
Alter Table niff_fis_cfopcst Add Tipo Varchar2(1) Default 'E' Not Null;

--Alterado em 09/03/2020
Alter Table Niff_Fis_Arquivei Add Tributacao Varchar2(200) Null;
Alter Table Niff_Fis_Arquivei Add OpcaoSimples Varchar2(200) Null;
Alter Table Niff_Fis_Arquivei Add ValorServico Number Default 0;
Alter Table Niff_Fis_Arquivei Add CodigoServico Number Null;
Alter Table Niff_Fis_Arquivei Add AliquotaServico Number Default 0;
Alter Table Niff_Fis_Arquivei Add ValorISS  Number Default 0;
Alter Table Niff_Fis_Arquivei Add ValorCredito  Number Default 0;
Alter Table Niff_Fis_Arquivei Add ISSRetido Varchar2(1) Default 'N';
Alter Table Niff_Fis_Arquivei Add Discriminacao Varchar2(5000); 

Alter Table Niff_Fis_Arquivei Add ValorPis  Number Default 0;
Alter Table Niff_Fis_Arquivei Add ValorCofins  Number Default 0;
Alter Table Niff_Fis_Arquivei Add ValorIR  Number Default 0;
Alter Table Niff_Fis_Arquivei Add ValorCSLL  Number Default 0;
Alter Table Niff_Fis_Arquivei Add DataCancelamento Date;


Alter Table Niff_Fis_Parametrosarquivei Add DiretorioNFSE Varchar2(1000) Null;

-- Alterado em 10/03/2020
Alter Table niff_fis_arquivei Add ValorInss Number Default 0;

-- Alterado em 12/03/2020
Create Table niff_fis_ParamCodServico (Id Number Not Null,
                                       idEmpresa Number Not Null,
                                       CodigoXML Varchar2(20) Not Null,
                                       CodigoGlobus Varchar2(20)) Tablespace Globus_Table;
                                       

alter table niff_fis_ParamCodServico
  add constraint PK_NiffParamCodServ primary key (ID)
  using index 
  tablespace GLOBUS_INDEX;

alter table niff_fis_ParamCodServico
  add constraint FK_ParamCodServ_EMPRESA foreign key (IDEMPRESA) 
  references NIFF_CHM_EMPRESAS (IDEMPRESA);                                       

-- Alterado em 27/05/2020
Alter Table Niff_Fis_Arquivei Add ComentarioUsuario Varchar2(4000);

Alter Table Niff_Fis_Arquivei Add ComentarioUsuario Varchar2(4000)

-- Alterado em 29/05/2020
Alter Table niff_chm_chamado Add TempoEstimadoMin Number Default 0;

Create Table niff_chm_TempoExecucao (Id Number Not Null,
                                     IdChamado Number Not Null,
                                     DataInicio Date Not Null,
                                     DataFim Date Null,
                                     TempoMin Number Default 0) Tablespace Globus_table;
                                     

alter table niff_chm_TempoExecucao
  add constraint PK_NiffTempoExec primary key (ID)
  using index 
  tablespace GLOBUS_INDEX;

alter table niff_chm_TempoExecucao
  add constraint FK_TempoExec_Chamado foreign key (IdChamado) 
  references NIFF_CHM_CHAMADO (IdChamado);                                         


alter table niff_chm_TempoExecucao Add IdUsuario Number Not Null;  

alter table niff_chm_TempoExecucao
  add constraint FK_TempoExec_Usuario foreign key (IdUsuario) 
  references NIFF_CHM_USUARIOS (IdUsuario);                                         

  -- Alterado em 09/07/2020
  Alter Table Niff_CTB_ValoresEndividamento Add ExcluidoNoCPG Varchar2(1) Default 'N' Not Null;
  Alter Table Niff_CTB_ValoresEndividamento Add EXCLUIDONOCPGREFERECIA Number;
  Alter Table Niff_CTB_ValoresEndividamento Add UTILIZADONAREFERENCIA Number;
  
  -- Alterado em 10/07/2020
  Alter Table niff_fis_arquivei Add TipoProcessamento Varchar2(20) Default 'Recebidos' Not Null;

  -- Alterado em 15/07/2020
  Alter Table niff_fis_importandoarquivei Add idnfeGlobus Number;
  Alter Table niff_fis_arquivei Add idnfeGlobus Number;

-- Alteado em 22/07/2020
Alter Table niff_chm_usuarios Add RecebeEmailDifRecebedoria Varchar2(1) Default 'N';

-- Alterado em 23/07/2020 porem não aplicado
Alter Table niff_pto_horario Add InicioAtestado Varchar2(5) Null;
Alter Table niff_pto_horario Add InicioFim Varchar2(5) Null;
Alter Table niff_pto_horario Add HomeOffice Varchar2(1) Default 'N' Not Null;
Alter Table niff_pto_horario Add Reducao Varchar2(1) Default 'N' Not Null;
Alter Table niff_pto_horario Add PercentualReducao Number Default 0 Null;

-- Alterado em 29/07/2020
Create Index idxRecSigonEmpData On niff_rec_sigom (Idempresa, Iniciojornadaglobus);
Create Table niff_ctb_EndividamentoConciliado (Id Number Not Null,
                                       IdEmpresa Number Not Null,
                                       REFERENCIA	Varchar2(6) Not Null,
                                       CODIGOFORN	Number Not Null,
                                       MODALIDADE	VARCHAR2(50) Not Null,
                                       TIPO	VARCHAR2(30) Not Null,
                                       Realizado Number,
                                       Previsto  Number,
                                       Juros Number,
                                       PrevistoCurtoCPG  Number,
                                       PrevistoLongoCPG  Number,
                                       PrevistoCurtoCTB  Number,
                                       PrevistoLongoCTB  Number,
                                       JurosCurtoCPG  Number,
                                       JurosLongoCPG  Number,
                                       JurosCurtoCTB  Number,
                                       JurosLongoCTB  Number,
                                       PrevistoConciliado Number,
                                       JurosConciliado Number ) Tablespace Globus_table;
                                       
alter table niff_ctb_EndividamentoConciliado
  add constraint PK_NiffEndConc primary key (ID)
  using index 
  tablespace GLOBUS_INDEX;

alter table niff_ctb_EndividamentoConciliado
  add constraint FK_EndConc_Empresa foreign key (IdEmpresa) 
  references Niff_Chm_Empresas (IdEmpresa);     
                                       
-- Alterado em 30/07/2020
Alter Table niff_ctb_endividamentoconciliado Add Ativo Varchar2(1) Default 'S' Not Null;
Alter Table niff_ctb_endividamentoconciliado Add DataInclusao Date Default Sysdate Not Null;
Alter Table niff_ctb_endividamentoconciliado Add DataCancelamento Date Null;

-- Alterado em 31/07/2020

Create Table niff_fis_cfopEmitidas (Id Number Not Null,
                                    CFOP Number Not Null,
                                    Natureza Varchar2(1000) Not Null, 
                                    Lei Number Not Null,
                                    CST Number,
                                    Operacao Number) Tablespace Globus_table;
                                    
alter table niff_fis_cfopEmitidas
  add constraint PK_NiffCFOPEmit primary key (ID)
  using index 
  tablespace GLOBUS_INDEX;

-- Alterado em 14/08/2020
Alter Table niff_fis_cfopEmitidas Add IdEmpresa Number;

alter table niff_fis_cfopEmitidas
  add constraint FK_CFOPEmi_Empresa foreign key (IdEmpresa) 
  references Niff_Chm_Empresas (IdEmpresa);     

-- Alterado em 17/08/2020
Alter Table niff_fis_cfopemitidas Add SerieGlobus Varchar2(15) Null;
Alter Table niff_fis_cfopemitidas Add SerieCompara Varchar2(15) Null;

-- Alterado em 21/10/2020
Alter Table Niff_Chm_Usuarios Add AbreServicoExcel Varchar2(1) Default 'N' Not Null;
