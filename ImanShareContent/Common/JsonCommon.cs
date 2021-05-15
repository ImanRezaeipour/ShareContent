using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImanShareContent.Model;

namespace ImanShareContent.Common
{
    public class JsonCommon
    {
        public List<PostModel> GetPostList(string path)
        {
            try {
                var queuePath = path;
                var jsonData = System.IO.File.ReadAllText(queuePath);
                var postList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<PostModel>>(jsonData) ?? new System.Collections.Generic.List<PostModel>();
                return postList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<ContentModel> GetContentList(string path)
        {
            try
            {
                var queuePath = path;
                var jsonData = System.IO.File.ReadAllText(queuePath);
                var contentList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<ContentModel>>(jsonData) ?? new System.Collections.Generic.List<ContentModel>();
                return contentList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string AddPost(string path, PostModel post)
        {
            try
            {
                var postList = GetPostList(path);
                postList.Add(post);
                var queuePath = path;
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(postList);
                System.IO.File.WriteAllText(queuePath, jsonData);
                return "OK";
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
