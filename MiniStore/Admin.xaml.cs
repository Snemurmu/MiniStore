using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

namespace MiniStore
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-QAJII73\\SQLEXPRESS;Initial Catalog=Products;Integrated Security=True");

        public Admin()
        {
            InitializeComponent();      
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            //Open database connection
            try
            {                
                con.Open();
                string query = "Insert into Products values(@Name, @ID, @Amount, @Price)";
                SqlCommand cmd = new SqlCommand(query, con);                
                cmd.Parameters.AddWithValue("Name", name.Text);
                cmd.Parameters.AddWithValue("ID", int.Parse(id.Text));
                cmd.Parameters.AddWithValue("Amount", int.Parse(amount.Text));                
                cmd.Parameters.AddWithValue("Price", double.Parse(price.Text));

                //Execute the query
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully!");
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            //Open database connection
            try
            {                
                con.Open();
                string query = "Update Products set name=@Name, amount=@Amount, price=@Price where id=@ID";
                SqlCommand cmd = new SqlCommand(query, con);                
                cmd.Parameters.AddWithValue("Name", name.Text);
                cmd.Parameters.AddWithValue("ID", int.Parse(id.Text));
                cmd.Parameters.AddWithValue("Amount", int.Parse(amount.Text));               
                cmd.Parameters.AddWithValue("Price", double.Parse(price.Text));

                //Execute the query
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Updated Successfully!");
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            //Open database connection
            try
            {                
                con.Open();
                string query = "SELECT name, amount, price from Products where id=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("ID", int.Parse(id.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    name.Text = (string)sqlReader.GetValue(0);
                    amount.Text = sqlReader.GetValue(1).ToString();                    
                    price.Text = sqlReader.GetValue(2).ToString();
                }

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            //Open database connection
            try
            {                
                con.Open();
                string query = "DELETE Products where id=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("ID", int.Parse(id.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Deleted Successfully!");
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
