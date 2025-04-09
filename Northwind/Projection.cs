using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Northwind.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace Northwind
{
	internal class Projection
	{
		private NorthwindContext context;
		public Projection()
		{
			context = new NorthwindContext();
		}

		public void Query1()
		{
			//retrieve a list of all product names from the Products table.
			var products = context.Products.Select(e => e).ToList();

			foreach (var product in products)
			{
				Console.WriteLine(product.ProductName);
			}
		}
		public void Query2()
		{
			//create a query that returns a list of anonymous objects containing the ProductName
			//and UnitPrice for all products in the Products table
			var products = context.Products.Select(e => new { ProductName = e.ProductName, UnitPrice = e.UnitPrice }).ToList();
		}
		public void Query3()
		{
			//Write a query that uses Select to get the CompanyName and ContactName of
			//customers from the Customers table who are located in "Germany".
			var query = context.Customers.Where(e => e.Country == "Germany")
				   .Select(e => new { CompanyName = e.CompanyName, ContactName = e.ContactName }).ToList();
		}
		public void Query4()
		{
			//Use Select to retrieve a list of OrderID and OrderDate from the Orders table,
			//where the OrderDate is formatted as a string in the "MM/dd/yyyy" format.

			var orderDetails = context.Orders
							.Select(o => new
							{
								OrderID = o.OrderId,
								OrderDate = o.OrderDate.HasValue ? o.OrderDate.Value.ToString("MM/dd/yyyy") : "N/A"
							})
							.ToList();
		}
		public void Query5()
		{
			//Write a LINQ query with Select to calculate the total value of each product
			//in stock (i.e., UnitPrice * UnitsInStock) from the Products table, returning
			//the ProductName and the calculated total.

			var query = context.Products.Select(e => new
			{
				ProductName = e.ProductName,
				totalvalue = e.UnitPrice * e.UnitsInStock
			}).ToList();

		}
		public void Query6()
		{
			//Write a LINQ query using Select to retrieve a list of all ProductName and UnitPrice
			//values from the Products table where the UnitPrice is greater than 20.
			var query = context.Products.Where(e => e.UnitPrice > 20)
				  .Select(e => new { ProductName = e.ProductName, UnitPrice = e.UnitPrice }).ToList();

		}
		public void Query7()
		{
			//Use Select to get the CompanyName and ContactName of customers from the
			//Customers table where the City is "London".
			var query = context.Customers.Where(e => e.City == "London")
				.Select(e => new { CompanyName = e.CompanyName, ContactName = e.ContactName }).ToList();
		}
		public void Query8()
		{
			//Write a query with Select to retrieve the OrderID and OrderDate from the Orders table,
			//where the OrderDate is transformed into a string formatted as "yyyy-MM-dd",
			//and only include orders from 1998.
			var query = context.Orders
						.Where(e => e.OrderDate.HasValue && e.OrderDate.Value.Year == 1998)
						.Select(e => new
						{
							OrderID = e.OrderId,
							OrderDate = e.OrderDate.HasValue ? e.OrderDate.Value.ToString("MM/dd/yyyy") : "N/A"
						}).ToList();
		}
		public void Query9()
		{
			//Use Select to create a list of ProductName and a calculated field called
			//PriceWithTax(where PriceWithTax = UnitPrice * 1.20) from the Products table
			//for products that are not discontinued(Discontinued == false)

			var query = context.Products.Where(e => !e.Discontinued)
				.Select(e => new { ProductName = e.ProductName, PriceWithTax = e.UnitPrice.HasValue ? 1.20 : default }).ToList();
		}
		public void Query10()
		{
			//Write a LINQ query with Select to retrieve the EmployeeID and a custom field TitleCategory
			//from the Employees table, where TitleCategory is "Sales" if the Title contains "Sales"
			//(e.g., "Sales Representative"), and "Other" otherwise.
			var query = context.Employees
					.Select(e => new
					{
						EmployeeID = e.EmployeeId,
						TitleCategory = e.Title != null && e.Title.Contains("Sales", StringComparison.OrdinalIgnoreCase) ? "Sales" : "Other"
					})
					.ToList();
		}
		public void Query11()
		{
			//Retrieve All Product Names Ordered by a Specific Customer
			//Write a LINQ query using SelectMany to retrieve a list of ProductName(from the Products table)
			//for all products ordered by a specific customer(e.g., CustomerID = 'ALFKI').Include the OrderID
			//from the Orders table in the result.
			var query = context.Customers.Where(e => e.ContactName == "ALFKI").SelectMany(e => e.Orders)
				.SelectMany(e => e.OrderDetails).Select(od => new
				{
					OrderID = od.Order.OrderId,
					ProductName = od.Product.ProductName
				}).ToList();
		}
		public void Query12()
		{
			//Question 2: Get Total Quantity of Products Ordered in a Specific Year
			//Write a LINQ query using SelectMany to calculate the total Quantity of products
			//ordered in 1997 across all orders.Return a list of ProductID and the total Quantity for each product.
			//Hint: Start with Orders, filter by year, use SelectMany to flatten the Order Details, and group by ProductID.

			var query = context.Orders.Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1997)
				.SelectMany(e => e.OrderDetails).Select(e => new
				{
					Quantity = e.Quantity,
					ProductID = e.ProductId,
				}).ToList();
		}
		public void Query13()
		{
			//Question 3: List All Employees Who Handled Orders for a Specific Category
			//Write a LINQ query using SelectMany to retrieve a list of EmployeeID and LastName(from the Employees table)
			//for employees who handled orders that include products from a specific
			//category(e.g., CategoryID = 1).Include the CategoryName in the result.
			//Hint: Start with Employees, use SelectMany to access their Orders,
			//then Order Details, and finally join with Products and Categories.

			//var query=context.Categories.Where(e=>e.CategoryName=="").SelectMany(e=>e.Products).SelectMany(e=>e.OrderDetails)
			//	.Select(e => e.Order).Select(e => new
			//	{
			//		EmployeeId=e.EmployeeId,	
			//		LastName=e.Employee.LastName,
			//		CategoryName=e
			//	})


		}
		public void Query14()
		{
			//List All Order Details for Orders Handled by a Specific Employee
			//Write a LINQ query using SelectMany to retrieve a list of OrderID and Quantity(from OrderDetails)
			//for all orders handled by an employee with EmployeeID = 5.


			var query = context.Orders.Where(e => e.EmployeeId == 5).SelectMany(e => e.OrderDetails)
									.Select(e => new
									{
										OrderID = e.OrderId,
										Quantity = e.Quantity
									}).ToList();

			foreach (var OrderDetail in query)
			{
				Console.WriteLine($"{OrderDetail.Quantity}  {OrderDetail.OrderID}");
			}

		}
		public void Query15()
		{
		

		}
		public void Query16()
		{
			//Write a LINQ query to retrieve a list of ProductName and a calculated field
			//StockValue (where StockValue = UnitPrice * UnitsInStock) from the Products table.
			//Only include products where UnitsInStock is greater than 50 and UnitPrice is not null.


			var query = context.Products.Where(e => e.UnitsInStock.HasValue && e.UnitsInStock.Value > 50 && e.UnitPrice.HasValue)
				.Select(e => new { ProductName = e.ProductName, StockValue = e.UnitPrice * e.UnitsInStock }).ToList();

		}
		public void Query17()
		{
			//Write a LINQ query to retrieve a list of OrderID and Quantity from the Order Details table
			//for all orders placed by customers whose Country is "Germany". Only include orders where
			//the Quantity is greater than 20.


			var query = context.Customers.Where(e => e.Country == "Germany").SelectMany(e => e.Orders)
				.SelectMany(e => e.OrderDetails).Where(e => e.Quantity > 20)
				.Select(e => new { OrderID = e.OrderId, Quantity = e.Quantity }).ToList();

		}
		public void Query18()
		{
			//Write a LINQ query to retrieve a list of EmployeeID, LastName, and HireDate from the Employees table.
			//Sort the results by HireDate in ascending order, and for employees hired on the same date,
			//sort by LastName in alphabetical order.

			var query = context.Employees.Select(e => new
			{
				EmployeeID = e.EmployeeId,
				LastName = e.LastName,
				HireDate = e.HireDate
			}).OrderBy(e => e.HireDate).ThenBy(e => e.LastName).ToList();

		}
		public void Query19()
		{
			//Write a LINQ query to retrieve a list of CustomerID and CompanyName from the Customers table.
			//Skip the first 10 customers and take the next 5 customers, ordered alphabetically by CompanyName.


			var query = context.Customers
				.Select(e => new { CustomerID = e.CustomerId, CompanyName = e.CompanyName }).OrderBy(e => e.CompanyName)
				.Skip(10).Take(5).ToList();
		}
		public void Query20()
		{
			//Write a LINQ query to retrieve the SupplierID and CompanyName of the first supplier
			//(from the Suppliers table) whose Country is "Japan". If no such supplier exists
			//, return a default object with SupplierID = -1 and CompanyName = "Not Found".


			var query = context.Suppliers.Where(e => e.Country == "Japan")
				.Select(e => new { SupplierID = e.SupplierId, CompanyName = e.CompanyName })
				.FirstOrDefault() ?? new { SupplierID = -1, CompanyName = "Not Found" };



		}
		public void Query21()
		{
			//Write a LINQ query to check if there are any orders in the Orders table that were
			//shipped to "France" and have a Freight cost greater than 100.Return a boolean result.

			var query = context.Orders.Any(e => e.ShipCountry == "France" && e.Freight > 100);

		}
		public void Query22()
		{
			//Write a LINQ query to check if all products in the Products table with UnitsInStock
			//greater than 0 also have a UnitPrice greater than 10.Return a boolean result.

			var query = context.Products
								.Where(p => p.UnitsInStock > 0)
								.All(p => p.UnitPrice > 10);
		}
		public void Query23()
		{
			// Write a LINQ query to count the number of orders in the Orders table placed
			// in 1997 where the ShipVia(shipper) is 1.

			var query = context.Orders.Where(e => e.OrderDate.Value.Year == 1997 && e.ShipVia == 1).Count();
			Console.WriteLine(query);


		}
		public void Query24() {
			//Write a LINQ query to calculate the total Freight cost for all orders in the Orders table
			//placed by the employee with EmployeeID = 3.

			var query = context.Orders.Where(e => e.EmployeeId == 3).Sum(e => e.Freight);
		}
		public void Query25() {
			//Write a LINQ query to calculate the average UnitPrice of products in the Products table
			//that belong to CategoryID = 2 and are not discontinued (Discontinued = false).

			var query = context.Products.Where(e=>e.CategoryId==2&&!e.Discontinued).Average(e => e.UnitPrice);
		}
		public void Query26() {
			//Write a LINQ query to find the minimum UnitPrice of products in the Products table supplied
			//by suppliers from "USA". If no such products exist, return 0.

			var query=context.Products.Where(e=>e.Supplier!=null && e.Supplier.Country == "USA")
									  .Select(e => e.UnitPrice).DefaultIfEmpty(0).Min();
		}
		
		public void Query27() {
			//Write a LINQ query to find the maximum Freight cost among all orders in the Orders table
			//that were shipped in 1998.

			var query = context.Orders.Where(e => e.ShippedDate.Value.Year == 1998).Max(e => e.Freight);
		}
		public void Query28(){
			//Write a LINQ query to retrieve a distinct list of Country values from the
			//Customers table where the Region is not null, ordered alphabetically.

			var query = context.Customers.Where(e => e.Region != null).Select(e => e.Country).Distinct()
				.OrderBy(e => e).ToList();
		
		}
		public void Query29(){
			//Write a LINQ query to retrieve a list of ProductID and a calculated field
			//TotalValue (where TotalValue = UnitPrice * Quantity) from the Order Details table
			//for all orders placed by customers in "London". Only include order details where Discount is 0.

		       var query=context.Customers.Where(e=>e.City== "London")
								.SelectMany(e => e.Orders).SelectMany(e=>e.OrderDetails)
								.Select(e=>new
								{
									TotalValue=e.UnitPrice*e.Quantity,
									ProductID=e.ProductId
								}).ToList();


		}
		public void Query30(){
			//Write a LINQ query to retrieve the CategoryID and CategoryName of the category
			//with the description "Seafood" from the Categories table. If no such category exists, return null.

			var query = context.Categories.Where(e => e.Description == "Seafood").Select(e => new
			{
				CategoryID = e.CategoryId,
				CategoryName = e.CategoryName,
			}).FirstOrDefault();

		}
		public void Query31(){
			//Write a LINQ query to retrieve a list of OrderID and OrderDate from the Orders table
			//for orders placed by the customer with CustomerID = 'VINET'.
			//Sort the results by OrderDate in descending order.

			var query = context.Customers.Where(e => e.CustomerId == "VINET").SelectMany(e => e.Orders)
									   .Select(e => new { OrderID = e.OrderId, OrderDate = e.OrderDate })
									   .OrderByDescending(e => e.OrderDate).ToList();
		
		}
		public void Query32(){
			//Write a LINQ query to retrieve the first 10 ProductID and Quantity pairs from the Order Details table
			//for all orders handled by employees whose Title contains "Sales".
			//Order the results by Quantity in descending order.

			var query=context.Employees.Where(e=>e.Title.Contains("Sales")).SelectMany(e=>e.Orders)
									   .SelectMany(e => e.OrderDetails).Select(e =>new
									   {
										   ProductID=e.ProductId,
										   Quantity=e.Quantity,
									   }).OrderByDescending(e => e.Quantity).Take(10).ToList();

		}
		public void Query33(){
			// Write a LINQ query to calculate the total Quantity of products ordered across all orders
			// placed in "Brazil".Return a single integer value.

			var query = context.Orders.Where(e => e.ShipCountry == "Brazil").SelectMany(e => e.OrderDetails)
				.Sum(e => e.Quantity);
		}
		public void Query34(){
			//Write a LINQ query to check if any employee with TitleOfCourtesy = 'Mr.'
			//has handled an order that includes a product with UnitPrice greater than 50. Return a boolean result.

			var query = context.Employees.Where(e => e.TitleOfCourtesy == "Mr.").SelectMany(e => e.Orders)
										.SelectMany(e => e.OrderDetails).Any(e => e.UnitPrice > 50);

		}
		public void Query35()
		{
			//Write a LINQ query to retrieve a list of OrderID and a calculated field TotalOrderValue
			//(where TotalOrderValue is the sum of UnitPrice * Quantity across all order details for the order)
			//from the Orders table. Only include orders placed in 1997 by customers whose Country is "UK",
			//and ensure the TotalOrderValue is greater than 1000. Sort the results by TotalOrderValue
			//in descending order.


			var quewry = context.Customers.Where(e => e.Country == "UK")
				.SelectMany(e => e.Orders).Where(e => e.OrderDate.Value.Year == 1997).Select(e => new
				{
					OrderID = e.OrderId,
					TotalOrderValue = e.OrderDetails.Sum(e => e.UnitPrice * e.Quantity)
				}).Where(e => e.TotalOrderValue > 1000)
				.OrderByDescending(e => e.TotalOrderValue).ToList();
		}
		public void Query36(){
			//Write a LINQ query to retrieve a list of CustomerID and CompanyName from the Customers table.
			//Only include customers whose Country is "France" and whose City is not "Paris".
			//Sort the results by CompanyName in alphabetical order.

			var query=context.Customers.Where(e=>e.Country== "France"&&e.City!= "Paris")
				                       .Select(e=>new
									   {
										   CustomerID=e.CustomerId,
										   CompanyName=e.CompanyName,	

									   }).OrderBy(e=>e.CompanyName).ToList();	


		}
		public void Query37(){
			//Write a LINQ query to retrieve a list of EmployeeID and a calculated field AverageOrderFreight
			//(where AverageOrderFreight is the average Freight cost across all orders handled by the employee)
			//from the Employees table. Only include employees who have handled orders in 1997 where
			//the ShipCountry is "USA", and ensure the AverageOrderFreight is greater than 100.
			//Sort the results by AverageOrderFreight in ascending order.

			var query = context.Employees.Where(e=>e.Orders.Any(e=>e.OrderDate.Value.Year==1997&&e.ShipCountry=="USA"))
					    .Select(e => new
					   {
					   EmployeeID=e.EmployeeId,
					   AverageOrderFreight=e.Orders.Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1997 && o.ShipCountry == "USA")
                      .Average(o => o.Freight.HasValue ? o.Freight.Value : 0)
				      }).Where(e=>e.AverageOrderFreight>100).OrderBy(e=>e.AverageOrderFreight).ToList();

		}
		public void Query38(){
			//Write a LINQ query to retrieve a list of OrderID and a calculated field TotalItems
			//(where TotalItems is the sum of Quantity across all order details for the order) from the Orders table.
			//Only include orders placed in 1998 where the ShipVia (shipper) is 2,
			//and ensure the TotalItems is greater than 50. Sort the results by TotalItems in descending order.

			var query=context.Employees.Where(e=>e.Orders.Any(e=>e.OrderDate.Value.Year==1998&&e.ShipVia==2))
						   .SelectMany(e => e.Orders).Select(e => new
						   {
							   OrderID=e.OrderId,
							   TotalItems=e.OrderDetails.Sum(e=>e.Quantity)
						   }).Where(e=>e.TotalItems>50).OrderByDescending(e=>e.TotalItems).ToList();


			foreach (var x in query)
			{
				Console.WriteLine(x.OrderID + "   " + x.TotalItems);
			}
		}


		public void Query39(){

			var query = context.Orders
		.Where(o => o.OrderDate.Value.Year == 1998 && o.ShipVia == 2)
		.Select(o => new
		{
			OrderID = o.OrderId,
			TotalItems = o.OrderDetails.Sum(od => od.Quantity)
		})
		.Where(x => x.TotalItems > 50)
		.OrderByDescending(x => x.TotalItems)
		.ToList();


			foreach(var x in query)
			{
				Console.WriteLine(x.OrderID+"   "+x.TotalItems);
			}
		}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
		//public void Query28(){}
	}
}
