using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevops.Models
{
    public class IpModel
    {
        public String ip { get; set; }
        public String city { get; set; }
        public String region { get; set; }
        public String country { get; set; }
        public String loc { get; set; }
        public String org { get; set; }
        public String timezone { get; set; }
    }
}
