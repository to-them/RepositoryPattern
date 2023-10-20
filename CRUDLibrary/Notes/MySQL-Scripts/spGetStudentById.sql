use dbtest;
-- 
DELIMITER //

CREATE PROCEDURE spGetStudentById(
    IN _Id int(11)
	-- IN countryName VARCHAR(255)
)
BEGIN
	select * from students where Id=_Id;
	-- SELECT * FROM offices WHERE country = countryName;
END //

DELIMITER ;