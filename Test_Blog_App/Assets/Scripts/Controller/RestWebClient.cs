using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace BlogApp
{
    public class RestWebClient : MonoBehaviour
    {
        const string defaultContentType = "application/json";

        /// <summary>
        ///Generic template for a HTTP Get request to the restful GET API
        /// </summary>
        public IEnumerator HttpGet(string url, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
        {
            // create object og UnityWebRequest
            using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Set headers
                if(headers != null)
                {
                    foreach (RequestHeader header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }
                
                // wait for result
                yield return webRequest.SendWebRequest();
                
                // subsequent checks to determine network error
                if(webRequest.isNetworkError){
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }
                
                // subsequent checks to return network response
                if(webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Data = data
                    });
                }
            }
        }

        /// <summary>
        ///Generic template for a HTTP Post request to the restful POST API
        /// </summary>
        public IEnumerator HttpPost(string url, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null, WWWForm form = null)
        {
            // create object og UnityWebRequest
            using(UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
            {
                // Set headers
                if(headers != null)
                {
                    foreach (RequestHeader header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.uploadHandler.contentType = defaultContentType;               

                // wait for result
                yield return webRequest.SendWebRequest();

                // subsequent checks to determine network error
                if(webRequest.isNetworkError)
                {
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }

                // subsequent checks to return network response
                if(webRequest.isDone)
                {
                    
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Data = data
                    });
                }
            }
        }
    }
}

