CRIAÇÃO DO BANCO DE DADOS

1 - Criar um servidor chamado Vertrau com senha 123
2 - Criar um banco chamado bdVertrau

3 - Executar script de criação da tabela de usuário:

CREATE TABLE usuario
(
    id SERIAL PRIMARY KEY,
    nome character varying(150) NOT NULL,
    sobrenome character varying(150),
    email character varying(150) NOT NULL,
    genero integer NOT NULL,
    datanascimento date,
    CONSTRAINT usuarios_email_key UNIQUE (email)
);


4 - Executar script de criação da tabela de endereço:

CREATE TABLE endereco
(
    id SERIAL PRIMARY KEY,
    cep character varying(9) COLLATE pg_catalog."default" NOT NULL,
    estado character varying(2) COLLATE pg_catalog."default" NOT NULL,
    rua character varying(150) COLLATE pg_catalog."default" NOT NULL,
    bairro character varying(150) COLLATE pg_catalog."default" NOT NULL,
    numero integer NOT NULL,
    complemento character varying(150) COLLATE pg_catalog."default",
    idusuario integer NOT NULL,
    CONSTRAINT endereco_idusuario_fkey FOREIGN KEY (idusuario)
        REFERENCES public.usuario (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);




5 - Executar o .NET no visual studio (Não consegui configurar o banco no Docker, por isso não usei)

6 - No visual studio code, executar o comando: "docker run -p 4200:4200 angular-docker"