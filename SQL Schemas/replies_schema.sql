DROP TABLE IF EXISTS 'replies'
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
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci