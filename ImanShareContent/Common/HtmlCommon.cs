using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImanShareContent.Common
{
    public class HtmlCommon
    {
        public HtmlAgilityPack.HtmlDocument LoadWeb(string webAddress)
        {
            try
            {
                // Download Document -- Critical way -- Timeout Solution
                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument htmlDoc = web.Load(webAddress);
                htmlDoc.OptionFixNestedTags = true;

                return htmlDoc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string DownloadFile(string source, string destination)
        {
            try
            {
                // Download File -- Critical way -- Timeout Solution
                System.Net.WebClient Client = new System.Net.WebClient();
                Client.DownloadFile(source, destination);
                return destination;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
