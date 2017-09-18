-- phpMyAdmin SQL Dump
-- version 4.0.10deb1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Nov 21, 2016 at 12:09 AM
-- Server version: 5.5.50-0ubuntu0.14.04.1
-- PHP Version: 5.5.9-1ubuntu4.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `LabLocator`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`vineetakandala`@`%` PROCEDURE `sp_AssignLabs`(IN p_labID int(10),IN p_assignto varchar(30),IN p_assignedby varchar(30),IN p_startDate date )
BEGIN
Insert into LabAssignment(labID,userID,assignedBy,start_date) Values(p_labID,p_assignto,p_assignedby,p_startDate);
Update Labs set status = 'Assigned' where labID = p_labID;
END$$

CREATE DEFINER=`vineetakandala`@`%` PROCEDURE `sp_FacultyView`(IN facultyID varchar(20))
BEGIN
SELECT l.labID, l.labType, l.labAreaSqft, d.departmentID, d.deptName,la.userID as 'Assigned to',DATE_FORMAT(la.start_Date,"%m-%d-%Y") as 'Start Date',DATE_FORMAT(la.end_Date,"%m-%d-%Y") as 'End Date',l.status
FROM Labs l INNER JOIN LabAssignment la ON l.labID = la.labID
INNER JOIN Department d ON d.departmentID = l.departmentID 
WHERE userID = facultyID;
END$$

CREATE DEFINER=`vineetakandala`@`%` PROCEDURE `sp_LabStatusView`()
BEGIN
SELECT  l.labID, l.labType, l.labAreaSqft, d.departmentID, d.deptName,la.userID as 'Assigned to',DATE_FORMAT(la.start_Date,"%m-%d-%Y") as 'Start Date',DATE_FORMAT(la.end_Date,"%m-%d-%Y") as 'End Date', l.status
FROM LabAssignment la INNER JOIN Labs l ON l.labID = la.labID
INNER JOIN Department d ON d.departmentID = l.departmentID
INNER JOIN Users u ON u.userID = la.userID;
END$$

CREATE DEFINER=`vineetakandala`@`%` PROCEDURE `sp_UnassignLabs`(IN p_labID int(10) )
BEGIN
UPDATE Labs set status = 'Available' where labID=p_labID;
UPDATE LabAssignment set end_Date=curdate() where labID = p_labID and end_Date is NULL;
END$$

CREATE DEFINER=`vineetakandala`@`%` PROCEDURE `sp_ViewUniversityLabs`(IN p_userID varchar(20) )
BEGIN
select labID,labType,l.departmentID,d.deptName,l.status,c.collegeName from Labs l inner join Department d on l.departmentID = d.departmentID inner join College c on d.collegeID = c.collegeID inner join Organization o on o.OrganizationID = c.OrganizationID where c.OrganizationID in(select OrganizationID from college where collegeID in(select collegeID from Department where departmentID in(select departmentID from Users where userID ='p_userID')));
END$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `college`
--


