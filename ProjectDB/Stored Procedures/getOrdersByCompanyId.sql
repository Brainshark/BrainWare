CREATE PROCEDURE [dbo].[getOrdersByCompanyId]
	@CompanyId int = 0
AS
Begin
	SELECT c.name, o.description, o.order_id FROM company c INNER JOIN [order] o on c.company_id=o.company_id
	Where c.company_id=@CompanyId
End
