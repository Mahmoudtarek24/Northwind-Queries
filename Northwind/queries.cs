using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Northwind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Northwind
{
	internal class queries
	{
		private NorthwindContext context;
		public queries()
		{
			context = new NorthwindContext();
		}
		public void Query1()
		{
			//Write a LINQ query to check if there are any products in the Products
			//table with a UnitPrice greater than 100.

			var query = context.Products.Any(e => e.UnitPrice > 100);
		}
		public void Query2()
		{
			//Write a LINQ query to check if all customers in the Customers table from "Germany"
			//have a non-null Phone number.

			var query = context.Customers.Where(e => e.Country == "Germany").All(e => e.Phone == null);
		}
		public void Query3()
		{
			//Write a LINQ query to retrieve the EmployeeID and LastName of the first employee in the Employees
			//table whose TitleOfCourtesy is "Dr.".

			var query = context.Employees.Where(e => e.TitleOfCourtesy == "Dr.").Select(e => new
			{
				EmployeeID = e.EmployeeId,
				First = e.FirstName,
			}).FirstOrDefault();
		}
		public void Query4()
		{
			//Write a LINQ query to retrieve the OrderID and OrderDate of the last order in the Orders table
			//placed in 1996.

			var query = context.Orders.Where(e => e.OrderDate.Value.Year == 1996).Select(e => new
			{
				OrderID = e.OrderId,
				OrderDate = e.OrderDate,
			}).LastOrDefault();
		}
		public void Query5()
		{
			//Write a LINQ query to retrieve the SupplierID and CompanyName of the only supplier in the
			//Suppliers table whose Country is "Japan". If there isn’t exactly one such supplier,
			//return a default object with SupplierID = -1 and CompanyName = "Not Found".

			var query = context.Suppliers.Where(e => e.Country == "Japan").Select(e => new
			{
				SupplierID = e.SupplierId,
				CompanyName = e.CompanyName,
			}).FirstOrDefault() ?? new { SupplierID = 1, CompanyName = "ali" };
		}
		public void Query6()
		{
			//Write a LINQ query to check if there are any orders in the Orders table with a Freight cost less than 10.

			var query = context.Orders.Any(e => e.Freight < 10);
		}
		public void Query7()
		{
			//Write a LINQ query to check if all products in the Products table
			//that are not discontinued (Discontinued is false) have UnitsInStock greater than 0.
			var query = context.Products
							 .Where(p => !p.Discontinued).All(p => p.UnitsInStock > 0);
		}
		public void Query8()
		{
			//Write a LINQ query to retrieve the CategoryID and CategoryName of the first category in the
			//Categories table whose Description contains "beverage" (case-insensitive).


			var query = context.Categories.Where(e => e.Description.Contains("beverage")).Select(e => new
			{
				CategoryID = e.CategoryId,
				CategoryName = e.CategoryName,
			}).FirstOrDefault();

		}
		public void Query9()
		{
			//Write a LINQ query to retrieve the ShipperID and CompanyName of the last shipper in the Shippers table
			//when sorted by CompanyName in alphabetical order.

			var query = context.Shippers.OrderBy(e => e.CompanyName).Select(e => new
			{
				ShipperID = e.ShipperId,
				CompanyName = e.CompanyName
			}).LastOrDefault();
		}
		public void Query10()
		{
			// Write a LINQ query to check if any customer in the Customers table has placed an order in 1997
			// with a Freight cost greater than 200.

			var query = context.Customers.Any(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1997 && e.Freight > 200));
		}
		public void Query11()
		{
			//Write a LINQ query to check if all orders in the Orders table placed by the employee with
			//EmployeeID = 5 were shipped within 10 days of the OrderDate (i.e., ShippedDate - OrderDate <= 10 days).
			//Handle cases where ShippedDate might be null.

			var query = context.Orders.Where(e => e.EmployeeId == 5)
				.All(o => (o.ShippedDate.Value - o.OrderDate.Value).Days <= 10);
		}
		public void Query12()
		{
			//Write a LINQ query to retrieve the CustomerID and CompanyName of the first customer in the Customers table
			//whose Country is "USA", sorted by CompanyName in ascending order.

			var query = context.Customers.Where(e => e.Country == "USA").Select(e => new
			{
				CustomerID = e.CustomerId,
				CompanyName = e.CompanyName,
			}).OrderBy(e => e.CompanyName).FirstOrDefault();
		}
		public void Query13()
		{
			//Write a LINQ query to retrieve the OrderID and Freight of the last order in the Orders table
			//placed by customers from "France" in 1998, sorted by Freight in descending order.

			var query = context.Orders.Where(e => e.Customer.Country == "France" && e.OrderDate.Value.Year == 1997)
				.Select(e => new
				{
					OrderID = e.OrderId,
					Freight = e.Freight,
				}).OrderBy(e => e.Freight).LastOrDefault();
		}
		public void Query14()
		{
			//Write a LINQ query to check if any order in the Orders table placed by an employee whose
			//Title contains "Sales" has a total order value (sum of UnitPrice * Quantity across all order details)
			//greater than 5000.

			var query=context.Orders.Where(e=>e.Employee.Title.Contains("Sales"))
				             .Any(e=>e.OrderDetails.Sum(e=>e.UnitPrice*e.Quantity) > 5000);
			
				             


		}
		public void Query15()
		{
			//Write a LINQ query to check if all products in the Products table with CategoryID = 1
			//have been ordered at least once in an order placed by a customer from "Spain".

			var query = context.Products.Where(e => e.CategoryId == 1)
					  .All(e => e.OrderDetails.Any(e => e.Order.Customer.Country == "Spain"));
		}
		public void Query16()
		{
			//Write a LINQ query to retrieve the SupplierID and CompanyName of the first supplier in the
			//Suppliers table who has at least one product with UnitsInStock greater than 50,

			//sorted by CompanyName in ascending order. If no such supplier exists,
			//return a default object with SupplierID = -1 and CompanyName = "Not Found".

			var query = context.Suppliers.Where(e => e.Products.Any(e => e.UnitPrice > 50)).Select(e => new
			{
				SupplierID = e.SupplierId,
				CompanyName = e.CompanyName
			}).OrderBy(e => e.CompanyName).FirstOrDefault() ?? new { SupplierID = -1, CompanyName = "Not Found" };

		}
		public void Query17()
		{
			//Write a LINQ query to retrieve the EmployeeID and LastName of the last employee in the Employees table
			//who has handled an order in 1998 with a total quantity (sum of Quantity across all order details) 
			//greater than 100, sorted by EmployeeID in descending order.

			var query = context.Employees
						.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1998 && e.OrderDetails.Sum(e => e.Quantity) > 100))
						.OrderByDescending(e => e.EmployeeId)
						.Select(e => new
						{
							EmployeeID = e.EmployeeId,
							LastName = e.LastName
						}).LastOrDefault();

		}
		public void Query18()
		{
			//Write a LINQ query to retrieve the ProductID and ProductName of the first product in the Products table
			//that has been ordered in an order with ShipCountry as "Canada" and Discount greater than 0,
			//sorted by ProductName in ascending order. If no such product exists, return null.

			var query = context.Products.Where(e => e.OrderDetails.Any(e => e.Order.ShipCountry == "Canada" && e.Discount > 0))
								.OrderBy(e => e.ProductName)
								.Select(e => new
								{
									ProductID = e.ProductId,
									ProductName = e.ProductName,
								}).FirstOrDefault();

		}
		public void Query19() {
			//Write a LINQ query to check if all employees in the Employees table who have handled orders in 1997
			//have at least one order with a Freight cost less than 100.

			var query=context.Employees.Where(e=>e.Orders.Any(e=>e.OrderDate.Value.Year==1997))
							 .All(e => e.Orders.Any(o => o.OrderDate.Value.Year == 1997 && o.Freight < 100));
			 
		}
		public void Query20() {
			//Write a LINQ query to retrieve the CustomerID and CompanyName of the only customer in
			//the Customers table who has placed exactly 5 orders in 1996. If there isn’t exactly one such customer,
			//return a default object with CustomerID = "N/A" and CompanyName = "Not Found".

			var query = context.Customers
						.Where(c => c.Orders.Count(o => o.OrderDate.Value.Year == 1996) == 5)
						.Select(c => new { CustomerID= c.CustomerId, CompanyName = c.CompanyName })
						.SingleOrDefault() ?? new { CustomerID = "N/A", CompanyName = "Not Found" };

		}
		public void Query21()
		{
			///Write a LINQ query to retrieve the OrderID and a calculated field 
			///TotalOrderValue (where TotalOrderValue is the sum of UnitPrice *
			///Quantity across all order details for the order) 
			///of the last order in the Orders table placed by customers whose Country is "UK" in 1997, 
			///sorted by TotalOrderValue in descending order.


		}
		public void Query22() {
			//Write a LINQ query to check if all orders in the Orders table placed in 1996 have a non-null ShippedDate.

			var query = context.Orders.All(e => e.OrderDate.Value.Year == 1996);


		}
		public void Query23() {
			//Write a LINQ query to retrieve the ProductID and ProductName of the first product in
			//the Products table whose UnitPrice is less than 20.

			var query = context.Products.Where(e => e.UnitPrice < 20).Select(e=>new
			{
				ProductID=e.ProductId,
				ProductName=e.ProductName,
			}).FirstOrDefault();
		}
		public void Query24() {
			//Write a LINQ query to retrieve the SupplierID and CompanyName of the last supplier in the
			//Suppliers table when sorted by SupplierID in ascending order.

			var query = context.Suppliers.Select(e => new
			{
				SupplierID = e.SupplierId,
				CompanyName = e.CompanyName,
			}).OrderBy(e => e.SupplierID).LastOrDefault();
		}
		public void Query25() {
			//Write a LINQ query to check if any employee in the Employees table has handled an order in 1997
			//with a Freight cost greater than 150.

			var query = context.Employees
				       .Any(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1997 && e.Freight > 150));
		}
		public void Query26() {
			//Write a LINQ query to check if all products in the Products table with CategoryID = 3
			//have UnitsInStock greater than 5.

			var query = context.Products.Where(e => e.CategoryId == 3).All(p => p.UnitsInStock > 5);
		}
		public void Query27() {
			//Write a LINQ query to retrieve the CustomerID and CompanyName of the first customer
			//in the Customers table who has placed an order in 1998, sorted by CompanyName in ascending order.

			var query = context.Customers.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1998))
								  .Select(e => new
								  {
									  CustomerID = e.CustomerId,
									  CompanyName = e.CompanyName
								  }).OrderBy(e => e.CompanyName).FirstOrDefault();
		}
		public void Query28() {
			//Write a LINQ query to retrieve the OrderID and OrderDate of the last order in the Orders table
			//placed by customers whose Country is "Italy", sorted by OrderDate in descending order.

			var query = context.Orders.Where(e => e.Customer.Country == "Italy").Select(e => new
			{
				OrderID = e.OrderId,
				OrderDate = e.OrderDate
			}).OrderByDescending(e => e.OrderDate).LastOrDefault();
		}
		public void Query29() {
			//Write a LINQ query to check if any product in the Products table has been ordered in an
			//order placed by a customer from "Germany" with a Quantity greater than 30 and a Discount greater than 0.

			var query = context.Products
				 .Any(e => e.OrderDetails.Any(e => e.Order.Customer.Country == "Germany" && e.Quantity > 30 && e.Discount > 0));
		}
		public void Query30() {
			//عايزين نعرف هل كل الموظفين اللي اشتغلوا على طلبات في سنة 1998 عندهم على الأقل طلب واحد
			//في نفس السنة، تكون قيمته أكبر من 2000 ولا لأ؟
			//Employee have orders in 1998 //الموضف بالميت عنده طلب بالتاريخ ده 
			var query = context.Employees.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1998))
							 ;


           // بعد اما جبنا الموضفين الي اشتغلو علي طلب في سنة 1998
		   // هل كل موضف من دوا عنده طلب  قيكته اكبر من 2000
		}
		public void Query31() {
			//Write a LINQ query to retrieve the EmployeeID and LastName of the first employee in the Employees table
			//who has handled an order in 1997 with a Freight cost greater than 100,
			//sorted by LastName in ascending order. If no such employee exists,
			//return a default object with EmployeeID = -1 and LastName = "Not Found".




		}
		public void Query32() {
			//Check if all customers who placed orders in 1997 have at least one order with a
			//Freight cost greater than 150.

			var query = context.Customers.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1997))
									.All(e => e.Orders
										.Where(e =>  e.OrderDate.Value.Year == 1997)
										.Any(e => e.Freight > 150));

		}
		public void Query33()
		{

			//	Check if all employees who handled orders in 1996 have at least one order that contains
			//	more than 3 different products(i.e., order details count > 3).

			var query = context.Employees.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1996))
								  .All(e => e.Orders.Where(e => e.OrderDate.Value.Year == 1996)
								  .Any(e => e.OrderDetails.Count() > 3));

		}
		public void Query34() {
			//Check if all customers who placed orders in 1997 have at least one order with
			//a Freight cost greater than 150.

			var query = context.Customers.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1997))
						  .All(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1997 && e.Freight > 150));

		}
		public void Query35() {
			//Check if all employees who handled orders in 1996 have at least one order
			//that contains more than 3 products (OrderDetails.Count > 3).

			var query = context.Employees.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1996))
						  .All(e => e.Orders.Where(e => e.OrderDate.Value.Year == 1996)
						  .Any(e => e.OrderDetails.Count > 3));

			var result = context.Employees
				.Where(e => e.Orders.Any(o => o.OrderDate.Value.Year == 1996))
				.All(e => e.Orders.Any(o => o.OrderDate.Value.Year == 1996 && o.OrderDetails.Count > 3));

		}
		public void Query36() {
			//Check if all suppliers who have products in stock (UnitsInStock > 0)
			//have at least one product with UnitPrice greater than 50.

			var query = context.Suppliers
						.Where(s => s.Products != null && s.Products.Any(p => p.UnitsInStock > 0))
						.All(s => s.Products.Any(p => p.UnitPrice > 50));

		}
		public void Query37() {
			//Check if all customers who ordered in 1998 have at least one order shipped
			//to a different country than their own.

			var query = context.Customers.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1998))
						  .All(e => e.Orders.Any(o => o.OrderDate.Value.Year == 1998 && o.ShipCountry != e.Country));
		}
		public void Query38()
		{
			//Check if all employees who handled orders in 1997 have at least one order with a
			//total order value greater than 3000.

			var query = context.Employees.Where(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1997))
							 .All(e => e.Orders.Any(e => e.OrderDate.Value.Year == 1997
							 && e.OrderDetails.Sum(e => e.Quantity * e.UnitPrice) > 3000));
			                       


		}
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }
		//public void Query1() { }

	}
}
