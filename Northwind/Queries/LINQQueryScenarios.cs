using Microsoft.EntityFrameworkCore;
using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Queries
{
	public class LINQQueryScenarios
	{
		private readonly NorthwindContext context;
		public LINQQueryScenarios()
		{
			context = new NorthwindContext();	
		}

		//Check if a product with name "Chai" exists. If it exists, increase its unit price by 10%. If not,
		//add a new product with that name in category 1, price $18, supplier 1.
		public void query1()
		{
			var ChaiProduct = context.Products.Where(e => e.ProductName == "Chai").FirstOrDefault();
			if (ChaiProduct is null)
			{
				var product = new Product
				{
					ProductName = "Chai",
					CategoryId = 1,
					UnitPrice = 18,
					SupplierId = 1
				};
				context.Products.Add(product);	
				context.SaveChanges();
				return;
			}

			ChaiProduct.UnitPrice = ChaiProduct.UnitPrice + (ChaiProduct.UnitPrice * 0.1m);
			context.SaveChanges();	
		}

		//. Find all orders that have been placed but not shipped (ShippedDate is null) for more than 30 days.
		//Update their freight cost by adding a $5 late shipping fee.
		public void query2()
		{
			var notShippedOrders = context.Orders.Where(e => e.ShippedDate == null && e.OrderDate != null
						&& EF.Functions.DateDiffDay(e.OrderDate.Value, DateTime.Now) > 30).ToList();

			foreach(var order in notShippedOrders)
			{
				order.Freight += 5m;
			}
			context.SaveChanges();	
		}

		// Check if customer "ALFKI" has any pending orders (not shipped). If yes, do nothing.
		// If no, delete all their old orders from before 1997.
		public void query3()
		{
			var hasPendingOrders = context.Orders
				.Any(e => e.CustomerId == "ALFKI" && e.ShippedDate == null);

			if (hasPendingOrders)
				return; 

			var oldOrders = context.Orders
				.Where(e => e.CustomerId == "ALFKI" && e.OrderDate < new DateTime(1997, 1, 1)).ToList();

			context.Orders.RemoveRange(oldOrders);
			context.SaveChanges();
		}

		//Validate if employee with ID 5 exists and has handled more than 50 orders.
		//If true, give them a bonus by inserting a record in a hypothetical Bonus table. If false, show a message.
		public void query4()
		{
			var employee = context.Employees.SingleOrDefault(e => e.EmployeeId == 5);
			if (employee is null)
			{
				Console.WriteLine("❌ Employee not found.");
				return;
			}

			var orderCount = context.Orders.Count(e => e.EmployeeId == 5);

			if (orderCount > 50)
			{

			}
		}

	}
}
