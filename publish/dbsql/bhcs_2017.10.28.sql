/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50719
Source Host           : 127.0.0.1:3306
Source Database       : bhcs

Target Server Type    : MYSQL
Target Server Version : 50719
File Encoding         : 65001

Date: 2017-10-28 23:10:02
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for ClassBuilding
-- ----------------------------
DROP TABLE IF EXISTS `ClassBuilding`;
CREATE TABLE `ClassBuilding` (
  `BuildingId` char(36) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `Address` varchar(64) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  `IsDelete` bit(1) NOT NULL,
  PRIMARY KEY (`BuildingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClassBuilding
-- ----------------------------
INSERT INTO `ClassBuilding` VALUES ('ce96ad99-8602-4473-937c-c1fc95b24771', '德欣楼', '在天堂', '2017-10-26 18:58:27', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员', '2017-10-26 18:58:27', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员', '\0');

-- ----------------------------
-- Table structure for Classes
-- ----------------------------
DROP TABLE IF EXISTS `Classes`;
CREATE TABLE `Classes` (
  `ClassesId` char(36) NOT NULL,
  `ClassesNo` varchar(16) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `GradeId` char(36) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  `IsDelete` bit(1) NOT NULL,
  PRIMARY KEY (`ClassesId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Classes
-- ----------------------------
INSERT INTO `Classes` VALUES ('8d54632b-488d-40b7-8bd8-3133bd544203', '1002', '01班', '353151ca-19fb-4cd5-98e7-7df9f66c7a4a', '2017-10-22 22:37:01', 'd66f144e-b3ea-4dc8-bc23-bde94119832b', '', '2017-10-22 22:37:01', 'd66f144e-b3ea-4dc8-bc23-bde94119832b', '', '\0');

-- ----------------------------
-- Table structure for ClassRoom
-- ----------------------------
DROP TABLE IF EXISTS `ClassRoom`;
CREATE TABLE `ClassRoom` (
  `RoomId` char(36) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `RoomNo` varchar(16) NOT NULL,
  `ClassBuildingId` char(36) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  `IsDelete` bit(1) NOT NULL,
  PRIMARY KEY (`RoomId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClassRoom
-- ----------------------------
INSERT INTO `ClassRoom` VALUES ('a7d091d9-4005-44ee-9169-ba7b216c7eea', '课室1', 'Room001', 'ce96ad99-8602-4473-937c-c1fc95b24771', '2017-10-26 19:05:08', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员', '2017-10-26 19:05:08', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员', '\0');

-- ----------------------------
-- Table structure for Course
-- ----------------------------
DROP TABLE IF EXISTS `Course`;
CREATE TABLE `Course` (
  `CourseId` char(36) NOT NULL,
  `Name` varchar(32) NOT NULL,
  `NatureId` char(36) NOT NULL,
  `Hours` decimal(10,2) NOT NULL,
  `MajorId` char(36) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  `IsDelete` bit(1) NOT NULL,
  PRIMARY KEY (`CourseId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Course
-- ----------------------------
INSERT INTO `Course` VALUES ('1bf34c6d-3bb1-40c4-ba3c-b1f6605979e9', '专业01', '9f854f0d-5b07-4210-a814-f247bc826ef8', '12.00', 'fc58d48b-1753-485a-81d8-c59ad1098734', '2017-10-28 20:32:09', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员', '2017-10-28 20:32:09', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员', '\0');
INSERT INTO `Course` VALUES ('6ea03832-06d3-4c99-8c75-1be3e197bf40', '英语课', '5bf81b1f-31c0-4328-b7dc-638023ded7de', '1.00', 'fc58d48b-1753-485a-81d8-c59ad1098734', '2017-10-26 00:10:32', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '系统管理员', '2017-10-26 00:10:32', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '系统管理员', '\0');

-- ----------------------------
-- Table structure for CourseNature
-- ----------------------------
DROP TABLE IF EXISTS `CourseNature`;
CREATE TABLE `CourseNature` (
  `NatureId` char(36) NOT NULL,
  `Name` varchar(32) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  `IsDelete` bit(1) NOT NULL,
  PRIMARY KEY (`NatureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CourseNature
-- ----------------------------
INSERT INTO `CourseNature` VALUES ('5bf81b1f-31c0-4328-b7dc-638023ded7de', '公共课', '2017-10-26 00:02:43', 'a39dc228-2868-4e09-843b-1a480ad98a54', '', '2017-10-26 00:02:43', 'a39dc228-2868-4e09-843b-1a480ad98a54', '', '\0');
INSERT INTO `CourseNature` VALUES ('693f5c9e-a48a-4f76-a8ca-9c589930db7e', '选修课', '2017-10-26 00:09:56', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '系统管理员', '2017-10-26 00:09:56', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '系统管理员', '\0');
INSERT INTO `CourseNature` VALUES ('9f854f0d-5b07-4210-a814-f247bc826ef8', '专业课', '2017-10-26 00:06:51', '7684b7d4-b549-44a9-a6ec-ab3bd974e174', '', '2017-10-26 00:06:51', '7684b7d4-b549-44a9-a6ec-ab3bd974e174', '', '\0');

-- ----------------------------
-- Table structure for Function
-- ----------------------------
DROP TABLE IF EXISTS `Function`;
CREATE TABLE `Function` (
  `FuncId` char(36) NOT NULL,
  `PageId` char(36) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `Url` varchar(64) NOT NULL,
  `OperationNum` int(11) NOT NULL,
  PRIMARY KEY (`FuncId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Function
-- ----------------------------
INSERT INTO `Function` VALUES ('e69e4312-26e1-4e8d-8897-ed2d5df3ec14', 'b44e42a4-d705-4f15-8ac6-ab286dd48c67', 'dd', 'dddd', '1');

-- ----------------------------
-- Table structure for Grade
-- ----------------------------
DROP TABLE IF EXISTS `Grade`;
CREATE TABLE `Grade` (
  `GradeId` char(36) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  PRIMARY KEY (`GradeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Grade
-- ----------------------------
INSERT INTO `Grade` VALUES ('353151ca-19fb-4cd5-98e7-7df9f66c7a4a', '大一02级', '0', '2017-10-22 22:18:51', '3efab548-7448-4e91-88d0-8acd5f72622a', '', '2017-10-22 22:18:51', '3efab548-7448-4e91-88d0-8acd5f72622a', '');

-- ----------------------------
-- Table structure for Major
-- ----------------------------
DROP TABLE IF EXISTS `Major`;
CREATE TABLE `Major` (
  `MajorId` char(36) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `IsEnable` bit(1) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  `IsDelete` bit(1) NOT NULL,
  PRIMARY KEY (`MajorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Major
-- ----------------------------
INSERT INTO `Major` VALUES ('fc58d48b-1753-485a-81d8-c59ad1098734', '计算机', '', '2017-10-17 20:06:44', 'fc58d48b-1753-485a-81d8-c59ad1098734', '0', '2017-10-31 20:07:33', null, null, '\0');

-- ----------------------------
-- Table structure for Menu
-- ----------------------------
DROP TABLE IF EXISTS `Menu`;
CREATE TABLE `Menu` (
  `MenuId` char(36) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `Sort` int(11) NOT NULL,
  PRIMARY KEY (`MenuId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Menu
-- ----------------------------
INSERT INTO `Menu` VALUES ('00000000-0000-0000-0000-000000000000', '系统管理', '2');
INSERT INTO `Menu` VALUES ('2ffe527b-1744-4a65-afcb-5bd377e98e7e', '智能排课', '1');
INSERT INTO `Menu` VALUES ('4ada0ba0-c389-4b77-9eca-197c5e4c0c1b', '信息查询', '1');
INSERT INTO `Menu` VALUES ('fc58d48b-1753-485a-81d8-c59ad1098734', '首页', '1');

-- ----------------------------
-- Table structure for Page
-- ----------------------------
DROP TABLE IF EXISTS `Page`;
CREATE TABLE `Page` (
  `PageId` char(36) NOT NULL,
  `MenuId` char(36) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `Url` varchar(64) NOT NULL,
  PRIMARY KEY (`PageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Page
-- ----------------------------
INSERT INTO `Page` VALUES ('2ffe527b-1744-4a65-afcb-5bd377e98e7e', 'fc58d48b-1753-485a-81d8-c59ad1098734', '首页', '/home/index');
INSERT INTO `Page` VALUES ('b44e42a4-d705-4f15-8ac6-ab286dd48c67', '2ffe527b-1744-4a65-afcb-5bd377e98e7e', '页面管理', '/system/page');

-- ----------------------------
-- Table structure for Role
-- ----------------------------
DROP TABLE IF EXISTS `Role`;
CREATE TABLE `Role` (
  `RoleId` char(36) NOT NULL,
  `RoleName` varchar(16) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  `IsDelete` bit(1) NOT NULL,
  PRIMARY KEY (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Role
-- ----------------------------
INSERT INTO `Role` VALUES ('70db5f91-75b3-4377-aed0-bc3e801e3091', 'aaa', '2017-10-15 10:43:51', 'e0886349-c7a2-4c7b-ba00-be31301cf7b7', '', null, '00000000-0000-0000-0000-000000000000', null, '\0');
INSERT INTO `Role` VALUES ('a29e8eb5-4949-419e-8ad8-4c5b447c8a91', '学生', '2017-10-15 23:29:42', 'fc58d48b-1753-485a-81d8-c59ad1098734', '', null, '00000000-0000-0000-0000-000000000000', null, '\0');
INSERT INTO `Role` VALUES ('C5BEC45F-C3CF-4423-9982-18B3B928F39E', '系统管理员', '2017-09-09 12:12:12', 'C5BEC45F-C3CF-4423-9982-18B3B928F39E', '系统', null, null, null, '\0');

-- ----------------------------
-- Table structure for RoleMenu
-- ----------------------------
DROP TABLE IF EXISTS `RoleMenu`;
CREATE TABLE `RoleMenu` (
  `RoleMenuId` char(36) NOT NULL,
  `RoleId` char(36) NOT NULL,
  `MenuId` char(36) NOT NULL,
  `PageId` char(36) DEFAULT NULL,
  `FuncId` char(36) DEFAULT NULL,
  PRIMARY KEY (`RoleMenuId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of RoleMenu
-- ----------------------------
INSERT INTO `RoleMenu` VALUES ('fc58d48b-1753-485a-81d8-c59ad1098734', 'a29e8eb5-4949-419e-8ad8-4c5b447c8a91', 'fc58d48b-1753-485a-81d8-c59ad1098734', '2ffe527b-1744-4a65-afcb-5bd377e98e7e', null);

-- ----------------------------
-- Table structure for Student
-- ----------------------------
DROP TABLE IF EXISTS `Student`;
CREATE TABLE `Student` (
  `StudentId` char(36) NOT NULL,
  `StudentNo` varchar(16) NOT NULL,
  `ClassesId` char(36) NOT NULL,
  `GradeId` char(36) NOT NULL,
  `MajorId` char(36) NOT NULL,
  `UserId` char(36) NOT NULL,
  PRIMARY KEY (`StudentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Student
-- ----------------------------

-- ----------------------------
-- Table structure for Teacher
-- ----------------------------
DROP TABLE IF EXISTS `Teacher`;
CREATE TABLE `Teacher` (
  `TeacherId` char(36) NOT NULL,
  `UserId` char(36) NOT NULL,
  `MajorId` char(36) NOT NULL,
  PRIMARY KEY (`TeacherId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Teacher
-- ----------------------------
INSERT INTO `Teacher` VALUES ('4231da47-e358-483f-94fb-c90622a000cc', 'b8966bea-5d77-4dbf-8f6f-8a9d551b370d', 'fc58d48b-1753-485a-81d8-c59ad1098734');
INSERT INTO `Teacher` VALUES ('99250d55-38e7-452c-abc4-ecc2a4baca0d', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', 'fc58d48b-1753-485a-81d8-c59ad1098734');

-- ----------------------------
-- Table structure for TeachingPlan
-- ----------------------------
DROP TABLE IF EXISTS `TeachingPlan`;
CREATE TABLE `TeachingPlan` (
  `PlanId` char(36) NOT NULL,
  `Semester` varchar(32) NOT NULL,
  `Remark` varchar(512) DEFAULT NULL,
  `MajorId` char(36) NOT NULL,
  `GradeId` char(36) NOT NULL,
  `IsAccept` bit(1) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  PRIMARY KEY (`PlanId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of TeachingPlan
-- ----------------------------
INSERT INTO `TeachingPlan` VALUES ('8ac27a17-9544-40a6-8194-bcc703f391f9', '56', null, 'fc58d48b-1753-485a-81d8-c59ad1098734', '353151ca-19fb-4cd5-98e7-7df9f66c7a4a', '\0', '2017-10-28 21:16:06', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员', '2017-10-28 21:16:06', 'a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员');

-- ----------------------------
-- Table structure for TeachingPlanCourse
-- ----------------------------
DROP TABLE IF EXISTS `TeachingPlanCourse`;
CREATE TABLE `TeachingPlanCourse` (
  `PlanCourseId` char(36) NOT NULL,
  `TeachingPlanId` char(36) NOT NULL,
  `CourseId` char(36) NOT NULL,
  `ClassesId` char(36) DEFAULT NULL,
  `MainTeacherId` char(36) DEFAULT NULL,
  `CourseNumber` int(11) DEFAULT NULL,
  `BuildingId` char(36) DEFAULT NULL,
  PRIMARY KEY (`PlanCourseId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of TeachingPlanCourse
-- ----------------------------
INSERT INTO `TeachingPlanCourse` VALUES ('59eb0c4b-6f4f-4a94-b8db-597783249df6', '8ac27a17-9544-40a6-8194-bcc703f391f9', '1bf34c6d-3bb1-40c4-ba3c-b1f6605979e9', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '0', '00000000-0000-0000-0000-000000000000');
INSERT INTO `TeachingPlanCourse` VALUES ('e59c8fc4-c2a9-4b41-a2c3-23806ffc628d', '8ac27a17-9544-40a6-8194-bcc703f391f9', '6ea03832-06d3-4c99-8c75-1be3e197bf40', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '0', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for TeachingPlanCourseMerge
-- ----------------------------
DROP TABLE IF EXISTS `TeachingPlanCourseMerge`;
CREATE TABLE `TeachingPlanCourseMerge` (
  `MergeId` char(36) NOT NULL,
  `MergeCode` varchar(16) NOT NULL,
  `PlanCourseId` char(36) NOT NULL,
  `ClassesId` char(36) NOT NULL,
  PRIMARY KEY (`MergeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of TeachingPlanCourseMerge
-- ----------------------------

-- ----------------------------
-- Table structure for User
-- ----------------------------
DROP TABLE IF EXISTS `User`;
CREATE TABLE `User` (
  `UserId` char(36) NOT NULL,
  `UserName` varchar(16) NOT NULL,
  `Account` varchar(16) NOT NULL,
  `Password` varchar(64) NOT NULL,
  `Mobile` varchar(16) DEFAULT NULL,
  `Email` varchar(32) DEFAULT NULL,
  `RoleId` char(36) NOT NULL,
  `State` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateBy` char(36) NOT NULL,
  `CreateByName` varchar(16) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` char(36) DEFAULT NULL,
  `UpdateByName` varchar(16) DEFAULT NULL,
  `IsDelete` bit(1) NOT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of User
-- ----------------------------
INSERT INTO `User` VALUES ('a268c02c-f3e9-49a9-8361-8a7a7d33b0f1', '管理员', 'admin', 'e10adc3949ba59abbe56e057f20f883e', '12345678901', '11@ww.com', 'a29e8eb5-4949-419e-8ad8-4c5b447c8a91', '1', '2017-10-19 21:07:07', '48eb7fbe-ba5d-43e2-8eb7-c8f206650c54', '', '2017-10-19 21:07:07', '48eb7fbe-ba5d-43e2-8eb7-c8f206650c54', '', '\0');
INSERT INTO `User` VALUES ('b8966bea-5d77-4dbf-8f6f-8a9d551b370d', 'test', 'test1', '123456', '12345678901', '2@d.e', 'a29e8eb5-4949-419e-8ad8-4c5b447c8a91', '1', '2017-10-19 21:15:11', 'e90f80cd-9a46-4ba8-b1f0-a0951739bd5c', '', '2017-10-19 21:15:11', 'e90f80cd-9a46-4ba8-b1f0-a0951739bd5c', '', '\0');
