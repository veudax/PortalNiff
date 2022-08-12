create table Niff_Calendario
(
  Id              NUMBER not null,
  ANO             NUMBER(4),
  MES             VARCHAR2(20),
  MESKEY          NUMBER(2),
  DIA             NUMBER(2),
  DATA            DATE,
  DIASEMANA       VARCHAR2(12),
  FIMSEMANA       VARCHAR2(1),
  FERIADO         VARCHAR2(1),
  TRIMESTRE       VARCHAR2(12),
  DIAMES          VARCHAR2(5),
  ANOMES          NUMBER(6),
  DATACADASTROBSP DATE,
  DESCRICAOTPDIA  VARCHAR2(30)
) Tablespace Globus_Table;

alter table Niff_Calendario
  add constraint XPKNiff_Calendario primary key (Id)
  using index 
  tablespace Globus_index
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 128K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate indexes 
create unique index BSP.XAK1NiffCalendario on Niff_Calendario (DATA)
  tablespace BSP_INDEX
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 192K
    next 1M
    minextents 1
    maxextents unlimited
  );
create index BSP.XIE1NiffCalendario on Niff_Calendario (ANO, MESKEY)
  tablespace BSP_INDEX
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 192K
    next 1M
    minextents 1
    maxextents unlimited
  );
create index BSP.XIE2NiffCalendario on Niff_Calendario (ANOMES)
  tablespace BSP_INDEX
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 192K
    next 1M
    minextents 1
    maxextents unlimited
  );
create index BSP.XIE3NiffCalendario on Niff_Calendario (DIASEMANA)
  tablespace BSP_INDEX
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 256K
    next 1M
    minextents 1
    maxextents unlimited
  );