﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BUS;
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminEditCategory : Window
    {
        private BUS_Category bc = new BUS_Category();
        public static Category cate { get; set; }
        public AdminEditCategory()
        {
            InitializeComponent();
            cateID.Text = cate.CategoryID.ToString();
            cateName.Text = cate.Name;
        }


        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void edit(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(cateID.Text);
            string name = cateName.Text;
            if (bc.editCategory(id, name))
                MessageBox.Show("Sửa thành công");
            else
                MessageBox.Show("Sửa không thành công");
            Admin a = this.Owner as Admin;
            a.categoryList.ItemsSource = bc.getCategories();
        }
    }
}