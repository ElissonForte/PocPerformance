USE [AdventureWorksLT]
GO

/****** Object:  StoredProcedure [dbo].[getClients]    Script Date: 5/2/2018 1:03:39 PM ******/
DROP PROCEDURE [dbo].[getClients]
GO

/****** Object:  StoredProcedure [dbo].[getClients]    Script Date: 5/2/2018 1:03:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getClients]
(
	@strID VARCHAR(MAX)
)
AS
BEGIN

	IF OBJECT_ID('tempdb..#tmpID') IS NOT NULL
    BEGIN
        DROP TABLE #tmpID;
    END

	SELECT DISTINCT RTRIM(LTRIM(CAST(value AS VARCHAR(20)))) ID
    INTO #tmpID
    FROM STRING_SPLIT(@strID, ',')

	CREATE INDEX IX_ID_ID ON #tmpID(ID);

	SELECT [CustomerID]
        , ([Title] + ' ' + [FirstName] + ' ' + [MiddleName] + ' ' + [LastName]) AS[Name]
        ,[CompanyName]
        ,[SalesPerson]
        ,[EmailAddress]
        ,[Phone]
	FROM [SalesLT].[Customer] WITH (NOLOCK) WHERE CustomerID IN (SELECT ID FROM #tmpID)

END
GO


