using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_Product
    {
        private MyContext entities;
        public DAO_Product()
        {
            entities = new MyContext();
        }
        public  List<Product> getProducts()
        {
            return entities.Products.ToList();
        }

        public  List<Product> getProducts(bool isActive)
        {
            return entities.Products.Where(p => p.IsActive == isActive).ToList();
        }

        public  List<Product> getProducts(string keyword, int? cateID, bool? isActive, double? minPrice, double? maxPrice)
        {
            var products = getProducts();
            if (keyword != null)
                products = products.Where(p => p.Name.ToUpper().Contains(keyword.ToUpper())).ToList();
            if (cateID != null && cateID != -1)
                products = products.Where(p => p.CategoryID == cateID).ToList();
            if (isActive != null)
                products = products.Where(p => p.IsActive == isActive).ToList();
            if (minPrice != null)
                products = products.Where(p => p.ProductOptions.Where(o => o.Price >= minPrice && o.IsActive).ToList().Count > 0).ToList();
            if (maxPrice != null)
                products = products.Where(p => p.ProductOptions.Where(o => o.Price <= maxPrice && o.IsActive).ToList().Count > 0).ToList();
            return products;
        }

        public  Product getProduct(Product p)
        {
            return entities.Products.Find(p.ProductID);
        }

        public  List<Product> getProducts(int cateID)
        {
            return entities.Products.Where(p => p.CategoryID.Equals(cateID)).ToList();
        }

        public bool addProduct(Product product, List<ProductOption> productOptions)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;
                    
                    entities.Products.Add(product);
                    for (int i = 0; i < productOptions.Count; i++)
                    {
                        productOptions[i].Product = product;
                    }
                    entities.ProductOptions.AddRange(productOptions);


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

        public bool editProduct(Product product, List<ProductOption> productOptions)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;

                    Product p = entities.Products.Find(product.ProductID);
                    p.Name = product.Name;
                    p.CategoryID = product.CategoryID;
                    p.Image = product.Image;
                    p.IsActive = product.IsActive;
                    List<ProductOption> removePO = new List<ProductOption>();
                    foreach (ProductOption po in p.ProductOptions)
                    {
                        ProductOption productOption = product.ProductOptions.Where(o => o.ProductOptionID == po.ProductOptionID).FirstOrDefault();
                        if (productOption != null)
                        {
                            po.Size = productOption.Size;
                            po.Price = productOption.Price;
                            po.IsActive = productOption.IsActive;
                            product.ProductOptions.Remove(productOption);
                        }
                        else
                        {
                            removePO.Add(po);
                        }
                    }
                    foreach (ProductOption po in removePO)
                    {
                        p.ProductOptions.Remove(po);
                        entities.ProductOptions.Remove(po);
                    }
                    entities.ProductOptions.AddRange(productOptions);

                    entities.ChangeTracker.DetectChanges();
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool delProduct(int ProductID)
        {
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Configuration.AutoDetectChangesEnabled = false;

                    
                    DAO_Order daoOrder = new DAO_Order();
                    if (daoOrder.GetOrderDetailsByProductID(ProductID).Count > 0)
                    {
                        return false;
                    }
                    Product p = entities.Products.Find(ProductID);
                    List<ProductOption> pos = p.ProductOptions.ToList();
                    entities.Products.Remove(p);

                    entities.ChangeTracker.DetectChanges();
                    int result = entities.SaveChanges();
                    entities.Configuration.AutoDetectChangesEnabled = true;

                    transaction.Commit();

                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                    return false;
                }
            }
        }

       
    }
}
