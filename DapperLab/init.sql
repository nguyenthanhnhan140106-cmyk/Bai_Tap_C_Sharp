CREATE DATABASE StudentDb;
GO

USE SchoolDB;

CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
);

INSERT INTO Students (Name, Age) VALUES 
('Nguyễn Văn An', 21),
('Trần Thị Bình', 22),
('Lê Minh Châu', 20);