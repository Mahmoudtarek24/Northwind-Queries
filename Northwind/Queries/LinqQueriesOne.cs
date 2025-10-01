using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Queries
{
	public class LinqQueriesOne
	{
		private readonly NorthwindContext context;
		public LinqQueriesOne(NorthwindContext context)
		{
			this.context = context;		
		}
		/// Get all products from the database
		public void query1() =>  context.Products.ToList();

		//Get all customers from a specific city (e.g., "London")
		public void query2() => context.Customers.Where(e=>e.City== "London").ToList();
		//Get all employees whose last name starts with 'D' 
		public void query3() => context.Employees.Where(e=>e.LastName.StartsWith("D")).ToList();
		// Get all orders placed in 1997
		public void query4() => context.Orders.Where(e => e.OrderDate.Value.Year == 1997).ToList();
		//Get all products that are discontinued
		public void query5() => context.Products.Where(e=>!e.Discontinued).ToList();
		// Get all products with unit price greater than $20
		public void query6() => context.Products.Where(e=>e.UnitPrice>20).ToList();
		//Get all suppliers from USA
		public void query7() => context.Suppliers.Where(e=>e.Country=="USA").ToList();
		//Get all categories (just the category names)
		public void query8() => context.Categories.Select(e=>e.CategoryName).ToList();	
		//Get all products that are currently in stock (UnitsInStock > 0)
		public void query9() => context.Products.Where(e=>e.UnitsInStock>0).ToList();
		//Count total number of products
		public void query10() => context.Products.Count();
		//Count how many customers are from Mexico
		public void query11() => context.Customers.Where(e => e.Country == "Mexico").Count();
		//Count total number of orders
		public void query12() =>context.Orders.Count();
		//Count how many products are discontinued
		public void query13() => context.Products.Where(e=>!e.Discontinued).Count();
		//Get the highest product unit price
		public void query14() => context.Products.Max(e => e.UnitPrice);
		//Group products by category ID and count products in each category
		public void query15() =>
			context.Products.GroupBy(e=>e.CategoryId)
			.Select(e=>new { key =e.Key, NumberOfProduct = e.Count(),}).ToList();
		//Group customers by country and count customers per country
		public void query16() =>
			context.Customers.GroupBy(e => e.Country)
			.Select(e => new{ key = e.Key,NumberOfCountrty = e.Count(),}).ToList();
		//Group orders by customer ID and count orders per customer
		public void query17() => 
			context.Orders.GroupBy(e=>e.CustomerId)
			.Select(e => new { key = e.Key,	NumberOfOrder = e.Count(),}).ToList();
		//Group products by supplier and count products per supplier
		public void query18() =>
			context.Products.GroupBy(e=>e.Supplier)
				.Select(e => new {	key = e.Key, NumberOfProduct = e.Count(), }).ToList();
		//public void query15() =>
		//public void query15() =>
		//public void query15() =>
		//public void query15() =>
	}
}
