create table {user}.ACTION_LOG
(
  uuid        VARCHAR2(36) not null,
  create_user VARCHAR2(36) not null, 
  update_user VARCHAR2(36) not null, 
  create_date DATE default sysdate not null,
  update_date DATE default sysdate not null,
  is_active   VARCHAR2(1) default 'Y' not null,
  attendant_uuid VARCHAR2(36) not null, 
  class_name VARCHAR2(128) null, 
  function_name VARCHAR2(128) null, 
  parameter VARCHAR2(4000) null 
);
alter table {user}.ACTION_LOG
  add constraint ACTION_LOG_PK primary key (UUID)
  using index ;