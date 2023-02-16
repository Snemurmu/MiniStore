using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RestAPITesting.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    
    public partial class Admin : Window
    {
        HttpClient client = new HttpClient();
        SqlConnection con = new SqlConnection("Data Source=desktop-qajii73\\sqlexpress;Initial Catalog=Products;Integrated Security=True");

        public Admin()
        {
            //to add to our project to establish http connection with RestAPI
            client.BaseAddress = new Uri("https://localhost:7026/api/Product/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();      
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            this.saveProducts();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            updateProduct();
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            listProductsById();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            deleteProductById();
        }

        private void main_Click(object sender, RoutedEventArgs e)
        {            
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void table_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Products", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            datagrid.DataContext = ds;
            con.Close();
        }
        private async void saveProducts()
        {
            Product product = new Product();
            product.Name = name.Text;
            product.Id = int.Parse(id.Text);
            product.Amount = float.Parse(amount.Text);
            product.Price = float.Parse(price.Text);

            HttpResponseMessage response = await client.PostAsJsonAsync<Product>("AddProduct/", product);

            //get the response from restAPI server
            ServerStatus3.Content = response.StatusCode.ToString();
        }

        private void listProductsById()
        {
           
            var response = client.GetFromJsonAsync<Response>("GetAllProductsByID/" + int.Parse(id.Text));
           
            Product product = response.Result.products;
            if (product != null)
            {
                name.Text = product.Name;
                amount.Text = product.Amount.ToString();
                price.Text = product.Price.ToString();

                ServerStatus.Content = response.Result.StatusMessage;
            }
            else
            {
                ServerStatus.Content = response.Result.StatusMessage;
            }                   
        }

        private async void deleteProductById()
        {
            HttpResponseMessage response = await client.DeleteAsync("DeleteProduct/" + int.Parse(id.Text));
            if(response != null)
            {
                ServerStatus1.Content = response.StatusCode.ToString();
            }
            else
            {
                 MessageBox.Show("ID does not Exist!");
            }
        }

        private async void updateProduct()
        {
            Product product = new Product();
            product.Name = name.Text;
            product.Id = int.Parse(id.Text);
            product.Amount = float.Parse(amount.Text);
            product.Price = float.Parse(price.Text);

            HttpResponseMessage response = await client.PutAsJsonAsync<Product>("UpdateProduct/", product);

            //get the response from restAPI server
            ServerStatus2.Content = response.StatusCode.ToString();
        }
    }
}
