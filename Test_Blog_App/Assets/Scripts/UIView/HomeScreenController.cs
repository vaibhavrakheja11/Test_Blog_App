using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlogApp
{
    public class HomeScreenController : MonoBehaviour
    {
        

        List<RawImage> m_displayPictures = new List<RawImage>();

        public GameObject Content;
        public void Start()
        {
            AppController.Instance.m_apiHandler.HandleAllBlogsEvent();
            AppController.Instance.m_apiHandler.GenerateBlogCards += GenerateCards;
            
        
        }

        public void GenerateCards(BlogsDataResponse m_blogsDataResponse)
        {
            foreach (RawImage raw in Content.GetComponentsInChildren<RawImage>())
            {
                m_displayPictures.Add(raw);
            }
            StartCoroutine(setImage(m_blogsDataResponse.data[1].image.file_sizes.original.url));
        }

         IEnumerator setImage(string url) {
           

            WWW www = new WWW(url);
            yield return www;

            Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, false);
            // calling this function with StartCoroutine solves the problem
            Debug.Log("Why on earh is this never called?");

            www.LoadImageIntoTexture(texture);
            m_displayPictures[0].texture = texture;
            www.Dispose();
            www = null;
        }

        void OnDisable()
        {
            AppController.Instance.m_apiHandler.GenerateBlogCards -= GenerateCards;
        }
        
    }
}

