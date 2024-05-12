using Microsoft.AspNetCore.Mvc;

namespace WebForTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public void Read()
        {

        }

        [HttpPost]
        public void Create()
        {

        }

        [HttpPut]
        public void Update() 
        { 

        }

        [HttpDelete]
        public void Delete() 
        {

        }

        
    }
}
