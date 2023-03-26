/*  Ryan Hanson
    Project 3.3
    Table Schema
*/
/*  Created by tableDesign_SQL 221120031631
      
  IMPLEMENTATION SETTINGS...
    Schema types (schemaTypes):
      ENTITY;RELATIONSHIP
    Design data types mapped to SQL data types (dataTypes):
      PROJ_NO=CHAR(4);DATE=DATE;TEAM_NO=CHAR(4);PROG_NAME=VARCHAR(50);DEV_NO=CHAR(4);NAME=VARCHAR(50);TEXT=VARCHAR(100);COMP_NAME=VARCHAR(50);DEV_COUNT=CHAR(4);REPO_NAME=VARCHAR(50);DEV_NO=CHAR(4);GIGABYTE=CHAR(10);SOURCECODE_ID=CHAR(4);EDITOR_NAME=VARCHAR(50);PATH=VARCHAR(100);FILE_NAME=VARCHAR(50);LANG_NAME=VARCHAR(50);EXTENSION=CHAR(15);DICT_NAME=VARCHAR(50);FILE_COUNT=CHAR(10);
      (use ;-delimited name=value pairs to map specific, and/or DEFAULT, and/or unspecified ID types)
    ID/Reference constraint suffixes (idSuffix, refSuffix):
      _PK, _FK
*/
CREATE DATABASE IF NOT EXISTS RH_Project3_3;
USE RH_Project3_3;

CREATE TABLE ProgramDevelopment (
  id                    /* PROJ_NO */           CHAR(4)         NOT NULL,    /* PK */
  startDate             /* DATE */              DATE            NOT NULL,
  completionDate        /* DATE */              DATE            NOT NULL,
# DevTeam_id            /* TEAM_NO */           CHAR(4)         NOT NULL,    /* FK */  (n/a by norm: many in 1:many)
  Program_programName   /* PROG_NAME */         VARCHAR(50)     NOT NULL,    /* FK */
      CONSTRAINT  ProgramDevelopment_PK PRIMARY KEY (id)
);

CREATE TABLE SoftwareDeveloper (
  id                    /* DEV_NO */            CHAR(4)         NOT NULL,    /* PK */
  firstName             /* NAME */              VARCHAR(50)         NULL,
  lastName              /* NAME */              VARCHAR(50)     NOT NULL,
  dateOfBirth           /* DATE */              DATE            NOT NULL,
  education             /* TEXT */              VARCHAR(100)        NULL,
  Computer_computerName     /* COMP_NAME */     VARCHAR(50)     NOT NULL,    /* FK */
  DevTeam_id            /* TEAM_NO */           CHAR(4)         NOT NULL,    /* FK */
      CONSTRAINT  SoftwareDeveloper_PK PRIMARY KEY (id)
);

CREATE TABLE DevTeam (
  id                    /* TEAM_NO */           CHAR(4)         NOT NULL,    /* PK */
  teamName              /* NAME */              VARCHAR(50)     NOT NULL,
  devCount              /* DEV_COUNT */         CHAR(4)         NOT NULL,
  Repository_repoName   /* REPO_NAME */         VARCHAR(50)     NOT NULL,    /* FK */
# SoftwareDeveloper_id  /* DEV_NO */            CHAR(4)         NOT NULL,    /* FK */  (n/a by norm: many in 1:many)
  Program_programName   /* PROG_NAME */         VARCHAR(50)     NOT NULL,    /* FK */
  ProgramDevelopment_id     /* PROJ_NO */       CHAR(4)         NOT NULL,    /* FK */
      CONSTRAINT  DevTeam_PK PRIMARY KEY (id)
);

CREATE TABLE Computer (
  computerName          /* COMP_NAME */         VARCHAR(50)     NOT NULL,    /* PK */
  OS                    /* TEXT */              VARCHAR(100)    NOT NULL,
  CPU                   /* TEXT */              VARCHAR(100)    NOT NULL,
  storageSizeGB         /* GIGABYTE */          CHAR(10)        NOT NULL,
  SoftwareDeveloper_id  /* DEV_NO */            CHAR(4)         NOT NULL,    /* FK */
# SourceCodeEditor_id   /* SOURCECODE_ID */     CHAR(4)         NOT NULL,    /* FK */  (n/a by norm: many in 1:many)
  Repository_repoName   /* REPO_NAME */         VARCHAR(50)     NOT NULL,    /* FK */
      CONSTRAINT  Computer_PK PRIMARY KEY (computerName)
);

CREATE TABLE SourceCodeEditor (
  id                    /* SOURCECODE_ID */     CHAR(4)         NOT NULL,    /* PK */
  editorName            /* EDITOR_NAME */       VARCHAR(50)     NOT NULL,
  workingFolder         /* PATH */              VARCHAR(100)    NOT NULL,
  Computer_computerName     /* COMP_NAME */     VARCHAR(50)     NOT NULL,    /* FK */
# File_nameOfFile       /* FILE_NAME */         VARCHAR(50)     NOT NULL,    /* FK */  (n/a by norm: many in 1:many)
      CONSTRAINT  SourceCodeEditor_PK PRIMARY KEY (id)
);

CREATE TABLE File (
  nameOfFile            /* FILE_NAME */         VARCHAR(50)     NOT NULL,    /* PK */
  programmingLanguage   /* LANG_NAME */         VARCHAR(50)         NULL,
  fileType              /* EXTENSION */         CHAR(15)        NOT NULL,
  SourceCodeEditor_id   /* SOURCECODE_ID */     CHAR(4)         NOT NULL,    /* FK */
  Program_programName   /* PROG_NAME */         VARCHAR(50)     NOT NULL,    /* FK */
      CONSTRAINT  File_PK PRIMARY KEY (nameOfFile)
);

CREATE TABLE Repository (
  repoName              /* REPO_NAME */         VARCHAR(50)     NOT NULL,    /* PK */
  repoDictator          /* DICT_NAME */         VARCHAR(50)     NOT NULL,
# Computer_computerName     /* COMP_NAME */     VARCHAR(50)     NOT NULL,    /* FK */  (n/a by norm: many in 1:many)
  DevTeam_id            /* TEAM_NO */           CHAR(4)         NOT NULL,    /* FK */
  Program_programName   /* PROG_NAME */         VARCHAR(50)     NOT NULL,    /* FK */
      CONSTRAINT  Repository_PK PRIMARY KEY (repoName)
);

CREATE TABLE Program (
  programName           /* PROG_NAME */         VARCHAR(50)     NOT NULL,    /* PK */
  fileCount             /* FILE_COUNT */        CHAR(10)        NOT NULL,
  DevTeam_id            /* TEAM_NO */           CHAR(4)         NOT NULL,    /* FK */
  Repository_repoName   /* REPO_NAME */         VARCHAR(50)     NOT NULL,    /* FK */
# File_nameOfFile       /* FILE_NAME */         VARCHAR(50)     NOT NULL,    /* FK */  (n/a by norm: many in 1:many)
  ProgramDevelopment_id     /* PROJ_NO */       CHAR(4)         NOT NULL,    /* FK */
      CONSTRAINT  Program_PK PRIMARY KEY (programName)
);

/*************************************************************************************
    Note for _relationship_ constraints below:
        
    Apply these constraints AFTER adding initial data, to avoid nuisance errors and 
    to more directly identify issues with _entity_ design or initial data
**************************************************************************************/
/*
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
*/
