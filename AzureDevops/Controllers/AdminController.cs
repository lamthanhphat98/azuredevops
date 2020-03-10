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
using Microsoft.EntityFrameworkCore;

namespace AzureDevops.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        //SE6288222
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
            var getWeather = context.Weather.Where(x => x.Name.Equals(weatherClient.Name) && x.Date.Equals(weatherClient.Date)).Include(x=>x.WeatherDetail).FirstOrDefault();
            if (getWeather != null)
            {
                getWeather.Name = weatherClient.Name;
                getWeather.Region = weatherClient.Region;
                getWeather.Tempure = weatherClient.TempureMorning;
                getWeather.WeatherCondition = weatherClient.WeatherCondition;
                getWeather.Country = weatherClient.Country;
                context.Weather.Update(getWeather);
                context.SaveChanges();
                var weatherDetail = context.WeatherDetail.Where(x => x.Id == getWeather.WeatherDetail.FirstOrDefault().Id).FirstOrDefault();              
                weatherDetail.DayOfWeek = ConvertToDay.ConvertIntToDay(weatherClient.DayOfWeek);
                weatherDetail.Name = weatherClient.Name;
                weatherDetail.TempureMorning = weatherClient.TempureMorning;
                weatherDetail.TempureNight = weatherClient.TempureNight;
                weatherDetail.Rain = weatherClient.Rain;
                weatherDetail.WindSpeed = weatherClient.WindSpeed;
                context.WeatherDetail.Update(weatherDetail);
                context.SaveChanges();
            }else
            {
                var weather = mapper.Map<Weather>(weatherClient);
                weather.Tempure = weatherClient.TempureMorning;
                context.Weather.Add(weather);
                context.SaveChanges();
                var weatherDetail = mapper.Map<WeatherDetail>(weatherClient);
                weatherDetail.WeatherId = weather.Id;
                weatherDetail.DayOfWeek = ConvertToDay.ConvertIntToDay(weatherClient.DayOfWeek);
                weatherDetail.Name = weatherClient.Name;
                weatherDetail.TempureMorning = weatherClient.TempureMorning;
                weatherDetail.TempureNight = weatherClient.TempureNight;
                context.WeatherDetail.Add(weatherDetail);
                context.SaveChanges();
            }
           
            return Ok(true);
        }

        [HttpPost("update_weather")]
        public IActionResult UpdateWeather(WeatherClientModel weatherClient)
        {
                 
            return Ok(true);
        }

        [HttpGet("weather")]
        public IActionResult GetWeathersToday()
        {
            var today = DateTime.Today;       
            var getall = context.Weather.Where(x => x.Date.Equals(today)).OrderByDescending(x => x.Id).AsEnumerable().GroupBy(x => x.Name).Select(x=>x.First()).ToList();
            return Ok(getall);
        }
        [HttpGet("weather_header")]
        public IActionResult GetHeaderWeather([FromQuery] String location)
        {
            var today = DateTime.Today;
            if(location.Split("_").Length != 0)
            {
                location = location.Replace("_", " ");
                var getLocation = context.Weather.Where(x => x.Date.Equals(today) && x.Name.Equals(location)).OrderByDescending(x=>x.Id).FirstOrDefault();
                var getRandomWeather = context.Weather.Where(x => x.Date.Equals(today) && x.Id != getLocation.Id && !x.Name.Equals(getLocation.Name)).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                getRandomWeather.Add(getLocation);
                return Ok(getRandomWeather);
            }else
            {
                var getLocation = context.Weather.Where(x => x.Date.Equals(today) && x.Name.Equals(location)).OrderByDescending(x => x.Id).FirstOrDefault();
                var getRandomWeather = context.Weather.Where(x => x.Date.Equals(today) && x.Id != getLocation.Id && !x.Name.Equals(getLocation.Name)).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                getRandomWeather.Add(getLocation);
                return Ok(getRandomWeather);
            }            
        }

        [HttpGet("weather_detail")]
        public IActionResult GetAllWeatherByName([FromQuery] String name)
        {
            //var getDetail = context.WeatherDetail.Where(x => x.Name.Equals(name)).ToList();
            var today = DateTime.Now;
            var getDetails = context.WeatherDetail.Include(x=>x.Weather).Where(x=>x.Name.Equals(name) && x.Date >= today).OrderByDescending(x => x.Id).Take(7).ToList();
            return Ok(getDetails.OrderBy(x=>x.Date));
        }

        [HttpGet("weather_detail_id")]
        public IActionResult GetWeatherById([FromQuery] int id)
        {
            //var getDetail = context.WeatherDetail.Where(x => x.Name.Equals(name)).ToList();
            var getDetails = context.WeatherDetail.Include(x=>x.Weather).Where(x => x.Id == id).FirstOrDefault();
            return Ok(getDetails);
        }
        [HttpGet("suggest_search")]
        public IActionResult GetAllCityNameBySearchKeyword([FromQuery] String name)
        {
            var today = DateTime.Today;
            var result = context.Weather.ToList().Where(x=>x.Name.Contains(name)).GroupBy(test => test.Name)
                   .Select(grp => grp.First()).Take(5).ToList();
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult LoginByAdmin([FromBody] Users user)
        {
            var getUser = context.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if(getUser!= null)
            {
                return Ok(getUser.Name);
            }
            return NotFound("Username or password maybe wrong");
        }
    }
}