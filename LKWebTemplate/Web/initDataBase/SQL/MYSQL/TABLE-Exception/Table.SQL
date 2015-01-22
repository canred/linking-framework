CREATE TABLE `error_log` (
  `uuid` varchar(32) NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT NULL,
  `is_active` varchar(1) NOT NULL,
  `error_code` varchar(32) NOT NULL,
  `error_time` varchar(32) NOT NULL,
  `error_message` text NOT NULL,
  `application_name` varchar(32) DEFAULT NULL,
  `attendant_uuid` varchar(32) DEFAULT NULL,
  `error_type` varchar(32) DEFAULT NULL,
  `is_read` varchar(1) DEFAULT 'N',
  `error_logcol` varchar(45) DEFAULT 'N',
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
