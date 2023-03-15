DROP DATABASE IF EXISTS Juego;
CREATE DATABASE Juego;
USE Juego;

CREATE TABLE Jugador (
	Username TEXT NOT NULL,
	Password TEXT NOT NULL
	ID INTEGER PRIMARY KEY NOT NULL
) ENGINE = InnoDB;

CREATE TABLE Partida (
	Identificador INTEGER PRIMARY KEY NOT NULL,
	Fecha_Y_Hora DATETIME NOT NULL, 
	Duración INT NOT NULL,
	Ganador TEXT NOT NULL
)ENGINE = InnoDB;


INSERT INTO Jugador VALUES ('Ferran' , 'password1' , 123);
INSERT INTO Jugador VALUES ('Miguel' , 'password2' , 234);
INSERT INTO Jugador VALUES ('Pol' , 'password3' , 345);

INSERT INTO Partida VALUES (1, '2002-01-22 03:50:16' , 60 , 'Galder');
INSERT INTO Partida VALUES (2, '2005-03-16 18:23:01' , 120 , 'Alex');




