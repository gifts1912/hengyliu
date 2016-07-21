using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnBoardingV1._0.Models;
using System.Web.ModelBinding;

namespace OnBoardingV1._0
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Product> GetProduct([QueryString("ProductId")] int? productId)
        {
            var _db = new OnBoardingV1._0.Models.ProductContext();
            IQueryable<Product> query = _db.Products;
            if(productId.HasValue && productId > 0)
            {
                query = query.Where(p => p.ProductID == productId);
            }
            else
            {
                query = null;
            }
            return query;
        }
    }
}