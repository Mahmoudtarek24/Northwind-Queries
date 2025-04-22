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
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
	}
}
