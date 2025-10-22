using Azure;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		//Check if all products have a unit price greater than $0
		public void query36() => context.Products.All(e => e.UnitPrice > 0);
		//Get customers from USA or UK using Contains with a list
		public void query37() => context.Customers.Where(e => e.Country == "USA" || e.Country == "UK").ToList();
		//Get the average order freight cost for orders shipped to Germany
		public void query38() => context.Orders.Where(e => e.ShipCountry == "Germany").Average(e => e.Freight);
		//Get product names starting with letters A-M(first half of alphabet)
	    public void query39() => context.Products.Where(e => e.ProductName[0] >= 'A' && e.ProductName[0] <= 'M')
		                                         .ToList();
		//Get the 3rd page of products(page size = 10) using Skip and Take
		public void query40() => context.Products.Skip((3-1)*10).Take(10).ToList();
		//Get all orders where order date and shipped date are in the same month
		public void query41() => context.Orders.Where(e => e.ShippedDate.Value.Month == e.OrderDate.Value.Month).ToList();
		//Find the maximum units in stock across all products
		public void query42() => context.Products.Max(e => e.UnitsInStock);
		//Get employees who report to employee ID 2, return full names only
		public void query43() => context.Employees.Where(e=>e.ReportsTo==2).Select(e => e.FirstName + " " + e.LastName)
		                                          .ToList();
		//Combine two lists: customers from France + customers from Spain using Union
		public void query44() => context.Customers.Where(e => e.Country == "France")
										.Union(context.Customers.Where(e => e.Country == "Spain")).ToList();
		//Get order IDs where at least one order detail has quantity > 50
		public void query45() => context.Orders.Where(e=>e.OrderDetails.Any(e=>e.Quantity>50))
		                                .Select(o => o.OrderId).ToList();
		//Get products with the top 5 highest unit prices, show name and price
		public void query46() => context.Products.OrderByDescending(e => e.UnitPrice).Take(5)
			                                 .Select(e => new { e.ProductName, e.UnitPrice }).ToList();
		//Check if customer "ALFKI" exists in the database
		public void query47() => context.Customers.Any(e => e.ContactName == "ALFKI");
		//Get distinct countries from both Customers and Suppliers tables using Union 
		public void query48() => context.Customers.Select(e => e.Country)
								 .Union(context.Suppliers.Select(e => e.Country)).ToList();
		//Get the first product alphabetically, or null if no products exist
		public void query49() => context.Products.OrderBy(p => p.ProductName).FirstOrDefault();
		//Group employees by city, then count and order by count descending
		public void query50() => context.Employees.GroupBy(e=>e.City)
									.Select(g => new
									{
										City = g.Key,
										EmployeeCount = g.Count()
									}).OrderByDescending(e=>e.EmployeeCount).ToList();
		//Get all orders placed in the last quarter of 1996 (Oct-Dec)
		public void query51() => context.Orders.Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1996 &&
			                                   (o.OrderDate.Value.Month >= 10 && o.OrderDate.Value.Month <= 12)).ToList();
		//Get products where units in stock + units on order > 100
		public void query52() => context.Products.Where(e=>(e.UnitsInStock+e.UnitsOnOrder)>100).ToList();
		//Get the total sum of (quantity * unit price) for all order details
		public void query53() => context.OrderDetails.Sum(e => e.Quantity * e.UnitPrice);
		//Get customers who have placed more than 10 orders
		public void query54() => context.Orders.GroupBy(e=>e.CustomerId)
			                     .Select(e=>new
								 {
									 key =e.Key,
									 ordercount = e.Count()
								 }).Where(e=>e.ordercount>10).ToList();

		//    context.Customers.Where(c => c.Orders.Count() > 10)
		//       .Select(c => new
		//       {
		//           c.CustomerId,
		//           c.CompanyName,
		//           OrderCount = c.Orders.Count()
		//	      }).ToList();

		//Take products while unit price is less than $20 (use TakeWhile)
		//public void query55() => 
		//Get the employee with the earliest birth date (oldest employee)
		public void query56() => context.Employees.OrderBy(e => e.BirthDate).FirstOrDefault();
		//Get all unique regions from customers (excluding nulls and empty strings)
		public void query57() => context.Customers.Select(e => e.Region).Distinct().ToList();
		//Join orders with customers, show order ID, date, and customer company name
		public void query58() => (from ord in context.Orders
								  join cust in context.Customers on ord.CustomerId equals cust.CustomerId
								  select new
								  {
									  orderId = ord.OrderId,
									  companyName = cust.CompanyName,
								  }).ToList();
		//Get products that have never been ordered (left join scenario)
		public void query59() => (from prod in context.Products
								  join OrdDet in context.OrderDetails
									  on prod.ProductId equals OrdDet.ProductId into productOrders
								  from OrdDet in productOrders.DefaultIfEmpty()
								  where OrdDet == null
								  select prod).ToList();

		//Get the top 3 customers by total number of orders placed
		public void query60() => context.Customers.OrderByDescending(e=>e.Orders.Count())
			                            .Take(3).Select(e=> new
										{
											e.CustomerId,
											e.CompanyName,
											TotalOrders = e.Orders.Count()
										}).ToList();
		//Check if there are any orders with null shipped date (not shipped yet)
		public void query61() => context.Orders.Any(e => e.ShippedDate == null);
		//Get all products grouped by supplier, show supplier ID and total units in stock
		public void query62() => context.Products.GroupBy(e => e.SupplierId)
										.Select(e => new
										{
											supplierId = e.Key,
											totalUnit = e.Sum(e => e.UnitsInStock)
										}).ToList();
		//Get orders from 1997 where freight is between $50 and $200
		public void query63() => context.Orders.Where(e => e.OrderDate.Value.Year == 1997 &&
		                                       (e.Freight >= 50 && e.Freight <= 200)).ToList();
		//Get the 2nd most expensive product
		public void query64() => context.Products.OrderByDescending(e => e.UnitPrice).Skip(1).Take(1);
		//Select first 100 orders, then group by customer and count
		public void query65() => context.Orders.Take(100).GroupBy(o => o.CustomerId)
										.Select(g => new
										{
											CustomerId = g.Key,
											OrderCount = g.Count()
										}).ToList();
		//Get all employees except those from "London" or "Seattle"
		public void query66() => context.Employees.Where(e => e.City != "London" && e.City != "Seattle").ToList();
 		//Get product categories with average product price > $30
		public void query67() => context.Products.GroupBy(e=>e.CategoryId)
			                             .Select(e=>new
										 {
											 CategoryId = e.Key,
											 AveragePrice = e.Average(p => p.UnitPrice)
										 }).Where(e=>e.AveragePrice>30).ToList();
		//Get distinct employee cities and customer cities combined
		public void query68() => context.Employees.Select(e => e.City).Union(context.Customers.Select(e => e.City)).ToList();
		//Get the difference between max and min product prices using aggregation
		//	public void query69() => ;
		//Get all orders where customer ID starts with 'A' and employee ID is even
		public void query70() => context.Orders.Where(e => e.CustomerId.StartsWith("A") && e.EmployeeId % 2 == 0).ToList();
		//Zip product names with their category names into tuples
		//public void query71() => 
		//Get order details where discount is greater than 0, group by product
		public void query72() => context.OrderDetails.Where(od => od.Discount > 0).GroupBy(e=>e.ProductId)
			                            .Select(e=>new
										{
											ProductId = e.Key,
											CountOrdersWithDiscount = e.Count(),
											TotalDiscount = e.Sum(od => od.Discount)
										}).ToList();
		//Get customers who have the same city as any supplier
		public void query73() => context.Customers.Where(e => context.Suppliers.Select(s => s.City).Contains(e.City)).ToList();
		//Get all products, if none exist return a default empty list
		public List<Product> query74() => context.Products.ToList() ?? new List<Product>();
		//Get the last 10 orders by order date, then reverse the list
		public void query75() => context.Orders.OrderByDescending(e => e.OrderDate).Take(10)
			                            .OrderBy(e => e.OrderDate).ToList();
		//Get orders shipped by shipper "Federal Shipping", include shipper name
		public void query76() => context.Orders.Where(o => o.ShipName == "Federal Shipping")
									   .Select(o => new
									   {
										   o.OrderId,
										   o.OrderDate,
										   o.ShippedDate,
										   ShipperName = o.ShipName
									   }).ToList();
		//Get employees and their subordinates count (who report to them)
		public void query77() => context.Employees
			.Select(e => new
			{
				EmployeeId = e.EmployeeId,
				FullName = e.FirstName + " " + e.LastName,
				SubordinatesCount = context.Employees.Count(e => e.ReportsTo == e.EmployeeId)
			}).ToList();
		//Get products where name contains "Chef" or "Tofu" (case-insensitive)
		public void query78() => context.Products.Where(e => e.ProductName.ToLower().Contains("chef".ToLower())
			                		|| e.ProductName.ToLower().Contains("tofu".ToLower())).ToList();
		//Calculate total revenue: sum of (unitprice * quantity * (1-discount)) for all orders
		public void query79() => context.OrderDetails
		                            .Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount));
		//Get the median unit price of products (middle value when sorted)
		public void query80()
		{
			var prices = context.Products.OrderBy(e => e.UnitPrice).Select(e => e.UnitPrice).ToList();
			var count = prices.Count();
			decimal? median;
			if (count == 0)
				median = 0; 
			else if (count % 2 == 0)
				median = (prices[count / 2 - 1] + prices[count / 2]) / 2;
			else
				median = prices[count / 2];
		}
		//Get customers who have ordered products from category "Seafood"
		public void query81() => (from cust in context.Customers
								  join ord in context.Orders on cust.CustomerId equals ord.CustomerId
								  join ordDet in context.OrderDetails on ord.OrderId equals ordDet.OrderId
								  join prod in context.Products on ordDet.ProductId equals prod.ProductId
								  join cat in context.Categories on prod.CategoryId equals cat.CategoryId
								  where cat.CategoryName == "Seafood"
								  select cust).ToList();
		//Skip products while price < $10, then take next 5 products
		public void query82() => context.Products.OrderBy(p => p.ProductId)        
		                                .SkipWhile(p => p.UnitPrice < 10) .Take(5).ToList();
		//Get all orders grouped by year and month, show count per period
		public void query83() => context.Orders.GroupBy(e => new { e.OrderDate!.Value.Year, e.OrderDate!.Value.Month })
										.Select(e => new
										{
											month = e.Key.Month,
											year = e.Key.Year,
											count = e.Count()
										}).ToList();
		//Get the least expensive product from each category
		public void query84() => context.Products.GroupBy(e => e.CategoryId)
										   .Select(e => new
										   {
											   CategoryId = e.Key,
											   LeastExpensiveProduct = e.OrderBy(e =>e.UnitPrice).FirstOrDefault()
										   }).ToList();
		//Check if any employee was born before 1950
		public void query85() => context.Employees.Any(e => e.BirthDate.Value.Year < 1950);
		//Get products supplied by companies from USA, sorted by price descending
		public void query86() => context.Products.Where(e => e.Supplier.Country == "USA")
			                                .OrderByDescending(e => e.UnitPrice).ToList();////////////
		//Get employee pairs where one reports to the other (self-join)
		public void query87() => (from emp in context.Employees
								  join mgr in context.Employees on emp.ReportsTo equals mgr.EmployeeId
								  select new
								  {
									  EmployeeName = emp.FirstName + " " + emp.LastName,
									  ManagerName = mgr.FirstName + " " + mgr.LastName
								  }).ToList();
		//Get orders with details: order ID, customer name, product names (comma-separated)
		public void query88() => (from ord in context.Orders
								  join ordDet in context.OrderDetails on ord.OrderId equals ordDet.OrderId
								  join prod in context.Products on ordDet.ProductId equals prod.ProductId
								  join cust in context.Customers on ord.CustomerId equals cust.CustomerId
								  select new
								  {
									  OrderId = ord.OrderId,
									  customerName = cust.ContactName,
									  ProductName = prod.ProductName,
								  }).ToList();
		//Get categories that have no discontinued products
		public void query89() => context.Categories.Where(e=>e.Products.All(e=>!e.Discontinued)).ToList();
		//Get the standard deviation of product prices (use manual calculation with Aggregate)
		//public void query90() => 
		//Get customers who ordered in both 1996 AND 1997
		public void query91() =>    context.Customers.Where(c => c.Orders.Any(o => o.OrderDate.Value.Year == 1996) &&
                                   c.Orders.Any(o => o.OrderDate.Value.Year == 1997)).ToList();
		//Get the second page of customers sorted by company name (page size 15)
		public void query92() => context.Customers.OrderBy(e=>e.ContactName).Skip((2-1)*15).Take(15).ToList();
		//Get products restocked recently: where units on order > reorder level
		public void query93() => context.Products.Where(p => p.UnitsOnOrder > p.ReorderLevel).ToList();
		//Find orders where total order value exceeds $1000
		public void query94() => context.Orders
				 .Where(e => e.OrderDetails.Sum(e => e.UnitPrice * e.Quantity * (1 - (decimal)e.Discount)) > 1000).ToList();
		//Get territories grouped by region with territory count
		public void query95() => context.Territories.GroupBy(t => t.Region.RegionDescription)
										.Select(g => new
										{
											Region = g.Key,
											TerritoryCount = g.Count()
										}).ToList();
		//Get the longest company name among customers
		public void query96() => context.Customers.OrderByDescending(e => e.ContactName.Length).FirstOrDefault();
		//Get all products from suppliers in the same country as customer "ALFKI"
		public void query97() => (from cust in context.Customers
								  join ord in context.Orders on cust.CustomerId equals ord.CustomerId
								  join ordDet in context.OrderDetails on ord.OrderId equals ordDet.OrderId
								  join prod in context.Products on ordDet.ProductId equals prod.ProductId
								  join sup in context.Suppliers on prod.SupplierId equals sup.SupplierId
								  where cust.CustomerId == "ALFKI" && cust.Country == sup.Country
								  select prod).Distinct().ToList();
		//Get employees hired in the same year, grouped by year
		//public void query98() => 
		//Get the top 5 products by total quantity sold across all orders
		public void query99() => context.OrderDetails.GroupBy(e => e.ProductId)
										.Select(g => new
										{
											ProductId = g.Key,
											TotalQuantity = g.Sum(e => e.Quantity)
										}).OrderByDescending(e => e.TotalQuantity).Take(5).ToList();
		//Get customers with no orders using GroupJoin and filtering
		public void query100() => (from cust in context.Customers
								   join ord in context.Orders
									   on cust.CustomerId equals ord.CustomerId into CustomerOrders
								   from custOrder in CustomerOrders.DefaultIfEmpty()
								   where custOrder == null
								   select cust).ToList();
		//Get orders where required date is before shipped date (late shipments)
		//public void query39() =>
		//Get the sum of units in stock for discontinued vs active products
		//public void query39() =>
		//Get products that appear in more than 20 orders
		//public void query39() =>
		//Prepend a "dummy" product to the products list
		//public void query39() =>
		//Get orders from customers in cities with more than 3 customers
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
		//
		//public void query39() =>
	}
}
