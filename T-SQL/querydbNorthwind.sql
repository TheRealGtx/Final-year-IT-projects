--Manzi Giuliano 5i 18/03/2024, some sql queries on Microsoft's Northwind db.
USE Northwind

PRINT '1) Della tabella Products mostrare ProductNames e UnitPrice, per tutti i record che hanno almeno 5 unit� nello stock'
PRINT ' '

 SELECT ProductName, UnitPrice FROM Products
 WHERE UnitsInStock >= 5;

 GO

PRINT '2) Della tabella Categories mostrare tutti i campi di tutti i record in cui CategoryName ha il secondo carattere uguale a ___ per stringhe di qualunque lunghezza, ordinando in modo decrescente
per chiave primaria'
PRINT ' '

 SELECT CategoryID, CategoryName FROM Categories
 WHERE CategoryName LIKE '_o%'	
 GROUP BY CategoryID, CategoryName;

 GO

PRINT '3) Della tabella Products mostrare tutti i campi dei record per i quali UnitsInStock � compreso tra ___ e ___ estremi compresi e poi in una seconda SELECT con estremi non compresi. In entrambe i casi
solo i record per i quali il campo ______ � NULL. In una terza SELECT solo i record con valori del campo <=, > dei due precedenti estremi.'
PRINT ' '

 SELECT * FROM Products
 WHERE UnitsInStock >= 32 AND UnitsInStock <= 60;

 SELECT * FROM Products
 WHERE UnitsInStock BETWEEN 32 AND 60;

 SELECT * FROM Products
 WHERE UnitsInStock <= 32 OR UnitsInStock > 60;

 GO

PRINT '4) Scrivere una query per la quale abbia senso usare DISTINCT.'
PRINT ' '

 SELECT DISTINCT ProductName
 FROM Products;

 GO

PRINT '5) Della tabella Products mostrare ProductNames e il campo calcolato dal prodotto tra UnitPrice e UnitsInStock assegnandogli un nome adeguato.'
PRINT ' '

 SELECT ProductName, (UnitPrice * UnitsInStock) AS Coefficiente
 FROM Products;

 GO

PRINT '6) Andate a vedere il contenuto della tabella Categories per capire quali sono i CategoryName disponibili. Poi scrivete una SELECT che metta in JOIN Products e Categories per mostrare i record
risultanti che hanno CategoryName uguale a uno dei 4 dei possibili valori.'
PRINT ' '

 SELECT ProductID, ProductName, CategoryName
 FROM Products
 INNER JOIN Categories
 ON Products.CategoryID = Categories.CategoryID;

 GO

PRINT '7) Della tabella Products mostrare ProductNames e il campo calcolato dell UnitPrice incrementato del 23,45% e assegnandogli un nome adeguato.'
PRINT ' '

 SELECT ProductName, ((23.45/UnitPrice)*100) AS NuovoPrice
 FROM Products;

 GO

PRINT '8) Della tabella Products ricavare tutti i record che hanno Discontinued falso (il tipo � BIT naturalmente) e salvarli nella nuova tabella di nome ProductsB. Poi, con altra istruzione,
aggiungete alla tabella ora gi� esistente ProductsB, sempre dalla tabella Products, i record per i quali UnitPrice � un numero pari.'
PRINT ' '

 SELECT *
 INTO ProductsB
 FROM Products
 WHERE Discontinued = 0;

 INSERT INTO ProductsB (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
 SELECT ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued FROM Products
 WHERE (UnitPrice % 2) = 0;

 GO

PRINT '9) Eseguire il JOIN tra le tre tabelle Products, Oder Details (realizza lassociazione N:N) e Order mostrando tutti i campi tranne le PK e le FK.'
PRINT ' '

 SELECT [ProductName], [SupplierID], [QuantityPerUnit], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [Quantity], [Discount], [CustomerID], [EmployeeID], [OrderDate], [RequiredDate], [ShippedDate], [ShipVia], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]
 FROM Products INNER JOIN [Order Details]
 ON Products.ProductID = [Order Details].ProductID
 INNER JOIN Orders
 ON Orders.OrderID = [Order Details].OrderID

 GO

PRINT '10) Mostrare un po� di esempi con calcoli con date e con l�uso di CAST.'
PRINT ' '

 SELECT DATEDIFF(day, CAST('15/08/2023' AS datetime), CAST('20/08/2023' AS datetime)) AS Giorni

 SELECT DATEADD(SECOND, 10, CAST('10:24:30' AS TIME)) AS TEMPO;

 GO

PRINT '11) Ricavare i record di Suppliers che non hanno record collegati in Products. Se tale situazione non si verifica per nessun record cercate dove applicare la stessa richiesta in un altra coppia
di tabelle.'
PRINT ' '

IF EXISTS (SELECT *
		   FROM Suppliers
		   WHERE SupplierID NOT IN (SELECT SupplierID
									FROM Products))
	SELECT *
	FROM Suppliers
	WHERE SupplierID NOT IN (SELECT SupplierID
							 FROM Products)
ELSE
	SELECT *
	FROM Customers
	WHERE CustomerID NOT IN (SELECT CustomerID
							 FROM Orders);

GO

PRINT '12) Con le stesse tabelle che avete scelto per la 11) fate anche un FULL JOIN e un CROSS JOIN ed inserite in un commento su cosa notate.'
PRINT ' '

