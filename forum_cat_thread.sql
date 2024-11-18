-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.40 - MySQL Community Server - GPL
-- Server OS:                    Linux
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
CREATE DATABASE IF NOT EXISTS `forum_cat_thread` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `forum_cat_thread`;

-- Dumping structure for table forum_cat_thread.Categories
CREATE TABLE IF NOT EXISTS `Categories` (
  `CategoryId` int NOT NULL AUTO_INCREMENT,
  `CategoryName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedBy` int NOT NULL,
  `CreatedDate` date NOT NULL,
  PRIMARY KEY (`CategoryId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table forum_cat_thread.Categories: ~10 rows (approximately)
REPLACE INTO `Categories` (`CategoryId`, `CategoryName`, `Description`, `CreatedBy`, `CreatedDate`) VALUES
	(1, 'General Discussion', 'A place for general discussions and off-topic chat.', 1, '2024-01-15'),
	(2, 'Programming', 'Discuss programming languages, frameworks, and tools.', 2, '2024-01-20'),
	(3, 'Web Development', 'Topics on HTML, CSS, JavaScript, and web technologies.', 3, '2024-02-10'),
	(4, 'Database Management', 'Share knowledge on SQL, NoSQL, and data storage.', 4, '2024-02-12'),
	(5, 'Mobile Development', 'Discuss Android, iOS, and mobile app development.', 2, '2024-03-05'),
	(6, 'DevOps', 'All about CI/CD, cloud services, and infrastructure automation.', 5, '2024-03-15'),
	(7, 'Machine Learning', 'A place for AI, ML, and data science enthusiasts.', 3, '2024-04-01'),
	(8, 'Cybersecurity', 'Discuss security best practices, news, and trends.', 6, '2024-04-20'),
	(9, 'Career Advice', 'Share and seek advice on careers in tech.', 1, '2024-05-10'),
	(10, 'Announcements', 'Official forum announcements and updates.', 7, '2024-06-01');

-- Dumping structure for table forum_cat_thread.Threads
CREATE TABLE IF NOT EXISTS `Threads` (
  `ThreadId` int NOT NULL AUTO_INCREMENT,
  `CategoryID` int NOT NULL,
  `ThreadTitle` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ThreadContent` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedBy` int NOT NULL,
  `CreatedDate` date NOT NULL,
  `ViewCount` int NOT NULL,
  `IsPinned` int NOT NULL,
  `IsClosed` int NOT NULL,
  PRIMARY KEY (`ThreadId`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table forum_cat_thread.Threads: ~20 rows (approximately)
REPLACE INTO `Threads` (`ThreadId`, `CategoryID`, `ThreadTitle`, `ThreadContent`, `CreatedBy`, `CreatedDate`, `ViewCount`, `IsPinned`, `IsClosed`) VALUES
	(1, 1, 'Welcome to the Forum!', 'Introduce yourself here and let’s get to know each other.', 1, '2024-01-15', 150, 1, 0),
	(2, 1, 'Forum Rules', 'Please read the forum rules before posting.', 1, '2024-01-16', 230, 1, 1),
	(3, 2, 'Best Programming Languages for Beginners', 'Which languages are best for beginners?', 2, '2024-02-01', 500, 0, 0),
	(4, 2, 'Java vs. Python', 'Debating the pros and cons of Java and Python.', 3, '2024-02-03', 600, 0, 0),
	(5, 3, 'HTML and CSS Tips', 'Share your best HTML and CSS tips here.', 4, '2024-02-15', 300, 0, 0),
	(6, 3, 'JavaScript Frameworks', 'Which framework is your favorite?', 5, '2024-02-20', 450, 0, 0),
	(7, 4, 'SQL Optimization Techniques', 'How do you optimize SQL queries?', 6, '2024-03-05', 350, 0, 0),
	(8, 4, 'NoSQL vs SQL', 'Discuss the differences and use cases.', 2, '2024-03-10', 400, 0, 0),
	(9, 5, 'Getting Started with Android Development', 'Tips for beginners in Android development.', 3, '2024-03-25', 200, 0, 0),
	(10, 5, 'Swift vs. Kotlin', 'Comparison of Swift and Kotlin for mobile apps.', 4, '2024-04-01', 220, 0, 0),
	(11, 6, 'Best Practices for CI/CD', 'What are your best CI/CD practices?', 5, '2024-04-15', 320, 0, 0),
	(12, 6, 'Using Docker with Kubernetes', 'A guide to deploying applications.', 1, '2024-04-20', 410, 0, 0),
	(13, 7, 'Getting Started with Machine Learning', 'Where to start learning machine learning?', 2, '2024-05-01', 540, 0, 0),
	(14, 7, 'TensorFlow vs PyTorch', 'Which machine learning framework do you prefer?', 6, '2024-05-10', 580, 0, 0),
	(15, 8, 'Cybersecurity Best Practices', 'What are some essential practices?', 5, '2024-05-20', 480, 1, 0),
	(16, 8, 'Latest News in Cybersecurity', 'Discuss recent cybersecurity events.', 3, '2024-05-25', 450, 0, 0),
	(17, 9, 'Job Hunting Tips', 'Share your tips for finding a job in tech.', 4, '2024-06-01', 600, 0, 0),
	(18, 9, 'How to Prepare for Tech Interviews', 'Preparation strategies for interviews.', 6, '2024-06-05', 650, 0, 0),
	(19, 10, 'Forum Maintenance', 'Scheduled maintenance for forum upgrades.', 1, '2024-06-15', 300, 1, 1),
	(20, 10, 'New Features Announcement', 'Introducing new forum features!', 1, '2024-06-20', 450, 1, 1);

-- Dumping structure for table forum_cat_thread.__EFMigrationsHistory
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table forum_cat_thread.__EFMigrationsHistory: ~0 rows (approximately)
REPLACE INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
	('20241022024112_InitialCreate', '8.0.8');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
