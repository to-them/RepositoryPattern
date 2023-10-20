CREATE OR REPLACE
PACKAGE BODY PKG_STUDENTS AS

  PROCEDURE spCreateStudent(stud_name VARCHAR2, stud_email VARCHAR2, stud_mobile VARCHAR2, stud_gender VARCHAR2, stud_dob DATE, o_stud_id OUT NUMBER) AS
  BEGIN
    INSERT INTO students(name, email, mobile, gender, dateofbirth) 
    VALUES(stud_name, stud_email, stud_mobile, stud_gender, stud_dob)
    RETURNING students.id into o_stud_id;
  END spCreateStudent;

  PROCEDURE spGetStudents(o_students out t_cursor) AS
  BEGIN
    open o_students for
    select * from students; 
  END spGetStudents;

  PROCEDURE spGetStudent(stud_id IN NUMBER, o_student OUT t_cursor) AS
  BEGIN
    open o_student for
    select * from students where id=stud_id;
  END spGetStudent;

  PROCEDURE spUpdateStudent(stud_id NUMBER, stud_name VARCHAR2, stud_email VARCHAR2, stud_mobile VARCHAR2, stud_gender VARCHAR2, stud_dob DATE) AS
  BEGIN
    UPDATE students SET students.name=stud_name, students.email=stud_email, students.mobile=stud_mobile, students.gender=stud_gender, students.dateofbirth=stud_dob 
    WHERE students.id=stud_id;
  END spUpdateStudent;

  PROCEDURE spDeleteStudent(stud_id NUMBER) AS
  BEGIN
    delete from students where id=stud_id;
  END spDeleteStudent;

END PKG_STUDENTS;