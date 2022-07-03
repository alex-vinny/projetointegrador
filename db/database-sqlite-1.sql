BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Jogos" (
	"ID"	INTEGER NOT NULL,
	"Codigo"	INTEGER NOT NULL,
	"Descricao"	TEXT,
	CONSTRAINT "PK_Jogos" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Pontuacoes" (
	"ID"	INTEGER NOT NULL,
	"JogoID"	INTEGER,
	"AutorID"	INTEGER,
	"Itens"	INTEGER,
	"Pontos"	INTEGER,
	"Erros"	INTEGER,
	"DataJogo"	TEXT,
	CONSTRAINT "FK_Pontuacoes_Jogos_JogoID" FOREIGN KEY("JogoID") REFERENCES "Jogos"("ID") ON DELETE RESTRICT,
	CONSTRAINT "FK_Pontuacoes_Usuarios_AutorID" FOREIGN KEY("AutorID") REFERENCES "Usuarios"("ID") ON DELETE RESTRICT
	CONSTRAINT "PK_Pontuacoes" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE INDEX IF NOT EXISTS "IX_Pontuacoes_JogoID" ON "Pontuacoes" (
	"JogoID"
);
CREATE INDEX IF NOT EXISTS "IX_Pontuacoes_AutorID" ON "Pontuacoes" (
	"AutorID"
);
CREATE INDEX IF NOT EXISTS "IX_Pontuacoes_DataJogo" ON "Pontuacoes" (
	"DataJogo"
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Jogos_Codigo" ON "Jogos" (
	"Codigo"
);
COMMIT;