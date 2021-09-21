CREATE TABLE Jogo(
	Id uuid DEFAULT ,
	Nome varchar(100),
	Produtora varchar(100),
	Preco float,
	CONSTRAINT jogo_pk PRIMARY KEY(Id)
);

INSERT INTO Jogo Values(
	'0ca314a5-9282-45d8-92c3-2985f2a9fd04', 
	'Fifa 21', 
	'EA', 
	'200'
);

INSERT INTO Jogo Values(
	'eb909ced-1862-4789-8641-1bba36c23db3',
	'Fifa 20', 
	'EA', 
	'190'
);

INSERT INTO Jogo Values(
	'5e99c84a-108b-4dfa-ab7e-d8c55957a7ec',
	'Fifa 19', 
	'EA', 
	'180'
);

INSERT INTO Jogo Values(
	'c3c9b5da-6a45-4de1-b28b-491cbf83b589',
	'GTA V', 
	'Rockstar', 
	'199.99'
);

INSERT INTO Jogo Values(
	'da033439-f352-4539-879f-515759312d53',
	'Fifa 18', 
	'EA', 
	'170'
);

INSERT INTO Jogo Values(
	'92576bd2-388e-4f5d-96c1-8bfda6c5a268',
	'Street Fighter V', 
	'Capcom', 
	'150'
);