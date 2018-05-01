USE $(dbname) 
GO

INSERT INTO Reason(Code, Description)
SELECT 'A', 'Carrier'
WHERE NOT EXISTS (select * from Reason where Code = 'A')

INSERT INTO Reason(Code, Description)
SELECT 'B', 'Weather'
WHERE NOT EXISTS (select * from Reason where Code = 'B')

INSERT INTO Reason(Code, Description)
SELECT 'C', 'National Air System'
WHERE NOT EXISTS (select * from Reason where Code = 'C')

INSERT INTO Reason(Code, Description)
SELECT 'D', 'Security'
WHERE NOT EXISTS (select * from Reason where Code = 'D')

INSERT INTO Reason(Code, Description)
SELECT 'Z', 'Late Aircraft'
WHERE NOT EXISTS (select * from Reason where Code = 'Z')