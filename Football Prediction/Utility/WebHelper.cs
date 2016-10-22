using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Football_Prediction.Utility
{
    public class WebHelper
    {
        public WebHelper()
        {

        }
        public static async Task<string> GetStringFromRequest(string url)
        {
            string result = string.Empty;
            HttpClient client = new HttpClient();

            result =   await client.GetStringAsync(url);
            return result;
        }
    }
}
