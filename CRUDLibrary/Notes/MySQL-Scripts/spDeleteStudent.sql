use dbtest;
-- 
DELIMITER //

CREATE PROCEDURE spDeleteStudent(
    IN _Id int(11)
	-- IN countryName VARCHAR(255)
)
BEGIN
	delete from students where Id=_Id;
	-- SELECT * FROM offices WHERE country = countryName;
END //

DELIMITER ;