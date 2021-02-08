using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BlogApp
{    
    public class LoginController : MonoBehaviour
    {
        [SerializeField]
        GameObject m_loginPanel;

        [SerializeField]
        GameObject m_signUpPanel;

        string m_name;
        string m_username;

        string m_password;

        public void SetUsername(string username)
        {
            m_username = username;
        }

        public void SetName(string name)
        {
            m_name = name;
        }

        public void SetPassword(string password)
        {
            m_password = password;
        }

        public string GetPassword()
        {
            return m_password;
        }

        public string GetUsername()
        {
            return m_username;
        }

        public string GetName()
        {
            return m_name;
        }

        /// <summary>
        /// This method is responsible to handle login button
        /// </summary>
        public void HandleLoginEvent()
        {
            AppController.Instance.m_apiHandler.HandleLoginEvent();
        }

        /// <summary>
        /// This method is responsible to open sign up page
        /// </summary>
        public void HandleSignUpButton()
        {
            this.m_loginPanel.SetActive(false);
            this.m_signUpPanel.SetActive(true);
        }

        /// <summary>
        /// This method is responsible to handle Sign up button event
        /// </summary>
        public void HandleSignUpEvent()
        {
            AppController.Instance.m_apiHandler.HandleSignUpEvent();
        }

        
        void OnEnable()
        {
            AppController.Instance.m_apiHandler.GetAuthData += GetAuthData;
        }

        /// <summary>
        /// This method is responsible to handle auth data
        /// </summary>
        public void GetAuthData(ResponseData authData = null)
        {
            if(authData.token != null)
            {
                AppController.Instance.m_viewController.m_homeScreenController.gameObject.SetActive(true);
                AppController.Instance.m_viewController.m_homeScreenController.RefreshHomeScreen();
                this.gameObject.SetActive(false);
            }
            
        }

        /// <summary>
        /// This method is responsible to handle cancel button 
        /// </summary>
        public void HandleCancelLoginSignupEvent()
        {
            AppController.Instance.m_viewController.m_homeScreenController.gameObject.SetActive(true);
            AppController.Instance.m_viewController.m_loginController.m_loginPanel.gameObject.SetActive(true);
            AppController.Instance.m_viewController.m_loginController.m_signUpPanel.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

        void OnDisable()
        {
            AppController.Instance.m_apiHandler.GetAuthData -= GetAuthData;
        }


    }
}

