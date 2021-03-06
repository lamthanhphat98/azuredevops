﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AzureDevops.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace AzureDevops.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly WeatherContext context;
        public HomeController(WeatherContext _context)
        {
            context = _context;
        }
        [HttpGet("Delete")]
        public IActionResult Delete()
        {
            if(Directory.Exists("G:\\CN3\\submission"))
            {
                String[] getAllFilesPath = Directory.GetFiles("G:\\CN3\\submission");
                foreach (var item in getAllFilesPath)
                {
                    String filePath = item; 
                    //append Thuc's code
                }
              
            }
            return Ok(true);
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var ipaddress = HttpContext.GetServerVariable("REMOTE_ADDR");
            return Ok(ipaddress); 
        }
        [HttpGet("Get2")]
        public IActionResult Get2()
        {
            
            return Ok("New commit");
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
        [HttpGet("location")]
        public async Task<IActionResult> GetLocationByIp()
        {
            string responseContent = "";
            String url = "http://ipinfo.io/json";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        responseContent = await  content.ReadAsStringAsync();
                    }
                }
            }
            var IpModel = JsonConvert.DeserializeObject<IpModel>(responseContent);
            return Ok(IpModel);
        }
    }
}