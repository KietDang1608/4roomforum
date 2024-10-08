-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.22-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.4.0.6659
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for forum_cat_thread
CREATE DATABASE IF NOT EXISTS `forum_cat_thread` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `forum_cat_thread`;

-- Dumping structure for table forum_cat_thread.categories
CREATE TABLE IF NOT EXISTS `categories` (
  `CategoryId` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(50) DEFAULT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedDate` date DEFAULT NULL,
  PRIMARY KEY (`CategoryId`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table forum_cat_thread.categories: ~10 rows (approximately)
REPLACE INTO `categories` (`CategoryId`, `CategoryName`, `Description`, `CreatedBy`, `CreatedDate`) VALUES
	(1, 'General Discussion', 'General topics for any subject related to the forum.', 1, '2024-09-27'),
	(2, 'Announcements', 'Official announcements from the forum staff.', 1, '2024-09-27'),
	(3, 'Feedback', 'Provide feedback and suggestions for the forum.', 2, '2024-09-27'),
	(4, 'Support', 'Get help and support for forum-related issues.', 2, '2024-09-27'),
	(5, 'Off-Topic', 'Discuss anything not related to the main topics of the forum.', 3, '2024-09-27'),
	(6, 'Technology', 'Discuss the latest trends in technology.', 3, '2024-09-27'),
	(7, 'Gaming', 'Share news, tips, and updates about video games.', 4, '2024-09-27'),
	(8, 'Entertainment', 'Talk about movies, music, TV shows, and more.', 4, '2024-09-27'),
	(9, 'Programming', 'Discuss programming languages, frameworks, and coding tips.', 5, '2024-09-27'),
	(10, 'Job Opportunities', 'Share and discuss job opportunities in tech and other fields.', 5, '2024-09-27');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
