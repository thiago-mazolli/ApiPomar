-- Create table
create table API_ESPECIE
(
  esp_in_codigo    NUMBER(2) not null,
  esp_st_descricao VARCHAR2(50) not null
)
tablespace TSD_MEGA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 1M
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table API_ESPECIE
  add constraint PK_API_ESPECIE primary key (ESP_IN_CODIGO)
  using index 
  tablespace TSD_MEGA
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



-- Create table
create table API_ARVORE
(
  arv_in_codigo    NUMBER(3) not null,
  arv_st_descricao VARCHAR2(50) not null,
  arv_in_idade     NUMBER(5) not null,
  esp_in_codigo    NUMBER(2) not null
)
tablespace TSD_MEGA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 1M
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table API_ARVORE
  add constraint PK_API_ARVORE primary key (ARV_IN_CODIGO)
  using index 
  tablespace TSD_MEGA
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
alter table API_ARVORE
  add constraint FK_API_ARVORE_ESPECIE foreign key (ESP_IN_CODIGO)
  references API_ESPECIE (ESP_IN_CODIGO) on delete cascade;



-- Create table
create table API_GRUPO
(
  gru_in_codigo    NUMBER(2) not null,
  gru_st_descricao VARCHAR2(50) not null
)
tablespace TSD_MEGA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 1M
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table API_GRUPO
  add constraint PK_API_GRUPO primary key (GRU_IN_CODIGO)
  using index 
  tablespace TSD_MEGA
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



-- Create table
create table API_GRUPO_ARVORE
(
  gru_in_codigo NUMBER(2) not null,
  arv_in_codigo NUMBER(3) not null
)
tablespace TSD_MEGA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 1M
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table API_GRUPO_ARVORE
  add constraint PK_API_GRUPO_ARVORE primary key (GRU_IN_CODIGO, ARV_IN_CODIGO)
  using index 
  tablespace TSD_MEGA
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
alter table API_GRUPO_ARVORE
  add constraint FK_API_GRUPO_ARVORE_ARVORE foreign key (ARV_IN_CODIGO)
  references API_ARVORE (ARV_IN_CODIGO) on delete cascade;
alter table API_GRUPO_ARVORE
  add constraint FK_API_GRUPO_ARVORE_GRUPO foreign key (GRU_IN_CODIGO)
  references API_GRUPO (GRU_IN_CODIGO) on delete cascade;



-- Create table
create table API_COLHEITA
(
  col_in_codigo       NUMBER(5) not null,
  col_dt_datacolheita DATE not null,
  col_re_peso         NUMBER(6,3) not null,
  arv_in_codigo       NUMBER(2) not null
)
tablespace TSD_MEGA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 1M
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table API_COLHEITA
  add constraint PK_API_COLHEITA primary key (COL_IN_CODIGO)
  using index 
  tablespace TSD_MEGA
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
alter table API_COLHEITA
  add constraint FK_API_COLHEITA_ARVORE foreign key (ARV_IN_CODIGO)
  references API_ARVORE (ARV_IN_CODIGO) on delete cascade;
