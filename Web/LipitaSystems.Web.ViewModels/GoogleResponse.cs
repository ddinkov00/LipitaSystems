using System;
namespace LipitaSystems.Web.ViewModels
{
    public class GoogleResponse
    {
        public GoogleResponse()
        {
        }

        public bool Success { get; set; }

        public double Score { get; set; }

        public string Action { get; set; }

        public DateTime Challenge_ts { get; set; }

        public string Hostname { get; set; }
    }
}
