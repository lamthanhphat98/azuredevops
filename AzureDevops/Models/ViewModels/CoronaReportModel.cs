using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevops.Models.ViewModels
{
    public class CoronaReportModel
    {
        public String Country { get; set; }
        public CaseModel Cases { get; set; }
        public DeathModel Deaths { get; set; }
    }
}
