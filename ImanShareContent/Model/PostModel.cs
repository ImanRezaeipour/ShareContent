namespace ImanShareContent.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PostModel
    {
        public PostContentType Content { get; set; }
        public PostPriorityType Priority { get; set; }
        public PostCategoryType Category { get; set; }
        public string Id { get; set; }
        public string Photo { get; set; }
        public string Audio { get; set; }
        public string Voice { get; set; }
        public string Video { get; set; }
        public string Caption { get; set; }
        public string Link { get; set; }
        public string ShortLink { get; set; }
        public string Article { get; set; }
        public string ShortArticle { get; set; }
        public string ArticleLength { get; set; }
        public string ArticlePhotoCount { get; set; }
        public string ArticleVideoCount { get; set; }
        public string CaptionWordCount { get; set; }
        public string CaptionBlockWordCount { get; set; }
        public string GetDate { get; set; }
        public string GetTime { get; set; }
    }
}
