USE APPLICATION_ENI
GO

-- Tables Mathias BENOIST --
CREATE TABLE ECF(
	idECF			int 		NOT NULL	PRIMARY KEY,
	code			char(8) 	NOT NULL,
	libelle 		char(30) 	NOT NULL,
	coefficient 	float 		NULL,
	typeNotation 	smallint 	NULL,
	nbreVersions 	int 		NULL,
	commentaire 	char(150) 	NULL
)
Go
CREATE TABLE COMPETENCE(
	idCompetence	int			NOT NULL	PRIMARY KEY,
	code			char(8)		NOT NULL,
	libelle			char(100)	NOT NULL
)
Go
CREATE TABLE COMPETENCESECF(
	idECF			int	NOT NULL,
	idCompetence	int NOT NULL,
	PRIMARY KEY (idECF,idCompetence),
	FOREIGN KEY (idECF) 		References ECF(idECF),
	FOREIGN KEY (idCompetence) 	References COMPETENCE(idCompetence)
)
Go
CREATE TABLE FORMATIONSECF(
	idECF 			int 	NOT NULL,
	idFormation 	char(8) NOT NULL,
	PRIMARY KEY (idECF,idFormation),
	FOREIGN KEY (idECF) 		References ECF(idECF),
	FOREIGN KEY (idFormation) 	References Formation(CodeFormation)
)
Go
CREATE TABLE SESSIONSECF(
	idSessionECF	int			NOT NULL	PRIMARY KEY,
	idECF			int			NOT NULL,
	date			DateTime	NOT NULL,
	version			int			NOT NULL,
	FOREIGN KEY (idECF)			References ECF(idECF)
)
Go
CREATE TABLE PARTICIPANTSSESSIONECF(
	idSessionECF	int	NOT NULL,
	idStagiaire		int	NOT NULL,
	PRIMARY KEY (idSessionECF,idStagiaire),
	FOREIGN KEY (idSessionECF) 	References SESSIONSECF(idSessionECF),
	FOREIGN KEY (idStagiaire) 	References Stagiaire(CodeStagiaire)
)
Go
CREATE TABLE EVALUATION(
	idEvaluation	int			NOT NULL	PRIMARY KEY,
	idECF			int			NOT NULL,
	idStagiaire		int 		NOT NULL,
	idCompetence	int			NOT NULL,
	note			float		NULL,
	version			int			NULL,
	date			datetime	NULL,
	FOREIGN KEY (idECF)			References ECF(idECF),
	FOREIGN KEY (idStagiaire) 	References Stagiaire(CodeStagiaire),
	FOREIGN KEY (idCompetence) 	References COMPETENCE(idCompetence)
)
Go
-- Donnees --
--ECFS (9)
INSERT INTO ECF  
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(1,'ECFDEV01','Base de données',1,1,3,'commentaire libre...')
GO
INSERT INTO ECF 
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(2,'ECFDEV02','Développement client-serveur',1,1,2,'commentaire libre...')
GO
INSERT INTO ECF 
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(3,'ECFDEV03','Modélisation',1,1,2,'commentaire libre...')
GO
INSERT INTO ECF 
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(4,'ECFDEV04','Développement Web',1,1,2,'commentaire libre...')
GO
INSERT INTO ECF 
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(5,'ECFDEV05','Développement multi-tiers',1,1,1,'commentaire libre...')
GO
INSERT INTO ECF 
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(6,'ECFDEV06','Administration Tomcat',1,1,2,'commentaire libre...')
GO
INSERT INTO ECF 
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(7,'ECFDEV07','Conduite de projet',1,0,1,'commentaire libre...')
GO
INSERT INTO ECF 
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(8,'ECFDEV08','Anglais',1,0,2,'commentaire libre...')
GO
INSERT INTO ECF 
(idECF,code,libelle,coefficient,typeNotation,nbreVersions,commentaire)
VALUES
(9,'ECFDEV09','Exposé',1,0,1,'commentaire libre...')
GO
--COMPETENCES (24)
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(1,'CDEV01','Maquetter l''application')
Go                                                                             
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(2,'CDEV02','Programmer les formulaires et les états')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(3,'CDEV03','Programmer des pages Web')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(4,'CDEV04','Manipuler les données avec le langage SQL')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(5,'CDEV05','Développer les composants d''accès aux données')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(6,'CDEV06','Installer des composants')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(7,'CDEV07','Assister les utilisateurs')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(8,'CDEV08','Organiser son temps')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(9,'CDEV09','Communiquer dans un contexte professionnel')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(10,'CDEV10','Utiliser l''anglais dans son activité professionnelle en informatique')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(11,'CDEV11','Actualiser ses compétences techniques')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(12,'CDEV12','Modéliser les données')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(13,'CDEV13','Mettre en place la base de données')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(14,'CDEV14','Programmer dans le langage du SGBD')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(15,'CDEV15','Définir l''architecture de l''application')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(16,'CDEV16','Modéliser l''application à développer en utilisant UML')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(17,'CDEV17','Appliquer une démarche qualité')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(18,'CDEV18','Développer les composants métier')
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(19,'CDEV19','Manipuler les données réparties dans une architecture client-serveur x-tiers')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(20,'CDEV20','Développer les composants de la couche présentation (IHM)')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(21,'CDEV21','Développer des composants intégrés à l''informatique nomade')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(22,'CDEV22','Réaliser un test d''intégration')
Go                                                                      
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(23,'CDEV23','Déployer l''application')
Go
INSERT INTO COMPETENCE 
(idCompetence,code,libelle)
VALUES
(24,'CDEV24','Animer l''équipe de développement')
Go
--COMPETENCESECF (24)
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(1,4)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(1,13)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(1,14)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(2,2)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(2,5)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(2,6)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(2,8)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(2,19)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(2,20)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(3,12)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(4,1)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(4,3)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(4,22)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(5,15)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(5,16)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(5,18)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(5,21)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(6,23)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(7,17)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(7,24)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(8,10)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(9,7)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(9,9)
Go
INSERT INTO COMPETENCESECF
(idECF,idCompetence)
VALUES
(9,11)
Go 
--FORMATIONSECF (1)
INSERT INTO FORMATIONSECF
(idECF,idFormation)
VALUES
(1,'AL')
Go
--SESSIONSECF (5)
INSERT INTO SESSIONSECF
(idSessionECF,idECF,date,version)
VALUES
(1,1,'15-11-2012 00:00:00.000',1)
Go
INSERT INTO SESSIONSECF
(idSessionECF,idECF,date,version)
VALUES
(2,2,'01-11-2012 00:00:00.000',1)
Go
INSERT INTO SESSIONSECF
(idSessionECF,idECF,date,version)
VALUES
(3,3,'24-11-2012 00:00:00.000',1)
Go
INSERT INTO SESSIONSECF
(idSessionECF,idECF,date,version)
VALUES
(4,4,'23-11-2012 00:00:00.000',1)
Go
INSERT INTO SESSIONSECF
(idSessionECF,idECF,date,version)
VALUES
(5,5,'29-11-2012 00:00:00.000',1)
Go
--PARTICIPANTSSRSSIONECF (5)
INSERT INTO PARTICIPANTSSESSIONECF
(idSessionECF,idStagiaire)
VALUES
(1,818)
Go
INSERT INTO PARTICIPANTSSESSIONECF
(idSessionECF,idStagiaire)
VALUES
(2,818)
Go
INSERT INTO PARTICIPANTSSESSIONECF
(idSessionECF,idStagiaire)
VALUES
(3,818)
Go
INSERT INTO PARTICIPANTSSESSIONECF
(idSessionECF,idStagiaire)
VALUES
(4,818)
Go
INSERT INTO PARTICIPANTSSESSIONECF
(idSessionECF,idStagiaire)
VALUES
(5,818)
Go
--EVALUATION (8)
INSERT INTO EVALUATION
(idEvaluation,idECF,idStagiaire,idCompetence,note,version,date)
VALUES
(1,1,818,4,1,1,'15-11-2012 00:00:00.000')
Go
INSERT INTO EVALUATION
(idEvaluation,idECF,idStagiaire,idCompetence,note,version,date)
VALUES
(2,1,818,13,0,1,'15-11-2012 00:00:00.000')
Go
INSERT INTO EVALUATION
(idEvaluation,idECF,idStagiaire,idCompetence,note,version,date)
VALUES
(3,1,818,14,2,1,'15-11-2012 00:00:00.000')
Go
INSERT INTO EVALUATION
(idEvaluation,idECF,idStagiaire,idCompetence,note,version,date)
VALUES
(4,2,818,2,1,1,'01-11-2012 00:00:00.000')
Go
INSERT INTO EVALUATION
(idEvaluation,idECF,idStagiaire,idCompetence,note,version,date)
VALUES
(5,2,818,5,0,1,'01-11-2012 00:00:00.000')
Go
INSERT INTO EVALUATION
(idEvaluation,idECF,idStagiaire,idCompetence,note,version,date)
VALUES
(6,2,818,6,2,1,'01-11-2012 00:00:00.000')
Go
INSERT INTO EVALUATION
(idEvaluation,idECF,idStagiaire,idCompetence,note,version,date)
VALUES
(7,2,818,8,1,1,'01-11-2012 00:00:00.000')
Go
INSERT INTO EVALUATION
(idEvaluation,idECF,idStagiaire,idCompetence,note,version,date)
VALUES
(8,2,818,19,0,1,'01-11-2012 00:00:00.000')
Go

