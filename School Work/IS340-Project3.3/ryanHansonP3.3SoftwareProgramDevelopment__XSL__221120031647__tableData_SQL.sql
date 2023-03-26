/*  Ryan Hanson
    Project 3.3
    Table Data
    Copied from XSLerator tableSchema and pasted here to separate files
    */
/*  Created by tableData_SQL 221120031647
    Not-applicable (n/a) data pattern (naValue):
      n/a
    No-data (null) data value (nullValue):
      NULL
    Separator for composite ID values combined into single ID value (idSep):
      | ("bar")
*/
USE RH_Project3_3;

/*  ProgramDevelopment( id, startDate, completionDate, [N/A:DevTeam], Program )
*/
INSERT INTO ProgramDevelopment VALUES (
  5576, '2022-10-22', '2022-11-6', 'myProgram1' );
INSERT INTO ProgramDevelopment VALUES (
  5586, '2022-11-5', '2023-01-14', 'XProgram' );

/*  SoftwareDeveloper( id, firstName, lastName, dateOfBirth, education, Computer, DevTeam )
*/
INSERT INTO SoftwareDeveloper VALUES (
  1756, 'Ryan', 'Hanson', '2002-04-02', 'Student', 'RyansMac', 4467 );
INSERT INTO SoftwareDeveloper VALUES (
  1757, 'Steve', 'Jobs', '1955-02-24', NULL, 'StevesMac', 4467 );
INSERT INTO SoftwareDeveloper VALUES (
  1758, 'Bill', 'Gates', '1955-10-28', NULL, 'BillsPC', 4467 );
INSERT INTO SoftwareDeveloper VALUES (
  1856, 'Elon', 'Musk', '1971-06-28', 'Graduate', 'ElonsPC', 4477 );
INSERT INTO SoftwareDeveloper VALUES (
  1857, 'Joe', 'Rogan', '1967-08-11', NULL, 'JoesMac', 4477 );

/*  DevTeam( id, teamName, devCount, Repository, [N/A:SoftwareDeveloper], Program, ProgramDevelopment )
*/
INSERT INTO DevTeam VALUES (
  4467, 'TeamOne', 3, 'MyRepo1', 'MyProgram1', 5576 );
INSERT INTO DevTeam VALUES (
  4477, 'TeamX', 2, 'SpaceXRepo', 'XProgram', 5586 );

/*  Computer( computerName, OS, CPU, storageSizeGB, SoftwareDeveloper, [N/A:SourceCodeEditor], Repository )
*/
INSERT INTO Computer VALUES (
  'RyansMac', 'MacOS', 'M1', 250, 1756, 'MyRepo1' );
INSERT INTO Computer VALUES (
  'StevesMac', 'MacOS', 'M1 Pro', 500, 1757, 'MyRepo1' );
INSERT INTO Computer VALUES (
  'BillsPC', 'Windows', 'Intel I9', 550, 1758, 'MyRepo1' );
INSERT INTO Computer VALUES (
  'ElonsPC', 'Linux', 'Intel I9', 720, 1856, 'SpaceXRepo' );
INSERT INTO Computer VALUES (
  'JoesMac', 'MacOS', 'M2', 450, 1857, 'SpaceXRepo' );

/*  SourceCodeEditor( id, editorName, workingFolder, Computer, [N/A:File] )
*/
INSERT INTO SourceCodeEditor VALUES (
  1122, 'VSCode', '/Desktop/MyRepo1', 'RyansMac' );
INSERT INTO SourceCodeEditor VALUES (
  1123, 'Xcode', '/Desktop/MyRepo1', 'StevesMac' );
INSERT INTO SourceCodeEditor VALUES (
  1124, 'VSCode', '/Desktop/WorkFolder/MyRepo1', 'BillsPC' );
INSERT INTO SourceCodeEditor VALUES (
  1131, 'VSCode', '/home/ElonMusk/Desktop/SpaceXRepo', 'ElonsPC' );
INSERT INTO SourceCodeEditor VALUES (
  1132, 'Atom', '/Desktop/SpaceXRepo', 'JoesMac' );

/*  File( nameOfFile, programmingLanguage, fileType, SourceCodeEditor, Program )
*/
INSERT INTO File VALUES (
  'MyProg1CS', 'CSHARP', '.cs', 1122, 'MyProgram1' );
INSERT INTO File VALUES (
  'MyProg1Index', 'HTML', '.html', 1123, 'MyProgram1' );
INSERT INTO File VALUES (
  'MyProg1Styles', 'CSS', '.css', 1123, 'MyProgram1' );
INSERT INTO File VALUES (
  'MyProg1JS', 'JavaScript', '.js', 1124, 'MyProgram1' );
INSERT INTO File VALUES (
  'XProgCS', 'CSHARP', '.cs', 1131, 'XProgram' );
INSERT INTO File VALUES (
  'XProgIndex', 'HTML', '.html', 1131, 'XProgram' );
INSERT INTO File VALUES (
  'XProgStyles', 'CSS', '.css', 1132, 'XProgram' );

/*  Repository( repoName, repoDictator, [N/A:Computer], DevTeam, Program )
*/
INSERT INTO Repository VALUES (
  'MyRepo1', 'Ryan Hanson', 4467, 'MyProgram1' );
INSERT INTO Repository VALUES (
  'SpaceXRepo', 'Elon Musk', 4477, 'XProgram' );

/*  Program( programName, fileCount, DevTeam, Repository, [N/A:File], ProgramDevelopment )
*/
INSERT INTO Program VALUES (
  'MyProgram1', 4, 4467, 'MyRepo1', 5576 );
INSERT INTO Program VALUES (
  'XProgram', 3, 4477, 'SpaceXRepo', 5586 );
