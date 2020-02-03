using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureDevops.Models;
using AzureDevops.Models.ViewModels;
using AzureDevops.Ultilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevops.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        //SE62882
        private readonly WeatherContext context;
        private readonly IMapper mapper;
        public AdminController(WeatherContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        [HttpPost("weather")]
        public IActionResult PostWeather(WeatherClientModel weatherClient)
        {
            var weather = mapper.Map<Weather>(weatherClient);
            context.Weather.Add(weather);
            context.SaveChanges();
            var weatherDetail = mapper.Map<WeatherDetail>(weatherClient);
            weatherDetail.WeatherId = weather.Id;
            weatherDetail.DayOfWeek = ConvertToDay.ConvertIntToDay(weatherClient.DayOfWeek);
            weatherDetail.Name = weatherClient.Name;
            context.WeatherDetail.Add(weatherDetail);
            context.SaveChanges();
            return Ok(true);
        }
        [HttpGet("weather")]
        public IActionResult GetWeathersToday()
        {
            var today = DateTime.Today;
            var allWeather = context.Weather.Where(x => x.Date.Equals(today)).ToList();
            return Ok(allWeather);
        }
        [HttpGet("weather_header")]
        public IActionResult GetHeaderWeather([FromQuery] String location)
        {
            var today = DateTime.Today;
            if(location.Split("_").Length != 0)
            {
                location = location.Replace("_", " ");
                var getLocation = context.Weather.Where(x => x.Date.Equals(today) && x.Name.Equals(location)).FirstOrDefault();
                var getRandomWeather = context.Weather.Where(x => x.Date.Equals(today) && x.Id != getLocation.Id).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                getRandomWeather.Add(getLocation);
                return Ok(getRandomWeather);
            }else
            {
                var getLocation = context.Weather.Where(x => x.Date.Equals(today) && x.Name.Equals(location)).FirstOrDefault();
                var getRandomWeather = context.Weather.Where(x => x.Date.Equals(today) && x.Id != getLocation.Id).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                getRandomWeather.Add(getLocation);
                return Ok(getRandomWeather);
            }            
        }

        [HttpGet("weather_detail")]
        public IActionResult GetAllWeatherByName([FromQuery] String name)
        {
            var getDetails = context.WeatherDetail.Where(x=>x.Name.Equals(name)).OrderByDescending(x => x.Id).Take(7).ToList();
            return Ok(getDetails.OrderBy(x=>x.Id));
        }
        [HttpGet("suggest_search")]
        public IActionResult GetAllCityNameBySearchKeyword([FromQuery] String name)
        {
            var getCityNames = context.Weather.Where(x=>x.Name.Contains(name)).Take(5).ToList();
            return Ok(getCityNames);
        }

        [HttpPost("login")]
        public IActionResult LoginByAdmin([FromBody] Users user)
        {
            var getUser = context.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if(getUser!= null)
            {
                return Ok(true);
            }
            return NotFound("Username or password maybe wrong");
        }
    }
}