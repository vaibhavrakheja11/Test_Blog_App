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

        public void HandleLoginEvent()
        {
            AppController.Instance.m_apiHandler.HandleLoginEvent();
        }

        public void HandleSignUpButton()
        {
            this.m_loginPanel.SetActive(false);
            this.m_signUpPanel.SetActive(true);
        }

        public void HandleSignUpEvent()
        {
            AppController.Instance.m_apiHandler.HandleSignUpEvent();
        }

        void OnEnable()
        {
            AppController.Instance.m_apiHandler.GetAuthData += GetAuthData;
        }

        public void GetAuthData(ResponseData authData = null)
        {
            if(authData.token != null)
            {
                AppController.Instance.m_viewController.m_homeScreenController.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
            
        }

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

