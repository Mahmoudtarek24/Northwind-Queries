using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		//public void Query1(){}
		//public void Query1(){}
		//public void Query1(){}
	}
}
