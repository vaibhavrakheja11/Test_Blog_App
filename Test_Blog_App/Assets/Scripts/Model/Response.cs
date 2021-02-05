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
        public string token;
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
        public string m_createdAt;
        public string m_updatedAt;
        public string gender;
    }

    
}

