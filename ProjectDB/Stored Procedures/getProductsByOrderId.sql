CREATE PROCEDURE [dbo].[getProductsByOrderId]
	@OrderId int = 0
AS
Begin
	SELECT op.price as opPrice, op.order_id, op.product_id, op.quantity, p.name, p.price as productPrice FROM orderproduct op INNER JOIN product p on op.product_id=p.product_id
	Where op.order_id=@OrderId
End
