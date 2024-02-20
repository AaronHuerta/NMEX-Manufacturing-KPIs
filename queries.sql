--Queries plant
INSERT INTO Plant (Plant_description, Active)
VALUES ('A1', '1'), ('A2', '1'), ('PWT', '1'), ('C1', '1'), ('C2', '1'), ('NMEX', '1')

INSERT INTO Plant (Plant_description, Active)
VALUES ('EJEMPLO1', '1'), ('EJEMPLO2', '1')

DELETE Plant
WHERE Plant_id = 8;

SELECT*
FROM Plant


--Queries Location
INSERT INTO Location(Location_description, Plant_id, Active)
VALUES ('Trim B&K', 1, 1), ('Estampado rollos', 1, 1), ('Pintura', 2, 1)

SELECT Location_id, Location_description, L.Plant_id, L.Active
FROM Location AS L inner join Plant AS P 
ON L.Plant_id = P.Plant_id 
WHERE L.Active=1

SELECT Location_id, Location_description, L.Plant_id, L.Active, P.Plant_description AS Plant
FROM Location AS L inner join Plant AS P 
ON L.Plant_id = P.Plant_id 
WHERE L.Active=1 and Location_id = 4 and L.Plant_id = 1

SELECT*
FROM Location


UPDATE Location
SET Location_description= 'Ensamble YD XD', Plant_id='3',Active='1'
WHERE Location_id = 21


DELETE Location
WHERE Location_id <11 and Location_id >6



--Queries Models

SELECT Model_id, Model_description, Active
FROM Model
Where Active = 1



SELECT Model_id, Model_description, Active
FROM Model
Where Active = 1


INSERT INTO Model (Model_description, Active)
VALUES ('HP PROBOOK', 1)

UPDATE Model
SET Model_description = 'Prueba', Active = '1'
WHERE Model_id = 1;