using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WEBPCTSV.Helpers.Common
{
    public static class JsonUtils
    {
        public static string GetJsonFromLink(string url, string method, Dictionary<string,string> parameter)
        {
            WebClient webClient = new WebClient();
            foreach (KeyValuePair<string, string> value in parameter)
            {
                webClient.QueryString.Add(value.Key, value.Value);
            }
            var data = webClient.UploadValues(url, method, webClient.QueryString);

            // data here is optional, in case we recieve any string data back from the POST request.
            return UnicodeEncoding.UTF8.GetString(data);
        }
    }
}