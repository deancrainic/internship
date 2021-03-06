USE SqlAssignament;

CREATE TABLE Users (
	ID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Email NVARCHAR(50) NOT NULL,
	Pass NVARCHAR(30) NOT NULL,
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(25) NOT NULL
);

CREATE TABLE Properties (
	ID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Owner INT NOT NULL FOREIGN KEY REFERENCES Users(ID),
	Name NVARCHAR(50) NOT NULL,
	Description NVARCHAR(50),
	MaxGuests INT NOT NULL,
	Address NVARCHAR(100) NOT NULL,
	Price FLOAT NOT NULL CHECK (Price > 0)
);

CREATE TABLE Reservations (
	ID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Client INT NOT NULL FOREIGN KEY REFERENCES Users(ID),
	Owner INT NOT NULL FOREIGN KEY REFERENCES Users(ID),
	CheckinDate DATE NOT NULL,
	CheckoutDate DATE NOT NULL,
	GuestsNumber INT NOT NULL,
	TotalPrice FLOAT NOT NULL CHECK (TotalPrice > 0)
);