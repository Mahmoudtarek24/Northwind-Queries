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
		//For any product with units in stock less than reorder level AND units on order = 0,
		//automatically create a "reorder request" by updating units on order to 50.
		public void Query5()
		{
			var productsToReorder = context.Products
					.Where(p => p.UnitsInStock < p.ReorderLevel && p.UnitsOnOrder == 0).ToList();

			foreach (var product in productsToReorder)
				product.UnitsOnOrder = 50;

			context.SaveChanges();
		}

		public void query6()
		{
			//Calculates each customer's total purchase amount
			//(sum of all their order details: quantity × unitprice × (1-discount))

			var totalCustomerPurchase = (from cust in context.Customers
										 join ord in context.Orders on cust.CustomerId equals ord.CustomerId
										 join ordDet in context.OrderDetails on ord.OrderId equals ordDet.OrderId
										 select new { Customer = cust, Order = ord, OrderDetail = ordDet })
			 .GroupBy(x => x.Customer.CustomerId)
			 .Select(g => new
			 {
				 CustomerId = g.Key,
				 TotalPurchase = g.Sum(x => x.OrderDetail.Quantity * x.OrderDetail.UnitPrice * (1 - (decimal)x.OrderDetail.Discount)),
				 Orders = g.Select(x => x.Order).Distinct().ToList()   
			 })
			 .ToList();

			//Categorizes customers into tiers: Bronze (<$5000), Silver ($5000-$15000),
			//Gold ($15000-$30000), Platinum (>$30000)


			var customersTires = totalCustomerPurchase.Select(e=>new
			{
				e.CustomerId,
				e.TotalPurchase,
				e.Orders,
				Tiers = e.TotalPurchase > 30000 ? "Platinum" :
						e.TotalPurchase > 15000 ? "Gold" :
						e.TotalPurchase > 5000 ? "Silver" : "Bronze"
		    }).ToList();

			//For Gold/Platinum customers who haven't ordered in the last 6 months,
			//send them a special offer by creating discount coupons
			//(insert into hypothetical Coupons table with 15% discount)

			var sixMonthAgo = DateTime.Now.AddMonths(-6);
			var goldPlatinumCustomers = customersTires
				.Where(e => e.Tiers == "Platinum" || e.Tiers == "Gold"
				        && (!e.Orders.Any(e => e.OrderDate >= sixMonthAgo))).ToList();

			foreach(var cust in goldPlatinumCustomers)
			{
				var coupon = new Coupon
				{
					CustomerId = cust.CustomerId,
					DiscountPercentage = 15,
					CreatedDate = DateTime.Now
				};
				context.Coupons.Add(coupon);		
			}
			context.SaveChanges();

			//If a Bronze customer has more than 20 orders but low total spend,
			//flag them as "frequent buyer" and update their tier to Silver

			var bronzeCustomers= customersTires.Where(e=>e.Orders.Count()>20)
				 .Select(e => new
				 {
					 e.CustomerId,
					 e.TotalPurchase,
					 e.Orders,
					
					 Note = "Frequent Buyer"
				 }).ToList();
		}
	
		public void queryScenario()
		{
			//Identifies products where (UnitsInStock < ReorderLevel) AND (UnitsOnOrder = 0)

			var products = context.Products.Include(e=>e.Supplier).Include(e=>e.Category)
				         .Where(e=>e.UnitsInStock<e.ReorderLevel&&e.UnitsOnOrder==0).ToList();

			//For each product, checks if the supplier is from USA/UK (priority suppliers) or other countries

			var priorityCountries = products.All(e => e.Supplier.Country == "USA" || e.Supplier.Country == "Uk");
			if(!priorityCountries)
				Console.WriteLine("not all from USA/UK");

			//Calculates optimal reorder quantity based on: average monthly sales from
			//order details of last 6 months × 2 (for 2-month buffer)

			var sixMonthsAgo = DateTime.Now.AddMonths(-6);
			var salesProduct = (from ord in context.Orders
								join ordDet in context.OrderDetails on ord.OrderId equals ordDet.OrderId
								where ord.OrderDate.HasValue && ord.OrderDate.Value >= sixMonthsAgo
								group ordDet by ordDet.ProductId into g
								select new
								{
									ProductId = g.Key,
									UnitsSoldLast6Months = g.Sum(od => od.Quantity)
								}).ToList();

			var reorderSuggestions = salesProduct.Select(p => new
			{
				p.ProductId,
				p.UnitsSoldLast6Months,
				AverageMonthly = p.UnitsSoldLast6Months / 6.0,
				ReorderQuantity = (int)Math.Ceiling((p.UnitsSoldLast6Months / 6.0) * 2)
			}).ToList();

			//If supplier is priority AND product has been ordered by >10 different customers,
			//create urgent reorder (update UnitsOnOrder)

			// عدد العملاء المختلفين اللي اشتروا المنتج خلال آخر 6 شهور
			var distinctCustomersCount = (from cust in context.Customers
										  join ord in context.Orders on cust.CustomerId equals ord.CustomerId
										  join ordDet in context.OrderDetails on ord.OrderId equals ordDet.OrderId
										  where ord.OrderDate.HasValue && ord.OrderDate.Value >= sixMonthsAgo
										  group new { cust.CustomerId } by ordDet.ProductId into g
										  select new
										  {
											  ProductId = g.Key,
											  CustomerCount = g.Select(x => x.CustomerId).Distinct().Count()
										  }).ToList();
			foreach (var prod in products)
			{
				bool isPrioritySupplier = prod.Supplier.Country == "USA" || prod.Supplier.Country == "UK";

				var productStats = distinctCustomersCount.FirstOrDefault(x => x.ProductId == prod.ProductId);
				int customerCount = productStats?.CustomerCount ?? 0;
				if (isPrioritySupplier && customerCount > 10)
				{
					var reorderQty = reorderSuggestions
										.FirstOrDefault(r => r.ProductId == prod.ProductId)?.ReorderQuantity ?? 0;

					if (reorderQty > 0)
						prod.UnitsOnOrder = (short)reorderQty;
				}
				//If supplier is non-priority OR product is low-demand (<5 customers),
				//check if similar products from same category exist with better stock levels
				if (!isPrioritySupplier && customerCount < 5)
				{
					var alternative = context.Products
						.Where(e => e.CategoryId == prod.CategoryId && e.UnitsInStock > prod.UnitsInStock)
						.OrderByDescending(e => e.UnitsInStock).FirstOrDefault();
					if (alternative != null)
					{
						prod.Discontinued = true;

						var pendingOrders = context.OrderDetails
							.Where(o => o.ProductId == prod.ProductId && o.Order.Status == "Pending")
							.ToList();

						foreach (var order in pendingOrders)
							order.ProductId = alternative.ProductId;
					}
				}
			}
			context.SaveChanges();
		}

	}
}
