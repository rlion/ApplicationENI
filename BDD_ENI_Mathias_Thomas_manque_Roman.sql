USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[ContactENI]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Cours]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Entreprise]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Evenement]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Fonction]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Formation]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Module]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[ModuleParUnite]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[PlanningIndividuelDetail]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[PlanningIndividuelFormation]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[ProfilStagiaire]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Promotion]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Salle]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Stagiaire]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[StagiaireParEntreprise]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[Titre]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[TypeEntreprise]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[TypeEvenement]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[TypeLien]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[TypeProfil]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[UniteFormation]    Script Date: 04/18/2012 15:23:19 ******/
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

USE [APPLICATION_ENI]
GO

/****** Object:  Table [dbo].[UniteParFormation]    Script Date: 04/18/2012 15:23:19 ******/
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

ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Entreprise] FOREIGN KEY([CodeEntreprise])
REFERENCES [dbo].[Entreprise] ([CodeEntreprise])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Entreprise]
GO

ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Fonction] FOREIGN KEY([CodeFonction])
REFERENCES [dbo].[Fonction] ([CodeFonction])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Fonction]
GO

/*ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_ImportanceContact] FOREIGN KEY([CodeImportance])
REFERENCES [dbo].[ImportanceContact] ([CodeImportance])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_ImportanceContact]
GO
*/
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [CK_Contact_Fixe_Mobile] CHECK  (([TelFixe] IS NOT NULL OR [TelMobile] IS NOT NULL))
GO

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [CK_Contact_Fixe_Mobile]
GO

ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_CodeFonction]  DEFAULT ('_____') FOR [CodeFonction]
GO

ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_CodeImportance]  DEFAULT ((3)) FOR [CodeImportance]
GO

ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_Archive]  DEFAULT ((0)) FOR [Archive]
GO

/*ALTER TABLE [dbo].[ContactENI]  WITH CHECK ADD  CONSTRAINT [FK_ContactENI_Domaine] FOREIGN KEY([CodeDomaine])
REFERENCES [dbo].[Domaine] ([CodeDomaine])
GO

ALTER TABLE [dbo].[ContactENI] CHECK CONSTRAINT [FK_ContactENI_Domaine]
GO
*/
ALTER TABLE [dbo].[ContactENI]  WITH CHECK ADD  CONSTRAINT [CK_ContactENI] CHECK  (([ModeNotification]=(2) OR [ModeNotification]=(1) OR [ModeNotification]=(0)))
GO

ALTER TABLE [dbo].[ContactENI] CHECK CONSTRAINT [CK_ContactENI]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactENI', @level2type=N'CONSTRAINT',@level2name=N'CK_ContactENI'
GO

ALTER TABLE [dbo].[ContactENI] ADD  CONSTRAINT [DF_ContactENI_ModeNotification]  DEFAULT ((0)) FOR [ModeNotification]
GO

ALTER TABLE [dbo].[Cours]  WITH CHECK ADD  CONSTRAINT [FK_Cours_ContactENI] FOREIGN KEY([CodeFormateur])
REFERENCES [dbo].[ContactENI] ([CodeContactENI])
GO

ALTER TABLE [dbo].[Cours] CHECK CONSTRAINT [FK_Cours_ContactENI]
GO

ALTER TABLE [dbo].[Cours]  WITH CHECK ADD  CONSTRAINT [FK_Cours_Module] FOREIGN KEY([IdModule])
REFERENCES [dbo].[Module] ([IdModule])
GO

ALTER TABLE [dbo].[Cours] CHECK CONSTRAINT [FK_Cours_Module]
GO

ALTER TABLE [dbo].[Cours]  WITH CHECK ADD  CONSTRAINT [FK_Cours_Promotion] FOREIGN KEY([CodePromotion])
REFERENCES [dbo].[Promotion] ([CodePromotion])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Cours] CHECK CONSTRAINT [FK_Cours_Promotion]
GO

ALTER TABLE [dbo].[Cours]  WITH CHECK ADD  CONSTRAINT [FK_Cours_Salle] FOREIGN KEY([CodeSalle])
REFERENCES [dbo].[Salle] ([CodeSalle])
GO

ALTER TABLE [dbo].[Cours] CHECK CONSTRAINT [FK_Cours_Salle]
GO

