--SQL script that creates a weather station's database
CREATE DATABASE meteo;
GO

USE meteo;
GO

CREATE TABLE Stazioni (
	idNomeStazione		VARCHAR(10) PRIMARY KEY NOT NULL,
	IP_Statico			VARCHAR(39),
	Localita_Indirizzo	VARCHAR(50),
	Latitudine			DECIMAL(8,6),
	Longitudine			DECIMAL(8,6),
	Altitudine			INT,
	Descrizione			VARCHAR(50),
	Note				VARCHAR(50)
);
GO

CREATE TABLE GrandezzaFisica (
	idGrandezzaFisica				INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	GrandezzaFisica					VARCHAR(20) NOT NULL,
	Simbolo							VARCHAR(6),
	SimboloUnitaDiMisuraAdottato	VARCHAR(6) NOT NULL, 
	Note							VARCHAR(45)
);
GO

CREATE TABLE Sensori (
	idCodiceSensore					INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	idGrandezzaFisica				INT NOT NULL,
	Camera							TINYINT,
	Nome							VARCHAR(30),
	Tipo							VARCHAR(30),
	Caratteristiche					VARCHAR(40),
	Note							VARCHAR(50),
	FOREIGN KEY (idGrandezzaFisica)	REFERENCES GrandezzaFisica(idGrandezzaFisica)
);
GO

CREATE TABLE SensoriInstallati (
	idSensoriInstallati		INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	idCodiceSensore			INT REFERENCES Sensori(idCodiceSensore) NOT NULL,
	idNomeStazione			VARCHAR(10) REFERENCES Stazioni(idNomeStazione) NOT NULL,
	Note					VARCHAR(50)
);
GO

CREATE TABLE Rilevamenti (
	idRilevamenti			INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	idSensoriInstallati		INT NOT NULL,
	DataOra					DATETIME2 NOT NULL,
	Dato					VARCHAR(15) NOT NULL
);
GO