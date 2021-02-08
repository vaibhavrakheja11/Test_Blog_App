using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BlogApp
{
    
    /// <summary>
    ///  Serializable class to generate headers
    ///</summary>
    [Serializable] 
    public class RequestHeader
    {
        public string Key { get;set; }

        public string Value { get; set; }  

        public HeaderArgument headerType; 
    }
    
}

