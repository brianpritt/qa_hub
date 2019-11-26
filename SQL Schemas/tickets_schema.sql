CREATE TABLE `tickets` (
  `ticketid` int(11) NOT NULL AUTO_INCREMENT,
  `tickettitle` varchar(255) DEFAULT NULL,
  `ticketcategory` varchar(45) DEFAULT NULL,
  `ticketbody` varchar(255) DEFAULT NULL,
  `ticketauthor` varchar(45) DEFAULT NULL,
  `tickettime` datetime NOT NULL,
  `ticketupdate` datetime DEFAULT NULL,
  PRIMARY KEY (`ticketid`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci