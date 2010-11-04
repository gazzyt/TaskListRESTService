create table tasklist (
	id binary(16) PRIMARY KEY,
	name varchar(100) NOT NULL
	);
	
create table task (
	id binary(16) PRIMARY KEY,
	tasklist binary(16) NOT NULL,
	name varchar(100) NOT NULL,
	description varchar(254) NULL,
	due datetime,
	complete boolean,
	CONSTRAINT fk_task_tasklist FOREIGN KEY (tasklist) REFERENCES tasklist (id)
	);
	
	