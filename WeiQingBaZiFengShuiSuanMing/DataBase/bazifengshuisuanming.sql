/*
Navicat MySQL Data Transfer

Source Server         : weiqingConn
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : bazifengshuisuanming

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2017-04-09 02:30:46
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for article
-- ----------------------------
DROP TABLE IF EXISTS `article`;
CREATE TABLE `article` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '站长发布文章的主键',
  `cateid` int(11) NOT NULL DEFAULT '0' COMMENT '文章分类表的id',
  `title` varchar(50) NOT NULL DEFAULT '' COMMENT '文章标题',
  `keywords` varchar(50) NOT NULL DEFAULT '' COMMENT '关键字',
  `addtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '添加时间',
  `uid` int(11) NOT NULL DEFAULT '0' COMMENT '添加者id',
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '状态, 1 可查看, 0 禁止显示',
  `top` bit(1) NOT NULL DEFAULT b'0' COMMENT '置顶, 默认不置顶 false',
  `sort` int(11) NOT NULL DEFAULT '100' COMMENT '排序,0-100, 从小到大排列',
  `content` varchar(3000) NOT NULL DEFAULT '' COMMENT '文章内容',
  `img` varchar(200) NOT NULL DEFAULT '' COMMENT '文章封面图片, 用于轮播图',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of article
-- ----------------------------
INSERT INTO `article` VALUES ('1', '1', '四月四日', '清明节时期', '2017-04-04 02:37:21', '1', '1', '\0', '100', '<p>上班好好做, 努力, 终于找到了一个不错的工作</p>', '');
INSERT INTO `article` VALUES ('2', '1', '四月五号', '年前诸事不顺', '2017-04-05 00:41:01', '1', '1', '\0', '100', '<p>心情复杂</p>', '');

-- ----------------------------
-- Table structure for bazijianpi
-- ----------------------------
DROP TABLE IF EXISTS `bazijianpi`;
CREATE TABLE `bazijianpi` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(20) NOT NULL DEFAULT '' COMMENT '求测者的姓名',
  `sex` bit(1) NOT NULL DEFAULT b'1' COMMENT '性别',
  `born_date` datetime NOT NULL COMMENT '出生日期',
  `born_place` varchar(50) NOT NULL DEFAULT '' COMMENT '出生地',
  `addtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '添加时间',
  `bazi` varchar(50) NOT NULL DEFAULT '' COMMENT '八字',
  `state` int(10) NOT NULL DEFAULT '0' COMMENT '状态,0为申请,1为已预测',
  `yuce_content` varchar(2000) NOT NULL DEFAULT '' COMMENT '预测结果',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bazijianpi
-- ----------------------------
INSERT INTO `bazijianpi` VALUES ('1', '求测者', '\0', '2017-03-22 21:30:00', '未知', '2017-03-22 21:30:25', '甲戌丙子戊辰癸亥', '1', '<p>命很好.</p>');
INSERT INTO `bazijianpi` VALUES ('2', '求测者', '\0', '2017-03-22 21:10:00', '未知', '2017-03-22 21:30:48', '', '0', '');
INSERT INTO `bazijianpi` VALUES ('3', '求测者', '\0', '2017-03-24 22:24:00', '未知', '2017-03-24 22:24:24', '', '0', '');
INSERT INTO `bazijianpi` VALUES ('4', '求测者', '', '2017-03-24 22:25:00', '未知', '2017-03-24 22:25:52', '乙亥乙酉乙酉乙酉', '1', '<p>很不错</p>');
INSERT INTO `bazijianpi` VALUES ('5', '求测者', '', '2017-03-27 12:46:00', '未知', '2017-03-27 12:46:05', '', '0', '');
INSERT INTO `bazijianpi` VALUES ('6', '求测者', '', '2017-03-08 12:46:00', '未知', '2017-03-27 12:46:10', '', '0', '');

-- ----------------------------
-- Table structure for category
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `category_name` varchar(30) NOT NULL DEFAULT '' COMMENT '分类名称',
  `img` varchar(200) NOT NULL DEFAULT '' COMMENT '分类名称旁边的小图存储的路径, 如果为空加载默认图片',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of category
-- ----------------------------
INSERT INTO `category` VALUES ('1', '每日一文', '');
INSERT INTO `category` VALUES ('2', '命学浅析', '');
INSERT INTO `category` VALUES ('3', '移动版', '');

-- ----------------------------
-- Table structure for getpwdlog
-- ----------------------------
DROP TABLE IF EXISTS `getpwdlog`;
CREATE TABLE `getpwdlog` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `uid` int(10) NOT NULL DEFAULT '0' COMMENT '找回密码的用户id',
  `nick_name` varchar(20) NOT NULL DEFAULT '' COMMENT '找回密码的用户名',
  `ip_address` varchar(20) NOT NULL DEFAULT '' COMMENT '用户的ip地址',
  `log_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of getpwdlog
-- ----------------------------
INSERT INTO `getpwdlog` VALUES ('1', '1', '张先生', '::1', '2017-03-22 01:09:08');

-- ----------------------------
-- Table structure for liuyanban
-- ----------------------------
DROP TABLE IF EXISTS `liuyanban`;
CREATE TABLE `liuyanban` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `uid` int(10) NOT NULL DEFAULT '0' COMMENT '留言者的uid, 游客为0',
  `uname` varchar(50) NOT NULL DEFAULT '' COMMENT '如果登录则保存用户名, 否则显示为游客',
  `addtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '添加时间',
  `ip` varchar(20) NOT NULL DEFAULT '' COMMENT '客户端的ip地址',
  `state` int(10) NOT NULL DEFAULT '1' COMMENT '状态,默认为1 显示, 0 不显示',
  `content` varchar(500) NOT NULL DEFAULT '' COMMENT '留言内容',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of liuyanban
-- ----------------------------
INSERT INTO `liuyanban` VALUES ('1', '0', '游客', '2017-04-05 03:54:13', '::1', '1', '第一次留言,占个一楼');
INSERT INTO `liuyanban` VALUES ('2', '1', '张先生', '2017-04-05 03:54:40', '::1', '1', '登录之后试试留言');
INSERT INTO `liuyanban` VALUES ('3', '1', 'admin666', '2017-04-09 02:00:52', '::1', '1', '准备睡觉了');

-- ----------------------------
-- Table structure for login_log
-- ----------------------------
DROP TABLE IF EXISTS `login_log`;
CREATE TABLE `login_log` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `uid` int(11) NOT NULL DEFAULT '0' COMMENT '用户的id',
  `login_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '登录时间',
  `login_ip` varchar(30) NOT NULL DEFAULT '' COMMENT '登录的ip地址',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=151 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of login_log
-- ----------------------------
INSERT INTO `login_log` VALUES ('1', '1', '2017-03-22 18:21:33', '::1');
INSERT INTO `login_log` VALUES ('2', '1', '2017-03-23 23:17:58', '::1');
INSERT INTO `login_log` VALUES ('3', '1', '2017-03-23 23:22:44', '::1');
INSERT INTO `login_log` VALUES ('4', '1', '2017-03-24 00:35:47', '::1');
INSERT INTO `login_log` VALUES ('5', '1', '2017-03-24 22:22:22', '::1');
INSERT INTO `login_log` VALUES ('6', '1', '2017-03-24 23:22:04', '::1');
INSERT INTO `login_log` VALUES ('7', '1', '2017-03-24 23:44:22', '::1');
INSERT INTO `login_log` VALUES ('8', '1', '2017-03-24 23:55:31', '::1');
INSERT INTO `login_log` VALUES ('9', '1', '2017-03-26 01:37:47', '::1');
INSERT INTO `login_log` VALUES ('10', '1', '2017-03-26 12:19:42', '::1');
INSERT INTO `login_log` VALUES ('11', '1', '2017-03-27 00:30:57', '::1');
INSERT INTO `login_log` VALUES ('12', '1', '2017-03-27 01:03:06', '::1');
INSERT INTO `login_log` VALUES ('13', '1', '2017-03-27 01:08:21', '::1');
INSERT INTO `login_log` VALUES ('14', '1', '2017-03-27 01:14:07', '::1');
INSERT INTO `login_log` VALUES ('15', '1', '2017-03-27 01:23:06', '::1');
INSERT INTO `login_log` VALUES ('16', '1', '2017-03-27 10:35:04', '::1');
INSERT INTO `login_log` VALUES ('17', '1', '2017-03-27 12:04:40', '::1');
INSERT INTO `login_log` VALUES ('18', '1', '2017-03-27 12:07:35', '::1');
INSERT INTO `login_log` VALUES ('19', '1', '2017-03-27 12:11:48', '::1');
INSERT INTO `login_log` VALUES ('20', '1', '2017-03-27 12:15:10', '::1');
INSERT INTO `login_log` VALUES ('21', '1', '2017-03-27 12:18:10', '::1');
INSERT INTO `login_log` VALUES ('22', '1', '2017-03-27 12:19:52', '::1');
INSERT INTO `login_log` VALUES ('23', '1', '2017-03-27 12:44:35', '::1');
INSERT INTO `login_log` VALUES ('24', '1', '2017-03-27 12:53:11', '::1');
INSERT INTO `login_log` VALUES ('25', '1', '2017-03-27 13:02:02', '::1');
INSERT INTO `login_log` VALUES ('26', '1', '2017-03-27 13:06:30', '::1');
INSERT INTO `login_log` VALUES ('27', '1', '2017-03-27 14:58:13', '::1');
INSERT INTO `login_log` VALUES ('28', '1', '2017-03-27 16:47:18', '::1');
INSERT INTO `login_log` VALUES ('29', '1', '2017-03-27 16:58:53', '::1');
INSERT INTO `login_log` VALUES ('30', '1', '2017-03-27 18:32:32', '::1');
INSERT INTO `login_log` VALUES ('31', '1', '2017-03-27 23:48:04', '::1');
INSERT INTO `login_log` VALUES ('32', '1', '2017-03-27 23:55:16', '::1');
INSERT INTO `login_log` VALUES ('33', '1', '2017-03-27 23:57:08', '::1');
INSERT INTO `login_log` VALUES ('34', '1', '2017-03-28 00:28:53', '::1');
INSERT INTO `login_log` VALUES ('35', '1', '2017-03-28 00:34:49', '::1');
INSERT INTO `login_log` VALUES ('36', '1', '2017-03-29 00:52:47', '::1');
INSERT INTO `login_log` VALUES ('37', '1', '2017-03-30 02:02:04', '::1');
INSERT INTO `login_log` VALUES ('38', '1', '2017-03-30 23:57:23', '::1');
INSERT INTO `login_log` VALUES ('39', '1', '2017-03-31 00:21:18', '::1');
INSERT INTO `login_log` VALUES ('40', '1', '2017-03-31 02:26:21', '::1');
INSERT INTO `login_log` VALUES ('41', '1', '2017-03-31 02:27:52', '::1');
INSERT INTO `login_log` VALUES ('42', '1', '2017-03-31 16:18:30', '::1');
INSERT INTO `login_log` VALUES ('43', '1', '2017-03-31 16:40:43', '::1');
INSERT INTO `login_log` VALUES ('44', '1', '2017-03-31 16:52:35', '::1');
INSERT INTO `login_log` VALUES ('45', '1', '2017-03-31 20:19:40', '::1');
INSERT INTO `login_log` VALUES ('46', '1', '2017-03-31 20:48:11', '::1');
INSERT INTO `login_log` VALUES ('47', '1', '2017-03-31 21:00:36', '::1');
INSERT INTO `login_log` VALUES ('48', '1', '2017-03-31 21:16:41', '::1');
INSERT INTO `login_log` VALUES ('49', '1', '2017-04-01 23:12:31', '::1');
INSERT INTO `login_log` VALUES ('50', '1', '2017-04-01 23:12:56', '::1');
INSERT INTO `login_log` VALUES ('51', '1', '2017-04-01 23:13:17', '::1');
INSERT INTO `login_log` VALUES ('52', '1', '2017-04-01 23:13:34', '::1');
INSERT INTO `login_log` VALUES ('53', '1', '2017-04-01 23:21:04', '::1');
INSERT INTO `login_log` VALUES ('54', '1', '2017-04-01 23:24:35', '::1');
INSERT INTO `login_log` VALUES ('55', '1', '2017-04-01 23:25:07', '::1');
INSERT INTO `login_log` VALUES ('56', '1', '2017-04-02 01:04:20', '::1');
INSERT INTO `login_log` VALUES ('57', '1', '2017-04-02 01:05:12', '::1');
INSERT INTO `login_log` VALUES ('58', '1', '2017-04-02 02:38:29', '::1');
INSERT INTO `login_log` VALUES ('59', '1', '2017-04-02 02:55:45', '::1');
INSERT INTO `login_log` VALUES ('60', '1', '2017-04-02 18:21:09', '::1');
INSERT INTO `login_log` VALUES ('61', '1', '2017-04-02 22:54:00', '::1');
INSERT INTO `login_log` VALUES ('62', '1', '2017-04-02 22:58:05', '::1');
INSERT INTO `login_log` VALUES ('63', '1', '2017-04-02 22:58:43', '::1');
INSERT INTO `login_log` VALUES ('64', '1', '2017-04-02 23:00:31', '::1');
INSERT INTO `login_log` VALUES ('65', '1', '2017-04-02 23:46:05', '::1');
INSERT INTO `login_log` VALUES ('66', '1', '2017-04-03 02:23:15', '::1');
INSERT INTO `login_log` VALUES ('67', '1', '2017-04-03 03:35:04', '::1');
INSERT INTO `login_log` VALUES ('68', '1', '2017-04-03 03:58:16', '::1');
INSERT INTO `login_log` VALUES ('69', '1', '2017-04-03 04:01:49', '::1');
INSERT INTO `login_log` VALUES ('70', '1', '2017-04-03 04:04:04', '::1');
INSERT INTO `login_log` VALUES ('71', '1', '2017-04-03 04:35:35', '::1');
INSERT INTO `login_log` VALUES ('72', '1', '2017-04-03 04:36:06', '::1');
INSERT INTO `login_log` VALUES ('73', '1', '2017-04-03 04:38:20', '::1');
INSERT INTO `login_log` VALUES ('74', '1', '2017-04-03 04:38:42', '::1');
INSERT INTO `login_log` VALUES ('75', '1', '2017-04-03 04:38:59', '::1');
INSERT INTO `login_log` VALUES ('76', '1', '2017-04-03 04:52:52', '::1');
INSERT INTO `login_log` VALUES ('77', '1', '2017-04-03 17:04:34', '::1');
INSERT INTO `login_log` VALUES ('78', '1', '2017-04-03 17:08:41', '::1');
INSERT INTO `login_log` VALUES ('79', '1', '2017-04-03 17:09:20', '::1');
INSERT INTO `login_log` VALUES ('80', '1', '2017-04-03 17:59:23', '::1');
INSERT INTO `login_log` VALUES ('81', '1', '2017-04-03 19:05:07', '::1');
INSERT INTO `login_log` VALUES ('82', '1', '2017-04-03 19:21:01', '::1');
INSERT INTO `login_log` VALUES ('83', '1', '2017-04-03 19:28:37', '::1');
INSERT INTO `login_log` VALUES ('84', '1', '2017-04-03 19:56:42', '::1');
INSERT INTO `login_log` VALUES ('85', '1', '2017-04-03 20:10:11', '::1');
INSERT INTO `login_log` VALUES ('86', '1', '2017-04-03 20:23:27', '::1');
INSERT INTO `login_log` VALUES ('87', '1', '2017-04-03 20:31:56', '::1');
INSERT INTO `login_log` VALUES ('88', '1', '2017-04-03 20:34:38', '::1');
INSERT INTO `login_log` VALUES ('89', '1', '2017-04-03 20:37:04', '::1');
INSERT INTO `login_log` VALUES ('90', '1', '2017-04-03 20:40:24', '::1');
INSERT INTO `login_log` VALUES ('91', '1', '2017-04-03 23:38:53', '::1');
INSERT INTO `login_log` VALUES ('92', '1', '2017-04-04 00:34:41', '::1');
INSERT INTO `login_log` VALUES ('93', '1', '2017-04-04 02:31:37', '::1');
INSERT INTO `login_log` VALUES ('94', '1', '2017-04-04 02:37:13', '::1');
INSERT INTO `login_log` VALUES ('95', '1', '2017-04-04 03:18:26', '::1');
INSERT INTO `login_log` VALUES ('96', '1', '2017-04-04 19:09:08', '::1');
INSERT INTO `login_log` VALUES ('97', '1', '2017-04-04 22:17:55', '::1');
INSERT INTO `login_log` VALUES ('98', '1', '2017-04-04 22:33:45', '::1');
INSERT INTO `login_log` VALUES ('99', '1', '2017-04-04 23:54:59', '::1');
INSERT INTO `login_log` VALUES ('100', '1', '2017-04-05 00:36:46', '::1');
INSERT INTO `login_log` VALUES ('101', '1', '2017-04-05 01:28:59', '::1');
INSERT INTO `login_log` VALUES ('102', '1', '2017-04-05 03:54:23', '::1');
INSERT INTO `login_log` VALUES ('103', '1', '2017-04-05 11:20:15', '::1');
INSERT INTO `login_log` VALUES ('104', '1', '2017-04-05 11:31:14', '::1');
INSERT INTO `login_log` VALUES ('105', '1', '2017-04-05 12:35:22', '::1');
INSERT INTO `login_log` VALUES ('106', '2', '2017-04-05 12:36:57', '::1');
INSERT INTO `login_log` VALUES ('107', '2', '2017-04-05 12:40:25', '::1');
INSERT INTO `login_log` VALUES ('108', '3', '2017-04-05 12:40:55', '::1');
INSERT INTO `login_log` VALUES ('109', '4', '2017-04-05 12:42:43', '::1');
INSERT INTO `login_log` VALUES ('110', '4', '2017-04-05 12:42:48', '::1');
INSERT INTO `login_log` VALUES ('111', '1', '2017-04-05 12:43:04', '::1');
INSERT INTO `login_log` VALUES ('112', '1', '2017-04-05 12:48:04', '127.0.0.1');
INSERT INTO `login_log` VALUES ('113', '1', '2017-04-05 12:51:22', '127.0.0.1');
INSERT INTO `login_log` VALUES ('114', '1', '2017-04-05 12:51:29', '127.0.0.1');
INSERT INTO `login_log` VALUES ('115', '1', '2017-04-05 14:39:25', '127.0.0.1');
INSERT INTO `login_log` VALUES ('116', '1', '2017-04-05 18:14:03', '127.0.0.1');
INSERT INTO `login_log` VALUES ('117', '1', '2017-04-05 18:21:28', '127.0.0.1');
INSERT INTO `login_log` VALUES ('118', '1', '2017-04-05 21:10:02', '127.0.0.1');
INSERT INTO `login_log` VALUES ('119', '1', '2017-04-06 22:01:36', '127.0.0.1');
INSERT INTO `login_log` VALUES ('120', '1', '2017-04-06 23:18:55', '127.0.0.1');
INSERT INTO `login_log` VALUES ('121', '1', '2017-04-06 23:23:36', '::1');
INSERT INTO `login_log` VALUES ('122', '1', '2017-04-06 23:25:00', '::1');
INSERT INTO `login_log` VALUES ('123', '1', '2017-04-06 23:25:19', '::1');
INSERT INTO `login_log` VALUES ('124', '1', '2017-04-06 23:25:31', '::1');
INSERT INTO `login_log` VALUES ('125', '1', '2017-04-07 11:51:30', '::1');
INSERT INTO `login_log` VALUES ('126', '1', '2017-04-07 13:10:43', '::1');
INSERT INTO `login_log` VALUES ('127', '1', '2017-04-07 17:57:24', '::1');
INSERT INTO `login_log` VALUES ('128', '1', '2017-04-07 18:38:57', '::1');
INSERT INTO `login_log` VALUES ('129', '1', '2017-04-07 18:58:49', '::1');
INSERT INTO `login_log` VALUES ('130', '1', '2017-04-07 19:20:27', '::1');
INSERT INTO `login_log` VALUES ('131', '1', '2017-04-07 23:48:41', '::1');
INSERT INTO `login_log` VALUES ('132', '1', '2017-04-08 01:59:22', '::1');
INSERT INTO `login_log` VALUES ('133', '1', '2017-04-08 17:18:57', '::1');
INSERT INTO `login_log` VALUES ('134', '1', '2017-04-08 18:07:34', '::1');
INSERT INTO `login_log` VALUES ('135', '1', '2017-04-08 19:14:38', '::1');
INSERT INTO `login_log` VALUES ('136', '1', '2017-04-08 20:43:36', '::1');
INSERT INTO `login_log` VALUES ('137', '1', '2017-04-08 21:37:21', '::1');
INSERT INTO `login_log` VALUES ('138', '1', '2017-04-08 22:36:03', '::1');
INSERT INTO `login_log` VALUES ('139', '1', '2017-04-08 23:40:58', '::1');
INSERT INTO `login_log` VALUES ('140', '1', '2017-04-09 00:59:20', '::1');
INSERT INTO `login_log` VALUES ('141', '1', '2017-04-09 01:05:31', '::1');
INSERT INTO `login_log` VALUES ('142', '1', '2017-04-09 01:07:26', '::1');
INSERT INTO `login_log` VALUES ('143', '1', '2017-04-09 01:09:25', '::1');
INSERT INTO `login_log` VALUES ('144', '1', '2017-04-09 01:42:21', '::1');
INSERT INTO `login_log` VALUES ('145', '1', '2017-04-09 01:47:02', '::1');
INSERT INTO `login_log` VALUES ('146', '1', '2017-04-09 01:47:14', '::1');
INSERT INTO `login_log` VALUES ('147', '1', '2017-04-09 01:47:22', '::1');
INSERT INTO `login_log` VALUES ('148', '1', '2017-04-09 01:48:02', '::1');
INSERT INTO `login_log` VALUES ('149', '1', '2017-04-09 02:16:34', '::1');
INSERT INTO `login_log` VALUES ('150', '1', '2017-04-09 02:19:23', '127.0.0.1');

-- ----------------------------
-- Table structure for notice
-- ----------------------------
DROP TABLE IF EXISTS `notice`;
CREATE TABLE `notice` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '公告表的主键',
  `uid` int(11) NOT NULL DEFAULT '0' COMMENT '发布的用户id',
  `addtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发布时间',
  `content` varchar(1000) NOT NULL DEFAULT '' COMMENT '公告内容',
  `show` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否显示',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of notice
-- ----------------------------
INSERT INTO `notice` VALUES ('1', '1', '2017-04-02 02:38:56', '<p>今日暂无公告,没朋友</p>', '');

-- ----------------------------
-- Table structure for tiezi
-- ----------------------------
DROP TABLE IF EXISTS `tiezi`;
CREATE TABLE `tiezi` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000' COMMENT '帖子的id,title表的id',
  `addtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '添加时间',
  `uid` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '楼层的uid',
  `floor` int(11) NOT NULL DEFAULT '0' COMMENT '楼层',
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '状态,是否可以查看等,默认只加载状态为1的内容',
  `uname` varchar(20) NOT NULL DEFAULT '' COMMENT '发帖人的用户名,减少不必要的查询',
  `content` varchar(3000) NOT NULL DEFAULT '' COMMENT '帖子回复',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tiezi
-- ----------------------------
INSERT INTO `tiezi` VALUES ('1', '0000000001', '2017-03-27 23:57:29', '1', '1', '1', '张先生', '<p>第一次创建帖子</p>');
INSERT INTO `tiezi` VALUES ('2', '0000000002', '2017-03-28 00:34:53', '1', '1', '1', '张先生', '<p>心情复杂</p>');
INSERT INTO `tiezi` VALUES ('3', '0000000001', '2017-03-31 20:49:51', '1', '2', '1', '张先生', '<p>回帖测试看看</p>');
INSERT INTO `tiezi` VALUES ('4', '0000000001', '2017-03-31 20:50:18', '1', '3', '1', '张先生', '<p>我求饶恶趣味</p>');
INSERT INTO `tiezi` VALUES ('5', '0000000001', '2017-03-31 20:50:26', '1', '4', '1', '张先生', '<p>沃尔沃热</p>');
INSERT INTO `tiezi` VALUES ('6', '0000000001', '2017-03-31 20:50:30', '1', '5', '1', '张先生', '<p>玩儿玩儿</p>');
INSERT INTO `tiezi` VALUES ('7', '0000000001', '2017-03-31 20:51:17', '1', '6', '1', '张先生', '<p>威尔而亡</p>');
INSERT INTO `tiezi` VALUES ('8', '0000000001', '2017-03-31 20:51:22', '1', '7', '1', '张先生', '<p>我二七区委</p>');
INSERT INTO `tiezi` VALUES ('9', '0000000001', '2017-03-31 20:51:30', '1', '8', '1', '张先生', '<p>威尔认为</p>');
INSERT INTO `tiezi` VALUES ('10', '0000000001', '2017-03-31 20:53:09', '1', '9', '1', '张先生', '<p>请问请问</p>');
INSERT INTO `tiezi` VALUES ('11', '0000000001', '2017-03-31 20:56:08', '1', '10', '1', '张先生', '<p>werwr</p>');
INSERT INTO `tiezi` VALUES ('12', '0000000001', '2017-03-31 20:59:03', '1', '11', '1', '张先生', '<p>玩儿玩儿</p>');
INSERT INTO `tiezi` VALUES ('13', '0000000001', '2017-03-31 21:00:41', '1', '12', '1', '张先生', '<p>玩儿玩儿</p>');
INSERT INTO `tiezi` VALUES ('14', '0000000001', '2017-03-31 21:16:47', '1', '13', '1', '张先生', '<p>weq qwweqweq</p>');
INSERT INTO `tiezi` VALUES ('15', '0000000003', '2017-04-09 01:13:21', '1', '1', '1', 'admin666', '<p>大家不要在论坛发布其他无关的内容<br/></p>');

-- ----------------------------
-- Table structure for tiezi_jubao
-- ----------------------------
DROP TABLE IF EXISTS `tiezi_jubao`;
CREATE TABLE `tiezi_jubao` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tzid` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '指定楼层的帖子id,tiezi表的主键',
  `reason` varchar(500) NOT NULL DEFAULT '' COMMENT '举报的理由',
  `addtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '举报时间',
  `ip` varchar(50) NOT NULL DEFAULT '' COMMENT '举报人的ip地址',
  `jb_type` bit(1) NOT NULL DEFAULT b'1' COMMENT '举报类型,1 内容,0 回复',
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '审核状态,0 待审核, 1 已审核',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tiezi_jubao
-- ----------------------------
INSERT INTO `tiezi_jubao` VALUES ('1', '1', '我去饿啊饿', '2017-03-31 19:01:40', '::1', '\0', '1');
INSERT INTO `tiezi_jubao` VALUES ('2', '10', '举报测试', '2017-04-03 17:50:32', '::1', '\0', '0');
INSERT INTO `tiezi_jubao` VALUES ('3', '8', '举报内容看看', '2017-04-03 17:52:04', '::1', '', '0');

-- ----------------------------
-- Table structure for title
-- ----------------------------
DROP TABLE IF EXISTS `title`;
CREATE TABLE `title` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `uid` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '楼主',
  `art_title` varchar(100) NOT NULL DEFAULT '' COMMENT '标题',
  `keywords` varchar(100) NOT NULL DEFAULT '' COMMENT '关键字',
  `top` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否置顶,默认为0 不置顶',
  `sort` int(11) NOT NULL DEFAULT '100' COMMENT '排序, 0-100之间, 默认为100, 从小到大排列',
  `state` int(10) unsigned NOT NULL DEFAULT '1' COMMENT '是否可见',
  `addtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of title
-- ----------------------------
INSERT INTO `title` VALUES ('1', '1', '阿尔文', '王企鹅', '', '100', '1', '2017-03-27 23:57:29');
INSERT INTO `title` VALUES ('2', '1', '想要过的更好', '心情不好', '\0', '100', '1', '2017-03-28 00:34:53');
INSERT INTO `title` VALUES ('3', '1', '论坛相关说明', '新规定', '\0', '100', '1', '2017-04-09 01:13:21');

-- ----------------------------
-- Table structure for tzreply
-- ----------------------------
DROP TABLE IF EXISTS `tzreply`;
CREATE TABLE `tzreply` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tzid` int(11) NOT NULL DEFAULT '0' COMMENT 'tiezi表中的id',
  `uid` int(11) NOT NULL DEFAULT '0' COMMENT '用户id, 回复人的id',
  `addtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '添加时间',
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '状态,是否可查看等',
  `uname` varchar(20) NOT NULL DEFAULT '' COMMENT '回复人的用户名,  减少不必要的查询',
  `content` varchar(255) NOT NULL DEFAULT '' COMMENT '回复的内容',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tzreply
-- ----------------------------
INSERT INTO `tzreply` VALUES ('1', '1', '1', '2017-03-31 16:40:54', '1', '张先生', 'tertiary');
INSERT INTO `tzreply` VALUES ('2', '1', '1', '2017-03-31 16:41:52', '1', '张先生', '人参果投诉人');

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键,标识列',
  `nick_name` varchar(20) NOT NULL DEFAULT '未填写' COMMENT '昵称',
  `pwd` varchar(200) NOT NULL COMMENT '密码,保存的是hash算法处理之后的值',
  `email` varchar(50) NOT NULL COMMENT '邮箱,必须填写, 可以用于找回密码',
  `mobile` varchar(11) NOT NULL DEFAULT '' COMMENT '手机号,非必填, 当邮箱不可用的情况可以用于找回密码',
  `qq` varchar(11) NOT NULL DEFAULT '' COMMENT 'qq号,非必填,邮箱不可用时 可以用于找回密码',
  `wei_xin` varchar(50) NOT NULL DEFAULT '' COMMENT '微信号',
  `reg_date` datetime NOT NULL COMMENT '注册时间',
  `is_admin` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否为管理员.默认为否',
  `state` int(11) NOT NULL DEFAULT '1' COMMENT '状态,默认为可登录,被禁止后不能登录',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('1', 'admin666', '8868A3F76C3D792904D831C44612A377C04A58C9', '694060865@qq.com', '', '', '', '2017-03-18 21:38:02', '', '1');
INSERT INTO `user` VALUES ('4', '本间芽衣子', 'F6687CF03F6661A4F37293E247E200E757473236', '2312302652@qq.com', '', '', '', '2017-04-05 12:42:44', '\0', '1');
