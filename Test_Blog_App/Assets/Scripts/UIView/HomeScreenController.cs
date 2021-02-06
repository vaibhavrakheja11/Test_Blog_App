using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlogApp
{
    public class HomeScreenController : MonoBehaviour
    {
        

        RawImage m_displayPicture;

        public GameObject Content;
        public GameObject scrollItemPrefab;

        BlogsDataResponse m_blogsDataResponse;

        [SerializeField]
        List<GameObject> m_gameScreenLoader = new List<GameObject>();

        Queue<GameObject> m_currentTip = new Queue<GameObject>();
        
        [SerializeField]
        GameObject LoaderObject;

        [SerializeField]
        GameObject Footer;

        GameObject currentObject;

        public void Start()
        {
            AppController.Instance.m_apiHandler.HandleAllBlogsEvent();
            
            AppController.Instance.m_apiHandler.GenerateBlogCards += GenerateCards;

            foreach(GameObject loader in m_gameScreenLoader)
            {
                m_currentTip.Enqueue(loader);
            }
            currentObject = m_currentTip.Peek();
            currentObject.SetActive(true);
        }

        public void GenerateCards(BlogsDataResponse blogsDataResponse)
        {
            m_blogsDataResponse =  blogsDataResponse;
        }

         IEnumerator setImage(string url,BlogsData blogData, SpawnItem spawnItem) 
         {
            RawImage displayPicture = spawnItem.rawImage;
            WWW www = new WWW(url);
            yield return www;

            Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texture);
            displayPicture.texture = texture;

            if(AppController.Instance.m_viewController.m_blogScreenController.gameObject.active && blogData.id == AppController.Instance.m_viewController.m_blogScreenController.blogId)
            {
                spawnItem.HandleBlogDetailsEvent();
            }

            spawnItem.Spinner.SetActive(false);
            spawnItem.rawImage.gameObject.SetActive(true);

            www.Dispose();
            www = null;
        }

        public void GenerateItem(GameObject scrollContent, ScrollScript scrollScript, int num)
        {   
            if(LoaderObject.active)
            {
                LoaderObject.SetActive(false);
                Footer.SetActive(true);
            }


            GameObject scrollItemObj = Instantiate(scrollItemPrefab);
            scrollItemObj.transform.SetParent(scrollContent.transform, false);
            SpawnItem spawnItem = scrollItemObj.GetComponent<SpawnItem>();
            spawnItem.m_blogData = m_blogsDataResponse.data[num];
            if(m_blogsDataResponse.data[num].image != null)
            {
               try 
                {
                    StartCoroutine(setImage(m_blogsDataResponse.data[num].image.file_sizes.original.url, m_blogsDataResponse.data[num], spawnItem));
                }
                catch
                {

                }
            }
        }

        void OnDisable()
        {
            AppController.Instance.m_apiHandler.GenerateBlogCards -= GenerateCards;
        }

        public void NextTip()
        {
            currentObject = m_currentTip.Peek();
            currentObject.SetActive(false);
            m_currentTip.Enqueue(m_currentTip.Dequeue());
            currentObject = m_currentTip.Peek();
            currentObject.SetActive(true);
        }

        public void ShowDetails(BlogsData blogsData, Texture texture)
        {
            if(!AppController.Instance.m_viewController.m_blogScreenController.gameObject.active)
            {
                AppController.Instance.m_viewController.m_blogScreenController.gameObject.SetActive(true);
            }
            AppController.Instance.m_viewController.m_blogScreenController.ShowDetails(blogsData, texture);
        }


        
    }
}

