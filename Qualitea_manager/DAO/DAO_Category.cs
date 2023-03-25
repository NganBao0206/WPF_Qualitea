using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DTO;
namespace DAO
{
    public class DAO_Category
    {

        public static List<Category> getCategories()
        {
            MyContext entities = new MyContext();
            return entities.Categories.ToList();
        }

        public static List<Category> getCategories(string keyword)
        {
            MyContext entities = new MyContext();
            List<Category> cates = entities.Categories.Where(c => c.Name.Contains(keyword)).ToList();
            return cates;
        }

        public static Boolean addNewCategory(string categoryName)
        {
            MyContext entities = new MyContext();
            var category = new Category();
            category.Name = categoryName;
            entities.Categories.Add(category);

            if (entities.SaveChanges() > 0)
                
                return true;
            else
                return false;
        }
        
        public static Boolean delCategory(Category cate)
        {
            MyContext entities = new MyContext();
            if (cate.Products.Count > 0)
                return false;
            Category category = entities.Categories.Where(c => c.CategoryID.Equals(cate.CategoryID)).FirstOrDefault();
            entities.Categories.Remove(category);
            if (entities.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public static Boolean editCategory(int ID, string categoryName)
        {
            MyContext entities = new MyContext();
            Category category = entities.Categories.Where(c => c.CategoryID.Equals(ID)).FirstOrDefault();
            if (category == null)
                return false;
            category.Name = categoryName;
            if (entities.SaveChanges() > 0)
                return true;
            else
                return false;
        }


    }

}
