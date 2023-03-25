using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_Category
    {
        public static List<Category> getCategories()
        {
            return DAO_Category.getCategories();
        }

        public static List<Category> getCategories(string name)
        {
            return DAO_Category.getCategories(name);
        }

        private static string formatString(string s)
        {
            s = s.Trim();
            while (s.IndexOf("  ") >= 0)
            {
                s = s.Replace("  ", " ");
            }
            return s;
        }
        public static bool addNewCategory(string categoryName)
        {
            categoryName = formatString(categoryName);
            return DAO_Category.addNewCategory(categoryName);
        }

        public static string delCategories(List<Category> cates)
        {
            List<string> success = new List<string>();
            List<string> failure = new List<string>();
            foreach (Category cate in cates)
            {
                if (DAO_Category.delCategory(cate))
                {
                    success.Add(cate.Name);
                                        
                }
                else
                    failure.Add(cate.Name);
            }
            string result = "";
            for (int i = 0; i < success.Count; i++)
                if (i == 0)
                    result += "Đã xóa thành công " + success[0] + ", ";
                else
                    result += success[i] + ", ";
            if (result != "") result = result.Substring(0, result.Length - 2) + "\n";
            for (int i = 0; i < failure.Count; i++)
                if (i == 0)
                    result += "Không thể xóa " + failure[0] + ", ";
                else
                    result += failure[i] + ", ";
            if (result != "") result = result.Substring(0, result.Length - 2) + "\n";
            return result;
        }

        public static bool editCategory(int ID, string name)
        {
            name = formatString(name);
            return DAO_Category.editCategory(ID, name);
        }
        public static int getID(Category any)
        {
            return any.CategoryID;
        }

       
    }
}
    