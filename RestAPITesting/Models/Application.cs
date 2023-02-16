using System.Data;
using System.Data.SqlClient;

namespace RestAPITesting.Models
{
    public class Application
    {
        public Response GetAllProducts(SqlConnection con)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Products", con);
            DataTable dt = new DataTable();
            List<Product> listProducts = new List<Product>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Product product = new Product();
                    product.Id = (int)dt.Rows[i]["Id"];
                    product.Name = (string)dt.Rows[i]["Name"];
                    product.Amount = float.Parse(dt.Rows[i]["Amount"].ToString());
                    product.Price = float.Parse(dt.Rows[i]["Price"].ToString());

                    listProducts.Add(product);
                }
            }
            if (listProducts.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data Found";
                response.listProducts = listProducts;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
                response.listProducts = null;
            }
            return response;
        }

        public Response GetAllProductsByID(SqlConnection con, int id)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Products Where ID = '" + id + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                Product product = new Product();
                product.Id = (int)dt.Rows[0]["Id"];
                product.Name = (string)dt.Rows[0]["Name"];
                product.Amount = float.Parse(dt.Rows[0]["Amount"].ToString());
                product.Price = float.Parse(dt.Rows[0]["Price"].ToString());
                response.StatusCode = 200;
                response.StatusMessage = "Data Found";
                response.products = product;


            }

            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
                response.listProducts = null;
            }
            return response;
        }

        public Response AddProduct(SqlConnection con, Product product)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Products(Id, Name, Amount, Price) Values('" + product.Id + "','" + product.Name + "', '" + product.Amount + "', '" + product.Price + "') ", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Product Added";



            }

            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Inserted";

            }
            return response;
        }

        public Response UpdateProduct(SqlConnection con, Product product)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update Products set Name='" + product.Name + "', Amount='" + product.Amount + "', Price='" + product.Price + "' Where Id='" + product.Id + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Product Updated";



            }

            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Inserted";

            }
            return response;
        }

        public Response DeleteProduct(SqlConnection con, int Id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from Products Where ID = '" + Id + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Product Deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "ID does not Exist!";
            }

            return response;
        }

        public Response BuyProduct(SqlConnection con,string name, float amount)
        {
            Response response = new Response();
            
            SqlCommand cmd = new SqlCommand("Update Products set Amount=Amount - '" + amount + "' where Name='" + name + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Product Updated";
            }

            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Inserted";

            }
            return response;
        }
    }
}