ALTER TABLE [dbo].[Cours] ADD  CONSTRAINT [DF_Cours_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO

/*ALTER TABLE [dbo].[Entreprise]  WITH CHECK ADD  CONSTRAINT [FK_Entreprise_Organisme] FOREIGN KEY([CodeOrganisme])
REFERENCES [dbo].[Organisme] ([CodeOrganisme])
ON UPDATE CASCADE
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[Entreprise] CHECK CONSTRAINT [FK_Entreprise_Organisme]
GO
*/
/*ALTER TABLE [dbo].[Entreprise]  WITH CHECK ADD  CONSTRAINT [FK_Entreprise_Region] FOREIGN KEY([CodeRegion])
REFERENCES [dbo].[Region] ([CodeRegion])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Entreprise] CHECK CONSTRAINT [FK_Entreprise_Region]
GO
*/
/*ALTER TABLE [dbo].[Entreprise]  WITH CHECK ADD  CONSTRAINT [FK_Entreprise_SecteurActivite] FOREIGN KEY([CodeSecteur])
REFERENCES [dbo].[SecteurActivite] ([CodeSecteur])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Entreprise] CHECK CONSTRAINT [FK_Entreprise_SecteurActivite]
GO
*/
ALTER TABLE [dbo].[Entreprise]  WITH CHECK ADD  CONSTRAINT [FK_Entreprise_TypeEntreprise] FOREIGN KEY([CodeTypeEntreprise])
REFERENCES [dbo].[TypeEntreprise] ([CodeTypeEntreprise])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Entreprise] CHECK CONSTRAINT [FK_Entreprise_TypeEntreprise]
GO

ALTER TABLE [dbo].[Entreprise] ADD  CONSTRAINT [DF_Entreprise_CodeTypeEntreprise]  DEFAULT ('_____') FOR [CodeTypeEntreprise]
GO

ALTER TABLE [dbo].[Entreprise] ADD  CONSTRAINT [DF_Entreprise_CodeRegion]  DEFAULT ('__') FOR [CodeRegion]
GO

ALTER TABLE [dbo].[Entreprise] ADD  CONSTRAINT [DF_Entreprise_CodeSecteur]  DEFAULT ((0)) FOR [CodeSecteur]
GO

ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_ContactENI] FOREIGN KEY([CodeContactENI])
REFERENCES [dbo].[ContactENI] ([CodeContactENI])
GO

ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_ContactENI]
GO

ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Entreprise] FOREIGN KEY([CodeEntreprise])
REFERENCES [dbo].[Entreprise] ([CodeEntreprise])
ON UPDATE CASCADE
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Entreprise]
GO

ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Evenement] FOREIGN KEY([EvenementParent])
REFERENCES [dbo].[Evenement] ([CodeEvenement])
GO

ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Evenement]
GO

ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_EvenementStagiaire_Contact] FOREIGN KEY([CodeContact])
REFERENCES [dbo].[Contact] ([CodeContact])
GO

ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_EvenementStagiaire_Contact]
GO

ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_EvenementStagiaire_Stagiaire] FOREIGN KEY([CodeStagiaire])
REFERENCES [dbo].[Stagiaire] ([CodeStagiaire])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_EvenementStagiaire_Stagiaire]
GO

ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_EvenementStagiaire_TypeEvenementStagiaire] FOREIGN KEY([CodeTypeEvenement])
REFERENCES [dbo].[TypeEvenement] ([CodeTypeEvenement])
GO

ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_EvenementStagiaire_TypeEvenementStagiaire]
GO

ALTER TABLE [dbo].[Evenement] ADD  CONSTRAINT [DF_Evenement_FilsPresents]  DEFAULT ((0)) FOR [FilsPresents]
GO

ALTER TABLE [dbo].[Formation]  WITH CHECK ADD  CONSTRAINT [FK_Formation_Titre] FOREIGN KEY([CodeTitre])
REFERENCES [dbo].[Titre] ([CodeTitre])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Formation] CHECK CONSTRAINT [FK_Formation_Titre]
GO

