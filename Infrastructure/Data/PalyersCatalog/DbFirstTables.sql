DROP TABLE PlayerWeapons
DROP TABLE Players
DROP TABLE Weapons
DROP TABLE Races
DROP TABLE Kinds

CREATE TABLE Kinds(
	Id int not null identity,
	KindName nvarchar(100) not null unique,

	PRIMARY KEY(Id)
)

CREATE TABLE Races(
	Id int not null identity,
	RaceName nvarchar(100) not null unique,
	Hp int not null,
	
	PRIMARY KEY(Id)
)

CREATE TABLE Weapons(
	Id int not null identity,
	WeaponName nvarchar(100) not null unique,
	[Power] int not null,

	PRIMARY KEY(Id)
)
GO

CREATE TABLE Players(
	Id int not null identity,
	CharacterName nvarchar(100) not null unique,
	Age int not null,
	Gender varchar(10) not null,
	Kind int not null references Kinds(Id),
	Race int not null references Races(Id) 	

	PRIMARY KEY (Id)
)
GO

CREATE TABLE PlayerWeapons(	
	Player int not null,
    Weapon int not null,
	Quantity int not null,
    FOREIGN KEY (Player) REFERENCES Players(Id),
    FOREIGN KEY (Weapon) REFERENCES Weapons(Id),

	PRIMARY KEY (Weapon, Player)
)