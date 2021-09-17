using Kalabean.Domain.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Services
{
    public class SMSService : ISMSService
    {
        public async Task<string> SendPattern(string Code, string ToNum)
        {
            Kalabean.Domain.Requests.SMS.SendPatternRequest smsRequest = new Domain.Requests.SMS.SendPatternRequest()
            {
                inputData = new Domain.Requests.SMS.InputData() { SMSText = Code },
                toNum = ToNum
            };
            var client = new RestClient("http://188.0.240.110/api/select");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", smsRequest.ConvertToJson()
                , ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            return response.Content;
        }
    }
}