ALTER TABLE [dbo].[Formation] ADD  CONSTRAINT [DF_Formation_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO

ALTER TABLE [dbo].[Formation] ADD  CONSTRAINT [DF_Formation_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO

ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO

ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO

ALTER TABLE [dbo].[ModuleParUnite]  WITH CHECK ADD  CONSTRAINT [FK_ModuleParUnite_Module] FOREIGN KEY([IdModule])
REFERENCES [dbo].[Module] ([IdModule])
GO

ALTER TABLE [dbo].[ModuleParUnite] CHECK CONSTRAINT [FK_ModuleParUnite_Module]
GO

ALTER TABLE [dbo].[ModuleParUnite]  WITH CHECK ADD  CONSTRAINT [FK_ModuleParUnite_UniteParFormation] FOREIGN KEY([IdUnite])
REFERENCES [dbo].[UniteParFormation] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ModuleParUnite] CHECK CONSTRAINT [FK_ModuleParUnite_UniteParFormation]
GO

ALTER TABLE [dbo].[PlanningIndividuelDetail]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelDetail_Cours] FOREIGN KEY([IdCours])
REFERENCES [dbo].[Cours] ([IdCours])
GO

ALTER TABLE [dbo].[PlanningIndividuelDetail] CHECK CONSTRAINT [FK_PlanningIndividuelDetail_Cours]
GO

ALTER TABLE [dbo].[PlanningIndividuelDetail]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelDetail_PlanningIndividuelFormation] FOREIGN KEY([CodePlanning])
REFERENCES [dbo].[PlanningIndividuelFormation] ([CodePlanning])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PlanningIndividuelDetail] CHECK CONSTRAINT [FK_PlanningIndividuelDetail_PlanningIndividuelFormation]
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_Formation] FOREIGN KEY([CodeFormation])
REFERENCES [dbo].[Formation] ([CodeFormation])
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_Formation]
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_Promotion] FOREIGN KEY([CodePromotion])
REFERENCES [dbo].[Promotion] ([CodePromotion])
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_Promotion]
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_Stagiaire] FOREIGN KEY([CodeStagiaire])
REFERENCES [dbo].[Stagiaire] ([CodeStagiaire])
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_Stagiaire]
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_StagiaireParEntreprise] FOREIGN KEY([NumLien])
REFERENCES [dbo].[StagiaireParEntreprise] ([NumLien])
ON UPDATE CASCADE
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_StagiaireParEntreprise]
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation]  WITH CHECK ADD  CONSTRAINT [FK_PlanningIndividuelFormation_TypeProfil] FOREIGN KEY([CodeTypeProfil])
REFERENCES [dbo].[TypeProfil] ([CodeTypeProfil])
GO

ALTER TABLE [dbo].[PlanningIndividuelFormation] CHECK CONSTRAINT [FK_PlanningIndividuelFormation_TypeProfil]
GO

/*ALTER TABLE [dbo].[ProfilStagiaire]  WITH CHECK ADD  CONSTRAINT [FK_ProfilStagiaire_Allocation] FOREIGN KEY([CodeAllocation])
REFERENCES [dbo].[Allocation] ([CodeAllocation])
GO

ALTER TABLE [dbo].[ProfilStagiaire] CHECK CONSTRAINT [FK_ProfilStagiaire_Allocation]
GO
*/
ALTER TABLE [dbo].[ProfilStagiaire]  WITH CHECK ADD  CONSTRAINT [FK_ProfilStagiaire_ContactENI] FOREIGN KEY([CodeContactEni])
REFERENCES [dbo].[ContactENI] ([CodeContactENI])
GO

ALTER TABLE [dbo].[ProfilStagiaire] CHECK CONSTRAINT [FK_ProfilStagiaire_ContactENI]
GO

ALTER TABLE [dbo].[ProfilStagiaire]  WITH CHECK ADD  CONSTRAINT [FK_ProfilStagiaire_Stagiaire] FOREIGN KEY([CodeStagiaire])
REFERENCES [dbo].[Stagiaire] ([CodeStagiaire])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProfilStagiaire] CHECK CONSTRAINT [FK_ProfilStagiaire_Stagiaire]
GO

