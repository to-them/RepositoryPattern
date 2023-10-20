use dbtest;
DELIMITER //

CREATE PROCEDURE spUpdateStudent(
	IN _Name VARCHAR(100),
    IN _Email varchar(50),
    IN _Mobile varchar(50),
    IN _Gender varchar(20),
    IN _DateOfBirth datetime,
    IN _Id int(11)
	-- IN countryName VARCHAR(255)
)
BEGIN
	update students set Name=_Name,Email=_Email,Mobile=_Mobile,Gender=_Gender,DateOfBirth=_DateOfBirth
    where Id=_Id;
	-- SELECT * FROM offices WHERE country = countryName;
END //

DELIMITER ;