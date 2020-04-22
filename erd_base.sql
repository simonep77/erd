/*
SQLyog Ultimate v13.0.1 (64 bit)
MySQL - 8.0.13-4 : Database - ourKl13l8f
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
/*Table structure for table `report_connessioni` */

DROP TABLE IF EXISTS `report_connessioni`;

CREATE TABLE `report_connessioni` (
  `Id` smallint(6) NOT NULL,
  `Nome` varchar(100) NOT NULL,
  `ConnectionString` varchar(300) NOT NULL,
  `BdoDbConnectioType` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_connessioni` */

insert  into `report_connessioni`(`Id`,`Nome`,`ConnectionString`,`BdoDbConnectioType`) values (1,'GestioneSinistri','Server=remotemysql.com;UserId=ourKl13l8f;Password=IXehc1qbkZ;Database=ourKl13l8f;','MYSQLDataBase');

/*Table structure for table `report_destinatari_email` */

DROP TABLE IF EXISTS `report_destinatari_email`;

CREATE TABLE `report_destinatari_email` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `EstrazioneId` int(11) NOT NULL,
  `SmtpConfigId` int(11) NOT NULL DEFAULT '1',
  `Attivo` tinyint(4) NOT NULL DEFAULT '1',
  `MailFROM` text,
  `MailTO` text NOT NULL,
  `MailCC` text,
  `MailBCC` text,
  `MailSUBJ` text NOT NULL,
  `MailBODY` text NOT NULL,
  `Password` varchar(60) DEFAULT NULL,
  `CopyToId` int(11) DEFAULT NULL COMMENT 'Se valorizzato indica che la mail deve contener il link al file depositato piuttosto che l''allegato',
  PRIMARY KEY (`Id`),
  KEY `EstrazioneId` (`EstrazioneId`),
  KEY `CopyToId` (`CopyToId`),
  CONSTRAINT `report_destinatari_email_ibfk_1` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`id`),
  CONSTRAINT `report_destinatari_email_ibfk_3` FOREIGN KEY (`CopyToId`) REFERENCES `report_estrazioni_copyto` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `report_destinatari_email` */

insert  into `report_destinatari_email`(`Id`,`EstrazioneId`,`SmtpConfigId`,`Attivo`,`MailFROM`,`MailTO`,`MailCC`,`MailBCC`,`MailSUBJ`,`MailBODY`,`Password`,`CopyToId`) values (1,1,1,1,'simonep@fastwebnet.it','simone.pelaia@gmail.com',NULL,NULL,'Report Test 1','In allegato',NULL,NULL),(2,2,1,2,'simonep@fastwebnet.it','simone.pelaia@gmail.com',NULL,NULL,'Report Test 2','',NULL,1),(3,2,1,3,'simonep@fastwebnet.it','simone.pelaia@gmail.com',NULL,NULL,'Report Link','In allegato','1234',NULL);

/*Table structure for table `report_estrazioni` */

DROP TABLE IF EXISTS `report_estrazioni`;

CREATE TABLE `report_estrazioni` (
  `Id` int(11) NOT NULL,
  `Nome` varchar(100) NOT NULL,
  `Titolo` text,
  `Note` text,
  `Attivo` tinyint(4) DEFAULT NULL,
  `ConnessioneId` smallint(6) NOT NULL,
  `TipoFileId` tinyint(4) NOT NULL DEFAULT '1',
  `TemplateId` int(11) DEFAULT NULL,
  `SheetName` varchar(30) DEFAULT NULL,
  `SqlText` mediumtext NOT NULL,
  `CronString` varchar(50) DEFAULT NULL,
  `DataInizio` date NOT NULL DEFAULT '2001-01-01',
  `DataFine` date NOT NULL DEFAULT '9999-12-31',
  `NumOutputStorico` smallint(6) NOT NULL DEFAULT '10',
  `EstrazioniAccorpateIds` varchar(50) DEFAULT NULL COMMENT 'Eventuali Id di altre estrazioni separati da virgola da eseguire contestualemnte ed accorpare (solo Excel)',
  `InvioMailAttivo` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `ConnessioneId` (`ConnessioneId`),
  KEY `TipoFileId` (`TipoFileId`),
  KEY `TemplateId` (`TemplateId`),
  CONSTRAINT `report_estrazioni_ibfk_1` FOREIGN KEY (`ConnessioneId`) REFERENCES `report_connessioni` (`id`),
  CONSTRAINT `report_estrazioni_ibfk_2` FOREIGN KEY (`TipoFileId`) REFERENCES `report_tipi_file` (`id`),
  CONSTRAINT `report_estrazioni_ibfk_3` FOREIGN KEY (`TemplateId`) REFERENCES `report_templates` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_estrazioni` */

insert  into `report_estrazioni`(`Id`,`Nome`,`Titolo`,`Note`,`Attivo`,`ConnessioneId`,`TipoFileId`,`TemplateId`,`SheetName`,`SqlText`,`CronString`,`DataInizio`,`DataFine`,`NumOutputStorico`,`EstrazioniAccorpateIds`,`InvioMailAttivo`) values (1,'Prova','Estrazione test',NULL,1,1,2,NULL,'Test','SELECT *\r\nFROM report_tipi_file','10 0 * * *','2001-01-01','9999-12-31',10,NULL,1),(2,'Prova 2','Estrazione test 2',NULL,1,1,2,NULL,'Test2','SELECT *\r\nFROM report_tipi_file','10 0 * * *','2001-01-01','9999-12-31',10,NULL,1);

/*Table structure for table `report_estrazioni_copyto` */

DROP TABLE IF EXISTS `report_estrazioni_copyto`;

CREATE TABLE `report_estrazioni_copyto` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `EstrazioneId` int(11) NOT NULL,
  `Path` varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Può includere l''interopolazione {output.xxx} dove output è l''oggetto con il risultato dell''esecuzione',
  `User` varchar(80) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'Da valorizzare solo se diverse da quelle di esecuzione',
  `Pass` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'Da valorizzare solo se diverse da quelle di esecuzione',
  PRIMARY KEY (`Id`),
  KEY `EstrazioneId` (`EstrazioneId`),
  CONSTRAINT `report_estrazioni_copyto_ibfk_1` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `report_estrazioni_copyto` */

insert  into `report_estrazioni_copyto`(`Id`,`EstrazioneId`,`Path`,`User`,`Pass`) values (1,2,'D:\\DESKTOP\\aaa','sminky','volley2');

/*Table structure for table `report_estrazioni_output` */

DROP TABLE IF EXISTS `report_estrazioni_output`;

CREATE TABLE `report_estrazioni_output` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `EstrazioneId` int(11) NOT NULL,
  `StatoId` tinyint(4) NOT NULL,
  `EstrazioneEsito` text,
  `DataOraInizio` datetime NOT NULL,
  `DataOraFine` datetime DEFAULT NULL,
  `TipoFileId` tinyint(4) NOT NULL,
  `NomeFile` varchar(80) NOT NULL,
  `DataLen` int(11) DEFAULT NULL,
  `DataBlob` mediumblob,
  `MailEsito` text,
  `MailDataInvio` datetime DEFAULT NULL,
  `DataInserimento` datetime NOT NULL,
  `DataAggiornamento` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `TipoFileId` (`TipoFileId`),
  KEY `EstrazioneId` (`EstrazioneId`),
  KEY `StatoId` (`StatoId`),
  CONSTRAINT `report_estrazioni_output_ibfk_1` FOREIGN KEY (`TipoFileId`) REFERENCES `report_tipi_file` (`id`),
  CONSTRAINT `report_estrazioni_output_ibfk_2` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`id`),
  CONSTRAINT `report_estrazioni_output_ibfk_3` FOREIGN KEY (`StatoId`) REFERENCES `report_estrazioni_output_stati` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_estrazioni_output` */

/*Table structure for table `report_estrazioni_output_stati` */

DROP TABLE IF EXISTS `report_estrazioni_output_stati`;

CREATE TABLE `report_estrazioni_output_stati` (
  `Id` tinyint(4) NOT NULL,
  `Nome` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `report_estrazioni_output_stati` */

insert  into `report_estrazioni_output_stati`(`Id`,`Nome`) values (1,'In esecuzione'),(2,'Completato con successo'),(3,'Completato con errori');

/*Table structure for table `report_smtp_configs` */

DROP TABLE IF EXISTS `report_smtp_configs`;

CREATE TABLE `report_smtp_configs` (
  `Id` int(11) NOT NULL,
  `Nome` varchar(50) NOT NULL,
  `Smtp` varchar(255) NOT NULL,
  `Port` int(11) NOT NULL DEFAULT '25',
  `UseSSL` tinyint(1) DEFAULT '0',
  `Auth` tinyint(1) NOT NULL DEFAULT '0',
  `UserName` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Note` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_smtp_configs` */

insert  into `report_smtp_configs`(`Id`,`Nome`,`Smtp`,`Port`,`UseSSL`,`Auth`,`UserName`,`Password`,`Note`) values (1,'Gmail','smtp.fastwebnet.it',587,1,1,'simonep@fastwebnet.it','tT3X5hs4',NULL);

/*Table structure for table `report_templates` */

DROP TABLE IF EXISTS `report_templates`;

CREATE TABLE `report_templates` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `TemplateBlob` mediumblob NOT NULL,
  `Note` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `report_templates` */

insert  into `report_templates`(`Id`,`Nome`,`TemplateBlob`,`Note`) values (1,'Template Prova con tabellina e aggregatori','PK\0\0\0\0\0!\0A7��n\0\0\0\0\0[Content_Types].xml �(�\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�T�n�0�W�?D�V������[$��x�X$��(�}\'fQU��%Ql�[&�<��&YB@�l.�YO$`���r�=�H�E���V����5�\r��ӵL��b.j\"�\"%5�\n3���N�B��?C%�*����=��YK)ub8x�R-\ZJ�W��Q23V$��sU.���)�P���I����]�h:C@i��m23�	�1� g�/#ݺʸ2\n��x|`�G��㮶u_�;�ѐ�U�Oղw�j��s��4ȥ��-�Ze�N�	�xe|�o,����� �1��y��s�i�\0޺��s��	��V7�����88���\0�wa��:���Crh������ݝ�A����\0\0��\0PK\0\0\0\0\0!\0�U0#�\0\0\0L\0\0\0_rels/.rels �(�\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0��MO�0��H�����ݐBKwAH�!T~�I����$ݿ\'T\Z�G�~����<���!��4��;#�w����qu*&r�Fq���v�����GJy(v��*����K��#F��D��.W\Z	��=��Z�MY�b���BS���7��ϛז��\r?�9L�ҙ�sbgٮ|�l!��\ZUSh9i�b�r:\"y_dl��D���|-N��R\"4�2�G�%��Z�4�˝y�7	ë��ɂ���\0\0��\0PK\0\0\0\0\0!\0��-|\0\0/\0\0\0\0\0xl/workbook.xml�U�n�8}_`�AK�U�(S�%�.��@�\r�4}	0m�D-E���{����MQd�5l�䈇gfΌ��vUi<2�rQ������L�^���Ԝ �U��i)j6CO�E���1�\n�ybc\0@��P�TXV����hX\r���U��k�m$�y[0���rl۳*�k4 �5b���E�U�V�d%U@�-x�Ъ�5p���13Q5\0��K��zPdTY�\\�B҇��a��I�z��6��&0�����X�K���/�Ƕ��Yv/c�:$bI��u����FV��{��o�a�V��\0��F4���A�銗�v��A��#�t�Jd��UI��ghK�eg�k�`u���Ț�|%���hW��*��|��O�0�b���E�V�ý_���;*(ܸf�v\\2(,��\n#���^QU�,g(\n���������?����%��]җE��I3�����?��dpPߕ��_��!��#�����u	ǣ�:����lL�06�p��%��#��S?!8��	q��3�2A;U�S��g����>�������3����c����`���M햳m�,\n�4v_x�����������7~�*�I����a�o��0Ƙ@&u���f9d�L��hd�x�0~��pvAp��8\Z���J}�j�lԽ�S�.��Шuo��K�2�}�@ܼf��@9Y�b�h�G��bqA���Z\'���a2ZfP8z�o��=T۩���Oa�rp\ZR��>1�d�d�;愌3\"����$N�>������/����,*Ս���q�l�d:D	x��\r�Ih��\"I1����1�8�cG��j�d���76��՟fTuP����u��t�{�\\\r���Utp�t�O�������O��8�Ϟu���;\0\0\0��\0PK\0\0\0\0\0!\0�>���\0\0\0�\0\0\Z\0xl/_rels/workbook.xml.rels �(�\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�RMK�0���0w�v�t/\"�U�ɴ)�&!3~��*�]X�K/o�y���v�5���+��zl�;o���b��G�����s�>��,�8��(%���\"D��҆4j�0u2js��MY�˴���S쭂��� �)f���C����y��	I<\ry\0���!+��E���fMy�k�����K�5=|�t ��G)�s墙�U��tB��)���,���f�����\0\0\0��\0PK\0\0\0\0\0!\0f����\0\0C\0\0\0\0\0xl/worksheets/sheet1.xml�Uێ�0}�����!$(d�����n/ώkS۹l��{ǰa�d�F+�\0��9g�30�;�ك�BV)�����D�I��/�ސmX��BV��\'��n���� գ���N�֘:q]ͷP2��\Z*|�KU2��j��Z˚��p��%mu��s�a!���ʴ$\n\nfпފZ��J~]����qY�H��0O\r)%%O�7�Tl]`�G��89*�x�\'�f�J�\\I-s� ��z�N�\\�;���o��������B�͒u\\�Y�F�AGf˥���R�k1���lڛ��U�����r��h>ī`�{�o:gw�fE�)���2��d���7}vM[?@�\0j����\\K�h����!�n\0��q#�0��@�v��Vc`�N�����j\Z��\"�lW�����fkP6�4m�$��4�Ea\'hlsY ��R�I�c�֪��6���^�#��6��ޮ��S��������|��(�/�����sd�;����UI�u�c����<lR��kfG>H��1Z�ؙ��O	�B��\'���ca�3b~B�,mȢ]hӶ˳=tF��Apg�a���!��\"BnW�`����{�^���ݮ{^��B����u-��۵��,��k�m�Ԧ�7M��ٹ���nı���\'�m�#SQiR@ތTL�jg�s�������48:��-~o\0[�s�Թ��t�\"��̮&5�A=����Q\"���m>()��2�	�z�}{���u�}�&\0\0\0��\0PK\0\0\0\0\0!\0	�V\0\0� \0\0\0\0\0xl/theme/theme1.xml�Y[�5~G�?X��6�˪)ʵK��Vݴ�Go�d���#��m�*��� ^�x�!�@��Ԋˏ��3���^�\"@��V�;���>s|���	C�DH��NP�R	I\'|J�y\'�7�Z�\n�S�xJ:����ko�q縉$�|*�p\'��Z��r�X^���o3.��Q��S��@o�ʵJ�QN0M��ԎAM)�=��		���̑*�&Li�$���ӓ�Fȕ�3�N1�0Ӕ���C ���:A���kW�x/bj��%72�\\.0=��9��x3iFa���o\0Lm��ac���3\0<��J3[\\��Z?̱(���=h�Uo�o�܍���P�?�F}�7�m�^�7p�P�olᛕ� l:�\r(f4=�BW�F��^�2�l�oG�Y˕(ȆMv�)f<U�r-��@V4Ej� 3<�<�cF�EtC�-p�%Wj�Q���\'4�LD�����,�[C�$\'�.T\'�Z�����<�����|������sU��>N���_}���߾���O>ͦ>��6��7>����R+.\\���������?���O<ڻ��1M�D����X��~r,^Nbc�H�t{TU�\0o�0��z�u�},�^_>pl=��RQ��7��r�z\\xpS�eyx�L����������7w�N����+�����1�é�s���o����ޥ���!�.�L�w)�a�uɘ;�T�����v|sx�8�z@N]$l�<Ə	s�x/N|*�8a����}F����\r��H�	�h8%R�dnX����0���U�\"��\'>��s9�\'�\'��4�m���R�;\\�����!��ӝ�O����= Wۤ\"A�/K��u����b3L|,��î]A���[Ν�> ��3<%�{�cA�/�F߈�U��/�n`7W�sJ$A��٦�*��=\"s�Þ��9�Y�4�b��[u\'u��R�m69���(��/^�ܖ��J��.�wb�]�Y��u%�����}��e�%Ȑ��baߌ1s&(f�����-�8�/D��jĖ^���i�0@a��;	M�[��+{����0P����Rg��+pv���e�\0/�;N�mκ�j.���_���˗��e-sY��޾^K-S�/P�]��Iv�|f��#�b�@����7��M;��$7-�E_����ld�����/�5T5�ι�U�%Zp	#3l���n�wZ&�|�u:�U���\\(�*�+�f�T*C7�E�n���C�˺6@˾��d�u��� Dᯌ0+�+�+ZZ�:T�(n\\�m���^�;Afdh�Ay>�qʚ�����\\h�w9��\0%�:�H���;��W���D�1�J7�+\rcxγ�n�_d��EH�+ֻ�0��z��$r�Xj3K�Y\'h�#�W��E\'�A��&��ߺ0����D�lÿ\n�,�T,���t26H�\"1�t��M6��p���ZB��\Z�Z��Aw�Lf32Qvح���>�\n�F���Z�/!�G������!ŢfU;pJ%\\T3oN)܄m��ȿsSN��U�ɡl�E���&�nHtc�y���z���v��\\���}�Q�=g�fqf:��OM?���C޲�8D�2�6�Բ���� Q���sN�8,ӊ�Ӵ��4�9;uM�����Dc��6g���z�����ĺ�4�o.��[m~�\0�c\0��K��	%�YE_v��l��*��Z\n�	ޫDݰ_���J+\Z��zX)��n�ԍ�zuU+�^�,*N�Qva?�+�ʯ�����}����2�I��+��1�\\�Wk��}v\r���f>@H�FmԮ�{�R����A�Uj��Ҡ�oF�~�j��Ԁ�n�6��R����FE��j��a��\r���0�>��XyF�/��Ʈk\0\0��\0PK\0\0\0\0\0!\0ޛ?1\0\0�\0\0\r\0\0\0xl/styles.xml�Vێ�0}G�,�gsiS�*	��FBZ�.�n��D�����w�Nڦ���%��>s�s���e\'8���eJ�8�\n0��P%�u�������Y�$M��e��YҚ��;J\r٦xgL�����QA�+�P	+�҂���oMI��C��Q�|A��=�R\"���o�B���e�����H�׵T�l9P��))P�t�:}t����ЪU��\\_U+��t��\'�	���~]���G\"M}M�MΒJIӢB�IqD�,?J�I�v	2<�ʒ����YR(�42�:�9g�D�~ǚp���n��`�Л#kp��	wo���ѳɒ����}9�-�d��n`b�C��T�2�	\Z�w�B��Ʝ���ݵ&�0���@�8+-�z�.X����f;,0YҎ�)�M����OG�} ƭ�%T�1�6��)K8��jV��ר��Pƀ���d�V�p���a\0����V������܋\\��@����x\r���X�1Z�=�����â�:�?�4\"M��8����P��B�C�PP�(Otz@[`k������Y�@=���Ni�\0m!@����UO��D�3�V0Np���7q:��6F���IHȾH)~k\n��\rj@�=��h@��fWq�v��uI+������\r-�^@��]�ؽ2\"���-�pfo�v榅�h�Y�?_�^,6�y�̓�ܛNh�-��Ƌ���f�/�(X��\'t(�RA?�t�r�czv {��x4��;-\0�1�E4^�a�� ��32��I��qmf��u��#��#;Y��a�-�xi����c��\Z[!I0�E�1���J�\0\0��\0PK\0\0\0\0\0!\0N��[�\0\0\0�\0\0\0\0\0xl/sharedStrings.xmll�MK1���������AD��A�xZO�N�LaڎM����vWWa�@y����o�ɧ�ᴪA`l����=�\0Al��}���	���@�(�H\Z:��RJj:��4`,�%�`����4d��:D�<��s�� �4F�P��ѿ�x���\"o�-%�(�!?t��`�n,�j���x���Ubۋ��i?��#�9x��Mӷ�O�󋞯6M)�pH͢-���5cر�Ӳ��|\0\0��\0PK\0\0\0\0\0!\0;m2K�\0\0\0B\0\0#\0\0\0xl/worksheets/_rels/sheet1.xml.rels�����0E��Cx{�օCS7\"�U�b��ۗ���{�e���p��6��<�f�,Ժ��ch��{�-�A�8��	-<�a�.��NNJ�ǐX��Q$��~�ٱ�	��>��I�y0���Ь�jm�_�/N��,�}W�:=RY��}<n���H�τI9�`>�H9�E��bA�w��k}�m����	\0\0��\0PK\0\0\0\0\0!\0���\0\0�$\0\0\'\0\0\0xl/printerSettings/printerSettings1.bin엽JA�������DP�����ڨH�)\r9AT�SXYYI������G�D����]��٘�3aogg3ss�����AY��\"�b%�Sk\Z9��8�����1���AT�WO�d�zWН�P�Ũ6�:�4�>�lq*�4/W��<]v���o��:�F\Z���$�7��H#� V��Z����z��-�Յ���)�\n��:�,.Iw3�͙�%�H\n�������۲��H�*�-�i��Vg��\'$�7ƵmH_����y�p��TIXa��|\\�(S�u\n4K��_�m�ʨ����{.�x�n�fM����I��O�$ȩ���~��64��h��mRt�%��AP���+���@�x�x�\"^;)�m2	ZE͛��A1�=��m@�x��-ؿu��ߗ�_���ڭ��j�;��L��0��)�k,���-c_\rmd��v�O^gt�:�\'�y�)��<�^i��Y��vPnB@�.%��8��|���D�o?ʨ\n! ��B@! ����>\0\0\0��\0PK\0\0\0\0\0!\0V#��E\0\0k\0\0\0docProps/core.xml �(�\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0��QO� ��M�\r�-��s!m��ٓK�lF����X(�ۿ���V烏p��8�|qTU���Z(M�@�ZH�/��v�Q�<ӂU�����Ey}�sCymamk�KpQ iG�)��{C1v�\0��$8tw�U̇��c�����a�	�n����|@�w[u\0�1T�@{��$��^V�?:e�TҟL�t�;fދ����`l�&i&]��?�/��MW5���T�Sn��ږk��d�F�����v�s~�� �N�͗�@��xQ�F�\"_����a�DeF2�i�e��9��^��̷Q�uN�O�-%)���_�2�ߣ�\0\0��\0PK\0\0\0\0\0!\0~����\0\0�\0\0\0docProps/app.xml �(�\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�SMo�0��0to�tE1��!]� i�L\'�dI�X#ٯm���;�7~���RW���l���rQ��	���J��n�>�\"�\Z\\�X�fq�߿S�\"&�������2������iBj��M{��\Z��EO�,/%	}��Y�\0ň���A�`z~�~w�LX�O1:k�xJ�͚rh��|4蔜\'�ۢyL�N�Tr�k�\r��J>�-B/�l�Zu���PHE�?Y�Q<@ƞN%:H<1��lt��LI߄��Em]HAI�\Z3�90��^l��p���	S�o�>�ֳ:o�Փ\'g/5�Yr��7H�D�s���@3Q�8�o1&Y�_c>����m��d}��G����8��ٗA�=@�a��P����z���맚��ޏ�M//凒OlS��[�_\0\0\0��\0PK-\0\0\0\0\0\0!\0A7��n\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0[Content_Types].xmlPK-\0\0\0\0\0\0!\0�U0#�\0\0\0L\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�\0\0_rels/.relsPK-\0\0\0\0\0\0!\0��-|\0\0/\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�\0\0xl/workbook.xmlPK-\0\0\0\0\0\0!\0�>���\0\0\0�\0\0\Z\0\0\0\0\0\0\0\0\0\0\0\0\0u\n\0\0xl/_rels/workbook.xml.relsPK-\0\0\0\0\0\0!\0f����\0\0C\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�\0\0xl/worksheets/sheet1.xmlPK-\0\0\0\0\0\0!\0	�V\0\0� \0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�\0\0xl/theme/theme1.xmlPK-\0\0\0\0\0\0!\0ޛ?1\0\0�\0\0\r\0\0\0\0\0\0\0\0\0\0\0\0\0P\0\0xl/styles.xmlPK-\0\0\0\0\0\0!\0N��[�\0\0\0�\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�\Z\0\0xl/sharedStrings.xmlPK-\0\0\0\0\0\0!\0;m2K�\0\0\0B\0\0#\0\0\0\0\0\0\0\0\0\0\0\0\0�\0\0xl/worksheets/_rels/sheet1.xml.relsPK-\0\0\0\0\0\0!\0���\0\0�$\0\0\'\0\0\0\0\0\0\0\0\0\0\0\0\0�\0\0xl/printerSettings/printerSettings1.binPK-\0\0\0\0\0\0!\0V#��E\0\0k\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0)\0\0docProps/core.xmlPK-\0\0\0\0\0\0!\0~����\0\0�\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0�!\0\0docProps/app.xmlPK\0\0\0\0\0\0&\0\0�$\0\0\0\0',NULL);

/*Table structure for table `report_tipi_file` */

DROP TABLE IF EXISTS `report_tipi_file`;

CREATE TABLE `report_tipi_file` (
  `Id` tinyint(4) NOT NULL,
  `Nome` varchar(50) NOT NULL,
  `Estensione` varchar(10) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_tipi_file` */

insert  into `report_tipi_file`(`Id`,`Nome`,`Estensione`) values (1,'CSV','.csv'),(2,'EXCEL','.xlsx');

/*Table structure for table `report_tipi_notifica` */

DROP TABLE IF EXISTS `report_tipi_notifica`;

CREATE TABLE `report_tipi_notifica` (
  `Id` tinyint(4) NOT NULL,
  `Nome` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_tipi_notifica` */

insert  into `report_tipi_notifica`(`Id`,`Nome`) values (1,'Nessuna'),(2,'Email con allegato'),(3,'Email con link');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
