/*  Ryan Hanson
    Project 3.3
    Relationship Schema
    Copied from XSLerator tableSchema and pasted here to separate files
    */

USE RH_Project3_3;


ALTER TABLE ProgramDevelopment
  ADD CONSTRAINT  ProgramDevelopment_Program_FK FOREIGN KEY (Program_programName)  REFERENCES Program (programName)
;

ALTER TABLE SoftwareDeveloper
  ADD CONSTRAINT  SoftwareDeveloper_Computer_FK FOREIGN KEY (Computer_computerName)  REFERENCES Computer (computerName)
;

ALTER TABLE SoftwareDeveloper
  ADD CONSTRAINT  SoftwareDeveloper_DevTeam_FK FOREIGN KEY (DevTeam_id)  REFERENCES DevTeam (id)
;

ALTER TABLE DevTeam
  ADD CONSTRAINT  DevTeam_Repository_FK FOREIGN KEY (Repository_repoName)  REFERENCES Repository (repoName)
;

ALTER TABLE DevTeam
  ADD CONSTRAINT  DevTeam_Program_FK FOREIGN KEY (Program_programName)  REFERENCES Program (programName)
;

ALTER TABLE DevTeam
  ADD CONSTRAINT  DevTeam_ProgramDevelopment_FK FOREIGN KEY (ProgramDevelopment_id)  REFERENCES ProgramDevelopment (id)
;

ALTER TABLE Computer
  ADD CONSTRAINT  Computer_SoftwareDeveloper_FK FOREIGN KEY (SoftwareDeveloper_id)  REFERENCES SoftwareDeveloper (id)
;

ALTER TABLE Computer
  ADD CONSTRAINT  Computer_Repository_FK FOREIGN KEY (Repository_repoName)  REFERENCES Repository (repoName)
;

ALTER TABLE SourceCodeEditor
  ADD CONSTRAINT  SourceCodeEditor_Computer_FK FOREIGN KEY (Computer_computerName)  REFERENCES Computer (computerName)
;

ALTER TABLE File
  ADD CONSTRAINT  File_SourceCodeEditor_FK FOREIGN KEY (SourceCodeEditor_id)  REFERENCES SourceCodeEditor (id)
;

ALTER TABLE File
  ADD CONSTRAINT  File_Program_FK FOREIGN KEY (Program_programName)  REFERENCES Program (programName)
;

ALTER TABLE Repository
  ADD CONSTRAINT  Repository_DevTeam_FK FOREIGN KEY (DevTeam_id)  REFERENCES DevTeam (id)
;

ALTER TABLE Repository
  ADD CONSTRAINT  Repository_Program_FK FOREIGN KEY (Program_programName)  REFERENCES Program (programName)
;

ALTER TABLE Program
  ADD CONSTRAINT  Program_DevTeam_FK FOREIGN KEY (DevTeam_id)  REFERENCES DevTeam (id)
;

ALTER TABLE Program
  ADD CONSTRAINT  Program_Repository_FK FOREIGN KEY (Repository_repoName)  REFERENCES Repository (repoName)
;

ALTER TABLE Program
  ADD CONSTRAINT  Program_ProgramDevelopment_FK FOREIGN KEY (ProgramDevelopment_id)  REFERENCES ProgramDevelopment (id)
;