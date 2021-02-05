using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BlogApp
{
    public class AppController : MonoBehaviour
    {
        private static AppController _instance;

        public static AppController Instance { get { return _instance; } }

        public APIHandler m_apiHandler;
        
        public ViewController m_viewController;

        public RestWebClient m_restWebClient;

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

