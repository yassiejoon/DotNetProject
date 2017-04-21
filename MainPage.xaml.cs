using System;
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
using System.Windows.Shapes;

namespace AutoParts
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        List<Products> productList = new List<Products>();
        List<Orders> orderList = new List<Orders>();
        Database db;

        public MainPage()
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
            InitializeComponent();
            refreshPeopleList();
        }

 
        private void refreshPeopleList()
        {
            //lvProductList.ItemsSource = db.GetAllProducts();
        }

        private void comboCompanyName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
            private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPurchaseSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPurchasePrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPurchaseCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hl1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          /*  int index = lvProductList.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            Products p = (Products)lvProductList.Items[index];
            lblProductID.Content = p.ProductId + "";
            lblProductName.Content = p.ProdName + "";
            lblSupplierName.Content = p.SupplierId + "";
            lblQtyPerUnit.Content = p.QuantityPerUnit + "";
            lblUnitsOnOrder.Content = p.UnitsOnOrder + "";
            lblUnitsOnStock.Content = p.UnitsOnStock + "";

            lblAddID.Content = p.ProductId + "";
            lblAddName.Content = p.ProdName + "";
            tbAddQty.Text = 1 + "";
*/
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       //     SELECT * FROM orders;
        }
    }
}
