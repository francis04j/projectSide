USE Dev
GO
SELECT * FROM SearchTerms
GO

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'SearchTerms')
BEGIN
    CREATE TABLE dbo.SearchTerms (
        ClusterId INT NOT NULL IDENTITY,
        SearchTermID UNIQUEIDENTIFIER CONSTRAINT [CT_SEARCH_SEARCHTERMID] DEFAULT (newsequentialid()) NOT NULL,
        Term NVARCHAR(250) NOT NULL,
        CreatedAt DATETIME DEFAULT (getutcdate()),
        CreatedBy INT,
        ModifiedAt DATETIME DEFAULT (getutcdate()),
        ModifiedBy INT,
        CONSTRAINT [PK_SearchTerms] PRIMARY KEY NONCLUSTERED ([ClusterId] ASC)
        )
END 
GO
    CREATE UNIQUE CLUSTERED INDEX [IX_SearchTerms_ClusterId] ON dbo.SearchTerms ([ClusterId])
    GO

    CREATE UNIQUE NONCLUSTERED INDEX [IX_SearchTerms_Term] ON dbo.SearchTerms([Term])
    GO
