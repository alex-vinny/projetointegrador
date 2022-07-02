/* 
 * Versão para PostgreSQL
 * 15/06/2022 - Autor: Alex Vinicius
 * Inserindo public."Jogos"
*/
DROP TABLE IF EXISTS "_Jogos_";
CREATE TEMP TABLE "_Jogos_" ("Codigo" integer, "Descricao" text);
-- Clean up
DELETE FROM "_Jogos_";
/* Declaring a variable */
INSERT INTO "_Jogos_" ("Codigo", "Descricao") VALUES
(1, 'Reflectere: Jogo de Palavras Cruzadas'),
(2, 'In Memory: Jogo da Memória');

-- Inserindo valores
INSERT INTO public."Jogos" ("Codigo", "Descricao")
SELECT "Codigo", "Descricao"
FROM "_Jogos_";