SELECT * FROM
Suppliers AS s FULL JOIN Products AS p
ON s.SupplierID = p.SupplierID;

SELECT * FROM
Suppliers AS s CROSS JOIN Products AS p

GO

--Il cross join combina tutti i campi delle tabelle, il full join si comporta come un inner join ma quando la condizione nell'ON non viene soddisfatta inserisce NULL

PRINT '13) Scrivete un adeguata self join per la tabella Employees.'
PRINT ' '

SELECT e1.LastName, e2.LastName AS Referente
FROM Employees AS e1 INNER JOIN Employees AS e2
ON e1.ReportsTo = e2.EmployeeID;

GO

PRINT '14) Nella tabella Products calcolate valore massimo, minimo e medio di UnitsInStock. Dare nomi con spazi ai campi.'
PRINT ' '

SELECT MAX(UnitsInStock) AS [Massimo di unit�], MIN(UnitsInStock) AS [Minimo di unit�], AVG(UnitsInStock) AS [Quantit� media di unit�]
FROM Products;

GO

PRINT '15) Per la 14) calcolate le stesse cose per gruppi, ogni gruppo deve essere lo stesso Suppliers.'
PRINT ' '

SELECT SupplierID AS Fornitore, MAX(UnitsInStock) AS [Massimo di unit�], MIN(UnitsInStock) AS [Minimo di unit�], AVG(UnitsInStock) AS [Quantit� media di unit�]
FROM Products
GROUP BY SupplierID;

GO

PRINT '16) Come la 15) ma mostrando solo i gruppi formati da almeno 3 record (verificare sui dati del DB se 3 � adeguato ai dati presenti, se non lo � cambiate opportunamente tale valore � ecc.).'
PRINT ' '

SELECT SupplierID AS Fornitore, MAX(UnitsInStock) AS [Massimo di unit�], MIN(UnitsInStock) AS [Minimo di unit�], AVG(UnitsInStock) AS [Quantit� media di unit�]
FROM Products
GROUP BY SupplierID
HAVING COUNT(*) > 3;

GO

PRINT '17) Sempre nella tabella Products mostrare i record che hanno il valore di UnitsInStock maggiore della sua media + 1.'
PRINT ' '

SELECT *
FROM Products
WHERE UnitsInStock > (SELECT AVG(UnitsInStock)
						FROM Products) + 1;

GO

PRINT '18) Sempre nella tabella Products mostrare i record per i quali in Suppliers il campo HomePage non � NULL. Da fare per forza con una sub-query.'
PRINT ' '

SELECT *
FROM Products
WHERE SupplierID IN (SELECT SupplierID
					FROM Suppliers
					WHERE HomePage IS NOT NULL)

GO

PRINT '19) Trovate il modo di usare in modo saggio ALL, EXISTS e ANY per scrivere tre query.'
PRINT ' '

SELECT *
FROM Customers
WHERE City = ANY(SELECT City
				FROM Suppliers)
ORDER BY City;
--Seleziona i clienti che sono nella stessa citt� di un fornitore

SELECT *
FROM Orders
WHERE ShipCountry <> ALL(SELECT Country
						FROM Employees);
--Seleziona tutti gli ordini che saranno spediti in un paese in cui non ci sono dipedenti

SELECT *
FROM Products as P
WHERE EXISTS (SELECT *
			FROM [Order Details]
			WHERE ProductID = p.ProductID);
--Seleziona tutti i prodotti per i quali esiste un ordine

GO

PRINT '20) Salvate le informazioni prodotte da 19) in tre nuove tabelle.'
PRINT ' '

SELECT *
INTO TabAny
FROM Customers
WHERE City = ANY(SELECT City
				FROM Suppliers)
ORDER BY City;
----Seleziona i clienti che sono nella stessa citt� di un fornitore

SELECT *
INTO TabAll
FROM Orders
WHERE ShipCountry <> ALL(SELECT Country
						FROM Employees);
----Seleziona tutti gli ordini che saranno spediti in un paese in cui non ci sono dipedenti

SELECT *
INTO TabExists
FROM Products as P
WHERE EXISTS (SELECT *
			FROM [Order Details]
			WHERE ProductID = p.ProductID);
----Seleziona tutti i prodotti per i quali esiste un ordine

GO

PRINT '21) Pensate a due SELECT che selezionino due diversi record da una stessa tabella scelta da voi, poi usatele con UNION, EXCEPT ed INTERSECT.'
PRINT ' '

SELECT Phone
FROM Customers
UNION
SELECT Fax
FROM Customers;

SELECT Phone
FROM Customers
EXCEPT
SELECT Fax
FROM Customers;

SELECT Phone
FROM Customers
INTERSECT
SELECT Fax
FROM Customers;

GO

PRINT '22) Mettete la UNION della 21) in una vista e mostratene l uso di base.'
PRINT ' '
GO

CREATE VIEW Contatti AS
SELECT Phone
FROM Customers
UNION
SELECT Fax
FROM Customers;

GO

SELECT * FROM Contatti;

--La vista esegue la query che gli abbiamo passato e ogni volta che selezioniamo qualcosa dlla vista essa eseguir� la query definita in fase di creazione per poi restiruirci il risultato richiesto