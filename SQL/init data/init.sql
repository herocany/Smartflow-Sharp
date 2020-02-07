--如果与当前数据库的关键字有冲突，请自行调整
USE Demo

GO
--T_USER
SET IDENTITY_INSERT T_USER ON 
INSERT INTO T_USER(IDENTIFICATION,USERNAME,USERPWD,EMPLOYEENAME,ORGCODE,ORGNAME) VALUES(1,'chengderen','123456','程德忍','001001','网络研发一组')
INSERT INTO T_USER(IDENTIFICATION,USERNAME,USERPWD,EMPLOYEENAME,ORGCODE,ORGNAME) VALUES(2,'xuq','123456','徐群','001001','网络研发一组')
INSERT INTO T_USER(IDENTIFICATION,USERNAME,USERPWD,EMPLOYEENAME,ORGCODE,ORGNAME) VALUES(3,'xyq','123456','徐焰群','001002','网络研发二组')
INSERT INTO T_USER(IDENTIFICATION,USERNAME,USERPWD,EMPLOYEENAME,ORGCODE,ORGNAME) VALUES(4,'zhangsan','123456','张三','002','市场部')
INSERT INTO T_USER(IDENTIFICATION,USERNAME,USERPWD,EMPLOYEENAME,ORGCODE,ORGNAME) VALUES(5,'wangwu','123456','王五','002','市场部')
INSERT INTO T_USER(IDENTIFICATION,USERNAME,USERPWD,EMPLOYEENAME,ORGCODE,ORGNAME) VALUES(6,'wanger','123456','王二','002','市场部')
INSERT INTO T_USER(IDENTIFICATION,USERNAME,USERPWD,EMPLOYEENAME,ORGCODE,ORGNAME) VALUES(7,'libin','123456','李斌','002','市场部')
INSERT INTO T_USER(IDENTIFICATION,USERNAME,USERPWD,EMPLOYEENAME,ORGCODE,ORGNAME) VALUES(8,'zhongsan','123456','钟三','002','市场部')
SET IDENTITY_INSERT T_USER OFF 


SET IDENTITY_INSERT T_ROLE ON 
INSERT INTO T_ROLE(Identification,Appellation)VALUES(1,'系统管理员')
INSERT INTO T_ROLE(Identification,Appellation)VALUES(2,'小组长')
INSERT INTO T_ROLE(Identification,Appellation)VALUES(3,'总经理')
INSERT INTO T_ROLE(Identification,Appellation)VALUES(4,'部门经理')
INSERT INTO T_ROLE(Identification,Appellation)VALUES(5,'部门助理')
INSERT INTO T_ROLE(Identification,Appellation)VALUES(6,'总经理秘书')
INSERT INTO T_ROLE(Identification,Appellation)VALUES(7,'项目经理')
INSERT INTO T_ROLE(Identification,Appellation)VALUES(8,'市场部经理')
SET IDENTITY_INSERT T_ROLE OFF 

--T_UMR
INSERT INTO T_UMR(RID,UUID)VALUES(1,1)
INSERT INTO T_UMR(RID,UUID)VALUES(1,2)
INSERT INTO T_UMR(RID,UUID)VALUES(1,3)
INSERT INTO T_UMR(RID,UUID)VALUES(2,4)
INSERT INTO T_UMR(RID,UUID)VALUES(2,8)
INSERT INTO T_UMR(RID,UUID)VALUES(3,1)
INSERT INTO T_UMR(RID,UUID)VALUES(4,2)
INSERT INTO T_UMR(RID,UUID)VALUES(4,5)
INSERT INTO T_UMR(RID,UUID)VALUES(5,7)
INSERT INTO T_UMR(RID,UUID)VALUES(6,6)
INSERT INTO T_UMR(RID,UUID)VALUES(7,3)
INSERT INTO T_UMR(RID,UUID)VALUES(7,8)
INSERT INTO T_UMR(RID,UUID)VALUES(8,4)