CREATE TABLE IF NOT EXISTS `college` (
  `collegeID` int(10) NOT NULL,
  `collegeName` varchar(30) NOT NULL,
  `organizationID` int(10) NOT NULL,
  PRIMARY KEY (`collegeID`),
  KEY `organizationID` (`organizationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `college`
--

INSERT INTO `college` (`collegeID`, `collegeName`, `organizationID`) VALUES
(201, 'College of Business', 101),
(202, 'Col Liberal Arts & Science', 101),
(203, 'Col of Arts & Architecture', 101),
(204, 'Academic Affairs', 101);

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE IF NOT EXISTS `department` (
  `departmentID` int(10) NOT NULL,
  `deptName` varchar(30) NOT NULL,
  `collegeID` int(10) NOT NULL,
  PRIMARY KEY (`departmentID`),
  KEY `collegeID` (`collegeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`departmentID`, `deptName`, `collegeID`) VALUES
(301, 'Accounting', 201),
(302, 'Economics', 201),
(303, 'Finance', 201),
(304, 'Anthropology', 202),
(305, 'Biology', 202),
(306, 'Chemistry', 202),
(307, 'Dance', 203),
(308, 'Music', 203),
(309, 'School of Architecture', 203),
(399, 'Academic Affairs', 204);

-- --------------------------------------------------------

--
-- Table structure for table `labassignment`
--

CREATE TABLE IF NOT EXISTS `labassignment` (
  `assignmentID` int(11) NOT NULL AUTO_INCREMENT,
  `labID` int(10) NOT NULL,
  `userID` varchar(20) NOT NULL,
  `assignedBy` varchar(20) NOT NULL,
  `start_Date` date DEFAULT NULL,
  `end_Date` date DEFAULT NULL,
  PRIMARY KEY (`assignmentID`),
  KEY `labID` (`labID`),
  KEY `userID` (`userID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=20 ;

--
-- Dumping data for table `labassignment`
--

INSERT INTO `labassignment` (`assignmentID`, `labID`, `userID`, `assignedBy`, `start_Date`, `end_Date`) VALUES
(3, 1112, 'asever', 'admin', NULL, NULL),
(4, 1112, 'nnajjar', 'admin', NULL, NULL),
(5, 1112, 'nnajjar', 'admin', NULL, NULL),
(6, 1111, 'asever', 'admin', NULL, NULL),
(7, 1112, 'asever', 'admin', NULL, NULL),
(8, 1112, 'vprovost', 'admin', NULL, NULL);


-- --------------------------------------------------------

--
-- Table structure for table `labs`
--

CREATE TABLE IF NOT EXISTS `labs` (
  `labID` int(10) NOT NULL AUTO_INCREMENT,
  `labType` varchar(30) NOT NULL,
  `departmentID` int(10) NOT NULL,
  `labAreaSqft` varchar(20) DEFAULT NULL,
  `status` varchar(20) DEFAULT 'Available',
  PRIMARY KEY (`labID`),
  KEY `departmentID` (`departmentID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1118 ;

--
-- Dumping data for table `labs`
--

INSERT INTO `labs` (`labID`, `labType`, `departmentID`, `labAreaSqft`, `status`) VALUES
(1101, 'Accounting Lab', 301, '500', 'Assigned'),
(1102, 'Economics Lab', 302, '200', 'Assigned'),
(1103, 'Business Intelligence', 301, '746', 'Assigned'),
(1105, 'Botany Lab', 305, '489', 'Assigned'),
(1106, 'SeminarHalls', 301, '1000', 'Assigned'),
(1107, 'BI Lab', 303, '600', 'Available'),
(1108, 'Zoology Lab', 305, '990', 'Available'),
(1109, 'DNA Testing', 305, '850', 'Assigned'),
(1110, 'XRay Diffraction Lab', 306, '1000', 'Assigned'),
(1111, 'Mass Spectrometry Center', 306, '1000', 'Assigned'),
(1112, 'Paramagnetic Resonance Lab', 306, '1000', 'Available'),
(1114, 'test Lab', 303, '255', 'Assigned'),
(1117, 'Dance Studio', 307, '1090', 'Available');

-- --------------------------------------------------------

--
-- Table structure for table `organization`
--

CREATE TABLE IF NOT EXISTS `organization` (
  `organizationID` int(10) NOT NULL,
  `organizationName` varchar(30) NOT NULL,
  PRIMARY KEY (`organizationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `organization`
--

INSERT INTO `organization` (`organizationID`, `organizationName`) VALUES
(101, 'UNC Charlotte');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `userID` varchar(20) NOT NULL,
  `firstName` varchar(20) NOT NULL,
  `lastName` varchar(20) NOT NULL,
  `userPosition` varchar(30) NOT NULL,
  `password` varchar(20) NOT NULL,
  `departmentID` int(20) NOT NULL,
  PRIMARY KEY (`userID`),
  KEY `departmentID` (`departmentID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userID`, `firstName`, `lastName`, `userPosition`, `password`, `departmentID`) VALUES
('admin', 'Lokesh', 'Kannan', 'Admin', 'testpass', 399),
('asever', 'Ali', 'Sever', 'Faculty', 'test', 301),
('brian', 'brian', 'b', 'Dean', 'test', 307),
('dc_accounting', 'Jane', 'Myer', 'Dept_Chair', 'test', 301),
('dc_biology', 'John', 'Legend', 'Dept_Chair', 'test', 305),
('dc_chemistry', 'Ram', 'Kumar', 'Dept_Chair', 'test', 306),
('dean_Business', 'Jean', 'Gray', 'Dean', 'test', 301),
('nnajjar', 'Nadia', 'Najjar', 'Faculty', 'test', 306),
('wwang', 'Weichao', 'Wang', 'Faculty', 'test', 305);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `college`
--
ALTER TABLE `college`
  ADD CONSTRAINT `college_ibfk_1` FOREIGN KEY (`organizationID`) REFERENCES `organization` (`organizationID`);

--
-- Constraints for table `department`
--
ALTER TABLE `department`
  ADD CONSTRAINT `department_ibfk_1` FOREIGN KEY (`collegeID`) REFERENCES `college` (`collegeID`);

--
-- Constraints for table `labassignment`
--
ALTER TABLE `labassignment`
  ADD CONSTRAINT `labassignment_ibfk_1` FOREIGN KEY (`labID`) REFERENCES `labs` (`labID`),
  ADD CONSTRAINT `labassignment_ibfk_2` FOREIGN KEY (`userID`) REFERENCES `users` (`userID`);

--
-- Constraints for table `labs`
--
ALTER TABLE `labs`
  ADD CONSTRAINT `labs_ibfk_3` FOREIGN KEY (`departmentID`) REFERENCES `department` (`departmentID`);

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`departmentID`) REFERENCES `department` (`departmentID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