/*ALTER TABLE [dbo].[ProfilStagiaire]  WITH CHECK ADD  CONSTRAINT [FK_ProfilStagiaire_Statut] FOREIGN KEY([CodeStatut])
REFERENCES [dbo].[Statut] ([CodeStatut])
GO

ALTER TABLE [dbo].[ProfilStagiaire] CHECK CONSTRAINT [FK_ProfilStagiaire_Statut]
GO
*/
ALTER TABLE [dbo].[ProfilStagiaire] ADD  CONSTRAINT [DF_ProfilStagiaire_CodeStatut]  DEFAULT ('__') FOR [CodeStatut]
GO

ALTER TABLE [dbo].[ProfilStagiaire] ADD  CONSTRAINT [DF_ProfilStagiaire_CodeAllocation]  DEFAULT ('__') FOR [CodeAllocation]
GO

ALTER TABLE [dbo].[ProfilStagiaire] ADD  CONSTRAINT [DF_ProfilStagiaire_EstEnRecherche]  DEFAULT ((0)) FOR [EstEnRecherche]
GO

ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Formation] FOREIGN KEY([CodeFormation])
REFERENCES [dbo].[Formation] ([CodeFormation])
GO

ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Formation]
GO

ALTER TABLE [dbo].[Promotion] ADD  CONSTRAINT [DF_Promotion_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO

/*ALTER TABLE [dbo].[Stagiaire]  WITH CHECK ADD  CONSTRAINT [FK_Stagiaire_Nationalite] FOREIGN KEY([CodeNationalite])
REFERENCES [dbo].[Nationalite] ([CodeNationalite])
GO

ALTER TABLE [dbo].[Stagiaire] CHECK CONSTRAINT [FK_Stagiaire_Nationalite]
GO
*/
/*ALTER TABLE [dbo].[Stagiaire]  WITH CHECK ADD  CONSTRAINT [FK_Stagiaire_OrigineMedia] FOREIGN KEY([CodeOrigineMedia])
REFERENCES [dbo].[OrigineMedia] ([CodeOrigineMedia])
GO

ALTER TABLE [dbo].[Stagiaire] CHECK CONSTRAINT [FK_Stagiaire_OrigineMedia]
GO
*/
/*ALTER TABLE [dbo].[Stagiaire]  WITH CHECK ADD  CONSTRAINT [FK_Stagiaire_Region] FOREIGN KEY([CodeRegion])
REFERENCES [dbo].[Region] ([CodeRegion])
GO

ALTER TABLE [dbo].[Stagiaire] CHECK CONSTRAINT [FK_Stagiaire_Region]
GO
*/
ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_CodeRegion]  DEFAULT ('__') FOR [CodeRegion]
GO

ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_CodeNationalite]  DEFAULT ('__') FOR [CodeNationalite]
GO

ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_CodeOrigineMedia]  DEFAULT ('__') FOR [CodeOrigineMedia]
GO

ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_Permis]  DEFAULT ((0)) FOR [Permis]
GO

ALTER TABLE [dbo].[Stagiaire] ADD  CONSTRAINT [DF_Stagiaire_EnvoiDocEnCours]  DEFAULT ((0)) FOR [EnvoiDocEnCours]
GO

ALTER TABLE [dbo].[StagiaireParEntreprise]  WITH CHECK ADD  CONSTRAINT [FK_StagiaireParEntreprise_Entreprise] FOREIGN KEY([CodeEntreprise])
REFERENCES [dbo].[Entreprise] ([CodeEntreprise])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[StagiaireParEntreprise] CHECK CONSTRAINT [FK_StagiaireParEntreprise_Entreprise]
GO

ALTER TABLE [dbo].[StagiaireParEntreprise]  WITH CHECK ADD  CONSTRAINT [FK_StagiaireParEntreprise_Fonction] FOREIGN KEY([CodeFonction])
REFERENCES [dbo].[Fonction] ([CodeFonction])
GO

ALTER TABLE [dbo].[StagiaireParEntreprise] CHECK CONSTRAINT [FK_StagiaireParEntreprise_Fonction]
GO

ALTER TABLE [dbo].[StagiaireParEntreprise]  WITH CHECK ADD  CONSTRAINT [FK_StagiaireParEntreprise_Stagiaire] FOREIGN KEY([CodeStagiaire])
REFERENCES [dbo].[Stagiaire] ([CodeStagiaire])
GO

ALTER TABLE [dbo].[StagiaireParEntreprise] CHECK CONSTRAINT [FK_StagiaireParEntreprise_Stagiaire]
GO

