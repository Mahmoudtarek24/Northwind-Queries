using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind
{
	public class JoinMethod
	{
		private NorthwindContext context;
		public JoinMethod()
		{
			context = new NorthwindContext();
		}

		public void Query1() {
			//Write a LINQ query to join the Orders table with the Customers table on CustomerID
			//and retrieve a list of OrderID (from Orders) and CompanyName (from Customers).

			var query = context.Orders
								.Join(context.Customers,
									  order => order.CustomerId,
									  customer => customer.CustomerId,
									  (order, customer) => new
									  {
										  order.OrderId,
										  customer.CompanyName
									  });

			var result = from order in context.Orders
						 join customer in context.Customers
						 on order.CustomerId equals customer.CustomerId
						 select new
						 {
							 order.OrderId,
							 customer.CompanyName
						 };
		} 
		public void Query2() {
			//Write a LINQ query to join the Order Details table with the Products table on ProductID
			//and retrieve a list of OrderID (from Order Details) and ProductName (from Products).


			var query = context.OrderDetails.Join(context.Products,
				orderDetails => orderDetails.ProductId,
				product => product.ProductId, (orderDetails, product) => new
				{
					OrderID = orderDetails.OrderId,
					ProductName = product.ProductName,

				});


			var result = from orderDetails in context.OrderDetails
						 join product in context.Products
						 on orderDetails.ProductId equals product.ProductId
						 select new
						 {
							 OrderID = orderDetails.OrderId,
							 ProductName = product.ProductName,
						 };
		}
		public void Query3() {
			//Write a LINQ query to join the Orders table with the Employees table on EmployeeID
			//and retrieve a list of OrderID (from Orders) and LastName (from Employees).


			var query =context.Orders.Join(context.Employees, 
				order=>order.EmployeeId,
				employee=>employee.EmployeeId,
				(order, employee) =>new
				{
					OrderID=order.OrderId,
					LastName=employee.LastName,
				}
				);	

		}
		public void Query4() {
			//Write a LINQ query to join the Products table with the Categories table on CategoryID
			//and retrieve a list of ProductName (from Products) and CategoryName (from Categories).

			var query = context.Products.Join(context.Categories, product => product.CategoryId, catgory => catgory.CategoryId,
							   (product, catgory) => new
							   {
								   ProductName = product.ProductName,
								   CategoryName = catgory.CategoryName,
							   });
		}
		public void Query5() {
			//Write a LINQ query to join the Orders table with the Shippers table on ShipVia
			//(where ShipVia matches ShipperID) and retrieve a list of OrderID (from Orders)
			//and CompanyName (from Shippers).

			var query = context.Orders.Join(context.Shippers, order => order.ShipVia, shipper => shipper.ShipperId,
				(order, shippper) => new
				{
					order = order.OrderId,
					companyname = shippper.CompanyName,
				});
		}

		public void Query6() {
			//Write a LINQ query to join the Products table with the Suppliers table on SupplierID
			//and retrieve a list of ProductID (from Products) and CompanyName (from Suppliers).


			var query = context.Products.Join(context.Suppliers, product => product.SupplierId, suppliers => suppliers.SupplierId,
				  (product, supplier) => new
				  {
					  ProductID = product.ProductId,
					  CompanyName = supplier.CompanyName,
				  });
		}
		public void Query7() {
			//Write a LINQ query to join the Orders table with the Customers table on CustomerID
			//and retrieve a list of OrderID (from Orders) and Country (from Customers).

			var query = context.Orders.Join(context.Customers, order => order.CustomerId, customer => customer.CustomerId,
				(order, customer) => new
				{
					orderId = order.OrderId,
					country = customer.Country

				});

		}
		public void Query9() {
			//Write a LINQ query to join the Employees table with the Orders table on EmployeeID
			//and retrieve a list of FirstName (from Employees) and OrderID (from Orders).


			var query = context.Employees.Join(context.Orders, employee => employee.EmployeeId, order => order.EmployeeId,
				(employee, order) => new
				{
					FirstName = employee.FirstName,
					OrderID = order.OrderId,
				});

		}
		public void Query10() {
			//Write a LINQ query to join the Orders table with the Customers table on CustomerID
			//and retrieve a list of OrderID (from Orders) and City (from Customers).

			var query = context.Orders.Join(context.Customers, order => order.CustomerId, customer => customer.CustomerId,
				(order, customer) => new
				{
					OrderID = order.OrderId,
					City = customer.City,
				});
		}
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
	}
}
