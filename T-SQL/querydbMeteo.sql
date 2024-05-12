--Manzi Giuliano 5i 23/02/2024, some queries on a weather station database.
USE meteo

--Query per verificare se un indirizzo IP dato (noto) � presente e associato ad una stazione meteo
SELECT * FROM Stazioni
WHERE IP_Statico = '192.168.1.1';

--Query per verificare se un ID di un sensore installato dato (noto) � presente
SELECT * FROM SensoriInstallati
WHERE idSensoriInstallati = 6;

--Query per verificare se un ID di un sensore installato dato (noto) appartiene ad una CAM oppure ad un sensore normale (No CAM)
SELECT Camera FROM Sensori
WHERE idCodiceSensore = (
SELECT idCodiceSensore FROM SensoriInstallati
WHERE idSensoriInstallati = 9);

--Query per ottenere il valore della chiave primaria di una stazione noto il valore della chiave primaria di un suo sensore installato
SELECT idNomeStazione FROM Stazioni
WHERE idNomeStazione = (
SELECT idNomeStazione FROM SensoriInstallati
WHERE idSensoriInstallati = 8);