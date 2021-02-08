using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BlogApp
{
    public class Response
    {
        public long StatusCode { get; set; }

        public string Error { get;set; }

        public string Data { get; set; }  

        public Dictionary<string, string> Headers {get; set;} 
    }

    [Serializable] 
    public class ResponseData
    {
        public string token = null;
        public string expires;
        public User user;
        
    }

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

    [Serializable] 
    public class BlogsDataResponse
    {
        public int offset;
        public int limit;
        public int total;
        public int remaining;
        public BlogsData[] data;
    }


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

