CREATE TABLE `action_log` (
  `uuid` varchar(36) NOT NULL,
  `create_user` varchar(36) NOT NULL,
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_user` varchar(36) NOT NULL,
  `update_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_active` char(1) NOT NULL,
  `attendant_uuid` varchar(36) NOT NULL,
  `class_name` varchar(128) DEFAULT NULL,
  `function_name` varchar(128) DEFAULT NULL,
  `parameter` varchar(400) DEFAULT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;