-- Tables Thomas RETIF --

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[ABSENCE]    Script Date: 10/09/2012 10:17:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ABSENCE](
	[id_absence] [int] NOT NULL IDENTITY(0, 1) PRIMARY KEY,
	[raison] [varchar](5000) NULL,
	[commentaire] [varchar](5000) NULL,
	[dateDebut] [datetime] NULL,
	[dateFin] [datetime] NULL,
	[justifiee] [bit] NULL,
	[isAbsence] [bit] NULL,
	[id_stagiaire] [int] NULL,
	
)

GO

SET ANSI_PADDING OFF
GO

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[OBSERVATION]    Script Date: 10/09/2012 10:41:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[OBSERVATION](
	[id_observation] [int] IDENTITY(0,1) NOT NULL PRIMARY KEY,
	[date] [date] NULL,
	[auteur] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[titre] [varchar](500) NULL,
	[texte] [varchar](5000) NULL,
	[id_stagiaire] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET IDENTITY_INSERT [dbo].[OBSERVATION] ON
INSERT [dbo].[OBSERVATION] ([id_observation], [date], [auteur], [type], [titre], [texte], [id_stagiaire]) VALUES (0, CAST(0x41360B00 AS Date), N'jgabillaud', N'Pédagogique', N'tets', N'test', 1)
INSERT [dbo].[OBSERVATION] ([id_observation], [date], [auteur], [type], [titre], [texte], [id_stagiaire]) VALUES (1, CAST(0x41360B00 AS Date), N'jgabillaud', N'Pédagogique', N'ds', N'sdqqsdfqsf', 1)
INSERT [dbo].[OBSERVATION] ([id_observation], [date], [auteur], [type], [titre], [texte], [id_stagiaire]) VALUES (6, CAST(0x41360B00 AS Date), N'jgabillaud', N'Pédagogique', N'ut', N'ut', 1)
SET IDENTITY_INSERT [dbo].[OBSERVATION] OFF
/****** Object:  Table [dbo].[ABSENCE]    Script Date: 10/10/2012 16:59:25 ******/

SET IDENTITY_INSERT [dbo].[ABSENCE] ON
INSERT [dbo].[ABSENCE] ([id_absence], [raison], [commentaire], [dateDebut], [dateFin], [justifiee], [isAbsence], [id_stagiaire]) VALUES (4, N'Panne de réveil', N'Panne de motivation plutôt...', CAST(0x0000A0E6009450C0 AS DateTime), CAST(0x0000A0E600A74450 AS DateTime), 0, 1, 1)
INSERT [dbo].[ABSENCE] ([id_absence], [raison], [commentaire], [dateDebut], [dateFin], [justifiee], [isAbsence], [id_stagiaire]) VALUES (11, N'test', N'test...', CAST(0x0000A0E6009450C0 AS DateTime), CAST(0x0000A0E600A74450 AS DateTime), 1, 0, 1)
INSERT [dbo].[ABSENCE] ([id_absence], [raison], [commentaire], [dateDebut], [dateFin], [justifiee], [isAbsence], [id_stagiaire]) VALUES (12, N'Panne de réveil', N'Panne de motivation plutôt...', CAST(0x0000A0E6009450C0 AS DateTime), CAST(0x0000A0E600A74450 AS DateTime), 0, 1, 1)
SET IDENTITY_INSERT [dbo].[ABSENCE] OFF
USE [APPLICATION_ENI]
GO
SET IDENTITY_INSERT [dbo].[Stagiaire] ON
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (12, N'Mr ', N'Pestiaux', N'Gilles', N'26 rue du Fond', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\1.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (13, N'Mr ', N'Grandieu', N'Paul', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\2.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (14, N'Mr ', N'Baudrier', N'Eude', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\3.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (15, N'Mr ', N'Gresnault', N'Sabri', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\4.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (16, N'Mr ', N'Martin', N'Goliath', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\5.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (17, N'Mr ', N'Drifue', N'David', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\6.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (18, N'Mr ', N'Popse', N'Roman', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\7.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (19, N'Mr ', N'Kaze', N'Mathias', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\1.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (20, N'Mr ', N'Judde', N'Li', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\2.jpg', 0, NULL)INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (11, N'Mr ', N'Grandieu', N'Paul', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\2.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (21, N'Mr ', N'Uqsdf', N'Hank', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\3.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (22, N'Mr ', N'Puitre', N'Chad', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\4.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (23, N'Mr ', N'Natrui', N'Grety', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\5.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (24, N'Mr ', N'Brunot', N'Jean-Michel', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\6.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (25, N'Mr ', N'Huibet', N'Bruno', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\7.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (26, N'Mr ', N'Vertuit', N'Gabriel', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\1.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (27, N'Mr ', N'Vunier', N'Thomas', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\2.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (28, N'Mr ', N'Crosnier', N'Paul', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\3.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (29, N'Mr ', N'Jerref', N'Luc', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\4.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (30, N'Mr ', N'Crobi', N'Bob', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\5.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (31, N'Mr ', N'Xerni', N'Saïe', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\6.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (32, N'Mr ', N'Wrap', N'Azgroupf', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\7.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (33, N'Mr ', N'Prius', N'Bill', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\1.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (34, N'Mr ', N'Yuidv', N'Rael', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\2.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (35, N'Mr ', N'Nuitre', N'Tajib', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\3.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (36, N'Mr ', N'Poitre', N'Gertrude', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\4.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (37, N'Mr ', N'Moiret', N'Pamela', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\5.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (38, N'Mr ', N'Noremt', N'Gilles', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\6.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (39, N'Mr ', N'Bruitui', N'Jeff', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\7.jpg', 0, NULL)
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (40, N'Mr ', N'Basse', N'Jeanne', N'8 bd José', NULL, NULL, N'35500', N'Rennes', N'0202020202    ', N'0202020202    ', N'test@gmail.com', NULL, N'__', N'__', N'__', NULL, NULL, NULL, 0, N'c:\testPhotos\1.jpg', 0, NULL)
SET IDENTITY_INSERT [dbo].[Stagiaire] OFF

-- Tables Roman LION --

USE [APPLICATION_ENI]
GO
/****** Object:  Table [dbo].[EPREUVETITRE]    Script Date: 11/10/2012 00:45:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EPREUVETITRE](
	[CodeSalle] [varchar](5) NOT NULL,
	[CodeTitre] [char](8) NOT NULL,
	[dateEpreuve] [datetime] NOT NULL,
 CONSTRAINT [PK_EPREUVETITRE] PRIMARY KEY CLUSTERED 
(
	[CodeSalle] ASC,
	[CodeTitre] ASC,
	[dateEpreuve] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EPTITREJURY]    Script Date: 11/10/2012 00:45:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EPTITREJURY](
	[idJury] [int] NOT NULL,
	[CodeTitre] [char](8) NOT NULL,
	[dateEpreuve] [datetime] NOT NULL,
	[CodeSalle] [varchar](5) NOT NULL,
 CONSTRAINT [PK_EPTITREJURY] PRIMARY KEY CLUSTERED 
(
	[idJury] ASC,
	[CodeTitre] ASC,
	[dateEpreuve] ASC,
	[CodeSalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JURY]    Script Date: 11/10/2012 00:45:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JURY](
	[idJury] [int] IDENTITY(1,1) NOT NULL,
	[civilite] [varchar](4) NULL,
	[nom] [varchar](50) NOT NULL,
	[prenom] [varchar](50) NOT NULL,
 CONSTRAINT [PK_JURY] PRIMARY KEY CLUSTERED 
(
	[idJury] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Titre]    Script Date: 11/10/2012 00:45:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
INSERT [dbo].[EPREUVETITRE] ([CodeSalle], [CodeTitre], [dateEpreuve]) VALUES (N'S101', N'AL      ', CAST(0x0000A12500000000 AS DateTime))
INSERT [dbo].[EPREUVETITRE] ([CodeSalle], [CodeTitre], [dateEpreuve]) VALUES (N'S102', N'AL      ', CAST(0x0000A12C00000000 AS DateTime))
INSERT [dbo].[EPTITREJURY] ([idJury], [CodeTitre], [dateEpreuve], [CodeSalle]) VALUES (1, N'AL      ', CAST(0x0000A12500000000 AS DateTime), N'S101')
INSERT [dbo].[EPTITREJURY] ([idJury], [CodeTitre], [dateEpreuve], [CodeSalle]) VALUES (2, N'AL      ', CAST(0x0000A12500000000 AS DateTime), N'S101')
INSERT [dbo].[EPTITREJURY] ([idJury], [CodeTitre], [dateEpreuve], [CodeSalle]) VALUES (3, N'AL      ', CAST(0x0000A12C00000000 AS DateTime), N'S102')
INSERT [dbo].[EPTITREJURY] ([idJury], [CodeTitre], [dateEpreuve], [CodeSalle]) VALUES (4, N'AL      ', CAST(0x0000A12C00000000 AS DateTime), N'S102')
SET IDENTITY_INSERT [dbo].[JURY] ON 

INSERT [dbo].[JURY] ([idJury], [civilite], [nom], [prenom]) VALUES (1, N'Mr', N'Bond', N'James')
INSERT [dbo].[JURY] ([idJury], [civilite], [nom], [prenom]) VALUES (2, N'Mme', N'Spears', N'Britney')
INSERT [dbo].[JURY] ([idJury], [civilite], [nom], [prenom]) VALUES (3, N'Mlle', N'Gomez', N'Selena')
INSERT [dbo].[JURY] ([idJury], [civilite], [nom], [prenom]) VALUES (4, N'Mr', N'Dujardin', N'Jean')
SET IDENTITY_INSERT [dbo].[JURY] OFF

ALTER TABLE [dbo].[EPREUVETITRE]  WITH CHECK ADD  CONSTRAINT [FK_EPREUVETITRE_Titre] FOREIGN KEY([CodeTitre])
REFERENCES [dbo].[Titre] ([CodeTitre])
GO
ALTER TABLE [dbo].[EPREUVETITRE] CHECK CONSTRAINT [FK_EPREUVETITRE_Titre]
GO
ALTER TABLE [dbo].[EPTITREJURY]  WITH CHECK ADD  CONSTRAINT [FK_EPTITREJURY_EPREUVETITRE] FOREIGN KEY([CodeSalle], [CodeTitre], [dateEpreuve])
REFERENCES [dbo].[EPREUVETITRE] ([CodeSalle], [CodeTitre], [dateEpreuve])
GO
ALTER TABLE [dbo].[EPTITREJURY] CHECK CONSTRAINT [FK_EPTITREJURY_EPREUVETITRE]
GO
ALTER TABLE [dbo].[EPTITREJURY]  WITH CHECK ADD  CONSTRAINT [FK_EPTITREJURY_JURY] FOREIGN KEY([idJury])
REFERENCES [dbo].[JURY] ([idJury])
GO
ALTER TABLE [dbo].[EPTITREJURY] CHECK CONSTRAINT [FK_EPTITREJURY_JURY]
GO

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[PASSAGETITRE]    Script Date: 11/29/2012 15:55:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PASSAGETITRE](
	[CodeTitre] [char](8) NOT NULL,
	[CodeStagiaire] [int] NOT NULL,
	[datePassage] [date] NOT NULL,
	[estObtenu] [bit] NULL,
	[estValide] [bit] NULL,
 CONSTRAINT [PK_PASSAGETITRE] PRIMARY KEY CLUSTERED 
(
	[CodeTitre] ASC,
	[CodeStagiaire] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PASSAGETITRE] ADD  CONSTRAINT [DF_PASSAGETITRE_estObtenu]  DEFAULT ((0)) FOR [estObtenu]
GO

ALTER TABLE [dbo].[PASSAGETITRE] ADD  CONSTRAINT [DF_PASSAGETITRE_estValide]  DEFAULT ((0)) FOR [estValide]
GO