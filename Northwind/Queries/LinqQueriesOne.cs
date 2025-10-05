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
		//Get top 5 most expensive products
		public void query19() => context.Products.OrderByDescending(e=>e.UnitPrice).Take(5).ToList();
		//Get all orders ordered by order date descending
		public void query20() => context.Orders.OrderByDescending(e=>e.OrderDate).ToList();
		//Count products in category 1
		public void query21() => context.Products.Count(e => e.CategoryId == 1);
		//Group orders by month of order date and count orders per month
		public void query22() => context.Orders.GroupBy(e=>e.OrderDate.Value.Month)
			.Select(e => new { month=e.Key, OrderNumber =e.Count()}).ToList();
		//Group customers by region and count customers per region
		public void query23() => context.Customers.GroupBy(e => e.Region)
			.Select(e => new { region = e.Key, CustomerNumber = e.Count() }).ToList();
		//Get product names with their category names
		public void query24() => (from pro in context.Products
								  join cat in context.Categories on pro.CategoryId equals cat.CategoryId
								  select new
								  {
									  catName = cat.CategoryName,
									  proName = pro.ProductName
								  }).ToList();
		//Get products with their category name and description
		public void query25() =>  (from pro in context.Products
								   join cat in context.Categories on pro.CategoryId equals cat.CategoryId
								   select new
								   {
									   catName = cat.CategoryName,	
									   catDescripthion =cat.Description,
									   product =pro
								   }).ToList();

		//Get the first 5 products ordered by name
		public void query26() => context.Products.OrderBy(e => e.ProductName).Take(5);
		//Check if any customer is from "Berlin"
		public void query27() => context.Customers.Any(e => e.City == "Berlin");
		//Get all product names as a comma-separated string using Aggregate
		public void query28() => context.Products.Select(e => e.ProductName).Aggregate((current, next) => current + ", " + next);
		//Find the single category with ID 3 (or throw exception if not found)
		public void query29() => context.Categories.Single(e => e.CategoryId == 3);
		//Get distinct cities where customers are located
		public void query30() => context.Customers.Select(e => e.City).Distinct();
		//Skip the first 10 orders and take the next 20
		public void query31() => context.Orders.Skip(10).Take(20);
		//Get all products and their categories, but only for products with price > $25
		public void query32() => (from pro in context.Products
								  join cat in context.Categories on pro.CategoryId equals cat.CategoryId
								  where pro.UnitPrice > 25
								  select new
								  {
									  product = pro,
									  category = cat
								  }).ToList();
		//Count how many orders have freight cost greater than $100
		public void query33() => context.Orders.Count(e => e.Freight > 100);
		//Get the last employee hired (use Last)
		public void query34() => context.Employees.OrderByDescending(e => e.HireDate).Last();
		// Group products by category and select category ID with product count 
		public void query35() => context.Products.GroupBy(e => e.CategoryId)
			                            .Select(e=>new
										{
											categoryId= e.Key,
											productCount= e.Count()
										}).ToList();

		//public void query36() =>
		//public void query37() =>
		//public void query38() =>
		//public void query39() =>
	}
}
