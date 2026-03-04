/*
SQLyog Community v13.1.9 (64 bit)
MySQL - 8.0.29 : Database - report_dispatcher_svil
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
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `ConnectionString` varchar(300) NOT NULL,
  `BdoDbConnectioType` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

/*Data for the table `report_connessioni` */


/*Table structure for table `report_destinatari_email` */

DROP TABLE IF EXISTS `report_destinatari_email`;

CREATE TABLE `report_destinatari_email` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EstrazioneId` int NOT NULL,
  `SmtpConfigId` int NOT NULL DEFAULT '1',
  `Attivo` tinyint NOT NULL DEFAULT '1',
  `MailFROM` text,
  `MailTO` text NOT NULL,
  `MailCC` text,
  `MailBCC` text,
  `MailSUBJ` text NOT NULL,
  `MailBODY` text NOT NULL,
  `Password` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `EstrazioneId` (`EstrazioneId`),
  CONSTRAINT `report_destinatari_email_ibfk_1` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_destinatari_email` */


/*Table structure for table `report_estrazioni` */

DROP TABLE IF EXISTS `report_estrazioni`;

CREATE TABLE `report_estrazioni` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Titolo` text,
  `Gruppo` varchar(100) DEFAULT NULL,
  `Note` text,
  `Attivo` tinyint DEFAULT NULL,
  `ConnessioneId` int NOT NULL,
  `TipoFileId` tinyint NOT NULL DEFAULT '1',
  `TemplateId` int DEFAULT NULL,
  `SheetName` varchar(30) DEFAULT NULL,
  `SqlText` mediumtext NOT NULL,
  `CronString` varchar(50) DEFAULT NULL,
  `DataInizio` date NOT NULL DEFAULT '2001-01-01',
  `DataFine` date NOT NULL DEFAULT '9999-12-31',
  `NumOutputStorico` smallint NOT NULL DEFAULT '10',
  `EstrazioniAccorpateIds` varchar(100) DEFAULT NULL COMMENT 'Eventuali Id di altre estrazioni separati da virgola da eseguire contestualemnte ed accorpare (solo Excel)',
  `AccorpaSoloDati` tinyint NOT NULL DEFAULT '0' COMMENT 'Esegue accorpamento dei dati (che devono avere identica struttura)',
  `InvioMailAttivo` tinyint NOT NULL DEFAULT '1',
  `CopyToPath` mediumtext COMMENT 'Percorso (fisico, UNC o hfs) comprensivo di nome file e variabili format per data',
  `NomeFileMask` varchar(150) DEFAULT NULL COMMENT 'Se impostato consente di personalizzare il nome del file generato. Eventuali parti dinamiche per data devono utilizzare lo standard di formattazione .NET',
  `UtenteIdInserimento` int NOT NULL,
  `UtenteIdAggiornamento` int NOT NULL,
  `DataInserimento` datetime NOT NULL DEFAULT '2020-01-01 00:00:00',
  `DataAggiornamento` datetime NOT NULL DEFAULT '9999-12-31 00:00:00',
  PRIMARY KEY (`Id`),
  KEY `ConnessioneId` (`ConnessioneId`),
  KEY `TipoFileId` (`TipoFileId`),
  KEY `TemplateId` (`TemplateId`),
  CONSTRAINT `report_estrazioni_ibfk_1` FOREIGN KEY (`ConnessioneId`) REFERENCES `report_connessioni` (`Id`),
  CONSTRAINT `report_estrazioni_ibfk_2` FOREIGN KEY (`TipoFileId`) REFERENCES `report_tipi_file` (`Id`),
  CONSTRAINT `report_estrazioni_ibfk_3` FOREIGN KEY (`TemplateId`) REFERENCES `report_templates` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_estrazioni` */

/*Table structure for table `report_estrazioni_output` */

DROP TABLE IF EXISTS `report_estrazioni_output`;

CREATE TABLE `report_estrazioni_output` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `EstrazioneId` int NOT NULL,
  `StatoId` tinyint NOT NULL,
  `EstrazioneEsito` text,
  `DataOraInizio` datetime NOT NULL,
  `DataOraFine` datetime DEFAULT NULL,
  `TipoFileId` tinyint NOT NULL,
  `NomeFile` varchar(80) NOT NULL,
  `DataLen` int DEFAULT NULL,
  `DataBlob` mediumblob,
  `MailEsito` text,
  `MailDataInvio` datetime DEFAULT NULL,
  `DataInserimento` datetime NOT NULL,
  `DataAggiornamento` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `TipoFileId` (`TipoFileId`),
  KEY `EstrazioneId` (`EstrazioneId`),
  KEY `StatoId` (`StatoId`),
  CONSTRAINT `report_estrazioni_output_ibfk_1` FOREIGN KEY (`TipoFileId`) REFERENCES `report_tipi_file` (`Id`),
  CONSTRAINT `report_estrazioni_output_ibfk_2` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`Id`),
  CONSTRAINT `report_estrazioni_output_ibfk_3` FOREIGN KEY (`StatoId`) REFERENCES `report_estrazioni_output_stati` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_estrazioni_output` */


/*Table structure for table `report_estrazioni_output_stati` */

DROP TABLE IF EXISTS `report_estrazioni_output_stati`;

CREATE TABLE `report_estrazioni_output_stati` (
  `Id` tinyint NOT NULL,
  `Nome` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_estrazioni_output_stati` */

