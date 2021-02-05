using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BlogApp
{    public class LoginController : MonoBehaviour
    {
        string m_username;

        string m_password;

        public void SetUsername(string username)
        {
            m_username = username;
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

        public void HandleLoginEvent()
        {
            AppController.Instance.m_apiHandler.HandleLoginEvent();
        }

        public void HandleSignUpEvent()
        {
            AppController.Instance.m_apiHandler.HandleSignUpEvent();
        }
    }
}

