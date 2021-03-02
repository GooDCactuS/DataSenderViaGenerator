using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSenderViaGenerator.Models
{
    public class SenderSettings
    {
        public string Url { get; set; }
        public string ServiceName { get; set; }
        public string EndPoint { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Data { get; set; }
    }
}
