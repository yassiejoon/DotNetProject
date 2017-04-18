using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace AutoParts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Register register = new Register();
        MainPage homepage = new MainPage();

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            register.Show();
            this.Close();
        }
        private void hl1_Click(object sender, RoutedEventArgs e)
        {

        }
            private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbUserName.Text.Length == 0)
            {
                lblMessage.Content = "Enter an UserName.";
                tbUserName.Focus();
            }
            else if (!Regex.IsMatch(tbUserName.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                lblMessage.Content = "Enter a valid UserName.";
                tbUserName.Select(0, tbUserName.Text.Length);
                tbUserName.Focus();
            }
            else
            {
                string username = tbUserName.Text;
                string password = pbPwd.Password;
                SqlConnection con = new SqlConnection("Data Source=TESTPURU;Initial Catalog=Data;User ID=sa;Password=wintellect");
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Employees where UserName='" + username + "'  and Password='" + password + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    homepage.Show();
                    Close();
                }
                else
                {
                    lblMessage.Content = "Sorry! Please enter existing username/password.";
                }
                con.Close();
            }
            
        }

        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
