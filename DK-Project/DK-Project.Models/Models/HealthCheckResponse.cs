using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Responses;

namespace DK_Project.Models.Models
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<IndividualHealthCheckResponse> HealthCheckList { get; set; }
        public TimeSpan HealthCheckDuration { get; set; }
    }
}
