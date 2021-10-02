BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "Usuarios" (
	"ID"	INTEGER NOT NULL,
	"Email"	TEXT,
	"Nome"	TEXT,
	"Senha"	TEXT,
	"Perfil"	INTEGER NOT NULL,
	"DicaSecreta"	TEXT,
	"PalavraSecreta"	TEXT,
	"Discriminator"	TEXT NOT NULL,
	"Idade"	INTEGER,
	"Sexo"	INTEGER,
	"SerieEscolar"	INTEGER,
	"Disciplina"	TEXT,
	CONSTRAINT "PK_Usuarios" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Cruzadas" (
	"ID"	INTEGER NOT NULL,
	"TamanhoX"	INTEGER NOT NULL,
	"TamanhoY"	INTEGER NOT NULL,
	"Criacao"	TEXT NOT NULL,
	"AutorID"	INTEGER,
	"CategoriaID"	INTEGER,
	CONSTRAINT "FK_Cruzadas_Usuarios_AutorID" FOREIGN KEY("AutorID") REFERENCES "Usuarios"("ID") ON DELETE RESTRICT,
	CONSTRAINT "FK_Cruzadas_Categorias_CategoriaID" FOREIGN KEY("CategoriaID") REFERENCES "Categorias"("ID") ON DELETE RESTRICT,
	CONSTRAINT "PK_Cruzadas" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "CruzadaItens" (
	"ID"	INTEGER NOT NULL,
	"PalavraID"	INTEGER,
	"PosicaoX"	INTEGER NOT NULL,
	"PosicaoY"	INTEGER NOT NULL,
	"Orientacao"	INTEGER NOT NULL,
	"CruzadaID"	INTEGER,
	CONSTRAINT "FK_CruzadaItens_Palavras_PalavraID" FOREIGN KEY("PalavraID") REFERENCES "Palavras"("ID") ON DELETE RESTRICT,
	CONSTRAINT "FK_CruzadaItens_Cruzadas_CruzadaID" FOREIGN KEY("CruzadaID") REFERENCES "Cruzadas"("ID") ON DELETE RESTRICT,
	CONSTRAINT "PK_CruzadaItens" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Sessoes" (
	"ID"	INTEGER NOT NULL,
	"Inicio"	TEXT NOT NULL,
	"Fim"	TEXT,
	"CruzadaID"	INTEGER,
	"Acertos"	INTEGER,
	"UsuarioID"	INTEGER,
	CONSTRAINT "FK_Sessoes_Cruzadas_CruzadaID" FOREIGN KEY("CruzadaID") REFERENCES "Cruzadas"("ID") ON DELETE RESTRICT,
	CONSTRAINT "FK_Sessoes_Usuarios_UsuarioID" FOREIGN KEY("UsuarioID") REFERENCES "Usuarios"("ID") ON DELETE RESTRICT,
	CONSTRAINT "PK_Sessoes" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Categorias" (
	"ID"	INTEGER NOT NULL,
	"Descricao"	TEXT UNIQUE,
	"DescricaoSemAcento"	TEXT,
	CONSTRAINT "PK_Categorias" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Palavras" (
	"ID"	INTEGER NOT NULL,
	"Valor"	TEXT,
	"Dica"	TEXT,
	"SerieEscolar"	INTEGER,
	"CategoriaID"	INTEGER,
	"ValorSemAcento"	TEXT,
	"DicaSemAcento"	TEXT,
	CONSTRAINT "FK_Palavras_Categorias_CategoriaID" FOREIGN KEY("CategoriaID") REFERENCES "Categorias"("ID") ON DELETE RESTRICT,
	CONSTRAINT "PK_Palavras" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE INDEX IF NOT EXISTS "IX_CruzadaItens_CruzadaID" ON "CruzadaItens" (
	"CruzadaID"
);
CREATE INDEX IF NOT EXISTS "IX_CruzadaItens_PalavraID" ON "CruzadaItens" (
	"PalavraID"
);
CREATE INDEX IF NOT EXISTS "IX_Cruzadas_AutorID" ON "Cruzadas" (
	"AutorID"
);
CREATE INDEX IF NOT EXISTS "IX_Cruzadas_CategoriaID" ON "Cruzadas" (
	"CategoriaID"
);
CREATE INDEX IF NOT EXISTS "IX_Sessoes_CruzadaID" ON "Sessoes" (
	"CruzadaID"
);
CREATE INDEX IF NOT EXISTS "IX_Sessoes_UsuarioID" ON "Sessoes" (
	"UsuarioID"
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Usuarios_Email_Nome" ON "Usuarios" (
	"Email",
	"Nome"
);
CREATE INDEX IF NOT EXISTS "IX_Palavras_CategoriaID" ON "Palavras" (
	"CategoriaID"
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Categorias_Descricao" ON "Categorias" (
	"DescricaoSemAcento"
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Palavras_Valor_Dica" ON "Palavras" (
	"ValorSemAcento",
	"DicaSemAcento"
);
COMMIT;
