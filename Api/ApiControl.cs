using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public class StateControl
    {
        public static async Task<CasesModel> LoadCases(string state,string type)
        {
            string url;
            
            if (type=="CASES")    url = "https://api.rootnet.in/covid19-in/stats/latest";
            else if(type=="BEDS") url = "https://api.rootnet.in/covid19-in/hospitals/beds";
            else                  url = "https://api.cowin.gov.in/api/v1/reports/v2/getPublicReports";
            Console.WriteLine(url);
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync(url);
                CasesModel cases = JsonConvert.DeserializeObject<CasesModel>(json);
                return cases;
            }
        }
    }
}
