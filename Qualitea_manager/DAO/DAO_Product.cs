using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_Product
    {
        private static MyContext entities = new MyContext();

        public static List<Product> getProducts()
        {
            return entities.Products.ToList();
        }

        public static List<Product> getProducts(bool isActive)
        {
            return entities.Products.Where(p => p.IsActive == isActive).ToList();
        }

        public static List<Product> getProducts(string keyword, int? cateID, bool? isActive, decimal? minPrice, decimal? maxPrice)
        {
            var products = getProducts();
            products = products.Where(p => p.Name.ToUpper().Contains(keyword.ToUpper())).ToList();
            if(cateID != null)
                products = products.Where(p => p.CategoryID == cateID).ToList();
            if(isActive != null)
                products = products.Where(p => p.IsActive == isActive).ToList();
            if (minPrice != null)
                products = products.Where(p => p.ProductOptions.Where(o => o.Price >= minPrice && o.IsActive).ToList().Count > 0).ToList();
            if (maxPrice != null)
                products = products.Where(p => p.ProductOptions.Where(o => o.Price <= maxPrice && o.IsActive).ToList().Count > 0).ToList();
            return products;
        }

        public static Product getProduct(Product p)
        {
            return entities.Products.Find(p.ProductID);
        }

        public static List<Product> getProducts(int cateID)
        {
            return entities.Products.Where(p => p.CategoryID.Equals(cateID)).ToList();
        }
        public static bool addProduct(string name, int cateID, string imageSource, List<Size> sizes, List<Decimal> money)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    Product newProduct = new Product();
                    newProduct.Name = name;
                    newProduct.CategoryID = cateID;
                    newProduct.Image = imageSource;
                    entities.Products.Add(newProduct);

                    List<ProductOption> options = new List<ProductOption>();
                    for (int i = 0; i < sizes.Count; i++)
                    {
                        ProductOption newOption = new ProductOption();
                        newOption.SizeID = sizes[i].SizeID;
                        newOption.Price = money[i];
                        newOption.Product = newProduct;
                        options.Add(newOption);
                    }
                    entities.ProductOptions.AddRange(options);

                    
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public static bool editProduct(Product product, string name, int cateID, string imageSource, List<Size> sizes, List<Decimal> money)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    
                    Product p = entities.Products.Find(product.ProductID);
                    p.Name = name;
                    p.CategoryID = cateID;
                    if (imageSource != "")
                    {
                        p.Image = imageSource;
                    }
                    if (sizes.Count == 0) p.IsActive = false;
                    List<ProductOption> options = p.ProductOptions.ToList();
                    List<ProductOption> newOptions = new List<ProductOption>();
                    foreach (ProductOption o in options)
                    {
                        bool flag = false;
                        for (int i = 0; i < sizes.Count; i++)
                        {
                            if (o.SizeID == sizes[i].SizeID)
                            {
                                ProductOption po = newOptions.Where(nop => nop.SizeID == sizes[i].SizeID).FirstOrDefault();
                                if (po != null)
                                    newOptions.Remove(po);
                                o.Price = money[i];
                                sizes.RemoveAt(i);
                                money.RemoveAt(i);
                                o.IsActive = true;
                                flag = true;
                                break;
                            }
                            else
                            {
                                ProductOption po = newOptions.Where(nop => nop.SizeID == sizes[i].SizeID).FirstOrDefault();
                                if (po == null)
                                {
                                    ProductOption newOption = new ProductOption();
                                    newOption.SizeID = sizes[i].SizeID;
                                    newOption.Price = money[i];
                                    newOption.Product = p;
                                    newOptions.Add(newOption);
                                }    
                               
                            }    
                        }
                        if (!flag)
                            o.IsActive = false;
                    }
                    entities.ProductOptions.AddRange(newOptions);
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    entities.ChangeTracker.DetectChanges();
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
