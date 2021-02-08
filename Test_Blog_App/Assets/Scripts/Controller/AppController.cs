using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BlogApp
{
    /// <summary>
    ///  Singleton class to validate the referenced class
    ///  This is the Controller for this MVC architecture
    ///  All UI controllers to access models via an instance of this class
    /// Models to provide information to UI view controller using an instance of this class
    ///</summary>

    public class AppController : MonoBehaviour
    {
        
        private static AppController _instance;

        public static AppController Instance { get { return _instance; } }
        
        // Get instance of API Handler Model
        public APIHandler m_apiHandler;
        
        // Get instance of ViewController class 
        public ViewController m_viewController;

        // Get instance of Rest Web Client class
        public RestWebClient m_restWebClient;

        /// <summary>
        ///  Singleton class to validate the referenced class
        ///  Set Up instance of this class
        ///  Destroy any additonal instance
        ///  </summary>
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }
    }
}

