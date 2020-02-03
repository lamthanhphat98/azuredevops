using AutoMapper;
using AzureDevops.Models;
using AzureDevops.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevops.Mapper
{
    public class AutoMapperEntity:Profile
    {
        public AutoMapperEntity()
        {
            CreateMap<WeatherClientModel, Weather>();
            CreateMap<WeatherClientModel, WeatherDetail>();

        }
    }
}