ALTER TABLE [dbo].[StagiaireParEntreprise]  WITH CHECK ADD  CONSTRAINT [FK_StagiaireParEntreprise_TypeLien] FOREIGN KEY([CodeTypeLien])
REFERENCES [dbo].[TypeLien] ([CodeTypeLien])
GO

ALTER TABLE [dbo].[StagiaireParEntreprise] CHECK CONSTRAINT [FK_StagiaireParEntreprise_TypeLien]
GO

ALTER TABLE [dbo].[Titre] ADD  CONSTRAINT [DF_Titre_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO

ALTER TABLE [dbo].[Titre] ADD  CONSTRAINT [DF_Titre_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO

/*ALTER TABLE [dbo].[TypeEvenement]  WITH CHECK ADD  CONSTRAINT [FK_TypeEvenementStagiaire_NatureEvenement] FOREIGN KEY([CodeNature])
REFERENCES [dbo].[NatureEvenement] ([CodeNature])
GO

ALTER TABLE [dbo].[TypeEvenement] CHECK CONSTRAINT [FK_TypeEvenementStagiaire_NatureEvenement]
GO
*/
ALTER TABLE [dbo].[UniteFormation] ADD  CONSTRAINT [DF_UniteFormation_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO

ALTER TABLE [dbo].[UniteFormation] ADD  CONSTRAINT [DF_UniteFormation_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO

ALTER TABLE [dbo].[UniteParFormation]  WITH CHECK ADD  CONSTRAINT [FK_UniteParFormation_Formation] FOREIGN KEY([CodeFormation])
REFERENCES [dbo].[Formation] ([CodeFormation])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UniteParFormation] CHECK CONSTRAINT [FK_UniteParFormation_Formation]
GO

ALTER TABLE [dbo].[UniteParFormation]  WITH CHECK ADD  CONSTRAINT [FK_UniteParFormation_UniteFormation] FOREIGN KEY([IdUniteFormation])
REFERENCES [dbo].[UniteFormation] ([IdUniteFormation])
GO

ALTER TABLE [dbo].[UniteParFormation] CHECK CONSTRAINT [FK_UniteParFormation_UniteFormation]
GO


USE [APPLICATION_ENI]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

--TABLE COMPETENCES--
CREATE TABLE [dbo].[COMPETENCE](
	[idCompetence] [char](8) NOT NULL,
	[code] [char](8) NOT NULL,
	[libelle] [char](30) NOT NULL,
 CONSTRAINT [PK_Competence] PRIMARY KEY CLUSTERED 
(
	[idCompetence] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

--VALEURS TABLE COMPETENCES--
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCE]
           ([idCompetence]
           ,[code]
           ,[libelle])
     VALUES
           ('1'
           ,'C1'
           ,'VB.NET 1A')
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCE]
           ([idCompetence]
           ,[code]
           ,[libelle])
     VALUES
           ('2'
           ,'C2'
           ,'VB.NET 2A')
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCE]
           ([idCompetence]
           ,[code]
           ,[libelle])
     VALUES
           ('3'
           ,'C3'
           ,'VB.NET 1B')
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCE]
           ([idCompetence]
           ,[code]
           ,[libelle])
     VALUES
           ('4'
           ,'C4'
           ,'SQL 1')
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCE]
           ([idCompetence]
           ,[code]
           ,[libelle])
     VALUES
           ('5'
           ,'C5'
           ,'SQL 2')
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCE]
           ([idCompetence]
           ,[code]
           ,[libelle])
     VALUES
           ('6'
           ,'C6'
           ,'C# 1')
GO

