using MySqlX.XDevAPI;
using Org.BouncyCastle.Math;
using RestAPITesting.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        HttpClient client = new HttpClient();
        public string[] Fruits { get; set; }
        public Sales()
        {
            client.BaseAddress = new Uri("https://localhost:7026/api/Product/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
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
            BuyProduct();
        }
        private async void BuyProduct()
        {
            Product product = new Product();
            product.Name = (string)fruits.SelectedValue;
            product.Amount = float.Parse(input.Text);

            HttpResponseMessage response = await client.PutAsync("BuyProduct/"+ product.Name + "/" + product.Amount,null);

            ServerStatus.Content = response.StatusCode.ToString();
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
