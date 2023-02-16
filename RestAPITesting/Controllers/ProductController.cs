
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestAPITesting.Models;
using System.Data.SqlClient;

namespace RestAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration; // receive the connection state with sql server

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllProducts")]

        public Response GetAllProducts()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.GetAllProducts(con);
            return response;
        }

        [HttpGet]
        [Route("GetAllProductsByID/{id}")]

        public Response GetAllProductsByID(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.GetAllProductsByID(con, id);
            return response;
        }

        [HttpPost]
        [Route("AddProduct")]
        public Response AddProduct(Product product)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.AddProduct(con, product);
            return response;
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public Response UpdateProduct(Product product)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.UpdateProduct(con, product);
            return response;
        }


        [HttpDelete]
        [Route("DeleteProduct/{Id}")]
        public Response DeleteProduct(int Id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.DeleteProduct(con, Id);
            return response;
        }
        [HttpPut]
        [Route("BuyProduct/{name}/{amount}")]
        public Response BuyProduct(string name, float amount)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.BuyProduct(con, name, amount);
            return response;
        }
    }
}
