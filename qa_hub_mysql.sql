

DROP TABLE IF EXISTS `tickets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tickets` (
  `ticketid` int(11) NOT NULL AUTO_INCREMENT,
  `tickettitle` varchar(255) DEFAULT NULL,
  `ticketcategory` varchar(45) DEFAULT NULL,
  `ticketbody` varchar(255) DEFAULT NULL,
  `ticketauthor` varchar(45) DEFAULT NULL,
  `tickettime` datetime NOT NULL,
  `ticketupdate` datetime DEFAULT NULL,
  PRIMARY KEY (`ticketid`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
