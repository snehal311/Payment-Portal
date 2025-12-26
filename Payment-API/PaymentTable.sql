Create table Payment (
Id UNIQUEIDENTIFIER not null Primary Key,
Reference VARCHAR(30) UNIQUE,
Amount DECIMAL(18,2) NOT NULL,
Currency VARCHAR(3) NOT NULL,
ClientRequestId UNIQUEIDENTIFIER UNIQUE,
CreatedAt DATETIME NOT NULL
)