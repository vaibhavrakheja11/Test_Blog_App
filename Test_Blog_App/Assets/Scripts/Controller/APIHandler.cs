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
        private const string BASE_URL = "http://cms.iversoft.ca/";

        [SerializeField]
        string m_apiKey = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhcHBfa2V5IjoiYmFzZTY0OlZtRmtjUXZKK1AyYWZCQW9TcmRlSmVOMjVCWE56alBlZGJVaWxl"+
                "MTVISE09IiwiaXNzIjoiaHR0cDovL2Ntcy5pdmVyc29mdC5jYSIsImlhdCI6MTYxMTA4NzY"+
                "yNCwiZXhwIjoxNjExMTE2NDI0LCJuYmYiOjE2MTEwODc2MjQsImp0aSI6IjZWVUU5cDJhVWN3VXpBV3UifQ.OCa6XhRZ5OhQ7H-XgI4WgwWM0rsmBk5GZnfa8zGJAkc";

        [SerializeField]
        List<RequestHeader> m_headers = new List<RequestHeader>();

        ResponseData m_authData = new ResponseData();

        BlogsDataResponse m_blogsDataResponse = new BlogsDataResponse();

        public Action<ResponseData> GetAuthData;

        public Action<BlogsData> GetNewBlogData;

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

            // TODO:: Send parameters in here in place of argument string
            // send a post request
            StartCoroutine(AppController.Instance.m_restWebClient.HttpPost($"{BASE_URL}api/authenticate?email="+AppController.Instance.m_viewController.m_loginController.GetUsername()
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
            form.AddField("name", AppController.Instance.m_viewController.m_loginController.GetName());
            form.AddField("email", AppController.Instance.m_viewController.m_loginController.GetUsername());
            form.AddField("password", AppController.Instance.m_viewController.m_loginController.GetPassword());
        
            // send a post request
            StartCoroutine(AppController.Instance.m_restWebClient.HttpPost($"{BASE_URL}api/users/new", 
            (r) => OnSignUpComplete(r), new List<RequestHeader> {header2,header3 }, form));
        }

        void OnLoginAuthComplete(Response response)
        {   
            if(response.Error!=null)
            {
                Debug.Log(response.Error);
                AppController.Instance.m_viewController.m_errorUIController.ShowError(response.Error.ToString());
            }
            else
            {
                m_authData = JsonUtility.FromJson<ResponseData>(response.Data);
                GetAuthData.Invoke(m_authData);
            }
            
        }

        public ResponseData GetAuthorizedData()
        {
            return m_authData;
        }

        void OnSignUpComplete(Response response)
        {
            if(response.Error!=null)
            {
                Debug.Log(response.Error);
                AppController.Instance.m_viewController.m_errorUIController.ShowError(response.Error.ToString());
            }
            else
            {
                m_authData = JsonUtility.FromJson<ResponseData>(response.Data);
                GetAuthData.Invoke(m_authData);
            }
            
        }
        public void HandleAllBlogsEvent()
        {

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
            StartCoroutine(AppController.Instance.m_restWebClient.HttpGet($"{BASE_URL}api/blog/list", 
            (r) => OnBlogsRecieved(r), new List<RequestHeader> {header2,header3}));
        }

        void OnBlogsRecieved(Response response)
        {
            if(response.Error!=null)
            {
                Debug.Log(response.Error);
                AppController.Instance.m_viewController.m_errorUIController.ShowError(response.Error.ToString());
            }
            else
            {
                m_blogsDataResponse = JsonUtility.FromJson<BlogsDataResponse>(response.Data);
                GenerateBlogCards.Invoke(m_blogsDataResponse);
            }
        }

        public void CreateBlog(byte[] bytes, string title, string content)
        {
            // Create a Web Form
            WWWForm form = new WWWForm();
            form.AddField("title", AppController.Instance.m_viewController.m_createBlogController.GetTitle());
            form.AddField("content", AppController.Instance.m_viewController.m_createBlogController.GetContent());
            form.AddField("published", 1);
            form.AddBinaryData("picture", bytes,"upload.png","image/png");

            // setup the request header
            RequestHeader header2 = new RequestHeader {
                Key = "Accept",
                Value = "application/json"
            };


            RequestHeader header3 = new RequestHeader {
                Key = "Authorization",
                Value = "Bearer "+m_authData.token
            };

            RequestHeader header = new RequestHeader {
                Key = "User",
                Value = m_authData.user.id
            };

            StartCoroutine(AppController.Instance.m_restWebClient.HttpPost($"{BASE_URL}api/blog/new", 
            (r) => OnCreateBlogComplete(r), new List<RequestHeader> {header2,header3,header}, form));
        }

        void OnCreateBlogComplete(Response response)
        {
            if(response.Error!=null)
            {
                Debug.Log(response.Error);
                AppController.Instance.m_viewController.m_errorUIController.ShowError(response.Error.ToString());
            }
            else
            {
                BlogsData blogData = JsonUtility.FromJson<BlogsData>(response.Data);
                if(blogData.image != null )
                {
                    GetNewBlogData.Invoke(blogData);
                }
            }
        }
    }

    public class SignUpCredentials
    {
        public string name;
        public string email;
        public string password;
    }

    public enum HeaderArgument
    {
        API_KEY,
        AUTH_KEY,
        USER
    }
}