insert  into `report_estrazioni_output_stati`(`Id`,`Nome`) values (1,'In esecuzione'),(2,'Completato con successo'),(3,'Completato con errori');

/*Table structure for table `report_estrazioni_sqlhistory` */

DROP TABLE IF EXISTS `report_estrazioni_sqlhistory`;

CREATE TABLE `report_estrazioni_sqlhistory` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `EstrazioneId` int NOT NULL,
  `UtenteId` int NOT NULL,
  `SqlText` mediumtext NOT NULL,
  `DataInserimento` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `EstrazioneId` (`EstrazioneId`),
  CONSTRAINT `report_estrazioni_sqlhistory_ibfk_1` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


/*Table structure for table `report_piano_schedulazione` */

DROP TABLE IF EXISTS `report_piano_schedulazione`;

CREATE TABLE `report_piano_schedulazione` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `EstrazioneId` int NOT NULL,
  `DataEsecuzione` datetime NOT NULL,
  `StatoId` tinyint NOT NULL,
  `OutputId` bigint DEFAULT NULL,
  `DataInserimento` datetime NOT NULL,
  `DataAggiornamento` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `EstrazioneId` (`EstrazioneId`),
  KEY `StatoId` (`StatoId`),
  KEY `OutputId` (`OutputId`),
  CONSTRAINT `report_piano_schedulazione_ibfk_1` FOREIGN KEY (`EstrazioneId`) REFERENCES `report_estrazioni` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `report_piano_schedulazione_ibfk_2` FOREIGN KEY (`StatoId`) REFERENCES `report_piano_schedulazione_stati` (`Id`),
  CONSTRAINT `report_piano_schedulazione_ibfk_3` FOREIGN KEY (`OutputId`) REFERENCES `report_estrazioni_output` (`Id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `report_piano_schedulazione_stati` */

DROP TABLE IF EXISTS `report_piano_schedulazione_stati`;

CREATE TABLE `report_piano_schedulazione_stati` (
  `Id` tinyint NOT NULL,
  `Nome` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_piano_schedulazione_stati` */

insert  into `report_piano_schedulazione_stati`(`Id`,`Nome`) values (1,'Pianificato'),(2,'In esecuzione'),(3,'Eseguito'),(4,'Saltato'),(5,'Esito non registrato');

/*Table structure for table `report_smtp_configs` */

DROP TABLE IF EXISTS `report_smtp_configs`;

CREATE TABLE `report_smtp_configs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL,
  `Smtp` varchar(255) NOT NULL,
  `Port` int NOT NULL DEFAULT '25',
  `UseSSL` tinyint(1) DEFAULT '0',
  `Auth` tinyint(1) NOT NULL DEFAULT '0',
  `UserName` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Note` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


/*Table structure for table `report_templates` */

DROP TABLE IF EXISTS `report_templates`;

CREATE TABLE `report_templates` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(200) NOT NULL,
  `TemplateBlob` mediumblob NOT NULL,
  `Note` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


/*Table structure for table `report_tipi_file` */

DROP TABLE IF EXISTS `report_tipi_file`;

CREATE TABLE `report_tipi_file` (
  `Id` tinyint NOT NULL,
  `Nome` varchar(50) NOT NULL,
  `Estensione` varchar(10) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_tipi_file` */

insert  into `report_tipi_file`(`Id`,`Nome`,`Estensione`) values (1,'CSV','.csv'),(2,'EXCEL','.xlsx');

/*Table structure for table `report_tipi_notifica` */

DROP TABLE IF EXISTS `report_tipi_notifica`;

CREATE TABLE `report_tipi_notifica` (
  `Id` tinyint NOT NULL,
  `Nome` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `report_tipi_notifica` */

insert  into `report_tipi_notifica`(`Id`,`Nome`) values (1,'Nessuna'),(2,'Email con allegato'),(3,'Email con link');

/*Table structure for table `report_utenti` */

DROP TABLE IF EXISTS `report_utenti`;

CREATE TABLE `report_utenti` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `Dominio` varchar(100) NOT NULL,
  `Nominativo` varchar(200) NOT NULL,
  `Email` varchar(200) DEFAULT NULL,
  `DataInserimento` datetime NOT NULL DEFAULT '2001-01-01 00:00:00',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Username` (`Username`,`Dominio`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
