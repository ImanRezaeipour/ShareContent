namespace ImanShareContent.API
{
    /// <summary>
    /// 
    /// </summary>
    public class TelegramAPI
    {
        /// <summary>
        /// Use this method to send photos. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatID">Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
        /// <param name="photo">Photo to send. You can either pass a file_id as String to resend a photo that is already on the Telegram servers, or upload a new photo using multipart/form-data.</param>
        /// <param name="caption">Photo caption (may also be used when resending photos by file_id).</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns></returns>
        public string SendPhoto(string chatID, string photo, string caption="", string replyToMessageId="", string replyMarkup="")
        {
            try {
                string botToken = System.Configuration.ConfigurationManager.AppSettings["BotToken"].ToString();
                string webAddress = "https://api.telegram.org/bot" + botToken + "/sendPhoto";
                System.Net.HttpWebRequest requestToServerEndpoint = (System.Net.HttpWebRequest) System.Net.WebRequest.Create(webAddress);

                string boundaryString = "----RandomBoundary";

                requestToServerEndpoint.Method = System.Net.WebRequestMethods.Http.Post;
                requestToServerEndpoint.ContentType = "multipart/form-data; boundary=" + boundaryString;
                requestToServerEndpoint.KeepAlive = true;
                requestToServerEndpoint.Credentials = System.Net.CredentialCache.DefaultCredentials;

                System.IO.MemoryStream postDataStream = new System.IO.MemoryStream();
                System.IO.StreamWriter postDataWriter = new System.IO.StreamWriter(postDataStream);

                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", "chat_id", chatID);

                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", "caption", caption);

                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\" \r\nContent-Type: {2}\r\n\r\n", "photo", System.IO.Path.GetFileName(photo), System.IO.Path.GetExtension(photo));

                postDataWriter.Flush();

                System.IO.FileStream fileStream = new System.IO.FileStream(photo, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    postDataStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();

                postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
                postDataWriter.Flush();

                requestToServerEndpoint.ContentLength = postDataStream.Length;

                using (System.IO.Stream s = requestToServerEndpoint.GetRequestStream())
                {
                    postDataStream.WriteTo(s);
                }
                postDataStream.Close();

                System.Net.WebResponse response = requestToServerEndpoint.GetResponse();
                System.IO.StreamReader responseReader = new System.IO.StreamReader(response.GetResponseStream());
                string replyFromServer = responseReader.ReadToEnd();
                return replyFromServer;
            }
            catch(System.Exception ex)
            {
                return "";
            }
        }
    }
}
