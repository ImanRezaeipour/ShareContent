using System.Linq;
using ImanShareContent.Model;
using ImanShareContent.API;
using ImanShareContent.Common;
using ImanShareContent.Publish;

namespace ImanShareContent.Content
{
    /// <summary>
    /// 
    /// </summary>
    public class TechnologyContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTechnologyContent()
        {
            // Declaring Log4Net 
            log4net.ILog logger = log4net.LogManager.GetLogger(typeof(string));

            try
            {
                // Load All Content List
                string contentPath = System.Configuration.ConfigurationManager.AppSettings["TechnologyContentPath"].ToString();
                var contentList = new JsonCommon().GetContentList(contentPath + "ContentList.json");
                if (contentList == null)
                    return "OK";

                foreach(var content in contentList)
                {
                    // Load Web Site
                    HtmlAgilityPack.HtmlNode.ElementsFlags["link"] = HtmlAgilityPack.HtmlElementFlag.Closed;
                    var htmlDoc = new HtmlCommon().LoadWeb(content.FeedAddress);
                    if (htmlDoc.DocumentNode == null)
                        return "OK";

                    // Collection of Article (Item) Node
                    HtmlAgilityPack.HtmlNodeCollection itemNode = htmlDoc.DocumentNode.SelectNodes(content.ItemNode);
                    if (itemNode == null)
                        return "OK";

                    // Create Post
                    foreach (HtmlAgilityPack.HtmlNode item in itemNode)
                    {
                        PostModel post = new PostModel();

                        // Link Assignment
                        post.Link = item.ChildNodes["link"].InnerText;
                        if (post.Link == null)
                            continue;

                        // Content Post List
                        var postList = new JsonCommon().GetPostList(contentPath + "PostList.json");
                        if (postList == null)
                            continue;

                        // Check Post is not Exist
                        if (postList.Exists(x => x.Link == post.Link) == true)
                            continue;

                        // Get Short Text
                        post.ShortArticle = new TextCommon().RemoveTags(item.ChildNodes["description"].InnerText);

                        // Load Article
                        HtmlAgilityPack.HtmlDocument articleDoc = new HtmlCommon().LoadWeb(post.Link);
                        if (articleDoc.DocumentNode == null)
                            continue;

                        post.Id = new TextCommon().UniqueId(content.Name);

                        post.Content = PostContentType.Photo;

                        post.Priority = PostPriorityType.Normal;

                        post.Category = PostCategoryType.Technology;

                        post.Photo = new HtmlCommon().DownloadFile(content.HaveRoot == "1" ? articleDoc.DocumentNode.SelectSingleNode(content.PostPhoto).GetAttributeValue("src", null) : content.WebAddress + articleDoc.DocumentNode.SelectSingleNode(content.PostPhoto).GetAttributeValue("src", null), contentPath + @"Media\" + post.Id + ".jpg");
                        if (post.Photo == null)
                            continue;

                        post.Voice = null;

                        post.Video = null;

                        post.Audio = null;

                        post.Caption = new TextCommon().RemoveSpace(articleDoc.DocumentNode.SelectSingleNode(content.PostCaption).InnerText);

                        post.Article = new TextCommon().RemoveTags(articleDoc.DocumentNode.SelectSingleNode(content.PostArticle).InnerText);

                        post.ArticleLength = new TextCommon().WordCount(post.Article);

                        post.ShortLink = new BitlyAPI().Shorten(post.Link, "bit.ly", "txt");
                        if (post.ShortLink == null)
                            continue;

                        post.GetDate = System.DateTime.Now.ToShortDateString();

                        post.GetTime = System.DateTime.Now.ToShortTimeString();

                        post.ArticlePhotoCount = null;

                        post.ArticleVideoCount = null;

                        post.CaptionWordCount = "0";

                        // Add To Content Black Post Queue
                        post.CaptionBlockWordCount = "0";
                        if (post.CaptionBlockWordCount.To<int>() > 0)
                        {
                            var addBlockPost = new JsonCommon().AddPost(contentPath + "BlockPostList.json", post);
                            if (addBlockPost == null)
                                continue;
                            continue;
                        }

                        // Add To Content Post Queue
                        var addPost = new JsonCommon().AddPost(contentPath + "PostList.json", post);
                        if (addPost == null)
                            continue;

                        // Add To Telegram Post Queue
                        var addTelegram = new TelegramPublish().AddTelegramQueue(post);
                        if (addTelegram == null)
                            continue;
                    }
                }
                return "OK";
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }
    }
}
