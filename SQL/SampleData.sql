
INSERT INTO Blogs (BlogName, AuthorName) VALUES
('Tech Trails', 'Alex Byte'),
('Foodie Corners', 'Casey Chef'),
('Travel Snapshots', 'Riley Roads'),
('Quiet Reading Room', 'Morgan Page');


INSERT INTO Tags (TagName) VALUES
('CSharp'),
('ASP.NET'),
('MVC'),
('SQL'),
('Recipes'),
('Dessert'),
('Travel'),
('Photography'),
('Review');



INSERT INTO Posts (Title, Body, DatePosted, BlogID, Tags) VALUES

('Getting started with ASP.NET Core',
 'An introduction to building your first ASP.NET Core web app.',
 '2025-12-01 09:00:00', 1,
 'CSharp,ASP.NET,MVC'),

('Working with EF Core and SQL',
 'Basic CRUD operations using Entity Framework Core and SQL Server.',
 '2025-12-02 10:30:00', 1,
 'CSharp,SQL'),

('Building a simple blog',
 'Using MVC to build a simple blog application.',
 '2025-12-03 14:15:00', 1,
 'ASP.NET,MVC,SQL'),


('Easy weeknight pasta',
 'A quick pasta recipe you can make in under 30 minutes.',
 '2025-12-04 18:00:00', 2,
 'Recipes,Dessert'),

('Baking chocolate chip cookies',
 'Soft and chewy chocolate chip cookies for any occasion.',
 '2025-12-05 16:45:00', 2,
 'Recipes,Dessert'),


('Weekend in Montreal',
 'Photos and notes from a short city break in Montreal.',
 '2025-12-06 11:20:00', 3,
 'Travel,Photography,Review');


INSERT INTO PostTags (PostID, TagID) VALUES

(1, 1),  -- CSharp
(1, 2),  -- ASP.NET
(1, 3),  -- MVC

(2, 1),  -- CSharp
(2, 4),  -- SQL

(3, 2),  -- ASP.NET
(3, 3),  -- MVC
(3, 4),  -- SQL

(4, 5),  -- Recipes
(4, 6),  -- Dessert

(5, 5),  -- Recipes
(5, 6),  -- Dessert

(6, 7),  -- Travel
(6, 8),  -- Photography
(6, 9);  -- Review
