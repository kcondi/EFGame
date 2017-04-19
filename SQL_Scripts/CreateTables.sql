create table Player(
	PlayerId int PRIMARY KEY,
	Username nvarchar(128),
	Email nvarchar(128),
	Password nvarchar(128),
	MMR int
)

create table Match(
	MatchId int PRIMARY KEY,
	Name nvarchar(128)
)

create table Mission(
	MissionId int PRIMARY KEY,
	Name nvarchar(128),
	Text nvarchar(128),
	MatchId int NOT NULL FOREIGN KEY REFERENCES Match(MatchId)
)

create table Player_Match(
	PlayerId int NOT NULL FOREIGN KEY REFERENCES Player(PlayerId) ON DELETE CASCADE ON UPDATE CASCADE,
	MatchId int NOT NULL FOREIGN KEY REFERENCES Match(MatchId) ON DELETE CASCADE ON UPDATE CASCADE,
	primary key(PlayerId,MatchId)
)
	