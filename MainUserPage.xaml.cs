using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Win32;
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
    /// Interaction logic for MainUserPage.xaml
    /// </summary>
    public partial class MainUserPage : Window
    {
        //      List<Product> productList = new List<Product>();
        //      List<Order> orderList = new List<Order>();
        List<Parts> partList = new List<Parts>();
        Database db;
        private bool unsavedChanges = false;
        private string openFilePath = null;
       // private object lvOrderList;

        public MainUserPage()
        {
            try
            {
                db = new Database();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error opening database connection: " + e.Message);
                Environment.Exit(1);
            }
     //       InitializeComponent();
            List<Orders> items = new List<Orders>();
     //       lvOrderList.ItemsSource = items;
            refreshPeopleList();
        }
        private void refreshPeopleList()
        {
      //      lvOrderList.ItemSource
        }

        private void updateStatus()
        {
            Title = (openFilePath == null ? "new file" : System.IO.Path.GetFileName(openFilePath))
                + (unsavedChanges ? " (unsaved changes)" : "");
            
        }

        private void tbBillAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedChanges = true;
            updateStatus();
        }

        private void tbShipAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedChanges = true;
            updateStatus();
        }

        private void btnRibbonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (unsavedChanges)
            {
                MessageBoxResult result = MessageBox.Show("Save unsaved changes?", "Unsaved changes",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    case MessageBoxResult.Yes:
                        btnRibbonSave_Click(null, null);
                        break;
                }
            }
        }

        private void btnRibbonSave_Click(object sender, RoutedEventArgs e)
        {
            
                try
                {
       //             File.WriteAllText(openFilePath, lvOrderList.Text);
                    unsavedChanges = false;
                    updateStatus();
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    MessageBox.Show(this, "Error reading file " + ex.Message);
                }

            
        }



    }

    
}
