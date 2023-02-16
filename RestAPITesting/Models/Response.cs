namespace RestAPITesting.Models
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public Product products { get; set; }

        public List<Product> listProducts { get; set; }

    }
}
