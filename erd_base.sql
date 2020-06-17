/*
SQLyog Ultimate v13.0.1 (64 bit)
MySQL - 5.7.19-enterprise-commercial-advanced-log : Database - report_dispatcher
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
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `ConnectionString` varchar(300) NOT NULL,
  `BdoDbConnectioType` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

/*Data for the table `report_connessioni` */


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
  CONSTRAINT `report_destinatari_email_ibfk_1` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`Id`),
  CONSTRAINT `report_destinatari_email_ibfk_3` FOREIGN KEY (`CopyToId`) REFERENCES `report_estrazioni_copyto` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;

/*Data for the table `report_destinatari_email` */

/*Table structure for table `report_estrazioni` */

DROP TABLE IF EXISTS `report_estrazioni`;

CREATE TABLE `report_estrazioni` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Titolo` text,
  `Gruppo` varchar(100) DEFAULT NULL,
  `Note` text,
  `Attivo` tinyint(4) DEFAULT NULL,
  `ConnessioneId` int(11) NOT NULL,
  `TipoFileId` tinyint(4) NOT NULL DEFAULT '1',
  `TemplateId` int(11) DEFAULT NULL,
  `SheetName` varchar(30) DEFAULT NULL,
  `SqlText` mediumtext NOT NULL,
  `CronString` varchar(50) DEFAULT NULL,
  `DataInizio` date NOT NULL DEFAULT '2001-01-01',
  `DataFine` date NOT NULL DEFAULT '9999-12-31',
  `NumOutputStorico` smallint(6) NOT NULL DEFAULT '10',
  `EstrazioniAccorpateIds` varchar(50) DEFAULT NULL COMMENT 'Eventuali Id di altre estrazioni separati da virgola da eseguire contestualemnte ed accorpare (solo Excel)',
  `AccorpaSoloDati` tinyint(4) NOT NULL DEFAULT '0' COMMENT 'Esegue accorpamento dei dati (che devono avere identica struttura)',
  `InvioMailAttivo` tinyint(4) NOT NULL DEFAULT '1',
  `CopyToPath` mediumtext COMMENT 'Percorso (fisico, UNC o hfs) comprensivo di nome file e variabili format per data',
  PRIMARY KEY (`Id`),
  KEY `ConnessioneId` (`ConnessioneId`),
  KEY `TipoFileId` (`TipoFileId`),
  KEY `TemplateId` (`TemplateId`),
  CONSTRAINT `report_estrazioni_ibfk_1` FOREIGN KEY (`ConnessioneId`) REFERENCES `report_connessioni` (`Id`),
  CONSTRAINT `report_estrazioni_ibfk_2` FOREIGN KEY (`TipoFileId`) REFERENCES `report_tipi_file` (`id`),
  CONSTRAINT `report_estrazioni_ibfk_3` FOREIGN KEY (`TemplateId`) REFERENCES `report_templates` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;

/*Data for the table `report_estrazioni` */


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
  CONSTRAINT `report_estrazioni_output_ibfk_2` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`Id`),
  CONSTRAINT `report_estrazioni_output_ibfk_3` FOREIGN KEY (`StatoId`) REFERENCES `report_estrazioni_output_stati` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;


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
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL,
  `Smtp` varchar(255) NOT NULL,
  `Port` int(11) NOT NULL DEFAULT '25',
  `UseSSL` tinyint(1) DEFAULT '0',
  `Auth` tinyint(1) NOT NULL DEFAULT '0',
  `UserName` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Note` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `report_smtp_configs` */


/*Table structure for table `report_templates` */

DROP TABLE IF EXISTS `report_templates`;

CREATE TABLE `report_templates` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `TemplateBlob` mediumblob NOT NULL,
  `Note` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `report_templates` */

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
