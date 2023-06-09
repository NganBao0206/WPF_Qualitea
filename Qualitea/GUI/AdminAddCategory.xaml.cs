﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminAddCategory : Window
    {
        private BUS_Category bc = new BUS_Category();
        public AdminAddCategory()
        {
            InitializeComponent();
        }


        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addNew(object sender, RoutedEventArgs e)
        {
            TextInfo textInfo = new CultureInfo("vi-VN", false).TextInfo;
            if (String.IsNullOrWhiteSpace(txtName.Text) || !Regex.IsMatch(txtName.Text, @"[\p{L}\s]+$"))
            {
                MessageBox.Show("Tên không đúng định dạng");
                return;
            }
            txtName.Text = textInfo.ToTitleCase(txtName.Text.Trim().Replace(@"\s+", " ").ToLower());

            if (bc.addNewCategory(txtName.Text))
            {
                MessageBox.Show("Thêm thành công");
                Admin a = this.Owner as Admin;
                a.categoryList.ItemsSource = bc.getCategories();
                txtName.Text = "";
            }
            else
                MessageBox.Show("Thêm thất bại");

        }
    }
}
