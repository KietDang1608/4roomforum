-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.39 - MySQL Community Server - GPL
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

-- Dumping data for table forum_cat_thread.Threads: ~0 rows (approximately)
REPLACE INTO `Threads` (`ThreadId`, `CategoryID`, `ThreadTitle`, `ThreadContent`, `CreatedBy`, `CreatedDate`, `ViewCount`, `IsPinned`, `IsClosed`) VALUES
	(1, 1, 'Apple iPhone 15: Worth the Upgrade?', 'I’ve been using an iPhone 12 for the last two years, and I’m considering upgrading to the iPhone 15. The new camera features and improved battery life look promising, but I’m not sure if it’s worth the price. Has anyone made the switch yet? How’s your experience so far with performance and battery? Any noticeable differences in day-to-day usage?', 1, '2023-10-10', 348, 0, 0),
	(2, 2, 'How to Fix Object Reference Not Set to an Instance of an Object in C#?', 'I\'ve been running into the "Object reference not set to an instance of an object" error in my C# project. It happens intermittently when calling a method. I’ve checked that the object is initialized before calling it, but still, the issue occurs. Can someone point me in the right direction?', 2, '2023-09-12', 1020, 0, 0),
	(3, 3, 'Best Strategies for Victory in Fortnite Season 10', 'Fortnite Season 10 has introduced several new mechanics, and I’m struggling to adjust. The new build meta is confusing, and I find myself getting overwhelmed by better players. Does anyone have any tips for adjusting to the new meta or suggestions on the best landing spots for a safe start?', 3, '2023-11-01', 712, 0, 0),
	(4, 4, 'Effective Home Workouts for Weight Loss During Quarantine', 'Due to the pandemic, I’ve been working from home and gained a few pounds. I’m looking for effective workout routines I can do at home with minimal equipment to shed some weight. What has worked for you?', 4, '2023-08-05', 498, 1, 0),
	(5, 5, 'How to Start Minimalist Living: Beginner Tips', 'I’ve been reading about minimalist living and I’m intrigued. I want to start decluttering and live a more simplified lifestyle, but I’m not sure where to begin. Should I start with my wardrobe, or focus on digital minimalism first?', 5, '2023-05-17', 600, 0, 0),
	(6, 6, '2024 U.S. Election: Thoughts on the Major Candidates', 'With the upcoming 2024 U.S. presidential election, there’s a lot of debate about the leading candidates from both major parties. I’m curious to hear everyone’s thoughts on their policies and platforms. Which issues are most important to you?', 6, '2023-07-23', 854, 0, 1),
	(7, 7, 'Best Affordable Electric Cars in 2024', 'I’m in the market for an electric car but don’t want to break the bank. What are some of the best options available in 2024 for someone looking for a reliable, affordable electric vehicle?', 7, '2023-10-05', 1294, 1, 0),
	(8, 8, 'Top 10 Hidden Gems to Visit in Europe', 'I\'m planning a backpacking trip across Europe and want to avoid the typical tourist spots. Can anyone suggest some hidden gems or off-the-beaten-path destinations that are worth visiting?', 8, '2023-09-02', 784, 1, 0),
	(9, 9, 'Remote Work vs. Office Work: Which Is Better for Productivity?', 'With the rise of remote work, many people, including myself, are trying to figure out whether working from home or going back to the office is better for productivity. What’s been your experience?', 9, '2023-11-08', 650, 0, 1),
	(10, 10, 'Best Air Fryer Recipes for Healthy Eating', 'I recently bought an air fryer and am looking for some healthy and easy recipes to try. I’ve already done the usual fries and chicken wings, but I’d love to explore more options like roasted veggies or even desserts! Does anyone have any great air fryer recipes they recommend?', 10, '2023-09-14', 732, 0, 0),
	(11, 2, 'C# Best Practices: Tips for Writing Clean Code', 'I\'ve been working on a large-scale project and want to ensure that the codebase remains clean and maintainable. What are some of the best practices in C# for writing clean, efficient code? Looking for advice on naming conventions, code comments, and architecture tips.', 4, '2023-10-25', 545, 0, 0),
	(12, 3, 'The Best Gaming Laptops of 2024', 'I\'m looking for a new gaming laptop and want to find the best performance without breaking the bank. Does anyone have suggestions on good gaming laptops under $2000? I’m mainly interested in high refresh rates, good cooling systems, and portability.', 3, '2023-10-19', 1123, 0, 1),
	(13, 5, 'Decluttering Tips: Where to Start with Minimalism?', 'I\'m new to minimalism and want to start decluttering my home. Does anyone have tips on where to start? Should I focus on clothing first or tackle bigger areas like the garage?', 5, '2023-08-15', 923, 0, 0),
	(14, 6, 'How to Stay Informed During Election Season?', 'With all the information being shared online, it can be overwhelming to keep up with election news. What are some reliable sources for staying informed about the candidates and issues? Do you rely on traditional news outlets, or do you get information from social media and podcasts?', 6, '2023-09-05', 431, 0, 1),
	(15, 4, 'How to Improve Your Daily Workout Routine', 'I\'ve been following a workout routine for a few months but feel like I’m not seeing the results I expected. How can I make my workouts more effective and keep myself motivated? Any tips for mixing up cardio and strength training?', 4, '2023-09-23', 675, 1, 0),
	(16, 7, 'Are Hybrid Cars Still Worth Buying in 2024?', 'I\'ve been thinking about buying a hybrid car, but with the rise of electric vehicles, I\'m wondering if hybrids are still a good investment. Are there any benefits to choosing a hybrid over an electric vehicle?', 7, '2023-11-01', 520, 0, 0),
	(17, 8, 'Best Backpacking Routes in Asia for 2024', 'I\'m planning a backpacking trip through Asia next year and want recommendations for the best routes. I’m interested in nature and cultural experiences, so any hidden gems would be appreciated. Also, any advice on budgeting for a month-long trip would help!', 8, '2023-09-29', 856, 0, 1),
	(18, 9, 'Tips for Handling Job Interviews as a Remote Worker', 'I\'ve been working remotely for the past few years and have an upcoming interview for a new remote position. Are there any specific tips for handling remote job interviews, especially when it comes to video conferencing and showcasing my skills?', 9, '2023-10-12', 943, 1, 0),
	(19, 1, 'Samsung vs Apple: Which Phone Brand Offers Better Value?', 'I\'ve always been an Apple user but have been thinking about switching to Samsung. I’m curious to know which brand offers better value in terms of performance, camera quality, and long-term durability. What’s your experience with both?', 1, '2023-08-30', 1432, 1, 0),
	(20, 2, 'How to Optimize Database Queries in SQL Server?', 'I\'m working on an application where some database queries are taking longer than expected to run. What are some best practices for optimizing SQL Server queries? I’m using complex joins and subqueries, so any advice on indexing and query optimization would be great.', 2, '2023-11-06', 845, 0, 1);

-- Dumping structure for table forum_cat_thread.__EFMigrationsHistory
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table forum_cat_thread.__EFMigrationsHistory: ~1 rows (approximately)
REPLACE INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
	('20241022024112_InitialCreate', '8.0.8');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
