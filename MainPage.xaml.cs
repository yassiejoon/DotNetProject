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
using System.Globalization;
using System.Windows.Interop;
using Microsoft.Win32;
using System.Windows.Forms;


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
                System.Windows.MessageBox.Show("Error opening database connection: " + e.Message);
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
            lvProductList.ItemsSource = db.GetAllProducts();
            lvInventoryList.ItemsSource = db.GetAllProducts();
        }

        

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

            Purchase pc = (Purchase)lvPurchaseList.SelectedItem;
            if (pc == null)
            {
                return;
            }

            int productId = int.Parse(tbkRemoveID.Text);
            string productName = tbkRemoveName.Text;
            int custSupplierId = pc.CustSupplierId;
            decimal costPrice = pc.CostPrice;
            string quantityStr = tbRemoveQty.Text;
            int quantity;
            if (!int.TryParse(quantityStr, out quantity))
            {
                System.Windows.Forms.MessageBox.Show("Quantity must be an integer");
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
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            lvPurchaseList.ItemsSource = purchaseList;
            lvPurchaseList.SelectedIndex = -1;
            tbkRemoveID.Text = "";
            tbkRemoveName.Text = "";
            tbRemoveQty.Text = "";

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Products p = (Products)lvProductList.SelectedItem;
            if (p == null)
            {
                return;
            }

            int productId = int.Parse(tbkAddID.Text);
            string productName = tbkAddName.Text;
            int custSupplierId = p.CustSupplierId;
            decimal costPrice = p.CostPrice;
            string quantityStr = tbAddQty.Text;
            int quantity;
            if (!int.TryParse(quantityStr, out quantity))
            {
                System.Windows.MessageBox.Show("Quantity must be an integer");
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
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            lvPurchaseList.ItemsSource = purchaseList;
            lvProductList.SelectedIndex = -1;
            tbkAddID.Text = "";
            tbkAddName.Text = "";
            tbAddQty.Text = "";

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

            tbkProductID.Text = p.ProductId + "";
            tbkProductName.Text = p.ProductName + "";
            tbkSupplierName.Text = p.CustSupplierId + "";
            tbkQtyPreUnit.Text = p.QuantityPerUnit + "";
            tbkUnitsOnOrder.Text = p.UnitsOnOrder + "";
            tbkUnitsOnStock.Text = p.UnitsOnStock + "";

            tbkSupplierID.Text = cs.CustSupplierId + "";
            tbkCompanyName.Text = cs.CompanyName + "";
            tbkContactName.Text = cs.ContactName + "";
            tbkPhone.Text = cs.Phone + "";
            tbkAddress.Text = cs.Address + "";

            tbkAddID.Text = p.ProductId + "";
            tbkAddName.Text = p.ProductName + "";
            tbAddQty.Text = 1 + "";
            //tbAddQty.Focusable = true;
            //Keyboard.Focus(tbAddQty);

            lvPurchaseList.SelectedIndex = -1;
            tbkRemoveID.Text = "";
            tbkRemoveName.Text = "";
            tbRemoveQty.Text = "";
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

            tbkProductID.Text = pd.ProductId + "";
            tbkProductName.Text = pd.ProductName + "";
            tbkSupplierName.Text = pd.CustSupplierId + "";
            tbkQtyPreUnit.Text = pd.QuantityPerUnit + "";
            tbkUnitsOnOrder.Text = pd.UnitsOnOrder + "";
            tbkUnitsOnStock.Text = pd.UnitsOnStock + "";

            tbkSupplierID.Text = cs.CustSupplierId + "";
            tbkCompanyName.Text = cs.CompanyName + "";
            tbkContactName.Text = cs.ContactName + "";
            tbkPhone.Text = cs.Phone + "";
            tbkAddress.Text = cs.Address + "";

            tbkRemoveID.Text = pd.ProductId + "";
            tbkRemoveName.Text = pd.ProductName + "";
            tbRemoveQty.Text = 1 + "";

            lvProductList.SelectedIndex = -1;
            tbkAddID.Text = "";
            tbkAddName.Text = "";
            tbAddQty.Text = "";
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

        public object ModalDialog { get; private set; }
        public object ModalDialogParent { get; private set; }

        private void comboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox cmb = sender as System.Windows.Controls.ComboBox;
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

        private void btnRibbonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnRibbonSave_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnRibbonOpen_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnRibbonDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnRibbonExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
