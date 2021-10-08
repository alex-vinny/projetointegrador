ALTER TABLE public."Categorias"
    ADD COLUMN "DescricaoSemAcento" text COLLATE pg_catalog."default";

CREATE UNIQUE INDEX "Categoria_DescricaoSemAcento"
    ON public."Categorias" ("DescricaoSemAcento");

ALTER TABLE public."Categorias" 
    ADD CONSTRAINT "DescricaoSemAcento_UNIQUE"
    UNIQUE USING INDEX "Categoria_DescricaoSemAcento";

CREATE UNIQUE INDEX "Palavra_ValorSemAcento_DicaSemAcento"
    ON public."Categorias" ("ValorSemAcento", "DicaSemAcento");

ALTER TABLE public."Usuarios"
    ADD COLUMN "DicaSecreta" text COLLATE pg_catalog."default";

ALTER TABLE public."Usuarios"
    ADD COLUMN "PalavraSecreta" text COLLATE pg_catalog."default";

ALTER TABLE public."Palavras"
    ADD COLUMN "SerieEscolar" INT;