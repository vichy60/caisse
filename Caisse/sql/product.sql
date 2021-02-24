Create Table product 
idProduct int auto increment,
title VARCHAR(128) not null default "",
price decimal not null default 0, 
stock TINYINT not null default 0 ;