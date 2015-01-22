/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
DROP TABLE IF EXISTS `action_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `action_log` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'Y',
  `attendant_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `class_name` varchar(128) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `function_name` varchar(128) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `parameter` text COLLATE ucs2_unicode_ci,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `action_log` WRITE;
/*!40000 ALTER TABLE `action_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `action_log` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `application_head`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `application_head` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'Y',
  `name` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `description` varchar(500) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `id` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `web_site` varchar(200) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `application_head_fk1_idx` (`create_user`,`update_user`),
  KEY `application_head_fk2_idx` (`update_user`),
  CONSTRAINT `application_head_fk1` FOREIGN KEY (`create_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `application_head_fk2` FOREIGN KEY (`update_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `application_head` WRITE;
/*!40000 ALTER TABLE `application_head` DISABLE KEYS */;
INSERT INTO `application_head` VALUES ('13111517364100129','2013-11-15 09:36:41','2013-11-15 09:36:41','Y','WebTemplate','WebTemplate','WebTemplate','13111517225500304','13111517225500304','WebTemplate');
/*!40000 ALTER TABLE `application_head` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `application_head_v`;
/*!50001 DROP VIEW IF EXISTS `application_head_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `application_head_v` (
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `name` tinyint NOT NULL,
  `description` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `web_site` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `appmenu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `appmenu` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'Y',
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `name_zh_tw` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `name_zh_cn` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `name_en_us` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `id` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `appmenu_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `haschild` varchar(1) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `application_head_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `ord` int(11) DEFAULT NULL,
  `parameter_class` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `image` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `sitemap_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `action_mode` varchar(50) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `is_default_page` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'Y',
  `is_admin` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`uuid`),
  KEY `appmenu_fk1_idx` (`create_user`),
  KEY `appmenu_fk2_idx` (`update_user`),
  KEY `appmenu_fk3_idx` (`appmenu_uuid`),
  KEY `appmenu_fk4_idx` (`application_head_uuid`),
  KEY `appmenu_fk5_idx` (`sitemap_uuid`),
  CONSTRAINT `appmenu_fk1` FOREIGN KEY (`create_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `appmenu_fk2` FOREIGN KEY (`update_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `appmenu_fk3` FOREIGN KEY (`appmenu_uuid`) REFERENCES `appmenu` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `appmenu_fk4` FOREIGN KEY (`application_head_uuid`) REFERENCES `application_head` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `appmenu_fk5` FOREIGN KEY (`sitemap_uuid`) REFERENCES `sitemap` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `appmenu` WRITE;
/*!40000 ALTER TABLE `appmenu` DISABLE KEYS */;
INSERT INTO `appmenu` VALUES ('13111518062500413','Y','2013-11-15 10:06:25','13111517225500304','2013-11-15 10:06:26','13111517225500304','管理者','管理者','管理者','管理者','A1','Y','13111517364100129',99,NULL,NULL,NULL,NULL,'N','N'),('13111518072100460','Y','2013-11-15 10:07:21','13111517225500304','2013-11-15 10:07:21','13111517225500304','基本資料維護','基本資料維護','基本資料維護','基本資料維護','13111518062500413','Y','13111517364100129',1,NULL,NULL,NULL,NULL,'Y','N'),('13111518081400507','Y','2013-11-15 10:08:14','13111517225500304','2013-11-15 10:29:43','13111517225500304','公司維護','公司維護','公司維護','公司維護','13111518072100460','N','13111517364100129',1,NULL,NULL,'13111518014800145',NULL,'N','N'),('13111518083300534','Y','2013-11-15 10:08:34','13111517225500304','2013-11-15 10:08:34','13111517225500304','部門維護','部門維護','部門維護','部門維護','13111518072100460','N','13111517364100129',2,NULL,NULL,NULL,NULL,'N','N'),('13111518085500563','Y','2013-11-15 10:08:56','13111517225500304','2013-11-15 10:30:13','13111517225500304','人員維護','人員維護','人員維護','人員維護','13111518072100460','N','13111517364100129',3,NULL,NULL,'13111518014400133',NULL,'N','N'),('13111518093000616','Y','2013-11-15 10:09:31','13111517225500304','2013-11-15 10:09:31','13111517225500304','系統設定','系統設定','系統設定','系統設定','13111518062500413','Y','13111517364100129',2,NULL,NULL,NULL,NULL,'N','N'),('13111518095800649','Y','2013-11-15 10:09:59','13111517225500304','2013-11-15 10:30:25','13111517225500304','功能','功能','功能','功能','13111518093000616','N','13111517364100129',1,NULL,NULL,'13111518015000157',NULL,'N','N'),('13111518102400682','Y','2013-11-15 10:10:25','13111517225500304','2013-11-15 10:30:38','13111517225500304','Sitemap','Sitemap','Sitemap','Sitemap','13111518093000616','N','13111517364100129',2,NULL,NULL,'13111518014000121',NULL,'N','N'),('13111518110700727','Y','2013-11-15 10:11:08','13111517225500304','2013-11-15 10:30:50','13111517225500304','選單','選單','選單','選單','13111518093000616','N','13111517364100129',3,NULL,NULL,'13111518015800193',NULL,'N','N'),('13111518113000758','Y','2013-11-15 10:11:31','13111517225500304','2013-11-15 10:31:03','13111517225500304','系統','系統','系統','系統','13111518093000616','N','13111517364100129',4,NULL,NULL,'13111518015500181',NULL,'N','N'),('13111518115600793','Y','2013-11-15 10:11:56','13111517225500304','2013-11-15 10:31:16','13111517225500304','權限','權限','權限','權限','13111518062500413','N','13111517364100129',3,NULL,NULL,'13111518015300169',NULL,'N','N'),('A1','Y','2013-11-15 10:05:00',NULL,'2013-11-15 10:05:00',NULL,'ROOT','ROOT','ROOT','WebTemplate',NULL,'Y','13111517364100129',1,NULL,NULL,'13111517510400729',NULL,'N','N');
/*!40000 ALTER TABLE `appmenu` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `appmenu_apppage_v`;
/*!50001 DROP VIEW IF EXISTS `appmenu_apppage_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `appmenu_apppage_v` (
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `name_zh_tw` tinyint NOT NULL,
  `name_zh_cn` tinyint NOT NULL,
  `name_en_us` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `appmenu_uuid` tinyint NOT NULL,
  `haschild` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `ord` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `image` tinyint NOT NULL,
  `sitemap_uuid` tinyint NOT NULL,
  `action_mode` tinyint NOT NULL,
  `is_default_page` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `url` tinyint NOT NULL,
  `description` tinyint NOT NULL,
  `func_name` tinyint NOT NULL,
  `func_parameter_class` tinyint NOT NULL,
  `p_mode` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `appmenu_apppage_v_1`;
/*!50001 DROP VIEW IF EXISTS `appmenu_apppage_v_1`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `appmenu_apppage_v_1` (
  `uuid` tinyint NOT NULL,
  `description` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `url` tinyint NOT NULL,
  `func_name` tinyint NOT NULL,
  `func_parameter_class` tinyint NOT NULL,
  `p_mode` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `apppage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `apppage` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'Y',
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `id` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `name` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `description` varchar(500) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `url` varchar(200) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `parameter_class` varchar(200) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `application_head_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `p_mode` varchar(100) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `apppage_fk1_idx` (`create_user`),
  KEY `apppage_fk2_idx` (`update_user`),
  KEY `apppage_fk3_idx` (`application_head_uuid`),
  CONSTRAINT `apppage_fk1` FOREIGN KEY (`create_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `apppage_fk2` FOREIGN KEY (`update_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `apppage_fk3` FOREIGN KEY (`application_head_uuid`) REFERENCES `application_head` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `apppage` WRITE;
/*!40000 ALTER TABLE `apppage` DISABLE KEYS */;
INSERT INTO `apppage` VALUES ('13111517401900296','Y','2013-11-15 09:40:20','13111517225500304','2013-11-15 09:40:20','13111517225500304','公司維護','公司維護','公司維護','~/admin/basic/company.aspx','','13111517364100129',''),('13111517404600317','Y','2013-11-15 09:40:47','13111517225500304','2013-11-15 09:40:47','13111517225500304','人員維護','人員維護','人員維護','~/admin/basic/attendant.aspx','','13111517364100129',''),('13111517411100334','Y','2013-11-15 09:41:12','13111517225500304','2013-11-15 09:41:12','13111517225500304','系統','系統','系統','~/admin/system/system.aspx','','13111517364100129',''),('13111517413800355','Y','2013-11-15 09:41:38','13111517225500304','2013-11-15 09:41:38','13111517225500304','功能','功能','功能','~/admin/system/function.aspx','','13111517364100129',''),('13111517420300372','Y','2013-11-15 09:42:03','13111517225500304','2013-11-15 09:42:03','13111517225500304','sitemap','sitemap','sitemap','~/admin/system/sitemap.aspx','','13111517364100129',''),('13111517422900393','Y','2013-11-15 09:42:30','13111517225500304','2013-11-15 09:42:30','13111517225500304','選單','選單','選單','~/admin/system/menu.aspx','','13111517364100129',''),('13111517430000414','Y','2013-11-15 09:43:00','13111517225500304','2013-11-15 09:43:00','13111517225500304','權限','權限','權限','~/admin/authority/authority/grouphead_query.aspx','','13111517364100129',''),('13111517510400729','Y','2013-11-15 09:51:05','13111517225500304','2013-11-15 09:51:05','13111517225500304','WebTemplate','WebTemplate','WebTemplate','~/default.aspx','','13111517364100129','');
/*!40000 ALTER TABLE `apppage` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `apppage_v`;
/*!50001 DROP VIEW IF EXISTS `apppage_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `apppage_v` (
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `name` tinyint NOT NULL,
  `description` tinyint NOT NULL,
  `url` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `p_mode` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `attendant`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attendant` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'Y',
  `company_uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `account` varchar(50) COLLATE ucs2_unicode_ci NOT NULL,
  `c_name` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `e_name` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `email` varchar(300) COLLATE ucs2_unicode_ci NOT NULL,
  `password` varchar(50) COLLATE ucs2_unicode_ci NOT NULL,
  `is_supper` varchar(1) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'N',
  `is_admin` varchar(1) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'N',
  `code_page` varchar(10) COLLATE ucs2_unicode_ci NOT NULL,
  `department_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `phone` varchar(50) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `site_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `gender` varchar(1) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `birthday` timestamp NULL DEFAULT NULL,
  `hire_date` timestamp NULL DEFAULT NULL,
  `quit_date` timestamp NULL DEFAULT NULL,
  `is_manager` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'N',
  `is_direct` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'N',
  `grade` varchar(45) COLLATE ucs2_unicode_ci DEFAULT '0',
  `id` varchar(100) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `is_default_pass` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'N',
  `src_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `attendant_fk1_idx` (`company_uuid`),
  CONSTRAINT `attendant_fk1` FOREIGN KEY (`company_uuid`) REFERENCES `company` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `attendant` WRITE;
/*!40000 ALTER TABLE `attendant` DISABLE KEYS */;
INSERT INTO `attendant` VALUES ('13111517225500304','2013-11-15 09:22:56','2013-11-15 09:22:56','Y','ist','canred','陳慧鴻','Canred Chen','canred.chen@gmail.com','canred','Y','Y','TW',NULL,'0982650501',NULL,'M',NULL,NULL,NULL,'N','N',NULL,'',NULL,NULL);
/*!40000 ALTER TABLE `attendant` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `attendant_v`;
/*!50001 DROP VIEW IF EXISTS `attendant_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `attendant_v` (
  `company_id` tinyint NOT NULL,
  `company_c_name` tinyint NOT NULL,
  `company_e_name` tinyint NOT NULL,
  `department_id` tinyint NOT NULL,
  `department_c_name` tinyint NOT NULL,
  `department_e_name` tinyint NOT NULL,
  `site_id` tinyint NOT NULL,
  `site_c_name` tinyint NOT NULL,
  `site_e_name` tinyint NOT NULL,
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `company_uuid` tinyint NOT NULL,
  `account` tinyint NOT NULL,
  `c_name` tinyint NOT NULL,
  `e_name` tinyint NOT NULL,
  `email` tinyint NOT NULL,
  `password` tinyint NOT NULL,
  `is_supper` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `code_page` tinyint NOT NULL,
  `department_uuid` tinyint NOT NULL,
  `phone` tinyint NOT NULL,
  `site_uuid` tinyint NOT NULL,
  `gender` tinyint NOT NULL,
  `birthday` tinyint NOT NULL,
  `hire_date` tinyint NOT NULL,
  `quit_date` tinyint NOT NULL,
  `is_manager` tinyint NOT NULL,
  `is_direct` tinyint NOT NULL,
  `grade` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `is_default_pass` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_logon_v`;
/*!50001 DROP VIEW IF EXISTS `authority_logon_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_logon_v` (
  `company_id` tinyint NOT NULL,
  `company_c_name` tinyint NOT NULL,
  `company_e_name` tinyint NOT NULL,
  `department_id` tinyint NOT NULL,
  `department_c_name` tinyint NOT NULL,
  `department_e_name` tinyint NOT NULL,
  `site_id` tinyint NOT NULL,
  `site_c_name` tinyint NOT NULL,
  `site_e_name` tinyint NOT NULL,
  `login_info` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL,
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `company_uuid` tinyint NOT NULL,
  `account` tinyint NOT NULL,
  `c_name` tinyint NOT NULL,
  `e_name` tinyint NOT NULL,
  `email` tinyint NOT NULL,
  `password` tinyint NOT NULL,
  `is_supper` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `code_page` tinyint NOT NULL,
  `department_uuid` tinyint NOT NULL,
  `phone` tinyint NOT NULL,
  `site_uuid` tinyint NOT NULL,
  `gender` tinyint NOT NULL,
  `birthday` tinyint NOT NULL,
  `hire_date` tinyint NOT NULL,
  `quit_date` tinyint NOT NULL,
  `is_manager` tinyint NOT NULL,
  `is_direct` tinyint NOT NULL,
  `grade` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `src_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_menu_v`;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_menu_v` (
  `is_user_default_page` tinyint NOT NULL,
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `name_zh_tw` tinyint NOT NULL,
  `name_zh_cn` tinyint NOT NULL,
  `name_en_us` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `appmenu_uuid` tinyint NOT NULL,
  `haschild` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `ord` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `image` tinyint NOT NULL,
  `sitemap_uuid` tinyint NOT NULL,
  `action_mode` tinyint NOT NULL,
  `is_default_page` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL,
  `application_name` tinyint NOT NULL,
  `url` tinyint NOT NULL,
  `func_parameter_class` tinyint NOT NULL,
  `p_mode` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_menu_v_1`;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v_1`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_menu_v_1` (
  `attendant_uuid` tinyint NOT NULL,
  `application_name` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `appmenu_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_menu_v_2`;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v_2`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_menu_v_2` (
  `uuid` tinyint NOT NULL,
  `url` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `p_mode` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_menu_v_3`;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v_3`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_menu_v_3` (
  `is_user_default_page` tinyint NOT NULL,
  `application_name` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL,
  `appmenu_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_menu_v_4`;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v_4`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_menu_v_4` (
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `name_zh_tw` tinyint NOT NULL,
  `name_zh_cn` tinyint NOT NULL,
  `name_en_us` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `appmenu_uuid` tinyint NOT NULL,
  `haschild` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `ord` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `image` tinyint NOT NULL,
  `sitemap_uuid` tinyint NOT NULL,
  `action_mode` tinyint NOT NULL,
  `is_default_page` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL,
  `application_name` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_overview_v`;
/*!50001 DROP VIEW IF EXISTS `authority_overview_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_overview_v` (
  `url` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `description` tinyint NOT NULL,
  `p_mode` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `action_mode` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_overview_v_1`;
/*!50001 DROP VIEW IF EXISTS `authority_overview_v_1`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_overview_v_1` (
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `name_zh_tw` tinyint NOT NULL,
  `name_zh_cn` tinyint NOT NULL,
  `name_en_us` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `appmenu_uuid` tinyint NOT NULL,
  `haschild` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `ord` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `image` tinyint NOT NULL,
  `sitemap_uuid` tinyint NOT NULL,
  `action_mode` tinyint NOT NULL,
  `is_default_page` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `is_user_default_page` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_sitemap_v`;
/*!50001 DROP VIEW IF EXISTS `authority_sitemap_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_sitemap_v` (
  `url` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `p_mode` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `sitemap_root_uuid` tinyint NOT NULL,
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `menu_parameter_class` tinyint NOT NULL,
  `action_mode` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_sitemap_v_1`;
/*!50001 DROP VIEW IF EXISTS `authority_sitemap_v_1`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_sitemap_v_1` (
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `name_zh_tw` tinyint NOT NULL,
  `name_zh_cn` tinyint NOT NULL,
  `name_en_us` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `appmenu_uuid` tinyint NOT NULL,
  `haschild` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `ord` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `image` tinyint NOT NULL,
  `sitemap_uuid` tinyint NOT NULL,
  `action_mode` tinyint NOT NULL,
  `is_default_page` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `is_user_default_page` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `authority_url_v`;
/*!50001 DROP VIEW IF EXISTS `authority_url_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authority_url_v` (
  `url` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `company` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'Y',
  `class` varchar(45) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'A',
  `id` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `c_name` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `e_name` varchar(100) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `week_shift` int(11) DEFAULT '0',
  `name_zh_cn` varchar(128) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `concurrent_user` varchar(128) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `expired_date` timestamp NULL DEFAULT NULL,
  `sales_attendant_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `ou_sync_type` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `company_fk1_idx` (`sales_attendant_uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `company` WRITE;
/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `company` VALUES ('ist','2013-11-15 08:45:17','2013-11-15 09:21:15','Y','I','ist','宜特科技股份有限公司','IST Group Company',0,'宜特科技股份有限公司','',NULL,'','');
/*!40000 ALTER TABLE `company` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `company_v`;
/*!50001 DROP VIEW IF EXISTS `company_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `company_v` (
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `class` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `c_name` tinyint NOT NULL,
  `e_name` tinyint NOT NULL,
  `week_shift` tinyint NOT NULL,
  `ou_sync_type` tinyint NOT NULL,
  `name_zh_cn` tinyint NOT NULL,
  `concurrent_user` tinyint NOT NULL,
  `expired_date` tinyint NOT NULL,
  `sales_attendant_uuid` tinyint NOT NULL,
  `sales_account` tinyint NOT NULL,
  `sales_c_name` tinyint NOT NULL,
  `sales_e_name` tinyint NOT NULL,
  `sales_email` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `department` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'Y',
  `company_uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `id` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `c_name` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `e_name` varchar(100) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `parent_department_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `manager_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `parent_department_id` varchar(100) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `manager_id` varchar(100) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `parent_department_uuid_list` varchar(4000) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `s_name` varchar(100) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `cost_center` varchar(100) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `src_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `full_department_name` varchar(4000) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `department_fk1_idx` (`company_uuid`),
  KEY `department_fk2_idx` (`parent_department_uuid`),
  CONSTRAINT `department_fk1` FOREIGN KEY (`company_uuid`) REFERENCES `company` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `department_fk2` FOREIGN KEY (`parent_department_uuid`) REFERENCES `department` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `department` WRITE;
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
/*!40000 ALTER TABLE `department` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `department_v`;
/*!50001 DROP VIEW IF EXISTS `department_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `department_v` (
  `company_id` tinyint NOT NULL,
  `company_c_name` tinyint NOT NULL,
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `company_uuid` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `c_name` tinyint NOT NULL,
  `e_name` tinyint NOT NULL,
  `parent_department_uuid` tinyint NOT NULL,
  `manager_uuid` tinyint NOT NULL,
  `parent_department_id` tinyint NOT NULL,
  `manager_id` tinyint NOT NULL,
  `parent_department_uuid_list` tinyint NOT NULL,
  `s_name` tinyint NOT NULL,
  `cost_center` tinyint NOT NULL,
  `src_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `error_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `error_log` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'Y',
  `error_code` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `error_time` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `error_message` text COLLATE ucs2_unicode_ci,
  `application_name` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `attendant_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `error_type` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `is_read` varchar(1) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `error_log_fk1_idx` (`attendant_uuid`),
  CONSTRAINT `error_log_fk1` FOREIGN KEY (`attendant_uuid`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `error_log` WRITE;
/*!40000 ALTER TABLE `error_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `error_log` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `error_log_v`;
/*!50001 DROP VIEW IF EXISTS `error_log_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `error_log_v` (
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `error_code` tinyint NOT NULL,
  `error_time` tinyint NOT NULL,
  `error_message` tinyint NOT NULL,
  `application_name` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL,
  `error_type` tinyint NOT NULL,
  `is_read` tinyint NOT NULL,
  `c_name` tinyint NOT NULL,
  `e_name` tinyint NOT NULL,
  `id` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `group_appmenu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `group_appmenu` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `appmenu_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `group_head_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `is_default_page` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'N',
  `is_active` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'Y',
  PRIMARY KEY (`uuid`),
  KEY `group_appmenu_fk1_idx` (`appmenu_uuid`),
  KEY `group_appmenu_fk2_idx` (`group_head_uuid`),
  CONSTRAINT `group_appmenu_fk1` FOREIGN KEY (`appmenu_uuid`) REFERENCES `appmenu` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `group_appmenu_fk2` FOREIGN KEY (`group_head_uuid`) REFERENCES `group_head` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `group_appmenu` WRITE;
/*!40000 ALTER TABLE `group_appmenu` DISABLE KEYS */;
INSERT INTO `group_appmenu` VALUES ('13111518442500061','2013-11-15 10:44:26','13111517225500304','2013-11-15 10:44:26',NULL,'13111518062500413','13111518322902110','N','Y'),('13111518442500062','2013-11-15 10:44:26','13111517225500304','2013-11-15 10:44:26',NULL,'A1','13111518322902110','N','Y'),('13111518442600065','2013-11-15 10:44:27','13111517225500304','2013-11-15 10:44:27',NULL,'13111518072100460','13111518322902110','N','Y'),('13111518442700068','2013-11-15 10:44:28','13111517225500304','2013-11-15 10:44:28',NULL,'13111518081400507','13111518322902110','N','Y'),('13111518442800073','2013-11-15 10:44:29','13111517225500304','2013-11-15 10:44:29',NULL,'13111518083300534','13111518322902110','N','Y'),('13111518442900076','2013-11-15 10:44:30','13111517225500304','2013-11-15 10:44:30',NULL,'13111518085500563','13111518322902110','N','Y'),('13111518443000079','2013-11-15 10:44:30','13111517225500304','2013-11-15 10:44:30',NULL,'13111518093000616','13111518322902110','N','Y'),('13111518443100082','2013-11-15 10:44:31','13111517225500304','2013-11-15 10:44:31',NULL,'13111518095800649','13111518322902110','N','Y'),('13111518443100087','2013-11-15 10:44:32','13111517225500304','2013-11-15 10:44:32',NULL,'13111518102400682','13111518322902110','N','Y'),('13111518443200090','2013-11-15 10:44:33','13111517225500304','2013-11-15 10:44:33',NULL,'13111518110700727','13111518322902110','N','Y'),('13111518443300093','2013-11-15 10:44:34','13111517225500304','2013-11-15 10:44:34',NULL,'13111518113000758','13111518322902110','N','Y'),('13111518443400096','2013-11-15 10:44:34','13111517225500304','2013-11-15 10:44:34',NULL,'13111518115600793','13111518322902110','N','Y');
/*!40000 ALTER TABLE `group_appmenu` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `group_appmenu_v`;
/*!50001 DROP VIEW IF EXISTS `group_appmenu_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `group_appmenu_v` (
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `appmenu_uuid` tinyint NOT NULL,
  `group_head_uuid` tinyint NOT NULL,
  `is_default_page` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `group_attendant`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `group_attendant` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci DEFAULT 'Y',
  `create_date` timestamp NULL DEFAULT NULL,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_date` timestamp NULL DEFAULT NULL,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `group_head_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `attendant_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `group_attendant_fk1_idx` (`create_user`),
  KEY `group_attendant_fk2_idx` (`update_user`),
  KEY `group_attendant_fk3_idx` (`group_head_uuid`),
  KEY `group_attendant_fk4_idx` (`attendant_uuid`),
  CONSTRAINT `group_attendant_fk1` FOREIGN KEY (`create_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `group_attendant_fk2` FOREIGN KEY (`update_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `group_attendant_fk3` FOREIGN KEY (`group_head_uuid`) REFERENCES `group_head` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `group_attendant_fk4` FOREIGN KEY (`attendant_uuid`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `group_attendant` WRITE;
/*!40000 ALTER TABLE `group_attendant` DISABLE KEYS */;
INSERT INTO `group_attendant` VALUES ('13111518330402192','Y','2013-11-15 10:33:05','13111517225500304',NULL,'13111517225500304','13111518322902110','13111517225500304');
/*!40000 ALTER TABLE `group_attendant` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `group_attendant_v`;
/*!50001 DROP VIEW IF EXISTS `group_attendant_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `group_attendant_v` (
  `group_name_zh_tw` tinyint NOT NULL,
  `group_name_zh_cn` tinyint NOT NULL,
  `group_name_en_us` tinyint NOT NULL,
  `is_group_active` tinyint NOT NULL,
  `company_uuid` tinyint NOT NULL,
  `company_id` tinyint NOT NULL,
  `company_c_name` tinyint NOT NULL,
  `company_e_name` tinyint NOT NULL,
  `group_id` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `attendant_c_name` tinyint NOT NULL,
  `attendant_E_name` tinyint NOT NULL,
  `account` tinyint NOT NULL,
  `email` tinyint NOT NULL,
  `is_attendant_active` tinyint NOT NULL,
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `group_head_uuid` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL,
  `department_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `group_head`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `group_head` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'Y',
  `name_zh_tw` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `name_zh_cn` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `name_en_us` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `company_uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `id` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `application_head_uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  PRIMARY KEY (`uuid`),
  KEY `group_head_fk1_idx` (`company_uuid`),
  KEY `group_head_fk2_idx` (`create_user`),
  KEY `group_head_fk3_idx` (`update_user`),
  KEY `group_head_fk4_idx` (`application_head_uuid`),
  CONSTRAINT `group_head_fk1` FOREIGN KEY (`company_uuid`) REFERENCES `company` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `group_head_fk2` FOREIGN KEY (`create_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `group_head_fk3` FOREIGN KEY (`update_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `group_head_fk4` FOREIGN KEY (`application_head_uuid`) REFERENCES `application_head` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `group_head` WRITE;
/*!40000 ALTER TABLE `group_head` DISABLE KEYS */;
INSERT INTO `group_head` VALUES ('13111518322902110','2013-11-15 10:32:30','2013-11-15 10:32:30','Y','系統管理者','系統管理者','系統管理者','ist','WebTemplate','13111517225500304',NULL,'13111517364100129');
/*!40000 ALTER TABLE `group_head` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `group_head_v`;
/*!50001 DROP VIEW IF EXISTS `group_head_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `group_head_v` (
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_group_active` tinyint NOT NULL,
  `name_zh_tw` tinyint NOT NULL,
  `name_zh_cn` tinyint NOT NULL,
  `name_en_us` tinyint NOT NULL,
  `company_uuid` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `application_id` tinyint NOT NULL,
  `application_name` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `other_authority_application_v`;
/*!50001 DROP VIEW IF EXISTS `other_authority_application_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `other_authority_application_v` (
  `Application_Name` tinyint NOT NULL,
  `Application_Id` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `attendant_c_name` tinyint NOT NULL,
  `attendant_e_name` tinyint NOT NULL,
  `attendant_uuid` tinyint NOT NULL,
  `department_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `site`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `site` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'Y',
  `name_zh_tw` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `name_zh_cn` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `name_zh_us` varchar(200) COLLATE ucs2_unicode_ci NOT NULL,
  `company_uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `id` varchar(100) COLLATE ucs2_unicode_ci NOT NULL,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `application_head_uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `c_name` varchar(200) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `e_name` varchar(200) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `site_fk1_idx` (`create_user`),
  KEY `site_fk2_idx` (`update_user`),
  KEY `site_fk3_idx` (`company_uuid`),
  KEY `site_fk4_idx` (`application_head_uuid`),
  CONSTRAINT `site_fk1` FOREIGN KEY (`create_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `site_fk2` FOREIGN KEY (`update_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `site_fk3` FOREIGN KEY (`company_uuid`) REFERENCES `company` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `site_fk4` FOREIGN KEY (`application_head_uuid`) REFERENCES `application_head` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `site` WRITE;
/*!40000 ALTER TABLE `site` DISABLE KEYS */;
/*!40000 ALTER TABLE `site` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `sitemap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sitemap` (
  `uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `is_active` varchar(1) COLLATE ucs2_unicode_ci NOT NULL DEFAULT 'Y',
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `create_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_user` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `sitemap_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `apppage_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  `root_uuid` varchar(45) COLLATE ucs2_unicode_ci NOT NULL,
  `haschild` varchar(1) COLLATE ucs2_unicode_ci NOT NULL,
  `application_head_uuid` varchar(45) COLLATE ucs2_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `sitemap_fk1_idx` (`create_user`),
  KEY `sitemap_fk2_idx` (`update_user`),
  KEY `sitemap_fk3_idx` (`sitemap_uuid`),
  KEY `sitemap_fk4_idx` (`apppage_uuid`),
  KEY `sitemap_fk5_idx` (`application_head_uuid`),
  CONSTRAINT `sitemap_fk1` FOREIGN KEY (`create_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `sitemap_fk2` FOREIGN KEY (`update_user`) REFERENCES `attendant` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `sitemap_fk3` FOREIGN KEY (`sitemap_uuid`) REFERENCES `sitemap` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `sitemap_fk4` FOREIGN KEY (`apppage_uuid`) REFERENCES `apppage` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `sitemap_fk5` FOREIGN KEY (`application_head_uuid`) REFERENCES `application_head` (`uuid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `sitemap` WRITE;
/*!40000 ALTER TABLE `sitemap` DISABLE KEYS */;
INSERT INTO `sitemap` VALUES ('13111517510400729','Y','2013-11-15 09:51:20',NULL,'2013-11-15 09:51:20',NULL,NULL,'13111517510400729','13111517510400729','Y','13111517364100129'),('13111518014000121','Y','2013-11-15 10:01:41','13111517225500304','2013-11-15 10:01:41','13111517225500304','13111517510400729','13111517420300372','13111517510400729','Y','13111517364100129'),('13111518014400133','Y','2013-11-15 10:01:45','13111517225500304','2013-11-15 10:01:45','13111517225500304','13111517510400729','13111517404600317','13111517510400729','Y','13111517364100129'),('13111518014800145','Y','2013-11-15 10:01:48','13111517225500304','2013-11-15 10:01:48','13111517225500304','13111517510400729','13111517401900296','13111517510400729','Y','13111517364100129'),('13111518015000157','Y','2013-11-15 10:01:51','13111517225500304','2013-11-15 10:01:51','13111517225500304','13111517510400729','13111517413800355','13111517510400729','Y','13111517364100129'),('13111518015300169','Y','2013-11-15 10:01:53','13111517225500304','2013-11-15 10:01:53','13111517225500304','13111517510400729','13111517430000414','13111517510400729','Y','13111517364100129'),('13111518015500181','Y','2013-11-15 10:01:56','13111517225500304','2013-11-15 10:01:56','13111517225500304','13111517510400729','13111517411100334','13111517510400729','Y','13111517364100129'),('13111518015800193','Y','2013-11-15 10:01:59','13111517225500304','2013-11-15 10:01:59','13111517225500304','13111517510400729','13111517422900393','13111517510400729','Y','13111517364100129');
/*!40000 ALTER TABLE `sitemap` ENABLE KEYS */;
UNLOCK TABLES;
DROP TABLE IF EXISTS `sitemap_v`;
/*!50001 DROP VIEW IF EXISTS `sitemap_v`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `sitemap_v` (
  `uuid` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `create_user` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `update_user` tinyint NOT NULL,
  `sitemap_uuid` tinyint NOT NULL,
  `apppage_uuid` tinyint NOT NULL,
  `root_uuid` tinyint NOT NULL,
  `haschild` tinyint NOT NULL,
  `application_head_uuid` tinyint NOT NULL,
  `name` tinyint NOT NULL,
  `description` tinyint NOT NULL,
  `url` tinyint NOT NULL,
  `p_mode` tinyint NOT NULL,
  `parameter_class` tinyint NOT NULL,
  `apppage_is_active` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `vw_attendant_query`;
/*!50001 DROP VIEW IF EXISTS `vw_attendant_query`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `vw_attendant_query` (
  `uuid` tinyint NOT NULL,
  `create_date` tinyint NOT NULL,
  `update_date` tinyint NOT NULL,
  `is_active` tinyint NOT NULL,
  `company_uuid` tinyint NOT NULL,
  `account` tinyint NOT NULL,
  `c_name` tinyint NOT NULL,
  `e_name` tinyint NOT NULL,
  `email` tinyint NOT NULL,
  `password` tinyint NOT NULL,
  `is_supper` tinyint NOT NULL,
  `is_admin` tinyint NOT NULL,
  `code_page` tinyint NOT NULL,
  `department_uuid` tinyint NOT NULL,
  `department_c_name` tinyint NOT NULL,
  `phone` tinyint NOT NULL,
  `site_uuid` tinyint NOT NULL,
  `gender` tinyint NOT NULL,
  `birthday` tinyint NOT NULL,
  `hire_date` tinyint NOT NULL,
  `quit_date` tinyint NOT NULL,
  `is_manager` tinyint NOT NULL,
  `is_direct` tinyint NOT NULL,
  `grade` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `src_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
DROP TABLE IF EXISTS `vw_department_query`;
/*!50001 DROP VIEW IF EXISTS `vw_department_query`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `vw_department_query` (
  `company_c_name` tinyint NOT NULL,
  `company_uuid` tinyint NOT NULL,
  `company_e_name` tinyint NOT NULL,
  `manager_account` tinyint NOT NULL,
  `manager_c_name` tinyint NOT NULL,
  `full_department_name` tinyint NOT NULL,
  `uuid` tinyint NOT NULL,
  `id` tinyint NOT NULL,
  `c_name` tinyint NOT NULL,
  `e_name` tinyint NOT NULL,
  `parent_department_uuid` tinyint NOT NULL,
  `manager_uuid` tinyint NOT NULL,
  `parent_department_id` tinyint NOT NULL,
  `manager_id` tinyint NOT NULL,
  `s_name` tinyint NOT NULL,
  `cost_center` tinyint NOT NULL,
  `src_uuid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;
/*!50001 DROP TABLE IF EXISTS `application_head_v`*/;
/*!50001 DROP VIEW IF EXISTS `application_head_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `application_head_v` AS select `application_head`.`uuid` AS `uuid`,`application_head`.`create_date` AS `create_date`,`application_head`.`update_date` AS `update_date`,`application_head`.`is_active` AS `is_active`,`application_head`.`name` AS `name`,`application_head`.`description` AS `description`,`application_head`.`id` AS `id`,`application_head`.`create_user` AS `create_user`,`application_head`.`update_user` AS `update_user`,`application_head`.`web_site` AS `web_site` from `application_head` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `appmenu_apppage_v`*/;
/*!50001 DROP VIEW IF EXISTS `appmenu_apppage_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `appmenu_apppage_v` AS select `am`.`uuid` AS `uuid`,`am`.`is_active` AS `is_active`,`am`.`create_date` AS `create_date`,`am`.`create_user` AS `create_user`,`am`.`update_date` AS `update_date`,`am`.`update_user` AS `update_user`,`am`.`name_zh_tw` AS `name_zh_tw`,`am`.`name_zh_cn` AS `name_zh_cn`,`am`.`name_en_us` AS `name_en_us`,`am`.`id` AS `id`,`am`.`appmenu_uuid` AS `appmenu_uuid`,`am`.`haschild` AS `haschild`,`am`.`application_head_uuid` AS `application_head_uuid`,`am`.`ord` AS `ord`,`am`.`parameter_class` AS `parameter_class`,`am`.`image` AS `image`,`am`.`sitemap_uuid` AS `sitemap_uuid`,`am`.`action_mode` AS `action_mode`,`am`.`is_default_page` AS `is_default_page`,`am`.`is_admin` AS `is_admin`,`x`.`url` AS `url`,`x`.`description` AS `description`,`x`.`func_name` AS `func_name`,`x`.`func_parameter_class` AS `func_parameter_class`,`x`.`p_mode` AS `p_mode` from (`appmenu` `am` left join `appmenu_apppage_v_1` `x` on(((`am`.`sitemap_uuid` = `x`.`uuid`) and (`am`.`application_head_uuid` = `x`.`application_head_uuid`)))) where (`am`.`is_active` = '\0\0\0Y') */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `appmenu_apppage_v_1`*/;
/*!50001 DROP VIEW IF EXISTS `appmenu_apppage_v_1`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `appmenu_apppage_v_1` AS select `si`.`uuid` AS `uuid`,`page`.`description` AS `description`,`page`.`application_head_uuid` AS `application_head_uuid`,replace(`page`.`url`,'\0\0\0/','\0\0\0/\0\0\0 ') AS `url`,`page`.`name` AS `func_name`,`page`.`parameter_class` AS `func_parameter_class`,`page`.`p_mode` AS `p_mode` from (`sitemap` `si` join `apppage` `page` on(((`si`.`application_head_uuid` = `page`.`application_head_uuid`) and (`si`.`apppage_uuid` = `page`.`uuid`)))) where ((`si`.`is_active` = '\0\0\0Y') and (`page`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `apppage_v`*/;
/*!50001 DROP VIEW IF EXISTS `apppage_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `apppage_v` AS select `apppage`.`uuid` AS `uuid`,`apppage`.`is_active` AS `is_active`,`apppage`.`create_date` AS `create_date`,`apppage`.`create_user` AS `create_user`,`apppage`.`update_date` AS `update_date`,`apppage`.`update_user` AS `update_user`,`apppage`.`id` AS `id`,`apppage`.`name` AS `name`,`apppage`.`description` AS `description`,`apppage`.`url` AS `url`,`apppage`.`parameter_class` AS `parameter_class`,`apppage`.`application_head_uuid` AS `application_head_uuid`,`apppage`.`p_mode` AS `p_mode` from `apppage` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `attendant_v`*/;
/*!50001 DROP VIEW IF EXISTS `attendant_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `attendant_v` AS select `cmp`.`id` AS `company_id`,`cmp`.`c_name` AS `company_c_name`,`cmp`.`e_name` AS `company_e_name`,`dep`.`id` AS `department_id`,`dep`.`c_name` AS `department_c_name`,`dep`.`e_name` AS `department_e_name`,`sit`.`id` AS `site_id`,`sit`.`c_name` AS `site_c_name`,`sit`.`e_name` AS `site_e_name`,`a`.`uuid` AS `uuid`,`a`.`create_date` AS `create_date`,`a`.`update_date` AS `update_date`,`a`.`is_active` AS `is_active`,`a`.`company_uuid` AS `company_uuid`,`a`.`account` AS `account`,`a`.`c_name` AS `c_name`,`a`.`e_name` AS `e_name`,`a`.`email` AS `email`,`a`.`password` AS `password`,`a`.`is_supper` AS `is_supper`,`a`.`is_admin` AS `is_admin`,`a`.`code_page` AS `code_page`,`a`.`department_uuid` AS `department_uuid`,`a`.`phone` AS `phone`,`a`.`site_uuid` AS `site_uuid`,`a`.`gender` AS `gender`,`a`.`birthday` AS `birthday`,`a`.`hire_date` AS `hire_date`,`a`.`quit_date` AS `quit_date`,`a`.`is_manager` AS `is_manager`,`a`.`is_direct` AS `is_direct`,`a`.`grade` AS `grade`,`a`.`id` AS `id`,`a`.`is_default_pass` AS `is_default_pass` from (((`attendant` `a` left join `department` `dep` on((`a`.`department_uuid` = `dep`.`uuid`))) left join `site` `sit` on((`a`.`site_uuid` = `sit`.`uuid`))) join `company` `cmp` on((`a`.`company_uuid` = `cmp`.`uuid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_logon_v`*/;
/*!50001 DROP VIEW IF EXISTS `authority_logon_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_logon_v` AS select `cmp`.`id` AS `company_id`,`cmp`.`c_name` AS `company_c_name`,`cmp`.`e_name` AS `company_e_name`,`dep`.`id` AS `department_id`,`dep`.`c_name` AS `department_c_name`,`dep`.`e_name` AS `department_e_name`,`sit`.`id` AS `site_id`,`sit`.`c_name` AS `site_c_name`,`sit`.`e_name` AS `site_e_name`,(case `att`.`c_name` when '\0\0\0A\0\0\0N\0\0\0O\0\0\0N\0\0\0Y\0\0\0M\0\0\0O\0\0\0U\0\0\0S' then ' ' else (((((`att`.`c_name` + '(') + `att`.`e_name`) + ')(') + `cmp`.`id`) + ')') end) AS `login_info`,`att`.`uuid` AS `attendant_uuid`,`att`.`uuid` AS `uuid`,`att`.`create_date` AS `create_date`,`att`.`update_date` AS `update_date`,`att`.`is_active` AS `is_active`,`att`.`company_uuid` AS `company_uuid`,`att`.`account` AS `account`,`att`.`c_name` AS `c_name`,`att`.`e_name` AS `e_name`,`att`.`email` AS `email`,`att`.`password` AS `password`,`att`.`is_supper` AS `is_supper`,`att`.`is_admin` AS `is_admin`,`att`.`code_page` AS `code_page`,`att`.`department_uuid` AS `department_uuid`,`att`.`phone` AS `phone`,`att`.`site_uuid` AS `site_uuid`,`att`.`gender` AS `gender`,`att`.`birthday` AS `birthday`,`att`.`hire_date` AS `hire_date`,`att`.`quit_date` AS `quit_date`,`att`.`is_manager` AS `is_manager`,`att`.`is_direct` AS `is_direct`,`att`.`grade` AS `grade`,`att`.`id` AS `id`,`att`.`src_uuid` AS `src_uuid` from (((`attendant` `att` left join `department` `dep` on((`att`.`department_uuid` = `dep`.`uuid`))) left join `site` `sit` on((`att`.`site_uuid` = `sit`.`uuid`))) join `company` `cmp` on((`att`.`company_uuid` = `cmp`.`uuid`))) where ((`att`.`is_active` = '\0\0\0Y') and (`cmp`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_menu_v`*/;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_menu_v` AS select (case when isnull(`d`.`is_user_default_page`) then 'N' else 'Y' end) AS `is_user_default_page`,`auth_m`.`uuid` AS `uuid`,`auth_m`.`is_active` AS `is_active`,`auth_m`.`create_date` AS `create_date`,`auth_m`.`create_user` AS `create_user`,`auth_m`.`update_date` AS `update_date`,`auth_m`.`update_user` AS `update_user`,`auth_m`.`name_zh_tw` AS `name_zh_tw`,`auth_m`.`name_zh_cn` AS `name_zh_cn`,`auth_m`.`name_en_us` AS `name_en_us`,`auth_m`.`id` AS `id`,`auth_m`.`appmenu_uuid` AS `appmenu_uuid`,`auth_m`.`haschild` AS `haschild`,`auth_m`.`application_head_uuid` AS `application_head_uuid`,`auth_m`.`ord` AS `ord`,`auth_m`.`parameter_class` AS `parameter_class`,`auth_m`.`image` AS `image`,`auth_m`.`sitemap_uuid` AS `sitemap_uuid`,`auth_m`.`action_mode` AS `action_mode`,`auth_m`.`is_default_page` AS `is_default_page`,`auth_m`.`is_admin` AS `is_admin`,`auth_m`.`attendant_uuid` AS `attendant_uuid`,`auth_m`.`application_name` AS `application_name`,`x`.`url` AS `url`,`x`.`parameter_class` AS `func_parameter_class`,`x`.`p_mode` AS `p_mode` from ((`authority_menu_v_4` `auth_m` left join `authority_menu_v_2` `x` on(((`auth_m`.`sitemap_uuid` = `x`.`uuid`) and (`auth_m`.`application_head_uuid` = `x`.`application_head_uuid`)))) left join `authority_menu_v_3` `d` on(((`auth_m`.`uuid` = `d`.`appmenu_uuid`) and (`auth_m`.`application_head_uuid` = `d`.`application_head_uuid`) and (`auth_m`.`attendant_uuid` = `d`.`attendant_uuid`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_menu_v_1`*/;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v_1`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_menu_v_1` AS select distinct `ga`.`attendant_uuid` AS `attendant_uuid`,`ap`.`name` AS `application_name`,`g`.`application_head_uuid` AS `application_head_uuid`,`gm`.`appmenu_uuid` AS `appmenu_uuid` from (((`group_appmenu` `gm` join `group_attendant` `ga`) join `application_head` `ap`) join `group_head` `g` on(((`ap`.`uuid` = `g`.`application_head_uuid`) and (`ga`.`group_head_uuid` = `g`.`uuid`) and (`gm`.`group_head_uuid` = `g`.`uuid`)))) where ((`ga`.`is_active` = '\0\0\0Y') and (`g`.`is_active` = '\0\0\0Y') and (`gm`.`is_active` = '\0\0\0Y') and (`ap`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_menu_v_2`*/;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v_2`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_menu_v_2` AS select `si`.`uuid` AS `uuid`,`p`.`url` AS `url`,`p`.`parameter_class` AS `parameter_class`,`p`.`p_mode` AS `p_mode`,`si`.`application_head_uuid` AS `application_head_uuid` from (`sitemap` `si` join `apppage` `p` on(((`si`.`apppage_uuid` = `p`.`uuid`) and (`si`.`application_head_uuid` = `p`.`application_head_uuid`)))) where ((`si`.`is_active` = '\0\0\0Y') and (`p`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_menu_v_3`*/;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v_3`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_menu_v_3` AS select distinct `gm`.`is_default_page` AS `is_user_default_page`,`ap`.`name` AS `application_name`,`g`.`application_head_uuid` AS `application_head_uuid`,`ga`.`attendant_uuid` AS `attendant_uuid`,`gm`.`appmenu_uuid` AS `appmenu_uuid` from (((`group_appmenu` `gm` join `group_attendant` `ga`) join `application_head` `ap`) join `group_head` `g` on(((`ap`.`uuid` = `g`.`application_head_uuid`) and (`ga`.`group_head_uuid` = `g`.`uuid`) and (`gm`.`group_head_uuid` = `g`.`uuid`)))) where ((`ga`.`is_active` = '\0\0\0Y') and (`g`.`is_active` = '\0\0\0Y') and (`gm`.`is_active` = '\0\0\0Y') and (`ap`.`is_active` = '\0\0\0Y') and (`gm`.`is_default_page` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_menu_v_4`*/;
/*!50001 DROP VIEW IF EXISTS `authority_menu_v_4`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_menu_v_4` AS select `m`.`uuid` AS `uuid`,`m`.`is_active` AS `is_active`,`m`.`create_date` AS `create_date`,`m`.`create_user` AS `create_user`,`m`.`update_date` AS `update_date`,`m`.`update_user` AS `update_user`,`m`.`name_zh_tw` AS `name_zh_tw`,`m`.`name_zh_cn` AS `name_zh_cn`,`m`.`name_en_us` AS `name_en_us`,`m`.`id` AS `id`,`m`.`appmenu_uuid` AS `appmenu_uuid`,`m`.`haschild` AS `haschild`,`m`.`application_head_uuid` AS `application_head_uuid`,`m`.`ord` AS `ord`,`m`.`parameter_class` AS `parameter_class`,`m`.`image` AS `image`,`m`.`sitemap_uuid` AS `sitemap_uuid`,`m`.`action_mode` AS `action_mode`,`m`.`is_default_page` AS `is_default_page`,`m`.`is_admin` AS `is_admin`,`b`.`attendant_uuid` AS `attendant_uuid`,`b`.`application_name` AS `application_name` from (`appmenu` `m` left join `authority_menu_v_1` `b` on(((`m`.`application_head_uuid` = `b`.`application_head_uuid`) and (`m`.`uuid` = `b`.`appmenu_uuid`)))) where (`m`.`is_active` = '\0\0\0Y') */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_overview_v`*/;
/*!50001 DROP VIEW IF EXISTS `authority_overview_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_overview_v` AS select distinct `p`.`url` AS `url`,`p`.`parameter_class` AS `parameter_class`,`p`.`description` AS `description`,`p`.`p_mode` AS `p_mode`,`p`.`application_head_uuid` AS `application_head_uuid`,`p`.`is_active` AS `is_active`,`p`.`create_date` AS `create_date`,`p`.`create_user` AS `create_user`,`p`.`update_date` AS `update_date`,`p`.`update_user` AS `update_user`,`auth_m`.`action_mode` AS `action_mode`,`auth_m`.`attendant_uuid` AS `attendant_uuid` from ((`apppage` `p` join `sitemap` `si` on((`p`.`uuid` = `si`.`apppage_uuid`))) join `authority_overview_v_1` `auth_m` on(((`p`.`application_head_uuid` = `auth_m`.`application_head_uuid`) and (`si`.`application_head_uuid` = `auth_m`.`application_head_uuid`) and (`si`.`root_uuid` = `auth_m`.`sitemap_uuid`)))) where ((`p`.`is_active` = '\0\0\0Y') and (`si`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_overview_v_1`*/;
/*!50001 DROP VIEW IF EXISTS `authority_overview_v_1`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_overview_v_1` AS select distinct `m`.`uuid` AS `uuid`,`m`.`is_active` AS `is_active`,`m`.`create_date` AS `create_date`,`m`.`create_user` AS `create_user`,`m`.`update_date` AS `update_date`,`m`.`update_user` AS `update_user`,`m`.`name_zh_tw` AS `name_zh_tw`,`m`.`name_zh_cn` AS `name_zh_cn`,`m`.`name_en_us` AS `name_en_us`,`m`.`id` AS `id`,`m`.`appmenu_uuid` AS `appmenu_uuid`,`m`.`haschild` AS `haschild`,`m`.`application_head_uuid` AS `application_head_uuid`,`m`.`ord` AS `ord`,`m`.`parameter_class` AS `parameter_class`,`m`.`image` AS `image`,`m`.`sitemap_uuid` AS `sitemap_uuid`,`m`.`action_mode` AS `action_mode`,`m`.`is_default_page` AS `is_default_page`,`m`.`is_admin` AS `is_admin`,`gp_m`.`is_default_page` AS `is_user_default_page`,`gp_a`.`attendant_uuid` AS `attendant_uuid` from (((`group_appmenu` `gp_m` join `group_head` `gp_h` on((`gp_m`.`group_head_uuid` = `gp_h`.`uuid`))) join `group_attendant` `gp_a` on((`gp_h`.`uuid` = `gp_a`.`group_head_uuid`))) join `appmenu` `m` on((`gp_m`.`appmenu_uuid` = `m`.`uuid`))) where ((`m`.`is_active` = '\0\0\0Y') and (`gp_m`.`is_active` = '\0\0\0Y') and (`gp_h`.`is_active` = '\0\0\0Y') and (`gp_a`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_sitemap_v`*/;
/*!50001 DROP VIEW IF EXISTS `authority_sitemap_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_sitemap_v` AS select `p`.`url` AS `url`,`p`.`parameter_class` AS `parameter_class`,`p`.`p_mode` AS `p_mode`,`p`.`application_head_uuid` AS `application_head_uuid`,`si`.`root_uuid` AS `sitemap_root_uuid`,`p`.`uuid` AS `uuid`,`p`.`is_active` AS `is_active`,`p`.`create_date` AS `create_date`,`p`.`create_user` AS `create_user`,`p`.`update_date` AS `update_date`,`p`.`update_user` AS `update_user`,`auth_m`.`parameter_class` AS `menu_parameter_class`,`auth_m`.`action_mode` AS `action_mode`,`auth_m`.`is_admin` AS `is_admin`,`auth_m`.`attendant_uuid` AS `attendant_uuid` from ((`apppage` `p` join `sitemap` `si` on((`p`.`uuid` = `si`.`apppage_uuid`))) join `authority_sitemap_v_1` `auth_m` on(((`p`.`application_head_uuid` = `auth_m`.`application_head_uuid`) and (`si`.`application_head_uuid` = `auth_m`.`application_head_uuid`) and (`si`.`root_uuid` = `auth_m`.`sitemap_uuid`)))) where ((`p`.`is_active` = '\0\0\0Y') and (`si`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_sitemap_v_1`*/;
/*!50001 DROP VIEW IF EXISTS `authority_sitemap_v_1`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_sitemap_v_1` AS select distinct `m`.`uuid` AS `uuid`,`m`.`is_active` AS `is_active`,`m`.`create_date` AS `create_date`,`m`.`create_user` AS `create_user`,`m`.`update_date` AS `update_date`,`m`.`update_user` AS `update_user`,`m`.`name_zh_tw` AS `name_zh_tw`,`m`.`name_zh_cn` AS `name_zh_cn`,`m`.`name_en_us` AS `name_en_us`,`m`.`id` AS `id`,`m`.`appmenu_uuid` AS `appmenu_uuid`,`m`.`haschild` AS `haschild`,`m`.`application_head_uuid` AS `application_head_uuid`,`m`.`ord` AS `ord`,`m`.`parameter_class` AS `parameter_class`,`m`.`image` AS `image`,`m`.`sitemap_uuid` AS `sitemap_uuid`,`m`.`action_mode` AS `action_mode`,`m`.`is_default_page` AS `is_default_page`,`m`.`is_admin` AS `is_admin`,`gp_m`.`is_default_page` AS `is_user_default_page`,`gp_a`.`attendant_uuid` AS `attendant_uuid` from (((`group_appmenu` `gp_m` join `group_head` `gp_h` on((`gp_m`.`group_head_uuid` = `gp_h`.`uuid`))) join `group_attendant` `gp_a` on((`gp_h`.`uuid` = `gp_a`.`group_head_uuid`))) join `appmenu` `m` on((`gp_m`.`appmenu_uuid` = `m`.`uuid`))) where ((`m`.`is_active` = '\0\0\0Y') and (`gp_m`.`is_active` = '\0\0\0Y') and (`gp_h`.`is_active` = '\0\0\0Y') and (`gp_a`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `authority_url_v`*/;
/*!50001 DROP VIEW IF EXISTS `authority_url_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `authority_url_v` AS select distinct `ap`.`url` AS `url`,`ap`.`application_head_uuid` AS `application_head_uuid` from (`sitemap` `si` join `apppage` `ap` on(((`si`.`apppage_uuid` = `ap`.`uuid`) and (`si`.`application_head_uuid` = `ap`.`application_head_uuid`)))) where ((`si`.`is_active` = '\0\0\0Y') and (`ap`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `company_v`*/;
/*!50001 DROP VIEW IF EXISTS `company_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `company_v` AS select `a`.`uuid` AS `uuid`,`a`.`create_date` AS `create_date`,`a`.`update_date` AS `update_date`,`a`.`is_active` AS `is_active`,`a`.`class` AS `class`,`a`.`id` AS `id`,`a`.`c_name` AS `c_name`,`a`.`e_name` AS `e_name`,`a`.`week_shift` AS `week_shift`,`a`.`ou_sync_type` AS `ou_sync_type`,`a`.`name_zh_cn` AS `name_zh_cn`,`a`.`concurrent_user` AS `concurrent_user`,`a`.`expired_date` AS `expired_date`,`a`.`sales_attendant_uuid` AS `sales_attendant_uuid`,`atten`.`account` AS `sales_account`,`atten`.`c_name` AS `sales_c_name`,`atten`.`e_name` AS `sales_e_name`,`atten`.`email` AS `sales_email` from (`company` `a` left join `attendant` `atten` on((`a`.`sales_attendant_uuid` = `atten`.`uuid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `department_v`*/;
/*!50001 DROP VIEW IF EXISTS `department_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `department_v` AS select `cmp`.`id` AS `company_id`,`cmp`.`c_name` AS `company_c_name`,`a`.`uuid` AS `uuid`,`a`.`create_date` AS `create_date`,`a`.`update_date` AS `update_date`,`a`.`is_active` AS `is_active`,`a`.`company_uuid` AS `company_uuid`,`a`.`id` AS `id`,`a`.`c_name` AS `c_name`,`a`.`e_name` AS `e_name`,`a`.`parent_department_uuid` AS `parent_department_uuid`,`a`.`manager_uuid` AS `manager_uuid`,`a`.`parent_department_id` AS `parent_department_id`,`a`.`manager_id` AS `manager_id`,`a`.`parent_department_uuid_list` AS `parent_department_uuid_list`,`a`.`s_name` AS `s_name`,`a`.`cost_center` AS `cost_center`,`a`.`src_uuid` AS `src_uuid` from (`department` `a` join `company` `cmp` on((`a`.`company_uuid` = `cmp`.`uuid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `error_log_v`*/;
/*!50001 DROP VIEW IF EXISTS `error_log_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `error_log_v` AS select `error_log`.`uuid` AS `uuid`,`error_log`.`create_date` AS `create_date`,`error_log`.`update_date` AS `update_date`,`error_log`.`is_active` AS `is_active`,`error_log`.`error_code` AS `error_code`,`error_log`.`error_time` AS `error_time`,`error_log`.`error_message` AS `error_message`,`error_log`.`application_name` AS `application_name`,`error_log`.`attendant_uuid` AS `attendant_uuid`,`error_log`.`error_type` AS `error_type`,`error_log`.`is_read` AS `is_read`,`attendant`.`c_name` AS `c_name`,`attendant`.`e_name` AS `e_name`,`attendant`.`id` AS `id` from (`error_log` join `attendant` on((`error_log`.`attendant_uuid` = `attendant`.`uuid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `group_appmenu_v`*/;
/*!50001 DROP VIEW IF EXISTS `group_appmenu_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `group_appmenu_v` AS select `a`.`uuid` AS `uuid`,`a`.`is_active` AS `is_active`,`a`.`create_date` AS `create_date`,`a`.`create_user` AS `create_user`,`a`.`update_date` AS `update_date`,`a`.`update_user` AS `update_user`,`a`.`appmenu_uuid` AS `appmenu_uuid`,`a`.`group_head_uuid` AS `group_head_uuid`,`a`.`is_default_page` AS `is_default_page` from `group_appmenu` `a` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `group_attendant_v`*/;
/*!50001 DROP VIEW IF EXISTS `group_attendant_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `group_attendant_v` AS select `gp_h`.`name_zh_tw` AS `group_name_zh_tw`,`gp_h`.`name_zh_cn` AS `group_name_zh_cn`,`gp_h`.`name_en_us` AS `group_name_en_us`,`gp_h`.`is_active` AS `is_group_active`,`att`.`company_uuid` AS `company_uuid`,`cmp`.`id` AS `company_id`,`cmp`.`c_name` AS `company_c_name`,`cmp`.`e_name` AS `company_e_name`,`gp_h`.`id` AS `group_id`,`gp_h`.`application_head_uuid` AS `application_head_uuid`,`att`.`c_name` AS `attendant_c_name`,`att`.`e_name` AS `attendant_E_name`,`att`.`account` AS `account`,`att`.`email` AS `email`,`att`.`is_active` AS `is_attendant_active`,`gp_a`.`uuid` AS `uuid`,`gp_a`.`create_date` AS `create_date`,`gp_a`.`update_date` AS `update_date`,`gp_a`.`is_active` AS `is_active`,`gp_a`.`group_head_uuid` AS `group_head_uuid`,`gp_a`.`attendant_uuid` AS `attendant_uuid`,`att`.`department_uuid` AS `department_uuid` from (((`group_head` `gp_h` join `group_attendant` `gp_a` on((`gp_h`.`uuid` = `gp_a`.`group_head_uuid`))) join `attendant` `att` on((`gp_a`.`attendant_uuid` = `att`.`uuid`))) join `company` `cmp` on((`att`.`company_uuid` = `cmp`.`uuid`))) where ((`gp_h`.`is_active` = '\0\0\0Y') and (`gp_a`.`is_active` = '\0\0\0Y') and (`att`.`is_active` = '\0\0\0Y') and (`cmp`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `group_head_v`*/;
/*!50001 DROP VIEW IF EXISTS `group_head_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `group_head_v` AS select `gh`.`uuid` AS `uuid`,`gh`.`create_date` AS `create_date`,`gh`.`update_date` AS `update_date`,`gh`.`is_active` AS `is_group_active`,`gh`.`name_zh_tw` AS `name_zh_tw`,`gh`.`name_zh_cn` AS `name_zh_cn`,`gh`.`name_en_us` AS `name_en_us`,`gh`.`company_uuid` AS `company_uuid`,`gh`.`id` AS `id`,`gh`.`create_user` AS `create_user`,`gh`.`update_user` AS `update_user`,`gh`.`application_head_uuid` AS `application_head_uuid`,`ah`.`id` AS `application_id`,`ah`.`name` AS `application_name` from (`group_head` `gh` join `application_head` `ah` on((`gh`.`application_head_uuid` = `ah`.`uuid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `other_authority_application_v`*/;
/*!50001 DROP VIEW IF EXISTS `other_authority_application_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `other_authority_application_v` AS select distinct `app`.`name` AS `Application_Name`,`app`.`id` AS `Application_Id`,`gp_h`.`application_head_uuid` AS `application_head_uuid`,`att`.`c_name` AS `attendant_c_name`,`att`.`e_name` AS `attendant_e_name`,`gp_a`.`attendant_uuid` AS `attendant_uuid`,`att`.`department_uuid` AS `department_uuid` from (((`application_head` `app` join `group_head` `gp_h` on((`app`.`uuid` = `gp_h`.`application_head_uuid`))) join `group_attendant` `gp_a` on((`gp_h`.`uuid` = `gp_a`.`group_head_uuid`))) join `attendant` `att` on((`gp_a`.`attendant_uuid` = `att`.`uuid`))) where ((`app`.`is_active` = '\0\0\0Y') and (`gp_h`.`is_active` = '\0\0\0Y') and (`gp_a`.`is_active` = '\0\0\0Y') and (`att`.`is_active` = '\0\0\0Y')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `sitemap_v`*/;
/*!50001 DROP VIEW IF EXISTS `sitemap_v`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `sitemap_v` AS select `si`.`uuid` AS `uuid`,`si`.`is_active` AS `is_active`,`si`.`create_date` AS `create_date`,`si`.`create_user` AS `create_user`,`si`.`update_date` AS `update_date`,`si`.`update_user` AS `update_user`,`si`.`sitemap_uuid` AS `sitemap_uuid`,`si`.`apppage_uuid` AS `apppage_uuid`,`si`.`root_uuid` AS `root_uuid`,`si`.`haschild` AS `haschild`,`si`.`application_head_uuid` AS `application_head_uuid`,`ap`.`name` AS `name`,`ap`.`description` AS `description`,`ap`.`url` AS `url`,`ap`.`p_mode` AS `p_mode`,`ap`.`parameter_class` AS `parameter_class`,`ap`.`is_active` AS `apppage_is_active` from (`sitemap` `si` join `apppage` `ap` on(((`si`.`apppage_uuid` = `ap`.`uuid`) and (`si`.`application_head_uuid` = `ap`.`application_head_uuid`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `vw_attendant_query`*/;
/*!50001 DROP VIEW IF EXISTS `vw_attendant_query`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_attendant_query` AS select `a`.`uuid` AS `uuid`,`a`.`create_date` AS `create_date`,`a`.`update_date` AS `update_date`,`a`.`is_active` AS `is_active`,`a`.`company_uuid` AS `company_uuid`,`a`.`account` AS `account`,`a`.`c_name` AS `c_name`,`a`.`e_name` AS `e_name`,`a`.`email` AS `email`,`a`.`password` AS `password`,`a`.`is_supper` AS `is_supper`,`a`.`is_admin` AS `is_admin`,`a`.`code_page` AS `code_page`,`a`.`department_uuid` AS `department_uuid`,`d`.`c_name` AS `department_c_name`,`a`.`phone` AS `phone`,`a`.`site_uuid` AS `site_uuid`,`a`.`gender` AS `gender`,`a`.`birthday` AS `birthday`,`a`.`hire_date` AS `hire_date`,`a`.`quit_date` AS `quit_date`,`a`.`is_manager` AS `is_manager`,`a`.`is_direct` AS `is_direct`,`a`.`grade` AS `grade`,`a`.`id` AS `id`,`a`.`src_uuid` AS `src_uuid` from (`attendant` `a` left join `department` `d` on((`a`.`department_uuid` = `d`.`uuid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!50001 DROP TABLE IF EXISTS `vw_department_query`*/;
/*!50001 DROP VIEW IF EXISTS `vw_department_query`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_department_query` AS select `c`.`c_name` AS `company_c_name`,`c`.`uuid` AS `company_uuid`,`c`.`e_name` AS `company_e_name`,`emp`.`account` AS `manager_account`,`emp`.`c_name` AS `manager_c_name`,`d`.`full_department_name` AS `full_department_name`,`d`.`uuid` AS `uuid`,`d`.`id` AS `id`,`d`.`c_name` AS `c_name`,`d`.`e_name` AS `e_name`,`d`.`parent_department_uuid` AS `parent_department_uuid`,`d`.`manager_uuid` AS `manager_uuid`,`d`.`parent_department_id` AS `parent_department_id`,`d`.`manager_id` AS `manager_id`,`d`.`s_name` AS `s_name`,`d`.`cost_center` AS `cost_center`,`d`.`src_uuid` AS `src_uuid` from ((`department` `d` left join `company` `c` on((`d`.`company_uuid` = `c`.`uuid`))) left join `attendant` `emp` on((`emp`.`uuid` = `d`.`manager_uuid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

