using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace MiniStore
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        SqlConnection con;
        public string[] Fruits { get; set; }
        public Sales()
        {

            InitializeComponent();           
            Fruits = new string[] { "Apple", "Orange", "Raspberry", "Blueberry", "Cauliflower" };
            DataContext = this;            
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            double quantity = Convert.ToDouble(input.Text);

            if ((String)fruits.SelectedValue == "Apple")
            {
                total.Text = (quantity * 2.10).ToString();
            }
            else if ((String)fruits.SelectedValue == "Orange")
            {
                total.Text = (quantity * 2.49).ToString();
            }
            else if ((String)fruits.SelectedValue == "Raspberry")
            {
                total.Text = (quantity * 2.35).ToString();
            }
            else if ((String)fruits.SelectedValue == "Blueberry")
            {
                total.Text = (quantity * 1.45).ToString();
            }
            else if ((String)fruits.SelectedValue == "Cauliflower")
            {
                total.Text = (quantity * 2.22).ToString();
            }

        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            //Open database connection
            try
            {
                string connectionString = "Data Source=DESKTOP-QAJII73\\SQLEXPRESS;Initial Catalog=Products;Integrated Security=True";
                con = new SqlConnection(connectionString);
                con.Open();
                string query = "Update Products set amount = amount - @Amount where name = @Name";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("Name", (String)fruits.SelectedValue);
                cmd.Parameters.AddWithValue("Amount", int.Parse(input.Text));

                //Execute the query
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sold!");
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
