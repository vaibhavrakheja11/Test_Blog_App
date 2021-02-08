using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BlogApp
{
    
    /// <summary>
    ///  class to generate json serialize web request response
    ///</summary>
    public class Response
    {
        public long StatusCode { get; set; }

        public string Error { get;set; }

        public string Data { get; set; }  

        public Dictionary<string, string> Headers {get; set;} 
    }

    /// <summary>
    ///  class to generate json serialize web request response data
    ///</summary>
    [Serializable] 
    public class ResponseData
    {
        public string token = null;
        public string expires;
        public User user;
        
    }
    
    /// <summary>
    ///  class to generate json serialize web request response for user details
    ///</summary>
    [Serializable] 
    public class User 
    {
        public string id;
        public string role;
        public string name;
        public string email;
        public string createdAt;
        public string updatedAt;
        public string gender;
    }

    /// <summary>
    ///  class to generate json serialize web request response of all blogs data
    ///</summary>
    [Serializable] 
    public class BlogsDataResponse
    {
        public int offset;
        public int limit;
        public int total;
        public int remaining;
        public BlogsData[] data;
    }

    /// <summary>
    ///  class to generate json serialize web request response response of blog details
    ///</summary>
    [Serializable] 
    public class BlogsData
    {
        public int id;
        public string author;

        public string title;
        public string content;
        public string created_at;
        public string updated_at;
        public string published;
        public int views;
        public User author_user;
        public Image image;
        
    }
    
    /// <summary>
    ///  class to generate json serialize web request response response of blog image details
    ///</summary>
    [Serializable] 
    public class Image
    {
        public int id;
        public int blog_post_id;
        public string file_location;
        public int user_profile_id;
        public string original_name;
        public FileSize file_sizes;
    }
    
    /// <summary>
    ///  class to generate json serialize web request response response of blog image sizes
    ///</summary>
    [Serializable] 
    public class FileSize 
    {
       public Original original;
       public Thumb thumb;

    }

    [Serializable] 
    public class Original 
    {
       public string url;
    }

    [Serializable]
    public class Thumb 
    {
       public string url;
    }
}

