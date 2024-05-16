using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductSpecification
{
	public class ProductSpecification
	{
		private int pageIndex = 1;
		private int pageSize =5 ;
		private string? name ;

		public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
		
        public int PageIndex
		{
			get { return pageIndex; }
			set { pageIndex = value; }
		}
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > 10 ? 10 : value; }
		}
		public string? Name
		{
			get { return name?.ToLower(); }
			set { name = value; }
		}





	}
}
