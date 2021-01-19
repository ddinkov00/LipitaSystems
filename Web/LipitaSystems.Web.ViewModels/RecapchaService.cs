using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LipitaSystems.Web.ViewModels
{
    public class RecapchaService
    {
        private RecapchaSettings settings;

        public RecapchaService(IOptions<RecapchaSettings> options)
        {
            this.settings = options.Value;
        }

        public async Task<GoogleResponse> RecVer(string token)
        {
            var data = new RecapchaData
            {
                Response = token,
                Secret = "6LfNGDIaAAAAALpxzmvUtekHcJQp4sisi8EfQUiE",
            };

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={data.Secret}&response={data.Response}");
            var respRec = JsonConvert.DeserializeObject<GoogleResponse>(response);
            return respRec;
        }
    }
}
