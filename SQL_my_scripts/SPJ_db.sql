CREATE DATABASE [SPJ_db]
ON
( 
   NAME = 'SPJ_db', 
   FILENAME = 'C:\Users\MSSQLSERVER\DataBases\SPJ_db.mdf', 
   SIZE = 5120KB , MAXSIZE = 10240KB , FILEGROWTH = 1024KB 
)
LOG ON 
( 
   NAME = 'SPJ_db_log', 
   FILENAME = 'C:\Users\MSSQLSERVER\DataBases\SPJ_db.ldf', 
   SIZE = 1024KB , MAXSIZE = 10240KB , FILEGROWTH = 1024KB 
);

GO

USE SPJ_db;

GO

CREATE TABLE S (
   S# int NOT NULL,
   SNAME varchar(50) NOT NULL,
   STATUS int NOT NULL,
   CITY varchar(50) NOT NULL, 
   CONSTRAINT PK_S PRIMARY KEY(S#)
); 

CREATE TABLE P (
	P# int NOT NULL,
	PNAME varchar(50) NOT NULL,
	COLOR varchar(30) NOT NULL,
	WEIGHT NUMERIC(16, 4) NOT NULL,
	CITY varchar(50) NOT NULL,
	CONSTRAINT PK_P PRIMARY KEY(P#)
);

CREATE TABLE J (
	J# int NOT NULL,
	JNAME varchar(50) NOT NULL,
	CITY varchar(50) NOT NULL,
	CONSTRAINT PK_J PRIMARY KEY(J#)
);

CREATE TABLE SPJ (
	S# int NOT NULL,
	P# int NOT NULL,
	J# int NOT NULL,
	QTY int NOT NULL,
	CONSTRAINT PK_SPJ PRIMARY KEY(S#, P#, J#),
	CONSTRAINT FK_SPJ_S FOREIGN KEY(S#) REFERENCES S(S#),
	CONSTRAINT FK_SPJ_P FOREIGN KEY(P#) REFERENCES P(P#),
	CONSTRAINT FK_SPJ_J FOREIGN KEY(J#) REFERENCES J(J#)
);

GO

INSERT INTO S (S#, SNAME, STATUS, CITY)
VALUES (1, 'Smith', 20, 'London'),
	(2, 'Jones', 10, 'Paris'),
	(3, 'Blake', 30, 'Paris'),
	(4, 'Clark', 20, 'London'),
	(5, 'Adams', 30, 'Athens');

INSERT INTO P (P#, PNAME, COLOR, WEIGHT, CITY)
VALUES (1, 'Nut', 'Red', 12.0, 'London'),
	(2, 'Bolt', 'Green', 17.0, 'Paris'),
	(3, 'Screw', 'Blue', 17.0, 'Oslo'),
	(4, 'Screw', 'Red', 14.0, 'London'),
	(5, 'Cam', 'Blue', 12.0, 'Paris'),
	(6, 'Cog', 'Red', 19.0, 'London');

INSERT INTO J (J#, JNAME, CITY)
VALUES (1, 'Sorter', 'Paris'),
	(2, 'Display', 'Rome'),
	(3, 'OCR', 'Athens'),
	(4, 'Console', 'Athens'),
	(5, 'RAID', 'London'),
	(6, 'EDS', 'Oslo'),
	(7, 'Tape', 'London');

INSERT INTO SPJ (S#, P#, J#, QTY)
VALUES (1, 1, 1, 200),
	(1, 1, 4, 700),
	(2, 3, 1, 400),
	(2, 3, 2, 200),
	(2, 3, 3, 200),
	(2, 3, 4, 500),
	(2, 3, 5, 600),
	(2, 3, 6, 400),
	(2, 3, 7, 800),
	(2, 5, 2, 100),
	(3, 3, 1, 200),
	(3, 4, 2, 500),
	(4, 6, 3, 300),
	(4, 6, 7, 300),
	(5, 2, 2, 200),
	(5, 2, 4, 100),
	(5, 5, 5, 500),
	(5, 5, 7, 100),
	(5, 6, 2, 200),
	(5, 1, 4, 100),
	(5, 3, 4, 200),
	(5, 4, 4, 800),
	(5, 5, 4, 400),
	(5, 6, 4, 500);

GO