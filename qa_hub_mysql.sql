CREATE DATABASE `qa_hub_sql_2`;
USE `qa_hub_sql_2`;
CREATE TABLE `tickets` (
  `ticketid` int(11) NOT NULL AUTO_INCREMENT,
  `tickettitle` varchar(255) DEFAULT NULL,
  `ticketcategory` varchar(45) DEFAULT NULL,
  `ticketbody` varchar(255) DEFAULT NULL,
  `ticketauthor` varchar(45) DEFAULT NULL,
  `tickettime` datetime NOT NULL,
  `ticketupdate` datetime DEFAULT NULL,
  PRIMARY KEY (`ticketid`)
);
CREATE TABLE `replies` (
  `replyid` int(11) NOT NULL AUTO_INCREMENT,
  `replyauthor` varchar(45) DEFAULT NULL,
  `replybody` varchar(255) DEFAULT NULL,
  `replytime` datetime DEFAULT NULL,
  `replyupdate` datetime DEFAULT NULL,
  `ticketid` int(11) DEFAULT NULL,
  PRIMARY KEY (`replyid`),
  KEY `ticketid_idx` (`ticketid`),
  CONSTRAINT `ticketid` FOREIGN KEY (`ticketid`) REFERENCES `tickets` (`ticketid`) ON DELETE CASCADE ON UPDATE CASCADE
);