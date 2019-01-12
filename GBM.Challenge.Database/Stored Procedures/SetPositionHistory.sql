CREATE PROCEDURE [gbm].[SetPositionHistory]
	@Id               UNIQUEIDENTIFIER	NOT NULL,
    @TaxyId           UNIQUEIDENTIFIER  NOT NULL,
    @DriverId         UNIQUEIDENTIFIER  NOT NULL,
    @TravelId         UNIQUEIDENTIFIER  NOT NULL,
    @Latitude         VARCHAR (14)		NOT NULL,
    @Longitude        VARCHAR (14)		NOT NULL,
    @RegistrationDate DATETIME			NOT NULL
AS
BEGIN

    SET NOCOUNT ON;
    
    MERGE
        [gbm].[PositionHistory] AS target
    USING
        (SELECT @Id,   
				@TravelId)
        AS source
            ([Id],
             [TravelId])
        ON
            (target.Id = source.Id)
    WHEN MATCHED THEN
        UPDATE SET [Latitude] = @Latitude, [Longitude] = @Longitude, [RegistrationDate] = @RegistrationDate
    WHEN NOT MATCHED THEN
        INSERT VALUES  (@Id,	
						@Latitude,
						@Longitude,   
						@RegistrationDate,     
						@TaxyId,
						@DriverId,
						@TravelId
						);

END;