--TABLE ECFS--
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ECF](
	[idECF] [char](8) NOT NULL,
	[code] [char](8) NOT NULL,
	[libelle] [char](30) NOT NULL,
	[coefficient] [float] NULL,
	[typeNotation] [smallint] NULL,
	[nbreVersions] [int] NULL,
	[commentaire] [char](150) NULL,
 CONSTRAINT [PK_ECFS_1] PRIMARY KEY CLUSTERED 
(
	[idECF] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

--VALEURS TABLE ECFS--
INSERT INTO [APPLICATION_ENI].[dbo].[ECF]
           ([idECF]
           ,[code]
           ,[libelle]
           ,[coefficient]
           ,[typeNotation]
           ,[nbreVersions]
           ,[commentaire])
     VALUES
           ('1'
           ,'ECF1'
           ,'VB.NET'
           ,1
           ,0
           ,2
           ,'1er ECF développement')
GO
INSERT INTO [APPLICATION_ENI].[dbo].[ECF]
           ([idECF]
           ,[code]
           ,[libelle]
           ,[coefficient]
           ,[typeNotation]
           ,[nbreVersions]
           ,[commentaire])
     VALUES
           ('2'
           ,'ECF2'
           ,'SQL'
           ,1
           ,1
           ,3
           ,'ECF SQL vient après VB.NET (ECF1)')
GO
INSERT INTO [APPLICATION_ENI].[dbo].[ECF]
           ([idECF]
           ,[code]
           ,[libelle]
           ,[coefficient]
           ,[typeNotation]
           ,[nbreVersions]
           ,[commentaire])
     VALUES
           ('3'
           ,'ECF3'
           ,'C#'
           ,1
           ,1
           ,2
           ,'2ème ECF Développement vient après VB.NET (ECF1)')
GO

--TABLE COMPETENCESECF--
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[COMPETENCESECF](
	[idECF] [char](8) NOT NULL,
	[idCompetence] [char](8) NOT NULL,
 CONSTRAINT [PK_COMPETENCESECF] PRIMARY KEY CLUSTERED 
(
	[idECF] ASC,
	[idCompetence] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[COMPETENCESECF]  WITH CHECK ADD  CONSTRAINT [FK_CompetenceECFS_Competences] FOREIGN KEY([idCompetence])
REFERENCES [dbo].[COMPETENCE] ([idCompetence])
GO

ALTER TABLE [dbo].[COMPETENCESECF] CHECK CONSTRAINT [FK_CompetenceECFS_Competences]
GO

ALTER TABLE [dbo].[COMPETENCESECF]  WITH CHECK ADD  CONSTRAINT [FK_COMPETENCESECF_ECFS] FOREIGN KEY([idECF])
REFERENCES [dbo].[ECF] ([idECF])
GO

ALTER TABLE [dbo].[COMPETENCESECF] CHECK CONSTRAINT [FK_COMPETENCESECF_ECFS]
GO

--VALEURS TABLE COMPETENCESECF--
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCESECF]
           ([idECF]
           ,[idCompetence])
     VALUES
           (1
           ,1)
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCESECF]
           ([idECF]
           ,[idCompetence])
     VALUES
           (1
           ,2)
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCESECF]
           ([idECF]
           ,[idCompetence])
     VALUES
           (1
           ,3)
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCESECF]
           ([idECF]
           ,[idCompetence])
     VALUES
           (2
           ,4)
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCESECF]
           ([idECF]
           ,[idCompetence])
     VALUES
           (2
           ,5)
GO
INSERT INTO [APPLICATION_ENI].[dbo].[COMPETENCESECF]
           ([idECF]
           ,[idCompetence])
     VALUES
           (3
           ,6)
GO

