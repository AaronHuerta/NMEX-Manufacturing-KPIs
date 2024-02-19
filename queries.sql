--Queries plant
INSERT INTO Plant (Plant_description, Active)
VALUES ('A1', '1'), ('A2', '1'), ('PWT', '1'), ('C1', '1'), ('C2', '1'), ('NMEX', '1')

SELECT*
FROM Plant


--Queries Location
INSERT INTO Location(Location_description, Plant_id, Active)
VALUES ('Trim B&K', 1, 1), ('Estampado rollos', 1, 1), ('Pintura', 2, 1)

SELECT Location_id, Location_description, L.Plant_id, L.Active
FROM Location AS L inner join Plant AS P 
ON L.Plant_id = P.Plant_id 
WHERE L.Active=1

