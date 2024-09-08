CREATE Database NimbusLabs
GO

USE NimbusLabs
GO

CREATE Table Alumno
(
id int PRIMARY KEY IDENTITY,
nombre varchar(150) NOT NULL,
apellidos varchar(150) NOT NULL,
edad int NOT NULL
)
GO

INSERT INTO Alumno VALUES
('Eduardo', 'Trejo Sanjuan', 32),
('Abigail', 'Riversa Salvador', 32),
('Francisco', 'Paulino Rello', 30)
GO

SELECT * FROM Alumno
GO

CREATE Table Materia
(
id int PRIMARY KEY IDENTITY,
nombre varchar(150) NOT NULL,
creditos int
)
GO

INSERT INTO Materia VALUES
('Matemáticas', 30),
('Español', 20),
('Física', 40),
('Química', 40)
GO

SELECT * FROM Materia
GO

CREATE Table Materias_Alumno
(
id int PRIMARY KEY IDENTITY,
idAlumno int,
idMateria int
CONSTRAINT FK_MateriasAlumno_Alumno FOREIGN KEY (idAlumno) REFERENCES Alumno(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT FK_MateriasAlumno_Materia FOREIGN KEY (idMateria) REFERENCES Materia(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE
)
GO

INSERT INTO Materias_Alumno VALUES
(1,2),
(1,3),
(2,1),
(2,4),
(3,2),
(3,4)
GO

SELECT * FROM Materias_Alumno
GO

DROP TABLE Alumno
DROP TABLE Materia
DROP TABLE Materias_Alumno
GO

SELECT Alumno.Id, Alumno.Nombre, Alumno.Apellidos, Alumno.Edad, SUM(Materia.Creditos)
FROM Materias_Alumno
INNER JOIN Alumno ON Materias_Alumno.IdAlumno = Alumno.Id
INNER JOIN Materia ON Materias_Alumno.IdMateria = Materia.Id
GROUP BY Alumno.Id, Alumno.Nombre, Alumno.Apellidos, Alumno.Edad
GO

SELECT 
FROM Materias_Alumno
INNER JOIN Alumno ON Materias_Alumno.IdAlumno = Alumno.Id
INNER JOIN Materia ON Materias_Alumno.IdMateria = Materia.Id