--TABLE ECFSFORMATION--
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ECFSFORMATION](
	[idECF] [char](8) NOT NULL,
	[idFormation] [char](8) NOT NULL,
 CONSTRAINT [PK_ECFSFORMATION] PRIMARY KEY CLUSTERED 
(
	[idECF] ASC,
	[idFormation] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ECFSFORMATION]  WITH CHECK ADD  CONSTRAINT [FK_ECFFormations_ECFS] FOREIGN KEY([idECF])
REFERENCES [dbo].[ECF] ([idECF])
GO

ALTER TABLE [dbo].[ECFSFORMATION] CHECK CONSTRAINT [FK_ECFFormations_ECFS]
GO

ALTER TABLE [dbo].[ECFSFORMATION]  WITH CHECK ADD  CONSTRAINT [FK_ECFFormations_Formation] FOREIGN KEY([idFormation])
REFERENCES [dbo].[Formation] ([CodeFormation])
GO

ALTER TABLE [dbo].[ECFSFORMATION] CHECK CONSTRAINT [FK_ECFFormations_Formation]
GO

--TABLE EVALUATION--
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EVALUATION](
	[idEvaluation] [char](8) NOT NULL,
	[idECF] [char](8) NOT NULL,
	[idStagiaire] [char](8) NOT NULL,
	[idCompetence] [char](8) NOT NULL,
	[note] [float] NULL,
	[version] [int] NULL,
	[date] [datetime] NULL,
 CONSTRAINT [PK_EVALUATIONS] PRIMARY KEY CLUSTERED 
(
	[idEvaluation] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EVALUATION]  WITH CHECK ADD  CONSTRAINT [FK_EVALUATIONS_COMPETENCES] FOREIGN KEY([idCompetence])
REFERENCES [dbo].[COMPETENCE] ([idCompetence])
GO

ALTER TABLE [dbo].[EVALUATION] CHECK CONSTRAINT [FK_EVALUATIONS_COMPETENCES]
GO

ALTER TABLE [dbo].[EVALUATION]  WITH CHECK ADD  CONSTRAINT [FK_EVALUATIONS_ECFS] FOREIGN KEY([idECF])
REFERENCES [dbo].[ECF] ([idECF])
GO

ALTER TABLE [dbo].[EVALUATION] CHECK CONSTRAINT [FK_EVALUATIONS_ECFS]
GO

--TABLE SESSIONSECF--
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SESSIONSECF](
	[idECF] [char](8) NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_SESSIONSECF] PRIMARY KEY CLUSTERED 
(
	[idECF] ASC,
	[date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SESSIONSECF]  WITH CHECK ADD  CONSTRAINT [FK_SESSIONSECF_ECFS] FOREIGN KEY([idECF])
REFERENCES [dbo].[ECF] ([idECF])
GO

ALTER TABLE [dbo].[SESSIONSECF] CHECK CONSTRAINT [FK_SESSIONSECF_ECFS]
GO

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


/********************** AJOUT CODE ROMAN ***************************/

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
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
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'AL      ', N'Archi. Log.', N'Architecte Logiciel', CAST(0x00009E9900000000 AS DateTime), 1, 1, N'1', N'ALROM', N'ALNSF')
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'ASR     ', N'Archi. Spé. Réseau', N'Architecte Spécialiste Réseau', CAST(0x0000922800000000 AS DateTime), 1, 0, N'2', N'ASRROM', N'ASRNSF')
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'CDI     ', N'Concept Dev. Log.', N'Concepteur Développeur Informatique', CAST(0x0000916800000000 AS DateTime), 1, 0, N'2', N'CDIROM', N'CDINSF')
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'DL      ', N'Dev. Log.', N'Développeur Logiciel', CAST(0x0000901A00000000 AS DateTime), 1, 0, N'3', N'DLROM', N'DLNSF')
INSERT [dbo].[Titre] ([CodeTitre], [LibelleCourt], [LibelleLong], [DateCreation], [TitreENI], [Archiver], [niveau], [codeRome], [codeNSF]) VALUES (N'EISI    ', N'EISI', N'EISI', CAST(0x00009ED600000000 AS DateTime), 1, 0, N'1', N'EISIROM', N'EISINSF')
ALTER TABLE [dbo].[Titre] ADD  CONSTRAINT [DF_Titre_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
ALTER TABLE [dbo].[Titre] ADD  CONSTRAINT [DF_Titre_Archiver]  DEFAULT ((0)) FOR [Archiver]
GO
ALTER TABLE [dbo].[EPREUVETITRE]  WITH CHECK ADD  CONSTRAINT [FK_EPREUVETITRE_Salle] FOREIGN KEY([CodeSalle])
REFERENCES [dbo].[Salle] ([CodeSalle])
GO
ALTER TABLE [dbo].[EPREUVETITRE] CHECK CONSTRAINT [FK_EPREUVETITRE_Salle]
GO
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notifie le niveau diplomant associé au titre' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Titre', @level2type=N'COLUMN',@level2name=N'niveau'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notifie le codeRome associé au titre' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Titre', @level2type=N'COLUMN',@level2name=N'codeRome'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Notifie le codeRome associé au titre' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Titre', @level2type=N'COLUMN',@level2name=N'codeNSF'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Définir les titres décernés par les formations de l''ENI' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Titre'
GO
