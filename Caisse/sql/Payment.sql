Create table Payment (
idPayment int auto increment,
datePayment DateTime default CURRENT_TIMESTAMP,
amountPayment decimal default 0,

amountINPayment decimal default 0,
amountOUTPayment decimal default 0,

typePayment varchar(14) NOT NULL CHECK (name IN ("CashPayment","CardPayment"),
idOrder foreign key Orders.idOrder );