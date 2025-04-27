using Northwind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Northwind
{
	internal class GroupByQueries
	{
		private NorthwindContext context;
		public GroupByQueries()
		{
			context = new NorthwindContext();
		}

		public void Query1(){
			//Write a LINQ query to group the Products table by CategoryID and retrieve a list
			//of CategoryID and the count of products in each category.

			var query = context.Products.GroupBy(e => e.CategoryId).Select(e => new
			{
				CategoryID=e.Key,
				countOfProduct=e.Count()
			}).ToList();

			var query2 = from p in context.Products
						 group p by p.CategoryId into g
						 select new { CategoryID = g.Key, ProductCount = g.Count() };

		}
		public void Query2(){
			//Write a LINQ query to group the Orders table by ShipCountry and retrieve a
			//list of ShipCountry and the number of orders shipped to each country.

			var query = context.Orders.GroupBy(e => e.ShipCountry).Select(e => new
			{
				ShipCountry=e.Key,
				NofOrderShipped= e.Count()
			}).ToList();

			var query2 = from o in context.Orders
						 group o by o.ShipCountry into g
						 select new { ShipCountry = g.Key, NofOrderShipped = g.Count() };

		}
		public void Query3(){
			//Write a LINQ query to group the Customers table by Country and retrieve a list of Country
			//and the count of customers from each country. Sort the results by the count of customers
			//in descending order.

			var query=context.Customers.GroupBy(e=>e.Country).Select(e=>new
			{
				listCountry=e.Key,
				countcustomers=e.Count()
			}).OrderByDescending(e=>e.countcustomers).ToList();

			var result = from c in context.Customers
						 group c by c.Country into g
						 select new { listCountry = g.Key, countcustomers = g.Count() };

		}
		public void Query4(){
			//Write a LINQ query to group the Employees table by Title and retrieve a list of Title
			//and the number of employees with each title. Sort the results by Title in alphabetical order.

			var query = context.Employees.GroupBy(e => e.Title).Select(e => new
			{
				listofTitle = e.Key,
				numberofEmployees = e.Count()
			}).ToList();
		}
		public void Query5(){
			//Write a LINQ query to group the Orders table by the year of OrderDate and
			//retrieve a list of the year and the number of orders placed in each year.
			//Sort the results by year in ascending order.

			var query = context.Orders.GroupBy(e => e.OrderDate.Value.Year).Select(e => new
			{
				OrdersYears = e.Key,
				Number = e.Count()
			}).OrderBy(e => e.OrdersYears).ToList();

		}
		public void Query6(){
			//Write a LINQ query to group the Products table by Discontinued status and
			//retrieve a list of Discontinued (true/false) and the count of products for each status.

			var query = context.Products.GroupBy(e => e.Discontinued).Select(e => new
			{
				Discontinued = e.Key,
				CountdownEvent = e.Count()
			}).ToList();
		}
		public void Query7(){
			//Write a LINQ query to group the Suppliers table by Country and
			//retrieve a list of Country and the number of suppliers from each country.
			//Only include countries with more than 2 suppliers,
			//and sort the results by the count of suppliers in descending order.


			var query = context.Suppliers.GroupBy(e => e.Country).Select(e => new
			{
				Countrys=e.Key,
				NumberCountry = e.Count()
			}).Where(e=>e.NumberCountry>2).OrderByDescending(g => g.NumberCountry).ToList();


			var result=(from s in context.Suppliers
					   group s by s.Country  into g
					   where g.Count()>2
					   select new
					   {
						   Country = g.Key,
						   SupplierCount = g.Count()
					   }).OrderByDescending(g => g.SupplierCount).ToList();

		}
		public void Query8()
		{
			//Write a LINQ query to group the Orders table by EmployeeID and retrieve a list of EmployeeID
			//and the total Freight cost for all orders handled by each employee.
			//Sort the results by total Freight cost in descending order.

			var query = context.Orders.GroupBy(e => e.EmployeeId).Select(e => new
			{
				EmployeeIds = e.Key,
				FrigtCost = e.Sum(o => o.Freight)
			}).OrderByDescending(e => e.FrigtCost).ToList();


			var result=(from o in context.Orders
					   group o by o.EmployeeId into g	
					   select new
					   {
						   EmployeeIds = g.Key,
						   FrigtCost = g.Sum(o => o.Freight)
					   }).OrderByDescending(e => e.FrigtCost).ToList();

		}
		public void Query9(){
			//Write a LINQ query to group the Products table by CategoryID and retrieve a list of CategoryID
			//and the average UnitPrice of products in each category. Sort the results
			//by average UnitPrice in ascending order.

			var query = context.Products.GroupBy(e => e.CategoryId).Select(e => new
			{
				CategoryID = e.Key,
				UneitPrice = e.Average(e => e.UnitPrice)
			}).OrderBy(e => e.UneitPrice).ToList();


		}
		public void Query10(){
			//Write a LINQ query to group the Orders table by ShipVia (shipper ID) and retrieve a list of ShipVia
			//and the total number of orders handled by each shipper.
			//Only include shippers who handled orders in 1997,
			//and sort the results by the number of orders in descending order.

			var query = context.Orders.Where(o => o.OrderDate.Value.Year == 1997)
				.GroupBy(e => e.ShipVia).Select(e => new
										{
											ShipVia = e.Key,	
											orders=e.Count()	
										}).ToList();	

		}
		public void Query11(){
			//Write a LINQ query to group the Orders table by EmployeeID and retrieve a list of EmployeeID
			//and the total Freight cost for orders handled by each employee.
			//Only include employees who handled orders in 1998,
			//and sort the results by total Freight cost in descending order.

			var query = context.Orders.Where(e => e.OrderDate.Value.Year == 1998)
				.GroupBy(e => e.EmployeeId).Select(e => new
				{
					EmployeeID = e.Key,
					FreighCost = e.Sum(e => e.Freight)
				}).OrderByDescending(e => e.FreighCost).ToList();


		}
		public void Query12(){
			//Write a LINQ query to group the Products table by SupplierID and
			//retrieve a list of SupplierID and the average UnitPrice of products supplied by each supplier.
			//Only include suppliers with an average UnitPrice greater than 30,
			//and sort the results by average UnitPrice in ascending order.


			var query = context.Products.GroupBy(e => e.SupplierId).Select(e => new
			{
				SupplierID = e.Key,
				UnitPrice = e.Average(e => e.UnitPrice)
			}).Where(e => e.UnitPrice > 30).OrderBy(e => e.UnitPrice).ToList();

		}
		public void Query13(){
			//Write a LINQ query to group the Customers table by Country and retrieve a list of Country
			//and the count of customers from each country.
			//Only include countries with more than 5 customers,
			//and sort the results by the count of customers in descending order.

			var query=context.Customers.GroupBy(e=>e.Country).Select(e=>new
			{
				country=e.Key,
				countcus=e.Count()
			}).Where(e=>e.countcus>5).OrderByDescending(e=>e.countcus).ToList();	

		}
		public void Query14(){
			//Write a LINQ query to group the Orders table by the month of OrderDate and
			//retrieve a list of the month (1–12) and the minimum Freight cost for orders placed in each month.
			//Only include months from orders placed in 1996,
			//and sort the results by minimum Freight cost in ascending order.


			var query = context.Orders.Where(e => e.OrderDate.Value.Year > 1996).GroupBy(e => e.OrderDate.Value.Month)
						.Select(e => new
						{
							month = e.Key,
							MinFreight = e.Min(e => e.Freight)
						}).ToList();
		}
		public void Query15(){
			//Write a LINQ query to group the Products table by CategoryID and retrieve a list of CategoryID
			//and the maximum UnitsInStock for products in each category.
			//Only include categories where the maximum UnitsInStock is less than 100,
			//and sort the results by maximum UnitsInStock in descending order.

			var query=context.Products.GroupBy(e=>e.CategoryId)
				             .Select(e=>new
							 {
								 CategoryID= e.Key,
								 maxUnitsInStock=e.Max(e=>e.UnitsInStock)
							 }).Where(e=>e.maxUnitsInStock<100).ToList();	

		}
		//public void Query1(){}
	}
}
