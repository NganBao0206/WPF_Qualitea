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

        public static List<Product> getProducts()
        {
            return DAO_Product.getProducts();
        }

        public static List<Product> getProducts(bool isActive)
        {
            return DAO_Product.getProducts(isActive);
        }

        public static List<Product> getProducts(string keyword, int? cateID, bool? isActive, decimal? minPrice, decimal? maxPrice)
        {
            return DAO_Product.getProducts(keyword, cateID, isActive, minPrice, maxPrice);
        }

        public static bool addProduct(string name, int cateID, string imageSource, List<DTO.Size> sizes, List<Decimal> money)
        {
            try
            {
                cloudinary clou = new cloudinary();
                string url = clou.addImageForce(imageSource);
                return DAO_Product.addProduct(name, cateID, url, sizes, money);
            }
            catch(Exception)
            {
                return false;
            }

        }
        public static bool editProduct(Product product, string name, int cateID, string imageSource, List<Size> sizes, List<Decimal> money)
        {
            try
            {
                string url = "";
                string oldUrl = DAO_Product.getProduct(product).Image;
                int lastSlash = oldUrl.LastIndexOf("/");
                int dot = oldUrl.LastIndexOf(".");
                string publicID = oldUrl.Substring(lastSlash + 1, dot - lastSlash - 1);
                cloudinary clou = new cloudinary();
                if (imageSource != "")
                {
                    
                    url = clou.addImage(imageSource);
                }
                bool result = DAO_Product.editProduct(product, name, cateID, url, sizes, money);
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

        public static object getProducts(object isActive)
        {
            throw new NotImplementedException();
        }
    }
}
