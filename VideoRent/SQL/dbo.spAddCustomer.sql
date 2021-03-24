CREATE PROCEDURE [dbo].[spAddCustomer]
	@Name varchar(255),
	@MembershipType varchar(255),
	@Birthdate datetime2(7),
	@IsSubscribedToNewsletter bit
AS
	DECLARE @MembershipTypeId tinyint
	SET @MembershipTypeId = (SELECT Id FROM MembershipTypes WHERE Name = @MembershipType)

	INSERT INTO Customers (Name, MembershipTypeId, Birthdate, IsSubscribedToNewsletter)
	VALUES(@Name, @MembershipType, @Birthdate, @IsSubscribedToNewsletter) 