namespace ImanShareContent.API
{
    /// <summary>
    /// 
    /// </summary>
    public class BitlyAPI
    {
        /// <summary>
        /// Given a long URL, returns a Bitlink.
        /// </summary>
        /// <param name="longURL">a long URL to be shortened (example: http://betaworks.com\/).</param>
        /// <param name="domain">the short domain to use; either bit.ly, j.mp, or bitly.com or a custom short domain. The default for this parameter is the short domain selected by each user in their bitly account settings. Passing a specific domain via this parameter will override the default settings.</param>
        /// <param name="format">json, xml, txt. Default: json.</param>
        /// <returns></returns>
        public string Shorten(string longURL, string domain = "bit.ly", string format="json")
        {
            try {
                var request = (System.Net.HttpWebRequest) System.Net.WebRequest.Create("https://api-ssl.bitly.com/v3/shorten");

                string bitlyToken= System.Configuration.ConfigurationManager.AppSettings["BitlyToken"].ToString();
                string postData = "access_token=" + bitlyToken + "&longUrl=" + longURL + "&domain" + domain + "&format=" + format;

                var data = System.Text.Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (System.Net.HttpWebResponse) request.GetResponse();

                var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString.Replace("\n","");
            }
            catch(System.Exception ex)
            {
                return "";
            }
        }
    }
}
