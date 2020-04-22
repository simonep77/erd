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
  `Path` varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Pu√≤ includere l''interopolazione {output.xxx} dove output √® l''oggetto con il risultato dell''esecuzione',
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

insert  into `report_templates`(`Id`,`Nome`,`TemplateBlob`,`Note`) values (1,'Template Prova con tabellina e aggregatori','PK\0\0\0\0\0!\0A7Çœn\0\0\0\0\0[Content_Types].xml ¢(†\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0¨T…n¬0ΩWÍ?DæVâ°á™™∫[$ËòxíX$∂Â(¸}\'fQU±¡%Qlœ[&Û<≠⁄&YB@„l.˙YO$`ßç≠rÒ=˝HüEÇ§¨Vç≥êã5†\rÔÔ”µL∏⁄b.j\"ˇ\"%5¥\n3Á¡ÚNÈB´à?C%Ω*Ê™˘ÿÎ=…¬YK)ub8xÉR-\ZJﬁWººQ23V$ØõsU.î˜ç)±Pπ¥˙IÍ “†]±h:C@i¨®m23Ü	±1Ú gÄ/#›∫ ∏2\n√⁄x|`ÎG∫ù„Æ∂u_¸;Ç—êåU†O’≤wπj‰èÛôsÛÏ4»•≠â- ZeÏN˜	˛xe|ıo,§ÛÅœË û1êÒyΩÑsÜi›\0ﬁ∫ÌÙs≠Ë	ÒÙV7˚îé‘88èú⁄\0ówaëÆ:ıÅÏCrhÿˆå˘´€›ù¢A‡ñÒ˛\0\0ˇˇ\0PK\0\0\0\0\0!\0µU0#Ù\0\0\0L\0\0\0_rels/.rels ¢(†\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0¨íMO√0ÜÔH¸á»˜’›êBKwAHª!T~ÄI‹µç£$›ø\'T\ZÉGΩ~¸ €›<çÍ»!ˆ‚4¨ã;#∂w≠Üó˙qu*&rñFq¨·ƒv’ı’ˆôGJy(vΩè*´∏®°K…ﬂ#F”ÒD±œ.W\Z	•Ü=ôÅZ∆MYﬁb¯Æ’BSÌ≠Ü∞∑7†Íìœõ◊ñ¶È\r?à9LÏ“ô»sbgŸÆ|»l!ı˘\ZUSh9i∞bûr:\"y_dl¿ÛDõø˝|-Nú»R\"4¯2œG«%†ıZ¥4ÒÀùyƒ7	√´»…Çã®ﬁ\0\0ˇˇ\0PK\0\0\0\0\0!\0‰èÚ-|\0\0/\0\0\0\0\0xl/workbook.xml¨U€n€8}_`ˇAK‰Uë(S≤%ÿ.¨÷@€\r“4}	0mñD-E≈ä˛{áíÌÿMQd”5l“‰àágfŒå¶ÔvUi<2ŸrQœæ¥ë¡ÍL‰º^œ–Áõ‘ú £U¥Œi)j6CO¨EÔÊ˛1›\nπybc\0@›ŒP°TXVõ¨¢Ì•hX\rñïêU∞îk´m$£y[0¶™“rl€≥* k4 Ú5bµ‚ãE÷U¨Vàd%U@ø-x”–™Ï5pïõÆ13Q5\0Ò¿KÆûzPdTY∞\\◊B“á‹ﬁa◊ÿI¯z√6Œ·&0Ω∏™‚ô≠X©KÄ∂“/¸«∂ÖÒYv/c:$bIˆ»uè¨§˜FVﬁÀ{√ˆo£aêVØï\0Ç˜F4˜»ÕAÛÈäóÏvêÆAõÊ#≠t¶Jdî¥UIŒÀghK±eg≤k¬éó`u∞Ô¯»öÂ|%çú≠hW™Ú*√Û|«’OÇ0•b≤¶äE¢V†√Ω_ø´π;*(‹∏fˇv\\2(,–¯\n#Õ˙–^QUù,g(\nÓ‚≈ÕÚ˛”Ú√?ìªòµ%öª]“óEîI3ÌÆ˛úÜˇ?˙‘dpPﬂïí¸_∆Ô!üË#‰≤ûÔÀu	«£˚:ìæˇÍçlLÏ06ΩpÏõƒ%±π#ﬂÙS?!8ç¢	qøÅ3“2A;UÏS≠°gàêüò>–›¡ÇÌ†„˘3çØˆ˛cÍ˘á·`˚¶÷MÌñ≥m˚,\nΩ4v_xùãÌçúâãåßÛÂ∂7~·π*¿Iüå¡ÔaÔo∆◊0∆ò@&uÁ–Ãf9dÏLìêhdíxº0~íöpvApÇ”8\Z˜å¨J}˚j˝l‘Ω‰S±.π¿–®uoÌ£åËK‰2«}Á@‹ºfπÆ@9YÌ±bËÉhæG¸ÎbqAÇãË¬ùZ\'èÇŒa2ZfP8zÍo˜±=T€©˜≠öOaÕrp\ZRæ€>1Ìd‰öd‚;ÊÑå3\"±ì∏„$N¬>Î˙•¸≠µ/ù‡∂“,*’ç§Ÿﬁq◊l“d:D	xûí\r›IhèÄ\"I1‰˚∂Ü1›8πcGâõjâdµ˚´76∂â’üfTuPÙ∫ﬁ˚u†«tø{‹\\\r˚åùUtpÎtÔOˇÍ¡Ù∂◊≈OÔ±˙8Ë±œûuàﬁ¸;\0\0\0ˇˇ\0PK\0\0\0\0\0!\0Å>îóÛ\0\0\0∫\0\0\Z\0xl/_rels/workbook.xml.rels ¢(†\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0¨RMKƒ0Ω˛á0wõvŸt/\"ÏUÎ…¥)€&!3~Ùﬂ*∫]X÷K/oÜyÔÕ«v˜5‚ı¡+®äzlÔ;oÕÛÕbÌ≠ÇGÏÍÎ´Ìösπ>í»,û8Ê¯(%á£¶\"DÙπ“Ü4jŒ0u2js– MYﬁÀ¥‰Ä˙ÑSÏ≠Ç¥∑∑ ö)fÂˇπC€ˆüÇy—Û	I<\ry\0—Ë‘!+¯¡EˆÚº¸fMyŒk¡£˙Â´K™5=|Üt á»G)ísÂ¢ôªUÔ·tB˚ )ø€Ú,ÀÙÔf‰…«’ﬂ\0\0\0ˇˇ\0PK\0\0\0\0\0!\0fÛƒ„Î\0\0C\0\0\0\0\0xl/worksheets/sheet1.xmlúU€éõ0}Ø‘∞¸Æ!$(dï´∫ï™n/œékS€πl´˛{«∞a”d´F+Ö\0ÊÃ9g∆30æ;ñŸÉ“BV)ıè®∏ÃDµIÈ◊/´ﬁêmXï±BVê“\'–ÙnÚ˛›¯ ’£ﬁÇïNÈ÷ò:q]Õ∑P2Ì»\Z*|íKU2É∑j„ÍZÀö†≤pœ∏%muáÃs¡a!˘ÆÑ ¥$\n\nf–øﬁäZüÿJ~]…‘„ÆÓqY÷H±Ö0O\r)%%OÓ7ïTl]`ﬁGøœ89*¸xÑ\'ôf˝J©\\I-s„ ≥€zæN‰é\\∆;¶Î¸o¢Ò˚ÆÇΩ∞¯BºÕíu\\¡Y¯F≤AGfÀ•íù»R˙k1Ü˝Âl⁄õ∆”UØøÑΩÈrˆ¢h>ƒ´`Ó{·o:gwÿfE‰)ù˘…2¢Ód‹Ùœ7}vM[?@‹\0j¯îÿˆ\\K˘hÅ˜∏‰!£n\0ñëq#ˆ0á¢@‚v¯èVc`‹N·¸˙§∂j\Z˙ì\"‰lWòœÚƒfkP6¬4mü$Ÿ”4«Ea\'hlsY ˛ìRÿI√c«÷™»Ã6•Å≈^Ë#öù6≤¸ﬁÆ˚÷Sá€“ƒ·˘¸|‰ÙÉ(˛/˜†âƒÛsd‡;±Ôç¬¯UI∑u‹c¡õåï<lR¥ÆkfG>H˛ô1Z∂ÿôß¥O	ñB„Ï\'ﬁÿ›ca˘3b~Bÿ,m»¢]h”∂À≥=tF∞∑Apg°a¡•”!¬Òó\"BnW¥`¨—´°{ç^◊≈˙›Æ{^ÏËBÒ¸Ÿ‡u-‹“€µ‹’,æ‡kÁ®mù‘¶ô7M∏‹Ÿπ±∫’nƒ±Ïæ¿\'„öm‡#SQiR@ﬁåTLâjgŒs⁄»⁄öÌ„µ48:ßª-~o\0[»s∞‘πîÊtÉ\"ñ˜ÃÆ&5´A=àü¯öQ\"ï¿¡m>()≠•2ä	Ézâ}{©˚¨uÿ}˛&\0\0\0ˇˇ\0PK\0\0\0\0\0!\0	ßV\0\0» \0\0\0\0\0xl/theme/theme1.xmlÏY[è5~G‚?XÛûÊ6ìÀ™) µKª€V›¥àGo‚d‹ıå#€ŸmÑ*°Úƒ ^êx„!ê@Ò¬è©‘äÀè‡ÿ3…ÿá^ÿ\"@ªëVÁ;««Á>s|ı≠á	CßDH ”NPΩR	I\'|J”y\'∏7ïZí\nßSÃxJ:¡ä»‡≠koæqÔ©ò$Å|*˜p\'àïZÏïÀr√X^·í¬o3.¨‡QÃÀSÅœ@o¬ µJ•QN0Mî‚‘éAM)∫=õ—		Æ≠’Ãë*©&LiÂ$ó±∞”ì™F»ïÏ3ÅN1Î0”îüç…C Ü•Ç:A≈¸ÂkWÀx/bjá¨%72π\\.0=©ô9≈¸x3iFa£ª—o\0Lm„ÜÕacÿÿË3\0<ô¿J3[\\ùÕZ?Ã±(˚Í—=hÍUoÈØoŸ‹çÙ«¡P¶?‹¬èF}¢É7†m·£^ª7pıPÜol·õïÓ l:˙\r(f4=ŸBW¢FΩø^Ì2„lﬂoG·®YÀï(»ÜMvÈ)f<Uªr-¡∏@V4Ejµ 3<Å<ÓcFèEtC‚-p %WjïQ•ˇı\'4ﬂLDÒ¡ñ¥∂,ë[C⁄$\'Ç.T\'∏ZÚÙßüû<˛·…„ü|¡ì«ﬂÊsUé‹>NÁ∂‹Ô_}¸«Ô£ﬂæˇÚ˜O>Õ¶>èó6˛Ÿ7>˚˘óøR+.\\ÒÙ≥Ôû˝›”œ?˙ıÎO<⁄ª€1MàD∑»∫ÀX†«~r,^NbcÍH‡t{TUÏ\0o≠0Û·zƒu·},„^_>pl=ä≈RQœÃ7„ƒrŒz\\xpSœeyxºLÁ˛…≈“∆›≈¯‘7wßNÄáÀ–+ı©Ï«ƒ1Û√©¬síÖÙo¸ÑœÍﬁ•‘ÒÎ!ù.˘L°w)ÍaÍu…ò;âTÌ”‚≤Ú°v|sxı8Û≠z@N]$lÃ<∆è	s‹x/N|*«8a∂√∞ä}F≠ƒƒ∆\r•ÇHœ	„h8%R˙dnXØÙõ¿0˛∞≤U‚\"Ö¢\'>ùòs9‡\'˝\'ØÕ4çmÏ€ÚR£;\\˘‡á‹›!˙‚Ä”ù·æOâÓÁ¡= W€§\"AÙ/K·âÂu¬›˝∏b3L|,”â√Æ]AΩŸ—[Œù‘> Ñ·3<%›{€cAè/üFﬂàÅUˆâ/±n`7WısJ$A¶ÆŸ¶»*ùî=\"sæ√û√’9‚Y·4¡bóÊ[u\'u·îÛRÈm69±Å∑(Äê/^ß‹ñ†√JÓ·.≠wbÏú]˙Y˙Ûu%ú¯Ω»É}˘‡e˜%»êóñbaﬂå1s&(få°¿—-à8·/DÙπjƒñ^πôªiã0@a‰‘;	Mü[¸ú+{¢¶ÏÒ0P¯ˇùRg•Ïü+pv·˛ÉeÕ\0/”;NímŒ∫¨j.´ö‡_’Ï⁄ÀóµÃe-sYÀ¯ﬁæ^K-Sî/PŸ]”ÛIv∂|fî±#µb‰@öÆèÑ7öÈM; Ù$7-¿E_ÛìÉõldê‡Í™‚£/†5T5ÕŒπÃUœ%Zp	#3lö©‰ún”wZ&á|öu:´U›’Ã\\(±*∆+—f∫T*C7öE˜n£ﬁÙCÁ¶À∫6@Àæå÷dÆuèÕı D·Øå0+ª+⁄+ZZ˝:TÎ(n\\¶m¢Ø‹^‘;Afdh∆Ay>’q ö…ÎËÍ‡\\h§w9ìŸ\0%ˆ:äH∑µ≠;óßWó•⁄D⁄1¬J7◊+\rcxŒ≥”nπ_d¨€EHÛ¥+÷ª°0£Ÿz±÷$réXj3K—Y\'h‘#∏Wô‡E\'òA«æ&»©ﬂ∫0õ√≈ÀDâl√ø\n≥,ÑT,„Ã·Üt26H®\"1ötΩ¸M6∞‘pà±≠ZB¯◊\Z◊Z˘∑AwÉLf32Qvÿ≠ÌÈÏ>„\nÔØF¸’¡Zí/!‹GÒÙ≥•∏ã!≈¢fU;pJ%\\T3oN)‹Ñmà¨»øsSNªˆUî…°l≥EåÛ≈&ÛnHtcéy⁄¯¿z ◊›v·Ò\\∞˚‘}˛Q≠=gëfqf:¨¢OM?ôææCﬁ≤™8D´2Í6Ô‘≤‡∫ˆöÎ QΩßƒsN›8,”ä…”¥≈€4¨9;uMª¿Ç¿ÚDcáﬂ6gÑ◊ØzÚÉ‹˘¨’ƒ∫Æ4âo.ÕÌ[m~¸\0»c\0˜áK¶§	%‹YE_vô—lëá*Ø·Z\n⁄	ﬁ´D›∞_ã˙•J+\Zñ¬zX)µ¢nΩ‘ç¢zuU+É^Ì,*N™Qva?Ç+∂ ØÌÕ¯÷’}≤æ•π2·Iôõ+˘≤1‹\\›WkŒ’}v\rè∆˙f>@HÁΩFm‘Æ∑{çRªﬁï¬AØUj˜Ω“†—oFÉ~‘jèË‘Ä√nΩ6Ü≠R£⁄Ôó¬FEõﬂjóöa≠÷\rõ›÷0Ï> ÀXyFπ/¿Ω∆Æk\0\0ˇˇ\0PK\0\0\0\0\0!\0ﬁõ?1\0\0ı\0\0\r\0\0\0xl/styles.xmlºV€é”0}G‚,øgsiS⁄*	¢ÌFBZ“.Øn‚§æDéª§ã¯w∆N⁄¶‹ŸÙ%ˆÿ>s∆s∆”‰e\'8∫ß∫eJ¶8º\n0¢≤P%ìuäﬂﬂÂﬁ£÷YÆ$MÒÅ∂¯eˆ¸Y“öß∑;J\rŸ¶xgL≥Ù˝∂ÿQA⁄+’P	+ï“ÇòÍ⁄oMIŸ⁄CÇ˚QÃ|Aòƒ=¬R\"à˛∏oºBâÜ∂eúôÉ√¬HÀ◊µTöl9PÌ¬))PŒtÑ:}t‚¨ﬂ˘¨–™Uïπ\\_U+Ë˜t˛¬\'≈	êá∆~]ƒﬁÈG\"M}MÔôMŒíJI”¢BÌ•IqDÌ,?JıIÊv	2<Ï íˆ›ñ˚YR(Æ42ê:∏9gëD–~«öp∂’Ãn´à`¸–õ#kpŸˆ	woçæÂ—≥…í≠›ıœ}9ó-¯dúün`bÉCñÄT’2á	\Z∆wáBï†Íû≤€˜õ›µ&á0äˇ¸@´8+-ãzÌ.X◊€ÁÓf;,0Y“éñ)ûM˙à∞ΩOGŒ} ∆≠“%TÏ1œ6•Ω)K8≠†jVÔÏ◊®∆˙P∆Ä™≥§d§Vípõ¢„âa\0∞Â¸÷VıáÍª´ê‹ã\\ò◊@ﬁõ‹„x\r√ØüX¸1Zè=ÇçÅÚﬂ√¢Æ:·?·4\"M√Ø8´•†∂Pú‹B¿CîPP„(Otz@[`kò¡ôÛÏ¬Yñ@=ıæ—Niˆ\0m!@Üˆı”UO∫üD¯3ßV0NpˇœÂ7q:•Ä6FºêﬂIH»æH)~k\nß∏\rj@€=„Ü…h@ÀÓ¨fWq∆vßÛìuI+≤ÁÊÓ¥ò‚Û¯\r-Ÿ^@˙á]ÔÿΩ2\"≈ÁÒç-∫pfoìvÊ¶ÖæhØYä?_Ø^,6◊y‰ÕÉ’‹õNhÏ-‚’∆ãßÎ’fì/Ç(Xı®\'t(◊RA?·tŸrËczv {∂•x4ÈÈ;-\0Ì1˜E4^≈a‡Âì Ù¶32˜Ê≥IÏÂqmf”’uú«#ÓÒ#;Y‡áaﬂ-˘xiò†ú…cÆé\Z[!I0˝E˛1˛˘ˇJˆ\0\0ˇˇ\0PK\0\0\0\0\0!\0N˝’[Ï\0\0\0ç\0\0\0\0\0xl/sharedStrings.xmllêMK1ÜÔÇˇ°‰∞Ë¡ÈËAD€ÓAºxZO‚°N„La⁄éM∆Ü˝ÔvWWa÷@yﬁ¢ñ°oò…ß®·¥™A`líÛ±’∞∫=π\0Al£≥}ä®·	ñÊ@±(ŸH\Z:Ê·RJj:ñ™4`,Ê%Â`πåπï4d¥é:DΩ<´Îs¨è ö4F÷Péå—øéxΩõç\"oª-%Ÿ(π!?tûÁ`ön,˚jªÔËxΩû˚Ub€ãúﬁi?È√#‹9x⁄œM”∑ΩOˇÛãûØ6M)≥pHÕ¢-†¥¯5cÿ±ø”≤º”|\0\0ˇˇ\0PK\0\0\0\0\0!\0;m2K¡\0\0\0B\0\0#\0\0\0xl/worksheets/_rels/sheet1.xml.relsÑè¡ä¬0E˜˛Cx{ì÷ÖCS7\"∏UÁb˙⁄€óê˜˝{≥e¿ÂÂpœÂ6õ˚<©fë,‘∫Ö‰ch∞{⁄-øA±8Í‹	-<êa”.æöNNJâ«êX±ÖQ$˝√~ƒŸ±é	©ê>ÊŸIây0…˘ã–¨™jmÚ_¥/NµÔ,‰}WÉ:=RY˛Ïé}<n£øŒHÚœÑI9ê`>¢H9»EÌÚÄbAÎwˆûk}¶mÃÀÛˆ	\0\0ˇˇ\0PK\0\0\0\0\0!\0ò‡¨∆\0\0‹$\0\0\'\0\0\0xl/printerSettings/printerSettings1.binÏóΩJA«ˇõúü±ëDPÉ¬˘Åï⁄®Hê)\r9AT¨SXYYI∞ü¿á–÷G∞DÒ‚ÃÓ]ºãŸòÉ3aogg3ssøΩΩ››AYê¬\"‡b%ÏSk\Z9™ã8ƒµÚÿ√1˝äòATîWO¿dºzW–ù˙Pà≈®6◊:ß4ù>älq*¨4/W⁄‡Æ<]v¿•Œo©ˇ:ëF\Zïëõ$◊7… H#‡ Vã Z£¯¡ÌÉzÌ”-í’Ö¿÷ﬂ)•\n‡∫Û:Î,.Iw3‹Õôñ%≈H\nÌ˛üù˝Ç¡€≤œ˙HÃ* -¶iáÌVgøÉ\'$Â7∆µmH_çç’‡y§p·˚TIXaΩ∂|\\Í(SÄu\n4KÖ„_ëmÖ ®Ôƒ„Òé{.∂x’nôfM“’øﬂIîˇOÎñ$»©ôÉ”~‚›64íØhìÄmRtÚ%¸ΩAP˛ ÷+„˛Ö@ˆxøx–\"^;)˛m2	ZEÕõì A1Ω=∫ám@˝xıë-ÿøuõ˛ﬂóﬁ_ÁÃ˛⁄≠É°jª;ô†LÄÒ0Ø·)√k,ÑÖÌ©-c_\rmd∂Èv¢O^gtÚ:’\'±y¡)Ñ¿<á^iÓYˇŸvPnB@Å.%¿ﬂ8ÔÕ|„Ü˝˝D…o? ®\n! ÑÄB@! ÑÄ¯ß>\0\0\0ˇˇ\0PK\0\0\0\0\0!\0V#ÓÙE\0\0k\0\0\0docProps/core.xml ¢(†\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0åíQO√ ÖﬂM¸\rÔ-¥õs!mó®ŸìKñlF„ÅªçX(¥€øó∂≥VÁÉèpŒ˝8ÁÜ|qTUÙ÷…Z(Mä@ÛZHΩ/–”vœQ‰<”ÇUµÜù¿°Ey}ïsCymamk÷KpQ iGπ)–¡{C1v¸\0äπ$8twµUÃá£›c√¯€Œôaû	ÊnÅ±àËå|@öw[u\0¡1T†@{á”$≈ﬂ^Vπ?:e‰T“üLËté;fﬁãÉ˚Ë‰`lö&i&]åê?≈/´«MW5ñ∫›TÊÇSnÅ˘⁄ñk®òd—F™∞¬èîvãs~æì ÓNøÕóÜ@ÓäÙxQàF˚\"_ Û‰˛aªDeF2ìiúe€ÙÜí9ùÃ^€˜Ã∑Q˚uNÒO‚-%)ùéâ_Ä2«ﬂ£¸\0\0ˇˇ\0PK\0\0\0\0\0!\0~ıÇ¿¶\0\0ú\0\0\0docProps/app.xml ¢(†\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0§SMo€0Ωÿ0to‰tE1≤ä!]— iÔ¨L\'¬dIêX#ŸØmØÆª;¨7~ÈÒÒëRW«÷¶lÉØƒrQäΩ	µı˚J‹ÌnŒ>ä\"¯\Z\\Xâfq•ﬂøSõ\"&≤òÜπ¢∏í2õ∂êúˆúiBjÅÿM{ö∆\ZºÊ±EOÚº,/%	}çıYú\0≈à∏ÍËAÎ`z~˘~wäLX´O1:kÄxJ˝Õörh®¯|4Ëîú\'≥€¢yLñN∫TrÓ™≠ák÷\r∏åJ>‘-B/⁄l Zu¥Í–PHE∂?Y∂Q<@∆ûN%:H<1≠ælt€≈LIﬂÑΩ≥Em]HAIÆ\Z3É90∑ÌÖ^l¸≥pƒ˙‚	SÆoÖ>¥÷≥:oÔ’ì\'g/5ŸYròø7HÙäDÁsâé£@3Q¬8‡o1&YÆ_c>®œ˛Ë∫m‚ƒd}µ˛Gæãª¿8¯¥ŸóAµ=@¬öèa⁄¸P∑º‘‰zêı¸Îßöø˝ﬁèüM//ÂáíOlSÚ˘[È_\0\0\0ˇˇ\0PK-\0\0\0\0\0\0!\0A7Çœn\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0[Content_Types].xmlPK-\0\0\0\0\0\0!\0µU0#Ù\0\0\0L\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0ß\0\0_rels/.relsPK-\0\0\0\0\0\0!\0‰èÚ-|\0\0/\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0Ã\0\0xl/workbook.xmlPK-\0\0\0\0\0\0!\0Å>îóÛ\0\0\0∫\0\0\Z\0\0\0\0\0\0\0\0\0\0\0\0\0u\n\0\0xl/_rels/workbook.xml.relsPK-\0\0\0\0\0\0!\0fÛƒ„Î\0\0C\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0®\0\0xl/worksheets/sheet1.xmlPK-\0\0\0\0\0\0!\0	ßV\0\0» \0\0\0\0\0\0\0\0\0\0\0\0\0\0\0…\0\0xl/theme/theme1.xmlPK-\0\0\0\0\0\0!\0ﬁõ?1\0\0ı\0\0\r\0\0\0\0\0\0\0\0\0\0\0\0\0P\0\0xl/styles.xmlPK-\0\0\0\0\0\0!\0N˝’[Ï\0\0\0ç\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0¨\Z\0\0xl/sharedStrings.xmlPK-\0\0\0\0\0\0!\0;m2K¡\0\0\0B\0\0#\0\0\0\0\0\0\0\0\0\0\0\0\0 \0\0xl/worksheets/_rels/sheet1.xml.relsPK-\0\0\0\0\0\0!\0ò‡¨∆\0\0‹$\0\0\'\0\0\0\0\0\0\0\0\0\0\0\0\0Ã\0\0xl/printerSettings/printerSettings1.binPK-\0\0\0\0\0\0!\0V#ÓÙE\0\0k\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0)\0\0docProps/core.xmlPK-\0\0\0\0\0\0!\0~ıÇ¿¶\0\0ú\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0•!\0\0docProps/app.xmlPK\0\0\0\0\0\0&\0\0Å$\0\0\0\0',NULL);

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
