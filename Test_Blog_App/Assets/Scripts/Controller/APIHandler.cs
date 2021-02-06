using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.IO;

namespace BlogApp
{    public class APIHandler : MonoBehaviour
    {
        [SerializeField]
        private const string m_baseUrl = "http://cms.iversoft.ca/";

        [SerializeField]
        private const string m_apiKey = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhcHBfa2V5IjoiYmFzZTY0OlZtRmtjUXZKK1AyYWZCQW9TcmRlSmVOMjVCWE56alBlZGJVaWxl"+
                "MTVISE09IiwiaXNzIjoiaHR0cDovL2Ntcy5pdmVyc29mdC5jYSIsImlhdCI6MTYxMTA4NzY"+
                "yNCwiZXhwIjoxNjExMTE2NDI0LCJuYmYiOjE2MTEwODc2MjQsImp0aSI6IjZWVUU5cDJhVWN3VXpBV3UifQ.OCa6XhRZ5OhQ7H-XgI4WgwWM0rsmBk5GZnfa8zGJAkc";
        ResponseData m_authData = new ResponseData();

        BlogsDataResponse m_blogsDataResponse = new BlogsDataResponse();

        public Action<BlogsDataResponse> GenerateBlogCards;

        public void HandleLoginEvent()
        {
            // send a get request
            //StartCoroutine(AppController.Instance.m_restWebClient.HttpGet($"{baseUrl}", (r) => OnRequestComplete(r)));

            // setup the request header
            RequestHeader header = new RequestHeader {
                Key = "Content-Type",
                Value = "application/json"
            };

            RequestHeader header2 = new RequestHeader {
                Key = "Accept",
                Value = "application/json"
            };

            RequestHeader header3 = new RequestHeader {
                Key = "Authorization",
                Value = m_apiKey
            };

            Debug.Log($"{m_baseUrl}api/authenticate?email="+AppController.Instance.m_viewController.m_loginController.GetUsername()
            +"&password="+AppController.Instance.m_viewController.m_loginController.GetPassword());

            // TODO:: Send parameters in here in place of argument string
            // send a post request
            StartCoroutine(AppController.Instance.m_restWebClient.HttpPost($"{m_baseUrl}api/authenticate?email="+AppController.Instance.m_viewController.m_loginController.GetUsername()
            +"&password="+AppController.Instance.m_viewController.m_loginController.GetPassword(), 
            (r) => OnLoginAuthComplete(r), new List<RequestHeader> { header, header2,header3 }));
        }

        public void HandleSignUpEvent()
        {
          
            // setup the request header
            RequestHeader header2 = new RequestHeader {
                Key = "Accept",
                Value = "application/json"
            };

            RequestHeader header3 = new RequestHeader {
                Key = "Authorization",
                Value = m_apiKey
            };

            WWWForm form = new WWWForm();
            form.AddField("name", "Iber2");
            form.AddField("email", "unity02@iversoft.ca");
            form.AddField("password", "Iversoft");
            Debug.Log(JsonUtility.ToJson(new SignUpCredentials { name = "test01", email = "unity01@iversoft.ca" , password = "Ivers0ft" }));

            // TODO:: Send parameters in here in place of argument string
            // send a post request
            StartCoroutine(AppController.Instance.m_restWebClient.HttpPost($"{m_baseUrl}api/users/new", 
            (r) => OnSignUpComplete(r), new List<RequestHeader> {header2,header3 }, form));
        }

        void OnLoginAuthComplete(Response response)
        {   
            m_authData = JsonUtility.FromJson<ResponseData>(response.Data);
        }

        void OnSignUpComplete(Response response)
        {
            Debug.Log(response.Data);
        }
        public void HandleAllBlogsEvent()
        {
            // send a get request
            //StartCoroutine(AppController.Instance.m_restWebClient.HttpGet($"{baseUrl}", (r) => OnRequestComplete(r)));

            // setup the request heade
            RequestHeader header2 = new RequestHeader {
                Key = "Accept",
                Value = "application/json"
            };

            RequestHeader header3 = new RequestHeader {
                Key = "Authorization",
                Value = m_apiKey
            };

            // TODO:: Send parameters in here in place of argument string
            // send a post request
            StartCoroutine(AppController.Instance.m_restWebClient.HttpGet($"{m_baseUrl}api/blog/list", 
            (r) => OnBlogsRecieved(r), new List<RequestHeader> {header2,header3}));
        }

        void OnBlogsRecieved(Response response)
        {
            Debug.Log(response.Data);
            m_blogsDataResponse = JsonUtility.FromJson<BlogsDataResponse>(response.Data);
            GenerateBlogCards.Invoke(m_blogsDataResponse);

        }

        public void CreateBlog(byte[] bytes, string title, string content)
        {
            // For testing purposes, also write to a file in the project folder
            File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
            


            // Create a Web Form
            WWWForm form = new WWWForm();
            form.AddField("title", "Iber04");
            form.AddField("content", "Iber04");
            form.AddField("published", 1);
            form.AddBinaryData("picture", bytes,"name.png","image/png");

            // setup the request header
            RequestHeader header2 = new RequestHeader {
                Key = "Accept",
                Value = "application/json"
            };


            RequestHeader header3 = new RequestHeader {
                Key = "Authorization",
                Value = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjEyOCwiaXNzIjoiaHR0cDovL2Ntcy5pdmVyc29mdC5jYS9hcGkvYXV0aGVudGljYXRlIiwiaWF"+
                "0IjoxNjEyNTk0ODQ0LCJleHAiOjE2MTI2MjM2NDQsIm5iZiI6MTYxMjU5NDg0NCwianRpIjoiSlZTemFxYjVNc0RQemNRNyJ9.BviMm1szjz5w7x7QV6HO-xH-EGehxlrhGe1U2IqUsOU"
            };

            RequestHeader header = new RequestHeader {
                Key = "User",
                Value = "128"
            };

            StartCoroutine(AppController.Instance.m_restWebClient.HttpPost($"{m_baseUrl}api/blog/new", 
            (r) => OnCreateBlogComplete(r), new List<RequestHeader> {header2,header3,header}, form));
        }

        void OnCreateBlogComplete(Response response)
        {
            Debug.Log(response.Data);
        }
    }

    public class SignUpCredentials
    {
        public string name;
        public string email;
        public string password;
    }
}
