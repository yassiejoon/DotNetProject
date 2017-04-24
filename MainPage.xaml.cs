using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        List<Products> productList = new List<Products>();
        List<Purchase> purchaseList = new List<Purchase>();
        List<CustSuppliers> custSupplierList = new List<CustSuppliers>();
        private Database db;

        public MainPage()
        {
            try
            {
                db = new Database();
                InitializeComponent();
                RefreshProductList();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error opening database connection: " + e.Message);
                Environment.Exit(1);
            }
        }
        //For Admin Window

        ModifyProduct modifyProduct = new ModifyProduct();

        private void btnModifyProduct_Click(object sender, RoutedEventArgs e)
        {
            modifyProduct.Show();
            this.Close();
        }

        //For Purchase Window
        private void RefreshProductList()
        {
    //        lvProductList.ItemsSource = db.GetAllProducts();
    //        lvInventoryList.ItemsSource = db.GetAllProducts();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

            Purchase pc = (Purchase)lvPurchaseList.SelectedItem;
            if (pc == null)
            {
                return;
            }

            int productId = int.Parse(tbRemoveID.Text);
            string productName = tbRemoveName.Text;
            int custSupplierId = pc.CustSupplierId;
            decimal costPrice = pc.CostPrice;
            string quantityStr = tbRemove.Text;
            int quantity;
            if (!int.TryParse(quantityStr, out quantity))
            {
                MessageBox.Show("Quantity must be an integer");
                return;
            }
            try
            {
                Purchase pur = new Purchase(productId, productName, custSupplierId, costPrice, pc.Quantity-quantity);
                purchaseList.Remove(pc);
                purchaseList.Add(pur);
                lvPurchaseList.Items.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

            lvPurchaseList.ItemsSource = purchaseList;
            lvPurchaseList.SelectedIndex = -1;
            tbRemoveID.Text = "";
            tbRemoveName.Text = "";
            tbRemove.Text = "";

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Products p = (Products)lvProductList.SelectedItem;
            if (p == null)
            {
                return;
            }

            int productId = int.Parse(tbAddID.Text);
            string productName = tbAddName.Text;
            int custSupplierId = p.CustSupplierId;
            decimal costPrice = p.CostPrice;
            string quantityStr = tbAdd.Text;
            int quantity;
            if (!int.TryParse(quantityStr, out quantity))
            {
                MessageBox.Show("Quantity must be an integer");
                return;
            }
            try
            {
                Purchase pc = new Purchase(productId, productName, custSupplierId, costPrice, quantity);
                if (purchaseList != null)
                {
                    foreach (Purchase pur in purchaseList)
                    {
                        if (pc.ProductId == pur.ProductId)
                        {
                            pc.Quantity = pc.Quantity + pur.Quantity;
                            purchaseList.Remove(pur);
                        }
                    }

                }
                purchaseList.Add(pc);
                lvPurchaseList.Items.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

            lvPurchaseList.ItemsSource = purchaseList;
            lvProductList.SelectedIndex = -1;
            tbAddID.Text = "";
            tbAddName.Text = "";
            tbAdd.Text = "";

            /*
            int ProductId = int.Parse(tbkAddID.Text);
            int CustSupplierID = int.Parse(tbkSupplierID.Text);
            int EmployeeID = 1;// (int)Register.tbEID.Text;
            decimal UnitPrice = 0;// (decimal)db.GetAllProducts.UnitPrice;
            int Quantity = int.Parse(tbAddQty.Text);
            decimal Discount = 0;
            DateTime OrderDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            DateTime ShippedDate = Convert.ToDateTime(DateTime.Today.AddDays(5).ToString("yyyy-MM-ddTHH:mm:ss"));
            string ShippedAddress = "";
            */
        }

        private void lvProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
 
            Products p = (Products)lvProductList.SelectedItem;
            if (p == null)
            {
                return;
            }

            CustSuppliers cs = db.FindSupplierById(p.CustSupplierId);

            tbProductID.Text = p.ProductId + "";
            tbProductName.Text = p.ProductName + "";
            tbSupplierName.Text = p.CustSupplierId + "";
            tbQtyPreUnit.Text = p.QuantityPerUnit + "";
            tbUnitsOnOrder.Text = p.UnitsOnOrder + "";
            tbUnitsOnStock.Text = p.UnitsOnStock + "";

            tbSupplierID.Text = cs.CustSupplierId + "";
            tbCompanyName.Text = cs.CompanyName + "";
            tbContactName.Text = cs.ContactName + "";
            tbPhone.Text = cs.Phone + "";
            tbAddress.Text = cs.Address + "";

            tbAddID.Text = p.ProductId + "";
            tbAddName.Text = p.ProductName + "";
            tbAdd.Text = 1 + "";
            //tbAddQty.Focusable = true;
            //Keyboard.Focus(tbAddQty);

            lvPurchaseList.SelectedIndex = -1;
            tbRemoveID.Text = "";
            tbRemoveName.Text = "";
            tbRemove.Text = "";
        }

        private void lvPurchaseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      
            Purchase p = (Purchase)lvPurchaseList.SelectedItem;
            if (p == null)
            {
                return;
            }

            Products pd = db.FindProductById(p.ProductId);
            CustSuppliers cs = db.FindSupplierById(p.CustSupplierId);

            tbProductID.Text = pd.ProductId + "";
            tbProductName.Text = pd.ProductName + "";
            tbSupplierName.Text = pd.CustSupplierId + "";
            tbQtyPreUnit.Text = pd.QuantityPerUnit + "";
            tbUnitsOnOrder.Text = pd.UnitsOnOrder + "";
            tbUnitsOnStock.Text = pd.UnitsOnStock + "";

            tbSupplierID.Text = cs.CustSupplierId + "";
            tbCompanyName.Text = cs.CompanyName + "";
            tbContactName.Text = cs.ContactName + "";
            tbPhone.Text = cs.Phone + "";
            tbAddress.Text = cs.Address + "";

            tbRemoveID.Text = pd.ProductId + "";
            tbRemoveName.Text = pd.ProductName + "";
            tbRemove.Text = 1 + "";

            lvProductList.SelectedIndex = -1;
            tbAddID.Text = "";
            tbAddName.Text = "";
            tbAdd.Text = "";
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

        //For Inventory Window

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
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvInventoryList.ItemsSource);

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
