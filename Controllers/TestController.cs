using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using MySql.Data.MySqlClient;

namespace ASPNETCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        //[HttpGet]
        //Route[("GetAllBooks")]

        //public JsonResult GetBooks()
        //{
        //   string query = @" select bookname from 
             
        //}


        
    }
}
