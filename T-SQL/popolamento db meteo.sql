--SQL script that populates a weather station's database.
INSERT INTO GrandezzaFisica (GrandezzaFisica, Simbolo, SimboloUnitaDiMisuraAdottato, Note) VALUES
	('Temperatura', 'T', 'C°', ''),
    ('Pressione', '', 'hPa', ''),
    ('Umidità relativa', '', '%', ''),
    ('Direzione del vento', '', '°', ''),
    ('Velocità del vento', '', 'km/h', ''),
    ('Pioggia', '', 'mm', ''),
	('Camera', '', '.jpeg', '');
GO

INSERT INTO Sensori (idGrandezzaFisica, Camera, Nome, Tipo, Caratteristiche,Note) VALUES
	(1, 0, 'DHT22', 'Digitale', 'Temperatura/Umidità relativa', ''),
    (3, 0, 'DHT22', 'Digitale', 'Temperatura/Umidità relativa', ''),
    (1, 0, 'DHT11', 'Digitale', 'Temperatura/Umidità relativa', ''),
    (3, 0, 'DHT11', 'Digitale', 'Temperatura/Umidità relativa', ''),
    (1, 0, 'BPM180', 'Digitale', 'Temperatura/Pressione', ''),
    (2, 0, 'BPM180', 'Digitale', 'Temperatura/Pressione', ''),
    (6, 0, 'MISOL WH-SP-RG', 'Digitale', 'Risoluzione 0,3 mm di pioggia', ''),
    (4, 0, 'p/m 80422', 'Analogico', 'Banderuola 16 quadranti reisistivo', ''),
    (4, 0, 'Autocostruito NL', 'Digitale', 'Banderuola 16 quadranti codice Gray', ''),
    (5, 0, 'Autocostruito NL', 'Digitale', 'Anemometro a coppette', ''),
    (5, 0, 'WH-SP-WS01', 'Digitale', 'Anemometro a coppette', ''),
    (7, 1, 'PI Camera V2.1', 'Digitale', '8Mp (3280 x 2462pixel)', '');
GO

INSERT INTO Stazioni (idNomeStazione, IP_Statico, Localita_Indirizzo, Latitudine, Longitudine, Altitudine, Descrizione) VALUES 
	('Carpe0', '', 'Monte Carpegna passo del Trabocchino', 00.000000, 00.000000, 1330, 'Sul crinale sopra l eremo'),
    ('Pascal01', NULL, 'P.le Macrelli, 100 –47521 Cesena (FC)', 44.143387, 12.253328, NULL, 'Installata in cima al tetto lato superiore'),
    ('Perti01', NULL, 'Monte della Perticara', NULL, NULL, 830, 'Sulla vetta');
GO

INSERT INTO SensoriInstallati (idCodiceSensore, idNomeStazione) VALUES
	(1, 'Pascal01'),
    (2, 'Pascal01'),
    (6, 'Pascal01'),
    (8, 'Pascal01'),
    (10, 'Pascal01'),
    (11, 'Pascal01'),
    (12, 'Pascal01'),
    (8, 'Perti01'),
    (12, 'Perti01');
GO

INSERT INTO Rilevamenti (idSensoriInstallati, DataOra, Dato) VALUES
	(1, '2022-06-03 06:27:41', '10'),
    (1, '2020-07-28 03:34:54', '15'),
    (2, '2022-08-29 02:03:09', '15'),
    (2, '2020-09-25 17:12:49', '20'),
    (3, '2023-09-11 05:15:12', '10'),
    (3, '2020-09-29 09:38:06', '8'),
    (4, '2020-08-21 16:34:57', '1'),
    (4, '2022-07-31 07:09:54', '2'),
    (5, '2020-05-19 20:42:44', '310'),
    (5, '2023-10-16 05:00:23', '220'),
    (6, '2024-02-01 04:25:58', '2'),
    (6, '2022-10-30 14:58:15', '1'),
    (7, '2023-07-08 14:05:54', '7.jpeg'),
    (7, '2020-09-15 19:44:21', '7.jpeg'),
    (8, '2023-01-21 02:35:10', '0.5'),
    (8, '2022-09-21 10:48:47', '1.5'),
    (9, '2021-12-19 09:42:50', '0.5'),
    (9, '2022-04-08 22:20:29', '3'),
    (2, '2022-07-16 11:54:31', '5'),
    (5, '2020-08-21 10:09:03', '40');
GO