use dbtest;
DELIMITER //

CREATE PROCEDURE spCreateStudent(
	IN _Name VARCHAR(100),
    IN _Email varchar(50),
    IN _Mobile varchar(50),
    IN _Gender varchar(20),
    IN _DateOfBirth datetime,
    OUT _Id int(11)
    -- IN countryName VARCHAR(255)
)
BEGIN
	insert INTO students(Name,Email,Mobile,Gender,DateOfBirth) 
    values(_Name,_Email,_Mobile,_Gender,_DateOfBirth);
    select Last_Insert_ID() into _Id;
	-- SELECT * FROM offices WHERE country = countryName;
END //

DELIMITER ;