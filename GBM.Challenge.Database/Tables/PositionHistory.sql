CREATE TABLE [gbm].[PositionHistory]
(
	[Id]               UNIQUEIDENTIFIER NOT NULL,
	[Latitude]         VARCHAR (14)	  NOT NULL,
	[Longitude]        VARCHAR (14)	  NOT NULL,
	[RegistrationDate] DATETIME NOT NULL,
	[TaxyId]           UNIQUEIDENTIFIER NOT NULL,
	[DriverId]         UNIQUEIDENTIFIER NOT NULL,
	[TravelId]         UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_PositionHistory_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);
