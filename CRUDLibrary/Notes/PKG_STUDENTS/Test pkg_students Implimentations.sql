-- ====Test pkg_students Implimentations====
SELECT * FROM students order by id DESC;

-- Create One
SET SERVEROUTPUT ON;
VARIABLE o_id NUMBER;
EXECUTE pkg_students.spCreateStudent('Chris Jones', 'jones@email.com', '713-876-3344', 'Male', '10-OCT-12', :o_id);
PRINT o_id;

-- Test Read All
SET SERVEROUTPUT ON;
VARIABLE v_cursor REFCURSOR;
EXECUTE pkg_students.spGetStudents(:v_cursor);
PRINT v_cursor;

-- Test Read One
SET SERVEROUTPUT ON;
VARIABLE v_cursor REFCURSOR;
EXECUTE pkg_students.spgetstudent(6, :v_cursor);
PRINT v_cursor;

-- Update Student
EXECUTE pkg_students.spupdatestudent(6, 'Seth JEAN', 'sam2@email.com', '713-222-3344', 'Male', '23-OCT-22');
select * from students WHERE id=6;

-- Delete Student
EXECUTE pkg_students.spdeletestudent(3);
select * from students ORDER by id;