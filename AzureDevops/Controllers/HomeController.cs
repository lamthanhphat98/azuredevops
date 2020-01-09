using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevops.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok("ok");
        }
        [HttpGet("ip")]
        public async Task<IActionResult> GetIp()
        {
            string responseContent = "";
            String url = "https://ipinfo.io/ip";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {

                    using (HttpContent content = response.Content)
                    {
                        responseContent = await content.ReadAsStringAsync();
                    }
                }
            }
            return Ok(responseContent);
           
        }
    }
}