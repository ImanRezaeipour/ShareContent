using System.Linq;
using ImanShareContent.Model;
using ImanShareContent.API;

namespace ImanShareContent.Publish
{
    /// <summary>
    /// 
    /// </summary>
    public class TelegramPublish
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToTechnologyGroup()
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(typeof(string));  //Declaring Log4Net  

            string telegramPublishPath = System.Configuration.ConfigurationManager.AppSettings["TelegramPublishPath"].ToString();
            string telegramTechnologyGroupID = System.Configuration.ConfigurationManager.AppSettings["TelegramTechnologyGroupID"].ToString();
            string telegramTechnologyGroupName = System.Configuration.ConfigurationManager.AppSettings["TelegramTechnologyGroupName"].ToString();
            //string telegramLoggerPath = System.Configuration.ConfigurationManager.AppSettings["telegramLoggerPath"].ToString();
            try
            {
                // Send Post Queue To Telegram Technology Channel
                var telegramQueuePath = telegramPublishPath + "TechnologyPostQueue.json";
                var telegramJsonData = System.IO.File.ReadAllText(telegramQueuePath);
                // Check Telegram Queue is not Empty
                if (telegramJsonData == null || telegramJsonData == "[]")
                {
                    logger.Error("List is Empty.");
                    return "OK";
                }

                var telegramPostList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<PostModel>>(telegramJsonData) ?? new System.Collections.Generic.List<PostModel>();

                // First Selection Method : Max Article Length
                PostModel post = telegramPostList.OrderByDescending(x=>x.ArticleLength).FirstOrDefault();

                // Two Selection Method : High Post Priority
                if(post == null)
                {

                }

                // Three Selection Method : Max Caption White Word Count
                if (post == null)
                {

                }

                // Four Selection Method : Max Article Image Count 
                if (post == null)
                {

                }

                // Anyway : First Post in Queue
                if (post == null)
                {
                    post = telegramPostList.FirstOrDefault();
                }

                TelegramAPI telegram = new TelegramAPI();
                // Send Photo Post
                if (post.Content == PostContentType.Photo)
                {
                    var caption = post.Caption + "\n\n" + post.ShortArticle + "\n\n🔗 " + post.ShortLink.Replace("http://", "") + "\n\n" + telegramTechnologyGroupName;
                    telegram.SendPhoto(telegramTechnologyGroupID, post.Photo, caption);
                }

                // Delete Post
                telegramPostList.Remove(post);
                telegramJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(telegramPostList);
                System.IO.File.WriteAllText(telegramQueuePath, telegramJsonData);

                return "OK";
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.ToString());
                return "BAD";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string AddTelegramQueue(PostModel post)
        {
            try
            {
                string telegramPublishPath = System.Configuration.ConfigurationManager.AppSettings["TelegramPublishPath"].ToString();
                var telegramQueuePath = "";
                if (post.Category==PostCategoryType.Technology)
                    telegramQueuePath = telegramPublishPath + "TechnologyPostQueue.json";
                var telegramJsonData = System.IO.File.ReadAllText(telegramQueuePath);
                var telegramPostList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<PostModel>>(telegramJsonData) ?? new System.Collections.Generic.List<PostModel>();
                if (telegramPostList.Exists(x => x.Link == post.Link) == true)
                    return "OK";
                telegramPostList.Add(post);
                telegramJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(telegramPostList);
                System.IO.File.WriteAllText(telegramQueuePath, telegramJsonData);
                return "OK";
            }
            catch (System.Exception ex)
            {
                return "BAD";
            }
        }
    }
}
