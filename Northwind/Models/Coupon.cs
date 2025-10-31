using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Models
{
	public class Coupon
	{
		public int Id { get; set; }	
		public string CustomerId { get; set; }	
		public decimal DiscountPercentage { get; set; }	
		public DateTime CreatedDate { get; set; }	 
	}
}
