﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace AutoParts
{
    /// <summary>
    /// Interaction logic for ModifyProduct.xaml
    /// </summary>
    public partial class ModifyProduct : Window
    {
        private Database db;
        public ModifyProduct()
        {
            try
            {
                db = new Database();
                InitializeComponent();
                lvModifyProductList.ItemsSource = db.GetAllProducts();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error opening database connection: " + e.Message);
                Environment.Exit(1);
            }
        }

        private bool handle = true;

        private void comboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void comboSortBy_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void Handle()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvModifyProductList.ItemsSource);

            switch (comboSortBy.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "ID":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("ProductId", ListSortDirection.Ascending));
                    break;
                case "Product Name":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("ProductName", ListSortDirection.Ascending));
                    break;
                case "SupplierId":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("CustSupplierId", ListSortDirection.Ascending));
                    break;
                case "Price":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("CostPrice", ListSortDirection.Ascending));
                    break;
                case "Units On Stock":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("UnitsOnStock", ListSortDirection.Ascending));
                    break;
                case "Units On Order":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("UnitsOnOrder", ListSortDirection.Ascending));
                    break;
                default:
                    return;
            }
        }

    }
}
