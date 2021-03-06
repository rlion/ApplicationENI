USE [APPLICATION_ENI]
GO
/****** Object:  Table [dbo].[Stagiaire]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stagiaire](
	[CodeStagiaire] [int] IDENTITY(10,1) NOT NULL,
	[Civilite] [char](3) NULL,
	[Nom] [varchar](50) NOT NULL,
	[Prenom] [varchar](50) NOT NULL,
	[Adresse1] [varchar](500) NOT NULL,
	[Adresse2] [varchar](500) NULL,
	[Adresse3] [varchar](500) NULL,
	[Codepostal] [char](5) NULL,
	[Ville] [varchar](100) NULL,
	[TelephoneFixe] [char](14) NULL,
	[TelephonePortable] [char](14) NULL,
	[Email] [varchar](100) NULL,
	[DateNaissance] [smalldatetime] NULL,
	[CodeRegion] [char](2) NULL,
	[CodeNationalite] [char](2) NULL,
	[CodeOrigineMedia] [char](2) NULL,
	[DateDernierEnvoiDoc] [datetime] NULL,
	[DateCreation] [datetime] NULL,
	[Repertoire] [varchar](100) NULL,
	[Permis] [bit] NOT NULL,
	[Photo] [varchar](100) NULL,
	[EnvoiDocEnCours] [bit] NOT NULL,
	[Historique] [varchar](max) NULL,
 CONSTRAINT [PK_Stagiaire] PRIMARY KEY CLUSTERED 
(
	[CodeStagiaire] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Référencer les coordonnées d''une personne de l''état candidat potentiel à l''état stagiaire et après..' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stagiaire'
GO
SET IDENTITY_INSERT [dbo].[Stagiaire] ON
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (735, N'M. ', N'BLUNT', N'James', N'109 rue du xxxxxxxx', NULL, NULL, N'44000', N'NANTES', N'01 01 50      ', N'06 07 08      ', N'mathiasbenois.com', CAST(0x787C0000 AS SmallDateTime), N'IF', N'__', N'06', NULL, CAST(0x00009F160104C577 AS DateTime), N'BENOIST_Mathias_818', 0, N'1.jpg', 0, N'
')
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (818, N'M. ', N'BENOIST', N'Mathias', N'109 rue du xxxxxxxx', NULL, NULL, N'44000', N'NANTES', N'01 01 50      ', N'06 07 08      ', N'mathiasbenois.com', CAST(0x787C0000 AS SmallDateTime), N'IF', N'__', N'06', NULL, CAST(0x00009F160104C577 AS DateTime), N'BENOIST_Mathias_818', 0, N'BENOIST_Mathias_818.jpeg', 0, N'
')
INSERT [dbo].[Stagiaire] ([CodeStagiaire], [Civilite], [Nom], [Prenom], [Adresse1], [Adresse2], [Adresse3], [Codepostal], [Ville], [TelephoneFixe], [TelephonePortable], [Email], [DateNaissance], [CodeRegion], [CodeNationalite], [CodeOrigineMedia], [DateDernierEnvoiDoc], [DateCreation], [Repertoire], [Permis], [Photo], [EnvoiDocEnCours], [Historique]) VALUES (2020, N'M. ', N'BERNARD', N'Francis', N'109 rue du xxxxxxxx', NULL, NULL, N'44000', N'NANTES', N'01 01 50      ', N'06 07 08      ', N'mathiasbenois.com', CAST(0x787C0000 AS SmallDateTime), N'IF', N'__', N'06', NULL, CAST(0x00009F160104C577 AS DateTime), N'BENOIST_Mathias_818', 0, N'2.jpg', 0, N'
')
SET IDENTITY_INSERT [dbo].[Stagiaire] OFF
/****** Object:  Table [dbo].[UniteFormation]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UniteFormation](
	[Libelle] [varchar](200) NOT NULL,
	[DureeEnHeures] [smallint] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DureeEnSemaines] [tinyint] NOT NULL,
	[DateModif] [timestamp] NOT NULL,
	[LibelleCourt] [varchar](10) NOT NULL,
	[IdUniteFormation] [int] IDENTITY(1,1) NOT NULL,
	[Archiver] [bit] NOT NULL,
 CONSTRAINT [PK_UniteFormation] PRIMARY KEY CLUSTERED 
(
	[IdUniteFormation] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[UniteFormation] ON
INSERT [dbo].[UniteFormation] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [LibelleCourt], [IdUniteFormation], [Archiver]) VALUES (N'Systèmes et Réseaux', 280, CAST(0x00009FB100000000 AS DateTime), 8, N'AL-SYS-RES', 77, 0)
INSERT [dbo].[UniteFormation] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [LibelleCourt], [IdUniteFormation], [Archiver]) VALUES (N'Développement', 175, CAST(0x00009FB200000000 AS DateTime), 5, N'AL-DEV', 80, 0)
INSERT [dbo].[UniteFormation] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [LibelleCourt], [IdUniteFormation], [Archiver]) VALUES (N'Conduite de projet', 140, CAST(0x00009FB200000000 AS DateTime), 4, N'AL-PROJ', 81, 0)
SET IDENTITY_INSERT [dbo].[UniteFormation] OFF
/****** Object:  Table [dbo].[TypeProfil]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeProfil](
	[CodeTypeProfil] [int] IDENTITY(1,1) NOT NULL,
	[Libelle] [varchar](200) NOT NULL,
	[LibelleCourt] [char](5) NOT NULL,
 CONSTRAINT [PK_TypeProfil] PRIMARY KEY CLUSTERED 
(
	[CodeTypeProfil] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Formation Continue, CP, Module, VAE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TypeProfil', @level2type=N'COLUMN',@level2name=N'CodeTypeProfil'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Le stagiaire suit la formation continue, contrat professionnel ou module' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TypeProfil'
GO
SET IDENTITY_INSERT [dbo].[TypeProfil] ON
INSERT [dbo].[TypeProfil] ([CodeTypeProfil], [Libelle], [LibelleCourt]) VALUES (1, N'Formation continue', N'FC   ')
INSERT [dbo].[TypeProfil] ([CodeTypeProfil], [Libelle], [LibelleCourt]) VALUES (2, N'Contrat de professionnalisation', N'CP   ')
INSERT [dbo].[TypeProfil] ([CodeTypeProfil], [Libelle], [LibelleCourt]) VALUES (3, N'Module', N'MO   ')
INSERT [dbo].[TypeProfil] ([CodeTypeProfil], [Libelle], [LibelleCourt]) VALUES (4, N'Période de professionnalisation', N'PP   ')
INSERT [dbo].[TypeProfil] ([CodeTypeProfil], [Libelle], [LibelleCourt]) VALUES (5, N'Convention de stage', N'CS   ')
SET IDENTITY_INSERT [dbo].[TypeProfil] OFF
/****** Object:  Table [dbo].[TypeLien]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeLien](
	[CodeTypeLien] [char](5) NOT NULL,
	[Libelle] [varchar](200) NULL,
 CONSTRAINT [PK_TypeLien] PRIMARY KEY CLUSTERED 
(
	[CodeTypeLien] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'_____', N'<non spécifié>')
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'CDD  ', N'CDD')
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'CDI  ', N'CDI')
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'CP   ', N'Contrat de professionnalisation')
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'CS   ', N'Convention de Stage')
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'INT  ', N'INTERIM')
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'POSIT', N'Positionnement Contrat de Professionnalisation')
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'PP   ', N'Période de professionnalisation')
INSERT [dbo].[TypeLien] ([CodeTypeLien], [Libelle]) VALUES (N'STG  ', N'STAGE')
/****** Object:  Table [dbo].[TypeEvenement]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeEvenement](
	[CodeTypeEvenement] [char](10) NOT NULL,
	[Libelle] [varchar](200) NULL,
	[CodeNature] [char](10) NOT NULL,
 CONSTRAINT [PK_TypeEvenement] PRIMARY KEY CLUSTERED 
(
	[CodeTypeEvenement] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'par exemple Envoi Doc, ...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TypeEvenement'
GO
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'AAAA      ', N'Administratif', N'ADM       ')
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'CAL       ', N'Suivi', N'ETS       ')
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'CALENDRIER', N'Création calendrier', N'PEDAGOGIE ')
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'CONV      ', N'Envoi de convention', N'ETS       ')
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'CV        ', N'Envoi de cv', N'ETS       ')
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'DEM-CP    ', N'Demande d''un contrat de professionnalisation', N'ETS       ')
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'DEM-EMP   ', N'Contact pour emploi', N'ETS       ')
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'DEM-PP    ', N'Demande d''une période de professionnalisation', N'ETS       ')
INSERT [dbo].[TypeEvenement] ([CodeTypeEvenement], [Libelle], [CodeNature]) VALUES (N'DEM-ST    ', N'Demande de stage', N'ETS       ')
/****** Object:  Table [dbo].[TypeEntreprise]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeEntreprise](
	[CodeTypeEntreprise] [char](5) NOT NULL,
	[Libelle] [varchar](200) NOT NULL,
 CONSTRAINT [PK_TypeEntreprise] PRIMARY KEY CLUSTERED 
(
	[CodeTypeEntreprise] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'_____', N'<non spécifié>')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'ASSUR', N'Assurances')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'BANK ', N'Banque')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'BTP  ', N'Bâtiment-Travaux Publics')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'COM  ', N'Entreprise commerciale')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'DISTR', N'Grande Distribution')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'EDL  ', N'Editeur Logiciel')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'ETS  ', N'Etablissements scolaires')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'EXPCO', N'EXPERTS COMPTABLES ET COMMISAIRES AUX COMPTES')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'HOP  ', N'Hôpital ou clinique')
INSERT [dbo].[TypeEntreprise] ([CodeTypeEntreprise], [Libelle]) VALUES (N'INT  ', N'Agences Intérim')
/****** Object:  Table [dbo].[Titre]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Titre](
	[CodeTitre] [char](8) NOT NULL,
	[LibelleCourt] [varchar](50) NOT NULL,
	[LibelleLong] [varchar](200) NULL,
	[DateCreation] [datetime] NOT NULL,
	[TitreENI] [bit] NOT NULL,
	[Archiver] [bit] NOT NULL,
	[DateModif] [timestamp] NULL,
	[niveau] [varchar](5) NULL,
	[codeRome] [varchar](20) NULL,
	[codeNSF] [varchar](20) NULL,
 CONSTRAINT [PK_Titre] PRIMARY KEY CLUSTERED 
(
	[CodeTitre] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notifie le niveau diplomant associé au titre' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Titre', @level2type=N'COLUMN',@level2name=N'niveau'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notifie le codeRome associé au titre' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Titre', @level2type=N'COLUMN',@level2name=N'codeRome'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notifie le codeRome associé au titre' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Titre', @level2type=N'COLUMN',@level2name=N'codeNSF'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Définir les titres décernés par les formations de l''ENI' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Titre'
GO
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'AL      ', N'ARCHITECTE LOGICIEL', N'Architecte Logiciel', CAST(0x00009FB100000000 AS DateTime), 0, 0, N'I', NULL, NULL)
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'ASR     ', N'ASR', N'Administrateur Système et Réseau', CAST(0x00009ED800000000 AS DateTime), 0, 0, N'II', NULL, NULL)
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'CDI     ', N'CDI', N'Concepteur Développeur Informatique', CAST(0x00009E9B00000000 AS DateTime), 0, 0, N'II', NULL, NULL)
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'DL      ', N'DL', N'Développeur Logiciel', CAST(0x00009E9B00000000 AS DateTime), 0, 0, N'III', NULL, NULL)
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'EISI    ', N'EISI', N'Expert en Informatique et Système d''Information', CAST(0x00009FB100000000 AS DateTime), 0, 0, N'I', NULL, NULL)
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'TSRIT   ', N'TSRIT', N'Technicien Supérieur en Réseaux Informatiques et Télécommunications', CAST(0x00009ED800000000 AS DateTime), 0, 0, NULL, NULL, NULL)
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'TSSI    ', N'TSSI', N'Technicien Supérieur de Support en Informatique', CAST(0x00009ED700000000 AS DateTime), 0, 0, N'III', NULL, NULL)
/****** Object:  Table [dbo].[Salle]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Salle](
	[CodeSalle] [varchar](5) NOT NULL,
	[Libelle] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Salle] PRIMARY KEY CLUSTERED 
(
	[CodeSalle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'000', N'Salle Réunion')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'001', N'Salle 001')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'002', N'Salle 002')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'003', N'Salle 003')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'101', N'Salle 101')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'102', N'Salle 102')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'104', N'Salle 104')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'105', N'Salle 105')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'201', N'Salle 201')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'202', N'Salle 202')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'204', N'Salle 204')
INSERT [dbo].[Salle] ([CodeSalle], [Libelle]) VALUES (N'205', N'Salle 205')

/****** Object:  Table [dbo].[ContactENI]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactENI](
	[CodeContactENI] [int] IDENTITY(1,1) NOT NULL,
	[Identite] [varchar](50) NOT NULL,
	[ModeNotification] [int] NOT NULL,
	[CodeDomaine] [varchar](5) NOT NULL,
 CONSTRAINT [PK_ContactENI] PRIMARY KEY CLUSTERED 
(
	[CodeContactENI] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[ContactENI] ON
INSERT [dbo].[ContactENI] ([CodeContactENI], [Identite], [ModeNotification], [CodeDomaine]) VALUES (1, N'Bonnart Sophie', 0, N'ADMIN')
INSERT [dbo].[ContactENI] ([CodeContactENI], [Identite], [ModeNotification], [CodeDomaine]) VALUES (3, N'BIERLING Laure', 0, N'ADMIN')
INSERT [dbo].[ContactENI] ([CodeContactENI], [Identite], [ModeNotification], [CodeDomaine]) VALUES (7, N'GABILLAUD Jerôme', 0, N'PEDA')
INSERT [dbo].[ContactENI] ([CodeContactENI], [Identite], [ModeNotification], [CodeDomaine]) VALUES (16, N'Leduc Sandra', 0, N'ADMIN')
INSERT [dbo].[ContactENI] ([CodeContactENI], [Identite], [ModeNotification], [CodeDomaine]) VALUES (18, N'Gaga Lady', 0, N'PEDA')
INSERT [dbo].[ContactENI] ([CodeContactENI], [Identite], [ModeNotification], [CodeDomaine]) VALUES (21, N'Kennedy John', 0, N'PEDA')
SET IDENTITY_INSERT [dbo].[ContactENI] OFF

/****** Object:  Table [dbo].[Fonction]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fonction](
	[CodeFonction] [char](5) NOT NULL,
	[Libelle] [varchar](100) NULL,
 CONSTRAINT [PK_Fonction] PRIMARY KEY CLUSTERED 
(
	[CodeFonction] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'ADMEX', N'Administrateur Exploitation')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'ADMIN', N'Administrateur Réseau')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'AL   ', N'Architecte Logiciel')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'AP   ', N'Concepteur Développeur')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'ARH  ', N'Assistante RH')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'ASS  ', N'Assistante Administrative')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'COM  ', N'COMPTABLE')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'COMM ', N'Commercial')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'DAF  ', N'DIRECTEUR ADMINISTRATIF ET FINANCIER')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'DEP  ', N'Technicien Déploiement')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'DIR  ', N'Directeur')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'DL   ', N'Développeur Logiciel')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'DRH  ', N'DRH')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'EISI ', N'Expert en Informatique et Système d''Information')
INSERT [dbo].[Fonction] ([CodeFonction], [Libelle]) VALUES (N'TUT  ', N'Tuteur en entreprise')

/****** Object:  Table [dbo].[Module]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Module](
	[Libelle] [varchar](200) NOT NULL,
	[DureeEnHeures] [smallint] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DureeEnSemaines] [tinyint] NOT NULL,
	[PrixPublicEnCours] [float] NOT NULL,
	[LibelleCourt] [varchar](20) NOT NULL,
	[IdModule] [int] IDENTITY(1,1) NOT NULL,
	[DateModif] [timestamp] NULL,
	[Archiver] [bit] NOT NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[IdModule] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Module] ON
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Utilisation en mode commande d''un système Linux', 35, CAST(0x00009F1700000000 AS DateTime), 1, 0, N'UNIX-UTILISATION', 62, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Notions de bases sur les réseaux - Préparation au CCNA 1', 70, CAST(0x00009F1700000000 AS DateTime), 2, 0, N'CISCO-1', 63, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Cours et Ateliers Administration Système Windows Server 2008', 35, CAST(0x00009F1700000000 AS DateTime), 1, 0, N'ADM-2008', 64, 1)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'TP synthèse Administration Système 2008', 35, CAST(0x00009F1700000000 AS DateTime), 1, 0, N'ADM-2008-TP', 65, 1)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Cours et ateliers Administration Systèmes Linux', 70, CAST(0x00009F1700000000 AS DateTime), 2, 0, N'ADM-LINUX', 66, 1)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Active Directory', 35, CAST(0x00009FB200000000 AS DateTime), 1, 0, N'AL-AD2008', 130, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Java (framework)', 70, CAST(0x00009FB200000000 AS DateTime), 2, 0, N'AL-J2E', 131, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Langage C#', 70, CAST(0x00009FB200000000 AS DateTime), 2, 0, N'AL-C#', 132, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Projet C#', 35, CAST(0x00009FB200000000 AS DateTime), 1, 0, N'AL-TPC#', 133, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Rappels sur la conduite de projet et présentation projet', 35, CAST(0x00009FB200000000 AS DateTime), 1, 0, N'AL-CP1', 134, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'ITIL et méthode SCRUM', 35, CAST(0x00009FB200000000 AS DateTime), 1, 0, N'AL-ITIL', 135, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Urbanisation du système d''information', 35, CAST(0x00009FB200000000 AS DateTime), 1, 0, N'AL-SOA', 136, 0)
INSERT [dbo].[Module] ([Libelle], [DureeEnHeures], [DateCreation], [DureeEnSemaines], [PrixPublicEnCours], [LibelleCourt], [IdModule], [Archiver]) VALUES (N'Présentation du projet ', 35, CAST(0x00009FB200000000 AS DateTime), 1, 0, N'AL-CP2', 137, 0)
SET IDENTITY_INSERT [dbo].[Module] OFF

/****** Object:  Table [dbo].[Formation]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Formation](
	[CodeFormation] [char](8) NOT NULL,
	[LibelleLong] [varchar](200) NOT NULL,
	[LibelleCourt] [varchar](50) NOT NULL,
	[DureeEnHeures] [smallint] NOT NULL,
	[TauxHoraire] [float] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[CodeTitre] [char](8) NULL,
	[PrixPublicEnCours] [float] NOT NULL,
	[HeuresCentre] [smallint] NULL,
	[HeuresStage] [smallint] NULL,
	[SemainesCentre] [tinyint] NULL,
	[SemainesStage] [tinyint] NULL,
	[DureeEnSemaines] [tinyint] NOT NULL,
	[DateModif] [timestamp] NOT NULL,
	[Archiver] [bit] NOT NULL,
 CONSTRAINT [PK_Formation] PRIMARY KEY CLUSTERED 
(
	[CodeFormation] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Formation] ([CodeFormation], [LibelleLong], [LibelleCourt], [DureeEnHeures], [TauxHoraire], [DateCreation], [CodeTitre], [PrixPublicEnCours], [HeuresCentre], [HeuresStage], [SemainesCentre], [SemainesStage], [DureeEnSemaines], [Archiver]) VALUES (N'AL      ', N'Architeccte logiciel', N'AL', 5, 5, CAST(0x0000787C00000000 AS DateTime), N'AL      ', 5, NULL, NULL, NULL, NULL, 5, 0)

/****** Object:  Table [dbo].[Entreprise]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Entreprise](
	[CodeEntreprise] [int] IDENTITY(1,1) NOT NULL,
	[RaisonSociale] [varchar](255) NOT NULL,
	[Adresse1] [varchar](500) NULL,
	[Adresse2] [varchar](500) NULL,
	[Adresse3] [varchar](500) NULL,
	[CodePostal] [char](5) NULL,
	[Ville] [varchar](100) NULL,
	[Telephone] [char](14) NULL,
	[Fax] [char](14) NULL,
	[SiteWeb] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[Observation] [varchar](max) NULL,
	[CodeTypeEntreprise] [char](5) NOT NULL,
	[CodeRegion] [char](2) NOT NULL,
	[CodeSecteur] [int] NOT NULL,
	[CodeOrganisme] [int] NULL,
 CONSTRAINT [PK_Entreprise] PRIMARY KEY CLUSTERED 
(
	[CodeEntreprise] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Référence un secteur d''activité (table SecteursActivite) dans le cas où les contact de l''entreprise sont susceptibles d''être jurys pour des CQP de ce secteur.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Entreprise', @level2type=N'COLUMN',@level2name=N'CodeSecteur'
GO
SET IDENTITY_INSERT [dbo].[Entreprise] ON
INSERT [dbo].[Entreprise] ([CodeEntreprise], [RaisonSociale], [Adresse1], [Adresse2], [Adresse3], [CodePostal], [Ville], [Telephone], [Fax], [SiteWeb], [Email], [Observation], [CodeTypeEntreprise], [CodeRegion], [CodeSecteur], [CodeOrganisme]) VALUES (9066, N'Capgemini', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'INT  ', N'1 ', 0, NULL)
SET IDENTITY_INSERT [dbo].[Entreprise] OFF
/****** Object:  Table [dbo].[ProfilStagiaire]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProfilStagiaire](
	[CodeStagiaire] [int] NOT NULL,
	[DateCreation] [datetime] NULL,
	[DateModification] [timestamp] NOT NULL,
	[CodeContactEni] [int] NULL,
	[TagEtude] [varchar](max) NULL,
	[TagExperience] [varchar](max) NULL,
	[TagTechnique] [varchar](max) NULL,
	[TagDivers] [varchar](max) NULL,
	[Observation] [varchar](max) NULL,
	[CodeStatut] [char](2) NULL,
	[CodeAllocation] [char](2) NULL,
	[EstEnRecherche] [bit] NOT NULL,
	[DateDisponibilite] [smalldatetime] NULL,
 CONSTRAINT [PK_ProfilStagiaire] PRIMARY KEY CLUSTERED 
(
	[CodeStagiaire] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notifie si le candidat est en recherche d''entreprise.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProfilStagiaire', @level2type=N'COLUMN',@level2name=N'EstEnRecherche'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notifie la date à partir de laquelle on peut proposer ce candidat aux entreprises' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProfilStagiaire', @level2type=N'COLUMN',@level2name=N'DateDisponibilite'
GO

/****** Object:  Table [dbo].[UniteParFormation]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UniteParFormation](
	[CodeFormation] [char](8) NOT NULL,
	[Position] [tinyint] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUniteFormation] [int] NOT NULL,
 CONSTRAINT [PK_UniteParFormation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[UniteParFormation] ON
INSERT [dbo].[UniteParFormation] ([CodeFormation], [Position], [Id], [IdUniteFormation]) VALUES (N'AL      ', 0, 119, 77)
INSERT [dbo].[UniteParFormation] ([CodeFormation], [Position], [Id], [IdUniteFormation]) VALUES (N'AL      ', 1, 120, 80)
INSERT [dbo].[UniteParFormation] ([CodeFormation], [Position], [Id], [IdUniteFormation]) VALUES (N'AL      ', 2, 121, 81)
SET IDENTITY_INSERT [dbo].[UniteParFormation] OFF
/****** Object:  Table [dbo].[StagiaireParEntreprise]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StagiaireParEntreprise](
	[CodeStagiaire] [int] NOT NULL,
	[CodeEntreprise] [int] NOT NULL,
	[DateLien] [datetime] NOT NULL,
	[CodeTypeLien] [char](5) NOT NULL,
	[DateDebutEnEts] [datetime] NULL,
	[DateFinEnEts] [datetime] NULL,
	[CodeFonction] [char](5) NULL,
	[Commentaire] [nvarchar](max) NULL,
	[NumLien] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_StagiaireParEntreprise] PRIMARY KEY CLUSTERED 
(
	[NumLien] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Champs de saisie libre pour ajouter des informations additionnelles sur le lien en question.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StagiaireParEntreprise', @level2type=N'COLUMN',@level2name=N'Commentaire'
GO
SET IDENTITY_INSERT [dbo].[StagiaireParEntreprise] ON
INSERT [dbo].[StagiaireParEntreprise] ([CodeStagiaire], [CodeEntreprise], [DateLien], [CodeTypeLien], [DateDebutEnEts], [DateFinEnEts], [CodeFonction], [Commentaire], [NumLien]) VALUES (818, 9066, CAST(0x0000A06800000000 AS DateTime), N'PP   ', NULL, NULL, N'AL   ', NULL, 515)
INSERT [dbo].[StagiaireParEntreprise] ([CodeStagiaire], [CodeEntreprise], [DateLien], [CodeTypeLien], [DateDebutEnEts], [DateFinEnEts], [CodeFonction], [Commentaire], [NumLien]) VALUES (735, 9066, CAST(0x0000A06800000000 AS DateTime), N'PP   ', NULL, NULL, N'AL   ', NULL, 518)
SET IDENTITY_INSERT [dbo].[StagiaireParEntreprise] OFF
/****** Object:  Table [dbo].[Promotion]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Promotion](
	[CodePromotion] [char](8) NOT NULL,
	[Libelle] [varchar](200) NOT NULL,
	[Debut] [datetime] NOT NULL,
	[Fin] [datetime] NOT NULL,
	[CodeFormation] [char](8) NOT NULL,
	[PrixPublicAffecte] [float] NOT NULL,
	[DateModif] [timestamp] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[PrixPECAffecte] [float] NOT NULL,
	[PrixFinanceAffecte] [float] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[CodePromotion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prix sans prise en charge' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promotion', @level2type=N'COLUMN',@level2name=N'PrixPublicAffecte'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prix restant à la charge du stagiaire' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promotion', @level2type=N'COLUMN',@level2name=N'PrixPECAffecte'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prix pris en charge par la région' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promotion', @level2type=N'COLUMN',@level2name=N'PrixFinanceAffecte'
GO
INSERT [dbo].[Promotion] ([CodePromotion], [Libelle], [Debut], [Fin], [CodeFormation], [PrixPublicAffecte], [DateCreation], [PrixPECAffecte], [PrixFinanceAffecte]) VALUES (N'AL1     ', N'architecte logiciels 1', CAST(0x0000787C00000000 AS DateTime), CAST(0x0000787C00000000 AS DateTime), N'AL      ', 50, CAST(0x0000787C00000000 AS DateTime), 50, 50)
INSERT [dbo].[Promotion] ([CodePromotion], [Libelle], [Debut], [Fin], [CodeFormation], [PrixPublicAffecte], [DateCreation], [PrixPECAffecte], [PrixFinanceAffecte]) VALUES (N'AL2     ', N'architecte logiciels 2', CAST(0x0000787C00000000 AS DateTime), CAST(0x0000787C00000000 AS DateTime), N'AL      ', 50, CAST(0x0000787C00000000 AS DateTime), 50, 50)
/****** Object:  Table [dbo].[Contact]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contact](
	[CodeContact] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Prenom] [varchar](50) NULL,
	[TelFixe] [char](14) NULL,
	[TelMobile] [char](14) NULL,
	[Fax] [char](14) NULL,
	[Email] [varchar](50) NULL,
	[CodeFonction] [char](5) NOT NULL,
	[Observation] [varchar](1000) NULL,
	[CodeEntreprise] [int] NULL,
	[CodeImportance] [int] NOT NULL,
	[Archive] [bit] NOT NULL,
	[Civilite] [varchar](4) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[CodeContact] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nom  du contact. ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'Nom'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'N° de téléphone fixe. Si null, le n° de protable doit être renseigné.
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'TelFixe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'N° de téléphone portable. Si null, le n° de fixe doit être renseigné.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'TelMobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Réference ver l''entreprise d''appartenance du contact. Null si contact hors entreprise (retraité, free lance, chomeur...)
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'CodeEntreprise'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Le contact est obsolète (1) ou courant (0). Nécessaire pour indiquer quelle occurence d''un contact est celle d''actualité, une personne ayant changé d''entreprise faisant l''objet de plusieurs fiches contact.
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'Archive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contacts en entreprise (ou hors entreprise si retraités) .' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact'
GO
SET IDENTITY_INSERT [dbo].[Contact] ON
INSERT [dbo].[Contact] ([CodeContact], [Nom], [Prenom], [TelFixe], [TelMobile], [Fax], [Email], [CodeFonction], [Observation], [CodeEntreprise], [CodeImportance], [Archive], [Civilite]) VALUES (9833, N'Beta', N'Test', N'0202020202    ', N'0602020202    ', N'0202020202    ', N'betatest@gmail.com', N'TUT  ', NULL, 9066, 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[Contact] OFF

/****** Object:  Table [dbo].[ModuleParUnite]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleParUnite](
	[Position] [tinyint] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUnite] [int] NOT NULL,
	[IdModule] [int] NOT NULL,
 CONSTRAINT [PK_ModuleParUnite] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ModuleParUnite] ON
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (0, 404, 119, 62)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (1, 405, 119, 63)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (2, 406, 119, 66)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (3, 407, 119, 64)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (4, 408, 119, 65)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (5, 409, 119, 130)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (0, 412, 120, 131)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (1, 413, 120, 132)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (2, 414, 120, 133)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (0, 419, 121, 134)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (1, 420, 121, 135)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (2, 421, 121, 136)
INSERT [dbo].[ModuleParUnite] ([Position], [Id], [IdUnite], [IdModule]) VALUES (3, 422, 121, 137)
SET IDENTITY_INSERT [dbo].[ModuleParUnite] OFF
/****** Object:  Table [dbo].[Evenement]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Evenement](
	[CodeEvenement] [int] IDENTITY(1,1) NOT NULL,
	[Commentaire] [varchar](max) NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateRappel] [datetime] NULL,
	[DateTraitement] [datetime] NULL,
	[CodeContactENI] [int] NOT NULL,
	[CodeTypeEvenement] [char](10) NOT NULL,
	[CodeStagiaire] [int] NULL,
	[CodeContact] [int] NULL,
	[EvenementParent] [int] NULL,
	[FilsPresents] [bit] NOT NULL,
	[CodeEntreprise] [int] NULL,
 CONSTRAINT [PK_Evenement] PRIMARY KEY CLUSTERED 
(
	[CodeEvenement] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indique si des événements liés existent (1). Utilisé pour indiquer qu''un événement entreprise correspond à n événements stagiaires par exemple.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Evenement', @level2type=N'COLUMN',@level2name=N'FilsPresents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Référence la ligne Entreprise éventuellement concernée par la ligne Evenement.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Evenement', @level2type=N'COLUMN',@level2name=N'CodeEntreprise'
GO
SET IDENTITY_INSERT [dbo].[Evenement] ON
INSERT [dbo].[Evenement] ([CodeEvenement], [Commentaire], [DateCreation], [DateRappel], [DateTraitement], [CodeContactENI], [CodeTypeEvenement], [CodeStagiaire], [CodeContact], [EvenementParent], [FilsPresents], [CodeEntreprise]) VALUES (5557, N'Msg laissé', CAST(0x0000A0C500000000 AS DateTime), CAST(0x0000A0D000000000 AS DateTime), NULL, 21, N'CAL       ', 2020, NULL, NULL, 0, 9066)
INSERT [dbo].[Evenement] ([CodeEvenement], [Commentaire], [DateCreation], [DateRappel], [DateTraitement], [CodeContactENI], [CodeTypeEvenement], [CodeStagiaire], [CodeContact], [EvenementParent], [FilsPresents], [CodeEntreprise]) VALUES (5565, N'Je renvoie une propo de calendrier ', CAST(0x0000A0C500000000 AS DateTime), NULL, NULL, 18, N'DEM-CP    ', 735, 9833, NULL, 0, 9066)
INSERT [dbo].[Evenement] ([CodeEvenement], [Commentaire], [DateCreation], [DateRappel], [DateTraitement], [CodeContactENI], [CodeTypeEvenement], [CodeStagiaire], [CodeContact], [EvenementParent], [FilsPresents], [CodeEntreprise]) VALUES (5571, N'envoyé cv ', CAST(0x0000A0C500000000 AS DateTime), NULL, NULL, 7, N'CV        ', NULL, 9833, NULL, 1, 9066)
SET IDENTITY_INSERT [dbo].[Evenement] OFF
/****** Object:  Table [dbo].[Cours]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cours](
	[Debut] [datetime] NOT NULL,
	[Fin] [datetime] NOT NULL,
	[DureeReelleEnHeures] [smallint] NOT NULL,
	[CodePromotion] [char](8) NOT NULL,
	[IdCours] [uniqueidentifier] NOT NULL,
	[CodeSalle] [varchar](5) NULL,
	[CodeFormateur] [int] NULL,
	[PrixPublicAffecte] [float] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModif] [timestamp] NOT NULL,
	[IdModule] [int] NOT NULL,
	[LibelleCours] [varchar](20) NOT NULL,
	[DureePrevueEnHeures] [smallint] NOT NULL,
 CONSTRAINT [PK_Cours] PRIMARY KEY CLUSTERED 
(
	[IdCours] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Duree en heures prévues - les jours fériés éventuels' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cours', @level2type=N'COLUMN',@level2name=N'DureeReelleEnHeures'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prix sans prise en charge' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cours', @level2type=N'COLUMN',@level2name=N'PrixPublicAffecte'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Duree standard du cours. Issue du Module.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cours', @level2type=N'COLUMN',@level2name=N'DureePrevueEnHeures'
GO
INSERT [dbo].[Cours] ([Debut], [Fin], [DureeReelleEnHeures], [CodePromotion], [IdCours], [CodeSalle], [CodeFormateur], [PrixPublicAffecte], [DateCreation], [IdModule], [LibelleCours], [DureePrevueEnHeures]) VALUES (CAST(0x0000787C00000000 AS DateTime), CAST(0x0000787D00000000 AS DateTime), 5, N'AL1     ', N'1fdfe86b-befa-4150-8701-03bc8e6dd93b', N'001', NULL, 100, CAST(0x0000787D00000000 AS DateTime), 135, N'test libellé cours', 500)
INSERT [dbo].[Cours] ([Debut], [Fin], [DureeReelleEnHeures], [CodePromotion], [IdCours], [CodeSalle], [CodeFormateur], [PrixPublicAffecte], [DateCreation], [IdModule], [LibelleCours], [DureePrevueEnHeures]) VALUES (CAST(0x0000787C00000000 AS DateTime), CAST(0x0000787D00000000 AS DateTime), 5, N'AL1     ', N'133b73f9-4630-4f16-be7a-0469c31a1e62', N'001', NULL, 100, CAST(0x0000787D00000000 AS DateTime), 135, N'test libellé cours', 500)
INSERT [dbo].[Cours] ([Debut], [Fin], [DureeReelleEnHeures], [CodePromotion], [IdCours], [CodeSalle], [CodeFormateur], [PrixPublicAffecte], [DateCreation], [IdModule], [LibelleCours], [DureePrevueEnHeures]) VALUES (CAST(0x0000787C00000000 AS DateTime), CAST(0x0000787D00000000 AS DateTime), 5, N'AL1     ', N'60bd6a8f-cdc5-4e0e-8ff1-1e63b8c17704', N'001', NULL, 100, CAST(0x0000787D00000000 AS DateTime), 135, N'test libellé cours', 500)
INSERT [dbo].[Cours] ([Debut], [Fin], [DureeReelleEnHeures], [CodePromotion], [IdCours], [CodeSalle], [CodeFormateur], [PrixPublicAffecte], [DateCreation], [IdModule], [LibelleCours], [DureePrevueEnHeures]) VALUES (CAST(0x0000787C00000000 AS DateTime), CAST(0x0000787D00000000 AS DateTime), 5, N'AL1     ', N'634784c5-f180-4723-b70c-31b6e00ec2b9', N'001', NULL, 100, CAST(0x0000787D00000000 AS DateTime), 135, N'test libellé cours', 500)
INSERT [dbo].[Cours] ([Debut], [Fin], [DureeReelleEnHeures], [CodePromotion], [IdCours], [CodeSalle], [CodeFormateur], [PrixPublicAffecte], [DateCreation], [IdModule], [LibelleCours], [DureePrevueEnHeures]) VALUES (CAST(0x0000787C00000000 AS DateTime), CAST(0x0000787D00000000 AS DateTime), 5, N'AL1     ', N'b56586fd-ba84-4ea4-972d-6d3ed06c1605', N'001', NULL, 100, CAST(0x0000787D00000000 AS DateTime), 135, N'test libellé cours', 500)
INSERT [dbo].[Cours] ([Debut], [Fin], [DureeReelleEnHeures], [CodePromotion], [IdCours], [CodeSalle], [CodeFormateur], [PrixPublicAffecte], [DateCreation], [IdModule], [LibelleCours], [DureePrevueEnHeures]) VALUES (CAST(0x0000787C00000000 AS DateTime), CAST(0x0000787D00000000 AS DateTime), 5, N'AL1     ', N'0d99c5da-38a1-4c8a-a1cd-900e83d3e0cc', N'001', NULL, 100, CAST(0x0000787D00000000 AS DateTime), 135, N'test libellé cours', 500)
/****** Object:  Table [dbo].[PlanningIndividuelFormation]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlanningIndividuelFormation](
	[CodePlanning] [int] IDENTITY(1,1) NOT NULL,
	[CodeStagiaire] [int] NOT NULL,
	[DateInscription] [datetime] NULL,
	[DateCreation] [datetime] NULL,
	[CodeTypeProfil] [int] NOT NULL,
	[CodeFormation] [char](8) NULL,
	[CodePromotion] [char](8) NULL,
	[DateModif] [timestamp] NULL,
	[NumLien] [int] NULL,
 CONSTRAINT [PK_PlanningIndividuelFormation] PRIMARY KEY CLUSTERED 
(
	[CodePlanning] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Element central de la vie du stagiaire à l''ENI.
Le planning individuel représente le cursus du stagiaire. Ce cursus peut être lié à une promotion (formation continue), à une formation(contrat pro -> dans ce cas à la charge de l''administratif de définir le cursus exact), à des cours indépendants (Module  -> dans ce cas à la charge de l''administratif de définir le cursus exact).
Tant que ce planning n''est pas validé par une inscription, la participation des stagiaires au cours n''est pas confirmée.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanningIndividuelFormation'
GO
SET IDENTITY_INSERT [dbo].[PlanningIndividuelFormation] ON
INSERT [dbo].[PlanningIndividuelFormation] ([CodePlanning], [CodeStagiaire], [DateInscription], [DateCreation], [CodeTypeProfil], [CodeFormation], [CodePromotion], [NumLien]) VALUES (1645, 818, CAST(0x0000A06800000000 AS DateTime), CAST(0x0000A06800000000 AS DateTime), 4, N'AL      ', N'AL1     ', 515)
INSERT [dbo].[PlanningIndividuelFormation] ([CodePlanning], [CodeStagiaire], [DateInscription], [DateCreation], [CodeTypeProfil], [CodeFormation], [CodePromotion], [NumLien]) VALUES (1646, 2020, CAST(0x0000A06800000000 AS DateTime), CAST(0x0000A06800000000 AS DateTime), 4, N'AL      ', N'AL1     ', 515)
INSERT [dbo].[PlanningIndividuelFormation] ([CodePlanning], [CodeStagiaire], [DateInscription], [DateCreation], [CodeTypeProfil], [CodeFormation], [CodePromotion], [NumLien]) VALUES (1647, 735, CAST(0x0000A06800000000 AS DateTime), CAST(0x0000A06800000000 AS DateTime), 4, N'AL      ', N'AL1     ', 515)
SET IDENTITY_INSERT [dbo].[PlanningIndividuelFormation] OFF
/****** Object:  Table [dbo].[PlanningIndividuelDetail]    Script Date: 10/27/2012 11:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanningIndividuelDetail](
	[CodePlanning] [int] NOT NULL,
	[IdCours] [uniqueidentifier] NOT NULL,
	[PrixCoursDevis] [float] NOT NULL,
	[PrixCoursPECDevis] [float] NULL,
	[PrixCoursFinanceDevis] [float] NULL,
	[Dispense] [bit] NOT NULL,
	[Inscrit] [bit] NOT NULL,
	[DebutCours] [datetime] NULL,
	[FinCours] [datetime] NULL,
	[HeuresReellesCours] [smallint] NULL,
 CONSTRAINT [PK_PlanningIndividuelDetail] PRIMARY KEY CLUSTERED 
(
	[CodePlanning] ASC,
	[IdCours] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[PlanningIndividuelDetail] ([CodePlanning], [IdCours], [PrixCoursDevis], [PrixCoursPECDevis], [PrixCoursFinanceDevis], [Dispense], [Inscrit], [DebutCours], [FinCours], [HeuresReellesCours]) VALUES (1645, N'1fdfe86b-befa-4150-8701-03bc8e6dd93b', 0, 0, 0, 0, 1, NULL, NULL, NULL)
INSERT [dbo].[PlanningIndividuelDetail] ([CodePlanning], [IdCours], [PrixCoursDevis], [PrixCoursPECDevis], [PrixCoursFinanceDevis], [Dispense], [Inscrit], [DebutCours], [FinCours], [HeuresReellesCours]) VALUES (1645, N'133b73f9-4630-4f16-be7a-0469c31a1e62', 0, 0, 0, 0, 1, NULL, NULL, NULL)
INSERT [dbo].[PlanningIndividuelDetail] ([CodePlanning], [IdCours], [PrixCoursDevis], [PrixCoursPECDevis], [PrixCoursFinanceDevis], [Dispense], [Inscrit], [DebutCours], [FinCours], [HeuresReellesCours]) VALUES (1645, N'60bd6a8f-cdc5-4e0e-8ff1-1e63b8c17704', 0, 0, 0, 0, 1, NULL, NULL, NULL)
INSERT [dbo].[PlanningIndividuelDetail] ([CodePlanning], [IdCours], [PrixCoursDevis], [PrixCoursPECDevis], [PrixCoursFinanceDevis], [Dispense], [Inscrit], [DebutCours], [FinCours], [HeuresReellesCours]) VALUES (1645, N'634784c5-f180-4723-b70c-31b6e00ec2b9', 0, 0, 0, 0, 1, NULL, NULL, NULL)
INSERT [dbo].[PlanningIndividuelDetail] ([CodePlanning], [IdCours], [PrixCoursDevis], [PrixCoursPECDevis], [PrixCoursFinanceDevis], [Dispense], [Inscrit], [DebutCours], [FinCours], [HeuresReellesCours]) VALUES (1645, N'b56586fd-ba84-4ea4-972d-6d3ed06c1605', 0, 0, 0, 0, 1, NULL, NULL, NULL)
INSERT [dbo].[PlanningIndividuelDetail] ([CodePlanning], [IdCours], [PrixCoursDevis], [PrixCoursPECDevis], [PrixCoursFinanceDevis], [Dispense], [Inscrit], [DebutCours], [FinCours], [HeuresReellesCours]) VALUES (1645, N'0d99c5da-38a1-4c8a-a1cd-900e83d3e0cc', 0, 0, 0, 0, 1, NULL, NULL, NULL)
/****** Object:  Default [DF_Contact_CodeFonction]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_CodeFonction]  DEFAULT ('_____') FOR [CodeFonction]
GO
/****** Object:  Default [DF_Contact_CodeImportance]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_CodeImportance]  DEFAULT ((3)) FOR [CodeImportance]
GO
/****** Object:  Default [DF_Contact_Archive]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_Archive]  DEFAULT ((0)) FOR [Archive]
GO
/****** Object:  Default [DF_ContactENI_ModeNotification]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ContactENI] ADD  CONSTRAINT [DF_ContactENI_ModeNotification]  DEFAULT ((0)) FOR [ModeNotification]
GO
/****** Object:  Default [DF_Cours_DateCreation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Cours] ADD  CONSTRAINT [DF_Cours_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
/****** Object:  Default [DF_Entreprise_CodeTypeEntreprise]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Entreprise] ADD  CONSTRAINT [DF_Entreprise_CodeTypeEntreprise]  DEFAULT ('_____') FOR [CodeTypeEntreprise]
GO
/****** Object:  Default [DF_Entreprise_CodeRegion]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Entreprise] ADD  CONSTRAINT [DF_Entreprise_CodeRegion]  DEFAULT ('__') FOR [CodeRegion]
GO
/****** Object:  Default [DF_Entreprise_CodeSecteur]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Entreprise] ADD  CONSTRAINT [DF_Entreprise_CodeSecteur]  DEFAULT ((0)) FOR [CodeSecteur]
GO
/****** Object:  Default [DF_Evenement_FilsPresents]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Evenement] ADD  CONSTRAINT [DF_Evenement_FilsPresents]  DEFAULT ((0)) FOR [FilsPresents]
GO
/****** Object:  Default [DF_Formation_DateCreation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Formation] ADD  CONSTRAINT [DF_Formation_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
/****** Object:  Default [DF_Formation_Archiver]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Formation] ADD  CONSTRAINT [DF_Formation_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO
/****** Object:  Default [DF_Module_DateCreation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
/****** Object:  Default [DF_Module_Archiver]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO
/****** Object:  Default [DF_ProfilStagiaire_CodeStatut]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ProfilStagiaire] ADD  CONSTRAINT [DF_ProfilStagiaire_CodeStatut]  DEFAULT ('__') FOR [CodeStatut]
GO
/****** Object:  Default [DF_ProfilStagiaire_CodeAllocation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ProfilStagiaire] ADD  CONSTRAINT [DF_ProfilStagiaire_CodeAllocation]  DEFAULT ('__') FOR [CodeAllocation]
GO
/****** Object:  Default [DF_ProfilStagiaire_EstEnRecherche]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ProfilStagiaire] ADD  CONSTRAINT [DF_ProfilStagiaire_EstEnRecherche]  DEFAULT ((0)) FOR [EstEnRecherche]
GO
/****** Object:  Default [DF_Promotion_DateCreation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Promotion] ADD  CONSTRAINT [DF_Promotion_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
/****** Object:  Default [DF_Stagiaire_CodeRegion]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_CodeRegion]  DEFAULT ('__') FOR [CodeRegion]
GO
/****** Object:  Default [DF_Stagiaire_CodeNationalite]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_CodeNationalite]  DEFAULT ('__') FOR [CodeNationalite]
GO
/****** Object:  Default [DF_Stagiaire_CodeOrigineMedia]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_CodeOrigineMedia]  DEFAULT ('__') FOR [CodeOrigineMedia]
GO
/****** Object:  Default [DF_Stagiaire_Permis]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_Permis]  DEFAULT ((0)) FOR [Permis]
GO
/****** Object:  Default [DF_Stagiaire_EnvoiDocEnCours]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_EnvoiDocEnCours]  DEFAULT ((0)) FOR [EnvoiDocEnCours]
GO
/****** Object:  Default [DF_Titre_DateCreation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Titre] ADD  CONSTRAINT [DF_Titre_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
/****** Object:  Default [DF_Titre_Archiver]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Titre] ADD  CONSTRAINT [DF_Titre_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO
/****** Object:  Default [DF_UniteFormation_DateCreation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[UniteFormation] ADD  CONSTRAINT [DF_UniteFormation_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
/****** Object:  Default [DF_UniteFormation_Archiver]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[UniteFormation] ADD  CONSTRAINT [DF_UniteFormation_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO
/****** Object:  Check [CK_Contact_Fixe_Mobile]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [CK_Contact_Fixe_Mobile] CHECK  (([TelFixe] IS NOT NULL OR [TelMobile] IS NOT NULL))
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [CK_Contact_Fixe_Mobile]
GO
/****** Object:  Check [CK_ContactENI]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ContactENI]  WITH CHECK ADD  CONSTRAINT [CK_ContactENI] CHECK  (([ModeNotification]=(2) OR [ModeNotification]=(1) OR [ModeNotification]=(0)))
GO
ALTER TABLE [dbo].[ContactENI] CHECK CONSTRAINT [CK_ContactENI]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactENI', @level2type=N'CONSTRAINT',@level2name=N'CK_ContactENI'
GO

/****** Object:  ForeignKey [FK_Contact_Entreprise]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Entreprise] FOREIGN KEY([CodeEntreprise])
REFERENCES [dbo].[Entreprise] ([CodeEntreprise])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Entreprise]
GO
/****** Object:  ForeignKey [FK_Contact_Fonction]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Fonction] FOREIGN KEY([CodeFonction])
REFERENCES [dbo].[Fonction] ([CodeFonction])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Fonction]
GO
/****** Object:  ForeignKey [FK_Cours_ContactENI]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Cours]  WITH CHECK ADD  CONSTRAINT [FK_Cours_ContactENI] FOREIGN KEY([CodeFormateur])
REFERENCES [dbo].[ContactENI] ([CodeContactENI])
GO
ALTER TABLE [dbo].[Cours] CHECK CONSTRAINT [FK_Cours_ContactENI]
GO
/****** Object:  ForeignKey [FK_Cours_Module]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Cours]  WITH CHECK ADD  CONSTRAINT [FK_Cours_Module] FOREIGN KEY([IdModule])
REFERENCES [dbo].[Module] ([IdModule])
GO
ALTER TABLE [dbo].[Cours] CHECK CONSTRAINT [FK_Cours_Module]
GO
/****** Object:  ForeignKey [FK_Cours_Promotion]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Cours]  WITH CHECK ADD  CONSTRAINT [FK_Cours_Promotion] FOREIGN KEY([CodePromotion])
REFERENCES [dbo].[Promotion] ([CodePromotion])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cours] CHECK CONSTRAINT [FK_Cours_Promotion]
GO
/****** Object:  ForeignKey [FK_Cours_Salle]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Cours]  WITH CHECK ADD  CONSTRAINT [FK_Cours_Salle] FOREIGN KEY([CodeSalle])
REFERENCES [dbo].[Salle] ([CodeSalle])
GO
ALTER TABLE [dbo].[Cours] CHECK CONSTRAINT [FK_Cours_Salle]
GO

/****** Object:  ForeignKey [FK_Entreprise_TypeEntreprise]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Entreprise]  WITH CHECK ADD  CONSTRAINT [FK_Entreprise_TypeEntreprise] FOREIGN KEY([CodeTypeEntreprise])
REFERENCES [dbo].[TypeEntreprise] ([CodeTypeEntreprise])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Entreprise] CHECK CONSTRAINT [FK_Entreprise_TypeEntreprise]
GO

/****** Object:  ForeignKey [FK_Evenement_ContactENI]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_ContactENI] FOREIGN KEY([CodeContactENI])
REFERENCES [dbo].[ContactENI] ([CodeContactENI])
GO
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_ContactENI]
GO
/****** Object:  ForeignKey [FK_Evenement_Entreprise]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Entreprise] FOREIGN KEY([CodeEntreprise])
REFERENCES [dbo].[Entreprise] ([CodeEntreprise])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Entreprise]
GO
/****** Object:  ForeignKey [FK_Evenement_Evenement]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Evenement] FOREIGN KEY([EvenementParent])
REFERENCES [dbo].[Evenement] ([CodeEvenement])
GO
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Evenement]
GO
/****** Object:  ForeignKey [FK_EvenementStagiaire_Contact]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_EvenementStagiaire_Contact] FOREIGN KEY([CodeContact])
REFERENCES [dbo].[Contact] ([CodeContact])
GO
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_EvenementStagiaire_Contact]
GO
/****** Object:  ForeignKey [FK_EvenementStagiaire_Stagiaire]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_EvenementStagiaire_Stagiaire] FOREIGN KEY([CodeStagiaire])
REFERENCES [dbo].[Stagiaire] ([CodeStagiaire])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_EvenementStagiaire_Stagiaire]
GO
/****** Object:  ForeignKey [FK_EvenementStagiaire_TypeEvenementStagiaire]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_EvenementStagiaire_TypeEvenementStagiaire] FOREIGN KEY([CodeTypeEvenement])
REFERENCES [dbo].[TypeEvenement] ([CodeTypeEvenement])
GO
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_EvenementStagiaire_TypeEvenementStagiaire]
GO
/****** Object:  ForeignKey [FK_Formation_Titre]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Formation]  WITH CHECK ADD  CONSTRAINT [FK_Formation_Titre] FOREIGN KEY([CodeTitre])
REFERENCES [dbo].[Titre] ([CodeTitre])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Formation] CHECK CONSTRAINT [FK_Formation_Titre]
GO
/****** Object:  ForeignKey [FK_ModuleParUnite_Module]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ModuleParUnite]  WITH CHECK ADD  CONSTRAINT [FK_ModuleParUnite_Module] FOREIGN KEY([IdModule])
REFERENCES [dbo].[Module] ([IdModule])
GO
ALTER TABLE [dbo].[ModuleParUnite] CHECK CONSTRAINT [FK_ModuleParUnite_Module]
GO
/****** Object:  ForeignKey [FK_ModuleParUnite_UniteParFormation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ModuleParUnite]  WITH CHECK ADD  CONSTRAINT [FK_ModuleParUnite_UniteParFormation] FOREIGN KEY([IdUnite])
REFERENCES [dbo].[UniteParFormation] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ModuleParUnite] CHECK CONSTRAINT [FK_ModuleParUnite_UniteParFormation]
GO
/****** Object:  ForeignKey [FK_PlanningIndividuelDetail_Cours]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[PlanningIndividuelDetail]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelDetail_Cours] FOREIGN KEY([IdCours])
REFERENCES [dbo].[Cours] ([IdCours])
GO
ALTER TABLE [dbo].[PlanningIndividuelDetail] CHECK CONSTRAINT [FK_PlanningIndividuelDetail_Cours]
GO
/****** Object:  ForeignKey [FK_PlanningIndividuelDetail_PlanningIndividuelFormation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[PlanningIndividuelDetail]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelDetail_PlanningIndividuelFormation] FOREIGN KEY([CodePlanning])
REFERENCES [dbo].[PlanningIndividuelFormation] ([CodePlanning])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlanningIndividuelDetail] CHECK CONSTRAINT [FK_PlanningIndividuelDetail_PlanningIndividuelFormation]
GO
/****** Object:  ForeignKey [FK_PlanningIndividuelFormation_Formation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_Formation] FOREIGN KEY([CodeFormation])
REFERENCES [dbo].[Formation] ([CodeFormation])
GO
ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_Formation]
GO
/****** Object:  ForeignKey [FK_PlanningIndividuelFormation_Promotion]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_Promotion] FOREIGN KEY([CodePromotion])
REFERENCES [dbo].[Promotion] ([CodePromotion])
GO
ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_Promotion]
GO
/****** Object:  ForeignKey [FK_PlanningIndividuelFormation_Stagiaire]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_Stagiaire] FOREIGN KEY([CodeStagiaire])
REFERENCES [dbo].[Stagiaire] ([CodeStagiaire])
GO
ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_Stagiaire]
GO
/****** Object:  ForeignKey [FK_PlanningIndividuelFormation_StagiaireParEntreprise]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_StagiaireParEntreprise] FOREIGN KEY([NumLien])
REFERENCES [dbo].[StagiaireParEntreprise] ([NumLien])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_StagiaireParEntreprise]
GO
/****** Object:  ForeignKey [FK_PlanningIndividuelFormation_TypeProfil]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_TypeProfil] FOREIGN KEY([CodeTypeProfil])
REFERENCES [dbo].[TypeProfil] ([CodeTypeProfil])
GO
ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_TypeProfil]
GO
/****** Object:  ForeignKey [FK_ProfilStagiaire_ContactENI]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ProfilStagiaire]  WITH CHECK ADD  CONSTRAINT [FK_ProfilStagiaire_ContactENI] FOREIGN KEY([CodeContactEni])
REFERENCES [dbo].[ContactENI] ([CodeContactENI])
GO
ALTER TABLE [dbo].[ProfilStagiaire] CHECK CONSTRAINT [FK_ProfilStagiaire_ContactENI]
GO
/****** Object:  ForeignKey [FK_ProfilStagiaire_Stagiaire]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[ProfilStagiaire]  WITH CHECK ADD  CONSTRAINT [FK_ProfilStagiaire_Stagiaire] FOREIGN KEY([CodeStagiaire])
REFERENCES [dbo].[Stagiaire] ([CodeStagiaire])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfilStagiaire] CHECK CONSTRAINT [FK_ProfilStagiaire_Stagiaire]
GO
/****** Object:  ForeignKey [FK_Promotion_Formation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Formation] FOREIGN KEY([CodeFormation])
REFERENCES [dbo].[Formation] ([CodeFormation])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Formation]
GO

/****** Object:  ForeignKey [FK_StagiaireParEntreprise_Entreprise]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[StagiaireParEntreprise]  WITH CHECK ADD  CONSTRAINT [FK_StagiaireParEntreprise_Entreprise] FOREIGN KEY([CodeEntreprise])
REFERENCES [dbo].[Entreprise] ([CodeEntreprise])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[StagiaireParEntreprise] CHECK CONSTRAINT [FK_StagiaireParEntreprise_Entreprise]
GO
/****** Object:  ForeignKey [FK_StagiaireParEntreprise_Fonction]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[StagiaireParEntreprise]  WITH CHECK ADD  CONSTRAINT [FK_StagiaireParEntreprise_Fonction] FOREIGN KEY([CodeFonction])
REFERENCES [dbo].[Fonction] ([CodeFonction])
GO
ALTER TABLE [dbo].[StagiaireParEntreprise] CHECK CONSTRAINT [FK_StagiaireParEntreprise_Fonction]
GO
/****** Object:  ForeignKey [FK_StagiaireParEntreprise_Stagiaire]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[StagiaireParEntreprise]  WITH CHECK ADD  CONSTRAINT [FK_StagiaireParEntreprise_Stagiaire] FOREIGN KEY([CodeStagiaire])
REFERENCES [dbo].[Stagiaire] ([CodeStagiaire])
GO
ALTER TABLE [dbo].[StagiaireParEntreprise] CHECK CONSTRAINT [FK_StagiaireParEntreprise_Stagiaire]
GO
/****** Object:  ForeignKey [FK_StagiaireParEntreprise_TypeLien]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[StagiaireParEntreprise]  WITH CHECK ADD  CONSTRAINT [FK_StagiaireParEntreprise_TypeLien] FOREIGN KEY([CodeTypeLien])
REFERENCES [dbo].[TypeLien] ([CodeTypeLien])
GO
ALTER TABLE [dbo].[StagiaireParEntreprise] CHECK CONSTRAINT [FK_StagiaireParEntreprise_TypeLien]
GO
/****** Object:  ForeignKey [FK_UniteParFormation_Formation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[UniteParFormation]  WITH CHECK ADD  CONSTRAINT [FK_UniteParFormation_Formation] FOREIGN KEY([CodeFormation])
REFERENCES [dbo].[Formation] ([CodeFormation])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UniteParFormation] CHECK CONSTRAINT [FK_UniteParFormation_Formation]
GO
/****** Object:  ForeignKey [FK_UniteParFormation_UniteFormation]    Script Date: 10/27/2012 11:49:43 ******/
ALTER TABLE [dbo].[UniteParFormation]  WITH CHECK ADD  CONSTRAINT [FK_UniteParFormation_UniteFormation] FOREIGN KEY([IdUniteFormation])
REFERENCES [dbo].[UniteFormation] ([IdUniteFormation])
GO
ALTER TABLE [dbo].[UniteParFormation] CHECK CONSTRAINT [FK_UniteParFormation_UniteFormation]
GO
