namespace WEBPCTSV.Helpers.Common
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public static class JsonUtils
    {
        public static string GetJsonFromLink(string url, string method, Dictionary<string, string> parameter)
        {
            WebClient webClient = new WebClient();
            foreach (KeyValuePair<string, string> value in parameter)
            {
                webClient.QueryString.Add(value.Key, value.Value);
            }

            var data = webClient.UploadValues(url, method, webClient.QueryString);

            // data here is optional, in case we recieve any string data back from the POST request.
            return Encoding.UTF8.GetString(data);
        }
    }
}