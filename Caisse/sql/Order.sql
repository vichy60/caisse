

Create Table Orders
idOrder int not null auto increment,
dateOrder Date default CURRENT_TIMESTAMP,
status varchar(14) NOT NULL CHECK (status IN ("Waiting","Confirmed","Canceled", "Error")) ;




Create Table ProductOrders
price decimal default 0,
/* productQuantity tinyint default 0, */
idOrder foreign key Order.idOrder,
idProduct foreign key product.idProduct ;



Select sum(price * productQuantity) as totalTTC from Orders as ticket
    left join ProductOrder prodOrd on ticket.idOrder = prodOrd.idOrder group by idOrder ;



delete from ProductOrders where idOrder = ? and productID = ? ;