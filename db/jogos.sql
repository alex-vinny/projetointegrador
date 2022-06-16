/* 
 * Versão para SQLite
 * 15/06/2022 - Autor: Alex Vinicius
 * Inserindo Jogos
*/

PRAGMA temp_store = 2; /* 2 means use in-memory */
CREATE TEMP TABLE IF NOT EXISTS _Jogos (Codigo INTEGER, Descricao TEXT);
-- Clean up
DELETE FROM _Jogos;
/* Declaring a variable */
INSERT INTO _Jogos (Codigo, Descricao) VALUES
(1, 'Reflectere: Jogo de Palavras Cruzadas'),
(2, 'In Memory: Jogo da Memória');

-- Inserindo valores
INSERT INTO Jogos (Codigo, Descricao)
SELECT Codigo, Descricao
FROM _Jogos
