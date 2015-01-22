create table {user}.ERROR_LOG
(
  uuid                  VARCHAR2(32) not null,
  create_date           DATE default sysdate,
  update_date           DATE default sysdate,
  is_active             VARCHAR2(1) not null,
  error_code            VARCHAR2(32) not null,
  error_time            VARCHAR2(32) not null,
  error_message         VARCHAR2(3000) not null,
  application_name 		VARCHAR2(32) not null,
  attendant_uuid        VARCHAR2(32),
  error_type            VARCHAR2(32),
  is_read varchar2(1) default 'N'
)
;
alter table {user}.ERROR_LOG
  add constraint ERROR_LOG_PK primary key (UUID)
  using index ;