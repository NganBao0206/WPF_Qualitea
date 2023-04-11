using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_Product
    {
        private DAO_Product dp;
        public BUS_Product()
        {
            dp = new DAO_Product();
        }
        public  List<Product> getProducts()
        {
            return dp.getProducts();
        }

        public  List<Product> getProducts(bool isActive)
        {
            return dp.getProducts(isActive);
        }

        public  List<Product> getProducts(string keyword, int? cateID, bool? isActive, double? minPrice, double? maxPrice)
        {
            return dp.getProducts(keyword, cateID, isActive, minPrice, maxPrice);
        }

        public Product getProduct(Product p)
        {
            return dp.getProduct(p);
        }

        public bool addProduct(Product product, List<ProductOption> productOptions)
        {
            try
            {
                cloudinary clou = new cloudinary();
                product.Image = clou.addImageForce(product.Image);
                foreach (ProductOption po in productOptions)
                {
                    po.Price = po.Price / 1000;
                }
                return dp.addProduct(product, productOptions);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool editProduct(Product product, List<ProductOption> productOptions)
        {
            try
            {
                string url = "";
                string oldUrl = dp.getProduct(product).Image;
                int lastSlash = oldUrl.LastIndexOf("/");
                int dot = oldUrl.LastIndexOf(".");
                string publicID = oldUrl.Substring(lastSlash + 1, dot - lastSlash - 1);
                cloudinary clou = new cloudinary();
                if (product.Image != oldUrl)
                {
                    url = clou.addImage(product.Image);
                    product.Image = url;
                }
                bool result = dp.editProduct(product, productOptions);
                if (result == true)
                {
                    if (url != "")
                        clou.delImage(publicID);
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  object getProducts(object isActive)
        {
            throw new NotImplementedException();
        }
    }
}
