CREATE TABLE nota_fiscal (
  id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  numberNfe varchar(10) NOT NULL,
  accessKey varchar(50) NOT NULL,
  dateEmission datetime NOT NULL,
  total_value float NOT NULL,
  numeroPedido int NOT NULL,
  description varchar(255) NULL,
  emitente_id int NOT NULL,
  destinatario_id int NOT NULL,
  CONSTRAINT FK_emitente FOREIGN KEY (emitente_id) REFERENCES pessoa (id),
  CONSTRAINT FK_destinatario FOREIGN KEY (destinatario_id) REFERENCES pessoa (id)
);

CREATE TABLE pessoa (
  id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  name varchar(100) NOT NULL,
  email varchar(40) NULL,
  cnpj_cpf varchar(14) NOT NULL,
  address varchar(50) NOT NULL,
  address_number varchar(8) NOT NULL,
  hood varchar(30) NOT NULL,
  city varchar(30) NOT NULL,
  uf varchar(2) NOT NULL,
  cep varchar(8) NOT NULL
);

CREATE TABLE produto (
  id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  nota_fiscal_id int NOT NULL,
  number_item int NOT NULL,
  code_item int NOT NULL,
  name varchar(50) NOT NULL,
  quantity int NOT NULL,
  unity_value float NOT NULL,
  total_value float NOT NULL,
  CONSTRAINT FK_nota_fiscal FOREIGN KEY (nota_fiscal_id) REFERENCES nota_fiscal (id)
);
--ORDEM DE CRIA��O
--pessoa, nota fiscal, produto
--Explicando as tabelas: a tabela pessoa contem as pessoas independente se elas s�o os emitentes ou destinat�rios, 
--por isso � a primeira a ser criada sem chave estrangeira.
--Logo em seguida a NotaFiscal, em questao de inser��o dos dados no banco � necess�rio primeiro ver se os destinat�rio e emitente j� est�o no banco,
--caso algum ou ambos n�o estejam primeiro devemos inseri-los independente da ordem para depois termos ambas as chaves deles usadas na cria��o da nota fiscal.
--Por ultimo uma nota fiscal pode ter infinitos produtos, logo o produto � apenas atrelado a nota, ja que cada produto pertence a uma nota, porem cada nota pode ter
--infinitos produtos logo a rela��o � de 1 nota pra 'N' produtos.