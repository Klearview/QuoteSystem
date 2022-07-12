/*** Admin Defualt Users ***/
DELETE FROM [dbo].[AspNetUsers] WHERE UserName = 'admin@klearview.ca';
DELETE FROM [dbo].[AspNetUsers] WHERE UserName = '1tomkinsnoa.per@gmail.com';
DELETE FROM [dbo].[AspNetUsers] WHERE UserName = '1tomkinsnoa@gmail.com';

INSERT INTO [dbo].[AspNetUsers] (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES
('15f1b8a1-9648-42f9-99dc-c75b8ef4ec85', 'admin@klearview.ca', 'ADMIN@KLEARVIEW.CA', 'admin@klearview.ca', 'ADMIN@KLEARVIEW.CA', 1, 'AQAAAAEAACcQAAAAECoVEomjueHOQdRB175BKOgyp/ec3GxNRP5EGB34KIvwSTyELq1bjn03jbuPhDkJ/Q==', '4Y2HQVKCGN2G5QPE2K4GETDIXABPQDNR', '5a63beae-f2b7-4ae8-ace6-ea0f24507c8d', NULL, 0, 0, NULL, 1, 0),
('48f3fac2-25ca-4129-a290-00c5682cde93', '1tomkinsnoa.per@gmail.com', '1TOMKINSNOA.PER@GMAIL.COM', '1tomkinsnoa.per@gmail.com', '1TOMKINSNOA.PER@GMAIL.COM', 1, 'AQAAAAEAACcQAAAAECHQ22+xJn9oM42cid+EdZVFZKUSu+H1EDckiA51cMWQM/8gffg7t8VUDUYfFHzKFA==', 'X3NUFWT6ZBDCR3Y437CUVHM7DFGKODMU', 'cd5632fa-4786-4140-94bf-f4c2d927bb31', NULL, 0, 0, NULL, 1, 0),
('7a5adf7c-7aaa-4703-9005-61caa7d13503', '1tomkinsnoa@gmail.com', '1TOMKINSNOA@GMAIL.COM', '1tomkinsnoa@gmail.com', '1TOMKINSNOA@GMAIL.COM', 1, 'AQAAAAEAACcQAAAAEIXG/DY7MFfL2DjoGu7XIh2qwXnwa+R+bzsKydQdbATLR7O0WWJnNXscLNorWmCUnw==', '3LXBJSRFFTKVONMBI7TNYF7446ESAT7B', 'b3d9555e-26b2-4e2b-adb3-206a1bd3b27c', NULL, 0, 0, NULL, 1, 0);
GO


/*** Roles ***/
DELETE FROM [dbo].[AspNetRoles] WHERE Name = 'Test';
DELETE FROM [dbo].[AspNetRoles] WHERE Name = 'AlwaysAdmin';
DELETE FROM [dbo].[AspNetRoles] WHERE Name = 'QuoteEditor';
DELETE FROM [dbo].[AspNetRoles] WHERE Name = 'Admin';

INSERT INTO [dbo].[AspNetRoles] (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES
('22ca75e5-5d29-4fd2-a836-fb0b2f53b16a', 'Test', 'TEST', '7b815d18-0e20-4ec9-891d-146506fcb938'),
('95f40c7f-7d95-4912-a8b7-d1ddcc392c66', 'AlwaysAdmin', 'ALWAYSADMIN', '5969d1c4-f0eb-479d-8ea1-6aa2612973fc'),
('aa639804-30a0-4bf6-b80a-1dfd06e2911d', 'QuoteEditor', 'QUOTEEDITOR', '915db4f8-fc82-4263-9925-02dffbd904cf'),
('da4a3ef8-9000-4a8c-8164-3d4ca43c02f4', 'Admin', 'ADMIN', 'c7b13b9e-4af3-4b23-ba0c-1eb898b6c28b');
GO

/*** User Roles ***/
DELETE FROM [dbo].[AspNetUserRoles];

INSERT INTO [dbo].[AspNetUserRoles] (UserId, RoleId)
VALUES
('48f3fac2-25ca-4129-a290-00c5682cde93', '22ca75e5-5d29-4fd2-a836-fb0b2f53b16a'), -- (1tomkinsnoa.per@gmail.com, Test)
('7a5adf7c-7aaa-4703-9005-61caa7d13503', '95f40c7f-7d95-4912-a8b7-d1ddcc392c66'), -- (1tomkinsnoa@gmail.com, AlwaysAdmin)
('15f1b8a1-9648-42f9-99dc-c75b8ef4ec85', '95f40c7f-7d95-4912-a8b7-d1ddcc392c66'), -- (admin@klearview.ca, AlwaysAdmin)
('15f1b8a1-9648-42f9-99dc-c75b8ef4ec85', 'aa639804-30a0-4bf6-b80a-1dfd06e2911d'), -- (admin@klearview.ca, QuoteEditor)
('48f3fac2-25ca-4129-a290-00c5682cde93', 'aa639804-30a0-4bf6-b80a-1dfd06e2911d'), -- (1tomkinsnoa.per@gmail.com, QuoteEditor)
('7a5adf7c-7aaa-4703-9005-61caa7d13503', 'aa639804-30a0-4bf6-b80a-1dfd06e2911d'), -- (1tomkinsnoa@gmail.com, QuoteEditor)
('15f1b8a1-9648-42f9-99dc-c75b8ef4ec85', 'da4a3ef8-9000-4a8c-8164-3d4ca43c02f4'), -- (admin@klearview.ca, Admin)
('7a5adf7c-7aaa-4703-9005-61caa7d13503', 'da4a3ef8-9000-4a8c-8164-3d4ca43c02f4'); -- (1tomkinsnoa@gmail.com, Admin)
GO