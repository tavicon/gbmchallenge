CREATE PROCEDURE [gbm].[GetPositionsByTravelHistory]
	 @TaxyId   UNIQUEIDENTIFIER NOT NULL,
	 @TravelId UNIQUEIDENTIFIER NOT NULL
AS
BEGIN
    SELECT  [Id], [TaxyId], [DriverId], [TravelId], [Latitude], [Longitude], [RegistrationDate]
    FROM    [gbm].[PositionHistory]
    WHERE   @TaxyId = @TaxyId AND @TravelId = @TravelId
END;
