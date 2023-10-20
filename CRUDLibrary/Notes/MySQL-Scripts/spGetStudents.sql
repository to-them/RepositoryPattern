use dbtest;
DELIMITER //

CREATE PROCEDURE spGetStudents()
BEGIN
	SELECT *  FROM students;
END //

DELIMITER ;
