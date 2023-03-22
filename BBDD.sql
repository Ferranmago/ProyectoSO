DROP DATABASE IF EXISTS Campeonato;
CREATE DATABASE Campeonato;
USE Campeonato;
CREATE TABLE Jugador (
	clave INTEGER PRIMARY KEY NOT NULL, 
	nombre TEXT NOT NULL
	) ENGINE = InnoDB;
CREATE TABLE Partida (
	identificador INTEGER PRIMARY KEY NOT NULL,
	ciudad TEXT NOT NULL
	) ENGINE = InnoDB;

CREATE TABLE Participacion (
	Jugador INTEGER NOT NULL,
	Partida INTEGER NOT NULL,
	Posicion INTEGER NOT NULL,
	FOREIGN KEY (Jugador) REFERENCES Jugador(clave),
	FOREIGN KEY (Partida) REFERENCES Partida(identificador)
	) ENGINE = InnoDB;

INSERT INTO Jugador VALUES(1,'Juan');
INSERT INTO Jugador VALUES(2,'Maria');
INSERT INTO Jugador VALUES(3,'Pedro');
INSERT INTO Jugador VALUES(4,'Luis');
INSERT INTO Jugador VALUES(5,'Julia');
INSERT INTO Partida VALUES(1,'Barcelona');
INSERT INTO Partida VALUES(2,'Madrid');
INSERT INTO Partida VALUES(3,'Sevilla');
INSERT INTO Participacion VALUES(1,1,1);
INSERT INTO Participacion VALUES(1,2,2);
INSERT INTO Participacion VALUES(1,3,2);
INSERT INTO Participacion VALUES(2,2,1);
INSERT INTO Participacion VALUES(2,3,4);
INSERT INTO Participacion VALUES(3,1,2);
INSERT INTO Participacion VALUES(4,1,4);
INSERT INTO Participacion VALUES(4,2,4);
INSERT INTO Participacion VALUES(4,3,1);
INSERT INTO Participacion VALUES(5,1,3);
INSERT INTO Participacion VALUES(5,2,3);

SELECT Partida.ciudad, Partida.identificador FROM Jugador, Partida, Participacion WHERE 
	Jugador.nombre = 'Juan' AND 
	Partida.identificador = Participacion.Partida AND
	Jugador.clave = Participacion.Jugador AND
	(Participacion.Posicion = 1 OR Participacion.Posicion = 2);
SELECT Participacion.Partida FROM Jugador, Participacion, Partida WHERE 
	Jugador.nombre = 'Juan' AND
	Participacion.Jugador = Jugador.clave AND 
	Participacion.Partida IN (SELECT Participacion.Partida FROM Jugador, Participacion WHERE 
	Jugador.nombre = 'Maria' AND Participacion.Jugador = Jugador.clave); 
	

SELECT Jugador.nombre FROM Jugador, Partida, Participacion WHERE
	Partida.ciudad = 'ciudad1' AND
	Partida.identificador = Participacion.Partida;

