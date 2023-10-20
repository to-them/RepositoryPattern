CREATE OR REPLACE
PACKAGE BODY PKG_STUDENTS2 AS

    -- insert one
  PROCEDURE spCreateStudent(
    stud_name IN STUDENTS.NAME%TYPE, 
    stud_email IN STUDENTS.EMAIL%TYPE, 
    stud_mobile IN STUDENTS.MOBILE%TYPE, 
    stud_gender IN STUDENTS.GENDER%TYPE, 
    stud_dob IN STUDENTS.DATEOFBIRTH%TYPE, 
    o_stud_id OUT NUMBER
    ) AS
  BEGIN
    INSERT INTO students(name, email, mobile, gender, dateofbirth) 
    VALUES(stud_name, stud_email, stud_mobile, stud_gender, stud_dob)
    RETURNING students.id into o_stud_id;
  END spCreateStudent;

    -- select all
  PROCEDURE spGetStudents(
    o_students out t_cursor
    ) AS
  BEGIN
    open o_students for
    select * from students;
  END spGetStudents;

    -- select one
  PROCEDURE spGetStudent(
    stud_id IN STUDENTS.ID%TYPE, 
    o_student OUT t_cursor
    ) AS
  BEGIN
    open o_student for
    select * from students where id=stud_id;
  END spGetStudent;

    -- update one
  PROCEDURE spUpdateStudent(
    stud_id IN STUDENTS.ID%TYPE, 
    stud_name IN STUDENTS.NAME%TYPE, 
    stud_email IN STUDENTS.EMAIL%TYPE, 
    stud_mobile IN STUDENTS.MOBILE%TYPE, 
    stud_gender IN STUDENTS.GENDER%TYPE, 
    stud_dob IN STUDENTS.DATEOFBIRTH%TYPE
    ) AS
  BEGIN
    UPDATE students SET students.name=stud_name, students.email=stud_email, students.mobile=stud_mobile, students.gender=stud_gender, students.dateofbirth=stud_dob 
    WHERE students.id=stud_id;
  END spUpdateStudent;

    -- delete
  PROCEDURE spDeleteStudent(
    stud_id IN STUDENTS.ID%TYPE
    ) AS
  BEGIN
    delete from students where id=stud_id;
  END spDeleteStudent;

END PKG_STUDENTS2;