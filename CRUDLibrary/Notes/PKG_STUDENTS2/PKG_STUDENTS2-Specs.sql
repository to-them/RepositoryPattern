CREATE OR REPLACE 
PACKAGE PKG_STUDENTS2 AS 

    TYPE t_cursor IS REF CURSOR;
    
    -- insert one 
    PROCEDURE spCreateStudent(
    stud_name IN STUDENTS.NAME%TYPE, 
    stud_email IN STUDENTS.EMAIL%TYPE, 
    stud_mobile IN STUDENTS.MOBILE%TYPE, 
    stud_gender IN STUDENTS.GENDER%TYPE, 
    stud_dob IN STUDENTS.DATEOFBIRTH%TYPE, 
    o_stud_id OUT NUMBER
    );
    
    -- select all
    PROCEDURE spGetStudents(
    o_students out t_cursor
    );
    
    -- select one by id
    PROCEDURE spGetStudent(
    stud_id IN STUDENTS.ID%TYPE, 
    o_student OUT t_cursor
    );
    
    -- update
    PROCEDURE spUpdateStudent(
    stud_id IN STUDENTS.ID%TYPE, 
    stud_name IN STUDENTS.NAME%TYPE, 
    stud_email IN STUDENTS.EMAIL%TYPE, 
    stud_mobile IN STUDENTS.MOBILE%TYPE, 
    stud_gender IN STUDENTS.GENDER%TYPE, 
    stud_dob IN STUDENTS.DATEOFBIRTH%TYPE
    );
    
    -- delete
    PROCEDURE spDeleteStudent(
    stud_id IN STUDENTS.ID%TYPE
    ); 

END PKG_STUDENTS2;