--T_ORG
INSERT INTO T_ORG(ORGNAME,ORGCODE,PARENTCODE,DESCRIPTION)VALUES('组织机构','000','0','')
INSERT INTO T_ORG(ORGNAME,ORGCODE,PARENTCODE,DESCRIPTION)VALUES('网络研发部','001','000','')
INSERT INTO T_ORG(ORGNAME,ORGCODE,PARENTCODE,DESCRIPTION)VALUES('市场部','002','000','')
INSERT INTO T_ORG(ORGNAME,ORGCODE,PARENTCODE,DESCRIPTION)VALUES('综合管理部','003','000','')
INSERT INTO T_ORG(ORGNAME,ORGCODE,PARENTCODE,DESCRIPTION)VALUES('后勤保障门','004','000','')
INSERT INTO T_ORG(ORGNAME,ORGCODE,PARENTCODE,DESCRIPTION)VALUES('网络研发一组','001001','001','')
INSERT INTO T_ORG(ORGNAME,ORGCODE,PARENTCODE,DESCRIPTION)VALUES('网络研发二组','001002','001','')

GO

USE [Smartflow]
GO
INSERT [dbo].[t_category] ([NID], [Name], [Url], [Script], [Expression]) VALUES (N'7F1CC595-5BB1-4144-A72F-C509D4ACC3FB', N'请假流程', N'../Smartflow.Web.Mvc/Vacation/edit.html', N'UPDATE T_VACATION SET  [NodeName] = @NodeName   WHERE NID=@NID', N'')
GO
--请整调整连接符串
INSERT [dbo].[t_config] ([ID], [Name], [ConnectionString], [ProviderName]) VALUES (1, N'业务库', N'server=127.0.0.1;database=Demo;uid=sa;pwd=123456', N'System.Data.SqlClient')
GO
INSERT [dbo].[t_structure] ([NID], [StructName], [StructXml], [CateCode], [CateName], [Status], [Memo], [CreateDateTime]) VALUES (N'0440A7FE-E124-4CF9-AC43-13264EF22287', N'请假流程（2.0）', N'<workflow mode="Mix"><start id="32" name="开始" layout="267 32 74 5" category="start"><transition name="line" destination="34" layout="327,74 395,73" id="40" direction="go"></transition></start><end id="33" name="结束" layout="273 23 290 2" category="end"></end><node id="34" name="节点" layout="395 79 53 14" category="node"><transition name="line" destination="35" layout="575,73 685,73" id="41" direction="go"></transition><transition name="原路退回" destination="32" layout="327,74 395,73" id="40" direction="back"></transition></node><node id="35" name="节点" layout="685 79 53 14" category="node"><transition name="line" destination="36" layout="775,93 775,154" id="42" direction="go"></transition><transition name="原路退回" destination="34" layout="575,73 685,73" id="41" direction="back"></transition></node><node id="36" name="节点" layout="685 105 154 13" category="node"><transition name="line" destination="37" layout="775,194 776,266" id="43" direction="go"></transition><transition name="line" destination="38" layout="685,174 578,174" id="44" direction="go"></transition><transition name="原路退回" destination="35" layout="775,93 775,154" id="42" direction="back"></transition></node><node id="37" name="节点" layout="686 118 266 11" category="node"><transition name="line" destination="33" layout="686,286 333,290" id="45" direction="go"></transition><transition name="原路退回" destination="36" layout="775,194 776,266" id="43" direction="back"></transition></node><node id="38" name="节点" layout="398 87 154 12" category="node"><transition name="line" destination="33" layout="398,174 303,175 303,260" id="46" direction="go"><marker x="298" y="170" length="62"/></transition><transition name="原路退回" destination="36" layout="685,174 578,174" id="44" direction="back"></transition></node></workflow>', N'7F1CC595-5BB1-4144-A72F-C509D4ACC3FB', N'请假流程', 0, N'横向节点', NULL)
GO
INSERT [dbo].[t_structure] ([NID], [StructName], [StructXml], [CateCode], [CateName], [Status], [Memo], [CreateDateTime]) VALUES (N'292087CB-C2EE-4F3F-8A64-9FD2795818D8', N'新版—流程测试', N'<workflow mode="Transition"><start id="32" name="开始" layout="191 23 94 -7" category="start" cooperation="0"><transition name="提交部门经理审批" destination="34" layout="251,94 350,94" id="37" direction="go"></transition></start><end id="33" name="结束" layout="451 28 221 -2" category="end" cooperation="0"></end><node id="34" name="部门经理" layout="350 118 74 21" category="node" cooperation="1"><group id="5" name="部门经理"/><transition name="提交项目资产管理员审批" destination="35" layout="530,94 653,95" id="38" direction="go"></transition></node><node id="35" name="项目资产管理员" layout="653 131 75 7" category="node" cooperation="0"><group id="4" name="项目资产管理员"/><transition name="提交超级管理员审批" destination="36" layout="743,115 744,199" id="39" direction="go"></transition><actor id="1" name="chengderen"/><actor id="4" name="wangwu"/><actor id="8" name="zhongsan"/></node><node id="36" name="超级管理员" layout="654 106 199 8" category="node" cooperation="0"><group id="1" name="超级管理员"/><transition name="结束" destination="33" layout="654,219 511,221" id="40" direction="go"></transition></node></workflow>', N'7F1CC595-5BB1-4144-A72F-C509D4ACC3FB', N'请假流程', 0, N'新库。', CAST(N'2020-02-01T15:42:59.130' AS DateTime))
GO
INSERT [dbo].[t_structure] ([NID], [StructName], [StructXml], [CateCode], [CateName], [Status], [Memo], [CreateDateTime]) VALUES (N'6003237F-DB38-4EB0-96B5-5EE0AE060829', N'请假流程（加入自动判断节点）', N'<workflow mode="Mix"><start id="32" name="开始" layout="346 30 82 -7" category="start"><transition name="提交研发部小组长审批" destination="34" layout="376,112 377,151" id="40" direction="go"></transition></start><end id="33" name="结束" layout="348 37 457 -6" category="end"></end><node id="34" name="研发部小组长" layout="287 112 151 19" category="node"><group id="2" name="小组长"/><transition name="提交部门助理审批" destination="35" layout="377,191 377,230" id="41" direction="go"></transition><transition name="原路回退" destination="32" layout="376,112 377,151" id="40" direction="back"></transition></node><node id="35" name="部门助理" layout="287 105 230 17" category="node"><group id="5" name="部门助理"/><transition name="依据请假天自动判定" destination="37" layout="377,270 377,305" id="42" direction="go"></transition><transition name="原路回退" destination="34" layout="377,191 377,230" id="41" direction="back"></transition></node><node id="36" name="部门经理" layout="114 129 366 13" category="node"><group id="4" name="部门经理"/><transition name="结束" destination="33" layout="204,406 203,460 348,457" id="43" direction="go"><marker x="198" y="455" length="67"/></transition><transition name="原路回退" destination="37" layout="327,330 204,330 204,366" id="44" direction="back"><marker x="199" y="325" length="46"/></transition></node><decision id="37" name="分支节点" layout="327 56 330 -10" category="decision"><command><text><![CDATA[select * from [dbo].[t_vacation] where instanceID=@InstanceID]]></text><id><![CDATA[1]]></id></command><transition name="提交部门经理审批" destination="36" layout="327,330 204,330 204,366" id="44" direction="go"><expression><![CDATA[DAY>10]]></expression><marker x="199" y="325" length="46"/></transition><transition name="结束" destination="33" layout="377,355 378,427" id="45" direction="go"><expression><![CDATA[DAY<10]]></expression></transition><transition name="原路回退" destination="35" layout="377,270 377,305" id="42" direction="back"><expression><![CDATA[]]></expression></transition></decision></workflow>', N'7F1CC595-5BB1-4144-A72F-C509D4ACC3FB', N'请假流程', 0, N'加入自动判断节点，即按照设定的条件自行流转。', NULL)
GO
INSERT [dbo].[t_structure] ([NID], [StructName], [StructXml], [CateCode], [CateName], [Status], [Memo], [CreateDateTime]) VALUES (N'9D24166D-8CCD-4968-A541-91CB3A4A698F', N'按线导航测试', N'<workflow mode="Transition"><start id="32" name="开始" layout="122 5 82 1" category="start"><transition name="提交研发部小组长审批" destination="35" layout="182,82 233,82" id="40" direction="go"></transition></start><end id="33" name="结束" layout="571 17 306 0" category="end"></end><node id="34" name="部门助理" layout="510 124 63 8" category="node"><group id="5" name="部门助理"/><transition name="提交部门经理审批" destination="36" layout="690,83 787,84" id="41" direction="go"></transition></node><node id="35" name="研发部小组长" layout="233 133 62 8" category="node"><group id="2" name="小组长"/><transition name="提部部门助理审批" destination="34" layout="413,82 510,83" id="42" direction="go"></transition></node><node id="36" name="部门经理" layout="787 46 64 19" category="node"><group id="4" name="部门经理"/><transition name="提交总经理助理审批" destination="37" layout="877,104 878,169" id="43" direction="go"></transition></node><node id="37" name="总经理助理" layout="788 106 169 2" category="node"><group id="6" name="总经理秘书"/><transition name="提交总经理审批" destination="38" layout="788,189 690,189" id="44" direction="go"></transition></node><node id="38" name="总经理" layout="510 102 169 16" category="node"><group id="3" name="总经理"/><transition name="结束" destination="33" layout="600,209 601,276" id="45" direction="go"></transition><transition name="打回部门助理审批" destination="34" layout="600,169 600,103" id="46" direction="go"></transition><transition name="打回研发小组长审批" destination="35" layout="510,189 322,188 323,102" id="47" direction="go"><marker x="317" y="183" length="106"/></transition></node></workflow>', N'7F1CC595-5BB1-4144-A72F-C509D4ACC3FB', N'请假流程', 0, N'按线流转。', NULL)
GO
INSERT [dbo].[t_structure] ([NID], [StructName], [StructXml], [CateCode], [CateName], [Status], [Memo], [CreateDateTime]) VALUES (N'E081D8FB-6D3D-4521-BD3B-29B89FBE7896', N'请假流程1.7', N'<workflow mode="Mix"><start id="32" name="开始" layout="337 20 117 19" category="start" cooperation="0"><transition name="提交研发部小组长审批" destination="34" layout="367,147 366,202" id="36" direction="go"></transition></start><end id="33" name="结束" layout="335 11 474 -7" category="end" cooperation="0"><action id="Smartflow.Bussiness.WorkflowService.TestAction" name="TestAction"/></end><node id="34" name="研发部小组长" layout="276 132 202 5" category="node" cooperation="0"><group id="2" name="小组长"/><transition name="提交部门经理审批" destination="35" layout="366,242 364,301" id="37" direction="go"></transition><transition name="原路回退" destination="32" layout="367,147 366,202" id="36" direction="back"></transition></node><node id="35" name="部门经理" layout="274 79 301 6" category="node" cooperation="0"><transition name="结束" destination="33" layout="364,341 365,444" id="38" direction="go"></transition><transition name="原路回退" destination="34" layout="366,242 364,301" id="37" direction="back"></transition><actor id="1" name="chengderen"/><actor id="4" name="zhangsan"/></node></workflow>', N'7F1CC595-5BB1-4144-A72F-C509D4ACC3FB', N'请假流程', 1, N'普通员工的请假流程单。', NULL)
GO
