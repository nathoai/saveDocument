CREATE DATABASE project1
GO

USE project1
GO

CREATE TABLE tb_sub
(
	id			int not null identity(1,1),
	code		varchar(100) not null,
	name		nvarchar(500) not null,
	stt			char,
	created		datetime,
	updated		datetime,
	primary key(id)
)
GO

CREATE TABLE tb_inf
(
	id			int not null identity(1,1),
	code		varchar(100) not null,
	name		nvarchar(500) not null,
	descr		nvarchar(500),
	pri			char,		
	created		datetime,
	updated		datetime,
	stt			char,
	primary key(id)
)
GO

CREATE TABLE tb_det
(
	id			int not null identity(1,1),
	id_sub		int not null,
	id_inf		int not null,
	timedet		int not null,
	code		varchar(100) not null,
	name		nvarchar(500) not null,
	descr		nvarchar(500),
	pri			char,		
	link		varchar(500),
	created		datetime,
	updated		datetime,
	stt			char,
	primary key(id),
	foreign key(id_sub) references tb_sub(id),
	foreign key(id_inf) references tb_inf(id)
)
GO


CREATE TABLE tb_per
(
	id			int not null identity(1,1),
	name		nvarchar(200) not null,
	descr		nvarchar(200),
	created		datetime,
	updated		datetime,
	stt			char,
	primary key(id)
)
GO

CREATE TABLE tb_user
(
	id			int not null identity(1,1),
	id_per		int not null,
	name		nvarchar(200) not null,
	username	varchar(50) not null,
	pass		varbinary(200) not null,
	created		datetime,
	updated		datetime,
	stt			char,
	primary key(id),
	foreign key(id_per) references tb_per(id)
)
GO

-- CREATE STRORE
CREATE PROCEDURE sp_insAcc
	@id_per		int,
	@name		nvarchar(200),
	@user		varchar(50),
	@pass		varchar(50),
	@key		varchar(10)
AS
	INSERT INTO tb_user(id_per, name, username, pass, created, stt) 
	VALUES (@id_per, @name, @user, ENCRYPTBYPASSPHRASE(@key, @pass), GETDATE(), 0)
GO

CREATE PROCEDURE sp_upAcc
	@id_per		int,
	@name		nvarchar(200),
	@user		varchar(50),
	@pass		varchar(50),
	@key		varchar(10),
	@id			int
AS
	UPDATE tb_user
	SET 
		id_per = @id_per, 
		name = @name, 
		username = @user,
		pass = ENCRYPTBYPASSPHRASE(@key, @pass), 
		updated = GETDATE()
	WHERE id = @id
GO

CREATE PROCEDURE sp_delAcc
	@id		int
AS
	UPDATE tb_user
	SET stt = 1
	WHERE id = @id
GO	

CREATE PROCEDURE sp_infAcc
	@user	varchar(50)
AS
	SELECT id FROM tb_user WHERE username = @user and stt = 0
GO

CREATE PROCEDURE sp_getAcc
AS
	SELECT a.id, a.name, a.username, a.pass, a.id_per, b.name as role
	FROM tb_user a JOIN tb_per b ON a.id_per = b.id
	WHERE a.stt = 0