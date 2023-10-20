CREATE OR REPLACE 
PACKAGE PKG_STUDENTS AS 

  TYPE t_cursor IS REF CURSOR;
    PROCEDURE spCreateStudent(stud_name VARCHAR2, stud_email VARCHAR2, stud_mobile VARCHAR2, stud_gender VARCHAR2, stud_dob DATE, o_stud_id OUT NUMBER);
    PROCEDURE spGetStudents(o_students out t_cursor);
    PROCEDURE spGetStudent(stud_id IN NUMBER, o_student OUT t_cursor);
    PROCEDURE spUpdateStudent(stud_id NUMBER, stud_name VARCHAR2, stud_email VARCHAR2, stud_mobile VARCHAR2, stud_gender VARCHAR2, stud_dob DATE);
    PROCEDURE spDeleteStudent(stud_id NUMBER); 

END PKG_STUDENTS;