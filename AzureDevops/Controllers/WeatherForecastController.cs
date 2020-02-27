using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureDevops.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        Socket clientSock;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
           // ConnectToServerWinform();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("send")]
        public IActionResult SendResultToServer()
        {
           
            return Ok(true);
        }
        private void ConnectToServerWinform()
        {
            Task.Run(() =>
            {
                IPAddress[] ipAddress = Dns.GetHostAddresses("192.168.1.215");
                IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5656);
                clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                clientSock.Connect(ipEnd);
                string fileName = "test.txt";// "Your File Name"; 
                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                byte[] clientData = new byte[4 + fileNameByte.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                clientSock.Send(clientData);
            });
        }
    }
}
