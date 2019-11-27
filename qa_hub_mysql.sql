CREATE DATABASE `qa_hub_sql`;
USE `qa_hub_sql`;
CREATE TABLE `tickets` (
  `ticketid` int(11) NOT NULL AUTO_INCREMENT,
  `tickettitle` varchar(255) DEFAULT NULL,
  `ticketcategory` varchar(45) DEFAULT NULL,
  `ticketbody` varchar(255) DEFAULT NULL,
  `ticketauthor` int(11) NOT NULL,
  `tickettime` datetime NOT NULL,
  `ticketupdate` datetime DEFAULT NULL,
  PRIMARY KEY (`ticketid`),
  KEY `userid_idx` (`ticketauthor`)
)
);
CREATE TABLE `replies` (
  `replyid` int(11) NOT NULL AUTO_INCREMENT,
  `replyauthor` int(11) DEFAULT NULL,
  `replybody` varchar(255) DEFAULT NULL,
  `replytime` datetime DEFAULT NULL,
  `replyupdate` datetime DEFAULT NULL,
  `ticketid` int(11) DEFAULT NULL,
  PRIMARY KEY (`replyid`),
  KEY `ticketid_idx` (`ticketid`),
  CONSTRAINT `ticketid` FOREIGN KEY (`ticketid`) REFERENCES `tickets` (`ticketid`) ON DELETE CASCADE ON UPDATE CASCADE
)
);
CREATE TABLE `users` (
  `userid` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(100) DEFAULT NULL,
  `useremail` varchar(100) DEFAULT NULL,
  `userteam` varchar(45) DEFAULT NULL,
  `usercreatedtime` datetime DEFAULT NULL,
  PRIMARY KEY (`userid`)
);